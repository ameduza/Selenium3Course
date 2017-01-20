using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Lesson10
{
    public class ProductPage : Base
    {
        private MainPage _mainPage;

        //private readonly By _productNameLocator = By.CssSelector(@"h1.title");
        [FindsBy(How = How.CssSelector, Using = @"h1.title")]
        private IWebElement _productName { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = @"div#box-product div.manufacturer img")]
        private IWebElement _productManufacturer { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"div#box-product div.price-wrapper s.regular-price")]
        private IWebElement _productPriceRegular { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"div#box-product div.price-wrapper strong.campaign-price")]
        private IWebElement _productPriceCampaign { get; set; }

        private By _productSizeSelectLocator = By.CssSelector(@"select[name=options\[Size\]]");
        //[FindsBy(How = How.CssSelector, Using = @"select[name=options\[Size\]]")]
        //private IWebElement _productSizeSelectLocator { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"button[name=add_cart_product]")]
        private IWebElement _addToCartButton { get; set; }

        private By _quantityLocator = By.CssSelector(@"input[name=quantity]");

        
        public ProductPage(EventFiringWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            PageFactory.InitElements(_driver, this);
            _mainPage = new MainPage(_driver, _wait);
            ;
        }

        public Product GetProductDetails()
        {
            var p = new Product
            {
                Name = _productName.Text,
                Manufacturer = _productManufacturer.GetAttribute(@"title"),
 
                PriceRegular = _productPriceRegular.Text,
                PriceRegularColor = _productPriceRegular.GetCssValue("color"),      // should be == '#777' (gray)
                PriceRegularStyle = _productPriceRegular.GetAttribute("tagName"),   // should be == s
                PriceRegularSize = float.Parse(_productPriceRegular.GetCssValue("font-size").Remove(2)), // remove 'px', 'em' etc.
                
                PriceCampaign = _productPriceCampaign.Text,
                PriceCampaignColor = _productPriceCampaign.GetCssValue("color"),      // should be == '#c00' (red)
                PriceCampaignStyle = _productPriceCampaign.GetAttribute("tagName"),   // should be == strong
                PriceCampaignSize = float.Parse(_productPriceCampaign.GetCssValue("font-size").Remove(2)) // remove 'px', 'em' etc.

            };
            return p;
        }

        public void AddToCart(int quantity)
        {
            if (!IsElementNotPresent(_driver, _productSizeSelectLocator))
            {
                var size = new SelectElement(_driver.FindElement(_productSizeSelectLocator));
                size.SelectByIndex(2);
            }

            var cartItemsQuantityInitialElement = _mainPage.GetCartItemsQuantityElement();
            var initialQount = _mainPage.GetCartItemsQuantity();
            var targetQount = initialQount + quantity;
            SetText(_driver.FindElement(_quantityLocator), quantity.ToString());  
            _addToCartButton.Click();
            
            _wait.Until(ExpectedConditions.TextToBePresentInElement(cartItemsQuantityInitialElement, targetQount.ToString() ));
        }

        public MainPage OpenMainPage()
        {
            return _mainPage.OpenHomePage(_driver, _wait);
        }
    }
}

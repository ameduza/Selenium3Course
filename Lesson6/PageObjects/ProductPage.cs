using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Lesson6
{
    public class ProductPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        //private readonly By _productNameLocator = By.CssSelector(@"h1.title");
        [FindsBy(How = How.CssSelector, Using = @"h1.title")]
        private IWebElement _productName { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = @"div#box-product div.manufacturer img")]
        private IWebElement _productManufacturer { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"div#box-product div.price-wrapper s.regular-price")]
        private IWebElement _productPriceRegular { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"div#box-product div.price-wrapper strong.campaign-price")]
        private IWebElement _productPriceCampaign { get; set; }
        
        
        public ProductPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            PageFactory.InitElements(_driver, this);
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
    }
}

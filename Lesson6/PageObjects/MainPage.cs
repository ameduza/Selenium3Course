using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Lesson6
{
    public class MainPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private readonly By _productsListLocator = By.CssSelector("li.product");
        private readonly By _productsStickerLocator = By.CssSelector("div.sticker");
        [FindsBy(How = How.CssSelector, Using = @"div#box-campaigns a.link")]
        private IWebElement _productsCampaigns { get; set; }

        private readonly By _productNameLocator = By.CssSelector(@"div.name");
        private readonly By _productManufacturerLocator = By.CssSelector(@"div.manufacturer");
        private readonly By _productPriceRegularLocator = By.CssSelector(@"div.price-wrapper s.regular-price");
        private readonly By _productPriceCampaignLocator = By.CssSelector(@"div.price-wrapper strong.campaign-price");
        

        public MainPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            PageFactory.InitElements(_driver, this);
        }

        public int GetProductsCount()
        {
            return _driver.FindElements(_productsListLocator).Count;
        }

        public IList<IWebElement> GetProductsList(IWebDriver driver)
        {
            return new List<IWebElement>(driver.FindElements(_productsListLocator));
        }

        public int GetProductStickerCount(IWebElement product)
        {
            return product.FindElements(_productsStickerLocator).Count;
        }


        public ProductPage OpenFirstCampaignsProduct()
        {
            _productsCampaigns.Click();
            return new ProductPage(_driver, _wait);
        }
        public Product GetCampaignsFirstProductDetails()
        {
            var p = new Product
            {
                Name = _productsCampaigns.FindElement(_productNameLocator).GetAttribute("textContent"),
                Manufacturer = _productsCampaigns.FindElement(_productManufacturerLocator).GetAttribute("textContent"),
                
                PriceRegular = _productsCampaigns.FindElement(_productPriceRegularLocator).Text,
                PriceRegularColor = _productsCampaigns.FindElement(_productPriceRegularLocator).GetCssValue("color"),      // should be == '#777' (gray)
                PriceRegularStyle = _productsCampaigns.FindElement(_productPriceRegularLocator).GetAttribute("tagName"),   // should be == s
                PriceRegularSize = float.Parse(_productsCampaigns.FindElement(_productPriceRegularLocator).GetCssValue("font-size").Remove(2)), // remove 'px', 'em' etc.
                
                PriceCampaign = _productsCampaigns.FindElement(_productPriceCampaignLocator).Text,
                PriceCampaignColor = _productsCampaigns.FindElement(_productPriceCampaignLocator).GetCssValue("color"),      // should be == '#c00' (red)
                PriceCampaignStyle = _productsCampaigns.FindElement(_productPriceCampaignLocator).GetAttribute("tagName"),   // should be == strong
                PriceCampaignSize = float.Parse(_productsCampaigns.FindElement(_productPriceCampaignLocator).GetCssValue("font-size").Remove(2)) // remove 'px', 'em' etc.

            };
            return p;
        }
    }
}

using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Lesson3.PageObjects
{
    public class MainPage
    {
        private IWebDriver _driver;
        private readonly By _productsListLocator = By.CssSelector("li.product");
        private readonly By _productsStickerLocator = By.CssSelector("div.sticker");

        public MainPage(IWebDriver driver)
        {
            _driver = driver;
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
    }
}

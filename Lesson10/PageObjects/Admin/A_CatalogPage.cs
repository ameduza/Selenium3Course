using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Lesson10
{
    public class A_CatalogPage : Base
    {
        [FindsBy(How = How.CssSelector, Using = @"div > a.button:nth-child(2)")]
        private IWebElement _addNewProductLink { get; set; }

        [FindsBy(How = How.XPath, Using = @"//table[@class='dataTable']//tr[@class='row']//td[3]//a")]
        private IList<IWebElement> _productNameColumn { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"input[name=query]")]
        private IWebElement _search { get; set; }

        public A_CatalogPage(EventFiringWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            Init();
        }
        private void Init()
        {
            PageFactory.InitElements(_driver, this);
        }

        public A_ProductPage OpenAddNewProduct()
        {
            _addNewProductLink.Click();
            _wait.Until(ExpectedConditions.TitleIs("Add New Product | My Store"));
            return new A_ProductPage(_driver, _wait);
        }

        public IList<string> GetProductNamesList()
        {
            var productNameColumn = _productNameColumn;
            return productNameColumn.Select(c => c.GetAttribute("outerText")).ToList();
        }

        public IList<IWebElement> GetProductsList()
        {
            var productNameColumn = _productNameColumn;
            return productNameColumn;
        }

        public A_CatalogPage SearchProduct(string productName)
        {
            Base.SetText(_search, productName); 
            _search.SendKeys(Keys.Return);
            return new A_CatalogPage(_driver, _wait);
        }
    }
}

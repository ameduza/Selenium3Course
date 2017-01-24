using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;

namespace Lesson10
{
    public class A_ProductPage : Base
    {
        public readonly By h1Locator = By.XPath("//h1");

#region Navigation tabs
        [FindsBy(How = How.CssSelector, Using = @"ul.index li:nth-child(1) a")]
        private IWebElement _tabGeneral { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"ul.index li:nth-child(2) a")]
        private IWebElement _tabInformation { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"ul.index li:nth-child(4) a")]
        private IWebElement _tabPrices { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"button[name=save]")]
        private IWebElement _saveButton { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"button[name=cancel]")]
        private IWebElement _cancelButton { get; set; }

#endregion
#region Locators\Elements General
        [FindsBy(How = How.CssSelector, Using = @"input[name=status]")]
        private IList<IWebElement> _statusRadioButtons { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"input[name=name\[en\]]")]
            private IWebElement _name { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"input[name=code]")]
            private IWebElement _code { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"tr:nth-child(1) tr:nth-child(1) input[name=categories\[\]]")]
        private IWebElement _categoriesRoot { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"tr:nth-child(1) tr:nth-child(2) input[name=categories\[\]]")]
        private IWebElement _categoriesRubberDuck { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"tr:nth-child(1) tr:nth-child(3) input[name=categories\[\]]")]
        private IWebElement _categoriesSubcategory { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"select[name=default_category_id]")]
        private IWebElement _defaultCategory { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"tr:nth-child(2) input[name=product_groups\[\]]")]
            private IWebElement _productGroupFemale { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"tr:nth-child(3) input[name=product_groups\[\]]")]
            private IWebElement _productGroupMale { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"tr:nth-child(4) input[name=product_groups\[\]]")]
            private IWebElement _productGroupUnisex { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"input[name=quantity]")]
            private IWebElement _quantity { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"select[name=sold_out_status_id]")]
            private IWebElement _soldOutStatus { get; set; }
        //TODO: Upload Image ???
        [FindsBy(How = How.CssSelector, Using = @"input[name=new_images\[\]]")]
        private IWebElement _images { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"input[name=date_valid_from]")]
            private IWebElement _dateValidFrom { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"input[name=date_valid_to]")]
            private IWebElement _dateValidTo { get; set; }
#endregion
#region Locators\Elements Information
        [FindsBy(How = How.CssSelector, Using = @"select[name=manufacturer_id]")]
            private IWebElement _manufacturer { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"input[name=keywords]")]
            private IWebElement _keywords { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"input[name=short_description\[en\]]")]
            private IWebElement _shortDescription { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"div.trumbowyg-editor")]
            private IWebElement _descriptionTextArea { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"input[name=head_title\[en\]]")]
            private IWebElement _headTitle { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"input[name=meta_description\[en\]]")]
            private IWebElement _metaDescription { get; set; }
#endregion
#region Locators\Elements Prices
        [FindsBy(How = How.CssSelector, Using = @"input[name=purchase_price]")]
            private IWebElement _purchasePriceValue { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"select[name=purchase_price_currency_code]")]
        private IWebElement _purchasePriceCurrency { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"input[name=prices\[USD\]]")]
        private IWebElement _priceUsd { get; set; }
        //[FindsBy(How = How.CssSelector, Using = @"input[name=gross_prices\[USD\]]")]
        //private IWebElement _priceUsdGross { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"input[name=prices\[EUR\]]")]
        private IWebElement _priceEur { get; set; }
        //[FindsBy(How = How.CssSelector, Using = @"input[name=gross_prices\[EUR\]]")]
        //private IWebElement _priceEurGross { get; set; }
#endregion

        public A_ProductPage(EventFiringWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            PageFactory.InitElements(_driver, this);

        }

        private A_CatalogPage DoSave()
        {
            _saveButton.Click();

            _wait.Until(ExpectedConditions.TitleIs("Catalog | My Store"));
            return new A_CatalogPage(_driver, _wait);
        }

        public A_CatalogPage DoCancel()
        {
            _cancelButton.Click();

            _wait.Until(ExpectedConditions.TitleIs("Catalog | My Store"));
            return new A_CatalogPage(_driver, _wait);
        }

        internal A_CatalogPage FillNewProductData(Product testProduct)
        {
            var p = testProduct;
            SetStatus(p.Status);
            _name.SendKeys(p.Name);
            _code.SendKeys(p.Code);
            SetCategories(p.Categories);
            new SelectElement(_defaultCategory).SelectByText(p.DefaultCategory);
            SetProductGroups(p.ProductGroups);
            _quantity.SendKeys(p.Quantity.ToString(CultureInfo.InvariantCulture));
            new SelectElement(_soldOutStatus).SelectByText(p.SoldOutStatus);
            //TODO: UploadImages
            _images.SendKeys(p.ImagesUrls[0]);  // по уму надо писать метод, который будет добавлять N-ное колво файлов, 
                //в зависимости от входных данных. Тут намеренно это опустил, чтоб не тратить время. 
                // Работа с массивами показана на примере p.Categories и p.ProductGroups

            _dateValidFrom.SendKeys(p.DateValidFrom);
            _dateValidTo.SendKeys(p.DateValidTo);

            OpenTab("Information");
            new SelectElement(_manufacturer).SelectByText(p.Manufacturer);
            _keywords.SendKeys(p.Keywords);
            _shortDescription.SendKeys(p.ShortDescription);
            _descriptionTextArea.SendKeys(p.Description);
            _headTitle.SendKeys(p.HeadTitle);
            _metaDescription.SendKeys(p.MetaDescription);

            OpenTab("Prices");
            Base.SetText(_purchasePriceValue, p.PurchasePriceValue.ToString(CultureInfo.InvariantCulture));
            new SelectElement(_purchasePriceCurrency).SelectByText(p.PurchasePriceCurrency);
            _priceUsd.SendKeys(p.PriceUsd.ToString(CultureInfo.InvariantCulture));
            _priceEur.SendKeys(p.PriceEur.ToString(CultureInfo.InvariantCulture));
            
            return DoSave();
        }

        public bool IsH1Exists()
        {
            try
            {
                _driver.FindElement(h1Locator);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            return true;
        }

        private void SetStatus(string status)
        {
            switch (status)
            {
                case "Enabled": _statusRadioButtons[0].Click();
                    break;
                case "Disabled": _statusRadioButtons[1].Click(); 
                    break;
            }
        }

        private void OpenTab(string tabName)
        {
            switch (tabName)
            {
                case "General": _tabGeneral.Click();
                    break;
                case "Information": _tabInformation.Click();
                    break;
                case "Prices": _tabPrices.Click();
                    break;
            }

        }

        private void SetCategories(string[] categories)
        {
            _categoriesRoot.Click();  // remove selection from Root
            foreach (var group in categories)
            {
                switch (group)
                {
                    case "Root": _categoriesRoot.Click();
                        break;
                    case "Rubber Ducks": _categoriesRubberDuck.Click();
                        break;
                    case "Subcategory": _categoriesSubcategory.Click();
                        break;
                }
            }
        }
        private void SetProductGroups(string[] groups)
        {
            foreach (var gender in groups)
            {
                switch (gender)
                {
                    case "Female": _productGroupFemale.Click();
                        break;
                    case "Male": _productGroupMale.Click();
                        break;
                    case "Unisex": _productGroupUnisex.Click();
                        break;
                }
            }
        }
    }
}

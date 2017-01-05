using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Lesson5.PageObjects.Admin
{
    class A_CountriesPage
    {
        private IWebDriver _driver;
        //$$('form[name="countries_form"] tr.row td:nth-child(5)')
        private readonly By _countriesNameTabColumnLocator = By.CssSelector(@"form[name='countries_form'] tr.row td:nth-child(5)");
        private readonly By _countriesZoneTabColumnLocator = By.CssSelector(@"form[name='countries_form'] tr.row td:nth-child(6)");


        public A_CountriesPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        internal List<string> GetCountriesList()
        {
            var countriesColumn = _driver.FindElements(_countriesNameTabColumnLocator);
            return countriesColumn.Select(c => c.GetAttribute("outerText")).ToList();
        }

        internal List<string> GetNotEmptyZonesCountriesUrlList()
        {
            var countriesColumn = _driver.FindElements(_countriesNameTabColumnLocator);
            var zonesClmn = _driver.FindElements(_countriesZoneTabColumnLocator);

           


            var urlList = new List<string>();


            return urlList;
        }
    }
}

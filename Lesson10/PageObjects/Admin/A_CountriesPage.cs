using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Lesson10.PageObjects.Admin;
using OpenQA.Selenium.Support.Events;

namespace Lesson10
{
    class A_CountriesPage : Base
    {

        //$$('form[name="countries_form"] tr.row td:nth-child(5)')
        private readonly By _countriesNameTabColumnLocator = By.CssSelector(@"form[name='countries_form'] tr.row td:nth-child(5):not(:last-child)");
        private readonly By _countriesRowLocator = By.CssSelector(@"form[name='countries_form'] tr.row");
        private readonly By _countriesUrlTabColumnLocator = By.CssSelector(@"td:nth-child(5) a");
        private readonly By _countriesZoneTabColumnLocator = By.CssSelector(@"td:nth-child(6)");
        private readonly By _zonesNameTabColumnLocator = By.CssSelector(@"table#table-zones tr:not(.header):not(:last-child) td:nth-child(3)");
        private readonly By _addNewCountryButtonLocator = By.CssSelector(@"a.button");

        public A_CountriesPage(EventFiringWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            PageFactory.InitElements(_driver, this);
        }

        internal List<string> GetCountriesList()
        {
            var countriesColumn = _driver.FindElements(_countriesNameTabColumnLocator);
            return countriesColumn.Select(c => c.GetAttribute("outerText")).ToList();
        }

        internal IEnumerable<string> GetCountryZonesList()
        {
            var zonesColumn = _driver.FindElements(_zonesNameTabColumnLocator);
            return zonesColumn.Select(c => c.GetAttribute("outerText")).ToList();
            }

        internal List<string> GetNotEmptyZonesCountriesUrlList()
        {
            var countriesCount = GetCountriesList().Count;
            var urlList = new List<string>();

            for (int i = 0; i < countriesCount; i++)
            {
                var countriesRow = _driver.FindElements(_countriesRowLocator).ToList();
                var zonesClmn =
                    countriesRow.ElementAt(i).FindElement(_countriesZoneTabColumnLocator).GetAttribute(@"textContent");
                if (int.Parse(zonesClmn) > 0)
                {
                    urlList.Add(countriesRow.ElementAt(i).FindElement(_countriesUrlTabColumnLocator).GetAttribute(@"href"));
                }

            }
            return urlList;
        }

        public A_CountryEditPage OpenNewCountryPage()
        {
            _driver.FindElement(_addNewCountryButtonLocator).Click();
            _wait.Until(ExpectedConditions.TitleIs(@"Add New Country | My Store"));
            return new A_CountryEditPage(_driver, _wait);
        }
    }
}

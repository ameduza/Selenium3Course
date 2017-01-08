using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Lesson6
{
    class A_GeoZonesPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        private readonly By _countriesNameColumnLocator = By.CssSelector(@"form[name=geo_zones_form] tr.row td:nth-child(3) a");
        private readonly By _zoneSelectLocator = By.CssSelector(@"table#table-zones td:nth-child(3) option[selected ='selected']");
            
        public A_GeoZonesPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            PageFactory.InitElements(_driver, this);
        }

        internal IEnumerable<string> GetCountryZonesUrlList()
        {
            var zonesColumn = _driver.FindElements(_countriesNameColumnLocator);
            return zonesColumn.Select(c => c.GetAttribute("href")).ToList();
        }

        internal IEnumerable<string> GetCountryZonesList()
        {
            var zonesColumn = _driver.FindElements(_zoneSelectLocator);
            return zonesColumn.Select(z => z.GetAttribute("textContent")).ToList();

        }
    }
}

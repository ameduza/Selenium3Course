using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Lesson3.PageObjects;
using Lesson5;
using Lesson5.PageObjects.Admin;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Lesson3
{
    [TestFixture]
    public class Tests : Base
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Start()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }
//Задание 9. Проверить сортировку стран и геозон в админке
//Сделайте сценарии, которые проверяют сортировку стран и геозон (штатов) в учебном приложении litecart.

//1) на странице http://localhost/litecart/admin/?app=countries&doc=countries
//а) проверить, что страны расположены в алфавитном порядке
//б) для тех стран, у которых количество зон отлично от нуля -- открыть страницу этой страны и там проверить, что зоны расположены в алфавитном порядке
//2) на странице http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones
//зайти в каждую из стран и проверить, что зоны расположены в алфавитном порядке
        
        [Test]
        public void Admin_Countries_CheckSortOrder()
        {
            //login as admin, get countriesList
            _driver.Navigate().GoToUrl("http://localhost/litecard/admin");
            _wait.Until(ExpectedConditions.TitleIs("My Store"));
            var countriesList = new LoginPage(_driver, _wait)
                .DoLogin(@"admin", @"admin")
                .OpenCountries()
                .GetCountriesList();
            
            //а) проверить, что страны расположены в алфавитном порядке
            Assert.That(IsListSortedAsc(countriesList));
        }

        [Test]
        public void Admin_Countries_CheckZoneSortOrder()
        {
            //login as admin
            _driver.Navigate().GoToUrl("http://localhost/litecard/admin");
            _wait.Until(ExpectedConditions.TitleIs("My Store"));
            //navigateTo Countries
            var countriesPage = new LoginPage(_driver, _wait)
                .DoLogin(@"admin", @"admin")
                .OpenCountries();
            var countriesList = countriesPage.GetNotEmptyZonesCountriesUrlList();
            //б) для тех стран, у которых количество зон отлично от нуля -- открыть страницу этой страны и там проверить, что зоны расположены в алфавитном порядке
            foreach (var country in countriesList)
            {
                _driver.Navigate().GoToUrl(country);
                var zonesList = countriesPage.GetCountryZonesList();

                Assert.That(IsListSortedAsc(zonesList));
            }
        }
        
        [Test]
        public void Admin_GeoZones_CheckZonesSortOrder()
        {
            //login as admin
            _driver.Navigate().GoToUrl("http://localhost/litecard/admin");
            _wait.Until(ExpectedConditions.TitleIs("My Store"));
            //2) на страницу http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones
            var geoZonesPage = new LoginPage(_driver, _wait)
                .DoLogin(@"admin", @"admin")
                .OpenGeoZones();
            var geoZones = geoZonesPage.GetCountryZonesUrlList();
            //зайти в каждую из стран и проверить, что зоны расположены в алфавитном порядке
            foreach (var geoZone in geoZones)
            {
                _driver.Navigate().GoToUrl(geoZone);
                var zonesList = geoZonesPage.GetCountryZonesList();

                Assert.That(IsListSortedAsc(zonesList));
            }
        }
        
        [TearDown]
        public void Stop()
        {
            if (_driver != null) _driver.Quit();
        }



    }
}

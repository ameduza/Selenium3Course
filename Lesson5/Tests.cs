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

        [Test]
        public void Admin_CountriesCheckSortOrder()
        {
            //login as admin
            _driver.Navigate().GoToUrl("http://localhost/litecard/admin");
            _wait.Until(ExpectedConditions.TitleIs("My Store"));
            var countriesPage = new LoginPage(_driver)
                .DoLogin(@"admin", @"admin")
                .OpenCountries();
            _wait.Until(ExpectedConditions.TitleIs("Countries | My Store"));
            
            var countriesList = countriesPage.GetCountriesList();
            //а) проверить, что страны расположены в алфавитном порядке
            Assert.That(IsListSortedAsc(countriesList));
        }

        [Test]
        public void Admin_CountriesCheckZoneSortOrder()
        {
            //login as admin
            //navigateTo Countries
            _driver.Navigate().GoToUrl("http://localhost/litecard/admin");
            _wait.Until(ExpectedConditions.TitleIs("My Store"));
            LoginPage loginPage = new LoginPage(_driver);

            A_CountriesPage countriesPage = loginPage.DoLogin(@"admin", @"admin").OpenCountries();
            _wait.Until(ExpectedConditions.TitleIs("Countries | My Store"));
            List<string> countriesList = countriesPage.GetCountriesList();

            //б) для тех стран, у которых количество зон отлично от нуля -- открыть страницу этой страны и там проверить, что зоны расположены в алфавитном порядке

        }
        [Test]
        public void Admin_CheckZonessortOrder()
        {
            //login as admin
            //2) на странице http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones
            //зайти в каждую из стран и проверить, что зоны расположены в алфавитном порядке

        }



        [TearDown]
        public void Stop()
        {
            if (_driver != null) _driver.Quit();
        }



    }
}

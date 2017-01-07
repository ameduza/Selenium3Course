using System;
using System.Collections.Generic;
using System.Drawing;
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

        [Test]
        public void OpenCorrectGood()
        {
            //Задание 10. Проверить, что открывается правильная страница товара
            //Сделайте сценарий, который проверяет, что при клике на товар открывается правильная страница товара в учебном приложении litecart.

            //1) Открыть главную страницу
            //2) Кликнуть по первому товару в категории Campaigns
            //3) Проверить, что открывается страница правильного товара

            //Более точно, проверить, что
            //а) совпадает текст названия товара
            //б) совпадает цена (обе цены)


            _driver.Navigate().GoToUrl(@"http://localhost/litecard/en/");
            _wait.Until(ExpectedConditions.TitleIs(@"Online Store | My Store"));

            var mainPage = new MainPage(_driver, _wait);
            var productMainPage = mainPage
                .GetCampaignsFirstProductDetails();

            var productProductPage = mainPage
                .OpenFirstCampaignsProduct()
                .GetProductDetails();
            
            #region Color const
            var c_grey1 = ColorTranslator.FromHtml("#777");
            var priceRegularColorMainPage = Argb2RgbaFormat(c_grey1);

            var c_grey2 = ColorTranslator.FromHtml("#666");
            var priceRegularColorProductPage = Argb2RgbaFormat(c_grey2);

            var c_red = ColorTranslator.FromHtml("#c00"); //red
            var priceCampaignColor = Argb2RgbaFormat(c_red);

            const string priceRegularStyle = @"S";
            const string priceCampaignStyle = @"STRONG";
            #endregion
            
            #region Assertions

            //а) совпадает текст названия товара
            //б) совпадает цена (обе цены)
            Assert.That(productMainPage.Name == productProductPage.Name, @"Название продукта на главной странице не совпадает на странице продукта");
            Assert.That(productMainPage.Manufacturer == productProductPage.Manufacturer, @"Производитель на главной странице не совпадает на странице продукта");
            Assert.That(productMainPage.PriceRegular == productProductPage.PriceRegular, @"Цена обычная на главной странице не совпадает на странице продукта");
            Assert.That(productMainPage.PriceCampaign == productProductPage.PriceCampaign, @"Цена по скидке на главной странице не совпадает на странице продукта");

            //Кроме того, проверить стили цены на главной странице и на странице товара -- первая цена серая, зачёркнутая, маленькая, вторая цена красная жирная, крупная.        

            Assert.That(productMainPage.PriceRegularColor, Is.EqualTo(priceRegularColorMainPage), @"Цвет обычной цены на главной странице неверный");
            Assert.That(productMainPage.PriceRegularStyle, Is.EqualTo(priceRegularStyle), @"Стиль обычной цены на главной странице неверный");
            Assert.That(productMainPage.PriceRegularSize, Is.LessThan(productMainPage.PriceCampaignSize), @"Размер обычной цены больше скидочной на главной странице");

            Assert.That(productProductPage.PriceRegularColor, Is.EqualTo(priceRegularColorProductPage), @"Цвет обычной цены на странице продукта неверный");
            Assert.That(productProductPage.PriceRegularStyle, Is.EqualTo(priceRegularStyle), @"Стиль обычной цены на странице продукта неверный");
            Assert.That(productProductPage.PriceRegularSize, Is.LessThan(productProductPage.PriceCampaignSize), @"Размер обычной цены больше скидочной на странице продукта");


            Assert.That(productMainPage.PriceCampaignColor, Is.EqualTo(priceCampaignColor), @"Цвет скидочной цены на главной странице неверный");
            Assert.That(productMainPage.PriceCampaignStyle, Is.EqualTo(priceCampaignStyle), @"Стиль скидочной цены на главной странице неверный");

            Assert.That(productProductPage.PriceCampaignColor, Is.EqualTo(priceCampaignColor), @"Цвет скидочной цены на странице продукта неверный");
            Assert.That(productProductPage.PriceCampaignStyle, Is.EqualTo(priceCampaignStyle), @"Стиль скидочной цены на странице продукта неверный");



            // уточнить что нужно сделать чтобы сравнивать целиком экземпляры классов:
            // https://github.com/nunit/docs/wiki/EqualConstraint  Notes 1.
            //Assert.That(productMainPage == productProductPage, @"Продукты не равны");
            //Assert.That(productMainPage.Equals(productProductPage), @"Продукты не равны");
#endregion           
        }




        [TearDown]
        public void Stop()
        {
            if (_driver != null) _driver.Quit();
        }
    }
}

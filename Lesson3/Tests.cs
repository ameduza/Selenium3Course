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
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Lesson3
{
    [TestFixture]
    public class Tests
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

        [Test]
        public void ClickAllLeftMenuLinks()
        {
            _driver.Navigate().GoToUrl("http://localhost/litecard/admin");
            _wait.Until(ExpectedConditions.TitleIs("My Store"));
            LoginPage loginPage = new LoginPage(_driver);

            AdminPage adminPage = loginPage.DoLogin(@"admin", @"admin");
            int menuItemsCount = adminPage.GetMainMenuItemsCount();
            for (int i = 1; i <= menuItemsCount; i++)
            {
                adminPage.ClickMainMenuItem(i);
                Assert.True(adminPage.IsH1Exists());

                int subMenuItemsCount = adminPage.GetSubMenuItemsCount();
                if (subMenuItemsCount <= 0) continue;
                for (int n = 1; n <= subMenuItemsCount; n++)
                {
                    adminPage.ClickSubMenuItem(n);
                    Assert.True(adminPage.IsH1Exists());
                }
            }
        }

        [Test]
        public void CheckGoodsStickers()
        {
            //Сделайте сценарий, проверяющий наличие стикеров у всех товаров в учебном приложении litecart на главной странице. 
            //Стикеры -- это полоски в левом верхнем углу изображения товара, на которых написано New или Sale или что-нибудь другое. 
            //Сценарий должен проверять, что у каждого товара имеется ровно один стикер.
            _driver.Url = "http://localhost/litecard/en/";
            _wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));

            MainPage mainPage = new MainPage(_driver);
            IList<IWebElement> productsList = mainPage.GetProductsList(_driver);
            foreach (var product in productsList)
            {
                Assert.That(1, Is.EqualTo(mainPage.GetProductStickerCount(product)));
            }
        }

        [TearDown]
        public void Stop()
        {
            if (_driver != null) _driver.Quit();
        }



    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Selenium3
{
    [TestFixture]
    public class Firefox
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            FirefoxOptions options = new FirefoxOptions();
            //options.UseLegacyImplementation = true;
            options.BrowserExecutableLocation = @"c:\Program Files\FireFoxNightly\firefox.exe";
            driver = new FirefoxDriver(options);
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void FirstTestFF()
        {
            driver.Url = "http://www.google.com/";
            driver.FindElement(By.Name("q")).SendKeys("webdriver");
            driver.FindElement(By.Name("btnG")).Click();
            wait.Until(ExpectedConditions.TitleIs("webdriver - Поиск в Google"));
        }

        [Test]
        public void LiteCardLoginFF()
        {
            driver.Navigate().GoToUrl("http://localhost/litecard/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();


        }

        [TearDown]
        public void stop()
        {
            if (driver != null) driver.Quit();
            driver = null;
        }
    }
}

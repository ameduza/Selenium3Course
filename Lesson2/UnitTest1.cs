using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Selenium2
{
    [TestFixture]
    public class UnitTest1
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private IWebDriver driverFf;
        private WebDriverWait waitFF;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void FirstTest()
        {
            driver.Url = "http://www.google.com/";
            driver.FindElement(By.Name("q")).SendKeys("webdriver");
            driver.FindElement(By.Name("btnG")).Click();
            wait.Until(ExpectedConditions.TitleIs("webdriver - Поиск в Google"));
        }

        [Test]
        public void LiteCardLogin()
        {
            driver.Navigate().GoToUrl("http://localhost/litecard/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            //  " find You are now logged in as admin" message 
            driver.FindElement(By.ClassName("success"));
                

        }

        [TearDown]
        public void stop()
        {
            if (driver != null) driver.Quit();
            driver = null;
        }
    }
}

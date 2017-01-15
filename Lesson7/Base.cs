using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace Lesson7
{
    public class Base
    {
        protected IWebDriver _driver;
        protected WebDriverWait _wait;
        private const int _waitDefaultTime = 10;
        
        [SetUp]
        public void Start()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_waitDefaultTime));
        }

        [TearDown]
        public void Stop()
        {
            if (_driver != null) _driver.Quit();
        }

        public static bool IsListSortedAsc(IEnumerable<string> listToCheck)
        {
            var l = new List<string>(listToCheck);
            l.Sort();
            return l.SequenceEqual(listToCheck);
        }

        public static string Argb2RgbaFormat(Color c)
        {
            return String.Format("rgba({0}, {1}, {2}, {3})", c.R, c.G, c.B, 1);  // вместо c.A пришслось вписать 1
        }

        public static void SetText(IWebElement element, string keys)
        {
            element.Clear();
            element.SendKeys(keys);
        }

        public static string GetRandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool IsElementNotPresent(IWebDriver driver, By locator)
        {
            _driver = driver;
            try
            {
                _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
                return _driver.FindElements(locator).Count == 0;
            }
            finally 
            {
                _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(_waitDefaultTime));
            }
        }
    }
}

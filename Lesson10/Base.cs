using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;

namespace Lesson10
{
    public class Base
    {
        protected EventFiringWebDriver _driver;
        protected WebDriverWait _wait;
        private const int _waitDefaultTime = 10;
        private const string _screenFilePath = "C:\\Temp\\";
        
        [SetUp]
        public void Start()
        {
            _driver = new EventFiringWebDriver(new ChromeDriver());
            _driver.FindingElement += (sender, e) => Console.WriteLine("try to find: " + e.FindMethod);
            _driver.FindElementCompleted += (sender, e) => Console.WriteLine(e.FindMethod + " found");
            _driver.ExceptionThrown += OnDriverOnExceptionThrown;
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_waitDefaultTime));
        }

        private void OnDriverOnExceptionThrown(object sender, WebDriverExceptionEventArgs e)
        {
            Console.WriteLine(e.ThrownException);
            _driver.GetScreenshot().SaveAsFile(_screenFilePath + "  screenOnException.png", ImageFormat.Png);
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

        public bool IsElementNotPresent(EventFiringWebDriver driver, By locator)
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

        //public bool anyWindowOtherThanThat(IWebDriver driver, IReadOnlyCollection<string> oldWindows)
        //{
        //    string 
        //    driver.WindowHandles

        //    return result;
        //}
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lesson10;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;

namespace Lesson10
{
    public class A_CountryEditPage : Base
    {
        private readonly By _externalLinksLocator = By.CssSelector(@"form i.fa.fa-external-link");
        public A_CountryEditPage(EventFiringWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        public IList<IWebElement> GetExternalLinks()
        {
            return _driver.FindElements(_externalLinksLocator).ToList(); ;
        }

    }
}

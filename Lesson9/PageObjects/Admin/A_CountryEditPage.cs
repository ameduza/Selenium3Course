using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lesson9;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Lesson9
{
    public class A_CountryEditPage : Base
    {
        private readonly By _externalLinksLocator = By.CssSelector(@"form i.fa.fa-external-link");
        public A_CountryEditPage(IWebDriver driver, WebDriverWait wait)
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace Lesson3.PageObjects
{
    public class AdminPage
    {
        private IWebDriver _driver;
        private readonly By _mainMenuListLocator = By.XPath("//li[@id='app-']");
        private readonly By _subMenuListLocator = By.XPath("//ul[@class='docs']/li");
        private readonly By _h1Locator = By.XPath("//h1");

        public AdminPage(IWebDriver driver)
        {
            _driver = driver;
            Init();
        }

        private void Init()
        {
            PageFactory.InitElements(_driver, this);
        }

        private IList<IWebElement> GetMainMenuItems()
        {
            return _driver.FindElements(_mainMenuListLocator);
        }

        public int GetMainMenuItemsCount()
        {
            return GetMainMenuItems().Count;
        }

        private IWebElement FindMainMenuItem(int i)
        {
            return _driver.FindElement(By.XPath(@"//li[@id='app-'][" + i + "]"));
        }

        private IWebElement FindSubMenuItem(int n)
        {
            return _driver.FindElement(By.XPath(@"//ul[@class='docs']/li[" + n + "]"));
        }
        public void ClickMainMenuItem(int i)
        {
            FindMainMenuItem(i).Click();
        }

        public void ClickSubMenuItem(int n)
        {
            FindSubMenuItem(n).Click();
        }

        private IList<IWebElement> GetSubMenuItems()
        {
            return _driver.FindElements(_subMenuListLocator);
        }
        public int GetSubMenuItemsCount()
        {
            return GetSubMenuItems().Count;
        }

        internal bool IsH1Exists()
        {
            try
            {
                _driver.FindElement(_h1Locator);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            return true;
        }
    }
}

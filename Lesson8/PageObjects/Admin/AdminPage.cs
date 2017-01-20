using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Lesson8
{
    public class AdminPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait; 
        private readonly By _mainMenuListLocator = By.XPath("//li[@id='app-']");
        private readonly By _subMenuListLocator = By.XPath("//ul[@class='docs']/li");
        private readonly By _h1Locator = By.XPath("//h1");
        private readonly By _catalogLocator = By.XPath("//li[@id='app-'][2]");
        private readonly By _countriesLocator = By.XPath("//li[@id='app-'][3]");
        private readonly By _geoZonesLocator = By.XPath("//li[@id='app-'][6]");

        public AdminPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
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

        internal A_CatalogPage OpenCatalog()
        {
            _driver.FindElement(_catalogLocator).Click();
            _wait.Until(ExpectedConditions.TitleIs("Catalog | My Store"));
            return new A_CatalogPage(_driver, _wait);
        }

        internal A_CountriesPage OpenCountries()
        {
            _driver.FindElement(_countriesLocator).Click();
            _wait.Until(ExpectedConditions.TitleIs("Countries | My Store"));
            return new A_CountriesPage(_driver, _wait);
        }

        internal A_GeoZonesPage OpenGeoZones()
        {
            _driver.FindElement(_geoZonesLocator).Click();
            _wait.Until(ExpectedConditions.TitleIs("Geo Zones | My Store"));
            return new A_GeoZonesPage(_driver, _wait);
        }
    }
}

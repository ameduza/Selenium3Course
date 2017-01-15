using System.Collections.Generic;
using Lesson7.PageObjects.Admin;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Lesson7
{
    public class MainPage : Base
    {
#region Login locators       
        [FindsBy(How = How.CssSelector, Using = @"div#box-account-login tr:nth-child(5) a")]
        private IWebElement _registerCustomerElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"div#box-account li:nth-child(4) a")]
        private IWebElement _logoutLinkElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"div#box-account-login input[name=email]")]
        private IWebElement _emailFieldElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"div#box-account-login input[name=password]")]
        private IWebElement _passwordFieldElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"div#box-account-login button[name=login]")]
        private IWebElement _loginButtonElement { get; set; }
#endregion
#region Navigation locators
        protected By _cartItemsQuantityLocator = By.CssSelector(@"div#cart span.quantity");
        private readonly By _homeLinkLocator = By.CssSelector("nav#site-menu i.fa");
        private By _checkoutLinkLocator = By.CssSelector(@"div#cart a.link");
#endregion
#region Product locators
        private readonly By _productsListLocator = By.CssSelector("li.product");
#endregion
        
        public MainPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            PageFactory.InitElements(_driver, this);
        }


        public CreateAccountPage OpenCreateAccountPage()
        {
            _registerCustomerElement.Click();
            return new CreateAccountPage(_driver, _wait);
        }

        public MainPage DoLogout()
        {
            _logoutLinkElement.Click();
            return new MainPage(_driver, _wait);
        }

        public MainPage DoLogin(string email, string password)
        {
            SetText(_emailFieldElement, email);
            SetText(_passwordFieldElement, password);
            _loginButtonElement.Click();
            return new MainPage(_driver, _wait);
        }

        public MainPage OpenHomePage(IWebDriver driver, WebDriverWait wait)
        {
            _driver.FindElement(_homeLinkLocator).Click();
            return new MainPage(driver, wait);
        }

        public IWebElement GetCartItemsQuantityElement()
        {
            return _driver.FindElement(_cartItemsQuantityLocator);
        }

        public int GetCartItemsQuantity()
        {
            var cartItemsQuantityInitialElement = GetCartItemsQuantityElement();
            return int.Parse(cartItemsQuantityInitialElement.Text);
        }

        public IList<IWebElement> GetProductsList(IWebDriver driver)
        {
            return new List<IWebElement>(driver.FindElements(_productsListLocator));
        }


        public CartPage OpenCartPage()
        {
            _driver.FindElement(_checkoutLinkLocator).Click();
            _wait.Until(ExpectedConditions.TitleIs(@"Checkout | My Store"));
            return new CartPage(_driver, _wait);
        }
    }
}

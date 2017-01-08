using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Lesson6
{
    public class MainPage : Base
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        
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
            SendKeys(_emailFieldElement, email);
            SendKeys(_passwordFieldElement, password);
            _loginButtonElement.Click();
            return new MainPage(_driver, _wait);
        }
    }
}

﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Lesson9
{
    public class LoginPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [FindsBy(How = How.CssSelector, Using = @"form[name=login_form] input[name=username]")]
        private IWebElement _userNameElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"form[name=login_form] input[name=password]")]
        private IWebElement _passwordElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"form[name=login_form] button[name=login]")]
        private IWebElement _loginBtn { get; set; }

        public LoginPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            Init();
        }

        public void Init()
        {
            PageFactory.InitElements(_driver, this);
        }


        public AdminPage DoLogin(string username, string password)
        {
            _userNameElement.SendKeys(username);
            _passwordElement.SendKeys(password);
            _loginBtn.Click();
            return new AdminPage(_driver, _wait);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using  OpenQA.Selenium.Support.PageObjects;


namespace Lesson3.PageObjects
{
    public class LoginPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = @"form[name=login_form] input[name=username]")]
        public IWebElement _userNameElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"form[name=login_form] input[name=password]")]
        private IWebElement _passwordElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"form[name=login_form] button[name=login]")]
        private IWebElement _loginBtn { get; set; }

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            Init();
        }

        public void Init()
        {
            PageFactory.InitElements(_driver, this);
        }


        public AdminPage DoLogin(string username, string password)
        {
            //("div#box-login input[name=username]")
            _userNameElement.SendKeys(username);
            _passwordElement.SendKeys(password);
            _loginBtn.Click();
            return new AdminPage(_driver);
        }
    }
}

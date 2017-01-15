using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Lesson7
{
    public class CreateAccountPage : Base
    {

        [FindsBy(How = How.CssSelector, Using = @"form[name=customer_form] input[name=firstname]")]
        private IWebElement _firstName { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"form[name=customer_form] input[name=lastname]")]
        private IWebElement _lastName { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"form[name=customer_form] input[name=address1]")]
        private IWebElement _address1 { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"form[name=customer_form] input[name=postcode]")]
        private IWebElement _postcode { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"form[name=customer_form] input[name=city]")]
        private IWebElement _city { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"form[name=customer_form] select[name=country_code]")]
        private IWebElement _countrySelect { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"form[name=customer_form] select[name=zone_code]")]
        private IWebElement _stateSelect { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"form[name=customer_form] input[name=email]")]
        private IWebElement _email { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"form[name=customer_form] input[name=phone]")]
        private IWebElement _phone { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"form[name=customer_form] input[name=password]")]
        private IWebElement _passwordDesired { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"form[name=customer_form] input[name=confirmed_password]")]
        private IWebElement _passwordConfirm { get; set; }
        [FindsBy(How = How.CssSelector, Using = @"form[name=customer_form] button[name=create_account]")]
        private IWebElement _createAccountButton { get; set; }

        
        public CreateAccountPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            Init();
        }

        private void Init()
        {
            PageFactory.InitElements(_driver, this);
        }
        
        internal MainPage CreateNewAccount(Account a)
        {
            SetText(_firstName, a.FirstName);
            SetText(_lastName, a.LastName);
            SetText(_address1, a.Address1);
            SetText(_postcode, a.Postcode.ToString());
            SetText(_city, a.City);
            //_driver.ExecuteJavaScript("argument[0].style.opacity=1", _countrySelect);  - оказалось SelectElement и так отработал! :)
            var countrySelect = new SelectElement(_countrySelect);
            countrySelect.SelectByText(a.Country);
            SetText(_email, a.Email);
            SetText(_phone, a.Phone.ToString());
            SetText(_passwordDesired, a.Password);
            SetText(_passwordConfirm, a.Password);

            _createAccountButton.Click();   // костыль для неактивного комбобокса _stateSelect http://www.screencast.com/t/03cw7Tcb
            Init();                         // костыль для неактивного комбобокса _stateSelect http://www.screencast.com/t/03cw7Tcb
            var stateSelect = new SelectElement(_stateSelect);
            stateSelect.SelectByText(a.State);

            SetText(_passwordDesired, a.Password);
            SetText(_passwordConfirm, a.Password);
            _createAccountButton.Click();

            //_wait.Until(ExpectedConditions.TitleIs())
            return new MainPage(_driver, _wait);
        }
    }
}

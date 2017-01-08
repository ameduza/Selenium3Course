using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Lesson6;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace Lesson6
{
    [TestFixture]
    public class Tests : Base
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Start()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void RegisterNewUser()
        {
            //Задание 11. Сделайте сценарий регистрации пользователя
            //Сделайте сценарий для регистрации нового пользователя в учебном приложении litecart (не в админке, а в клиентской части магазина).

            //Сценарий должен состоять из следующих частей:

            //1) регистрация новой учётной записи с достаточно уникальным адресом электронной почты (чтобы не конфликтовало с ранее созданными пользователями),
            //2) выход (logout), потому что после успешной регистрации автоматически происходит вход,
            //3) повторный вход в только что созданную учётную запись,
            //4) и ещё раз выход.

            //Можно оформить сценарий либо как тест, либо как отдельный исполняемый файл.

            //Проверки можно никакие не делать, только действия -- заполнение полей, нажатия на кнопки и ссылки. Если сценарий дошёл до конца, то есть созданный пользователь смог выполнить вход и выход -- значит создание прошло успешно.

            //В форме регистрации есть капча, её нужно отключить в админке учебного приложения на вкладке Settings -> Security.
        

            _driver.Navigate().GoToUrl(@"http://localhost/litecard/en/");
            _wait.Until(ExpectedConditions.TitleIs(@"Online Store | My Store"));
            
            var testUser = new Account()
            {
                FirstName = "First Name",
                LastName = "Last Name",
                Address1 = "address1 data",
                Postcode = 12345,
                City = "Colorado village",
                Country = "United States",
                State = "Colorado",
                Email = GetRandomString(10) + "@ya.ru",
                Phone = 9119111122,
                Password = "qwerty123"
            };

            var mainPage = new MainPage(_driver, _wait)
                .OpenCreateAccountPage()
                .CreateNewAccount(testUser)
                .DoLogout()
                .DoLogin(testUser.Email, testUser.Password)
                .DoLogout();
        }

        [TearDown]
        public void Stop()
        {
            if (_driver != null) _driver.Quit();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace Lesson8
{
    [TestFixture]
    public class Tests : Base
    {

        [Test]
        public void CreateNewCountyExternalLinksTest()
        {
//Задание 14. Проверьте, что ссылки открываются в новом окне
//Сделайте сценарий, который проверяет, что ссылки на странице редактирования страны открываются в новом окне.

//Сценарий должен состоять из следующих частей:

//1) зайти в админку
//2) открыть пункт меню Countries (или страницу http://localhost/litecart/admin/?app=countries&doc=countries)
//3) открыть на редактирование какую-нибудь страну или начать создание новой
//4) возле некоторых полей есть ссылки с иконкой в виде квадратика со стрелкой -- они ведут на внешние страницы и открываются в новом окне, именно это и нужно проверить.

//Конечно, можно просто убедиться в том, что у ссылки есть атрибут target="_blank". Но в этом упражнении требуется именно кликнуть по ссылке, чтобы она открылась в новом окне, потом переключиться в новое окно, закрыть его, вернуться обратно, и повторить эти действия для всех таких ссылок.

//Не забудьте, что новое окно открывается не мгновенно, поэтому требуется ожидание открытия окна.
            _driver.Navigate().GoToUrl("http://localhost/litecard/admin");
            _wait.Until(ExpectedConditions.TitleIs("My Store"));
            var newCountryPage = new LoginPage(_driver, _wait)
                .DoLogin("admin", "admin")
                .OpenCountries()
                .OpenNewCountryPage();
            var links = newCountryPage.GetExternalLinks();
            foreach (var l in links)
            {
                var windowHandles = _driver.WindowHandles;
                l.Click();
                
                
                
                //_wait.Until();

                // Assert
            }
        }

        //public bool CC(IWebDriver driver, WebDriverWait wait, IReadOnlyCollection<string> initialHandleCount)
        //{

        //}
    }
}

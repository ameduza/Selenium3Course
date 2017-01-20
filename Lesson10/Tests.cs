using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace Lesson10
{
    [TestFixture]
    public class Tests : Base
    {

        [Test]
        public void TestBrowserLogs()
        {
//Задание 17. Проверьте отсутствие сообщений в логе браузера
//Сделайте сценарий, который проверяет, не появляются ли в логе браузера сообщения при открытии страниц в учебном приложении, а именно -- страниц товаров в каталоге в административной панели.

//Сценарий должен состоять из следующих частей:

//1) зайти в админку
//2) открыть каталог, категорию, которая содержит товары (страница http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1)
//3) последовательно открывать страницы товаров и проверять, не появляются ли в логе браузера сообщения (любого уровня)            
            _driver.Navigate().GoToUrl("http://localhost/litecard/admin");
            new LoginPage(_driver, _wait).DoLogin("admin", "admin");
            _driver.Navigate().GoToUrl("http://localhost/litecard/admin/?app=catalog&doc=catalog&category_id=1");
            _wait.Until(ExpectedConditions.TitleIs("Catalog | My Store"));
            var catalogPage = new A_CatalogPage(_driver, _wait).GetProductNamesList();


            // Assert
        }

        [Test]
        public void TestLogs()
        {
            _driver.Navigate().GoToUrl("http://google.com");
            _driver.FindElement(By.CssSelector("input#lst-ib")).SendKeys("status bat");
            _driver.GetScreenshot().SaveAsFile("C:\\Temp\\screen-.png", ImageFormat.Png);
            _driver.FindElement(By.CssSelector("input#lst-wb")).SendKeys("status bat");
        }

    }

}

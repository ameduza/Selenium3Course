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
        public void Admin_TestBrowserLogs()
        {
//Задание 17. Проверьте отсутствие сообщений в логе браузера
//Сделайте сценарий, который проверяет, не появляются ли в логе браузера сообщения при открытии страниц в учебном приложении, а именно -- страниц товаров в каталоге в административной панели.

//Сценарий должен состоять из следующих частей:

//1) зайти в админку
//2) открыть каталог, категорию, которая содержит товары (страница http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1)
//3) последовательно открывать страницы товаров и проверять, не появляются ли в логе браузера сообщения (любого уровня)            
            _driver.Navigate().GoToUrl("http://localhost/litecard/admin");
            new LoginPage(_driver, _wait).DoLogin("admin", "admin");
            new AdminPage(_driver, _wait).OpenCatalog();
            _wait.Until(ExpectedConditions.TitleIs("Catalog | My Store"));
            //Open all subfolders to get complete products list
            var catalogPage = new A_CatalogPage(_driver, _wait);
            int closedFoldersCount = 0;
            while (catalogPage.GetClosedFoldersList().Count > 0)
            {
                _driver.FindElement(catalogPage.folderClosedLocator).Click();
                closedFoldersCount++;
            }
            // Get products list
            var products = catalogPage.GetProductsList();
            //3) последовательно открывать страницы товаров и проверять, не появляются ли в логе браузера сообщения (любого уровня)            
            for (int i = 0; i < products.Count; i++)
            {
                var productPage = catalogPage.OpenProductByPosition(i);
                var browserLogs = _driver.Manage().Logs.GetLog("browser");
                var logEntriesCount = browserLogs.Count;
                Assert.That(logEntriesCount, Is.EqualTo(0), "При открытии продукта были обнаружены ошибки в логах браузера. Содержимое: {0}", browserLogs);
                productPage.DoCancel();
            }

        }

        [Test]
        public void TryScreenshot()
        {
            _driver.Navigate().GoToUrl("http://google.com");
            _driver.FindElement(By.CssSelector("input#lst-ib")).SendKeys("status bat");
            _driver.GetScreenshot().SaveAsFile("C:\\Temp\\screen-.png", ImageFormat.Png);
            _driver.FindElement(By.CssSelector("input#lst-wb")).SendKeys("status bat");
        }

    }

}

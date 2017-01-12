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
    public class Tests
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
                Email = Base.GetRandomString(10) + "@ya.ru",
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

        [Test]
        public void Admin_AddProduct()
        {
//Задание 12. Сделайте сценарий добавления товара
//Сделайте сценарий для добавления нового товара (продукта) в учебном приложении litecart (в админке).

//Для добавления товара нужно открыть меню Catalog, в правом верхнем углу нажать кнопку "Add New Product", заполнить поля с информацией о товаре и сохранить.

//Достаточно заполнить только информацию на вкладках General, Information и Prices. Скидки (Campains) на вкладке Prices можно не добавлять.
// есть выпадающие списки с одним вариантом выбора -- конечно их можно пропустить
//После сохранения товара нужно убедиться, что он появился в каталоге (в админке). Клиентскую часть магазина можно не проверять.
            _driver.Navigate().GoToUrl("http://localhost/litecard/admin");
            _wait.Until(ExpectedConditions.TitleIs("My Store"));

            var testProduct = new Product()
            {
                Status = "Enabled",
                Name = "Test Duck_" + Base.GetRandomString(10),  // можно timestamp вкрутить, как вариант
                Code = "test",
                Categories = new[] { "Rubber Ducks", "Subcategory" },
                DefaultCategory = "Subcategory",
                ProductGroups = new [] { "Female", "Unisex" },
                Quantity = 10,
                SoldOutStatus = "Temporary sold out",
                ImagesUrls = new [] {@"c:\\TEMP\\test_duck.png"},
                DateValidFrom = "01122016",
                DateValidTo = "01122017",
                
                //Information 
                Manufacturer = "ACME Corp.",
                Keywords = "keywords data text",
                ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.\n Suspendisse sollicitudin ante massa, eget ornare libero porta congue. Cras scelerisque dui non consequat sollicitudin. Sed pretium tortor ac auctor molestie. Nulla facilisi. ",
                HeadTitle = "Head title text",
                MetaDescription = "meta text",
                //Prices 
                PurchasePriceValue = 9.5f,
                PurchasePriceCurrency = "Euros",
                PriceUsd = 11,
                PriceEur = 10.12f,
            };
            
            
            var newPoductPage = new LoginPage(_driver, _wait)
                .DoLogin(@"admin", @"admin")
                .OpenCatalog()
                .OpenAddNewProduct();
            newPoductPage
                .FillNewProductData(testProduct);

            //После сохранения товара нужно убедиться, что он появился в каталоге (в админке). Клиентскую часть магазина можно не проверять.
            var productList = new A_CatalogPage(_driver, _wait)
                .SearchProduct(testProduct.Name)
                .GetProductNamesList();
            Assert.That(productList.Count == 1, "Продукт не найден");  // уникальность имени обеспечивается функцией Base.GetRandomString, на поиск товара в списке уже не хватило сил :)
        }



        [TearDown]
        public void Stop()
        {
            if (_driver != null) _driver.Quit();
        }
    }
}

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


namespace Lesson7
{
    [TestFixture]
    public class Tests : Base
    {

        [Test]
        public void CartBaseTest()
        {
//Задание 13. Сделайте сценарий работы с корзиной
//Сделайте сценарий для добавления товаров в корзину и удаления товаров из корзины.

//Сценарий должен состоять из следующих частей:

//1) открыть страницу какого-нибудь товара
//2) добавить его в корзину
//3) подождать, пока счётчик товаров в корзине обновится
//4) вернуться на главную страницу, повторить предыдущие шаги ещё два раза, чтобы в общей сложности в корзине было 3 единицы товара

//5) открыть корзину (в правом верхнем углу кликнуть по ссылке Checkout)
//6) удалить все товары из корзины один за другим, после каждого удаления подождать, пока внизу обновится таблица            
            _driver.Navigate().GoToUrl(baseUrl + @"litecard/en/");
            _wait.Until(ExpectedConditions.TitleIs(@"Online Store | My Store"));
            for (int i = 0; i < 3; i++)
            {
                var products = new MainPage(_driver, _wait)
                    .GetProductsList(_driver);
                var random = new Random();
                int r = random.Next(products.Count);
                //1) открыть страницу какого-нибудь товара
                products[r].Click();
                var productPage = new ProductPage(_driver, _wait);
                //2) добавить его в корзину
                //3) подождать, пока счётчик товаров в корзине обновится
                productPage.AddToCart(1);
                //4) вернуться на главную страницу, повторить предыдущие шаги ещё два раза, чтобы в общей сложности в корзине было 3 единицы товара
                productPage.OpenMainPage();
            }
            //5) открыть корзину (в правом верхнем углу кликнуть по ссылке Checkout)
            var cartPage = new MainPage(_driver, _wait).OpenCartPage();
            //6) удалить все товары из корзины один за другим, после каждого удаления подождать, пока внизу обновится таблица            
            cartPage.RemoveAllItemsFromCart();
            
            // Бонус - проверил, что после удаления на главной странице действительно отображается 0 товаров в корзине
            var cartProductscount =
                new MainPage(_driver, _wait)
                .OpenHomePage(_driver, _wait)
                .GetCartItemsQuantity();
            Assert.That(cartProductscount == 0, "Не все товары корзины удалились");

        }


    }
}

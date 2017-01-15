using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Lesson7.PageObjects.Admin
{
    public class CartPage : Base
    {

        private readonly By _productShortcutsLocator = By.CssSelector("ul.shortcuts a");
        private readonly By _itemsTableLocator = By.CssSelector(@"table.dataTable");
        private readonly By _removeButtonsLocator = By.CssSelector(@"button[name=remove_cart_item]");


        public CartPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            Init();
        }

        private void Init()
        {
            PageFactory.InitElements(_driver, this);
        }
        private IList<IWebElement> GetProducts()
        {
            return _driver.FindElements(_productShortcutsLocator);
        }

        public void RemoveAllItemsFromCart()
        {
            //6) удалить все товары из корзины один за другим, после каждого удаления подождать, пока внизу обновится таблица            
            var cartItemsCount = GetProducts().Count;
            for (int i = 0; i < cartItemsCount; i++)
            {
                var table = _driver.FindElement(_itemsTableLocator);
                if (!IsElementNotPresent(_driver, _productShortcutsLocator))
                {
                    _driver.FindElement(_productShortcutsLocator).Click(); //клик по первому товару из списка, чтобы остановить перелистывание :)
                }
                _driver.FindElement(_removeButtonsLocator).Click();  //клик по первой кнопке из списка
                _wait.Until(ExpectedConditions.StalenessOf(table));
            }
        }

    }
}

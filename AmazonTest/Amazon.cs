//list of packages are used in the class
using System;
using OpenQA.Selenium;
using NUnit.Framework;

namespace AmazonTest
{
    [TestFixture]
    public class Amazon
    {
        //variables are used in methods of this class
        private OpenQA.Selenium.IWebDriver driver;
        private OpenQA.Selenium.Support.UI.WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            //set values for variables  
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, timeout:TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestAmazon()
        {
            driver.Navigate().GoToUrl("https://www.amazon.com/");

            var InputSearch = driver.FindElement(By.Id("twotabsearchtextbox"));
            InputSearch.SendKeys("c sharp book");

            var Search = driver.FindElement(By.Id("nav-search-submit-button"));
            Search.Click();

            bool resultSearch;

            try
            {
                resultSearch = driver.FindElement(By.XPath("//span[@class = 'rush-component']//span[contains(text(), 'results for')]")).Displayed;
            }
            catch
            {
                resultSearch = false;
            }

            Assert.IsTrue(resultSearch, "Page isn't displayed");

            wait.Until(condition =>
            {
                try
                {
                    var expectedLink = driver.FindElement(By.XPath("//div[@data-cel-widget='search_result_0']//a[text()='Paperback'][1]"));
                    return expectedLink != null;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        
            driver.FindElement(By.XPath("//div[@data-cel-widget='search_result_0']//a[text()='Paperback'][1]")).Click();

            driver.FindElement(By.Id("add-to-cart-button")).Click();

            driver.FindElement(By.Id("nav-cart")).Click();

            bool shoppingCart;

            try
            {
                shoppingCart = driver.FindElement(By.XPath("//div[@id = 'sc-active-cart']//h1[contains(text(), 'Shopping Cart')]")).Displayed;
            }
            catch
            {
                shoppingCart = false;
            }

            Assert.IsTrue(shoppingCart, "Page isn't displayed");

            bool emptyCart;

            try
            {
                emptyCart = driver.FindElement(By.XPath("//div[@id = 'sc-active-cart']//h2[contains(text(), 'Cart is empty')]")).Displayed;
            }
            catch
            {
                emptyCart = false;
            }

            Assert.IsFalse(emptyCart, "Cart isn't empty");
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}
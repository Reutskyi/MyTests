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

        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}
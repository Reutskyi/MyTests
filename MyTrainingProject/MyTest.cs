//список пакетов, которые используются в этом классе
using NUnit.Framework;
using OpenQA.Selenium;
using System;
 
namespace ClassLibrary1
{
    [TestFixture]
    public class MyTest
    {
        //объявление переменных, которые будут использоваться в разных методах этого класса
        private OpenQA.Selenium.IWebDriver driver;
        private OpenQA.Selenium.Support.UI.WebDriverWait wait;
 
        //Метод Start() выполняется каждый раз перед выполнением тестового метода
        [SetUp]
        public void Start()
        {
            //присвоение значений объявленным переменным
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        [Test]
        public void TestWiki()
        {
            //переход на тестируемую страницу по URL
            driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Main_Page");
            // нахождение поля Поиск на странице по имени локатора
            var searchInput = driver.FindElement(By.Name("search"));
            // ввод данных в поле поиска
            searchInput.SendKeys("Penguin");
            // нахождение кнопки Начать Поиск по имени локатора
            var doSearchButton = driver.FindElement(By.Name("go"));
            //нажатие на кнопку
            doSearchButton.Click();
            //проверка условия, что ожидаемый заголовок страницы появился не позже чем через 10 секунд (параметр задан в методе Start()) 
            wait.Until(condition => {
                try
                {
                    var expectedHeader = driver.FindElement(By.XPath("//h1[contains(text(),'Penguin')]"));
                    return expectedHeader != null;
                }
                catch (Exception)
                {
                    return false;
                }
            });       
        }
 
        //Метод Stop() выполняется каждый раз после выполнения тестового метода
        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Guru99Demo
{
    class Guru99Demo
    {
        private ChromeDriver? driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Test()
        {
            driver!.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://www.google.com/");

            var title = driver.Title;
            Console.WriteLine(title);
            Thread.Sleep(5000);  // Let the user actually see something!

            IWebElement searchBox = driver.FindElement(By.Name("q"));
            searchBox.SendKeys("Selenium with C#");
            searchBox.Submit();

            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> searchResults = driver.FindElements(By.CssSelector("h3"));
            foreach (IWebElement result in searchResults)
            {
                Console.WriteLine(result.Text);
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver?.Close();
        }
    }
}

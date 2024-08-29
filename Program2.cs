using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    [TestFixture]
    public class PracticeItem
    {
        private IWebDriver? driver; // Declare driver as nullable

        [SetUp]
        public void SetUp()
        {
            // Initialize the driver in the SetUp method
            
        }

        [Test]
        public void TestGoogleSearch()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://www.google.com/");
            Thread.Sleep(5000);  // Let the user actually see something!
            IWebElement searchBox = driver.FindElement(By.Name("q"));
            searchBox.SendKeys("Selenium with C# example");
            searchBox.SendKeys(Keys.Enter);
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> searchResults = driver.FindElements(By.CssSelector("h3"));
            foreach (IWebElement result in searchResults)
        {
            Console.WriteLine(result.Text);
        }
            driver.Quit();

        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}

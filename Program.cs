using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace SeleniumTests
{
    [TestFixture]
    public class GettingStarted
    {
        private IWebDriver? driver; // Declare driver as nullable

        [SetUp]
        public void SetUp()
        {
            // Initialize the driver in the SetUp method
            driver = new ChromeDriver();
        }

        [Test]
        public void TestGoogleSearch()
        {
            driver!.Manage().Window.Maximize();
            driver!.Navigate().GoToUrl("http://www.google.com/");
            Thread.Sleep(5000);  // Let the user actually see something!

            IWebElement searchBox = driver.FindElement(By.Name("q"));
            searchBox.SendKeys("ChromeDriver");
            searchBox.Submit();

            Thread.Sleep(5000);  // Let the user actually see something!
        }

        [TearDown]
        public void TearDown()
        {
            driver!.Quit();
        }
    }
}

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWithCSharp
{
    [TestFixture]
    public class MultipleTests
    {
        private IWebDriver driver { get; set; } = null!;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void TestCase1_NavigateToExample()
        {
            driver.Navigate().GoToUrl("https://www.example.com");
            Thread.Sleep(2000);
            Console.WriteLine(driver.Title);
            Assert.IsTrue(driver.Title.Contains("Example"), "Title does not contain 'Example'");
        }

        [Test]
        public void TestCase2_SearchUsingGoogle()
        {
            driver.Navigate().GoToUrl("https://www.google.com");
            IWebElement searchBox = driver.FindElement(By.Name("q"));
            searchBox.SendKeys("Selenium WebDriver");
            searchBox.Submit();
            Console.WriteLine(driver.Title);
            Assert.IsTrue(driver.Title.Contains("Selenium WebDriver"), "Title does not contain 'Selenium WebDriver'");
        }

        [Test]
        public void TestCase3_NavigateToBing()
        {
            driver.Navigate().GoToUrl("https://www.bing.com");
            IWebElement searchBox = driver.FindElement(By.Name("q"));
            searchBox.SendKeys("NUnit");
            searchBox.Submit();
            Console.WriteLine(driver.Title);
            Assert.IsTrue(driver.Title.Contains("NUnit"), "Title does not contain 'NUnit'");

            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> searchResults = driver.FindElements(By.CssSelector("h3"));
            foreach (IWebElement result in searchResults)
            {
                Console.WriteLine(result.Text);
            }
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            
        }
    }
}

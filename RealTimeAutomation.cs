using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace RealTimeTest
{
    [TestFixture]
    public class Selenium
    {
         private WebDriver driver { get; set; } = null!;

        [SetUp]
        public void LaunchBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver?.Close();
        }

        [Test]
        public void TestCase1()
        {
            driver.Navigate().GoToUrl("https://quickref.me/index.html");
            Console.WriteLine(driver.Title);

            IWebElement quickRefHeader = driver.FindElement(By.XPath("//html/body/header/div[1]/div/a/span"));
            Console.WriteLine(quickRefHeader.Text);

            Assert.AreEqual(quickRefHeader.Text,"QuickRef.ME", "Page header does not match the expected header.");

            IWebElement searchArea = driver.FindElement(By.XPath("/html/body/section[2]/div/section/a[1]/div"));
            searchArea.Click();

            // IWebElement searchBox = driver.FindElement(By.Id("mysearch-input"));
            // searchBox.SendKeys("C#");

            // IWebElement searchResult = driver.FindElement(By.XPath("//*[@id='mysearch-list']/li/a"));
            // searchResult.Click();

            // IWebElement copyCode = driver.FindElement(By.XPath("//*[@id='mdLayout']/div[2]/div/div[1]/div/pre[1]/button"));
            // Console.WriteLine(copyCode.Text);
            // searchResult.Click();
        }
    }
}
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DemoQA
{
    class ProgressBar
    {
        IWebDriver driver = new ChromeDriver();

        [SetUp]
        public void SetUp()
        {
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        [Test]
        public void Test()
        {
            try
            {
                driver.Navigate().GoToUrl("https://demoqa.com/progress-bar");

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0,250)");
                Thread.Sleep(2000);

                IWebElement Title = driver.FindElement(By.XPath("//h1[@class='text-center']"));
                Console.WriteLine("Title: " + Title.Text);

                IWebElement startStopButton = driver.FindElement(By.Id("startStopButton"));
                startStopButton.Click();

                Thread.Sleep(6500);
                startStopButton.Click();

                IWebElement percentage = driver.FindElement(By.XPath("//div[@role='progressbar']"));
                Console.WriteLine(percentage.Text);
                Thread.Sleep(2000);

                startStopButton.Click();
                Thread.Sleep(4000);

                IWebElement resetButton = driver.FindElement(By.Id("resetButton"));
                Assert.IsTrue(resetButton.Displayed, "Message if false");

                IList<IWebElement> elements = driver.FindElements(By.ClassName("className"));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                Assert.Fail("Test failed due to an exception: " + ex.Message);
            }
        }

    }

}
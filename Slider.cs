using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DemoQA
{
    class Slider
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
            driver.Quit();
        }

        [Test]
        public void Test()
        {
            try
            {
                driver.Navigate().GoToUrl("https://demoqa.com/slider");

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0,250)");
                Thread.Sleep(2000);

                IWebElement Title = driver.FindElement(By.XPath("//h1[@class='text-center']"));
                Console.WriteLine("Title: " + Title.Text);

                IWebElement slider = driver.FindElement(By.XPath("//div[@class='range-slider__tooltip__label']"));
                // Actions actions = new Actions(driver);
                // actions.ClickAndHold(slider)
                //        .MoveByOffset(70, 0)
                //        .Release()
                //        .Perform();

                IJavaScriptExecutor sl = (IJavaScriptExecutor)driver;
                sl.ExecuteScript("arguments[0].value = arguments[1];", slider, "50");

                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                Assert.Fail("Test failed due to an exception: " + ex.Message);
            }
        }

    }

}
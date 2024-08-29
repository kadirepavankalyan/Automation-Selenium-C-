using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Globalization;

namespace DemoQA
{
    class DatePicker
    {
        IWebDriver driver = new ChromeDriver();
        private void ClearDatePickerInput(IWebElement element)
        {
            Actions actions = new(driver);
            actions.Click(element)
                   .KeyDown(Keys.Control)
                   .SendKeys("a")
                   .KeyUp(Keys.Control)
                   .SendKeys(Keys.Backspace)
                   .Perform();
        }

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
                driver.Navigate().GoToUrl("https://demoqa.com/date-picker");

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0, 300)");

                IWebElement datePickerDropdown = driver.FindElement(By.XPath("//div[@id='datePickerContainer']/h1"));
                Console.WriteLine("Page title: " + datePickerDropdown.Text);
                Assert.AreEqual("Date Picker", datePickerDropdown.Text);

                string currentDate = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                Console.WriteLine("Current date: " + currentDate);

                IWebElement datePickerInput = driver.FindElement(By.XPath("//input[@id='datePickerMonthYearInput']"));
                string datePickerValue = datePickerInput.GetAttribute("value");
                Console.WriteLine("Date picker value: " + datePickerValue);
                Assert.AreEqual(currentDate, datePickerValue);

                ClearDatePickerInput(datePickerInput);

                string newDate = "06/24/1999";
                datePickerInput.SendKeys(newDate);
                datePickerInput.SendKeys(Keys.Enter);

                datePickerValue = datePickerInput.GetAttribute("value");
                Console.WriteLine("New date picker value: " + datePickerValue);
                Assert.AreEqual(newDate, datePickerValue);
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

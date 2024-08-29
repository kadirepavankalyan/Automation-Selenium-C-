using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RadioButton
{
    class RadioButtons
    {
        [Test]
        public void RadioButton() {
            IWebDriver driver = new ChromeDriver();
            try
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://demoqa.com/radio-button");

                IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
                js.ExecuteScript("window.scrollBy(0,300)", "");
                Thread.Sleep(2000);
                
                IWebElement YesButton = driver.FindElement(By.XPath("//*[@id='app']/div/div/div/div[2]/div[2]/div[2]/label"));
                YesButton.Click();
                Thread.Sleep(2000);
                IWebElement SuccessMessage = driver.FindElement(By.ClassName("text-success"));
                Assert.AreEqual("Yes", SuccessMessage.Text);

                IWebElement Item = driver.FindElement(By.XPath("(//*[@id='item-3']/span)[1]"));
                Item.Click();

                IJavaScriptExecutor jss = (IJavaScriptExecutor) driver;
                jss.ExecuteScript("window.scrollBy(0,300)", "");
                Thread.Sleep(2000);

                for (int i = 1; i <= 3; i++)
                {
                    for (int j = 1; j <= 6; j++)
                    {
                        IWebElement RowData = driver.FindElement(By.XPath("//*[@id='app']/div/div/div/div[2]/div[2]/div[3]/div[1]/div[2]/div[" + i +  "]/div/div[" + j +"]"));
                        Console.WriteLine(RowData.Text);
                    }
                }
                driver.Close();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("The specified element does not exist.");
            }
        }
    }
}
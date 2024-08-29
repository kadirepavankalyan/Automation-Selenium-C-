using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Linq.Expressions;

namespace QADemo
{
    class PracticeElements
    {
        [Test]
        public void Test()
        {
            IWebDriver driver = new ChromeDriver();
        try {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://demoqa.com/text-box");

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollBy(0,370)", "");
            Thread.Sleep(3000);

            IWebElement userName = driver.FindElement(By.XPath("//*[@id='userName']"));
            userName.SendKeys("Jagga");

            IWebElement Email = driver.FindElement(By.XPath("//*[@id='userEmail']"));
            Email.SendKeys("Jagga@example.com");

            IWebElement CurrentAddress = driver.FindElement(By.XPath("//*[@id='currentAddress']"));
            CurrentAddress.SendKeys("2-4, Banglore, 560068");

            IWebElement PermanentAddress = driver.FindElement(By.XPath("//*[@id='permanentAddress']"));
            PermanentAddress.SendKeys("2-4, hyderabad, 568829");
            Thread.Sleep(1000);

            IWebElement submitButton = driver.FindElement(By.XPath("//*[@id='submit']"));
            submitButton.Click();

            IWebElement output = driver.FindElement(By.XPath("//*[@id='output']"));
            Console.WriteLine(output.Text);

            driver.Close();
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Element not found.");
        }
        catch (WebDriverTimeoutException)
        {
            Console.WriteLine("Timeout while waiting for the element.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        }
    }
}

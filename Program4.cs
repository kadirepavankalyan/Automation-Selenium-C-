using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Linq.Expressions;

namespace ToolsQA
{
    class FindElementCommands
    {
        [Test]
        public void Test()
        {
            try
            {
                IWebDriver driver = new ChromeDriver();

                driver.Manage().Window.Maximize();

                driver.Navigate().GoToUrl("https://toolsqa.com/selenium-webdriver/c-sharp/how-to-handle-different-types-of-alert-and-popup-box-in-selenium-csharp/");
                Console.WriteLine(driver.Title);

                driver.FindElement(By.ClassName("feedback-text")).Click();

                IWebElement header = driver.FindElement(By.XPath("(//*[@class='feedback-form']/div/div/label)[1]"));
                Console.WriteLine(header.Text);
                Assert.AreEqual(header.Text, "Title of the Issue:");

                Thread.Sleep(2000);

                Actions actions = new Actions(driver);
                actions.SendKeys(Keys.Escape).Perform();

                IWebElement home = driver.FindElement(By.XPath("/html/body/div[1]/div/section[1]/div/div/ul/li[1]/a"));
                home.Click();

                IWebElement heading = driver.FindElement(By.ClassName("new-training__heading"));
                Console.WriteLine("Website heading:" + heading.Text);

                Thread.Sleep(2000);

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0,630)", "");

                IReadOnlyCollection<IWebElement> courses = driver.FindElements(By.XPath("/html/body/div[1]/div[2]/div/div/div/a/div[2]/div[1]"));
                List<IWebElement> elementList = new List<IWebElement>(courses);
                // Print all course texts
                foreach (IWebElement result in courses)
                {
                    Console.WriteLine("Courses available in:" + result.Text);
                }
                Thread.Sleep(5000);

                elementList = new List<IWebElement>(courses);
                if (elementList.Count > 0)
                {
                    // Create a random number generator
                    Random random = new();
                    int randomIndex = random.Next(elementList.Count);
                    IWebElement randomElement = elementList[randomIndex];
                    randomElement.Click();
                    Console.WriteLine("Clicked on element with text: " + randomElement.Text);
                }
                // IWebElement CourseLink = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div/div/a[2]"));
                // CourseLink.Click();

                Thread.Sleep(2000);

                // Print the current URL
                // string currentUrl = driver.Url;
                // Console.WriteLine("Current URL is: " + currentUrl);

                // IWebElement courseTitle = driver.FindElement(By.ClassName("article-meta-data__title"));
                // Console.WriteLine("Course Title: " + courseTitle.Text);

                // IWebElement authorProfile = driver.FindElement(By.XPath("//section[1]/div[2]/div/div[1]/div/div[4]/a"));
                // string hrefValue = authorProfile.GetAttribute("href");
                // Console.WriteLine("The Author's LinkedIn Profile link: " + hrefValue);

                // IWebElement linkElement = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/section[1]/div[3]/div[2]/a"));
                // string hrefLink = linkElement.GetAttribute("href");
                // // handling new tab in browser
                // IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                // jse.ExecuteScript("window.open();");
                // var windowHandles = driver.WindowHandles;
                // driver.SwitchTo().Window(windowHandles[windowHandles.Count - 1]);

                // driver.Navigate().GoToUrl(hrefLink);

                // Thread.Sleep(5000);

                //search 
                // string seleniumCourse = "Selenium wait commands";
                // IWebElement searchText = driver.FindElement(By.XPath("(//*[@id='search-form']/input)[2]"));
                // searchText.SendKeys(seleniumCourse);
                // actions.SendKeys(Keys.Enter).Perform();

                // IReadOnlyCollection<IWebElement> SearchedCourse = driver.FindElements(By.ClassName("article__details"));
                // // Print all course texts
                // foreach (IWebElement result in SearchedCourse)
                // {
                //     Console.WriteLine("Courses display after search:" + result.Text);
                // }
                // Thread.Sleep(5000);

                // IWebElement Menu = driver.FindElement(By.ClassName("navbar__tutorial-menu--menu-bars"));
                // Menu.Click();

                // IReadOnlyCollection<IWebElement> menuList = driver.FindElements(By.XPath("/html/body/nav/div/div/div[1]/div/ul/li/div/span"));
                // // Print all course texts
                // foreach (IWebElement result in menuList)
                // {
                //     Console.WriteLine("menu list:" + result.Text);
                // }
                // Thread.Sleep(5000);

                driver.Quit();
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

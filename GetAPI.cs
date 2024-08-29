using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;

namespace DemoQA
{
    class GetAPI
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

        public class Book
        {
            public required string Isbn { get; set; }
            public required string Title { get; set; }
            public required string SubTitle { get; set; }
            public required string Author { get; set; }
            public DateTime PublishDate { get; set; }
            public required string Publisher { get; set; }
            public int Pages { get; set; }
            public required string Description { get; set; }
            public required string Website { get; set; }
        }

        [Test]
        public void Test()
        {
            try
            {
                driver.Navigate().GoToUrl("https://demoqa.com/login");

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0,250)");
                Thread.Sleep(2000);

                IWebElement userName = driver.FindElement(By.Id("userName"));
                userName.SendKeys("PavanKalyan");

                IWebElement password = driver.FindElement(By.Id("password"));
                password.SendKeys("Invalid");

                IWebElement loginButton = driver.FindElement(By.Id("login"));
                loginButton.Click();

                Thread.Sleep(2000);
                IWebElement message = driver.FindElement(By.XPath("//*[@id='output']/div/p"));
                Assert.AreEqual("Invalid username or password!", message.Text);

                password = driver.FindElement(By.Id("password"));
                password.Clear();
                password.SendKeys("P@van#7019");
                loginButton = driver.FindElement(By.Id("login"));
                loginButton.Click();
                Thread.Sleep(4000);

                IWebElement logoutButton = driver.FindElement(By.XPath("//div/button[text()='Log out']"));
                Assert.IsTrue(logoutButton.Displayed);

                Thread.Sleep(1000);
                js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0,350)");
                IWebElement goToStore = driver.FindElement(By.Id("gotoStore"));
                goToStore.Click();
                Thread.Sleep(1000);

                // Make the API call
                var client = new RestClient("https://demoqa.com/BookStore/v1/");
                var request = new RestRequest("Books", Method.Get);

                var response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {
                    var responseObject = JsonConvert.DeserializeObject<BookResponse>(response.Content);

                    if (responseObject?.Books != null && responseObject.Books.Count > 0)
                    {
                        foreach (var book in responseObject.Books)
                        {
                            Console.WriteLine($"ISBN: {book.Isbn}, Title: {book.Title}, Author: {book.Author}, Pages: {book.Pages}, website: {book.Website}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No books found.");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.Content}");
                }
            }
            catch (StaleElementReferenceException)
            {
                Console.WriteLine("Stale Element Reference Exception error.");
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
        public class BookResponse
        {
            public required List<Book> Books { get; set; }
        }
    }
}

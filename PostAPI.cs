using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;

namespace DemoQA
{
    class PostAPI
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

        public class LoginRequest
        {
            public required string UserName { get; set; }
            public required string Password { get; set; }
        }

        public class LoginResponse
        {
            public required string UserId { get; set; }
            public required string Username { get; set; }
            public required string Password { get; set; }
            public required string Token { get; set; }
            public required string Expires { get; set; }
            public required string Created_date { get; set; }
            public bool IsActive { get; set; }
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
                Thread.Sleep(3000);

                IWebElement logoutButton = driver.FindElement(By.XPath("//div/button[text()='Log out']"));
                Assert.IsTrue(logoutButton.Displayed);

                // Make the API call
                var client = new RestClient("https://demoqa.com/Account/v1/");
                var request = new RestRequest("Login", Method.Post);

                var loginRequest = new LoginRequest
                {
                    UserName = "PavanKalyan",
                    Password = "P@van#7019"
                };

                request.AddJsonBody(loginRequest);

                var response = client.Execute(request);

                if (response.IsSuccessful && response.Content != null)
                {
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
                    if (loginResponse != null)
                    {
                        Console.WriteLine($"UserId: {loginResponse.UserId}");
                        Console.WriteLine($"Username: {loginResponse.Username}");
                        Console.WriteLine($"Password: {loginResponse.Password}");
                        Console.WriteLine($"Token: {loginResponse.Token}");
                        Console.WriteLine($"Expires: {loginResponse.Expires}");
                        Console.WriteLine($"Created_date: {loginResponse.Created_date}");
                        Console.WriteLine($"IsActive: {loginResponse.IsActive}");
                    }
                    else
                    {
                        Console.WriteLine("Error: Unable to deserialize response content.");
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
    }
}

using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Linq.Expressions;

namespace QADemo
{
    class DownloadFile
    {
        [Test]
        public void Test()
        {
            string downloadDirectory = @"C:\demoProjects\Downloads";

            ClearDownloadDirectory(downloadDirectory);

            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", downloadDirectory);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("safebrowsing.enabled", true);

            IWebDriver driver = new ChromeDriver(options);

            try
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://demoqa.com/upload-download");
                Console.WriteLine(driver.Title);

                IWebElement downloadLink = driver.FindElement(By.Id("downloadButton"));
                downloadLink.Click();

                string expectedFileName = "sampleFile.jpeg";

                bool isFileDownloaded = VerifyFileDownloaded(downloadDirectory, expectedFileName);

                if (isFileDownloaded)
                {
                    Console.WriteLine("File downloaded successfully.");
                }
                else
                {
                    Console.WriteLine("File download failed.");
                }

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

        static void ClearDownloadDirectory(string downloadDirectory)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(downloadDirectory);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error clearing download directory: " + e.Message);
            }
        }

        static bool VerifyFileDownloaded(string downloadDirectory, string expectedFileName)
        {
            // Define the full path of the expected file
            string filePath = Path.Combine(downloadDirectory, expectedFileName);

            // Wait for the file to appear (with a timeout)
            int timeout = 30;
            int elapsedTime = 0;

            while (elapsedTime < timeout)
            {
                if (File.Exists(filePath))
                {
                    return true;
                }

                Thread.Sleep(1000); // Wait for 1 second
                elapsedTime++;
            }
            return false;
        }
    }
}

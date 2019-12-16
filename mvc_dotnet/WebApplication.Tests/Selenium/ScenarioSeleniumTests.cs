using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace WebApplication.Tests.Selenium
{
    [TestClass]
    public class ScenarioSeleniumTests
    {
        [TestMethod]
        public void Selenium_ScenarioPage_Details()
        {
            var browserDriverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--start-maximized");

            using (var chromeDriver = new ChromeDriver(browserDriverPath, options))
            {
                var url = "https://localhost:44392/";
                chromeDriver.Navigate().GoToUrl(url);

                var wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 1, 0));

                var mainLogo = chromeDriver.FindElement(By.LinkText("Scenario Assignments"));
                mainLogo.Click();
                var Username = chromeDriver.FindElementById("Email");
                Username.SendKeys("Admin@Admin.com");

                var Password = chromeDriver.FindElementById("Password");
                Password.SendKeys("Password");

                var LogIn = chromeDriver.FindElementById("Login");
                LogIn.Click(); 


                var startAssignment = chromeDriver.FindElementByPartialLinkText("Start");
                startAssignment.Click();

                var Answer = chromeDriver.FindElement(By.LinkText("A) Sure"));
                Answer.Click();

                var NextScene = chromeDriver.FindElement(By.LinkText("Next Scene"));
                NextScene.Click();

                chromeDriver.Close();
            }

        }
    }
}

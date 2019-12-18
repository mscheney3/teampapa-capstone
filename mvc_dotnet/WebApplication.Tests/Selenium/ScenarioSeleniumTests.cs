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
            options.AddArguments("--start-maximized", "--ignore-certificate-errors");

            using (var chromeDriver = new ChromeDriver(browserDriverPath, options))
            {
                var url = "https://sociemoti.fun/";
                chromeDriver.Navigate().GoToUrl(url);

                var wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 1, 0));

                var mainLogo = chromeDriver.FindElement(By.LinkText("Scenario Assignments"));
                mainLogo.Click();
                chromeDriver.FindElement(By.Id("Email")).SendKeys("test@test.com");
                chromeDriver.FindElement(By.Id("Password")).SendKeys("password");
                chromeDriver.FindElement(By.Id("Login")).Click();
                var wait1 = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 1, 0));
                var startAssignment = chromeDriver.FindElementById("begin-assignment");
                startAssignment.Click();

                var wait2 = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 1, 0));
                var Answer1 = chromeDriver.FindElement(By.LinkText("A) Sure"));
                Answer1.Click();

                var wait3 = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 1, 0));
                var NextScene = chromeDriver.FindElement(By.LinkText("Next Scene"));
                NextScene.Click();

                chromeDriver.Close();
            }

        }
    }
}

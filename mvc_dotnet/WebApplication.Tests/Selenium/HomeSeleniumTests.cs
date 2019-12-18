using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebApplication.Tests
{

    [TestClass]
    public class HomeSeleniumTests
    {
        [TestMethod]
        public void Selenium_SearchHomePage_Details()
        {
            var browserDriverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--start-maximized", "--ignore-certificate-errors");

            using (var chromeDriver = new ChromeDriver(browserDriverPath, options))
            {
                var url = "https://sociemoti.fun/";
                chromeDriver.Navigate().GoToUrl(url);



                var wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 1, 0));

                var mainLogo = chromeDriver.FindElement(By.Id("home-logo"));
                mainLogo.Click();


                var homeDisc = chromeDriver.FindElement(By.Id("home-disc"));
                Assert.IsTrue(homeDisc.Text.Contains("What would you like to do?"));

                chromeDriver.Close();
            }


        }


    }

}


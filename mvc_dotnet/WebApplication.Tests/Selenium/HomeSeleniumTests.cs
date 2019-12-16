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
        public class HomeSeleniumTest
        {
            [TestMethod]
            public void Selenium_SearchHomePage_Details()
            {
                var browserDriverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--start-maximized");

                using (var chromeDriver = new ChromeDriver(browserDriverPath, options))
                {
                    var url = "https://localhost:44392/";
                    chromeDriver.Navigate().GoToUrl(url);



                    var wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 1, 0));

                    var mainLogo = chromeDriver.FindElement(By.Id("home-logo"));
                    mainLogo.Click();

                var registerButton = chromeDriver.FindElement(By.LinkText("Register"));
                registerButton.Click();

                    var nav = chromeDriver.FindElement(By.ClassName("navigation"));


                var homeDisc = chromeDriver.FindElement(By.Id("home-disc"));
                Assert.IsTrue(nav.Text.Contains("What would you like to do?"));

                chromeDriver.Close();
                }


            }

            //[TestMethod]
            //public void Search_SurveyResults()
            //{
            //    var browserDriverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //    ChromeOptions options = new ChromeOptions();
            //    options.AddArguments("--start-maximized");

            //    using (var chromeDriver = new ChromeDriver(browserDriverPath, options))
            //    {
            //        var url = "http://localhost:60349/Survey/SurveyResults";
            //        chromeDriver.Navigate().GoToUrl(url);

            //        var wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 1, 0));


            //        var resultStats = chromeDriver.FindElement(By.Id("resultsTable"));

            //        Assert.IsTrue(resultStats.Text.Contains("Park Name"));
            //        Assert.IsTrue(resultStats.Text.Contains("Number of Votes"));


            //        chromeDriver.Close();
            //    }


            //}

        }

    }


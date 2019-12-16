using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace WebApplication.Tests.Selenium
{
    [TestClass]
    public class AccountSeleniumTests
    {

        [TestMethod]
        public void Selenium_Account_AssignStudent()
        {
            var browserDriverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--start-maximized");

            using (var chromeDriver = new ChromeDriver(browserDriverPath, options))
            {
                var login = "https://localhost:44392/Account/Login";
                chromeDriver.Navigate().GoToUrl(login);

                chromeDriver.FindElement(By.Id("Email")).SendKeys("admin@admin.com");
                chromeDriver.FindElement(By.Id("Password")).SendKeys("password");
                chromeDriver.FindElement(By.Id("Login")).Click();

                var wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 1, 0));

                var url = "https://localhost:44392/Account/AssignStudent";
                chromeDriver.Navigate().GoToUrl(url);

                var mainLogo = chromeDriver.FindElement(By.Id("home-logo"));
                mainLogo.Click();

                ////var registerButton = chromeDriver.FindElement(By.LinkText("Register"));


                ////var nav = chromeDriver.FindElement(By.ClassName("navigation"));


                //var homeDisc = chromeDriver.FindElement(By.Id("home-disc"));
                //Assert.IsTrue(homeDisc.Text.Contains("What would you like to do?"));

                chromeDriver.Close();
            }

        }

        [TestMethod]
        public void Selenium_Account_ChangePassword()
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

                //var registerButton = chromeDriver.FindElement(By.LinkText("Register"));


                //var nav = chromeDriver.FindElement(By.ClassName("navigation"));


                var homeDisc = chromeDriver.FindElement(By.Id("home-disc"));
                Assert.IsTrue(homeDisc.Text.Contains("What would you like to do?"));

                chromeDriver.Close();
            }

        }

        [TestMethod]
        public void Selenium_Account_CreateUser()
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

                //var registerButton = chromeDriver.FindElement(By.LinkText("Register"));


                //var nav = chromeDriver.FindElement(By.ClassName("navigation"));


                var homeDisc = chromeDriver.FindElement(By.Id("home-disc"));
                Assert.IsTrue(homeDisc.Text.Contains("What would you like to do?"));

                chromeDriver.Close();
            }

        }


        [TestMethod]
        public void Selenium_Account_ListOfUsers()
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

                //var registerButton = chromeDriver.FindElement(By.LinkText("Register"));


                //var nav = chromeDriver.FindElement(By.ClassName("navigation"));


                var homeDisc = chromeDriver.FindElement(By.Id("home-disc"));
                Assert.IsTrue(homeDisc.Text.Contains("What would you like to do?"));

                chromeDriver.Close();
            }

        }

        [TestMethod]
        public void Selenium_Account_Login()
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

                //var registerButton = chromeDriver.FindElement(By.LinkText("Register"));


                //var nav = chromeDriver.FindElement(By.ClassName("navigation"));


                var homeDisc = chromeDriver.FindElement(By.Id("home-disc"));
                Assert.IsTrue(homeDisc.Text.Contains("What would you like to do?"));

                chromeDriver.Close();
            }

        }

        [TestMethod]
        public void Selenium_Account_Register()
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

                //var registerButton = chromeDriver.FindElement(By.LinkText("Register"));


                //var nav = chromeDriver.FindElement(By.ClassName("navigation"));


                var homeDisc = chromeDriver.FindElement(By.Id("home-disc"));
                Assert.IsTrue(homeDisc.Text.Contains("What would you like to do?"));

                chromeDriver.Close();
            }

        }


        //public void Login()
        //{
        //    var url = "https://localhost:44392/Account/AssignStudent";
        //    chromeDriver.Navigate().GoToUrl(url);

        //    chromeDriver.FindElement(By.Id("Email")).SendKeys("admin@admin.com");
        //    chromeDriver.FindElement(By.Id("Password")).SendKeys("password");
        //    chromeDriver.FindElement(By.Id("Login")).Click();

        //}
    }
}

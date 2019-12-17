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

                var teachers = chromeDriver.FindElement(By.Name("teacher"));
                teachers.Click();


                var student = chromeDriver.FindElement(By.Name("student"));
                student.Click();

                var teacherId = chromeDriver.FindElement(By.Name("teacherId"));
                teacherId.Click();

                var studentId = chromeDriver.FindElement(By.Name("studentId"));
                studentId.Click();

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
                var login = "https://localhost:44392/Account/Login";
                chromeDriver.Navigate().GoToUrl(login);

                chromeDriver.FindElement(By.Id("Email")).SendKeys("admin@admin.com");
                chromeDriver.FindElement(By.Id("Password")).SendKeys("password");
                chromeDriver.FindElement(By.Id("Login")).Click();

                var wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 1, 0));

                var url = "https://localhost:44392/Account/ChangePassword";
                chromeDriver.Navigate().GoToUrl(url);


                chromeDriver.FindElement(By.Id("OldPassword")).SendKeys("password");
                chromeDriver.FindElement(By.Id("NewPassword")).SendKeys("password");
                chromeDriver.FindElement(By.Id("changePassword"));



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
                var login = "https://localhost:44392/Account/Login";
                chromeDriver.Navigate().GoToUrl(login);

                chromeDriver.FindElement(By.Id("Email")).SendKeys("admin@admin.com");
                chromeDriver.FindElement(By.Id("Password")).SendKeys("password");
                chromeDriver.FindElement(By.Id("Login")).Click();

                var wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 1, 0));

                var url = "https://localhost:44392/Account/CreateUser";
                chromeDriver.Navigate().GoToUrl(url);


                chromeDriver.FindElement(By.Id("Username")).SendKeys("test@test.com");
                chromeDriver.FindElement(By.Id("Password")).SendKeys("password");
                chromeDriver.FindElement(By.Id("Role")).SendKeys("Student");

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
                var login = "https://localhost:44392/Account/Login";
                chromeDriver.Navigate().GoToUrl(login);

                chromeDriver.FindElement(By.Id("Email")).SendKeys("admin@admin.com");
                chromeDriver.FindElement(By.Id("Password")).SendKeys("password");
                chromeDriver.FindElement(By.Id("Login")).Click();

                var wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 1, 0));

                var url = "https://localhost:44392/Account/ListOfUsers";
                chromeDriver.Navigate().GoToUrl(url);


                chromeDriver.FindElement(By.Id("user_Id")).SendKeys("41");
                chromeDriver.FindElement(By.Id("user_Password")).SendKeys("password");
                chromeDriver.FindElement(By.Id("user_Role")).SendKeys("Student");

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
                var login = "https://localhost:44392/Account/Login";
                chromeDriver.Navigate().GoToUrl(login);

                chromeDriver.FindElement(By.Id("Email")).SendKeys("admin@admin.com");
                chromeDriver.FindElement(By.Id("Password")).SendKeys("password");
                chromeDriver.FindElement(By.Id("Login")).Click();

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

                var login = "https://localhost:44392/Account/Register";
                chromeDriver.Navigate().GoToUrl(login);

                chromeDriver.FindElement(By.Id("Email")).SendKeys("test@test.com");
                chromeDriver.FindElement(By.Id("Password")).SendKeys("password");
                chromeDriver.FindElement(By.Id("ConfirmPassword")).SendKeys("password");
                chromeDriver.FindElement(By.Id("Register")).Click();


                chromeDriver.Close();
            }

        }


    }
}

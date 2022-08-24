using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using WebDriverManager.DriverConfigs.Impl;

namespace TestProject1
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver=new ChromeDriver();
            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitWait=(TimeSpan.FromSeconds(5));
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            //TestContext.Progress.WriteLine(driver.Title);
            //TestContext.Progress.WriteLine(driver.Url);

            driver.FindElement(By.Id("username")).SendKeys(" rahulshettyacademy");
            driver.FindElement(By.Name("password")).SendKeys(" 123456");
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span[1]/input[@id='terms']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();
            //driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();


            //Thread.Sleep(3000);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(driver.FindElement(By.Id("signInBtn")),"Sign In"));

            String errorMessage=driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errorMessage);


           IWebElement link= driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
           String hrefAttr=link.GetAttribute("href");
           String expectedUrl="https://rahulshettyacademy.com/documents-request";

            Assert.AreEqual(expectedUrl, hrefAttr);

          

            driver.Close();
        }




        [TearDown]

        public void CloseBrowser()
        {
           
        }
    }
}
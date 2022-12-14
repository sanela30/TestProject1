using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
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
           //driver.FindElement(By.XPath("//label[@class='customradio'][2]/span[@class='checkmark']")).Click();

          
            IList<IWebElement> radioButons = driver.FindElements(By.XPath("//label/input[@type='radio']"));


            foreach (IWebElement radioButon in radioButons)
            {
                if (radioButon.GetAttribute("value").Equals("user"))
                  {
                    radioButon.Click();
                  }
            }
            Thread.Sleep(3000);
            driver.FindElement(By.Id("okayBtn")).Click();


            IWebElement dropDown = driver.FindElement(By.CssSelector("select.form-control"));
            SelectElement s = new SelectElement(dropDown);
            s.SelectByText("Teacher");

            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span[1]/input[@id='terms']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();
            //driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();
            //Thread.Sleep(3000);

            //explicit wait helper
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(driver.FindElement(By.Id("signInBtn")),"Sign In"));

            String errorMessage=driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errorMessage);

            //validate url
            IWebElement link= driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hrefAttr=link.GetAttribute("href");
            String expectedUrl="https://rahulshettyacademy.com/documents-request";

            Assert.AreEqual(expectedUrl, hrefAttr);

           

            driver.Close();
        }
        /*
        [Test]

        public void test2()
    
            { }

        */

        [TearDown]

        public void CloseBrowser()
        {
           
        }
    }
}
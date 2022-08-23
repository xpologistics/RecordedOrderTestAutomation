using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace ReorderValidation
{
    public class DriverContext
    {
        IWebDriver driver;
        WebDriverWait wait;

        public IWebDriver SelectBrowser1()
        {

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            driver.Navigate().GoToUrl("https://uat-clm.xpo.com/LastmilePortal/angular2/");
            return driver;
        }

      
    }

}


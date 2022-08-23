using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReorderValidation
{
    public class BaseUITest
    {
        IWebDriver driver;
        string URL;
        public string UserName = "";
        public string UserPassword = "";
        LoginPage loginPage;
        WebDriverWait wait;

                

        public BaseUITest(IWebDriver driver)
         {
             this.driver = driver;
            PageFactory.InitElements(this.driver, this);
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();

        }
        
        
        public TPage GetPageInstance<TPage>() where TPage:LoginPage,new()
        {
            TPage pageInstance=new TPage();
             PageFactory.InitElements(driver, pageInstance);

            return pageInstance;
        }
        public void SwitchToFrame(IWebDriver driver, int index=0)
        {
            driver.SwitchTo().Frame(index);
        }
        public string GetCurrentWindow() => driver.CurrentWindowHandle;
        public void CloseCurrentWindow() => driver.Close();

        public void SwitchToDefaultScreen() => driver.SwitchTo().DefaultContent();
        public IWebElement WaitTillElementIsVisible(By locator)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
             var element=wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return element;
        }
        public void WaitTillFrameToBeLoaded(By locator)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            var element = wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(locator));
           
        }
        public IWebElement WaitTillElementIsClickable(IWebElement element)
        {
            try
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                var elementR = wait.Until(ExpectedConditions.ElementToBeClickable(element));
                return elementR;
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"WaitTillElementIsClickable:{ex}");
                return null;
            }
           
        }
        public bool WaitTillTextToBePresentInElementValue(IWebElement element,string text)
        {
         return wait.Until(ExpectedConditions.TextToBePresentInElementValue(element,text));
        }

        public bool WaitTillElementDisplayed(IWebDriver driver,IWebElement element)
        {
             wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(60));
             wait.Until(driver => element.Displayed);
             return true;
        }
        public int GetElementCount(By locator) => driver.FindElements(locator).Count();
        public void MoveToElementAndClick(IWebElement element)
        {
            Actions a = new Actions(driver);
            a.MoveToElement(element).Click(element).Build().Perform();
        }
        public void JClick(IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;                    
            js.ExecuteScript("arguments[0].click();", element);
        }

        public WebElement JFindElementByTagName(string tagname)
        {
            string javascript = $"document.getElementsByTagName('{tagname}')";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebElement element = (WebElement)js.ExecuteScript(javascript);
            return element;
        }
        public WebElement JFindElementById(string ID)
        {
            string javascript = $"document.getElementById('{ID}')";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebElement element = (WebElement)js.ExecuteScript(javascript);
            return element;
        }
        public void Delay()
        {
            Thread.Sleep(1700);
        }

    }
}


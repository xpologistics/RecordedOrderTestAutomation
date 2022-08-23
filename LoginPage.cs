using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ReorderValidation
{
    public class LoginPage:BaseUITest
    {
        IWebDriver driver;
        public LoginPage(IWebDriver driver):base(driver)
        {
            this.driver = driver;
           PageFactory.InitElements(this.driver, this);
        }
        //[FindsBy(How = How.XPath, Using = "//input[@name='Username']")]
        [FindsBy(How = How.XPath, Using = "//input[@type='email']")]
        private IWebElement Username;

        [FindsBy(How = How.XPath, Using = "//input[@type='submit' and @value='Next']")]
        private IWebElement NextButton;

        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        private IWebElement Password;

        [FindsBy(How = How.XPath, Using = "//input[@type='submit' and @value='Sign in']")]
        private IWebElement SignInButton;

        [FindsBy(How = How.XPath, Using = "//input[@type='button' and @value='No']")]
        private IWebElement NoButton;

        [FindsBy(How = How.XPath, Using = "//splash-dialog")]
        private IWebElement SplashDialogE;

        By SplashDialog = By.XPath("//splash-dialog");

        [FindsBy(How = How.XPath, Using = "//button[@type='button' and @id='saveBtn']//span[text()='Got It']")]
        private IWebElement GotItButton;

      /*  [FindsBy(How = How.XPath, Using = "//iframe")]
        private List<IWebElement> Frame;*/

        [FindsBy(How = How.XPath, Using = "//div[contains(@id,'cdk-overlay')]//mat-dialog-container")]
        private IWebElement OverlayDialog;

        By OverlayingDialog = By.XPath("//div[contains(@id,'cdk-overlay')]//mat-dialog-container");

        [FindsBy(How = How.XPath, Using = "//button[text()='Close']")]
        private IWebElement CloseButton;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'permanentMenu')]//li[@data-menuitem='Orders']//a")]
        private IWebElement OrderMenu;

        [FindsBy(How = How.XPath, Using = "//li[@data-menuitem='Orders']//a//span[@title='Sales Orders']")]
        private IWebElement SalesOrdersSubMenu;

        [FindsBy(How = How.XPath, Using = "//li[@data-menuitem='Orders']//a//span[@title='Work Orders']")]
        private IWebElement WorkOrdersSubMenu;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'permanentMenu')]//li[@data-menuitem='Routes']//a")]
        private IWebElement RoutesMenu;

        [FindsBy(How = How.XPath, Using = "//li[@data-menuitem='Routes']//a//span[@title='Management']")]
        private IWebElement ManagementSubMenu;

        [FindsBy(How = How.XPath, Using = "//li[@data-menuitem='Routes']//a//span[@title='Management']//a//span[@title='Delivery Trips']")]
        private IWebElement RMDeliveryTripsSubMenu;
        
        public IWebElement GetUsername() => Username;
        public IWebElement GetPassword() => Password;
        public IWebElement GetSignInButton() => SignInButton;

        public IWebElement GetNextButton() => NextButton;
        public IWebElement GetNoButton() => NoButton;
        public IWebElement GetSplashDialog() => SplashDialogE;

        public int GetSplashDialogCount()
        {
           // var element=JFindElementByTagName("splash-dialog");
            var count = driver.FindElements(SplashDialog).Count;
            return count;
        }
        public IWebElement GetGotItButton() => GotItButton;
      //  public List<IWebElement> GetFrameCount() => Frame;

        public IWebElement GetOverlayDialog() => OverlayDialog;
        public IWebElement GetCloseButton() => CloseButton;

        By OverlayDialogPop = By.XPath("//div[contains(@id,'cdk-overlay')]//mat-dialog-container");
        public By getOverlayDialogPop()
        {
            return OverlayDialogPop;
        }
        public WorkOrderPage Login(string UserName, string UserPassword)
        {
            // loginPage = new LoginPage(driver);
            //TODO: PageLoad Timeout
            Delay();
            WaitTillElementIsClickable(GetUsername());
            GetUsername().SendKeys(UserName);

            WaitTillElementIsClickable(GetNextButton());
            GetNextButton().Click();

            WaitTillElementIsClickable(GetPassword());
            GetPassword().SendKeys(UserPassword);

            WaitTillElementIsClickable(GetSignInButton());
            GetSignInButton().Click();

            WaitTillElementIsClickable(GetNoButton());
            GetNoButton().Click();
            Delay();
           Delay();
            //TODO: PageLoad Timeout
            //  if (GetSplashDialogCount()>0)
            CloseSplashDialog();
            Delay();
            var WorkOrderPage = ClickOnWorkOrderPage();
            return WorkOrderPage;
        }
        private void CloseSplashDialog()
        {
            if (GetElementCount(SplashDialog) > 0)
              //  if (SplashDialogE.Displayed)
                {
                WaitTillElementIsClickable(GetGotItButton());
                GetGotItButton().Click();
                CloseOverlayingDialog();
                //JClick(driver, JFindElementById("saveBtn"));
            }
        //    Delay();
           
        }
        private void CloseOverlayingDialog()
        {
            /*  if (GetElementCount(OverlayingDialog) > 0)
                  //  if (OverlayDialog.Displayed)
                    {*/
            Delay();
            Delay();
            SwitchToFrame(driver);
           
                WaitTillElementIsVisible(getOverlayDialogPop());
                
                WaitTillElementIsClickable(GetCloseButton());
                GetCloseButton().Click();
                SwitchToDefaultScreen();
         //   }
        }

        public WorkOrderPage ClickOnWorkOrderPage()
        {
            Delay();
           // CloseSplashDialog();
            WaitTillElementIsClickable(OrderMenu);
            if(OrderMenu.Displayed)
            {
                OrderMenu.Click();
                if(WorkOrdersSubMenu.Displayed)
                {
                    WorkOrdersSubMenu.Click();
                    return new WorkOrderPage(driver);
                }
                return null;
            } 
            else
            {
                return null;
            }
               
           
        }
        public RouteManagementPage? ClickOnRMDeliveryTripsPage()
        {
            if (RoutesMenu.Displayed)
            {
                RoutesMenu.Click();
                if (ManagementSubMenu.Displayed)
                {
                    ManagementSubMenu.Click();
                    RMDeliveryTripsSubMenu.Click();
                    return new RouteManagementPage(driver);
                }
            }
            return null;

        }
    }
}

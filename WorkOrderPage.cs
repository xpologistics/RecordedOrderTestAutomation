using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReorderValidation
{
    public class WorkOrderPage : BaseUITest
    {

        IWebDriver driver;

        public WorkOrderPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this.driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//xpo-input-container//input[@placeholder='Quick Search']")]
        private IWebElement WOQuickSearch;

        [FindsBy(How = How.XPath, Using = "//xpo-icon[@class='xico-action-magnifier']")]
        private IWebElement QuickSearchMagnifierIcon;

        [FindsBy(How = How.XPath, Using = "//div[@col-id='workOrderId']//a")]
        private IWebElement WorkOrderSearchLinkE;
        By WorkOrderSearchLink = By.XPath("//div[@col-id='workOrderId']//a");


        #region WorkOrderSearchListSection

        By WorkOrderStatus = By.XPath("//*[@col-id='status']");
        By SalesOrderStatus = By.XPath("//*[@col-id='status']");

        By OrderCategory = By.XPath("//div[@col-id='salesOrderCategoryId']/span/span");//gettext

        [FindsBy(How = How.XPath, Using = "//button[contains(@class,'xpo-quicksearch')]//xpo-icon")]
        private IWebElement CloseInWOSearchE;
        By CloseInWOSearch = By.XPath("//button[contains(@class,'xpo-quicksearch')]//xpo-icon");
        #endregion WorkOrderSearchListSection

        #region WorkOrderDetailedHeader
        [FindsBy(How = How.XPath, Using = "//xpo-data-full-drawer-backlink//following-sibling::h3//a")]
        private IWebElement BackArrowToWorkOrderSearchE;
        By BackArrowToWorkOrderSearch = By.XPath("//xpo-data-full-drawer-backlink//following-sibling::h3//a");
        By WorkOrderAccount = By.XPath("//span[contains(text(),'Account#')]//following-sibling::span");

        By WorkOrderMarket = By.XPath("//span[contains(text(),'Market')]//following-sibling::span");
        
        By WorkOrderCategory = By.XPath("//span[@id='OM-WorkorderDataDrawer-WorkOrderCategory']//following-sibling::span");

        By WorkOrderDetailedStatus = By.XPath("//span[@id='OM-WorkorderDataDrawer-WorkOrderStatus']//following-sibling::span");
        By WorkOrderOperationalStatus = By.XPath("//span[@id='OM-WorkorderDataDrawer-OperationalStatus']//following-sibling::span");
        By WorkOrderSettlementStatus = By.XPath("//span[@id='OM-WorkorderDataDrawer-SettlementStatus']//following-sibling::span");
        By WorkOrderBillingStatus = By.XPath("//span[contains(@id,'OM-WorkorderDataDrawer-BillingStatus')]/following-sibling::span");
        #endregion WorkOrderDetailedHeader

        //WO details

        #region GeneralTabDetails
        By OriginAddress = By.XPath("//span[normalize-space()='Origin']/../../../parent::xpo-card//span[@class='address-block']");
        By DestinationAddress = By.XPath("//span[normalize-space()='Destination']/../../../parent::xpo-card//span[@class='address-block']");
        By CustomerOrderNumber = By.XPath("//xpo-card[@class='order-refNum-details']//dt[contains(text(),'Customer')]//following-sibling::dd");

        #endregion GeneralTabDetails

        #region ServiceTabDetails
        // By ServiceGroupFuelSurcharge = By.XPath("//span[contains(text(),'Additional Services')]//parent::div[@class='service-actions']/..//xpo-card[2]//xpo-detail-item[2]//b[contains(text(),'Service Group')]");
        //xpo-card[@class='work-order-services-card'][2]//xpo-detail-item[2]//b[contains(text(),'Service Group')]
        By ServiceGroupFuelSurcharge = By.XPath("//xpo-card[2]//xpo-detail-item[2]//span");
        //xpo-card[2]//xpo-detail-item[2]//b[contains(text(),'Service Group')]
        #endregion ServiceTabDetails
        #region WorkOrderDetailsTabs
        [FindsBy(How = How.XPath, Using = "//div[@class='xpo-tab-list']//div[normalize-space()='Products']")]
        private IWebElement WorkOrderProductTabE;
        By WorkOrderProductTab = By.XPath("//div[@class='xpo-tab-list']//div[normalize-space()='Products']");

        [FindsBy(How = How.XPath, Using = "//div[@class='xpo-tab-list']//div[normalize-space()='Trips']")]
        private IWebElement WorkOrderTripsTabE;
        By WorkOrderTripsTab = By.XPath("//div[@class='xpo-tab-list']//div[normalize-space()='Trips']");

        [FindsBy(How = How.XPath, Using = "//div[@class='xpo-tab-list']//div[normalize-space()='Services']")]
        private IWebElement WorkOrderServicesTabE;
        By WorkOrderServicesTab = By.XPath("//div[@class='xpo-tab-list']//div[normalize-space()='Services']");

        [FindsBy(How = How.XPath, Using = "//div[@class='xpo-tab-list']//div[normalize-space()='General']")]
        private IWebElement WorkOrderGeneralTabE;
        By WorkOrderGeneralTab = By.XPath("//div[@class='xpo-tab-list']//div[normalize-space()='General']");

        [FindsBy(How = How.XPath, Using = "//span[@class='work-order-trip-title-value']//a")]
        private IWebElement RouteIDFromTripsTabE;
        By RouteIDFromTripsTab = By.XPath("//span[@class='work-order-trip-title-value']//a");

        #endregion WorkOrderDetailsTabs




        #region WorkOrderPageMethods
        public IWebElement GetWOQuickSearch() => WOQuickSearch;
        public void ClickQuickSearchMagnifierIcon() => QuickSearchMagnifierIcon.Click();
        public string EnterWOinQuickSearch(string workorderID)
        {
            Delay();
            WaitTillElementIsClickable(GetWOQuickSearch());
            GetWOQuickSearch().SendKeys(workorderID);

            WaitTillElementIsClickable(QuickSearchMagnifierIcon);
            ClickQuickSearchMagnifierIcon();
            WaitTillElementIsClickable(WorkOrderSearchLinkE);
            WorkOrderSearchLinkE.Click();
            Delay();
            return workorderID;
        }
        public void NavigateToTripsTab()
        {
            Delay();
            if (WorkOrderTripsTabE.Displayed)
            {
                WaitTillElementIsClickable(WorkOrderTripsTabE);
                WorkOrderTripsTabE.Click();
                Delay();
            }
        }
        public void NavigateToWorkOrderProductTab()
        {
            if (WorkOrderProductTabE.Displayed)
            {
                WaitTillElementIsClickable(WorkOrderProductTabE);
                WorkOrderProductTabE.Click();
            }
        }
        public void NavigateToWorkOrderServicesTab()
        {
            Delay();
            if (WorkOrderServicesTabE.Displayed)
            {
                WaitTillElementIsClickable(WorkOrderServicesTabE);
                WorkOrderServicesTabE.Click();
                Delay();
                Delay();
            }
        }
        public string GetAdditionalServicesFuelSurchargeType => driver.FindElement(ServiceGroupFuelSurcharge).Text;
        public void NavigateToWorkOrderGeneralTab()
        {
            if (WorkOrderGeneralTabE.Displayed)
            {
                WaitTillElementIsClickable(WorkOrderGeneralTabE);
                WorkOrderGeneralTabE.Click();
            }
        }
        public string GetOriginAddress() => driver.FindElement(OriginAddress).Text;
        public string GetDestinationAddress() => driver.FindElement(DestinationAddress).Text;
        public string GetCustomerOrderNumber() => driver.FindElement(CustomerOrderNumber).Text;


        public void GoBackToWOSearchPage()
        {
            WaitTillElementIsClickable(BackArrowToWorkOrderSearchE);
            BackArrowToWorkOrderSearchE.Click();
            Delay();
            WaitTillElementIsClickable(GetWOQuickSearch());
          //  WaitTillElementIsClickable(CloseInWOSearchE);
        //    CloseInWOSearchE.Click();
            GetWOQuickSearch().Clear();
        }
        public string GetWorkOrderAccountName() => driver.FindElement(WorkOrderAccount).Text;
        public string GetWorkOrderMarket()
        {
          WaitTillElementIsVisible(WorkOrderMarket);
            return driver.FindElement(WorkOrderMarket).Text;
        }
        public string GetWorkOrderCategory() => driver.FindElement(WorkOrderCategory).Text;
        public string GetWorkOrderStatus()=>driver.FindElement(WorkOrderDetailedStatus).Text;
        public string GetWorkOrderOperationalStatus => driver.FindElement(WorkOrderOperationalStatus).Text;
        public string GetWorkOrderSettlementStatus => driver.FindElement(WorkOrderSettlementStatus).Text;
        public string GetWorkOrderBillingStatus => driver.FindElement(WorkOrderBillingStatus).Text;

        // public string GetCustomerOrderNumber =>driver.FindElement(CustomerOrderNumber).Text;
        public string RouteIDFromWOPage => driver.FindElement(RouteIDFromTripsTab).Text;
        public RouteManagementPage SwitchToRMDeliveryTripsScreen()
        {
            
            Delay();
            WaitTillElementIsClickable(RouteIDFromTripsTabE);
            RouteIDFromTripsTabE.Click();
            string RouteManagementsTab = driver.WindowHandles[1];
            driver.SwitchTo().Window(RouteManagementsTab);
            return new RouteManagementPage(driver);
        }
        #endregion WorkOrderPageMethods
        public WorkOrderPage SwitchToWorkOrderWindowTab(WorkOrderPage workOrderPage)
        {
            var windows=driver.WindowHandles;
            string currWindow=driver.CurrentWindowHandle;
            string WOTab = driver.WindowHandles[0];
            driver.Close();
             driver.SwitchTo().Window(WOTab);
           // driver.Navigate().Refresh();
            return new WorkOrderPage(driver);
            
        }
    }
}

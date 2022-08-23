using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReorderValidation
{
    public class RouteManagementPage : BaseUITest
    {
        private IWebDriver driver;
        public RouteManagementPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//td[@col-id='routeStatusName']//span")]
        private IWebElement RouteStatus;

        [FindsBy(How = How.XPath, Using = "//td[@col-id='routeSystemId']/span/span")]
        private IWebElement RouteID;

        [FindsBy(How = How.XPath, Using = "//td[@col-id='routeStartTime']//span//span")]
        private IWebElement RouteStartTime;

        [FindsBy(How = How.XPath, Using = "//td[@col-id='routeEndTime']//span//span")]
        private IWebElement RouteEndTime;

        [FindsBy(How = How.XPath, Using = "//table[@id='RouteManagementTable']//td[@col-id='totalMiles']//span")]
        private IWebElement RoundTripDistance;

        [FindsBy(How = How.XPath, Using = "//td[@col-id='expandRow-0']/span/img")]
        private IWebElement RouteExpandIcon;

        [FindsBy(How = How.XPath, Using = "//table[@id='RouteManagementTable']//table[contains(@id,'Stop')]//tbody/tr[contains(@data-type,'PU')]")]
        private IWebElement PUStop;

        [FindsBy(How = How.XPath, Using = "//table[@id='RouteManagementTable']//table[contains(@id,'Stop')]//tbody/tr[contains(@data-type,'DEL/SVC')]")]
        private IWebElement DeliveryStopsE;
        By DeliveryStops = By.XPath("//table[@id='RouteManagementTable']//table[contains(@id,'Stop')]//tbody/tr[contains(@data-type,'DEL/SVC')]");

        [FindsBy(How = How.XPath, Using = "//table[contains(@id,'Stop')]//tr[contains(@data-type,'DEL/SVC')]//td[@col-id='workOrderId']//span//span")]
        private IWebElement ListOfWorkOrdersE;
        By ListOfWorkOrders = By.XPath("//table[contains(@id,'Stop')]//tr[contains(@data-type,'DEL/SVC')]//td[@col-id='workOrderId']//span//span");

        By StopTable = By.XPath("//table[contains(@id,'Stop')]//tr[contains(@data-type,'DEL/SVC')]");
        By StopNumbers = By.XPath("//td[@col-id='plannedSequence']//span[contains(@class,'stops')]");
        By WOinStops = By.XPath("//td[@col-id='workOrderId']//span//span");
        public void ExpandRoute()
        {
            WaitTillElementIsClickable(RouteExpandIcon);
            RouteExpandIcon.Click();
            Delay();
        }
        public string GetRouteStatusRMPage => RouteStatus.Text;
        public string GetRouteIDfromRMPage => RouteID.Text;
        public string GetRouteStartTime => RouteStartTime.Text;
        public string GetRouteEndTime => RouteEndTime.Text;
        public string GetTotalRoundTripDistance => RoundTripDistance.Text;
        public List<string> GetListOfWorkOrders()
        {
            var ListOfWO = driver.FindElements(ListOfWorkOrders);
            var TotalWorkOrders = new List<String>();
            foreach (var WorkOrders in ListOfWO)
            {
                TotalWorkOrders.Add(WorkOrders.Text);
            }
            return TotalWorkOrders;
        }
        public Dictionary<string, string> GetStopNumberWorkOrderNumber()         
        {
            var stopTable = driver.FindElement(StopTable);
            Dictionary<string, string> StopSeqWorkOrderId = new Dictionary<string, string>();
            var woNumber=stopTable.FindElements(WOinStops);
            var actualStopsSequence = stopTable.FindElements(StopNumbers);
          
                for (int i = 1; i < actualStopsSequence.Count; i++)
                {
                    StopSeqWorkOrderId.Add(woNumber[i].Text.ToString(), actualStopsSequence[i].Text.ToString());
                }
                return StopSeqWorkOrderId;
           
        }
         

    }
}

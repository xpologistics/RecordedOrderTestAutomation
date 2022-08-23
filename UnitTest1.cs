using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace ReorderValidation
{
    public class Tests
    {
        public IWebDriver driver;
        string URL;
        public string UserName = "";
        public string UserPassword = "";
        LoginPage loginPage;
        WebDriverWait wait;
        IOrderedEnumerable<IGrouping<string, SuccessCSVValues>> cSSGroupedValues;
        [SetUp]
        public void Setup()
        {
            Logger.CreateLogFile();
            cSSGroupedValues = SuccessCSVValues.GetGroupedRoutes("Indiana", SuccessCSVValues.eColumns);
           // cSSGroupedValues = SuccessCSVValues.GetGroupedRoutes("Boston", SuccessCSVValues.BostonHeaders);
            
            if (cSSGroupedValues == null)
            {
                Assert.Fail("Mismatch of Columns");
            }
            SelectBrowser();
            GetLoginCredentials();
        }
        private void GetLoginCredentials()
        {
            //   var configPath= ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;

            URL = ConfigurationManager.AppSettings["AppUrl"];
            UserName = ConfigurationManager.AppSettings["Username"];
            UserPassword = ConfigurationManager.AppSettings["Password"];
        }
        public IWebDriver SelectBrowser()
        {

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            driver.Navigate().GoToUrl("https://uat-clm.xpo.com/LastmilePortal/angular2/");
            return driver;
        }

        public void PageRefresh()
        {
            driver.Navigate().Refresh();
        }
        [Test]
        public void TestCSVValues()
        {
            try
            {
                DBHelper dbhelper= new DBHelper();
          var dt=  dbhelper.GetFacilityDetails("HD - Indiana Flat");
                if (cSSGroupedValues == null)
                {
                    Assert.Fail("Mismatch of Columns");
                }
               var colval= dt.Columns.ToString();
                // ch = new BaseUITest(driver);
                LoginPage loginPage = new LoginPage(driver);
                var WOPage = loginPage.Login(UserName, UserPassword);
                
                Logger.WriteLog("[PASS]: Login Successful");
                ExtentReportsHelper.LogReports();
               // var values = SuccessCSVValues.GetGroupedRoutes();
                Dictionary<string, string> ExpectedStopWOSequence = new Dictionary<string, string>();
                List<string> ExpectedWOSequence = new List<string>();
                List<string> ActualWOSequence = new List<string>();
                List<string> ExpectedStopSequence = new List<string>();
                List<string> ActualStopSequence = new List<string>();
                Dictionary<string, string> ActualStopWOSequence;
                foreach (var Route in cSSGroupedValues)
                {
                    foreach (var WODetails in Route)
                    {
                        var WOValue = WODetails.WorkOrderID;
                        WOPage.EnterWOinQuickSearch(WOValue);
                        Logger.WriteLog($"[INFO]: EnterWOinQuickSearch for WorkOrder ID: {WOValue}");

                        //************** WO heading validation

                        WOPage.NavigateToWorkOrderServicesTab();
                        WOPage.NavigateToWorkOrderGeneralTab();
                        VerifyWOHeaderValues("Account Name Check", WOPage.GetWorkOrderAccountName(), "785 - The Home Depot Dedicated Flat US");//TODO: need DB query
                        VerifyWOHeaderValues("Order Category Check", WOPage.GetWorkOrderCategory(), WODetails.StopType.Remove(0, 9).ToUpper());
                        VerifyWOHeaderValues("Order status Check", WOPage.GetWorkOrderStatus(), "CLOSED");

                        VerifyWOHeaderValues("Market name Check", WOPage.GetWorkOrderMarket(), "HD - Indiana Flat");//TODO: need DB query
                        VerifyWOHeaderValues("Billing status Check", WOPage.GetWorkOrderBillingStatus, "NOT STARTED");
                        VerifyWOHeaderValues("Settlement status Check", WOPage.GetWorkOrderSettlementStatus, "IN AUDIT");
                        //************** WO details validation
                        ValidateDestinationAddress(WOPage, WODetails);
                        ValidateOriginAddress(WOPage, "2225 NORTH POST ROAD, INDIANAPOLIS, IN, 46219, US");//TODO: need DB query
                        VerifyWOHeaderValues("Customer Order Check", WOPage.GetCustomerOrderNumber(), WODetails.CustomerOrderNumber);

                        ValidateServiceCharges(WOPage, WODetails, Route);

                        WOPage.NavigateToTripsTab();
                        VerifyRouteID(WOPage, WODetails);
                        if (Route.First().WorkOrderID.Equals(WODetails.WorkOrderID))
                        {
                            var RMPage = WOPage.SwitchToRMDeliveryTripsScreen();
                            RMPage.ExpandRoute();

                            ActualStopWOSequence = RMPage.GetStopNumberWorkOrderNumber();
                            foreach (var StopWO in ActualStopWOSequence)
                            {
                                Logger.WriteLog($"[INFO]: ActualStopSequence: {StopWO.Value} and related WorkOrderID: {StopWO.Key}");
                                ActualWOSequence.Add(StopWO.Key);
                                ActualStopSequence.Add(StopWO.Value);
                            }
                            Logger.WriteLog($"[INFO]: RouteStatus:{RMPage.GetRouteStatusRMPage}");
                            Logger.WriteLog($"[INFO]: TotalRoundTripDistance:{RMPage.GetTotalRoundTripDistance}");
                            Logger.WriteLog($"[INFO]: RouteStartTime:{RMPage.GetRouteStartTime} and RouteEndTime : {RMPage.GetRouteEndTime}");

                            WOPage.SwitchToWorkOrderWindowTab(WOPage);
                        }
                        ExpectedStopWOSequence.Add(WODetails.WorkOrderID, WODetails.StopNumber);
                        ExpectedWOSequence.Add(WODetails.WorkOrderID);
                        ExpectedStopSequence.Add(WODetails.StopNumber);
                        WOPage.GoBackToWOSearchPage();
                    }

                    VerifyWOStopSequence(ExpectedWOSequence, ActualWOSequence,"WO Sequence"); 
                    VerifyWOStopSequence(ExpectedStopSequence, ActualStopSequence,"Stop Sequence");
                }
            }
            finally
            {
                driver.Quit();
            }

        }

        private static void VerifyRouteID(WorkOrderPage WOPage, SuccessCSVValues WODetails)
        {
            var RouteID = WOPage.RouteIDFromWOPage.Split('_', 2);
            Logger.WriteLog($"[INFO]: Route ID On WorkOrder Page is :{WOPage.RouteIDFromWOPage}");

            bool RouteIDCompare = RouteID[0].ToString().Equals(WODetails.RouteID);
            Logger.WriteLog($"{(RouteIDCompare ? "[PASS]" : "[FAIL]")}: Route ID On CSV file is: {WODetails.RouteID} and {(RouteIDCompare ? "matches" : "does not matches")} with first Occurence on WorkOrder page {WODetails.RouteID}");
        }

        private static void VerifyWOStopSequence(List<string> ExpectedWOSequence, List<string> ActualWOSequence, string Check)
        {
            var comVale = ExpectedWOSequence.Except(ActualWOSequence).ToList();
            var revcomvalue = ActualWOSequence.Except(ExpectedWOSequence).ToList();

            var result = (comVale.Count==0 || revcomvalue.Count==0) ? "[PASS]" : "[FAIL]";
            Logger.WriteLog($"{result}: Total WorkOrderList Check: Count is {ActualWOSequence.Count}. {Check} Check {((comVale.Count == 0 || revcomvalue.Count == 0) ? "matching" : "not matching")}.");
            ValidateWOSequence(ExpectedWOSequence, ActualWOSequence);
        }

        private static void ValidateServiceCharges(WorkOrderPage WOPage, SuccessCSVValues WODetails, IGrouping<string, SuccessCSVValues> Route)
        {
            if (Route.First().WorkOrderID.Equals(WODetails.WorkOrderID))
            {
                WOPage.NavigateToWorkOrderServicesTab();

                if (WODetails.VehicleType.Contains("BOXTRUCK"))
                {
                    string compareServices = WOPage.GetAdditionalServicesFuelSurchargeType.Contains("Box") ? "PASS" : "FAIL";
                    Logger.WriteLog($"[{compareServices}]: ServiceCharges Check: Expected Vehicle type from CSV is:{WODetails.VehicleType} and Additional Service charge is:{WOPage.GetAdditionalServicesFuelSurchargeType}.");
                }
                else if (WODetails.VehicleType.Contains("FLAT"))
                {
                    string compareServices = WOPage.GetAdditionalServicesFuelSurchargeType.Contains("Flat") ? "PASS" : "FAIL";
                    Logger.WriteLog($"[{compareServices}]: ServiceCharges Check: Expected Vehicle type from CSV is:{WODetails.VehicleType} and Additional Service charge is:{WOPage.GetAdditionalServicesFuelSurchargeType}.");

                }
            }
        }

        private static void ValidateWOSequence(List<string> ExpectedWOSequence, List<string> ActualWOSequence)
        {
            for (int i = 0; i < ExpectedWOSequence.Count; i++)
            {
                var ExpectedVal = ExpectedWOSequence[i].ToString();
                var ActualVal = ActualWOSequence[i].ToString();
                
                Logger.WriteLog($"{(ExpectedVal.Equals(ActualVal) ? "[PASS]" : "[FAIL]")}: ExpectedVal: {ExpectedVal}, ActualVal: {ActualVal}");
            }
        }

       

        private static void ValidateOriginAddress(WorkOrderPage WOPage, string ExpectedOriginAddress)
        {
            var ActualOriginAddress = WOPage.GetOriginAddress();
            bool OriginAddressCompare = ActualOriginAddress.Contains(ExpectedOriginAddress);
            Logger.WriteLog($"{(OriginAddressCompare ? "[PASS]" : "[FAIL]")}: Origin Address Check: Actual Origin Address is {ActualOriginAddress} and ExpectedOriginAddress is{ExpectedOriginAddress}");

        }

        private static void ValidateDestinationAddress(WorkOrderPage WOPage, SuccessCSVValues WODetails)
        {
            var ExpectedDestinationAddress = WODetails.StopAddress.ToUpper() + ", " + WODetails.StopCity.ToUpper() + ", " + WODetails.StopState.ToUpper() + ", " + WODetails.StopZip.ToUpper();
           
            var ActualDestinationAddress = WOPage.GetDestinationAddress();
          
            bool DestinationAddressCompare = ActualDestinationAddress.Contains(ExpectedDestinationAddress);
            
            Logger.WriteLog($"{(DestinationAddressCompare ? "[PASS]" : "[FAIL]")}: Destination Address Check: Actual Destination Address is {ActualDestinationAddress}  Expected Destination address is {ExpectedDestinationAddress}");
        }

        public void VerifyWOHeaderValues(string ValidationCheck, string Actual, String Expected)
        {
            if (Actual != null)
            {
                var isCompare = Expected.Equals(Actual) ? "matches" : "does not matches";
                var result = Expected.Equals(Actual) ? "[PASS]" : "[FAIL]";
                Logger.WriteLog($"{result}: {ValidationCheck}-{Expected} value {isCompare} with {Actual}");
            }
            else
            {
                Logger.WriteLog($"[ERROR]: {ValidationCheck}-{Expected} value with Actual is NULL");
            }
        }
    }
}
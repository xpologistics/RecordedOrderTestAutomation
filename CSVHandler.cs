using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReorderValidation
{
    internal class CSVHandler
    {
        //csv methods
    }
    public class SuccessCSVValues
    {
        public List<SuccessCSVValues> list = null;
        IOrderedEnumerable<IGrouping<string, SuccessCSVValues>>? routeList = null;
        //List<string> routeList=null;
        public static List<string> eColumns = new List<string> { "Market", "CarrierName","RouteID","ShipDate","DriverName",
            "DriverKey","TractorNumber","CustomerOrderNumber","DeliveryWorkOrderNumber","Orderkey","StopNumber",
            "ActualDistance","StraightLineDistance","DepartTime","ArriveTime","SellingStoreNumber","FulfillmentLocationNumber",
            "FulfillmentLocationType","DestinationNumber","DestinationType","DestinationPostalCode","PurchasedServiceType","ServiceOptions",
                "ActualQuantity","Weight","TollCharge","VehicleType","CustomerDelay","StoreDelay","BillableStop","StopType",
                "SCOPIndicator","StopName","StopAddress","StopCity","StopState","StopZip","StopPhone","OutofAreaMilage","GeoStopNumber",
                "EarliestTime","LatestTime","TrackingNumber","DeliveryRateType","SalesOrderID","CLMRouteID","WorkOrderID","Error","TotalRouteDistance" };
        public static List<string> BostonHeaders = new List<string>()
        {
            "arrivaltime","carrier","city","codamount","customername","departtime","driverpservicetime","dropno",
            "earliesttime","general1","general2","general3","general4","latesttime","parrivaltime","pdeparttime",
            "phonehome","phoneoffice","promisedtime","salesperson","scheduledDate","serialno","specialhandling",
            "state","stoptype","store","streetname","streetno","streetnomodifier","streetprefix","streetsuffix",
            "streettype","suite","ticket","vehicle_1","zip","ColumnBinding","code","G3_carrier","g1_carrier","ColumnBinding_1",
            "operator","truck","GV_carrier","GV_doce","GV_helper","tuck1","driver1","GV_carrier1","full_address","citystatezip",
            "Special_instruction","Est_arrivaltime","format_date","try_year","strparrivaltime","ColumnBinding_2","Sche_date",
            "Time_window","dock1","helper1","ColumnBinding_3","ColumnBinding_4","statusmessage","Aggregation","Generals","SPH",
            "PRECallIns","extra1","requestDate","weight","pieceqty","thecode","dock","location","ordertype","deliverytype",
            "barcodemsg","SalesOrderID","CLMRouteID","WorkOrderID","Error"
        };
        //properties
        #region IndianaMarket
        public string Market { get; set; }
        public string CarrierName { get; set; }
        public string RouteID { get; set; }
        public string ShipDate { get; set; }
        public string DriverName { get; set; }
        public string DriverKey { get; set; }
        public string TractorNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string DeliveryWorkOrderNumber { get; set; }
        public string Orderkey { get; set; }
        public string StopNumber { get; set; }
        public string ActualDistance { get; set; }
        public string StraightLineDistance { get; set; }
        public string DepartTime { get; set; }
        public string ArriveTime { get; set; }
        public string SellingStoreNumber { get; set; }
        public string FulfillmentLocationNumber { get; set; }
        public string FulfillmentLocationType { get; set; }
        public string DestinationNumber { get; set; }
        public string DestinationType { get; set; }
        public string DestinationPostalCode { get; set; }
        public string PurchasedServiceType { get; set; }
        public string ServiceOptions { get; set; }
        public string ActualQuantity { get; set; }
        public string Weight { get; set; }
        public string TollCharge { get; set; }
        public string VehicleType { get; set; }
        public string CustomerDelay { get; set; }
        public string StoreDelay { get; set; }
        public string BillableStop { get; set; }
        public string StopType { get; set; }
        public string SCOPIndicator { get; set; }
        public string StopName { get; set; }
        public string StopAddress { get; set; }
        public string StopCity { get; set; }
        public string StopState { get; set; }
        public string StopZip { get; set; }
        public string StopPhone { get; set; }
        public string OutofAreaMilage { get; set; }
        public string GeoStopNumber { get; set; }
        public string EarliestTime { get; set; }
        public string LatestTime { get; set; }
        public string TrackingNumber { get; set; }
        public string DeliveryRateType { get; set; }
        public string SalesOrderID { get; set; }
        public string CLMRouteID { get; set; }
        public string WorkOrderID { get; set; }
        public string Error { get; set; }
        public string TotalRouteDistance { get; set; }

        #endregion IndianaMarket

        public static SuccessCSVValues FromCsv(string csvLine)
        {
            try
            {

                String[] values = System.Text.RegularExpressions.Regex.Split(csvLine, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                SuccessCSVValues successCSVValues = new SuccessCSVValues();
                successCSVValues.Market = Convert.ToString(values[GetHeaderIndex("Market")]).Trim('\"').Trim();
                successCSVValues.CarrierName = Convert.ToString(values[GetHeaderIndex("CarrierName")]).Trim('\"').Trim();
                successCSVValues.RouteID = Convert.ToString(values[GetHeaderIndex("RouteID")]).Trim('\"').Trim();
                successCSVValues.ShipDate = Convert.ToString(values[GetHeaderIndex("ShipDate")]).Trim('\"').Trim();
                successCSVValues.DriverName = Convert.ToString(values[GetHeaderIndex("DriverName")]).Trim('\"').Trim();
                successCSVValues.DriverKey = Convert.ToString(values[GetHeaderIndex("DriverKey")]).Trim('\"').Trim();
                successCSVValues.TractorNumber = Convert.ToString(values[GetHeaderIndex("TractorNumber")]).Trim('\"').Trim();
                successCSVValues.CustomerOrderNumber = Convert.ToString(values[GetHeaderIndex("CustomerOrderNumber")]).Trim('\"').Trim();
                successCSVValues.DeliveryWorkOrderNumber = Convert.ToString(values[GetHeaderIndex("DeliveryWorkOrderNumber")]).Trim('\"').Trim();
                successCSVValues.Orderkey = Convert.ToString(values[GetHeaderIndex("Orderkey")]).Trim('\"').Trim();
                successCSVValues.StopNumber = Convert.ToString(values[GetHeaderIndex("StopNumber")]).Trim('\"').Trim();
                successCSVValues.ActualDistance = Convert.ToString(values[GetHeaderIndex("ActualDistance")]).Trim('\"').Trim();
                successCSVValues.StraightLineDistance = Convert.ToString(values[GetHeaderIndex("StraightLineDistance")]).Trim('\"').Trim();
                successCSVValues.DepartTime = Convert.ToString(values[GetHeaderIndex("DepartTime")]).Trim('\"').Trim();
                successCSVValues.ArriveTime = Convert.ToString(values[GetHeaderIndex("ArriveTime")]).Trim('\"').Trim();
                successCSVValues.SellingStoreNumber = Convert.ToString(values[GetHeaderIndex("SellingStoreNumber")]).Trim('\"').Trim();
                successCSVValues.FulfillmentLocationNumber = Convert.ToString(values[GetHeaderIndex("FulfillmentLocationNumber")]).Trim('\"').Trim();
                successCSVValues.FulfillmentLocationType = Convert.ToString(values[GetHeaderIndex("FulfillmentLocationType")]).Trim('\"').Trim();
                successCSVValues.DestinationNumber = Convert.ToString(values[GetHeaderIndex("DestinationNumber")]).Trim('\"').Trim();
                successCSVValues.DestinationType = Convert.ToString(values[GetHeaderIndex("DestinationType")]).Trim('\"').Trim();
                successCSVValues.DestinationPostalCode = Convert.ToString(values[GetHeaderIndex("DestinationPostalCode")]).Trim('\"').Trim();
                successCSVValues.PurchasedServiceType = Convert.ToString(values[GetHeaderIndex("PurchasedServiceType")]).Trim('\"').Trim();
                successCSVValues.ServiceOptions = Convert.ToString(values[GetHeaderIndex("ServiceOptions")]).Trim('\"').Trim();
                successCSVValues.ActualQuantity = Convert.ToString(values[GetHeaderIndex("ActualQuantity")]).Trim('\"').Trim();
                successCSVValues.Weight = Convert.ToString(values[GetHeaderIndex("Weight")]).Trim('\"').Trim();
                successCSVValues.TollCharge = Convert.ToString(values[GetHeaderIndex("TollCharge")]).Trim('\"').Trim();
                successCSVValues.VehicleType = Convert.ToString(values[GetHeaderIndex("VehicleType")]).Trim('\"').Trim();
                successCSVValues.CustomerDelay = Convert.ToString(values[GetHeaderIndex("CustomerDelay")]).Trim('\"').Trim();
                successCSVValues.StoreDelay = Convert.ToString(values[GetHeaderIndex("StoreDelay")]).Trim('\"').Trim();
                successCSVValues.BillableStop = Convert.ToString(values[GetHeaderIndex("BillableStop")]).Trim('\"').Trim();
                successCSVValues.StopType = Convert.ToString(values[GetHeaderIndex("StopType")]).Trim('\"').Trim();
                successCSVValues.SCOPIndicator = Convert.ToString(values[GetHeaderIndex("SCOPIndicator")]).Trim('\"').Trim();
                successCSVValues.StopName = Convert.ToString(values[GetHeaderIndex("StopName")]).Trim('\"').Trim();
                successCSVValues.StopAddress = Convert.ToString(values[GetHeaderIndex("StopAddress")]).Trim('\"').Trim();
                successCSVValues.StopCity = Convert.ToString(values[GetHeaderIndex("StopCity")]).Trim('\"').Trim();
                successCSVValues.StopState = Convert.ToString(values[GetHeaderIndex("StopState")]).Trim('\"').Trim();
                successCSVValues.StopZip = Convert.ToString(values[GetHeaderIndex("StopZip")]).Trim('\"').Trim();
                successCSVValues.StopPhone = Convert.ToString(values[GetHeaderIndex("StopPhone")]).Trim('\"').Trim();
                successCSVValues.OutofAreaMilage = Convert.ToString(values[GetHeaderIndex("OutofAreaMilage")]).Trim('\"').Trim();
                successCSVValues.GeoStopNumber = Convert.ToString(values[GetHeaderIndex("GeoStopNumber")]).Trim('\"').Trim();
                successCSVValues.EarliestTime = Convert.ToString(values[GetHeaderIndex("EarliestTime")]).Trim('\"').Trim();
                successCSVValues.LatestTime = Convert.ToString(values[GetHeaderIndex("LatestTime")]).Trim('\"').Trim();
                successCSVValues.TrackingNumber = Convert.ToString(values[GetHeaderIndex("TrackingNumber")]).Trim('\"').Trim();
                successCSVValues.DeliveryRateType = Convert.ToString(values[GetHeaderIndex("DeliveryRateType")]).Trim('\"').Trim();
                successCSVValues.SalesOrderID = Convert.ToString(values[GetHeaderIndex("SalesOrderID")]).Trim('\"').Trim();
                successCSVValues.CLMRouteID = Convert.ToString(values[GetHeaderIndex("CLMRouteID")]).Trim('\"').Trim();
                successCSVValues.WorkOrderID = Convert.ToString(values[GetHeaderIndex("WorkOrderID")]).Trim('\"').Trim();
                successCSVValues.Error = Convert.ToString(values[GetHeaderIndex("Error")]).Trim('\"').Trim();
                successCSVValues.TotalRouteDistance = Convert.ToString(values[GetHeaderIndex("TotalRouteDistance")]).Trim('\"').Trim();
                
                return successCSVValues;
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"[ERROR]: Mismatch of columns observed: Exception is :{ex}");
                return null;
            }


        }
        public List<SuccessCSVValues> SuccessCSVReader(string Market)
        {
          //  List<SuccessCSVValues> values = File.ReadAllLines(@"C:\Users\smestry\OneDrive - XPO Logistics\Desktop\VS\Success-06222022-INDIANA_20220605.csv")
                 List<SuccessCSVValues> values = File.ReadAllLines(GetFilePath(Market))
                                          .Skip(1)
                                          .Select(v => SuccessCSVValues.FromCsv(v))
                                          .ToList();
            return values;
        }

        private static string[] GetHeaders()
        {
            //var header = File.ReadLines(@"C:\Users\smestry\OneDrive - XPO Logistics\Desktop\VS\Success-06222022-INDIANA_20220605.csv").ToList();
            var header = File.ReadLines(GetFilePath("Indiana")).ToList();
            var singleHeader = header[0].Trim('\"').Trim();
            var spaceRemovedHeader = singleHeader.Replace(" ", "");
            string[] stringSeparators = new string[] { "\",\"", "," };
            var arrH = spaceRemovedHeader.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            return arrH;//TODO: need TO ADD Logs(trycatch) on failure
        }
        public static string GetFilePath(string Market)
        {
            var filePath=ConfigurationManager.AppSettings[Market];
            return filePath;
        }
        public static bool compareHeaders(List<string> ExpectedHeaders)
        {
            
            var headerColumns = GetHeaders().ToList();
           // bool HeaderCompare=false;
            string missMatchedColumns = "";
            var val = headerColumns.Except(ExpectedHeaders).ToList();
            bool HeaderCompare =val.Count == 0;
            Logger.WriteLog($"{(HeaderCompare ? "[PASS]" : "[FAIL]")} :Header Columns check is {(HeaderCompare ? "passed" : "failed")} ");
            if (val.Count > 0)
            {
               
                foreach (var mismatchHeader in val)
                {
                    if (val.Count == 1)
                        missMatchedColumns=mismatchHeader;
                    else
                    missMatchedColumns  = missMatchedColumns+((missMatchedColumns=="")?"":",")+mismatchHeader.ToString();
                }
                Logger.WriteLog($"Mismatched columns are {missMatchedColumns}");
            }
            else
            {
                foreach(var col in headerColumns)
                {
                    missMatchedColumns = missMatchedColumns + ((missMatchedColumns == "") ? "" : ",") + col.ToString();
                }
                
                Logger.WriteLog($"[PASS] :Total columns count is {headerColumns.Count} and headers are: {missMatchedColumns}");
            }




            return HeaderCompare;
        }
        private static int GetHeaderIndex(string Header)
        {
            try
            {
                var headerColumns = GetHeaders();
                string headerValue = "";
                int count = 0;
                foreach (var header in headerColumns)
                {
                    headerValue = header;
                    if (header.Equals(Header))
                        return count;
                    else
                        count++;
                }
                Logger.WriteLog($"[ERROR]: Mismatch of columns observed for:{Header} Expected Header present is : {Header} is not found");
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"[ERROR]: Mismatch of columns observed for:{ex}");
            }

            return -1;//TODO: need TO ADD Logs(trycatch) on failure
        }
        public static IOrderedEnumerable<IGrouping<string, SuccessCSVValues>> GetGroupedRoutes(string Market,List<string> Headers)
        {
            if(compareHeaders(Headers))
            {
                SuccessCSVValues dailyValues = new SuccessCSVValues();

                var GroupedrouteList = dailyValues.GroupDataByRouteOrderByStopSequence(dailyValues.SuccessCSVReader(Market));
         
                return GroupedrouteList;
            }
           return null;
        }

        public IOrderedEnumerable<IGrouping<string, SuccessCSVValues>> GroupDataByRouteOrderByStopSequence(List<SuccessCSVValues> SuccessFileValues)
        {

            routeList = from successFileValue in SuccessFileValues
                        group successFileValue by successFileValue.RouteID into routeGroup
                        orderby routeGroup.Key ascending
                        select routeGroup;

            return routeList;
        }

    }
}

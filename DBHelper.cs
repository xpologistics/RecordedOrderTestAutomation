using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ReorderValidation
{
    public class DBHelper
    {
      
        SqlDataAdapter sqldataAdapter = new SqlDataAdapter();
       
        string connectionString = @"Data Source=lmtdw2004.qaamer.qacorp.xpo.com;Initial Catalog=RouteManagement;User Id=developer;Password=Developer1;";

        public static SqlConnection con;
        private DBHelper dBFactory;

        public DBHelper DataBaseFactory { get => dBFactory; set => dBFactory = value; }

        public DBHelper()
        {
            con = new SqlConnection(connectionString);
            DataBaseFactory = this;

        }
        public DataTable ExecuteQuery(string Query)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(Query, con))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                throw new ApplicationException($"Error running query: '{Query}'", ex);
                
            }
            finally
            {
                con.Close();
            }

            return dt;
        }
      
        public DataTable GetFacilityDetails(string WorkorderID)
        {
            string query = $@"select FacilityCode,AddressLine from FacilityManagement..Facilities where MarketId in(select MarketId from FacilityManagement..Markets where MarketName like '%HD - Indiana Flat%')";
            return ExecuteQuery(query);
        }

    }

}


using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace ReorderValidation
{
    public class Logger
    {
    
        private static string logFileName = string.Format("ReorderValidation-LogFile_{0:yyyyMMddhhmmss}", DateTime.Now);
        private static StreamWriter streamWriter =null;
       

        public static void CreateLogFile()
        {
            string dir = Directory.GetCurrentDirectory()+@"\Logs\";
            if(Directory.Exists(dir))
            {
               streamWriter = File.AppendText(dir + logFileName + ".log");
            }
            else
            {
                Directory.CreateDirectory(dir);
                streamWriter = File.AppendText(dir + logFileName + ".log");
            }
        }
        public static void WriteLog(string logMessage)
        {
            streamWriter.WriteLine("[{0}- {1}]:  {2}",DateTime.Now.ToLongTimeString(),DateTime.Now.ToLongDateString(), logMessage);
            streamWriter.Flush();
                        
        }

       
    }
}


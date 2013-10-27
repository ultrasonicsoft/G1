using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultrasonicsoft.Products
{
    public class Logger
    {
        public static void LogException(Exception ex)
        {
            string errorFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\\Errors.log";
            StringBuilder message = new StringBuilder();
            message.AppendFormat(Environment.NewLine);
            message.AppendFormat("==================================================================================================================================");
            message.AppendFormat(Environment.NewLine);
            message.AppendFormat("Date Time: {0}", DateTime.Now.ToString());
            message.AppendFormat(Environment.NewLine);
            message.AppendFormat("Error Message:{0}", ex.Message);
            message.AppendFormat(Environment.NewLine);
            message.AppendFormat("Stack Trace:{0}", ex.StackTrace);

            File.AppendAllText(errorFile, message.ToString());
        }

        public static void LogMessage(string logMessage)
        {
            string errorFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\\Errors.log";
            StringBuilder message = new StringBuilder();
            message.AppendFormat(Environment.NewLine);
            message.AppendFormat("==================================================================================================================================");
            message.AppendFormat(Environment.NewLine);
            message.AppendFormat("Date Time: {0}", DateTime.Now.ToString());
            message.AppendFormat(Environment.NewLine);
            message.AppendFormat("Error Message:{0}", logMessage);

            File.AppendAllText(errorFile, message.ToString());
        }
    }
}

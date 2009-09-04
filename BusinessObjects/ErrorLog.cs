//===============================================================
// Filename: ErrorLog.cs
// Date: 19/08/09
// --------------------------------------------------------------
// Description:
//   Error log
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 19/08/09
// Revision history:
//===============================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace Sedogo.BusinessObjects
{
    public enum logMessageLevel
    {
        errorMessage,
        warningMessage,
        infoMessage,
        debugMessage
    }

    //===============================================================
    // Class: ErrorLog
    //===============================================================
    public class ErrorLog
    {
        //===============================================================
        // Function: ErrorLog()
        // Description: Constructor
        //===============================================================
        public ErrorLog()
        {
        }

        //===============================================================
        // Function: ErrorLog()
        //===============================================================
        public void WriteLog(string moduleName, string functionName, string logText,
            logMessageLevel logLevel)
        {
            DateTime now = DateTime.Now;
            string strDate = now.Year + "/" + now.Month + "/" + now.Day + " ";
            if (now.Minute > 9)
            {
                strDate += now.Hour + ":" + now.Minute;
            }
            else
            {
                strDate += now.Hour + ":0" + now.Minute;
            }

            string logContents = strDate + " - " + moduleName + ":" + functionName + " - " + logText;
            // All error messages are written to the event log
            if (logLevel == logMessageLevel.errorMessage)
            {
                System.Diagnostics.EventLog appLog = new System.Diagnostics.EventLog();
                appLog.Source = "Sedogo";
                appLog.WriteEntry(logContents);
            }
            StreamWriter sw;
            if (logLevel <= GlobalSettings.errorLogLevel)
            {
                sw = new StreamWriter(GlobalSettings.errorLogFile, true);
                sw.WriteLine(logContents);
                sw.Flush();
                sw.Close();
            }
        }
    }
}

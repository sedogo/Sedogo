//===============================================================
// Filename: GlobalSettings.cs
// Date: 19/08/09
// --------------------------------------------------------------
// Description:
//   Global settings class
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

namespace Sedogo.BusinessObjects
{
    public class GlobalSettings
    {
        public static string connectionString = "";
        public static string errorLogFile = "";

        public static logMessageLevel errorLogLevel = logMessageLevel.warningMessage;
        public static string jobNumberMethod = "";

        //===============================================================
        // Function: GlobalSettings (Constructor)
        //===============================================================
        public GlobalSettings()
        {
        }
    }
}

//===============================================================
// Filename: mainForm.cs
// Date: 16/12/06
// --------------------------------------------------------------
// Description:
//   Tasks mainForm
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 16/12/06
// Revision history:
//=============================================================== 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Sedogo.BusinessObjects;

namespace sedogoTasks
{
    public partial class mainForm : Form
    {
        string connectionString = "";
        string errorLogFile;
        logMessageLevel errorLogLevel;

        //===============================================================
        // Function: mainForm (Constructor)
        //===============================================================
        public mainForm()
        {
            InitializeComponent();

            // Load configuration info
            connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"].ToString();
            errorLogFile = System.Configuration.ConfigurationManager.AppSettings["ErrorLogFile"].ToString();
            errorLogLevel = (logMessageLevel)int.Parse(System.Configuration.ConfigurationManager.AppSettings["ErrorLogLevel"].ToString());

            GlobalSettings.connectionString = connectionString;
            GlobalSettings.errorLogFile = errorLogFile;
            GlobalSettings.errorLogLevel = errorLogLevel;

            string[] args = Environment.GetCommandLineArgs();
            foreach (string arg in args)
            {
                if (arg == "-sendAlerts")
                {
                    EventAlert.SendAlertEmails();
                    Environment.Exit(0);
                }
                if (arg == "-sendBroadcast")
                {
                    MiscUtils.SendBroadcastEmail();
                    Environment.Exit(0);
                }
            }
        }

        //===============================================================
        // Function: sendAlertsButton_Click
        //===============================================================
        private void sendAlertsButton_Click(object sender, EventArgs e)
        {
            EventAlert.SendAlertEmails();
        }

        //===============================================================
        // Function: sendBroadcastEmailButton_Click
        //===============================================================
        private void sendBroadcastEmailButton_Click(object sender, EventArgs e)
        {
            MiscUtils.SendBroadcastEmail();
        }
    }
}

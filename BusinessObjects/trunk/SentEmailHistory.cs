//===============================================================
// Filename: SentEmailHistory.cs
// Date: 22/02/10
// --------------------------------------------------------------
// Description:
//   SentEmailHistory class
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 22/02/10
// Revision history:
//===============================================================

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;

namespace Sedogo.BusinessObjects
{
    //===============================================================
    // Class: SentEmailHistory
    //===============================================================
    public class SentEmailHistory
    {
	    private int         m_sentEmailHistoryID = -1;
    	
	    private string      m_sentFrom = "";
	    private string      m_sentTo = "";
	    private string      m_subject = "";
	    private string      m_body = "";
	    private DateTime    m_sentDate = DateTime.MinValue;
	    private string      m_loggedInUserName = "";

        private string m_loggedInUser = "";

        public int sentEmailHistoryID
        {
            get { return m_sentEmailHistoryID; }
        }
        public string sentFrom
        {
            get { return m_sentFrom; }
            set { m_sentFrom = value; }
        }
        public string sentTo
        {
            get { return m_sentTo; }
            set { m_sentTo = value; }
        }
        public string subject
        {
            get { return m_subject; }
            set { m_subject = value; }
        }
        public string body
        {
            get { return m_body; }
            set { m_body = value; }
        }
        public DateTime sentDate
        {
            get { return m_sentDate; }
        }
        public string loggedInUserName
        {
            get { return m_loggedInUserName; }
            set { m_loggedInUserName = value; }
        }

        //===============================================================
        // Function: SentEmailHistory (Constructor)
        //===============================================================
        public SentEmailHistory(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: Add
        //===============================================================
        public void Add()
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spAddSentEmailHistory", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@SentFrom", SqlDbType.NVarChar, 200).Value = m_sentFrom;
                cmd.Parameters.Add("@SentTo", SqlDbType.NVarChar, 200).Value = m_sentTo;
                cmd.Parameters.Add("@Subject", SqlDbType.NVarChar, 200).Value = m_subject;
                cmd.Parameters.Add("@Body", SqlDbType.NVarChar, -1).Value = m_body;
                cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LoggedInUserName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                SqlParameter paramSentEmailHistoryID = cmd.CreateParameter();
                paramSentEmailHistoryID.ParameterName = "@SentEmailHistoryID";
                paramSentEmailHistoryID.SqlDbType = SqlDbType.Int;
                paramSentEmailHistoryID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramSentEmailHistoryID);

                cmd.ExecuteNonQuery();
                m_sentEmailHistoryID = (int)paramSentEmailHistoryID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SentEmailHistory", "Add", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

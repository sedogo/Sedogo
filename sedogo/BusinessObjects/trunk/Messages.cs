//===============================================================
// Filename: Messages.cs
// Date: 28/09/09
// --------------------------------------------------------------
// Description:
//   Messages class
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 28/09/09
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
    // Class: Message
    //===============================================================
    public class Message
    {
        private int         m_messageID = -1;
        private int         m_parentMessageID = -1;
        private int         m_eventID = -1;
        private int         m_userID = -1;
        private int         m_postedByUserID = -1;
        private string      m_messageText = "";
        private Boolean     m_messageRead = false;
        private Boolean     m_deleted = false;
        private DateTime    m_createdDate = DateTime.MinValue;
        private string      m_createdByFullName = "";
        private DateTime    m_lastUpdatedDate = DateTime.MinValue;
        private string      m_lastUpdatedByFullName = "";

        private string      m_loggedInUser = "";

        public int parentMessageID
        {
            get { return m_parentMessageID; }
            set { m_parentMessageID = value; }
        }
        public int messageID
        {
            get { return m_messageID; }
        }
        public int eventID
        {
            get { return m_eventID; }
            set { m_eventID = value; }
        }
        public int userID
        {
            get { return m_userID; }
            set { m_userID = value; }
        }
        public int postedByUserID
        {
            get { return m_postedByUserID; }
            set { m_postedByUserID = value; }
        }
        public string messageText
        {
            get { return m_messageText; }
            set { m_messageText = value; }
        }
        public Boolean messageRead
        {
            get { return m_messageRead; }
            set { m_messageRead = value; }
        }
        public Boolean deleted
        {
            get { return m_deleted; }
        }
        public DateTime createdDate
        {
            get { return m_createdDate; }
        }
        public string createdByFullName
        {
            get { return m_createdByFullName; }
        }
        public DateTime lastUpdatedDate
        {
            get { return m_lastUpdatedDate; }
        }
        public string lastUpdatedByFullName
        {
            get { return m_lastUpdatedByFullName; }
        }

        //===============================================================
        // Function: Message (Constructor)
        //===============================================================
        public Message(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: Message (Constructor)
        //===============================================================
        public Message(string loggedInUser, int messageID)
        {
            m_loggedInUser = loggedInUser;
            m_messageID = messageID;

            ReadMessageDetails();
        }

        //===============================================================
        // Function: ReadMessageDetails
        //===============================================================
        public void ReadMessageDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectMessageDetails";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@MessageID";
                param.Value = m_messageID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventID")))
                {
                    m_eventID = int.Parse(rdr["EventID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("UserID")))
                {
                    m_userID = int.Parse(rdr["UserID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("PostedByUserID")))
                {
                    m_postedByUserID = int.Parse(rdr["PostedByUserID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("MessageText")))
                {
                    m_messageText = (string)rdr["MessageText"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("MessageRead")))
                {
                    m_messageRead = (Boolean)rdr["MessageRead"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Deleted")))
                {
                    m_deleted = (Boolean)rdr["Deleted"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedDate")))
                {
                    m_createdDate = (DateTime)rdr["CreatedDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedByFullName")))
                {
                    m_createdByFullName = (string)rdr["CreatedByFullName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LastUpdatedDate")))
                {
                    m_lastUpdatedDate = (DateTime)rdr["LastUpdatedDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LastUpdatedByFullName")))
                {
                    m_lastUpdatedByFullName = (string)rdr["LastUpdatedByFullName"];
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Message", "ReadMessageDetails", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
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

                SqlCommand cmd = new SqlCommand("spAddMessage", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ParentMessageID", SqlDbType.Int).Value = m_parentMessageID;
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = m_eventID;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = m_userID;
                cmd.Parameters.Add("@PostedByUserID", SqlDbType.Int).Value = m_postedByUserID;
                cmd.Parameters.Add("@MessageText", SqlDbType.NVarChar, -1).Value = m_messageText;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@CreatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                SqlParameter paramMessageID = cmd.CreateParameter();
                paramMessageID.ParameterName = "@MessageID";
                paramMessageID.SqlDbType = SqlDbType.Int;
                paramMessageID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramMessageID);

                cmd.ExecuteNonQuery();
                m_messageID = (int)paramMessageID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Message", "Add", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: Update
        //===============================================================
        public void Update()
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateMessage", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MessageID", SqlDbType.Int).Value = m_messageID;
                cmd.Parameters.Add("@MessageText", SqlDbType.NVarChar, -1).Value = m_messageText;
                cmd.Parameters.Add("@MessageRead", SqlDbType.Bit).Value = m_messageRead;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Message", "Update", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: Delete
        //===============================================================
        public void Delete()
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spDeleteMessage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MessageID", SqlDbType.Int).Value = m_messageID;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Message", "Delete", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: GetMessageCountForEvent
        //===============================================================
        public static int GetMessageCountForEvent(int eventID)
        {
            int messageCount = 0;

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectMessageCountForEvent";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@EventID";
                param.Value = eventID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == true)
                {
                    rdr.Read();
                    messageCount = (int)rdr[0];
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Message", "GetMessageCountForEvent", ex.Message,
                    logMessageLevel.errorMessage);
            }
            finally
            {
                conn.Close();
            }

            return messageCount;
        }

        //===============================================================
        // Function: GetUnreadMessageCountForUser
        //===============================================================
        public static int GetUnreadMessageCountForUser(int userID)
        {
            int messageCount = 0;

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectUnreadMessageCountForUser";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@UserID";
                param.Value = userID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == true)
                {
                    rdr.Read();
                    messageCount = (int)rdr[0];
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Message", "GetUnreadMessageCountForUser", ex.Message,
                    logMessageLevel.errorMessage);
            }
            finally
            {
                conn.Close();
            }

            return messageCount;
        }
        
        //===============================================================
        // Function: GetSentMessageCountForUser
        //===============================================================
        public static int GetSentMessageCountForUser(int userID)
        {
            int messageCount = 0;

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectSentMessageCountForUser";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@UserID";
                param.Value = userID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == true)
                {
                    rdr.Read();
                    messageCount = (int)rdr[0];
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Message", "GetSentMessageCountForUser", ex.Message,
                    logMessageLevel.errorMessage);
            }
            finally
            {
                conn.Close();
            }

            return messageCount;
        }
    }
}

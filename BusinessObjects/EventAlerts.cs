//===============================================================
// Filename: EventAlerts.cs
// Date: 23/10/09
// --------------------------------------------------------------
// Description:
//   Event alerts class
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 23/10/09
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
    // Class: EventAlert
    //===============================================================
    public class EventAlert
    {
        private int         m_eventAlertID = -1;
        private int         m_eventID = -1;
        private DateTime    m_alertDate = DateTime.MinValue;
        private string      m_alertText = "";
        private Boolean     m_completed = false;
        private Boolean     m_deleted = false;
        private DateTime    m_createdDate = DateTime.MinValue;
        private string      m_createdByFullName = "";
        private DateTime    m_lastUpdatedDate = DateTime.MinValue;
        private string      m_lastUpdatedByFullName = "";

        private string      m_loggedInUser = "";

        public int eventAlertID
        {
            get { return m_eventAlertID; }
        }
        public int eventID
        {
            get { return m_eventID; }
            set { m_eventID = value; }
        }
        public DateTime alertDate
        {
            get { return m_alertDate; }
            set { m_alertDate = value; }
        }
        public string alertText
        {
            get { return m_alertText; }
            set { m_alertText = value; }
        }
        public Boolean completed
        {
            get { return m_completed; }
            set { m_completed = value; }
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
        // Function: EventAlert (Constructor)
        //===============================================================
        public EventAlert(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: EventAlert (Constructor)
        //===============================================================
        public EventAlert(string loggedInUser, int eventAlertID)
        {
            m_loggedInUser = loggedInUser;
            m_eventAlertID = eventAlertID;

            ReadEventAlertDetails();
        }

        //===============================================================
        // Function: ReadEventAlertDetails
        //===============================================================
        public void ReadEventAlertDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventAlertDetails";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@EventAlertID";
                param.Value = m_eventAlertID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventID")))
                {
                    m_eventID = int.Parse(rdr["EventID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("AlertDate")))
                {
                    m_alertDate = (DateTime)rdr["AlertDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("AlertText")))
                {
                    m_alertText = (string)rdr["AlertText"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Completed")))
                {
                    m_completed = (Boolean)rdr["Completed"];
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
                errorLog.WriteLog("EventAlert", "ReadEventAlertDetails", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spAddEventAlert", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = m_eventID;
                if( m_alertDate > DateTime.MinValue )
                {
                    cmd.Parameters.Add("@AlertDate", SqlDbType.DateTime).Value = m_alertDate;
                }
                else
                {
                    cmd.Parameters.Add("@AlertDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@AlertText", SqlDbType.NVarChar, -1).Value = m_alertText;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@CreatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                SqlParameter paramEventAlertID = cmd.CreateParameter();
                paramEventAlertID.ParameterName = "@EventAlertID";
                paramEventAlertID.SqlDbType = SqlDbType.Int;
                paramEventAlertID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramEventAlertID);

                cmd.ExecuteNonQuery();
                m_eventAlertID = (int)paramEventAlertID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventAlert", "Add", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spUpdateEventAlert", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EventAlertID", SqlDbType.Int).Value = m_eventAlertID;
                if( m_alertDate > DateTime.MinValue )
                {
                    cmd.Parameters.Add("@AlertDate", SqlDbType.DateTime).Value = m_alertDate;
                }
                else
                {
                    cmd.Parameters.Add("@AlertDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@AlertText", SqlDbType.NVarChar, -1).Value = m_alertText;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventAlert", "Update", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spDeleteEventAlert", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EventAlertID", SqlDbType.Int).Value = m_eventAlertID;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventAlert", "Delete", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: GetEventAlertCountPending
        //===============================================================
        public static int GetEventAlertCountPending(int eventID)
        {
            int alertCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventAlertCountPending";
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                alertCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventAlert", "GetEventAlertCountPending", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return alertCount;
        }

        //===============================================================
        // Function: GetEventAlertCountPendingByUser
        //===============================================================
        public static int GetEventAlertCountPendingByUser(int userID)
        {
            int alertCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventAlertCountPendingByUser";
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                alertCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventAlert", "GetEventAlertCountPendingByUser", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return alertCount;
        }
    }
}

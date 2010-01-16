//===============================================================
// Filename: EventInvites.cs
// Date: 21/10/09
// --------------------------------------------------------------
// Description:
//   Event invites class
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 21/10/09
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
    // Class: EventInvite
    //===============================================================
    public class EventInvite
    {
        private int         m_eventInviteID = -1;
        private string      m_eventInviteGUID = "";
        private int         m_eventID = -1;
        private int         m_userID = -1;
        private string      m_emailAddress = "";
        private string      m_inviteAdditionalText = "";
        private Boolean     m_inviteEmailSent = false;
        private string      m_inviteEmailSentEmailAddress = "";
        private DateTime    m_inviteEmailSentDate = DateTime.MinValue;
        private Boolean     m_inviteAccepted = false;
        private DateTime    m_inviteAcceptedDate = DateTime.MinValue;
        private Boolean     m_inviteDeclined = false;
        private DateTime    m_inviteDeclinedDate = DateTime.MinValue;
        private Boolean     m_deleted = false;
        private DateTime    m_createdDate = DateTime.MinValue;
        private string      m_createdByFullName = "";
        private DateTime    m_lastUpdatedDate = DateTime.MinValue;
        private string      m_lastUpdatedByFullName = "";

        private string      m_loggedInUser = "";

        public int eventInviteID
        {
            get { return m_eventInviteID; }
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
        public string eventInviteGUID
        {
            get { return m_eventInviteGUID; }
        }
        public string emailAddress
        {
            get { return m_emailAddress; }
            set { m_emailAddress = value; }
        }
        public string inviteAdditionalText
        {
            get { return m_inviteAdditionalText; }
            set { m_inviteAdditionalText = value; }
        }
        public Boolean inviteEmailSent
        {
            get { return m_inviteEmailSent; }
            set { m_inviteEmailSent = value; }
        }
        public string inviteEmailSentEmailAddress
        {
            get { return m_inviteEmailSentEmailAddress; }
            set { m_inviteEmailSentEmailAddress = value; }
        }
        public DateTime inviteEmailSentDate
        {
            get { return m_inviteEmailSentDate; }
            set { m_inviteEmailSentDate = value; }
        }
        public Boolean inviteAccepted
        {
            get { return m_inviteAccepted; }
            set { m_inviteAccepted = value; }
        }
        public DateTime inviteAcceptedDate
        {
            get { return m_inviteAcceptedDate; }
            set { m_inviteAcceptedDate = value; }
        }
        public Boolean inviteDeclined
        {
            get { return m_inviteDeclined; }
            set { m_inviteDeclined = value; }
        }
        public DateTime inviteDeclinedDate
        {
            get { return m_inviteDeclinedDate; }
            set { m_inviteDeclinedDate = value; }
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
        // Function: EventInvite (Constructor)
        //===============================================================
        public EventInvite(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: EventInvite (Constructor)
        //===============================================================
        public EventInvite(string loggedInUser, int eventInviteID)
        {
            m_loggedInUser = loggedInUser;
            m_eventInviteID = eventInviteID;

            ReadEventInviteDetails();
        }

        //===============================================================
        // Function: ReadEventInviteDetails
        //===============================================================
        public void ReadEventInviteDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventInviteDetails";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@EventInviteID";
                param.Value = m_eventInviteID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventID")))
                {
                    m_eventID = int.Parse(rdr["EventID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("GUID")))
                {
                    m_eventInviteGUID = (string)rdr["GUID"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EmailAddress")))
                {
                    m_emailAddress = (string)rdr["EmailAddress"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("UserID")))
                {
                    m_userID = int.Parse(rdr["UserID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("InviteAdditionalText")))
                {
                    m_inviteAdditionalText = (string)rdr["InviteAdditionalText"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("InviteEmailSent")))
                {
                    m_inviteEmailSent = (Boolean)rdr["InviteEmailSent"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("InviteEmailSentEmailAddress")))
                {
                    m_inviteEmailSentEmailAddress = (string)rdr["InviteEmailSentEmailAddress"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("InviteEmailSentDate")))
                {
                    m_inviteEmailSentDate = (DateTime)rdr["InviteEmailSentDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("InviteAccepted")))
                {
                    m_inviteAccepted = (Boolean)rdr["InviteAccepted"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("InviteAcceptedDate")))
                {
                    m_inviteAcceptedDate = (DateTime)rdr["InviteAcceptedDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("InviteDeclined")))
                {
                    m_inviteDeclined = (Boolean)rdr["InviteDeclined"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("InviteDeclinedDate")))
                {
                    m_inviteDeclinedDate = (DateTime)rdr["InviteDeclinedDate"];
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
                errorLog.WriteLog("EventInvite", "ReadEventInviteDetails", ex.Message, logMessageLevel.errorMessage);
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
            m_eventInviteGUID = System.Guid.NewGuid().ToString();

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spAddEventInvite", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = m_eventID;
                cmd.Parameters.Add("@GUID", SqlDbType.NVarChar, 50).Value = m_eventInviteGUID;
                if (m_userID > 0)
                {
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = m_userID;
                }
                else
                {
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 200).Value = m_emailAddress;
                cmd.Parameters.Add("@InviteAdditionalText", SqlDbType.NVarChar, -1).Value = m_inviteAdditionalText;
                cmd.Parameters.Add("@InviteEmailSent", SqlDbType.Bit).Value = m_inviteEmailSent;
                cmd.Parameters.Add("@InviteEmailSentEmailAddress", SqlDbType.NVarChar, 200).Value = m_inviteEmailSentEmailAddress;
                if (m_inviteAcceptedDate > DateTime.MinValue)
                {
                    cmd.Parameters.Add("@InviteEmailSentDate", SqlDbType.DateTime).Value = m_inviteEmailSentDate;
                }
                else
                {
                    cmd.Parameters.Add("@InviteEmailSentDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@CreatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                SqlParameter paramEventInviteID = cmd.CreateParameter();
                paramEventInviteID.ParameterName = "@EventInviteID";
                paramEventInviteID.SqlDbType = SqlDbType.Int;
                paramEventInviteID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramEventInviteID);

                cmd.ExecuteNonQuery();
                m_eventInviteID = (int)paramEventInviteID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventInvite", "Add", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spUpdateEventInvite", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EventInviteID", SqlDbType.Int).Value = m_eventInviteID;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 200).Value = m_emailAddress;
                if (m_userID > 0)
                {
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = m_userID;
                }
                else
                {
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@InviteAdditionalText", SqlDbType.NVarChar, -1).Value = m_inviteAdditionalText;
                cmd.Parameters.Add("@InviteEmailSent", SqlDbType.Bit).Value = m_inviteEmailSent;
                cmd.Parameters.Add("@InviteEmailSentEmailAddress", SqlDbType.NVarChar, 200).Value = m_inviteEmailSentEmailAddress;
                if (m_inviteEmailSentDate > DateTime.MinValue)
                {
                    cmd.Parameters.Add("@InviteEmailSentDate", SqlDbType.DateTime).Value = m_inviteEmailSentDate;
                }
                else
                {
                    cmd.Parameters.Add("@InviteEmailSentDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@InviteAccepted", SqlDbType.Bit).Value = m_inviteAccepted;
                if (m_inviteAcceptedDate > DateTime.MinValue)
                {
                    cmd.Parameters.Add("@InviteAcceptedDate", SqlDbType.DateTime).Value = m_inviteAcceptedDate;
                }
                else
                {
                    cmd.Parameters.Add("@InviteAcceptedDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@InviteDeclined", SqlDbType.Bit).Value = m_inviteDeclined;
                if (m_inviteDeclinedDate > DateTime.MinValue)
                {
                    cmd.Parameters.Add("@InviteDeclinedDate", SqlDbType.DateTime).Value = m_inviteDeclinedDate;
                }
                else
                {
                    cmd.Parameters.Add("@InviteDeclinedDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventInvite", "Update", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spDeleteEventInvite", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EventInviteID", SqlDbType.Int).Value = m_eventInviteID;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventInvite", "Delete", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: GetInviteCount
        //===============================================================
        public static int GetInviteCount(int eventID)
        {
            int inviteCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventInviteCountByEventID";
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                inviteCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventInvite", "GetInviteCount", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return inviteCount;
        }

        //===============================================================
        // Function: GetInviteCountForEmailAddress
        //===============================================================
        public static int GetInviteCountForEmailAddress(int eventID, string inviteEmailSentEmailAddress)
        {
            int inviteCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventInviteCountByEventIDAndEmailAddress";
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                cmd.Parameters.Add("@InviteEmailSentEmailAddress", SqlDbType.NVarChar, 200).Value = inviteEmailSentEmailAddress;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                inviteCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventInvite", "GetInviteCountForEmailAddress", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return inviteCount;
        }

        //===============================================================
        // Function: GetEventInviteIDFromGUID
        //===============================================================
        public static int GetEventInviteIDFromGUID(string eventInviteGUID)
        {
            int eventInviteID = -1;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventInviteIDFromGUID";
                cmd.Parameters.Add("@GUID", SqlDbType.NVarChar, 50).Value = eventInviteGUID;
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == true)
                {
                    rdr.Read();
                    eventInviteID = int.Parse(rdr["EventInviteID"].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventInvite", "GetEventInviteIDFromGUID", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return eventInviteID;
        }

        //===============================================================
        // Function: GetPendingInviteCountForUser
        //===============================================================
        public static int GetPendingInviteCountForUser(int userID)
        {
            int inviteCount = 0;

            SedogoUser sedogoUser = new SedogoUser("", userID);

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectPendingInviteCountForUser";
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 200).Value = sedogoUser.emailAddress;

                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                inviteCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventInvite", "GetPendingInviteCountForUser", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return inviteCount;
        }

        //===============================================================
        // Function: CheckUserEventInviteExists
        //===============================================================
        public static int CheckUserEventInviteExists(int eventID, int userID)
        {
            int inviteCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spCheckUserEventInviteExists";
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                inviteCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventInvite", "CheckUserEventInviteExists", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return inviteCount;
        }

        //===============================================================
        // Function: GetEventInviteIDFromUserIDEventID
        //===============================================================
        public static int GetEventInviteIDFromUserIDEventID(int eventID, int userID)
        {
            int eventInviteID = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetEventInviteIDFromUserIDEventID";
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == true)
                {
                    rdr.Read();
                    eventInviteID = int.Parse(rdr[0].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventInvite", "CheckUserEventInviteExists", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return eventInviteID;
        }
    }
}

﻿//===============================================================
// Filename: Events.cs
// Date: 07/09/09
// --------------------------------------------------------------
// Description:
//   Events class
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 07/09/09
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
    // Class: SedogoEvent
    //===============================================================
    public class SedogoEvent
    {
        private int         m_eventID = -1;
        private int         m_userID = -1;
        private string      m_eventName = "";
        private string      m_dateType = "D";
        private DateTime    m_startDate = DateTime.MinValue;
        private DateTime    m_rangeStartDate = DateTime.MinValue;
        private DateTime    m_rangeEndDate = DateTime.MinValue;
        private int         m_beforeBirthday = -1;
        private int         m_categoryID = 1;
        private Boolean     m_privateEvent = false;
        private Boolean     m_deleted = false;
        private Boolean     m_eventAchieved = false;
        private string      m_eventPicFilename = "";
        private string      m_eventPicThumbnail = "";
        private string      m_eventPicPreview = "";
        private DateTime    m_createdDate = DateTime.MinValue;
        private string      m_createdByFullName = "";
        private DateTime    m_lastUpdatedDate = DateTime.MinValue;
        private string      m_lastUpdatedByFullName = "";

        private string      m_loggedInUser = "";

        public int eventID
        {
            get { return m_eventID; }
        }
        public int userID
        {
            get { return m_userID; }
            set { m_userID = value; }
        }
        public string eventName
        {
            get { return m_eventName; }
            set { m_eventName = value; }
        }
        public string dateType
        {
            get { return m_dateType; }
            set { m_dateType = value; }
        }
        public DateTime startDate
        {
            get { return m_startDate; }
            set { m_startDate = value; }
        }
        public DateTime rangeStartDate
        {
            get { return m_rangeStartDate; }
            set { m_rangeStartDate = value; }
        }
        public DateTime rangeEndDate
        {
            get { return m_rangeEndDate; }
            set { m_rangeEndDate = value; }
        }
        public int beforeBirthday
        {
            get { return m_beforeBirthday; }
            set { m_beforeBirthday = value; }
        }
        public int categoryID
        {
            get { return m_categoryID; }
            set { m_categoryID = value; }
        }
        public Boolean privateEvent
        {
            get { return m_privateEvent; }
            set { m_privateEvent = value; }
        }
        public Boolean eventAchieved
        {
            get { return m_eventAchieved; }
            set { m_eventAchieved = value; }
        }
        public Boolean deleted
        {
            get { return m_deleted; }
        }
        public string eventPicFilename
        {
            get { return m_eventPicFilename; }
            set { m_eventPicFilename = value; }
        }
        public string eventPicThumbnail
        {
            get { return m_eventPicThumbnail; }
            set { m_eventPicThumbnail = value; }
        }
        public string eventPicPreview
        {
            get { return m_eventPicPreview; }
            set { m_eventPicPreview = value; }
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
        // Function: SedogoEvent (Constructor)
        //===============================================================
        public SedogoEvent(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: SedogoEvent (Constructor)
        //===============================================================
        public SedogoEvent(string loggedInUser, int eventID)
        {
            m_loggedInUser = loggedInUser;
            m_eventID = eventID;

            ReadEventDetails();
        }

        //===============================================================
        // Function: ReadEventDetails
        //===============================================================
        public void ReadEventDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventDetails";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@EventID";
                param.Value = m_eventID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                if (!rdr.IsDBNull(rdr.GetOrdinal("UserID")))
                {
                    m_userID = int.Parse(rdr["UserID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventName")))
                {
                    m_eventName = (string)rdr["EventName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("DateType")))
                {
                    m_dateType = (string)rdr["DateType"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("StartDate")))
                {
                    m_startDate = (DateTime)rdr["StartDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("RangeStartDate")))
                {
                    m_rangeStartDate = (DateTime)rdr["RangeStartDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("RangeEndDate")))
                {
                    m_rangeEndDate = (DateTime)rdr["RangeEndDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("BeforeBirthday")))
                {
                    m_beforeBirthday = int.Parse(rdr["BeforeBirthday"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("CategoryID")))
                {
                    m_categoryID = int.Parse(rdr["CategoryID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("PrivateEvent")))
                {
                    m_privateEvent = (Boolean)rdr["PrivateEvent"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventAchieved")))
                {
                    m_eventAchieved = (Boolean)rdr["EventAchieved"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Deleted")))
                {
                    m_deleted = (Boolean)rdr["Deleted"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventPicFilename")))
                {
                    m_eventPicFilename = (string)rdr["EventPicFilename"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventPicThumbnail")))
                {
                    m_eventPicThumbnail = (string)rdr["EventPicThumbnail"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventPicPreview")))
                {
                    m_eventPicPreview = (string)rdr["EventPicPreview"];
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
                errorLog.WriteLog("SedogoEvent", "ReadEventDetails", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spAddEvent", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = m_userID;
                cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 200).Value = m_eventName;
                cmd.Parameters.Add("@DateType", SqlDbType.NChar, 1).Value = m_dateType;
                if( m_startDate > DateTime.MinValue )
                {
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = m_startDate;
                }
                else
                {
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                if (m_rangeStartDate > DateTime.MinValue)
                {
                    cmd.Parameters.Add("@RangeStartDate", SqlDbType.DateTime).Value = m_rangeStartDate;
                }
                else
                {
                    cmd.Parameters.Add("@RangeStartDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                if (m_rangeEndDate > DateTime.MinValue)
                {
                    cmd.Parameters.Add("@RangeEndDate", SqlDbType.DateTime).Value = m_rangeEndDate;
                }
                else
                {
                    cmd.Parameters.Add("@RangeEndDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@BeforeBirthday", SqlDbType.Int).Value = m_beforeBirthday;
                cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = m_categoryID;
                cmd.Parameters.Add("@PrivateEvent", SqlDbType.Bit).Value = m_privateEvent;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@CreatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                SqlParameter paramEventID = cmd.CreateParameter();
                paramEventID.ParameterName = "@EventID";
                paramEventID.SqlDbType = SqlDbType.Int;
                paramEventID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramEventID);

                cmd.ExecuteNonQuery();
                m_eventID = (int)paramEventID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "Add", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spUpdateEvent", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = m_eventID;
                cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 200).Value = m_eventName;
                cmd.Parameters.Add("@DateType", SqlDbType.NChar, 1).Value = m_dateType;
                if (m_startDate > DateTime.MinValue)
                {
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = m_startDate;
                }
                else
                {
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                if (m_rangeStartDate > DateTime.MinValue)
                {
                    cmd.Parameters.Add("@RangeStartDate", SqlDbType.DateTime).Value = m_rangeStartDate;
                }
                else
                {
                    cmd.Parameters.Add("@RangeStartDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                if (m_rangeEndDate > DateTime.MinValue)
                {
                    cmd.Parameters.Add("@RangeEndDate", SqlDbType.DateTime).Value = m_rangeEndDate;
                }
                else
                {
                    cmd.Parameters.Add("@RangeEndDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@BeforeBirthday", SqlDbType.Int).Value = m_beforeBirthday;
                cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = m_categoryID;
                cmd.Parameters.Add("@PrivateEvent", SqlDbType.Bit).Value = m_privateEvent;
                cmd.Parameters.Add("@EventAchieved", SqlDbType.Bit).Value = m_eventAchieved;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "Update", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: UpdateEventPic
        //===============================================================
        public void UpdateEventPic()
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateEventPics", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = m_eventID;
                cmd.Parameters.Add("@EventPicFilename", SqlDbType.NVarChar, 200).Value = m_eventPicFilename;
                cmd.Parameters.Add("@EventPicThumbnail", SqlDbType.NVarChar, 200).Value = m_eventPicThumbnail;
                cmd.Parameters.Add("@EventPicPreview", SqlDbType.NVarChar, 200).Value = m_eventPicPreview;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "UpdateEventPic", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spDeleteEvent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = m_eventID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "Delete", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    //===============================================================
    // Class: SedogoEventComment
    //===============================================================
    public class SedogoEventComment
    {
        private int         m_eventCommentID = -1;
        private int         m_eventID = -1;
        private int         m_postedByUserID = -1;
        private string      m_commentText = "";
        private Boolean     m_deleted = false;
        private DateTime    m_createdDate = DateTime.MinValue;
        private string      m_createdByFullName = "";
        private DateTime    m_lastUpdatedDate = DateTime.MinValue;
        private string      m_lastUpdatedByFullName = "";

        private string      m_loggedInUser = "";

        public int eventCommentID
        {
            get { return m_eventCommentID; }
        }
        public int eventID
        {
            get { return m_eventID; }
            set { m_eventID = value; }
        }
        public int postedByUserID
        {
            get { return m_postedByUserID; }
            set { m_postedByUserID = value; }
        }
        public string commentText
        {
            get { return m_commentText; }
            set { m_commentText = value; }
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
        // Function: SedogoEventComment (Constructor)
        //===============================================================
        public SedogoEventComment(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: SedogoEventComment (Constructor)
        //===============================================================
        public SedogoEventComment(string loggedInUser, int eventCommentID)
        {
            m_loggedInUser = loggedInUser;
            m_eventCommentID = eventCommentID;

            ReadEventCommentDetails();
        }

        //===============================================================
        // Function: ReadEventCommentDetails
        //===============================================================
        public void ReadEventCommentDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventCommentDetails";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@EventCommentID";
                param.Value = m_eventCommentID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventID")))
                {
                    m_eventID = int.Parse(rdr["EventID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("PostedByUserID")))
                {
                    m_postedByUserID = int.Parse(rdr["PostedByUserID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("CommentText")))
                {
                    m_commentText = (string)rdr["CommentText"];
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
                errorLog.WriteLog("SedogoEventComment", "ReadEventCommentDetails", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spAddEventComment", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = m_eventID;
                cmd.Parameters.Add("@PostedByUserID", SqlDbType.Int).Value = m_postedByUserID;
                cmd.Parameters.Add("@CommentText", SqlDbType.NVarChar, -1).Value = m_commentText;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@CreatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                SqlParameter paramEventCommentID = cmd.CreateParameter();
                paramEventCommentID.ParameterName = "@EventCommentID";
                paramEventCommentID.SqlDbType = SqlDbType.Int;
                paramEventCommentID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramEventCommentID);

                cmd.ExecuteNonQuery();
                m_eventCommentID = (int)paramEventCommentID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEventComment", "Add", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spUpdateEventComment", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EventCommentID", SqlDbType.Int).Value = m_eventCommentID;
                cmd.Parameters.Add("@CommentText", SqlDbType.NVarChar, -1).Value = m_commentText;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEventComment", "Update", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spDeleteEventComment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EventCommentID", SqlDbType.Int).Value = m_eventCommentID;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEventComment", "Delete", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

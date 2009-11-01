//===============================================================
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
        private int         m_timezoneID = 1;
        private Boolean     m_privateEvent = false;
        private int         m_createdFromEventID = 1;
        private string      m_eventDescription = "";
        private string      m_eventVenue = "";
        private Boolean     m_mustDo = false;
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
        public int timezoneID
        {
            get { return m_timezoneID; }
            set { m_timezoneID = value; }
        }
        public Boolean privateEvent
        {
            get { return m_privateEvent; }
            set { m_privateEvent = value; }
        }
        public int createdFromEventID
        {
            get { return m_createdFromEventID; }
            set { m_createdFromEventID = value; }
        }
        public string eventDescription
        {
            get { return m_eventDescription; }
            set { m_eventDescription = value; }
        }
        public string eventVenue
        {
            get { return m_eventVenue; }
            set { m_eventVenue = value; }
        }
        public Boolean mustDo
        {
            get { return m_mustDo; }
            set { m_mustDo = value; }
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
                if (!rdr.IsDBNull(rdr.GetOrdinal("TimezoneID")))
                {
                    m_timezoneID = int.Parse(rdr["TimezoneID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("PrivateEvent")))
                {
                    m_privateEvent = (Boolean)rdr["PrivateEvent"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedFromEventID")))
                {
                    m_createdFromEventID = int.Parse(rdr["CreatedFromEventID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventAchieved")))
                {
                    m_eventAchieved = (Boolean)rdr["EventAchieved"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Deleted")))
                {
                    m_deleted = (Boolean)rdr["Deleted"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventDescription")))
                {
                    m_eventDescription = (string)rdr["EventDescription"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventVenue")))
                {
                    m_eventVenue = (string)rdr["EventVenue"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("MustDo")))
                {
                    m_mustDo = (Boolean)rdr["MustDo"];
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
                cmd.Parameters.Add("@TimezoneID", SqlDbType.Int).Value = m_timezoneID;
                cmd.Parameters.Add("@PrivateEvent", SqlDbType.Bit).Value = m_privateEvent;
                cmd.Parameters.Add("@CreatedFromEventID", SqlDbType.Int).Value = m_createdFromEventID;
                cmd.Parameters.Add("@EventDescription", SqlDbType.NVarChar, -1).Value = m_eventDescription;
                cmd.Parameters.Add("@EventVenue", SqlDbType.NVarChar, -1).Value = m_eventVenue;
                cmd.Parameters.Add("@MustDo", SqlDbType.Bit).Value = m_mustDo;
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
                cmd.Parameters.Add("@TimezoneID", SqlDbType.Int).Value = m_timezoneID;
                cmd.Parameters.Add("@PrivateEvent", SqlDbType.Bit).Value = m_privateEvent;
                cmd.Parameters.Add("@CreatedFromEventID", SqlDbType.Int).Value = m_createdFromEventID;
                cmd.Parameters.Add("@EventAchieved", SqlDbType.Bit).Value = m_eventAchieved;
                cmd.Parameters.Add("@EventDescription", SqlDbType.NVarChar, -1).Value = m_eventDescription;
                cmd.Parameters.Add("@EventVenue", SqlDbType.NVarChar, -1).Value = m_eventVenue;
                cmd.Parameters.Add("@MustDo", SqlDbType.Bit).Value = m_mustDo;
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

        //===============================================================
        // Function: GetTrackingUserCount
        //===============================================================
        public static int GetTrackingUserCount(int eventID)
        {
            int trackingUserCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectTrackingUserCountByEventID";
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                trackingUserCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("TrackedEvent", "GetTrackingUserCount", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return trackingUserCount;
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

    //===============================================================
    // Class: TrackedEvent
    //===============================================================
    public class TrackedEvent
    {
        private int         m_trackedEventID = -1;
        private int         m_eventID = -1;
        private int         m_userID = -1;
        private DateTime    m_createdDate = DateTime.MinValue;
        private DateTime    m_lastUpdatedDate = DateTime.MinValue;

        private string      m_loggedInUser = "";

        public int trackedEventID
        {
            get { return m_trackedEventID; }
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
        public DateTime createdDate
        {
            get { return m_createdDate; }
        }
        public DateTime lastUpdatedDate
        {
            get { return m_lastUpdatedDate; }
        }

        //===============================================================
        // Function: TrackedEvent (Constructor)
        //===============================================================
        public TrackedEvent(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: TrackedEvent (Constructor)
        //===============================================================
        public TrackedEvent(string loggedInUser, int trackedEventID)
        {
            m_loggedInUser = loggedInUser;
            m_trackedEventID = trackedEventID;

            ReadTrackedEventDetails();
        }

        //===============================================================
        // Function: ReadTrackedEventDetails
        //===============================================================
        public void ReadTrackedEventDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectTrackedEventDetails";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@TrackedEventID";
                param.Value = m_trackedEventID;
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
                if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedDate")))
                {
                    m_createdDate = (DateTime)rdr["CreatedDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LastUpdatedDate")))
                {
                    m_lastUpdatedDate = (DateTime)rdr["LastUpdatedDate"];
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("TrackedEvent", "ReadTrackedEventDetails", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spAddTrackedEvent", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = m_eventID;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = m_userID;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;

                SqlParameter paramTrackedEventID = cmd.CreateParameter();
                paramTrackedEventID.ParameterName = "@TrackedEventID";
                paramTrackedEventID.SqlDbType = SqlDbType.Int;
                paramTrackedEventID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramTrackedEventID);

                cmd.ExecuteNonQuery();
                m_trackedEventID = (int)paramTrackedEventID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("TrackedEvent", "Add", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spDeleteTrackedEvent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TrackedEventID", SqlDbType.Int).Value = m_trackedEventID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("TrackedEvent", "Delete", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: GetTrackedEventID
        //===============================================================
        public static int GetTrackedEventID(int eventID, int userID)
        {
            int trackedEventID = -1;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectTrackedEventID";
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == true)
                {
                    rdr.Read();
                    trackedEventID = int.Parse(rdr["TrackedEventID"].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("TrackedEvent", "GetTrackedEventID", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return trackedEventID;
        }

        //===============================================================
        // Function: GetTrackedEventCount
        //===============================================================
        public static int GetTrackedEventCount(int userID)
        {
            int trackedEventCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectTrackedEventCountByUserID";
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                trackedEventCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("TrackedEvent", "GetTrackedEventCount", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return trackedEventCount;
        }
    }
}

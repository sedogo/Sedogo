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
using System.Net.Mail;

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
        private DateTime    m_eventAchievedDate = DateTime.MinValue;
        private string      m_eventPicFilename = "";
        private string      m_eventPicThumbnail = "";
        private string      m_eventPicPreview = "";
        private Boolean     m_showOnDefaultPage = false;
        private DateTime    m_createdDate = DateTime.MinValue;
        private string      m_createdByFullName = "";
        private DateTime    m_lastUpdatedDate = DateTime.MinValue;
        private string      m_lastUpdatedByFullName = "";
        private string      m_eventGUID = Guid.Empty.ToString();

        private string      m_loggedInUser = "";

        public string eventGUID
        {
            get { return m_eventGUID; }
        }

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
        public DateTime eventAchievedDate
        {
            get { return m_eventAchievedDate; }
            set { m_eventAchievedDate = value; }
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
        public Boolean showOnDefaultPage
        {
            get { return m_showOnDefaultPage; }
            set { m_showOnDefaultPage = value; }
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
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventAchievedDate")))
                {
                    m_eventAchievedDate = (DateTime)rdr["EventAchievedDate"];
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
                if (!rdr.IsDBNull(rdr.GetOrdinal("ShowOnDefaultPage")))
                {
                    m_showOnDefaultPage = (Boolean)rdr["ShowOnDefaultPage"];
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
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventGUID")))
                {
                    m_eventGUID = rdr["EventGUID"].ToString();
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
                if (m_eventAchievedDate > DateTime.MinValue)
                {
                    cmd.Parameters.Add("@EventAchievedDate", SqlDbType.DateTime).Value = m_eventAchievedDate;
                }
                else
                {
                    cmd.Parameters.Add("@EventAchievedDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@EventDescription", SqlDbType.NVarChar, -1).Value = m_eventDescription;
                cmd.Parameters.Add("@EventVenue", SqlDbType.NVarChar, -1).Value = m_eventVenue;
                cmd.Parameters.Add("@MustDo", SqlDbType.Bit).Value = m_mustDo;
                cmd.Parameters.Add("@ShowOnDefaultPage", SqlDbType.Bit).Value = m_showOnDefaultPage;
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
        // Function: SendEventUpdateEmail
        //===============================================================
        public void SendEventUpdateEmail(int updatingUserID)
        {
            StringBuilder emailBodyCopy = new StringBuilder();
            GlobalData gd = new GlobalData("");

            string dateString = "";
            DateTime startDate = m_startDate;
            SedogoUser eventOwner = new SedogoUser(m_loggedInUser, m_userID);
            SedogoUser updatingUser = new SedogoUser(m_loggedInUser, updatingUserID);
            MiscUtils.GetDateStringStartDate(eventOwner, m_dateType, m_rangeStartDate,
                m_rangeEndDate, m_beforeBirthday, ref dateString, ref startDate);

            string inviteURL = gd.GetStringValue("SiteBaseURL");
            inviteURL = inviteURL + "?EID=" + m_eventID.ToString();

            emailBodyCopy.AppendLine("<html>");
            emailBodyCopy.AppendLine("<head><title></title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">");
            emailBodyCopy.AppendLine("<style type=\"text/css\">");
            emailBodyCopy.AppendLine("	body, td, p { font-size: 15px; color: #9B9885; font-family: Arial, Helvetica, Sans-Serif }");
            emailBodyCopy.AppendLine("	p { margin: 0 }");
            emailBodyCopy.AppendLine("	h1 { color: #00ccff; font-size: 18px; font-weight: bold; }");
            emailBodyCopy.AppendLine("	a, .blue { color: #00ccff; text-decoration: none; }");
            emailBodyCopy.AppendLine("	img { border: 0; }");
            emailBodyCopy.AppendLine("</style></head>");
            emailBodyCopy.AppendLine("<body bgcolor=\"#f0f1ec\">");
            emailBodyCopy.AppendLine("  <table width=\"692\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            //emailBodyCopy.AppendLine("	<tr><td colspan=\"3\"><img src=\"http://www.sedogo.com/email-template/images/email-template_01.png\" width=\"692\" height=\"32\" alt=\"\"></td></tr>");
            emailBodyCopy.AppendLine("	<tr><td style=\"background: #fff\" width=\"30\"></td>");
            emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"632\">");
            emailBodyCopy.AppendLine("			<h1>" + updatingUser.firstName + " " + updatingUser.lastName + " has updated the following goal:</h1>");
            emailBodyCopy.AppendLine("			<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td width=\"60\">What:</td>");
            emailBodyCopy.AppendLine("					<td width=\"10\" rowspan=\"4\">&nbsp;</td>");
            emailBodyCopy.AppendLine("					<td width=\"530\">" + m_eventName + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td valign=\"top\">Owner:</td>");
            emailBodyCopy.AppendLine("					<td>" + eventOwner.firstName + " " + eventOwner.lastName + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td valign=\"top\">Where:</td>");
            emailBodyCopy.AppendLine("					<td>" + m_eventVenue + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td valign=\"top\">When:</td>");
            emailBodyCopy.AppendLine("					<td>" + dateString + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("			</table>");
            emailBodyCopy.AppendLine("			<p>To view this event, <a href=\"" + inviteURL + "\"><u>click here</u></a>.</p>");
            emailBodyCopy.AppendLine("			<br /><br />");
            emailBodyCopy.AppendLine("			<p>Regards</p><a href=\"http://www.sedogo.com\" class=\"blue\"><strong>The Sedogo Team.</strong></a><br />");
            emailBodyCopy.AppendLine("			<br /><br /><br /><a href=\"http://www.sedogo.com\">");
            //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/logo.gif\" />");
            emailBodyCopy.AppendLine("			</a></td>");
            emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"30\"></td></tr><tr><td colspan=\"3\">");
            //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/email-template_05.png\" width=\"692\" height=\"32\" alt=\"\">");
            emailBodyCopy.AppendLine("		</td></tr><tr><td colspan=\"3\"><small>This message was intended for <<RECIPIENT>>. To stop receiving these emails, go to your profile and uncheck the 'Enable email notifications' option.<br/>Sedogo offices are located at Sedogo Ltd, The Studio, 17 Blossom St, London E1 6PL.</small></td></tr>");
            emailBodyCopy.AppendLine("		</td></tr></table></body></html>");

            string emailSubject = m_eventName + " on " + dateString + " has been updated";

            string SMTPServer = gd.GetStringValue("SMTPServer");
            string mailFromAddress = gd.GetStringValue("MailFromAddress");
            string mailFromUsername = gd.GetStringValue("MailFromUsername");
            string mailFromPassword = gd.GetStringValue("MailFromPassword");

            // Sent the message to the event owner as well as the trackers
            if (updatingUserID != m_userID)
            {
                if (eventOwner.enableSendEmails == true)
                {
                    try
                    {
                        MailMessage message = new MailMessage(mailFromAddress, eventOwner.emailAddress);
                        message.ReplyTo = new MailAddress("noreply@sedogo.com");

                        message.Subject = emailSubject;
                        message.Body = emailBodyCopy.ToString().Replace("<<RECIPIENT>>", eventOwner.emailAddress);
                        message.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = SMTPServer;
                        if (mailFromPassword != "")
                        {
                            // If the password is blank, assume mail relay is permitted
                            smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
                        }
                        smtp.Send(message);

                        SentEmailHistory emailHistory = new SentEmailHistory("");
                        emailHistory.subject = emailSubject;
                        emailHistory.body = emailBodyCopy.ToString().Replace("<<RECIPIENT>>", eventOwner.emailAddress);
                        emailHistory.sentFrom = mailFromAddress;
                        emailHistory.sentTo = eventOwner.emailAddress;
                        emailHistory.Add();
                    }
                    catch (Exception ex)
                    {
                        SentEmailHistory emailHistory = new SentEmailHistory("");
                        emailHistory.subject = emailSubject;
                        emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString().Replace("<<RECIPIENT>>", eventOwner.emailAddress);
                        emailHistory.sentFrom = mailFromAddress;
                        emailHistory.sentTo = eventOwner.emailAddress;
                        emailHistory.Add();
                    }
                }
            }

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectTrackingUsersByEventID";
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = m_eventID;
                DbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    //int trackedEventID = int.Parse(rdr["TrackedEventID"].ToString());
                    int userID = int.Parse(rdr["UserID"].ToString());
                    string firstName = (string)rdr["FirstName"];
                    string lastName = (string)rdr["LastName"];
                    //string gender = (string)rdr["Gender"];
                    //string homeTown = (string)rdr["HomeTown"];
                    string emailAddress = (string)rdr["EmailAddress"];

                    if (updatingUserID != userID)
                    {
                        SedogoUser user = new SedogoUser(m_loggedInUser, userID);
                        if (user.enableSendEmails == true)
                        {
                            try
                            {
                                MailMessage message = new MailMessage(mailFromAddress, emailAddress);
                                message.ReplyTo = new MailAddress("noreply@sedogo.com");

                                message.Subject = emailSubject;
                                message.Body = emailBodyCopy.ToString().Replace("<<RECIPIENT>>", emailAddress);
                                message.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = SMTPServer;
                                if (mailFromPassword != "")
                                {
                                    // If the password is blank, assume mail relay is permitted
                                    smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
                                }
                                smtp.Send(message);

                                SentEmailHistory emailHistory = new SentEmailHistory("");
                                emailHistory.subject = emailSubject;
                                emailHistory.body = emailBodyCopy.ToString().Replace("<<RECIPIENT>>", emailAddress);
                                emailHistory.sentFrom = mailFromAddress;
                                emailHistory.sentTo = emailAddress;
                                emailHistory.Add();
                            }
                            catch (Exception ex)
                            {
                                SentEmailHistory emailHistory = new SentEmailHistory("");
                                emailHistory.subject = emailSubject;
                                emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString().Replace("<<RECIPIENT>>", emailAddress);
                                emailHistory.sentFrom = mailFromAddress;
                                emailHistory.sentTo = emailAddress;
                                emailHistory.Add();
                            }
                        }
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "SendEventUpdateEmail", ex.Message, logMessageLevel.errorMessage);
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
                errorLog.WriteLog("SedogoEvent", "GetTrackingUserCount", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return trackingUserCount;
        }

        //===============================================================
        // Function: GetEventTrackerID
        //===============================================================
        public int GetTrackedEventID(int userID)
        {
            int trackedEventID = -1;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectTrackedEventIDFromEventIDUserID";
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                DbDataReader rdr = cmd.ExecuteReader();
                if( rdr.HasRows == true )
                {
                    rdr.Read();
                    trackedEventID = int.Parse(rdr[0].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "GetTrackedEventID", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return trackedEventID;
        }

        //===============================================================
        // Function: GetMemberUserCount
        //===============================================================
        public static int GetMemberUserCount(int eventID)
        {
            int trackingUserCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectMemberUserCountByEventID";
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                trackingUserCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "GetTrackingUserCount", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return trackingUserCount;
        }

        //===============================================================
        // Function: GetCommentCount
        //===============================================================
        public static int GetCommentCount(int eventID)
        {
            int commentCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventCommentCountForEvent";
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                commentCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "GetCommentCount", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return commentCount;
        }

        //===============================================================
        // Function: GetPendingMemberUserCount
        //===============================================================
        public static int GetPendingMemberUserCount(int eventID)
        {
            int pendingUserCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectPendingMemberUserCountByEventID";
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                pendingUserCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "GetPendingMemberUserCount", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return pendingUserCount;
        }
    
        //===============================================================
        // Function: GetPendingMemberUserCount
        //===============================================================
        public static int GetPendingMemberUserCountByUserID(int userID)
        {
            int pendingUserCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectPendingMemberUserCountByUserID";
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                pendingUserCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "GetPendingMemberUserCountByUserID", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return pendingUserCount;
        }

        //===============================================================
        // Function: GetEventCountNotAchieved
        //===============================================================
        public static int GetEventCountNotAchieved(int userID)
        {
            int eventCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventCountNotAchievedByUserID";
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                eventCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "GetEventCountNotAchieved", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return eventCount;
        }

        //===============================================================
        // Function: GetEventCountAchieved
        //===============================================================
        public static int GetEventCountAchieved(int userID)
        {
            int eventCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventCountAchievedByUserID";
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                eventCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "GetEventCountAchieved", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return eventCount;
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
        private string      m_eventImageFilename = "";
        private string      m_eventImagePreview = "";
        private string      m_eventVideoFilename = "";
        private string      m_eventVideoLink = "";
        private string      m_eventLink = "";
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
        public string eventImageFilename
        {
            get { return m_eventImageFilename; }
            set { m_eventImageFilename = value; }
        }
        public string eventImagePreview
        {
            get { return m_eventImagePreview; }
            set { m_eventImagePreview = value; }
        }
        public string eventVideoFilename
        {
            get { return m_eventVideoFilename; }
            set { m_eventVideoFilename = value; }
        }
        public string eventVideoLink
        {
            get { return m_eventVideoLink; }
            set { m_eventVideoLink = value; }
        }
        public string eventLink
        {
            get { return m_eventLink; }
            set { m_eventLink = value; }
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
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventImageFilename")))
                {
                    m_eventImageFilename = (string)rdr["EventImageFilename"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventImagePreview")))
                {
                    m_eventImagePreview = (string)rdr["EventImagePreview"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventVideoFilename")))
                {
                    m_eventVideoFilename = (string)rdr["EventVideoFilename"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventVideoLink")))
                {
                    m_eventVideoLink = (string)rdr["EventVideoLink"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventLink")))
                {
                    m_eventLink = (string)rdr["EventLink"];
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
                cmd.Parameters.Add("@EventImageFilename", SqlDbType.NVarChar, 200).Value = m_eventImageFilename;
                cmd.Parameters.Add("@EventImagePreview", SqlDbType.NVarChar, 200).Value = m_eventImagePreview;
                cmd.Parameters.Add("@EventVideoLink", SqlDbType.NVarChar, 1000).Value = m_eventVideoLink;
                cmd.Parameters.Add("@EventLink", SqlDbType.NVarChar, 200).Value = m_eventLink;
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
        private Boolean     m_showOnTimeline = false;
        private Boolean     m_joinPending = false;
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
        public Boolean showOnTimeline
        {
            get { return m_showOnTimeline; }
            set { m_showOnTimeline = value; }
        }
        public Boolean joinPending
        {
            get { return m_joinPending; }
            set { m_joinPending = value; }
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
                if (!rdr.IsDBNull(rdr.GetOrdinal("ShowOnTimeline")))
                {
                    m_showOnTimeline = (Boolean)rdr["ShowOnTimeline"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("JoinPending")))
                {
                    m_joinPending = (Boolean)rdr["JoinPending"];
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
                cmd.Parameters.Add("@ShowOnTimeline", SqlDbType.Bit).Value = m_showOnTimeline;
                cmd.Parameters.Add("@JoinPending", SqlDbType.Bit).Value = m_joinPending;
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
        // Function: Update
        //===============================================================
        public void Update()
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateTrackedEvent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TrackedEventID", SqlDbType.Int).Value = m_trackedEventID;
                cmd.Parameters.Add("@ShowOnTimeline", SqlDbType.Bit).Value = m_showOnTimeline;
                cmd.Parameters.Add("@JoinPending", SqlDbType.Bit).Value = m_joinPending;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("TrackedEvent", "Update", ex.Message, logMessageLevel.errorMessage);
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

        //===============================================================
        // Function: GetJoinedEventCount
        //===============================================================
        public static int GetJoinedEventCount(int userID)
        {
            int trackedEventCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectJoinedEventCountByUserID";
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                trackedEventCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("TrackedEvent", "GetJoinedEventCount", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return trackedEventCount;
        }

        //===============================================================
        // Function: SendJoinRequestEmail
        //===============================================================
        public void SendJoinRequestEmail()
        {
            StringBuilder emailBodyCopy = new StringBuilder();
            GlobalData gd = new GlobalData("");

            SedogoEvent sedogoEvent = new SedogoEvent(m_loggedInUser, m_eventID);
            SedogoUser trackingUser = new SedogoUser(m_loggedInUser, m_userID);
            SedogoUser eventUser = new SedogoUser(m_loggedInUser, sedogoEvent.userID);

            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventUser, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);

            string inviteURL = gd.GetStringValue("SiteBaseURL");
            //inviteURL = inviteURL + "?EID=" + m_eventID.ToString();
            inviteURL = inviteURL + "?Redir=Requests";

            emailBodyCopy.AppendLine("<html>");
            emailBodyCopy.AppendLine("<head><title></title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">");
            emailBodyCopy.AppendLine("<style type=\"text/css\">");
            emailBodyCopy.AppendLine("	body, td, p { font-size: 15px; color: #9B9885; font-family: Arial, Helvetica, Sans-Serif }");
            emailBodyCopy.AppendLine("	p { margin: 0 }");
            emailBodyCopy.AppendLine("	h1 { color: #00ccff; font-size: 18px; font-weight: bold; }");
            emailBodyCopy.AppendLine("	a, .blue { color: #00ccff; text-decoration: none; }");
            emailBodyCopy.AppendLine("	img { border: 0; }");
            emailBodyCopy.AppendLine("</style></head>");
            emailBodyCopy.AppendLine("<body bgcolor=\"#f0f1ec\">");
            emailBodyCopy.AppendLine("  <table width=\"692\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            //emailBodyCopy.AppendLine("	<tr><td colspan=\"3\"><img src=\"http://www.sedogo.com/email-template/images/email-template_01.png\" width=\"692\" height=\"32\" alt=\"\"></td></tr>");
            emailBodyCopy.AppendLine("	<tr><td style=\"background: #fff\" width=\"30\"></td>");
            emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"632\">");
            emailBodyCopy.AppendLine("			<h1>The following user has requested to join one of your goals:</h1>");
            emailBodyCopy.AppendLine("			<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td width=\"60\">Who:</td>");
            emailBodyCopy.AppendLine("					<td width=\"10\" rowspan=\"4\">&nbsp;</td>");
            emailBodyCopy.AppendLine("					<td width=\"530\">" + trackingUser.firstName + " " + trackingUser.lastName + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td valign=\"top\">What:</td>");
            emailBodyCopy.AppendLine("					<td>" + sedogoEvent.eventName + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td valign=\"top\">Where:</td>");
            emailBodyCopy.AppendLine("					<td>" + sedogoEvent.eventVenue + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td valign=\"top\">When:</td>");
            emailBodyCopy.AppendLine("					<td>" + dateString + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("			</table>");
            emailBodyCopy.AppendLine("			<p>To accept or decline this request, <a href=\"" + inviteURL + "\"><u>click here</u></a>.</p>");
            emailBodyCopy.AppendLine("			<br /><br />");
            emailBodyCopy.AppendLine("			<p>Regards</p><a href=\"http://www.sedogo.com\" class=\"blue\"><strong>The Sedogo Team.</strong></a><br />");
            emailBodyCopy.AppendLine("			<br /><br /><br /><a href=\"http://www.sedogo.com\">");
            //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/logo.gif\" />");
            emailBodyCopy.AppendLine("			</a></td>");
            emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"30\"></td></tr><tr><td colspan=\"3\">");
            //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/email-template_05.png\" width=\"692\" height=\"32\" alt=\"\">");
            emailBodyCopy.AppendLine("		</td></tr><tr><td colspan=\"3\"><small>This message was intended for " + eventUser.emailAddress + ". To stop receiving these emails, go to your profile and uncheck the 'Enable email notifications' option.<br/>Sedogo offices are located at Sedogo Ltd, The Studio, 17 Blossom St, London E1 6PL.</small></td></tr>");
            emailBodyCopy.AppendLine("		</td></tr></table></body></html>");

            string emailSubject = "Someone has requested to join your Sedogo goal: " + sedogoEvent.eventName;

            string SMTPServer = gd.GetStringValue("SMTPServer");
            string mailFromAddress = gd.GetStringValue("MailFromAddress");
            string mailFromUsername = gd.GetStringValue("MailFromUsername");
            string mailFromPassword = gd.GetStringValue("MailFromPassword");

            if (eventUser.enableSendEmails == true)
            {
                try
                {
                    MailMessage message = new MailMessage(mailFromAddress, eventUser.emailAddress);
                    message.ReplyTo = new MailAddress("noreply@sedogo.com");

                    message.Subject = emailSubject;
                    message.Body = emailBodyCopy.ToString();
                    message.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = SMTPServer;
                    if (mailFromPassword != "")
                    {
                        // If the password is blank, assume mail relay is permitted
                        smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
                    }
                    smtp.Send(message);

                    SentEmailHistory emailHistory = new SentEmailHistory("");
                    emailHistory.subject = emailSubject;
                    emailHistory.body = emailBodyCopy.ToString();
                    emailHistory.sentFrom = mailFromAddress;
                    emailHistory.sentTo = eventUser.emailAddress;
                    emailHistory.Add();
                }
                catch (Exception ex)
                {
                    SentEmailHistory emailHistory = new SentEmailHistory("");
                    emailHistory.subject = emailSubject;
                    emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString();
                    emailHistory.sentFrom = mailFromAddress;
                    emailHistory.sentTo = eventUser.emailAddress;
                    emailHistory.Add();
                }
            }
        }

        //===============================================================
        // Function: SendJoinAcceptedEmail
        //===============================================================
        public void SendJoinAcceptedEmail()
        {
            StringBuilder emailBodyCopy = new StringBuilder();
            GlobalData gd = new GlobalData("");

            SedogoEvent sedogoEvent = new SedogoEvent(m_loggedInUser, m_eventID);
            SedogoUser trackingUser = new SedogoUser(m_loggedInUser, m_userID);
            SedogoUser eventUser = new SedogoUser(m_loggedInUser, sedogoEvent.userID);

            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventUser, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);

            string inviteURL = gd.GetStringValue("SiteBaseURL");
            inviteURL = inviteURL + "?EID=" + m_eventID.ToString();

            emailBodyCopy.AppendLine("<html>");
            emailBodyCopy.AppendLine("<head><title></title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">");
            emailBodyCopy.AppendLine("<style type=\"text/css\">");
            emailBodyCopy.AppendLine("	body, td, p { font-size: 15px; color: #9B9885; font-family: Arial, Helvetica, Sans-Serif }");
            emailBodyCopy.AppendLine("	p { margin: 0 }");
            emailBodyCopy.AppendLine("	h1 { color: #00ccff; font-size: 18px; font-weight: bold; }");
            emailBodyCopy.AppendLine("	a, .blue { color: #00ccff; text-decoration: none; }");
            emailBodyCopy.AppendLine("	img { border: 0; }");
            emailBodyCopy.AppendLine("</style></head>");
            emailBodyCopy.AppendLine("<body bgcolor=\"#f0f1ec\">");
            emailBodyCopy.AppendLine("  <table width=\"692\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            //emailBodyCopy.AppendLine("	<tr><td colspan=\"3\"><img src=\"http://www.sedogo.com/email-template/images/email-template_01.png\" width=\"692\" height=\"32\" alt=\"\"></td></tr>");
            emailBodyCopy.AppendLine("	<tr><td style=\"background: #fff\" width=\"30\"></td>");
            emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"632\">");
            emailBodyCopy.AppendLine("			<h1>Your request to join the following goal has been accepted:</h1>");
            emailBodyCopy.AppendLine("			<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td width=\"60\">Who:</td>");
            emailBodyCopy.AppendLine("					<td width=\"10\" rowspan=\"4\">&nbsp;</td>");
            emailBodyCopy.AppendLine("					<td width=\"530\">" + eventUser.firstName + " " + eventUser.lastName + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td valign=\"top\">What:</td>");
            emailBodyCopy.AppendLine("					<td>" + sedogoEvent.eventName + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td valign=\"top\">Where:</td>");
            emailBodyCopy.AppendLine("					<td>" + sedogoEvent.eventVenue + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td valign=\"top\">When:</td>");
            emailBodyCopy.AppendLine("					<td>" + dateString + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("			</table>");
            emailBodyCopy.AppendLine("			<p>To view this event, <a href=\"" + inviteURL + "\"><u>click here</u></a>.</p>");
            emailBodyCopy.AppendLine("			<br /><br />");
            emailBodyCopy.AppendLine("			<p>Regards</p><a href=\"http://www.sedogo.com\" class=\"blue\"><strong>The Sedogo Team.</strong></a><br />");
            emailBodyCopy.AppendLine("			<br /><br /><br /><a href=\"http://www.sedogo.com\">");
            //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/logo.gif\" />");
            emailBodyCopy.AppendLine("			</a></td>");
            emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"30\"></td></tr><tr><td colspan=\"3\">");
            //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/email-template_05.png\" width=\"692\" height=\"32\" alt=\"\">");
            emailBodyCopy.AppendLine("		</td></tr><tr><td colspan=\"3\"><small>This message was intended for " + trackingUser.emailAddress + ". To stop receiving these emails, go to your profile and uncheck the 'Enable email notifications' option.<br/>Sedogo offices are located at Sedogo Ltd, The Studio, 17 Blossom St, London E1 6PL.</small></td></tr>");
            emailBodyCopy.AppendLine("		</td></tr></table></body></html>");

            string emailSubject = "Your request to join Sedogo goal: " + sedogoEvent.eventName + " has been accepted";

            string SMTPServer = gd.GetStringValue("SMTPServer");
            string mailFromAddress = gd.GetStringValue("MailFromAddress");
            string mailFromUsername = gd.GetStringValue("MailFromUsername");
            string mailFromPassword = gd.GetStringValue("MailFromPassword");

            if (eventUser.enableSendEmails == true)
            {
                try
                {
                    MailMessage message = new MailMessage(mailFromAddress, trackingUser.emailAddress);
                    message.ReplyTo = new MailAddress("noreply@sedogo.com");

                    message.Subject = emailSubject;
                    message.Body = emailBodyCopy.ToString();
                    message.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = SMTPServer;
                    if (mailFromPassword != "")
                    {
                        // If the password is blank, assume mail relay is permitted
                        smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
                    }
                    smtp.Send(message);

                    SentEmailHistory emailHistory = new SentEmailHistory("");
                    emailHistory.subject = emailSubject;
                    emailHistory.body = emailBodyCopy.ToString();
                    emailHistory.sentFrom = mailFromAddress;
                    emailHistory.sentTo = trackingUser.emailAddress;
                    emailHistory.Add();
                }
                catch (Exception ex)
                {
                    SentEmailHistory emailHistory = new SentEmailHistory("");
                    emailHistory.subject = emailSubject;
                    emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString();
                    emailHistory.sentFrom = mailFromAddress;
                    emailHistory.sentTo = trackingUser.emailAddress;
                    emailHistory.Add();
                }
            }
        }
    }

    //===============================================================
    // Class: SedogoEventPicture
    //===============================================================
    public class SedogoEventPicture
    {
        private int         m_eventPictureID = -1;
        private int         m_eventID = -1;
        private int         m_postedByUserID = -1;
        private string      m_eventImageFilename = "";
        private string      m_eventImageThumbnail = "";
        private string      m_eventImagePreview = "";
        private string      m_caption = "";
        private Boolean     m_deleted = false;
        private DateTime    m_createdDate = DateTime.MinValue;
        private string      m_createdByFullName = "";
        private DateTime    m_lastUpdatedDate = DateTime.MinValue;
        private string      m_lastUpdatedByFullName = "";

        private string      m_loggedInUser = "";

        public int eventPictureID
        {
            get { return m_eventPictureID; }
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
        public string eventImageFilename
        {
            get { return m_eventImageFilename; }
            set { m_eventImageFilename = value; }
        }
        public string eventImagePreview
        {
            get { return m_eventImagePreview; }
            set { m_eventImagePreview = value; }
        }
        public string caption
        {
            get { return m_caption; }
            set { m_caption = value; }
        }
        public string eventImageThumbnail
        {
            get { return m_eventImageThumbnail; }
            set { m_eventImageThumbnail = value; }
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
        // Function: SedogoEventPicture (Constructor)
        //===============================================================
        public SedogoEventPicture(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: SedogoEventPicture (Constructor)
        //===============================================================
        public SedogoEventPicture(string loggedInUser, int eventPictureID)
        {
            m_loggedInUser = loggedInUser;
            m_eventPictureID = eventPictureID;

            ReadEventPictureDetails();
        }

        //===============================================================
        // Function: ReadEventPictureDetails
        //===============================================================
        public void ReadEventPictureDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventPictureDetails";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@EventPictureID";
                param.Value = m_eventPictureID;
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
                if (!rdr.IsDBNull(rdr.GetOrdinal("ImageFilename")))
                {
                    m_eventImageFilename = (string)rdr["ImageFilename"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("ImagePreview")))
                {
                    m_eventImagePreview = (string)rdr["ImagePreview"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("ImageThumbnail")))
                {
                    m_eventImageThumbnail = (string)rdr["ImageThumbnail"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Caption")))
                {
                    m_caption = (string)rdr["Caption"];
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
                errorLog.WriteLog("SedogoEventPicture", "ReadEventPictureDetails", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spAddEventPicture", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = m_eventID;
                cmd.Parameters.Add("@PostedByUserID", SqlDbType.Int).Value = m_postedByUserID;
                cmd.Parameters.Add("@ImageFilename", SqlDbType.NVarChar, 200).Value = m_eventImageFilename;
                cmd.Parameters.Add("@ImageThumbnail", SqlDbType.NVarChar, 200).Value = m_eventImageThumbnail;
                cmd.Parameters.Add("@ImagePreview", SqlDbType.NVarChar, 200).Value = m_eventImagePreview;
                cmd.Parameters.Add("@Caption", SqlDbType.NVarChar, 500).Value = m_caption;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@CreatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                SqlParameter paramEventPictureID = cmd.CreateParameter();
                paramEventPictureID.ParameterName = "@EventPictureID";
                paramEventPictureID.SqlDbType = SqlDbType.Int;
                paramEventPictureID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramEventPictureID);

                cmd.ExecuteNonQuery();
                m_eventPictureID = (int)paramEventPictureID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEventPicture", "Add", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spUpdateEventPicture", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EventPictureID", SqlDbType.Int).Value = m_eventPictureID;
                cmd.Parameters.Add("@ImageFilename", SqlDbType.NVarChar, 200).Value = m_eventImageFilename;
                cmd.Parameters.Add("@ImageThumbnail", SqlDbType.NVarChar, 200).Value = m_eventImageThumbnail;
                cmd.Parameters.Add("@ImagePreview", SqlDbType.NVarChar, 200).Value = m_eventImagePreview;
                cmd.Parameters.Add("@Caption", SqlDbType.NVarChar, 500).Value = m_caption;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEventPicture", "Update", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spDeleteEventPicture", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EventPictureID", SqlDbType.Int).Value = m_eventPictureID;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEventPicture", "Delete", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

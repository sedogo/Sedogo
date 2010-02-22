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
using System.Net.Mail;

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
        private Boolean     m_reminderEmailSent = false;
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
        public Boolean reminderEmailSent
        {
            get { return m_reminderEmailSent; }
            set { m_reminderEmailSent = value; }
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
                if (!rdr.IsDBNull(rdr.GetOrdinal("ReminderEmailSent")))
                {
                    m_reminderEmailSent = (Boolean)rdr["ReminderEmailSent"];
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
                cmd.Parameters.Add("@Completed", SqlDbType.Bit).Value = m_completed;
                cmd.Parameters.Add("@ReminderEmailSent", SqlDbType.Bit).Value = m_reminderEmailSent;
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

        //===============================================================
        // Function: SendAlertEmails
        //===============================================================
        public static void SendAlertEmails()
        {
            GlobalData gd = new GlobalData("");
            string SMTPServer = gd.GetStringValue("SMTPServer");
            string mailFromAddress = gd.GetStringValue("MailFromAddress");
            string mailFromUsername = gd.GetStringValue("MailFromUsername");
            string mailFromPassword = gd.GetStringValue("MailFromPassword");

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectAlertsToSendByEmail";
                DbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int eventAlertID = int.Parse(rdr["EventAlertID"].ToString());

                    // Send the email
                    EventAlert eventAlert = new EventAlert("", eventAlertID);
                    SedogoEvent sedogoEvent = new SedogoEvent("", eventAlert.eventID);
                    SedogoUser user = new SedogoUser("", sedogoEvent.userID);
                    string dateString = "";
                    DateTime startDate = sedogoEvent.startDate;

                    string emailSubject = "Sedogo alert for event " + sedogoEvent.eventName;

                    MiscUtils.GetDateStringStartDate(user, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                        sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);

                    string inviteURL = gd.GetStringValue("SiteBaseURL");
                    inviteURL = inviteURL + "?EID=" + eventAlert.eventID.ToString();

                    StringBuilder emailBodyCopy = new StringBuilder();
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
                    emailBodyCopy.AppendLine("			<h1>The following event has been updated:</h1>");
                    emailBodyCopy.AppendLine("			<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">");
                    emailBodyCopy.AppendLine("				<tr>");
                    emailBodyCopy.AppendLine("					<td width=\"60\">What:</td>");
                    emailBodyCopy.AppendLine("					<td width=\"10\" rowspan=\"5\">&nbsp;</td>");
                    emailBodyCopy.AppendLine("					<td width=\"530\"><a href=\"" + inviteURL + "\">" + sedogoEvent.eventName + "</a></td>");
                    emailBodyCopy.AppendLine("				</tr>");
                    emailBodyCopy.AppendLine("				<tr>");
                    emailBodyCopy.AppendLine("					<td valign=\"top\">Where:</td>");
                    emailBodyCopy.AppendLine("					<td>" + sedogoEvent.eventVenue + "</td>");
                    emailBodyCopy.AppendLine("				</tr>");
                    emailBodyCopy.AppendLine("				<tr>");
                    emailBodyCopy.AppendLine("					<td valign=\"top\">Who:</td>");
                    emailBodyCopy.AppendLine("					<td>" + user.firstName + " " + user.lastName + "</td>");
                    emailBodyCopy.AppendLine("				</tr>");
                    emailBodyCopy.AppendLine("				<tr>");
                    emailBodyCopy.AppendLine("					<td valign=\"top\">When:</td>");
                    emailBodyCopy.AppendLine("					<td>" + dateString + "</td>");
                    emailBodyCopy.AppendLine("				</tr>");
                    emailBodyCopy.AppendLine("				<tr>");
                    emailBodyCopy.AppendLine("					<td valign=\"top\">Message:</td>");
                    emailBodyCopy.AppendLine("					<td><p style=\"color:black\">" + eventAlert.alertText.Replace("\n", "<br/>") + "</p></td>");
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
                    emailBodyCopy.AppendLine("		</td></tr><tr><td colspan=\"3\"><small>This message was intended for " + user.emailAddress + ". To stop receiving these emails, go to your profile and uncheck the 'Enable email notifications' option.<br/>Sedogo offices are located at Sedogo Ltd, The Studio, 17 Blossom St, London E1 6PL.</small></td></tr>");
                    emailBodyCopy.AppendLine("		</td></tr></table></body></html>");

                    if (user.enableSendEmails == true)
                    {
                        try
                        {
                            MailMessage message = new MailMessage(mailFromAddress, user.emailAddress);
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
                            emailHistory.sentTo = user.emailAddress;
                            emailHistory.Add();
                        }
                        catch (Exception ex)
                        {
                            SentEmailHistory emailHistory = new SentEmailHistory("");
                            emailHistory.subject = emailSubject;
                            emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString();
                            emailHistory.sentFrom = mailFromAddress;
                            emailHistory.sentTo = user.emailAddress;
                            emailHistory.Add();
                        }

                        eventAlert.reminderEmailSent = true;
                        eventAlert.Update();
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("EventAlert", "SendAlertEmails", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

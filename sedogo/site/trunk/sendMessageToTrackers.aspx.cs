//===============================================================
// Filename: sendMessageToTrackers.aspx.cs
// Date: 01/11/09
// --------------------------------------------------------------
// Description:
//   Send message to event trackers
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 01/11/09
// Revision history:
//===============================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Net.Mail;
using Sedogo.BusinessObjects;

public partial class sendMessageToTrackers : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        int messageUserID = -1;
        if (Request.QueryString["UID"] != null)
        {
            messageUserID = int.Parse(Request.QueryString["UID"]);
        }

        if (!IsPostBack)
        {
            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

            eventNameLabel.Text = sedogoEvent.eventName;

            SetFocus(messageTextBox);

            if (Session["SendMessageCaptcha"] == null || (string)Session["SendMessageCaptcha"] == "N")
            {
                registerCaptcha.Visible = true;
            }
            else
            {
                registerCaptcha.Visible = false;
            }
        }
        PopulateTrackingList(eventID, messageUserID);
    }


    //===============================================================
    // Function: PopulateTrackingList
    //===============================================================
    private void PopulateTrackingList(int eventID, int messageUserID)
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            trackingLinksPlaceholder.Controls.Add(new LiteralControl("<p>"));

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectTrackingUsersByEventID";
            cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string profilePicThumbnail = "";

                int trackedEventID = int.Parse(rdr["TrackedEventID"].ToString());
                int userID = int.Parse(rdr["UserID"].ToString());
                var userGuid = rdr["GUID"].ToString();
                string firstName = (string)rdr["FirstName"];
                string lastName = (string)rdr["LastName"];
                string gender = (string)rdr["Gender"];
                //string homeTown = (string)rdr["HomeTown"];
                //string emailAddress = (string)rdr["EmailAddress"];
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicThumbnail")))
                {
                    profilePicThumbnail = (string)rdr["ProfilePicThumbnail"];
                }
                //string profilePicPreview = (string)rdr["ProfilePicPreview"];
                int avatarNumber = -1;
                if (!rdr.IsDBNull(rdr.GetOrdinal("AvatarNumber")))
                {
                    avatarNumber = int.Parse(rdr["AvatarNumber"].ToString());
                }

                string profileImagePath = "./images/profile/blankProfile.jpg";
                if (profilePicThumbnail != "")
                {
                    profileImagePath = ImageHelper.GetRelativeImagePath(userID, userGuid, ImageType.UserThumbnail).Replace("~", ".");
                }
                else
                {
                    if (avatarNumber > 0)
                    {
                        profileImagePath = "./images/avatars/avatar" + avatarNumber.ToString() + "sm.gif";
                    }
                    else
                    {
                        if (gender == "M")
                        {
                            // 1,2,5
                            int avatarID = 5;
                            switch ((userID % 6))
                            {
                                case 0: case 1: avatarID = 1; break;
                                case 2: case 3: avatarID = 2; break;
                            }
                            profileImagePath = "./images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                        }
                        else
                        {
                            // 3,4,6
                            int avatarID = 6;
                            switch ((userID % 6))
                            {
                                case 0: case 1: avatarID = 3; break;
                                case 2: case 3: avatarID = 4; break;
                            }
                            profileImagePath = "./images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                        }
                    }
                }

                CheckBox trackerCheckBox = new CheckBox();
                trackerCheckBox.ID = "trackerCheckBox_" + trackedEventID.ToString();
                if (messageUserID < 0 || messageUserID == userID)
                {
                    trackerCheckBox.Checked = true;
                }

                string outputText = "<img src=\"" + profileImagePath + "\" width=\"17\" style=\"margin-right:4px\" />&nbsp;"
                    + firstName + " " + lastName;

                trackingLinksPlaceholder.Controls.Add(new LiteralControl("<span style=\"white-space:nowrap\">"));
                trackingLinksPlaceholder.Controls.Add(trackerCheckBox);
                trackingLinksPlaceholder.Controls.Add(new LiteralControl("&nbsp;"));
                trackingLinksPlaceholder.Controls.Add(new LiteralControl(outputText));
                trackingLinksPlaceholder.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                trackingLinksPlaceholder.Controls.Add(new LiteralControl("</span>"));
            }
            rdr.Close();

            trackingLinksPlaceholder.Controls.Add(new LiteralControl("</p>"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }
    }

    //===============================================================
    // Function: saveChangesButton_click
    //===============================================================
    protected void saveChangesButton_click(object sender, EventArgs e)
    {
        Boolean continueSending = false;
        if (Session["SendMessageCaptcha"] == null || (string)Session["SendMessageCaptcha"] == "N")
        {
            if (registerCaptcha.IsValid == true)
            {
                Session["SendMessageCaptcha"] = "Y";
                continueSending = true;
            }
        }
        else
        {
            // Captcha has already been done in this session
            continueSending = true;
        }
        if (continueSending == true)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);

            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
            GlobalData gd = new GlobalData("");
            SedogoUser currentUser = new SedogoUser(Session["loggedInUserFullName"].ToString(),
                int.Parse(Session["loggedInUserID"].ToString()));

            string messageText = messageTextBox.Text;

            SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectTrackingUsersByEventID";
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                DbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int trackedEventID = int.Parse(rdr["TrackedEventID"].ToString());
                    int userID = int.Parse(rdr["UserID"].ToString());
                    string firstName = (string)rdr["FirstName"];
                    string lastName = (string)rdr["LastName"];
                    string emailAddress = (string)rdr["EmailAddress"];

                    string fieldID = "trackerCheckBox_" + trackedEventID.ToString();
                    CheckBox trackerCheckBox = (CheckBox)FindControl(fieldID);

                    if (trackerCheckBox.Checked == true)
                    {
                        Message message = new Message(Session["loggedInUserFullName"].ToString());
                        message.userID = userID;
                        message.eventID = eventID;
                        message.postedByUserID = int.Parse(Session["loggedInUserID"].ToString());
                        message.messageText = messageText;
                        message.Add();

                        StringBuilder emailBodyCopy = new StringBuilder();

                        string eventURL = gd.GetStringValue("SiteBaseURL");
                        eventURL = eventURL + "?EID=" + eventID.ToString();

                        string replyURL = gd.GetStringValue("SiteBaseURL");
                        replyURL = replyURL + "?ReplyID=" + eventID.ToString();

                        emailBodyCopy.AppendLine("<html>");
                        emailBodyCopy.AppendLine("<head><title></title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">");
                        emailBodyCopy.AppendLine("<style type=\"text/css\">");
                        emailBodyCopy.AppendLine("	body, td, p { font-size: 15px; color: #9B9885; font-family: Arial, Helvetica, Sans-Serif }");
                        emailBodyCopy.AppendLine("	p { margin: 0 }");
                        emailBodyCopy.AppendLine("	h1 { color: #00ccff; font-size: 18px; font-weight: bold; }");
                        emailBodyCopy.AppendLine("	a, .blue { color: #00ccff; text-decoration: none; }");
                        emailBodyCopy.AppendLine("</style></head>");
                        emailBodyCopy.AppendLine("<body bgcolor=\"#f0f1ec\">");
                        emailBodyCopy.AppendLine("  <table width=\"692\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                        //emailBodyCopy.AppendLine("	<tr><td colspan=\"3\"><img src=\"http://www.sedogo.com/email-template/images/email-template_01.png\" width=\"692\" height=\"32\" alt=\"\"></td></tr>");
                        emailBodyCopy.AppendLine("	<tr><td style=\"background: #fff\" width=\"30\"></td>");
                        emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"632\">");
                        //emailBodyCopy.AppendLine("			<h1>sedogo.com message</h1>");
                        emailBodyCopy.AppendLine("			<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">");
                        emailBodyCopy.AppendLine("				<tr>");
                        emailBodyCopy.AppendLine("					<td width=\"60\">From:</td>");
                        emailBodyCopy.AppendLine("					<td width=\"10\" rowspan=\"5\">&nbsp;</td>");
                        emailBodyCopy.AppendLine("					<td width=\"530\">" + currentUser.firstName + " " + currentUser.lastName + "</td>");
                        emailBodyCopy.AppendLine("				</tr>");
                        emailBodyCopy.AppendLine("				<tr>");
                        emailBodyCopy.AppendLine("					<td valign=\"top\">Goal:</td>");
                        emailBodyCopy.AppendLine("					<td><a href=\"" + eventURL + "\">" + sedogoEvent.eventName + "</a></td>");
                        emailBodyCopy.AppendLine("				</tr>");
                        emailBodyCopy.AppendLine("				<tr>");
                        emailBodyCopy.AppendLine("					<td valign=\"top\">Where:</td>");
                        emailBodyCopy.AppendLine("					<td>" + sedogoEvent.eventVenue + "</td>");
                        emailBodyCopy.AppendLine("				</tr>");
                        emailBodyCopy.AppendLine("				<tr>");
                        emailBodyCopy.AppendLine("					<td valign=\"top\">Message:</td>");
                        emailBodyCopy.AppendLine("					<td><p style=\"color:black\">" + messageText.Replace("\n", "<br/>") + "</p></td>");
                        emailBodyCopy.AppendLine("				</tr>");
                        emailBodyCopy.AppendLine("				<tr>");
                        emailBodyCopy.AppendLine("					<td valign=\"top\"></td>");
                        emailBodyCopy.AppendLine("					<td><a class=\"blue\" href=\"" + replyURL + "\">Click here to reply to this message</a></td>");
                        emailBodyCopy.AppendLine("				</tr>");
                        emailBodyCopy.AppendLine("			</table>");
                        emailBodyCopy.AppendLine("			<br /><br />");
                        emailBodyCopy.AppendLine("			<p>Regards</p><a href=\"http://www.sedogo.com\" class=\"blue\"><strong>The Sedogo Team.</strong></a><br />");
                        emailBodyCopy.AppendLine("			<br /><br /><br /><a href=\"http://www.sedogo.com\">");
                        //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/logo.gif\" />");
                        emailBodyCopy.AppendLine("			</a></td>");
                        emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"30\"></td></tr><tr><td colspan=\"3\">");
                        //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/email-template_05.png\" width=\"692\" height=\"32\" alt=\"\">");
                        emailBodyCopy.AppendLine("		</td></tr><tr><td colspan=\"3\"><small>This message was intended for " + emailAddress + ". To stop receiving these emails, go to your profile and uncheck the 'Enable email notifications' option.<br/>Sedogo offices are located at Sedogo Ltd, The Studio, 17 Blossom St, London E1 6PL.</small></td></tr>");
                        emailBodyCopy.AppendLine("		</td></tr></table></body></html>");

                        string emailSubject = "Sedogo message from " + currentUser.firstName + " regarding " + sedogoEvent.eventName;

                        string SMTPServer = gd.GetStringValue("SMTPServer");
                        string mailFromAddress = gd.GetStringValue("MailFromAddress");
                        string mailFromUsername = gd.GetStringValue("MailFromUsername");
                        string mailFromPassword = gd.GetStringValue("MailFromPassword");

                        SedogoUser inviteUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
                        if (inviteUser.enableSendEmails == true)
                        {
                            try
                            {
                                MailMessage mailMessage = new MailMessage(mailFromAddress, emailAddress);
                                mailMessage.ReplyTo = new MailAddress("noreply@sedogo.com");

                                mailMessage.Subject = emailSubject;
                                mailMessage.Body = emailBodyCopy.ToString();
                                mailMessage.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = SMTPServer;
                                if (mailFromPassword != "")
                                {
                                    // If the password is blank, assume mail relay is permitted
                                    smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
                                }
                                smtp.Send(mailMessage);

                                SentEmailHistory emailHistory = new SentEmailHistory(Session["loggedInUserFullName"].ToString());
                                emailHistory.subject = emailSubject;
                                emailHistory.body = emailBodyCopy.ToString();
                                emailHistory.sentFrom = mailFromAddress;
                                emailHistory.sentTo = emailAddress;
                                emailHistory.Add();
                            }
                            catch (Exception ex)
                            {
                                SentEmailHistory emailHistory = new SentEmailHistory(Session["loggedInUserFullName"].ToString());
                                emailHistory.subject = emailSubject;
                                emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString();
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
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
        }
    }
}

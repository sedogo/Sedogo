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
                string firstName = (string)rdr["FirstName"];
                string lastName = (string)rdr["LastName"];
                //string gender = (string)rdr["Gender"];
                //string homeTown = (string)rdr["HomeTown"];
                //string emailAddress = (string)rdr["EmailAddress"];
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicThumbnail")))
                {
                    profilePicThumbnail = (string)rdr["ProfilePicThumbnail"];
                }
                //string profilePicPreview = (string)rdr["ProfilePicPreview"];

                string profileImagePath = "./images/profile/blankProfile.jpg";
                if (profilePicThumbnail != "")
                {
                    profileImagePath = "./assets/profilePics/" + profilePicThumbnail;
                }

                CheckBox trackerCheckBox = new CheckBox();
                trackerCheckBox.ID = "trackerCheckBox_" + trackedEventID.ToString();
                if (messageUserID < 0 || messageUserID == userID)
                {
                    trackerCheckBox.Checked = true;
                }

                string outputText = "<img src=\"" + profileImagePath + "\" />&nbsp;"
                    + firstName + " " + lastName;

                //trackingLinksPlaceholder.Controls.Add(new LiteralControl("<table><tr><td>"));
                trackingLinksPlaceholder.Controls.Add(trackerCheckBox);
                trackingLinksPlaceholder.Controls.Add(new LiteralControl("&nbsp;"));
                trackingLinksPlaceholder.Controls.Add(new LiteralControl(outputText));
                trackingLinksPlaceholder.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                //trackingLinksPlaceholder.Controls.Add(new LiteralControl("</td></tr></table>"));
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
                    emailBodyCopy.AppendLine("	<tr><td colspan=\"3\"><img src=\"http://www.sedogo.com/email-template/images/email-template_01.png\" width=\"692\" height=\"32\" alt=\"\"></td></tr>");
                    emailBodyCopy.AppendLine("	<tr><td style=\"background: #fff\" width=\"30\"></td>");
                    emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"632\">");
                    //emailBodyCopy.AppendLine("			<h1>sedogo.com message</h1>");
                    emailBodyCopy.AppendLine("			<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"300\">");
                    emailBodyCopy.AppendLine("				<tr>");
                    emailBodyCopy.AppendLine("					<td width=\"60px\">From:</td>");
                    emailBodyCopy.AppendLine("					<td width=\"240px\">" + currentUser.firstName + " " + currentUser.lastName + "</td>");
                    emailBodyCopy.AppendLine("				</tr>");
                    emailBodyCopy.AppendLine("				<tr>");
                    emailBodyCopy.AppendLine("					<td width=\"60px\">Goal:</td>");
                    emailBodyCopy.AppendLine("					<td width=\"240px\">" + sedogoEvent.eventName + "</td>");
                    emailBodyCopy.AppendLine("				</tr>");
                    emailBodyCopy.AppendLine("				<tr>");
                    emailBodyCopy.AppendLine("					<td>Where:</td>");
                    emailBodyCopy.AppendLine("					<td>" + sedogoEvent.eventVenue + "</td>");
                    emailBodyCopy.AppendLine("				</tr>");
                    emailBodyCopy.AppendLine("				<tr>");
                    emailBodyCopy.AppendLine("					<td width=\"60px\">Message:</td>");
                    emailBodyCopy.AppendLine("					<td width=\"240px\">" + messageText.Replace("\n", "<br/>") + "</td>");
                    emailBodyCopy.AppendLine("				</tr>");
                    emailBodyCopy.AppendLine("			</table>");
                    emailBodyCopy.AppendLine("			<br /><br />");
                    emailBodyCopy.AppendLine("			<p>Regards</p><a href=\"http://www.sedogo.com\" class=\"blue\"><strong>The Sedogo Team.</strong></a><br />");
                    emailBodyCopy.AppendLine("			<br /><br /><br /><a href=\"http://www.sedogo.com\"><img src=\"http://www.sedogo.com/email-template/images/logo.gif\" /></a></td>");
                    emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"30\"></td></tr><tr><td colspan=\"3\">");
                    emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/email-template_05.png\" width=\"692\" height=\"32\" alt=\"\">");
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
                            mailMessage.ReplyTo = new MailAddress(mailFromAddress);

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
                        }
                        catch { }
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

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }
}

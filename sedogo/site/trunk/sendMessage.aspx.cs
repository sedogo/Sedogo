//===============================================================
// Filename: sendMessage.aspx.cs
// Date: 28/09/09
// --------------------------------------------------------------
// Description:
//   Send message
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 28/09/09
// Revision history:
//===============================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Globalization;
using System.Net.Mail;
using Sedogo.BusinessObjects;

public partial class sendMessage : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);
            int userID = int.Parse(Session["loggedInUserID"].ToString());

            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

            eventNameLabel.Text = sedogoEvent.eventName;

            SedogoUser eventOwner = new SedogoUser(Session["loggedInUserFullName"].ToString(), sedogoEvent.userID);

            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventOwner, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);

            messageToLabel.Text = eventOwner.firstName + " " + eventOwner.lastName;
            eventDateLabel.Text = dateString;
            eventVenueLabel.Text = sedogoEvent.eventVenue.Replace("\n", "<br/>");

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
            SedogoUser currentUser = new SedogoUser(Session["loggedInUserFullName"].ToString(),
                int.Parse(Session["loggedInUserID"].ToString()));
            SedogoUser eventOwner = new SedogoUser(Session["loggedInUserFullName"].ToString(),
                sedogoEvent.userID);

            string messageText = messageTextBox.Text;

            Message message = new Message(Session["loggedInUserFullName"].ToString());
            message.userID = sedogoEvent.userID;
            message.eventID = eventID;
            message.postedByUserID = int.Parse(Session["loggedInUserID"].ToString());
            message.messageText = messageText;
            message.Add();

            StringBuilder emailBodyCopy = new StringBuilder();

            GlobalData gd = new GlobalData("");
            string eventURL = gd.GetStringValue("SiteBaseURL");
            eventURL = eventURL + "?EID=" + eventID.ToString();

            string replyURL = gd.GetStringValue("SiteBaseURL");
            replyURL = replyURL + "?Redir=Messages&ReplyID=" + eventID.ToString();

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
            emailBodyCopy.AppendLine("		</td></tr><tr><td colspan=\"3\"><small>This message was intended for " + eventOwner.emailAddress + ". To stop receiving these emails, go to your profile and uncheck the 'Enable email notifications' option.<br/>Sedogo offices are located at Sedogo Ltd, The Studio, 17 Blossom St, London E1 6PL.</small></td></tr>");
            emailBodyCopy.AppendLine("		</td></tr></table></body></html>");

            string emailSubject = "Sedogo message from " + currentUser.firstName + " regarding " + sedogoEvent.eventName;

            string SMTPServer = gd.GetStringValue("SMTPServer");
            string mailFromAddress = gd.GetStringValue("MailFromAddress");
            string mailFromUsername = gd.GetStringValue("MailFromUsername");
            string mailFromPassword = gd.GetStringValue("MailFromPassword");

            if (eventOwner.enableSendEmails == true)
            {
                try
                {
                    MailMessage mailMessage = new MailMessage(mailFromAddress, eventOwner.emailAddress);
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
                    emailHistory.sentTo = eventOwner.emailAddress;
                    emailHistory.Add();
                }
                catch (Exception ex)
                {
                    SentEmailHistory emailHistory = new SentEmailHistory(Session["loggedInUserFullName"].ToString());
                    emailHistory.subject = emailSubject;
                    emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString();
                    emailHistory.sentFrom = mailFromAddress;
                    emailHistory.sentTo = eventOwner.emailAddress;
                    emailHistory.Add();
                }
            }

            Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
        }
    }
}

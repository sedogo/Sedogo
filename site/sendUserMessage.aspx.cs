//===============================================================
// Filename: sendUserMessage.aspx.cs
// Date: 19/11/09
// --------------------------------------------------------------
// Description:
//   Send message
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 19/11/09
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

public partial class sendUserMessage : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);
            int messageToUserID = int.Parse(Request.QueryString["UID"]);

            if( eventID > 0 )
            {
                goalNameDiv.Visible = true;
                SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
                eventNameLabel.Text = sedogoEvent.eventName;
            }
            else
            {
                goalNameDiv.Visible = false;
            }
            SedogoUser messageToUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), messageToUserID);

            messageToLabel.Text = messageToUser.firstName + " " + messageToUser.lastName;

            SetFocus(messageTextBox);
        }
    }

    //===============================================================
    // Function: saveChangesButton_click
    //===============================================================
    protected void saveChangesButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        int messageToUserID = int.Parse(Request.QueryString["UID"]);

        string messageText = messageTextBox.Text;

        Message message = new Message(Session["loggedInUserFullName"].ToString());
        message.userID = messageToUserID;
        message.eventID = eventID;
        message.postedByUserID = int.Parse(Session["loggedInUserID"].ToString());
        message.messageText = messageText;
        message.Add();

        SedogoUser currentUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), int.Parse(Session["loggedInUserID"].ToString()));
        SedogoUser messageToUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), messageToUserID);
        GlobalData gd = new GlobalData("");

        StringBuilder emailBodyCopy = new StringBuilder();

        string linkURL = gd.GetStringValue("SiteBaseURL");
        linkURL = linkURL + "?Redir=Messages";

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
        emailBodyCopy.AppendLine("	<tr><td colspan=\"3\"><img src=\"http://www.sedogo.com/email-template/images/email-template_01.png\" width=\"692\" height=\"32\" alt=\"\"></td></tr>");
        emailBodyCopy.AppendLine("	<tr><td style=\"background: #fff\" width=\"30\"></td>");
        emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"632\">");
        emailBodyCopy.AppendLine("			<h1>sedogo.com message</h1>");
        emailBodyCopy.AppendLine("			<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"300\">");
        emailBodyCopy.AppendLine("				<tr>");
        emailBodyCopy.AppendLine("					<td width=\"60px\">From:</td>");
        emailBodyCopy.AppendLine("					<td width=\"10px\" rowspan=\"3\">&nbsp;</td>");
        emailBodyCopy.AppendLine("					<td width=\"240px\">" + currentUser.firstName + " " + currentUser.lastName + "</td>");
        emailBodyCopy.AppendLine("				</tr>");
        emailBodyCopy.AppendLine("				<tr>");
        emailBodyCopy.AppendLine("					<td width=\"60px\">To:</td>");
        emailBodyCopy.AppendLine("					<td width=\"240px\">" + messageToUser.firstName + " " + messageToUser.lastName + "</td>");
        emailBodyCopy.AppendLine("				</tr>");
        emailBodyCopy.AppendLine("				<tr>");
        emailBodyCopy.AppendLine("					<td width=\"60px\" valign=\"top\">Message:</td>");
        emailBodyCopy.AppendLine("					<td width=\"240px\"><p style=\"color:black\">" + messageText.Replace("\n", "<br/>") + "</p></td>");
        emailBodyCopy.AppendLine("				</tr>");
        emailBodyCopy.AppendLine("			</table>");
        emailBodyCopy.AppendLine("			<p>To view all your messages, <a href=\"" + linkURL + "\">click here</a>.</p>");
        emailBodyCopy.AppendLine("			<br /><br />");
        emailBodyCopy.AppendLine("			<p>Regards</p><a href=\"http://www.sedogo.com\" class=\"blue\"><strong>The Sedogo Team.</strong></a><br />");
        emailBodyCopy.AppendLine("			<br /><br /><br /><a href=\"http://www.sedogo.com\"><img src=\"http://www.sedogo.com/email-template/images/logo.gif\" /></a></td>");
        emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"30\"></td></tr><tr><td colspan=\"3\">");
        emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/email-template_05.png\" width=\"692\" height=\"32\" alt=\"\">");
        emailBodyCopy.AppendLine("		</td></tr><tr><td colspan=\"3\"><small>To stop receiving these emails, go to your profile and uncheck the 'Enable email notifications' option.</small></td></tr>");
        emailBodyCopy.AppendLine("		</td></tr></table></body></html>");

        string emailSubject = "You have a new Sedogo message from: " + currentUser.firstName + " " + currentUser.lastName;

        string SMTPServer = gd.GetStringValue("SMTPServer");
        string mailFromAddress = gd.GetStringValue("MailFromAddress");
        string mailFromUsername = gd.GetStringValue("MailFromUsername");
        string mailFromPassword = gd.GetStringValue("MailFromPassword");

        SedogoUser inviteUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), messageToUserID);
        if (inviteUser.enableSendEmails == true)
        {
            try
            {
                MailMessage emailMessage = new MailMessage(mailFromAddress, messageToUser.emailAddress);
                emailMessage.ReplyTo = new MailAddress(mailFromAddress);

                emailMessage.Subject = emailSubject;
                emailMessage.Body = emailBodyCopy.ToString();
                emailMessage.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = SMTPServer;
                if (mailFromPassword != "")
                {
                    // If the password is blank, assume mail relay is permitted
                    smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
                }
                smtp.Send(emailMessage);
            }
            catch {}
        }

        Response.Redirect("message.aspx");
    }
}

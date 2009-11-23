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

            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
            SedogoUser messageToUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), messageToUserID);

            eventNameLabel.Text = sedogoEvent.eventName;
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

        try
        {
            StringBuilder emailBodyCopy = new StringBuilder();

            string linkURL = gd.GetStringValue("SiteBaseURL");
            linkURL = linkURL + "?Redir=Messages";

            emailBodyCopy.AppendLine("From: " + currentUser.firstName + " " + currentUser.lastName + "<br/>&nbsp;<br/>");
            emailBodyCopy.AppendLine("To: " + messageToUser.firstName + " " + messageToUser.lastName + "<br/>&nbsp;<br/>");
            emailBodyCopy.AppendLine("Message:<br/>");
            emailBodyCopy.AppendLine(messageText.Replace("\n","<br/>") + "<br/>&nbsp;<br/>");
            emailBodyCopy.AppendLine("To view all your messages, <a href=\"" + linkURL + "\">click here</a>.<br/>");
            emailBodyCopy.AppendLine("Regards,<br/>&nbsp;<br/>");
            emailBodyCopy.AppendLine("The Sedogo Team<br/>&nbsp;<br/>");
            emailBodyCopy.AppendLine("<img src=\"http://sedogo.websites.bta.com/images/sedogo.gif\" /><br/>");
            emailBodyCopy.AppendLine("Create your future and connect with others to make it happen");

            string emailSubject = "You have a new Sedogo message from: " + currentUser.firstName + " " + currentUser.lastName;

            string SMTPServer = gd.GetStringValue("SMTPServer");
            string mailFromAddress = gd.GetStringValue("MailFromAddress");
            string mailFromUsername = gd.GetStringValue("MailFromUsername");
            string mailFromPassword = gd.GetStringValue("MailFromPassword");

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

        Response.Redirect("message.aspx");
    }
}

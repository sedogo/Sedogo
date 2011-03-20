//===============================================================
// Filename: help.aspx
// Date: 20/12/09
// --------------------------------------------------------------
// Description:
//   Help
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 20/12/09
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
using System.Net.Mail;
using System.Text;
using Sedogo.BusinessObjects;

public partial class help : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int currentUserID = -1;
            SedogoUser currentUser = null;
            if (Session["loggedInUserID"] != null)
            {
                currentUserID = int.Parse(Session["loggedInUserID"].ToString());
                currentUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), currentUserID);

                emailAddressTextBox.Text = currentUser.emailAddress;
            }
            else
            {
                emailAddressTextBox.Text = "";
            }
        }
    }

    //===============================================================
    // Function: sendFeedbackButton_click
    //===============================================================
    protected void sendFeedbackButton_click(object sender, EventArgs e)
    {
        int currentUserID = -1;
        SedogoUser currentUser = null;
        string userEmailAddress = "";
        if (Session["loggedInUserID"] != null)
        {
            currentUserID = int.Parse(Session["loggedInUserID"].ToString());
            currentUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), currentUserID);
            userEmailAddress = currentUser.emailAddress;
        }
        else
        {
            userEmailAddress = emailAddressTextBox.Text;
        }

        string feedbackText = feedbackTextBox.Text;

        GlobalData gd = new GlobalData("");
        string SMTPServer = gd.GetStringValue("SMTPServer");
        string mailFromAddress = gd.GetStringValue("MailFromAddress");
        string mailFromUsername = gd.GetStringValue("MailFromUsername");
        string mailFromPassword = gd.GetStringValue("MailFromPassword");

        //MailMessage message = new MailMessage(mailFromAddress, "phil@axinteractive.com");
        MailMessage message = new MailMessage(mailFromAddress, "help@sedogo.com");
        message.ReplyTo = new MailAddress("noreply@sedogo.com");

        StringBuilder emailBody = new StringBuilder();
        emailBody.AppendLine("<html><body>");
        emailBody.AppendLine("Help:<br/>");
        emailBody.AppendLine(userEmailAddress + "<br/>");
        emailBody.AppendLine(feedbackText.Replace("\n", "<br/>") + "<br/>");
        emailBody.AppendLine("</body></html>");

        message.Subject = "Sedogo help";
        message.Body = emailBody.ToString();
        message.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = SMTPServer;
        if (mailFromPassword != "")
        {
            // If the password is blank, assume mail relay is permitted
            smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
        }
        try
        {
            smtp.Send(message);

            SentEmailHistory emailHistory = new SentEmailHistory("");
            emailHistory.subject = "Sedogo help";
            emailHistory.body = emailBody.ToString();
            emailHistory.sentFrom = mailFromAddress;
            emailHistory.sentTo = "help@sedogo.com";
            emailHistory.Add();
        }
        catch (Exception ex)
        {
            SentEmailHistory emailHistory = new SentEmailHistory("");
            emailHistory.subject = "Sedogo help";
            emailHistory.body = ex.Message + " -------- " + emailBody.ToString();
            emailHistory.sentFrom = mailFromAddress;
            emailHistory.sentTo = "help@sedogo.com";
            emailHistory.Add();
        }

        Response.Redirect("profileRedirect.aspx");
    }
}

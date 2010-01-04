//===============================================================
// Filename: feedback.aspx
// Date: 20/12/09
// --------------------------------------------------------------
// Description:
//   Send feedback
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

public partial class feedback : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //===============================================================
    // Function: sendFeedbackButton_click
    //===============================================================
    protected void sendFeedbackButton_click(object sender, EventArgs e)
    {
        string userEmailAddress = "(not logged in)";

        int currentUserID = -1;
        if (Session["loggedInUserID"] != null)
        {
            currentUserID = int.Parse(Session["loggedInUserID"].ToString());
            SedogoUser currentUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), currentUserID);
            userEmailAddress = currentUser.emailAddress;
        }
        
        string feedbackText = feedbackTextBox.Text;

        GlobalData gd = new GlobalData("");
        string SMTPServer = gd.GetStringValue("SMTPServer");
        string mailFromAddress = gd.GetStringValue("MailFromAddress");
        string mailFromUsername = gd.GetStringValue("MailFromUsername");
        string mailFromPassword = gd.GetStringValue("MailFromPassword");

        //MailMessage message = new MailMessage(mailFromAddress, "phil@axinteractive.com");
        MailMessage message = new MailMessage(mailFromAddress, "help@sedogo.com");
        message.ReplyTo = new MailAddress(mailFromAddress);

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
        }
        catch { }

        Response.Redirect("profileRedirect.aspx");
    }
}

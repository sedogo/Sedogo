//===============================================================
// Filename: lostActivation.aspx
// Date: 25/06/10
// --------------------------------------------------------------
// Description:
//   Lost activation
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 25/06/10
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

public partial class lostActivation : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["SedogoLoginEmailAddress"];
            // Check to make sure the cookie exists
            if (cookie != null)
            {
                emailAddressTextBox.Text = cookie.Value.ToString();
            }
            SetFocus(emailAddressTextBox);
        }
    }

    //===============================================================
    // Function: resendActivationButton_click
    //===============================================================
    protected void resendActivationButton_click(object sender, EventArgs e)
    {
        string loginEmailAddress = emailAddressTextBox.Text;

        int userID = SedogoUser.GetUserIDFromEmailAddress(loginEmailAddress);
        if (userID > 0)
        {
            SedogoUser user = new SedogoUser("", userID);
            if (user.loginEnabled == false)
            {
                // Send registration email
                GlobalData gd = new GlobalData((string)Session["loggedInUserFullName"]);

                string siteBaseURL = gd.GetStringValue("SiteBaseURL");

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
                emailBodyCopy.AppendLine("			<h1>Thanks for registering with Sedogo</h1>");
                emailBodyCopy.AppendLine("			<p>Please click on the link below to confirm your email.</p>");
                emailBodyCopy.AppendLine("			<p><a href=\"" + siteBaseURL + "/e/?G=" + user.GUID + "\"><u>click here</u></a>.</p>");
                emailBodyCopy.AppendLine("			<br /><br />");
                emailBodyCopy.AppendLine("			<p>Regards</p><a href=\"http://www.sedogo.com\" class=\"blue\"><strong>The Sedogo Team.</strong></a><br />");
                emailBodyCopy.AppendLine("			<br /></td>");
                emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"30\"></td></tr></table></body></html>");

                // Old version, removed because of spam filters
                //emailBodyCopy.AppendLine("			<br /><br /><br /><a href=\"http://www.sedogo.com\"><img src=\"http://www.sedogo.com/email-template/images/logo.gif\" /></a></td>");
                //emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"30\"></td></tr><tr><td colspan=\"3\">");
                //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/email-template_05.png\" width=\"692\" height=\"32\" alt=\"\">");
                //emailBodyCopy.AppendLine("		</td></tr></table></body></html>");

                string SMTPServer = gd.GetStringValue("SMTPServer");
                string mailFromAddress = gd.GetStringValue("MailFromAddress");
                string mailFromUsername = gd.GetStringValue("MailFromUsername");
                string mailFromPassword = gd.GetStringValue("MailFromPassword");

                string mailToEmailAddress = user.emailAddress;

                MailMessage message = new MailMessage(mailFromAddress, mailToEmailAddress);
                message.ReplyTo = new MailAddress("noreply@sedogo.com");

                message.Subject = "Sedogo registration";
                message.Body = emailBodyCopy.ToString();
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
                    emailHistory.subject = "Sedogo registration";
                    emailHistory.body = emailBodyCopy.ToString();
                    emailHistory.sentFrom = mailFromAddress;
                    emailHistory.sentTo = user.emailAddress;
                    emailHistory.Add();
                }
                catch (Exception ex)
                {
                    SentEmailHistory emailHistory = new SentEmailHistory("");
                    emailHistory.subject = "Sedogo registration";
                    emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString();
                    emailHistory.sentFrom = mailFromAddress;
                    emailHistory.sentTo = user.emailAddress;
                    emailHistory.Add();
                }
                
                Response.Redirect("default.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Reset password", "alert(\"This account has already been activated.\");", true);
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Reset password", "alert(\"No account with this email address was found.\");", true);
        }
    }
}

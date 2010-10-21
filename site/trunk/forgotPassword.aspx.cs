//===============================================================
// Filename: forgotPassword.aspx
// Date: 20/08/09
// --------------------------------------------------------------
// Description:
//   Forgot password
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 20/08/09
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

public partial class forgotPassword : System.Web.UI.Page
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
    // Function: forgotPasswordButton_click
    //===============================================================
    protected void forgotPasswordButton_click(object sender, EventArgs e)
    {
        string loginEmailAddress = emailAddressTextBox.Text;

        SedogoUser user = new SedogoUser("");
        string newPassword = "";
        Boolean resetStatus = user.ResetUserPassword(loginEmailAddress, ref newPassword);
        if (resetStatus == true)
        {
            // Send the user an email with the new password
            // Email the password to the user
            GlobalData gd = new GlobalData("");
            string SMTPServer = gd.GetStringValue("SMTPServer");
            string mailFromAddress = gd.GetStringValue("MailFromAddress");
            string mailFromUsername = gd.GetStringValue("MailFromUsername");
            string mailFromPassword = gd.GetStringValue("MailFromPassword");

            MailMessage message = new MailMessage(mailFromAddress, loginEmailAddress);
            message.ReplyTo = new MailAddress("noreply@sedogo.com");

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
            emailBodyCopy.AppendLine("			<h1>Your new sedogo.com password</h1>");
            emailBodyCopy.AppendLine("			<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td width=\"60\">Password:</td>");
            emailBodyCopy.AppendLine("					<td width=\"10\">&nbsp;</td>");
            emailBodyCopy.AppendLine("					<td width=\"530\">" + newPassword + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("			</table>");
            emailBodyCopy.AppendLine("			<br /><br />");
            emailBodyCopy.AppendLine("			<p>Regards</p><a href=\"http://www.sedogo.com\" class=\"blue\"><strong>The Sedogo Team.</strong></a><br />");
            emailBodyCopy.AppendLine("			<br /><br /><br /><a href=\"http://www.sedogo.com\">");
            //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/logo.gif\" />");
            emailBodyCopy.AppendLine("			</a></td>");
            emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"30\"></td></tr><tr><td colspan=\"3\">");
            //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/email-template_05.png\" width=\"692\" height=\"32\" alt=\"\">");
            emailBodyCopy.AppendLine("		</td></tr><tr><td colspan=\"3\"><small>This message was intended for " + loginEmailAddress + ". To stop receiving these emails, go to your profile and uncheck the 'Enable email notifications' option.<br/>Sedogo offices are located at Sedogo Ltd, The Studio, 17 Blossom St, London E1 6PL.</small></td></tr>");
            emailBodyCopy.AppendLine("		</td></tr></table></body></html>");

            message.Subject = "Sedogo password reset";
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
                emailHistory.subject = "Sedogo password reset";
                emailHistory.body = emailBodyCopy.ToString();
                emailHistory.sentFrom = mailFromAddress;
                emailHistory.sentTo = loginEmailAddress;
                emailHistory.Add();
            }
            catch (Exception ex)
            {
                SentEmailHistory emailHistory = new SentEmailHistory("");
                emailHistory.subject = "Sedogo password reset";
                emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString();
                emailHistory.sentFrom = mailFromAddress;
                emailHistory.sentTo = loginEmailAddress;
                emailHistory.Add();
            }

            Session["ResetPassword"] = "Y";
            Response.Redirect("default.aspx");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Reset password", "alert(\"No account with this email address was found.\");", true);
        }
    }
}

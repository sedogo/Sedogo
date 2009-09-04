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
            message.ReplyTo = new MailAddress(mailFromAddress);

            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine("<html><body>");
            emailBody.AppendLine("Your new password\n");
            emailBody.AppendLine(newPassword + "\n");
            emailBody.AppendLine("</body></html>");

            message.Subject = "Sedogo password reset";
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

            Session["ResetPassword"] = "Y";
            Response.Redirect("default.aspx");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Reset password", "alert(\"No account with this email address was found.\");", true);
        }
    }
}

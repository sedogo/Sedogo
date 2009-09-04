//===============================================================
// Filename: register.aspx.cs
// Date: 19/08/09
// --------------------------------------------------------------
// Description:
//   Register user
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 19/08/09
// Revision history:
//===============================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;
using System.Text;
using Sedogo.BusinessObjects;

public partial class register : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetFocus(firstNameTextBox);
        }
    }

    //===============================================================
    // Function: registerUserButton_click
    //===============================================================
    protected void registerUserButton_click(object sender, EventArgs e)
    {
        string emailAddress = emailAddressTextBox.Text;
        emailAddress = emailAddress.Trim().ToLower();
        string userPassword = passwordTextBox1.Text.Trim();

        // Verify this email has not been used before
        int testUserID = SedogoUser.GetUserIDFromEmailAddress(emailAddress);

        if( testUserID > 0 )
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"This email address is already registered, please login, or click on the forgot your password link on the home page.\");", true);
        }
        else
        {
            // Create the user
            SedogoUser newUser = new SedogoUser("");
            newUser.firstName = firstNameTextBox.Text;
            newUser.lastName = lastNameTextBox.Text;
            newUser.emailAddress = emailAddress;
            if( genderMaleRadioButton.Checked == true )
            {
                newUser.gender = "M";
            }
            else
            {
                newUser.gender = "F";
            }
            //homeTownTextBox
            newUser.Add();

            newUser.UpdatePassword(userPassword);

            // Send registration email
            GlobalData gd = new GlobalData((string)Session["loggedInContactName"]);

            string siteBaseURL = gd.GetStringValue("SiteBaseURL");

            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine("<html><body>");
            emailBody.AppendLine("Thanks for registering with Sedogo\n");
            emailBody.AppendLine("Please click on the link below to confirm:\n");
            emailBody.AppendLine("<a href=\"" + siteBaseURL + "/e/?G=" + newUser.GUID + "\">Click here</a>\n");
            emailBody.AppendLine("</body></html>");

            string SMTPServer = gd.GetStringValue("SMTPServer");
            string mailFromAddress = gd.GetStringValue("MailFromAddress");
            string mailFromUsername = gd.GetStringValue("MailFromUsername");
            string mailFromPassword = gd.GetStringValue("MailFromPassword");

            string mailToEmailAddress = emailAddress;

            MailMessage message = new MailMessage(mailFromAddress, mailToEmailAddress);
            message.ReplyTo = new MailAddress(mailFromAddress);

            message.Subject = "Sedogo registration";
            message.Body = emailBody.ToString();
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = SMTPServer;
            if (mailFromPassword != "")
            {
                // If the password is blank, assume mail relay is permitted
                smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
            }
            smtp.Send(message);

            Response.Redirect("registerWait.aspx");
        }
    }
}

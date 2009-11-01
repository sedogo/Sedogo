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
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;
using System.Text;
using System.Globalization;
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
            for( int day = 1 ; day <= 31 ; day++ )
            {
                dateOfBirthDay.Items.Add(new ListItem(day.ToString(),day.ToString()));
            }
            for( int month = 1 ; month <= 12 ; month++ )
            {
                DateTime loopDate = new DateTime(DateTime.Now.Year, month, 1);
                dateOfBirthMonth.Items.Add(new ListItem(loopDate.ToString("MMMM", CultureInfo.InvariantCulture), month.ToString()));
            }
            for (int year = 1900; year <= DateTime.Now.Year ; year++)
            {
                dateOfBirthYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            dateOfBirthDay.Items.Insert(0, new ListItem("", ""));
            dateOfBirthMonth.Items.Insert(0, new ListItem("", ""));
            dateOfBirthYear.Items.Insert(0, new ListItem("", ""));
            hiddenDateOfBirth.Attributes.Add("style", "display:none");
            dateOfBirthDay.Attributes.Add("onchange", "setHiddenDateField()");
            dateOfBirthMonth.Attributes.Add("onchange", "setHiddenDateField()");
            dateOfBirthYear.Attributes.Add("onchange", "setHiddenDateField()");

            dateOfBirthDay.SelectedValue = "";
            dateOfBirthMonth.SelectedValue = "";
            dateOfBirthYear.SelectedValue = "";

            try
            {
                SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

                SqlCommand cmd = new SqlCommand("spSelectTimezoneList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                timezoneDropDownList.DataSource = ds;
                timezoneDropDownList.DataBind();

                timezoneDropDownList.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }

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

            DateTime dateOfBirth;
            if (dateOfBirthYear.SelectedIndex > 0 && dateOfBirthMonth.SelectedIndex > 0
                && dateOfBirthDay.SelectedIndex > 0)
            {
                dateOfBirth = new DateTime(int.Parse(dateOfBirthYear.SelectedValue),
                    int.Parse(dateOfBirthMonth.SelectedValue), int.Parse(dateOfBirthDay.SelectedValue));
            }
            else
            {
                dateOfBirth = DateTime.MinValue;
            }

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
            newUser.homeTown = homeTownTextBox.Text;
            if (dateOfBirth > DateTime.MinValue)
            {
                newUser.birthday = dateOfBirth;
            }
            newUser.timezoneID = int.Parse(timezoneDropDownList.SelectedValue);
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

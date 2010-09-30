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
using Telerik.Web.UI;
using Sedogo.BusinessObjects;
using Newtonsoft.Json.Linq;

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

            if ((string)Application["DateFormat"] == "dmy")
            {
                dateString1.Text = "d + \"/\" + m + \"/\" + y";
            }
            else
            {
                dateString1.Text = "m + \"/\" + d + \"/\" + y";
            }

            if (Request.QueryString["from"] == "facebook")
                FillControlsWithDetailsFromFacebook();

            RadComboBoxItem avatarItem1 = new RadComboBoxItem("Avatar 1", "1");
            avatarItem1.ImageUrl = "/images/avatars/avatar1sm.gif";
            avatarComboBox.Items.Add(avatarItem1);
            RadComboBoxItem avatarItem2 = new RadComboBoxItem("Avatar 2", "2");
            avatarItem2.ImageUrl = "/images/avatars/avatar2sm.gif";
            avatarComboBox.Items.Add(avatarItem2);
            RadComboBoxItem avatarItem3 = new RadComboBoxItem("Avatar 3", "3");
            avatarItem3.ImageUrl = "/images/avatars/avatar3sm.gif";
            avatarComboBox.Items.Add(avatarItem3);
            RadComboBoxItem avatarItem4 = new RadComboBoxItem("Avatar 4", "4");
            avatarItem4.ImageUrl = "/images/avatars/avatar4sm.gif";
            avatarComboBox.Items.Add(avatarItem4);
            RadComboBoxItem avatarItem5 = new RadComboBoxItem("Avatar 5", "5");
            avatarItem5.ImageUrl = "/images/avatars/avatar5sm.gif";
            avatarComboBox.Items.Add(avatarItem5);
            RadComboBoxItem avatarItem6 = new RadComboBoxItem("Avatar 6", "6");
            avatarItem6.ImageUrl = "/images/avatars/avatar6sm.gif";
            avatarComboBox.Items.Add(avatarItem6);

            SetFocus(firstNameTextBox);
        }
    }

    /// <summary>
    /// loads details from facebook
    /// </summary>
    protected void FillControlsWithDetailsFromFacebook()
    {
        try
        {
            JObject fbuser = SedogoUser.GetFacebookUserDetails((string)Session["facebookUserAccessToken"]);
            if (fbuser == null)
                return;

            if(fbuser["first_name"]!=null)
                firstNameTextBox.Text = (string)fbuser["first_name"];
            if(fbuser["last_name"]!=null)
                lastNameTextBox.Text = (string)fbuser["last_name"];
            if (fbuser["email"] != null)
                emailAddressTextBox.Text = (string)fbuser["email"];
            if (fbuser["birthday"] != null)
            {
                try
                {
                    string bdStr = (string)fbuser["birthday"];
                    DateTime dt = DateTime.ParseExact(bdStr, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    ListItem dayItem = dateOfBirthDay.Items.FindByValue(dt.Day.ToString());
                    if (dayItem != null)
                    {
                        dateOfBirthDay.ClearSelection();
                        dayItem.Selected = true;
                    }
                    ListItem monthItem = dateOfBirthMonth.Items.FindByValue(dt.Month.ToString());
                    if(monthItem!=null)
                    {
                        dateOfBirthMonth.ClearSelection();
                        monthItem.Selected = true;
                    }
                    ListItem yearItem = dateOfBirthYear.Items.FindByValue(dt.Year.ToString());
                    if (yearItem != null)
                    {
                        dateOfBirthYear.ClearSelection();
                        yearItem.Selected = true;
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "setDate", "setHiddenDateField();", true);
                }
                catch (Exception ex)
                {

                }
            }
            if (fbuser["hometown"] != null && fbuser["hometown"]["name"]!=null)
                homeTownTextBox.Text = (string)fbuser["hometown"]["name"];
            if (fbuser["gender"] != null)
            {
                string gender = (string)fbuser["gender"];
                switch (gender)
                {
                    case "male":
                        genderMaleRadioButton.Checked = true;
                        genderFemaleRadioButton.Checked = false;
                        break;
                    case "female":
                        genderMaleRadioButton.Checked = false;
                        genderFemaleRadioButton.Checked = true;
                        
                        break;
                    default:
                        break;
                }
            }
            if (fbuser["timezone"] != null)
            {
                int tz = -1;
                if (int.TryParse((string)fbuser["timezone"], out tz))
                {
                    ListItem li = timezoneDropDownList.Items.FindByValue(tz.ToString());
                    if (li != null)
                    {
                        timezoneDropDownList.ClearSelection();
                        li.Selected = true;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Sedogo.BusinessObjects.ErrorLog errorLog = new Sedogo.BusinessObjects.ErrorLog();
            errorLog.WriteLog("Register", "FillControlsWithDetailsFromFacebook", ex.Message,
                Sedogo.BusinessObjects.logMessageLevel.errorMessage);
        }
    }

    //===============================================================
    // Function: registerUserButton_click
    //===============================================================
    protected void registerUserButton_click(object sender, EventArgs e)
    {
        if (registerCaptcha.IsValid == true)
        {
            string emailAddress = emailAddressTextBox.Text;
            emailAddress = emailAddress.Trim().ToLower();
            string userPassword = passwordTextBox1.Text.Trim();

            // Verify this email has not been used before
            int testUserID = SedogoUser.GetUserIDFromEmailAddress(emailAddress);

            if (testUserID > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"This email address is already registered, please log in, or click on the forgot your password link on the home page.\");", true);
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
                if (genderMaleRadioButton.Checked == true)
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
                //Nikita Knyazev. Facebook Authentication. Start
                try
                {
                    if (Session["facebookUserID"] != null)
                        newUser.facebookUserID = (long)Session["facebookUserID"];
                }
                catch (Exception ex)
                {
                }
                //Nikita Knyazev. Facebook Authentication. Finish
                newUser.avatarNumber = int.Parse(avatarComboBox.SelectedValue);
                newUser.Add();

                newUser.UpdatePassword(userPassword);

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
                emailBodyCopy.AppendLine("			<p><a href=\"" + siteBaseURL + "/e/?G=" + newUser.GUID + "\"><u>click here</u></a>.</p>");
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

                string mailToEmailAddress = emailAddress;

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
                    emailHistory.sentTo = emailAddress;
                    emailHistory.Add();
                }
                catch (Exception ex)
                {
                    SentEmailHistory emailHistory = new SentEmailHistory("");
                    emailHistory.subject = "Sedogo registration";
                    emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString();
                    emailHistory.sentFrom = mailFromAddress;
                    emailHistory.sentTo = emailAddress;
                    emailHistory.Add();
                }

                Response.Redirect("registerWait.aspx");
            }
        }
    }
}

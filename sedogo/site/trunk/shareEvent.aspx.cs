//===============================================================
// Filename: shareEvent.aspx.cs
// Date: 26/05/10
// --------------------------------------------------------------
// Description:
//   Share goal
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 26/05/10
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
using System.Text;
using System.Globalization;
using System.Net.Mail;
using Sedogo.BusinessObjects;

public partial class shareEvent : System.Web.UI.Page     // Cannot be a SedogoPage because this would not allow anonymous users
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);

            SedogoEvent sedogoEvent = new SedogoEvent("", eventID);
            SedogoUser eventOwner = new SedogoUser("", sedogoEvent.userID);

            int userID = -1;
            if (Session["loggedInUserID"] != null)
            {
                userID = int.Parse(Session["loggedInUserID"].ToString());
            }
            sidebarControl.userID = userID;
            string currentUserFullName = "";
            if (userID > 0)
            {
                SedogoUser user = new SedogoUser("", userID);
                sidebarControl.user = user;
                bannerAddFindControl.userID = userID;
                currentUserFullName = user.firstName + " " + user.lastName;

                addressBookLink1.Visible = true;
                addressBookLink2.Visible = true;
                addressBookLink3.Visible = true;
                addressBookLink4.Visible = true;
                addressBookLink5.Visible = true;
            }
            else
            {
                addressBookLink1.Visible = false;
                addressBookLink2.Visible = false;
                addressBookLink3.Visible = false;
                addressBookLink4.Visible = false;
                addressBookLink5.Visible = false;
            }

            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventOwner, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);

            additionalInviteTextTextBox.Text = currentUserFullName + " thought you might be interested in this.";

            eventTitleLabel.Text = sedogoEvent.eventName;
            eventOwnersNameLabel.Text = eventOwner.firstName + " " + eventOwner.lastName;
            eventDateLabel.Text = dateString;
            eventDescriptionLabel.Text = sedogoEvent.eventDescription.Replace("\n", "<br/>");
            eventNameLabel.Text = sedogoEvent.eventName;
            goalNameLabel.Text = sedogoEvent.eventName;
            goalVenueLabel.Text = sedogoEvent.eventVenue;
            editedDateLabel.Text = sedogoEvent.lastUpdatedDate.ToString("dd MMMM yyyy");

            string timelineColour = "#cd3301";
            switch (sedogoEvent.categoryID)
            {
                case 1:
                    timelineColour = "#cd3301";
                    break;
                case 2:
                    timelineColour = "#ff0b0b";
                    break;
                case 3:
                    timelineColour = "#ff6801";
                    break;
                case 4:
                    timelineColour = "#ff8500";
                    break;
                case 5:
                    timelineColour = "#d5b21a";
                    break;
                case 6:
                    timelineColour = "#8dc406";
                    break;
                case 7:
                    timelineColour = "#5b980c";
                    break;
                case 8:
                    timelineColour = "#079abc";
                    break;
                case 9:
                    timelineColour = "#5ab6cd";
                    break;
                case 10:
                    timelineColour = "#8a67c1";
                    break;
                case 11:
                    timelineColour = "#e54ecf";
                    break;
                case 12:
                    timelineColour = "#a5369c";
                    break;
                case 13:
                    timelineColour = "#a32672";
                    break;
            }
            pageBannerBarDiv.Style.Add("background-color", timelineColour);
        }
    }

    //===============================================================
    // Function: sendInvitesLink_click
    //===============================================================
    protected void sendInvitesLink_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        StringBuilder invalidEmailAddresses = new StringBuilder();
        string errorMessage = "";
        StringBuilder successfullyInvitedEmailAddresses = new StringBuilder();
        Boolean allInvitesSentOK = true;

        string invite1EmailAddress = inviteTextBox1.Text;
        string invite2EmailAddress = inviteTextBox2.Text;
        string invite3EmailAddress = inviteTextBox3.Text;
        string invite4EmailAddress = inviteTextBox4.Text;
        string invite5EmailAddress = inviteTextBox5.Text;

        string additionalInviteText = additionalInviteTextTextBox.Text;

        if (invite1EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite1EmailAddress, additionalInviteText, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite1EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite1EmailAddress);
            }
        }
        if (invite2EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite2EmailAddress, additionalInviteText, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite2EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite2EmailAddress);
            }
        }
        if (invite3EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite3EmailAddress, additionalInviteText, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite3EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite3EmailAddress);
            }
        }
        if (invite4EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite4EmailAddress, additionalInviteText, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite4EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite4EmailAddress);
            }
        }
        if (invite5EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite5EmailAddress, additionalInviteText, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite5EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite5EmailAddress);
            }
        }

        if (allInvitesSentOK == true)
        {
            Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert",
                "alert(\"Links to the following addresses could not be sent: "
                + invalidEmailAddresses.ToString().Replace("\n", ", ").Replace("\r", "") + "\");", true);

            inviteTextBox1.Text = "";
            inviteTextBox2.Text = "";
            inviteTextBox3.Text = "";
            inviteTextBox4.Text = "";
            inviteTextBox5.Text = "";
        }
    }

    //===============================================================
    // Function: SendInviteEmail
    //===============================================================
    private Boolean SendInviteEmail(int eventID, string emailAddress, string additionalInviteText,
        out string errorMessageDescription)
    {
        Boolean sentOK = false;
        GlobalData gd = new GlobalData("");
        errorMessageDescription = "";

        if (MiscUtils.IsValidEmailAddress(emailAddress) == true)
        {
            int userID = -1;
            if (Session["loggedInUserID"] != null)
            {
                userID = int.Parse(Session["loggedInUserID"].ToString());
            }

            string currentUserFullName = "";
            SedogoEvent sedogoEvent = new SedogoEvent("", eventID);
            if (userID > 0)
            {
                SedogoUser currentUser = new SedogoUser("", userID);
                currentUserFullName = currentUser.firstName + " " + currentUser.lastName;
            }
            SedogoUser eventOwner = new SedogoUser("", sedogoEvent.userID);

            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventOwner, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);

            StringBuilder emailBodyCopy = new StringBuilder();

            string eventURL = gd.GetStringValue("SiteBaseURL");
            eventURL = eventURL + "?EID=" + eventID.ToString();

            emailBodyCopy.AppendLine("<html>");
            emailBodyCopy.AppendLine("<head><title></title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">");
            emailBodyCopy.AppendLine("<style type=\"text/css\">");
            emailBodyCopy.AppendLine("	body, td, p { font-size: 15px; color: #9B9885; font-family: Arial, Helvetica, Sans-Serif }");
            emailBodyCopy.AppendLine("	p { margin: 0 }");
            emailBodyCopy.AppendLine("	h1 { color: #00ccff; font-size: 18px; font-weight: bold; }");
            emailBodyCopy.AppendLine("	a, .blue { color: #00ccff; text-decoration: none; }");
            emailBodyCopy.AppendLine("</style></head>");
            emailBodyCopy.AppendLine("<body bgcolor=\"#f0f1ec\">");
            emailBodyCopy.AppendLine("  <table width=\"692\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            //emailBodyCopy.AppendLine("	<tr><td colspan=\"3\"><img src=\"http://www.sedogo.com/email-template/images/email-template_01.png\" width=\"692\" height=\"32\" alt=\"\"></td></tr>");
            emailBodyCopy.AppendLine("	<tr><td style=\"background: #fff\" width=\"30\"></td>");
            emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"632\">");
            //emailBodyCopy.AppendLine("			<h1>sedogo.com message</h1>");
            emailBodyCopy.AppendLine("			<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td valign=\"top\">Goal:</td>");
            emailBodyCopy.AppendLine("					<td><a href=\"" + eventURL + "\">" + sedogoEvent.eventName + "</a></td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td valign=\"top\">When:</td>");
            emailBodyCopy.AppendLine("					<td>" + dateString + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td valign=\"top\">Where:</td>");
            emailBodyCopy.AppendLine("					<td>" + sedogoEvent.eventVenue + "</td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("				<tr>");
            emailBodyCopy.AppendLine("					<td valign=\"top\">Message:</td>");
            emailBodyCopy.AppendLine("					<td><p style=\"color:black\">" + additionalInviteText.Replace("\n", "<br/>") + "</p></td>");
            emailBodyCopy.AppendLine("				</tr>");
            emailBodyCopy.AppendLine("			</table>");
            emailBodyCopy.AppendLine("			<br /><br />");
            emailBodyCopy.AppendLine("			<p>Regards</p><a href=\"http://www.sedogo.com\" class=\"blue\"><strong>The Sedogo Team.</strong></a><br />");
            emailBodyCopy.AppendLine("			<br /><br /><br /><a href=\"http://www.sedogo.com\">");
            //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/logo.gif\" />");
            emailBodyCopy.AppendLine("			</a></td>");
            emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"30\"></td></tr><tr><td colspan=\"3\">");
            //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/email-template_05.png\" width=\"692\" height=\"32\" alt=\"\">");
            emailBodyCopy.AppendLine("		</td></tr><tr><td colspan=\"3\"><small>This message was intended for " + emailAddress + ". To stop receiving these emails, go to your profile and uncheck the 'Enable email notifications' option.<br/>Sedogo offices are located at Sedogo Ltd, The Studio, 17 Blossom St, London E1 6PL.</small></td></tr>");
            emailBodyCopy.AppendLine("		</td></tr></table></body></html>");

            string emailSubject = currentUserFullName + " wants to tell you about " + sedogoEvent.eventName;

            string SMTPServer = gd.GetStringValue("SMTPServer");
            string mailFromAddress = gd.GetStringValue("MailFromAddress");
            string mailFromUsername = gd.GetStringValue("MailFromUsername");
            string mailFromPassword = gd.GetStringValue("MailFromPassword");

            sentOK = true;

            try
            {
                MailMessage mailMessage = new MailMessage(mailFromAddress, emailAddress);
                mailMessage.ReplyTo = new MailAddress("noreply@sedogo.com");

                mailMessage.Subject = emailSubject;
                mailMessage.Body = emailBodyCopy.ToString();
                mailMessage.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = SMTPServer;
                if (mailFromPassword != "")
                {
                    // If the password is blank, assume mail relay is permitted
                    smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
                }
                smtp.Send(mailMessage);

                SentEmailHistory emailHistory = new SentEmailHistory("");
                emailHistory.subject = emailSubject;
                emailHistory.body = emailBodyCopy.ToString();
                emailHistory.sentFrom = mailFromAddress;
                emailHistory.sentTo = emailAddress;
                emailHistory.Add();
            }
            catch (Exception ex)
            {
                sentOK = false;
                errorMessageDescription = emailAddress + " - invalid email address";

                SentEmailHistory emailHistory = new SentEmailHistory("");
                emailHistory.subject = emailSubject;
                emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString();
                emailHistory.sentFrom = mailFromAddress;
                emailHistory.sentTo = emailAddress;
                emailHistory.Add();
            }
        }
        else
        {
            errorMessageDescription = emailAddress + " - invalid email address";
        }

        return sentOK;
    }

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }
}

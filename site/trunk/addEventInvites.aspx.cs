//===============================================================
// Filename: addEventInvites.aspx
// Date: 21/11/09
// --------------------------------------------------------------
// Description:
//   Add event invites
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 21/11/09
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
using System.IO;
using Sedogo.BusinessObjects;

public partial class addEventInvites : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

        eventNameLabel.Text = sedogoEvent.eventName;
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
        string invite6EmailAddress = inviteTextBox6.Text;
        string invite7EmailAddress = inviteTextBox7.Text;
        string invite8EmailAddress = inviteTextBox8.Text;
        string invite9EmailAddress = inviteTextBox9.Text;
        string invite10EmailAddress = inviteTextBox10.Text;
        string invite11EmailAddress = inviteTextBox11.Text;
        string invite12EmailAddress = inviteTextBox12.Text;
        string invite13EmailAddress = inviteTextBox13.Text;
        string invite14EmailAddress = inviteTextBox14.Text;
        string invite15EmailAddress = inviteTextBox15.Text;
        string invite16EmailAddress = inviteTextBox16.Text;
        string invite17EmailAddress = inviteTextBox17.Text;
        string invite18EmailAddress = inviteTextBox18.Text;
        string invite19EmailAddress = inviteTextBox19.Text;
        string invite20EmailAddress = inviteTextBox20.Text;

        string additionalInviteText = additionalInviteTextTextBox.Text;

        int currentUserID = int.Parse(Session["loggedInUserID"].ToString());
        SedogoUser currentUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), currentUserID);
        SedogoEvent currentEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
        string dateString = "";
        if (currentEvent.dateType == "D")
        {
            // Date
            dateString = "on " + currentEvent.startDate.ToString("ddd d MMMM yyyy");
        }
        if (currentEvent.dateType == "R")
        {
            // Range
            dateString = "between " + currentEvent.rangeStartDate.ToString("ddd d MMMM yyyy");
            dateString += "and " + currentEvent.rangeEndDate.ToString("ddd d MMMM yyyy");
        }
        if (currentEvent.dateType == "A")
        {
            // Birthday
            dateString = "before ";
            if (currentUser.gender == "M")
            {
                dateString += "his ";
            }
            else
            {
                dateString += "her ";
            }
            string dateSuffix = "";
            switch (currentEvent.beforeBirthday)
            {
                case 1: case 21: case 31: case 41: case 51: case 61: case 71: case 81: case 91: case 101:
                    dateSuffix = "st";
                    break;
                case 2: case 22: case 32: case 42: case 52: case 62: case 72: case 82: case 92: case 102:
                    dateSuffix = "nd";
                    break;
                case 3: case 23: case 33: case 43: case 53: case 63: case 73: case 83: case 93: case 103:
                    dateSuffix = "rd";
                    break;
                case 4: case 5: case 6: case 7: case 8: case 9: case 10:
                case 11: case 12: case 13: case 14: case 15: case 16: case 17: case 18: case 19: case 20:
                case 24: case 25: case 26: case 27: case 28: case 29: case 30:
                case 34: case 35: case 36: case 37: case 38: case 39: case 40:
                case 44: case 45: case 46: case 47: case 48: case 49: case 50:
                case 54: case 55: case 56: case 57: case 58: case 59: case 60:
                case 64: case 65: case 66: case 67: case 68: case 69: case 70:
                case 74: case 75: case 76: case 77: case 78: case 79: case 80:
                case 84: case 85: case 86: case 87: case 88: case 89: case 90:
                case 94: case 95: case 96: case 97: case 98: case 99: case 100:
                case 104: case 105: case 106: case 107: case 108: case 109: case 110:
                    dateSuffix = "th";
                    break;
                default:
                    break;
            }
            dateString += currentEvent.beforeBirthday + dateSuffix + " birthday";
        }

        if (invite1EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite1EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
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
            if (SendInviteEmail(eventID, invite2EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
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
            if (SendInviteEmail(eventID, invite3EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
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
            if (SendInviteEmail(eventID, invite4EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
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
            if (SendInviteEmail(eventID, invite5EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite5EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite5EmailAddress);
            }
        }
        if (invite6EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite6EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite6EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite6EmailAddress);
            }
        }
        if (invite7EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite7EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite7EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite7EmailAddress);
            }
        }
        if (invite8EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite8EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite8EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite8EmailAddress);
            }
        }
        if (invite9EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite9EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite9EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite9EmailAddress);
            }
        }
        if (invite10EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite10EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite10EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite10EmailAddress);
            }
        }
        if (invite11EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite11EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite11EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite11EmailAddress);
            }
        }
        if (invite12EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite12EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite12EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite12EmailAddress);
            }
        }
        if (invite13EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite13EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite13EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite13EmailAddress);
            }
        }
        if (invite14EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite14EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite14EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite14EmailAddress);
            }
        }
        if (invite15EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite15EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite15EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite15EmailAddress);
            }
        }
        if (invite16EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite16EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite16EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite16EmailAddress);
            }
        }
        if (invite17EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite17EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite17EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite17EmailAddress);
            }
        }
        if (invite18EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite18EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite18EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite18EmailAddress);
            }
        }
        if (invite19EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite19EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite19EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite19EmailAddress);
            }
        }
        if (invite20EmailAddress.Trim() != "")
        {
            if (SendInviteEmail(eventID, invite20EmailAddress, additionalInviteText, dateString,
                currentEvent, currentUser, out errorMessage) == false)
            {
                allInvitesSentOK = false;
                invalidEmailAddresses.AppendLine(invite20EmailAddress + ": " + errorMessage);
            }
            else
            {
                successfullyInvitedEmailAddresses.AppendLine(invite20EmailAddress);
            }
        }


        if (allInvitesSentOK == true)
        {
            Response.Redirect("addEventSummary.aspx?EID=" + eventID.ToString());
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", 
                "alert(\"Invitations to the following addresses could not be sent: "
                + invalidEmailAddresses.ToString().Replace("\n", ", ").Replace("\r", "") + "\");", true);

            inviteTextBox1.Text = "";
            inviteTextBox2.Text = "";
            inviteTextBox3.Text = "";
            inviteTextBox4.Text = "";
            inviteTextBox5.Text = "";
            inviteTextBox6.Text = "";
            inviteTextBox7.Text = "";
            inviteTextBox8.Text = "";
            inviteTextBox9.Text = "";
            inviteTextBox10.Text = "";
            inviteTextBox11.Text = "";
            inviteTextBox12.Text = "";
            inviteTextBox13.Text = "";
            inviteTextBox14.Text = "";
            inviteTextBox15.Text = "";
            inviteTextBox16.Text = "";
            inviteTextBox17.Text = "";
            inviteTextBox18.Text = "";
            inviteTextBox19.Text = "";
            inviteTextBox20.Text = "";
        }
    }

    //===============================================================
    // Function: SendInviteEmail
    //===============================================================
    private Boolean SendInviteEmail(int eventID, string emailAddress, string additionalInviteText,
        string dateString, SedogoEvent currentEvent, SedogoUser currentUser,
        out string errorMessageDescription)
    {
        Boolean sentOK = false;
        GlobalData gd = new GlobalData("");
        errorMessageDescription = "";

        if (MiscUtils.IsValidEmailAddress(emailAddress) == true)
        {
            try
            {
                // Check if they have already been invited
                int inviteCount = EventInvite.GetInviteCountForEmailAddress(eventID, emailAddress);
                if (inviteCount == 0)
                {
                    StringBuilder emailBodyCopy = new StringBuilder();

                    // Check if they are a Sedogo account holder
                    int sedogoUserID = SedogoUser.GetUserIDFromEmailAddress(emailAddress);
                    Boolean enableSendEmails = true;

                    if( sedogoUserID != currentUser.userID )
                    {
                        EventInvite newInvite = new EventInvite(Session["loggedInUserFullName"].ToString());
                        newInvite.eventID = eventID;
                        newInvite.emailAddress = emailAddress;
                        newInvite.inviteAdditionalText = additionalInviteText;
                        newInvite.userID = sedogoUserID;
                        newInvite.Add();

                        string inviteURL = gd.GetStringValue("SiteBaseURL");
                        inviteURL = inviteURL + "?EIG=" + newInvite.eventInviteGUID;
                        string eventURL = gd.GetStringValue("SiteBaseURL");
                        eventURL = eventURL + "/viewEvent.aspx?EID=" + currentEvent.eventID.ToString();

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
                        if (sedogoUserID > 0)
                        {
                            SedogoUser inviteUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), sedogoUserID);
                            if (inviteUser.enableSendEmails == false)
                            {
                                enableSendEmails = false;
                            }

                            inviteURL = inviteURL + "&UID=" + sedogoUserID.ToString();

                            emailBodyCopy.AppendLine("			<h1>You are invited to join the following goal:</h1>");
                            emailBodyCopy.AppendLine("			<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">");
                            emailBodyCopy.AppendLine("				<tr>");
                            emailBodyCopy.AppendLine("					<td width=\"60\">What:</td>");
                            emailBodyCopy.AppendLine("					<td width=\"10\">&nbsp;</td>");
                            emailBodyCopy.AppendLine("					<td width=\"530\">" + currentEvent.eventName + "</td>");
                            emailBodyCopy.AppendLine("				</tr>");
                            emailBodyCopy.AppendLine("				<tr>");
                            emailBodyCopy.AppendLine("					<td valign=\"top\">Where:</td>");
                            emailBodyCopy.AppendLine("					<td width=\"10\">&nbsp;</td>");
                            emailBodyCopy.AppendLine("					<td>" + currentEvent.eventVenue + "</td>");
                            emailBodyCopy.AppendLine("				</tr>");
                            emailBodyCopy.AppendLine("				<tr>");
                            emailBodyCopy.AppendLine("					<td valign=\"top\">When:</td>");
                            emailBodyCopy.AppendLine("					<td width=\"10\">&nbsp;</td>");
                            emailBodyCopy.AppendLine("					<td>" + dateString + "</td>");
                            emailBodyCopy.AppendLine("				</tr>");
                            emailBodyCopy.AppendLine("			</table>");
                            emailBodyCopy.AppendLine("			<p><span class=\"blue\">" + currentUser.firstName + "</span> has created this future goal on <a href=\"http://www.sedogo.com\">sedogo.com</a> and wants you to join in.</p>");
                            emailBodyCopy.AppendLine("			<p>To be part of this event, <a href=\"" + inviteURL + "\"><u>click here</u></a>.</p>");
                            emailBodyCopy.AppendLine("			<p>To see who else is part of making this goal happen, <a href=\"" + eventURL + "\"><u>click here</u></a>.</p>");
                            emailBodyCopy.AppendLine("			<p>To view this goal, <a href=\"" + eventURL + "\"><u>click here</u></a>.</p>");
                        }
                        else
                        {
                            emailBodyCopy.AppendLine("			<h1>You are invited to join the following goal:</h1>");
                            emailBodyCopy.AppendLine("			<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">");
                            emailBodyCopy.AppendLine("				<tr>");
                            emailBodyCopy.AppendLine("					<td width=\"60\">What:</td>");
                            emailBodyCopy.AppendLine("					<td width=\"10\">&nbsp;</td>");
                            emailBodyCopy.AppendLine("					<td width=\"530\">" + currentEvent.eventName + "</td>");
                            emailBodyCopy.AppendLine("				</tr>");
                            emailBodyCopy.AppendLine("				<tr>");
                            emailBodyCopy.AppendLine("					<td valign=\"top\">Where:</td>");
                            emailBodyCopy.AppendLine("					<td width=\"10\">&nbsp;</td>");
                            emailBodyCopy.AppendLine("					<td>" + currentEvent.eventVenue + "</td>");
                            emailBodyCopy.AppendLine("				</tr>");
                            emailBodyCopy.AppendLine("				<tr>");
                            emailBodyCopy.AppendLine("					<td valign=\"top\">When:</td>");
                            emailBodyCopy.AppendLine("					<td width=\"10\">&nbsp;</td>");
                            emailBodyCopy.AppendLine("					<td>" + dateString + "</td>");
                            emailBodyCopy.AppendLine("				</tr>");
                            emailBodyCopy.AppendLine("			</table>");
                            emailBodyCopy.AppendLine("			<p><span class=\"blue\">" + currentUser.firstName + "</span> has created this future goal on <a href=\"http://www.sedogo.com\">sedogo.com</a> and wants you to join in.</p>");
                            emailBodyCopy.AppendLine("			<p>To be part of this event, <a href=\"" + inviteURL + "\">sign up</a> for a free sedogo account now.");
                            emailBodyCopy.AppendLine("			<p>When you have completed the registration process, <a href=\"" + eventURL + "\">click here</a> to view this event.");
                        }
                        emailBodyCopy.AppendLine("			<br /><br />");
                        emailBodyCopy.AppendLine("			<p>Regards</p><a href=\"http://www.sedogo.com\" class=\"blue\"><strong>The Sedogo Team.</strong></a><br />");
                        emailBodyCopy.AppendLine("			<br /><br /><br /><a href=\"http://www.sedogo.com\">");
                        //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/logo.gif\" />");
                        emailBodyCopy.AppendLine("			</a></td>");
                        emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"30\"></td></tr><tr><td colspan=\"3\">");
                        //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/email-template_05.png\" width=\"692\" height=\"32\" alt=\"\">");
                        emailBodyCopy.AppendLine("		</td></tr><tr><td colspan=\"3\"><small>This message was intended for " + emailAddress + ". To stop receiving these emails, go to your profile and uncheck the 'Enable email notifications' option.<br/>Sedogo offices are located at Sedogo Ltd, The Studio, 17 Blossom St, London E1 6PL.</small></td></tr>");
                        emailBodyCopy.AppendLine("		</td></tr></table></body></html>");

                        string emailSubject = currentUser.firstName + " wants you to be a part of " + currentEvent.eventName + " " + dateString + "!";

                        string SMTPServer = gd.GetStringValue("SMTPServer");
                        string mailFromAddress = gd.GetStringValue("MailFromAddress");
                        string mailFromUsername = gd.GetStringValue("MailFromUsername");
                        string mailFromPassword = gd.GetStringValue("MailFromPassword");

                        if (enableSendEmails == true)
                        {
                            try
                            {
                                MailMessage message = new MailMessage(mailFromAddress, emailAddress);
                                message.ReplyTo = new MailAddress("noreply@sedogo.com");

                                message.Subject = emailSubject;
                                message.Body = emailBodyCopy.ToString();
                                message.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = SMTPServer;
                                if (mailFromPassword != "")
                                {
                                    // If the password is blank, assume mail relay is permitted
                                    smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
                                }
                                smtp.Send(message);

                                newInvite.inviteEmailSent = true;
                                newInvite.inviteEmailSentDate = DateTime.Now;
                                newInvite.inviteEmailSentEmailAddress = emailAddress;
                                newInvite.Update();

                                SentEmailHistory emailHistory = new SentEmailHistory(Session["loggedInUserFullName"].ToString());
                                emailHistory.subject = emailSubject;
                                emailHistory.body = emailBodyCopy.ToString();
                                emailHistory.sentFrom = mailFromAddress;
                                emailHistory.sentTo = emailAddress;
                                emailHistory.Add();
                            }
                            catch (Exception ex)
                            {
                                SentEmailHistory emailHistory = new SentEmailHistory(Session["loggedInUserFullName"].ToString());
                                emailHistory.subject = emailSubject;
                                emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString();
                                emailHistory.sentFrom = mailFromAddress;
                                emailHistory.sentTo = emailAddress;
                                emailHistory.Add();
                            }
                        }
                        sentOK = true;
                    }
                    else
                    {
                        errorMessageDescription = emailAddress + " - you cannot invite yourself";
                    }
                }
                else
                {
                    errorMessageDescription = emailAddress + " - already invited";
                }
            }
            catch (Exception ex)
            {
                errorMessageDescription = emailAddress + " - " + ex.Message;
            }
        }
        else
        {
            errorMessageDescription = emailAddress + " - invalid email address";
        }

        return sentOK;
    }

    //===============================================================
    // Function: click_skipInvitesLink
    //===============================================================
    protected void click_skipInvitesLink(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("addEventSummary.aspx?EID=" + eventID.ToString());
    }
}

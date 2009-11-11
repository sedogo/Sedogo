﻿//===============================================================
// Filename: eventInvites.aspx.cs
// Date: 21/10/09
// --------------------------------------------------------------
// Description:
//   Event invitations
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 21/10/09
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
using System.Net.Mail;
using Sedogo.BusinessObjects;

public partial class eventInvites : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);
            string action = "";
            int eventInviteID = -1;
            if (Request.QueryString["A"] != null && Request.QueryString["EIID"] != null)
            {
                action = Request.QueryString["A"].ToString();
                eventInviteID = int.Parse(Request.QueryString["EIID"]);

                if (action == "Delete")
                {
                    try
                    {
                        EventInvite inviteToDelete = new EventInvite(Session["loggedInUserFullName"].ToString(), eventInviteID);
                        inviteToDelete.Delete();
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert",
                            "alert(\"Error: " + ex.Message + "\");", true);
                    }
                }
                if (action == "InviteAgain")
                {
                    try
                    {
                        EventInvite repeatInvite = new EventInvite(Session["loggedInUserFullName"].ToString(), eventInviteID);
                        repeatInvite.inviteDeclined = false;
                        repeatInvite.inviteDeclinedDate = DateTime.MinValue;
                        repeatInvite.Update();
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert",
                            "alert(\"Error: " + ex.Message + "\");", true);
                    }
                }
            }

            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
            SedogoUser eventOwner = new SedogoUser("", sedogoEvent.userID);

            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventOwner, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);

            eventTitleLabel.Text = sedogoEvent.eventName;
            eventOwnersNameLabel.Text = eventOwner.firstName + " " + eventOwner.lastName;
            eventDateLabel.Text = dateString;
            eventDescriptionLabel.Text = sedogoEvent.eventDescription.Replace("\n", "<br/>");

            PopulateInvitations(eventID);
        }
    }

    //===============================================================
    // Function: PopulateInvitations
    //===============================================================
    private void PopulateInvitations(int eventID)
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectEventInvitesList";
            cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
            DbDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows == true)
            {
                while (rdr.Read())
                {
                    int eventInviteID = int.Parse(rdr["EventInviteID"].ToString());
                    string emailAddress = (string)rdr["EmailAddress"];
                    Boolean inviteEmailSent = (Boolean)rdr["InviteEmailSent"];
                    //string InviteEmailSentEmailAddress = (string)rdr["InviteEmailSentEmailAddress"];
                    //DateTime InviteEmailSentDate = (DateTime)rdr["InviteEmailSentDate"];
                    Boolean inviteAccepted = (Boolean)rdr["InviteAccepted"];
                    //DateTime inviteAcceptedDate = (DateTime)rdr["InviteAcceptedDate"];
                    //DateTime createdDate = (DateTime)rdr["CreatedDate"];
                    Boolean inviteDeclined = (Boolean)rdr["InviteDeclined"];

                    string outputText = "<h3>" + emailAddress + "</h3><p>";
                    if( inviteEmailSent == false )
                    {
                        outputText = outputText + "<i>Unable to send email!</i><br/>";
                    }
                    if (inviteAccepted == false && inviteDeclined == false)
                    {
                        outputText = outputText + "<i>Pending</i>";
                        outputText = outputText + " (<a href=\"eventInvites.aspx?EID=" + eventID.ToString()
                            + "&A=Delete&EIID=" + eventInviteID.ToString() + "\">Delete</a>)";
                    }
                    else
                    {
                        if (inviteAccepted == true)
                        {
                            outputText = outputText + "<i>Accepted</i>";
                        }
                        if (inviteDeclined == true)
                        {
                            outputText = outputText + "<i>Declined</i>";
                            outputText = outputText + " (<a href=\"eventInvites.aspx?EID=" + eventID.ToString()
                                + "&A=InviteAgain&EIID=" + eventInviteID.ToString() + "\">Invite again</a>)";
                        }
                    }
                    outputText = outputText + "</p>";

                    currentInvitesPlaceholder.Controls.Add(new LiteralControl(outputText));
                }
            }
            else
            {
                string outputText = "<p>&nbsp;<br/>There are no invitations for this event</p>";

                currentInvitesPlaceholder.Controls.Add(new LiteralControl(outputText));
            }
            rdr.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }
    }

    //===============================================================
    // Function: sendInvitesLink_click
    //===============================================================
    protected void sendInvitesLink_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        StringBuilder invalidEmailAddresses = new StringBuilder();
        StringBuilder errorMessageDescription = new StringBuilder();
        string errorMessage = "";
        StringBuilder successfullyInvitedEmailAddresses = new StringBuilder();
        Boolean allInvitesSentOK = true;

        string invite1EmailAddress = inviteTextBox1.Text;
        string invite2EmailAddress = inviteTextBox2.Text;
        string invite3EmailAddress = inviteTextBox3.Text;
        string invite4EmailAddress = inviteTextBox4.Text;
        string invite5EmailAddress = inviteTextBox5.Text;

        string additionalInviteText = additionalInviteTextTextBox.Text;

        int currentUserID = int.Parse(Session["loggedInUserID"].ToString());
        SedogoUser currentUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), currentUserID);
        SedogoEvent currentEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
        string dateString = "";
        if (currentEvent.dateType == "D")
        {
            // Date
            dateString = "on " + currentEvent.startDate.ToString("dd/MM/yyyy");
        }
        if (currentEvent.dateType == "R")
        {
            // Range
            dateString = "between " + currentEvent.rangeStartDate.ToString("dd/MM/yyyy");
            dateString = "and " + currentEvent.rangeEndDate.ToString("dd/MM/yyyy");
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
                invalidEmailAddresses.AppendLine(invite1EmailAddress);
                errorMessageDescription.AppendLine(errorMessage);
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
                invalidEmailAddresses.AppendLine(invite2EmailAddress);
                errorMessageDescription.AppendLine(errorMessage);
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
                invalidEmailAddresses.AppendLine(invite3EmailAddress);
                errorMessageDescription.AppendLine(errorMessage);
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
                invalidEmailAddresses.AppendLine(invite4EmailAddress);
                errorMessageDescription.AppendLine(errorMessage);
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
                invalidEmailAddresses.AppendLine(invite5EmailAddress);
                errorMessageDescription.AppendLine(errorMessage);
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
                "alert(\"Invitations to the following addresses could not be sent: "
                + invalidEmailAddresses.ToString().Replace("\n", ", ").Replace("\r", "") + "\");", true);
            //newUserInvitesTextBox.Text = invalidEmailAddresses.ToString();

            inviteTextBox1.Text = "";
            inviteTextBox2.Text = "";
            inviteTextBox3.Text = "";
            inviteTextBox4.Text = "";
            inviteTextBox5.Text = "";

            PopulateInvitations(eventID);
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

                    EventInvite newInvite = new EventInvite(Session["loggedInUserFullName"].ToString());
                    newInvite.eventID = eventID;
                    newInvite.emailAddress = emailAddress;
                    newInvite.inviteAdditionalText = additionalInviteText;
                    newInvite.userID = sedogoUserID;
                    newInvite.Add();

                    string inviteURL = gd.GetStringValue("SiteBaseURL");
                    inviteURL = inviteURL + "?EIG=" + newInvite.eventInviteGUID;

                    if (sedogoUserID > 0)
                    {
                        inviteURL = inviteURL + "&UID=" + sedogoUserID.ToString();

                        emailBodyCopy.AppendLine("What: " + currentEvent.eventName + "<br/>");
                        emailBodyCopy.AppendLine("Where: <venue>" + "<br/>");
                        emailBodyCopy.AppendLine("When: " + dateString + "<br/>");
                        emailBodyCopy.AppendLine("Join in<br/>");
                        emailBodyCopy.AppendLine("<Megan> has created this future goal on sedogo.com and wants you to join in.<br/>");
                        emailBodyCopy.AppendLine("To be part of this event, <a href=\"" + inviteURL + "\">click here</a>.<br/>");
                        emailBodyCopy.AppendLine("To see who else is part of making this goal happen, <a href=\"" + inviteURL + "\">click here</a>.<br/>");
                        emailBodyCopy.AppendLine("Regards,<br/>");
                        emailBodyCopy.AppendLine("The Sedogo Team<br/>");
                        emailBodyCopy.AppendLine("<img src=\"http://sedogo.websites.bta.com/images/sedogo.gif\" /><br/>");
                        emailBodyCopy.AppendLine("Sedogo helps you create future goals and connection with others to make them happen.<br/>");
                    }
                    else
                    {
                        emailBodyCopy.AppendLine("What: " + currentEvent.eventName + "<br/>");
                        emailBodyCopy.AppendLine("Where: <venue>" + "<br/>");
                        emailBodyCopy.AppendLine("When: " + dateString + "<br/>");
                        emailBodyCopy.AppendLine("Join in<br/>");
                        emailBodyCopy.AppendLine("<Megan> has created this future goal on sedogo.com and wants you to join in.<br/>");
                        emailBodyCopy.AppendLine("To be part of this event, <a href=\"" + inviteURL + "\">sign up</a> for a free sedogo account now.<br/>");
                        emailBodyCopy.AppendLine("Regards,<br/>");
                        emailBodyCopy.AppendLine("The Sedogo Team<br/>");
                        emailBodyCopy.AppendLine("<img src=\"http://sedogo.websites.bta.com/images/sedogo.gif\" /><br/>");
                        emailBodyCopy.AppendLine("Sedogo helps you create future goals and connection with others to make them happen.<br/>");
                    }

                    string emailSubject = currentUser.firstName + " wants you to be a part of " + currentEvent.eventName + " " + dateString + "!";

                    string SMTPServer = gd.GetStringValue("SMTPServer");
                    string mailFromAddress = gd.GetStringValue("MailFromAddress");
                    string mailFromUsername = gd.GetStringValue("MailFromUsername");
                    string mailFromPassword = gd.GetStringValue("MailFromPassword");

                    MailMessage message = new MailMessage(mailFromAddress, emailAddress);
                    message.ReplyTo = new MailAddress(mailFromAddress);

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

                    sentOK = true;
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
    // Function: click_backToEventDetailsLink
    //===============================================================
    protected void click_backToEventDetailsLink(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }
}
﻿//===============================================================
// Filename: viewEvent.aspx.cs
// Date: 14/09/09
// --------------------------------------------------------------
// Description:
//   View event
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 08/09/09
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
using Sedogo.BusinessObjects;

public partial class viewEvent : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);
            int userID = int.Parse(Session["loggedInUserID"].ToString());

            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

            if( sedogoEvent.userID != userID )
            {
                // Viewing someone elses event
                messagesHeader.Visible = false;
                messagesLink.Visible = false;
                invitesHeader.Visible = false;
                invitesLink.Visible = false;
                alertsHeader.Visible = false;
                alertsLink.Visible = false;
                //trackingHeader.Visible = false;
                //trackingLinksPlaceholder.Visible = false;

                editEventLink.Visible = false;
                achievedEventLink.Visible = false;
                uploadEventImage.Visible = false;

                if (TrackedEvent.GetTrackedEventID(eventID, userID) < 0)
                {
                    trackThisEventLink.Visible = true;
                }
                else
                {
                    // Event is already being tracked
                    trackThisEventLink.Visible = false;
                }
                createSimilarEventLink.Visible = true;
            }
            else
            {
                // Viewing own event
                messagesHeader.Visible = true;
                messagesLink.Visible = true;
                invitesHeader.Visible = true;
                invitesLink.Visible = true;
                alertsHeader.Visible = true;
                alertsLink.Visible = true;
                //trackingHeader.Visible = true;
                //trackingLinksPlaceholder.Visible = true;

                editEventLink.Visible = true;
                achievedEventLink.Visible = true;
                uploadEventImage.Visible = true;
                createSimilarEventLink.Visible = false;

                int messageCount = Message.GetMessageCountForEvent(eventID);
                int inviteCount = EventInvite.GetInviteCount(eventID);
                int alertsCount = EventAlert.GetEventAlertCountPending(eventID);

                if (messageCount == 1)
                {
                    messagesLink.Text = "You have " + messageCount.ToString() + " new message";
                }
                else
                {
                    messagesLink.Text = "You have " + messageCount.ToString() + " new messages";
                }
                if (inviteCount == 1)
                {
                    invitesLink.Text = "you have " + inviteCount.ToString() + " invitation";
                }
                else
                {
                    invitesLink.Text = "you have " + inviteCount.ToString() + " invitations";
                }
                if (alertsCount == 1)
                {
                    alertsLink.Text = "you have " + alertsCount.ToString() + " alert";
                }
                else
                {
                    alertsLink.Text = "you have " + alertsCount.ToString() + " alerts";
                }

                trackThisEventLink.Visible = false;
            }
            PopulateTrackingList(eventID);

            SedogoUser eventOwner = new SedogoUser("", sedogoEvent.userID);
            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventOwner, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);
            
            eventTitleLabel.Text = sedogoEvent.eventName;
            eventOwnersNameLabel.Text = eventOwner.firstName + " " + eventOwner.lastName;
            eventDateLabel.Text = dateString;
            eventDescriptionLabel.Text = sedogoEvent.eventDescription.Replace("\n", "<br/>");
            eventVenueLabel.Text = sedogoEvent.eventVenue.Replace("\n", "<br/>");

            if (sedogoEvent.eventPicPreview == "")
            {
                eventImage.ImageUrl = "~/images/eventImageBlank.png";
            }
            else
            {
                eventImage.ImageUrl = "~/assets/eventPics/" + sedogoEvent.eventPicPreview;
            }

            editEventLink.NavigateUrl = "editEvent.aspx?EID=" + eventID.ToString();

            if (sedogoEvent.eventAchieved == true)
            {
                achievedEventLink.Text = "re-open";
            }
            else
            {
                achievedEventLink.Text = "achieved";
            }

            if (userID == sedogoEvent.userID)
            {
                sendMessageButton.Visible = false;
            }
            else
            {
                sendMessageButton.Visible = true;
            }

            PopulateComments(eventID);
        }
    }

    //===============================================================
    // Function: PopulateComments
    //===============================================================
    private void PopulateComments(int eventID)
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectEventCommentsList";
            cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int eventCommentID = int.Parse(rdr["EventCommentID"].ToString());
                int postedByUserID = int.Parse(rdr["PostedByUserID"].ToString());
                string commentText = (string)rdr["CommentText"];
                DateTime createdDate = (DateTime)rdr["CreatedDate"];
                string createdByFullName = (string)rdr["CreatedByFullName"];
                string firstName = (string)rdr["FirstName"];
                string lastName = (string)rdr["LastName"];
                string emailAddress = (string)rdr["EmailAddress"];

                commentText = Server.HtmlEncode(commentText);
                string postedByUsername = firstName + " " + lastName;

                string outputText = "<h3>" + createdDate.ToString("dd/MM/yyyy") + "</h3><p>"
                    + commentText.Replace("\n", "<br/>")
                    + "<br/><i>Posted by: " + postedByUsername + "</i></p>";

                commentsPlaceHolder.Controls.Add(new LiteralControl(outputText));
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
    // Function: PopulateTrackingList
    //===============================================================
    private void PopulateTrackingList(int eventID)
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectTrackingUsersByEventID";
            cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string profilePicThumbnail = "";

                //int trackedEventID = int.Parse(rdr["TrackedEventID"].ToString());
                //int userID = int.Parse(rdr["UserID"].ToString());
                string firstName = (string)rdr["FirstName"];
                string lastName = (string)rdr["LastName"];
                //string gender = (string)rdr["Gender"];
                //string homeTown = (string)rdr["HomeTown"];
                //string emailAddress = (string)rdr["EmailAddress"];
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicThumbnail")))
                {
                    profilePicThumbnail = (string)rdr["ProfilePicThumbnail"];
                }
                //string profilePicPreview = (string)rdr["ProfilePicPreview"];

                string profileImagePath = "./images/profile/blankProfile.jpg";
                if (profilePicThumbnail != "")
                {
                    profileImagePath = "./assets/profilePics/" + profilePicThumbnail;
                }

                string outputText = "<p><img src=\"" + profileImagePath + "\" />" 
                    + firstName + " " + lastName + "<p>";

                trackingLinksPlaceholder.Controls.Add(new LiteralControl(outputText));
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
    // Function: click_achievedEventLink
    //===============================================================
    protected void click_achievedEventLink(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
        sedogoEvent.eventAchieved = !sedogoEvent.eventAchieved;
        sedogoEvent.Update();

        Response.Redirect("profileRedirect.aspx");
    }

    //===============================================================
    // Function: click_uploadEventImage
    //===============================================================
    protected void click_uploadEventImage(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("uploadEventImage.aspx?EID=" + eventID.ToString());
    }

    //===============================================================
    // Function: sendMessageButton_click
    //===============================================================
    protected void sendMessageButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("sendMessage.aspx?EID=" + eventID.ToString());
    }

    //===============================================================
    // Function: postCommentButton_click
    //===============================================================
    protected void postCommentButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("postComment.aspx?EID=" + eventID.ToString());
    }

    //===============================================================
    // Function: trackThisEventLink_click
    //===============================================================
    protected void trackThisEventLink_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        int userID = int.Parse(Session["loggedInUserID"].ToString());

        TrackedEvent trackedEvent = new TrackedEvent(Session["loggedInUserFullName"].ToString());
        trackedEvent.eventID = eventID;
        trackedEvent.userID = userID;
        trackedEvent.Add();

        trackThisEventLink.Visible = false;

        PopulateTrackingList(eventID);
    }

    //===============================================================
    // Function: click_inviteUsersLink
    //===============================================================
    protected void click_inviteUsersLink(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("eventInvites.aspx?EID=" + eventID.ToString());
    }

    //===============================================================
    // Function: click_alertsLink
    //===============================================================
    protected void click_alertsLink(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("eventAlerts.aspx?EID=" + eventID.ToString());
    }

    //===============================================================
    // Function: createSimilarEventLink_click
    //===============================================================
    protected void createSimilarEventLink_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        int userID = int.Parse(Session["loggedInUserID"].ToString());

        SedogoEvent viewedEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
        SedogoEvent newEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString());
        newEvent.beforeBirthday = 1;
        newEvent.categoryID = viewedEvent.categoryID;
        newEvent.dateType = viewedEvent.dateType;
        newEvent.eventDescription = viewedEvent.eventDescription;
        newEvent.eventVenue = viewedEvent.eventVenue;
        newEvent.eventName = viewedEvent.eventName;
        newEvent.mustDo = viewedEvent.mustDo;
        newEvent.rangeEndDate = viewedEvent.rangeEndDate;
        newEvent.rangeStartDate = viewedEvent.rangeStartDate;
        newEvent.startDate = viewedEvent.startDate;
        newEvent.userID = userID;
        newEvent.createdFromEventID = viewedEvent.eventID;
        newEvent.Add();

        Response.Redirect("viewEvent.aspx?EID=" + newEvent.eventID.ToString());
    }

    //===============================================================
    // Function: click_messageTrackingUsersLink
    //===============================================================
    protected void click_messageTrackingUsersLink(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("sendMessageToTrackers.aspx?EID=" + eventID.ToString());
    }
}

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

public partial class viewEvent : System.Web.UI.Page     // Cannot be a SedogoPage because this would not allow anonymous users
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);
            int userID = -1;
            string loggedInUserName = "";
            if (Session["loggedInUserID"] != null)
            {
                userID = int.Parse(Session["loggedInUserID"].ToString());
                loggedInUserName = Session["loggedInUserFullName"].ToString();
            }
            string action = "";
            if( Request.QueryString["A"] != null )
            {
                action = (string)Request.QueryString["A"];
            }

            if (action == "RemoveTracker")
            {
                int trackedEventID = int.Parse(Request.QueryString["TEID"].ToString());

                TrackedEvent trackedEvent = new TrackedEvent(loggedInUserName, trackedEventID);
                trackedEvent.Delete();
            }
            if (action == "AcceptRequest")
            {
                int trackedEventID = int.Parse(Request.QueryString["TEID"].ToString());

                TrackedEvent trackedEvent = new TrackedEvent(loggedInUserName, trackedEventID);
                trackedEvent.joinPending = false;
                trackedEvent.Update();
            }
            if (action == "RejectRequest")
            {
                int trackedEventID = int.Parse(Request.QueryString["TEID"].ToString());

                TrackedEvent trackedEvent = new TrackedEvent(loggedInUserName, trackedEventID);
                trackedEvent.Delete();
            }

            SedogoEvent sedogoEvent = new SedogoEvent(loggedInUserName, eventID);
            eventLabel1.Text = sedogoEvent.eventName;
            eventLabel2.Text = sedogoEvent.eventName;

            if (userID > 0)
            {
                int trackedEventID = TrackedEvent.GetTrackedEventID(eventID, userID);
                if (sedogoEvent.userID != userID)
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
                    deleteEventButton.Visible = false;
                    messageTrackingUsersLink.Visible = false;

                    editEventLink.Visible = false;
                    achievedEventLink.Visible = false;
                    uploadEventImage.Visible = false;
                    sendMessageButton.Visible = true;
                    sendMessageDiv.Visible = true;

                    if (trackedEventID < 0)
                    {
                        trackThisEventLink.Visible = true;
                        joinThisEventLink.Visible = true;
                    }
                    else
                    {
                        // Event is already being tracked
                        trackThisEventLink.Visible = false;

                        TrackedEvent trackedEvent = new TrackedEvent(loggedInUserName, trackedEventID);
                        if (trackedEvent.showOnTimeline == true)
                        {
                            if (trackedEvent.joinPending == false)
                            {
                                joinThisEventLink.Visible = false;
                            }
                            else
                            {
                                joinThisEventLink.Visible = false;
                            }
                        }
                        else
                        {
                            joinThisEventLink.Visible = true;
                        }
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
                    deleteEventButton.Visible = true;
                    deleteEventButton.Attributes.Add("onclick", "if(confirm('Are you sure you want to delete this event?')){}else{return false}");
                    sendMessageButton.Visible = false;

                    editEventLink.Visible = true;
                    achievedEventLink.Visible = true;
                    uploadEventImage.Visible = true;
                    createSimilarEventLink.Visible = false;
                    sendMessageDiv.Visible = false;

                    int messageCount = Message.GetMessageCountForEvent(eventID);
                    int inviteCount = EventInvite.GetInviteCount(eventID);
                    //int alertsCount = EventAlert.GetEventAlertCountPending(eventID);

                    if (messageCount == 1)
                    {
                        messagesLink.Text = "You have " + messageCount.ToString() + " new message";
                    }
                    else
                    {
                        messagesLink.Text = "You have " + messageCount.ToString() + " new messages";
                    }
                    inviteCountLabel.Text = inviteCount.ToString() + " pending";

                    trackThisEventLink.Visible = false;
                    joinThisEventLink.Visible = false;  // You cannot join your own event
                }
                PopulateTrackingList(eventID);
                PopulateAlertsList(eventID);
                PopulateRequestsList(eventID);

                loginRegisterPanel.Visible = false;

                // See if current user has been invited to this goal
                if (trackedEventID < 0)
                {
                    int eventInviteCount = EventInvite.CheckUserEventInviteExists(eventID, userID);
                    if (eventInviteCount > 0)
                    {
                        invitedPanel.Visible = true;
                    }
                    else
                    {
                        invitedPanel.Visible = false;
                    }
                }
                else
                {
                    invitedPanel.Visible = false;
                }
            }
            else
            {
                // Setup the window for a user who is not registered/logged in

                messagesHeader.Visible = false;
                messagesLink.Visible = false;
                invitesHeader.Visible = false;
                invitesLink.Visible = false;
                alertsHeader.Visible = false;
                alertsLink.Visible = false;
                //trackingHeader.Visible = true;
                //trackingLinksPlaceholder.Visible = true;
                deleteEventButton.Visible = false;

                editEventLink.Visible = false;
                achievedEventLink.Visible = false;
                uploadEventImage.Visible = false;
                createSimilarEventLink.Visible = false;

                trackThisEventLink.Visible = false;
                joinThisEventLink.Visible = false;
                invitedPanel.Visible = false;
                sendMessageDiv.Visible = false;

                messageTrackingUsersLink.Visible = false;
                sendMessageButton.Visible = false;
                postCommentButton.Visible = false;
                eventOwnersNameLabel.Enabled = false;
                eventOwnersNameLabel.NavigateUrl = "#";

                loginRegisterPanel.Visible = true;
            }

            SedogoUser eventOwner = new SedogoUser("", sedogoEvent.userID);
            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventOwner, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);
            
            eventTitleLabel.Text = sedogoEvent.eventName;
            eventOwnersNameLabel.Text = eventOwner.firstName + " " + eventOwner.lastName;
            if (sedogoEvent.userID != userID)
            {
                eventOwnersNameLabel.NavigateUrl = "userTimeline.aspx?UID=" + eventOwner.userID.ToString();
            }
            else
            {
                // If this is your own event then no link
                eventOwnersNameLabel.Enabled = false;
            }
            eventDateLabel.Text = dateString;
            eventDescriptionLabel.Text = sedogoEvent.eventDescription.Replace("\n", "<br/>");
            eventVenueLabel.Text = sedogoEvent.eventVenue.Replace("\n", "<br/>");
            //createdDateLabel.Text = sedogoEvent.createdDate.ToString("dd/MM/yyyy");
            lastUpdatedDateLabel.Text = sedogoEvent.lastUpdatedDate.ToString("ddd d MMMM yyyy");

            if (sedogoEvent.eventPicPreview == "")
            {
                eventImage.ImageUrl = "~/images/eventImageBlank.png";
                uploadEventImage.Text = "Add picture";
            }
            else
            {
                eventImage.ImageUrl = "~/assets/eventPics/" + sedogoEvent.eventPicPreview;
                uploadEventImage.Text = "Edit picture";
            }

            editEventLink.NavigateUrl = "editEvent.aspx?EID=" + eventID.ToString();

            if (sedogoEvent.eventAchieved == true)
            {
                achievedEventLink.Text = "Re-open";
            }
            else
            {
                achievedEventLink.Text = "Achieved";
            }

            sendMessageButton.Attributes.Add("style", "text-decoration: underline; display: block; background: url(images/ico_messages.gif) no-repeat left; padding-left: 20px; margin: 4px 0 20px 0");

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
                int postedByUserID = -1;
                string commentText = "";
                DateTime createdDate = DateTime.MinValue;
                string createdByFullName = "";
                string firstName = "";
                string lastName = "";
                string emailAddress = "";
                string profilePicThumbnail = "";

                if (!rdr.IsDBNull(rdr.GetOrdinal("CommentText")))
                {
                    commentText = (string)rdr["CommentText"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedDate")))
                {
                    createdDate = (DateTime)rdr["CreatedDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedByFullName")))
                {
                    createdByFullName = (string)rdr["CreatedByFullName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("FirstName")))
                {
                    firstName = (string)rdr["FirstName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LastName")))
                {
                    lastName = (string)rdr["LastName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EmailAddress")))
                {
                    emailAddress = (string)rdr["EmailAddress"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicThumbnail")))
                {
                    profilePicThumbnail = (string)rdr["ProfilePicThumbnail"];
                }

                commentText = Server.HtmlEncode(commentText);
                string postedByUsername = firstName + " " + lastName;
                string profileImage = "./images/profile/blankProfile.jpg";
                if (profilePicThumbnail != "")
                {
                    profileImage = "./assets/profilePics/" + profilePicThumbnail;
                }

                string outputText = "<p><img src=\"" + profileImage + "\" width=\"17\" style=\"margin-right:4px\" />&nbsp;" + postedByUsername + "</h3><p>"
                    + createdDate.ToString("ddd d MMMM yyyy") + "<br/>"
                    + commentText.Replace("\n", "<br/>") + "</p>";

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
        int loggedInUserID = int.Parse(Session["loggedInUserID"].ToString());

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

        int trackingUserCount = 0;
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

                int trackedEventID = int.Parse(rdr["TrackedEventID"].ToString());
                int userID = int.Parse(rdr["UserID"].ToString());
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
                Boolean showOnTimeline = (Boolean)rdr["ShowOnTimeline"];
                Boolean joinPending = (Boolean)rdr["JoinPending"];

                if (joinPending == false)
                {
                    string profileImagePath = "./images/profile/blankProfile.jpg";
                    if (profilePicThumbnail != "")
                    {
                        profileImagePath = "./assets/profilePics/" + profilePicThumbnail;
                    }

                    //string outputText = "<table><tr><td><img src=\"" + profileImagePath + "\" /></td>"
                    //    + "<td valign=\"bottom\">"
                    //    + "<a href=\"userTimeline.aspx?UID=" + userID.ToString() + "\" target=\"_top\">"
                    //    + firstName + " " + lastName + "</a>";
                    string outputText = "<p><a href=\"userTimeline.aspx?UID=" + userID.ToString() + "\" target=\"_top\">"
                        + "<img src=\"" + profileImagePath + "\" width=\"17\" style=\"margin-right:4px\" />"
                        + firstName + " " + lastName + "</a> ";
                    if (loggedInUserID == sedogoEvent.userID)
                    {
                        // This is my event!
                        outputText = outputText + " <a href=\"viewEvent.aspx?A=RemoveTracker&EID="
                            + eventID.ToString()
                            + "&TEID=" + trackedEventID.ToString() + "\">"
                            + "(Remove)</a> ";
                    } 
                    else if (loggedInUserID == userID)
                    {
                        // I am the tracker
                        outputText = outputText + " <a href=\"viewEvent.aspx?A=RemoveTracker&EID="
                            + eventID.ToString()
                            + "&TEID=" + trackedEventID.ToString() + "\">"
                            + "(Remove)</a> ";
                    }
                    //outputText = outputText + "</td></tr></table>";
                    outputText = outputText + "</p>";

                    if (showOnTimeline == true)
                    {
                        trackingLinksPlaceholder.Controls.Add(new LiteralControl(outputText));
                    }
                    else
                    {
                        followersLinksPlaceholder.Controls.Add(new LiteralControl(outputText));
                    }

                    trackingUserCount++;
                }
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

        if (trackingUserCount == 0)
        {
            messageTrackingUsersLink.Visible = false;
        }
    }

    //===============================================================
    // Function: PopulateRequestsList
    //===============================================================
    private void PopulateRequestsList(int eventID)
    {
        int loggedInUserID = int.Parse(Session["loggedInUserID"].ToString());

        //SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

        int pendingRequestCount = 0;
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectPendingMemberRequestsByEventID";
            cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string profilePicThumbnail = "";

                int trackedEventID = int.Parse(rdr["TrackedEventID"].ToString());
                int userID = int.Parse(rdr["UserID"].ToString());
                string firstName = (string)rdr["FirstName"];
                string lastName = (string)rdr["LastName"];
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicThumbnail")))
                {
                    profilePicThumbnail = (string)rdr["ProfilePicThumbnail"];
                }

                string profileImagePath = "./images/profile/blankProfile.jpg";
                if (profilePicThumbnail != "")
                {
                    profileImagePath = "./assets/profilePics/" + profilePicThumbnail;
                }

                string outputText = "<p>"
                    + "<img src=\"" + profileImagePath + "\" width=\"17\" style=\"margin-right:4px\" />"
                    + firstName + " " + lastName + "</a> ";

                outputText = outputText + " <a href=\"viewEvent.aspx?A=AcceptRequest&EID="
                    + eventID.ToString()
                    + "&TEID=" + trackedEventID.ToString() + "\">"
                    + "(Accept)</a> ";
                outputText = outputText + " <a href=\"viewEvent.aspx?A=RejectRequest&EID="
                    + eventID.ToString()
                    + "&TEID=" + trackedEventID.ToString() + "\">"
                    + "(Reject)</a> ";
                outputText = outputText + "</p>";

                requestsLinksPlaceholder.Controls.Add(new LiteralControl(outputText));

                pendingRequestCount++;
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

        //if (trackingUserCount == 0)
        //{
        //    messageTrackingUsersLink.Visible = false;
        //}
    }

    //===============================================================
    // Function: PopulateAlertsList
    //===============================================================
    private void PopulateAlertsList(int eventID)
    {
        int loggedInUserID = int.Parse(Session["loggedInUserID"].ToString());

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectEventAlertListPending";      //spSelectEventAlertList
            cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int eventAlertID = int.Parse(rdr["EventAlertID"].ToString());
                DateTime alertDate = (DateTime)rdr["AlertDate"];
                string alertText = (string)rdr["AlertText"];

                string outputText = "<a href=\"eventAlerts.aspx?EID=" + eventID.ToString() + ">"
                    + alertDate.ToString("ddd d MMMM yyyy") + "</a> ";

                alertsPlaceHolder.Controls.Add(new LiteralControl(outputText));
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

        sedogoEvent.SendEventUpdateEmail();

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

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
        sedogoEvent.SendEventUpdateEmail();

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

    //===============================================================
    // Function: joinThisEventLink_click
    //===============================================================
    protected void joinThisEventLink_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        int userID = int.Parse(Session["loggedInUserID"].ToString());

        int trackedEventID = TrackedEvent.GetTrackedEventID(eventID, userID);

        if (trackedEventID > 0)
        {
            TrackedEvent trackedEvent = new TrackedEvent(Session["loggedInUserFullName"].ToString(), trackedEventID);
            trackedEvent.showOnTimeline = true;
            trackedEvent.Update();
        }
        else
        {
            TrackedEvent trackedEvent = new TrackedEvent(Session["loggedInUserFullName"].ToString());
            trackedEvent.eventID = eventID;
            trackedEvent.userID = userID;
            trackedEvent.showOnTimeline = true;
            trackedEvent.joinPending = true;
            trackedEvent.Add();

            trackedEvent.SendJoinRequestEmail();

            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
            sedogoEvent.SendEventUpdateEmail();
        }

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }

    //===============================================================
    // Function: deleteEventButton_click
    //===============================================================
    protected void deleteEventButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        int userID = int.Parse(Session["loggedInUserID"].ToString());

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
        sedogoEvent.Delete();

        sedogoEvent.SendEventUpdateEmail();

        Response.Redirect("profileRedirect.aspx");
    }

    //===============================================================
    // Function: invitedButton_click
    //===============================================================
    protected void invitedButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        int userID = int.Parse(Session["loggedInUserID"].ToString());

        int eventInviteID = EventInvite.GetEventInviteIDFromUserIDEventID(eventID, userID);

        // Check if the user is already tracking this event
        if (TrackedEvent.GetTrackedEventID(eventID, userID) < 0)
        {
            TrackedEvent trackedEvent = new TrackedEvent(Session["loggedInUserFullName"].ToString());
            trackedEvent.eventID = eventID;
            trackedEvent.userID = userID;
            trackedEvent.joinPending = false;
            trackedEvent.showOnTimeline = true;
            trackedEvent.Add();
        }

        EventInvite eventInvite = new EventInvite(Session["loggedInUserFullName"].ToString(), eventInviteID);

        eventInvite.inviteAccepted = true;
        eventInvite.inviteAcceptedDate = DateTime.Now;
        eventInvite.Update();

        Response.Redirect("viewEvent.aspx?EID=" + eventInvite.eventID.ToString());
    }
}

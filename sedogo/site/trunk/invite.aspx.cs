//===============================================================
// Filename: invite.aspx.cs
// Date: 23/10/09
// --------------------------------------------------------------
// Description:
//   View invite
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 23/10/09
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

public partial class invite : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Session["loggedInUserID"].ToString());

            sidebarControl.userID = userID;
            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
            sidebarControl.user = user;
            bannerAddFindControl.userID = userID;

            PopulateInviteList(userID);
        }
    }

    //===============================================================
    // Function: PopulateInviteList
    //===============================================================
    private void PopulateInviteList(int userID)
    {
        int pendingInviteCount = EventInvite.GetPendingInviteCountForUser(userID);

        if (pendingInviteCount > 0)
        {
            noInvitesDiv.Visible = false;
            invitesDiv.Visible = true;
        }
        else
        {
            noInvitesDiv.Visible = true;
            invitesDiv.Visible = false;
        }

        try
        {
            SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

            SedogoUser sedogoUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);

            SqlCommand cmd = new SqlCommand("spSelectPendingInviteListForUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 200).Value = sedogoUser.emailAddress;
            cmd.CommandTimeout = 90;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            invitesRepeater.DataSource = ds;
            invitesRepeater.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //===============================================================
    // Function: invitesRepeater_ItemDataBound
    //===============================================================
    protected void invitesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null &&
            (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
        {
            DataRowView row = e.Item.DataItem as DataRowView;

            int eventID = int.Parse(row["EventID"].ToString());
            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

            SedogoUser eventOwner = new SedogoUser("", sedogoEvent.userID);
            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventOwner, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);

            HyperLink eventNameLabel = e.Item.FindControl("eventNameLabel") as HyperLink;
            eventNameLabel.NavigateUrl = "viewEvent.aspx?EID=" + row["EventID"].ToString();
            eventNameLabel.Text = row["EventName"].ToString();

            //HyperLink eventHyperlink = e.Item.FindControl("eventHyperlink") as HyperLink;
            //eventHyperlink.NavigateUrl = "viewEvent.aspx?EID=" + row["EventID"].ToString();

            HyperLink eventDateLabel = e.Item.FindControl("eventDateLabel") as HyperLink;
            eventDateLabel.NavigateUrl = "viewEvent.aspx?EID=" + row["EventID"].ToString();
            eventDateLabel.Text = dateString;

            HyperLink userNameLabel = e.Item.FindControl("userNameLabel") as HyperLink;
            userNameLabel.NavigateUrl = "userTimeline.aspx?UID=" + sedogoEvent.userID.ToString();
            userNameLabel.Text = row["FirstName"].ToString() + " " + row["LastName"].ToString();

            Image eventPicThumbnailImage = e.Item.FindControl("eventPicThumbnailImage") as Image;
            string eventPicThumbnail = row["eventPicThumbnail"].ToString();
            if (eventPicThumbnail == "")
            {
                eventPicThumbnailImage.ImageUrl = "./images/eventThumbnailBlank.png";
            }
            else
            {
                var _event = new SedogoEvent(string.Empty, eventID);
                eventPicThumbnailImage.ImageUrl = ResolveUrl(ImageHelper.GetRelativeImagePath(_event.eventID, _event.eventGUID, ImageType.EventThumbnail));
            }
        }
    }

    //===============================================================
    // Function: invitesRepeater_ItemCommand
    //===============================================================
    protected void invitesRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int eventInviteID = int.Parse(e.CommandArgument.ToString());

        EventInvite eventInvite = new EventInvite(Session["loggedInUserFullName"].ToString(), eventInviteID);

        if (e.CommandName == "acceptButton")
        {
            int currentUserID = int.Parse(Session["loggedInUserID"].ToString());
            // Check if the user is already tracking this event
            if (TrackedEvent.GetTrackedEventID(eventInvite.eventID, currentUserID) < 0)
            {
                TrackedEvent trackedEvent = new TrackedEvent(Session["loggedInUserFullName"].ToString());
                trackedEvent.eventID = eventInvite.eventID;
                trackedEvent.userID = currentUserID;
                trackedEvent.showOnTimeline = true;
                trackedEvent.Add();
            }

            eventInvite.inviteAccepted = true;
            eventInvite.inviteAcceptedDate = DateTime.Now;
            eventInvite.Update();

            eventInvite.SendInviteAcceptedEmail();

            Response.Redirect("viewEvent.aspx?EID=" + eventInvite.eventID.ToString());
        }
        if (e.CommandName == "declineButton")
        {
            eventInvite.inviteDeclined = true;
            eventInvite.inviteDeclinedDate = DateTime.Now;
            eventInvite.Update();
        }

        int userID = int.Parse(Session["loggedInUserID"].ToString());
        PopulateInviteList(userID);
    }

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        Response.Redirect("profile.aspx");
    }
}

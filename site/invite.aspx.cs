﻿//===============================================================
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

            //SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
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

            SqlCommand cmd = new SqlCommand("spSelectPendingInviteListForUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

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

            Literal eventNameLabel = e.Item.FindControl("eventNameLabel") as Literal;
            eventNameLabel.Text = row["EventName"].ToString();

            Literal userNameLabel = e.Item.FindControl("userNameLabel") as Literal;
            userNameLabel.Text = row["FirstName"].ToString() + " " + row["LastName"].ToString();

            Image eventPicThumbnailImage = e.Item.FindControl("eventPicThumbnailImage") as Image;
            string eventPicThumbnail = row["eventPicThumbnail"].ToString();
            if (eventPicThumbnail == "")
            {
                eventPicThumbnailImage.ImageUrl = "./images/eventThumbnailBlank.png";
            }
            else
            {
                eventPicThumbnailImage.ImageUrl = "./assets/eventPics/" + eventPicThumbnail;
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
                trackedEvent.Add();
            }

            eventInvite.inviteAccepted = true;
            eventInvite.inviteAcceptedDate = DateTime.Now;
            eventInvite.Update();
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
}

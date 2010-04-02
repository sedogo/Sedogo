﻿//===============================================================
// Filename: sidebar.aspx.cs
// Date: 22/03/10
// --------------------------------------------------------------
// Description:
//   Sidebar
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 22/03/10
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
using Sedogo.BusinessObjects;

public partial class sidebar : System.Web.UI.UserControl
{
    public Boolean      viewArchivedEvents = false;
    public int          userID = -1;
    public SedogoUser   user;

    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           userNameLabel.Text = user.fullName;

           profileTextLabel.Text = user.profileText.Replace("\n", "<br/>");

           if (user.profilePicThumbnail != "")
           {
               profileImage.ImageUrl = "~/assets/profilePics/" + user.profilePicThumbnail;
           }
           else
           {
               profileImage.ImageUrl = "~/images/profile/blankProfile.jpg";
           }
           profileImage.ToolTip = user.fullName + "'s profile picture";

           int messageCount = Message.GetUnreadMessageCountForUser(userID);
           if (messageCount == 1)
           {
               messageCountLink.Text = "<span>" + messageCount.ToString() + "</span> Message";
           }
           else
           {
               messageCountLink.Text = "<span>" + messageCount.ToString() + "</span> Messages";
           }
           int pendingInviteCount = EventInvite.GetPendingInviteCountForUser(userID);
           if (pendingInviteCount == 1)
           {
               inviteCountLink.Text = "<span>" + pendingInviteCount.ToString() + "</span> Invite";
           }
           else
           {
               inviteCountLink.Text = "<span>" + pendingInviteCount.ToString() + "</span> Invites";
           }

           int pendingAlertCount = EventAlert.GetEventAlertCountPendingByUser(userID);
           if (pendingAlertCount == 1)
           {
               alertCountLink.Text = "<span>" + pendingAlertCount.ToString() + "</span> Reminder";
           }
           else
           {
               alertCountLink.Text = "<span>" + pendingAlertCount.ToString() + "</span> Reminders";
           }

           int trackedEventCount = TrackedEvent.GetTrackedEventCount(userID);
           if (trackedEventCount == 1)
           {
               trackingCountLink.Text = "<span>" + trackedEventCount.ToString() + "</span> Goal Followed";
           }
           else
           {
               trackingCountLink.Text = "<span>" + trackedEventCount.ToString() + "</span> Goals Followed";
           }
           int pendingRequestsCount = SedogoEvent.GetPendingMemberUserCountByUserID(userID);
           if (pendingRequestsCount == 1)
           {
               goalJoinRequestsLink.Text = "<span>" + pendingRequestsCount.ToString() + "</span> Request";
           }
           else
           {
               goalJoinRequestsLink.Text = "<span>" + pendingRequestsCount.ToString() + "</span> Requests";
           }
           int joinedEventCount = TrackedEvent.GetJoinedEventCount(userID);
           if (joinedEventCount == 1)
           {
               groupGoalsLink.Text = "<span>" + joinedEventCount.ToString() + "</span> Group goal";
           }
           else
           {
               groupGoalsLink.Text = "<span>" + joinedEventCount.ToString() + "</span> Group goals";
           }
           addressBookLink.Text = "Address book";

           if (viewArchivedEvents == true)
           {
               viewArchiveLink.Text = "Hide Past Goals";
           }
           else
           {
               viewArchiveLink.Text = "Past Goals";
           }

           PopulateLatestSearches();
        }
    }

    //===============================================================
    // Function: PopulateLatestSearches
    //===============================================================
    private void PopulateLatestSearches()
    {
        int userID = int.Parse(Session["loggedInUserID"].ToString());

        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectSearchHistoryTop5";
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string searchText = "";

                int searchHistoryID = int.Parse(rdr["SearchHistoryID"].ToString());
                if (!rdr.IsDBNull(rdr.GetOrdinal("SearchText")))
                {
                    searchText = (string)rdr["SearchText"];
                }

                HyperLink searchHyperlink = new HyperLink();
                searchHyperlink.Text = searchText;
                searchHyperlink.NavigateUrl = "~/search2.aspx?Search=" + searchText;
                latestSearchesPlaceholder.Controls.Add(searchHyperlink);

                latestSearchesPlaceholder.Controls.Add(new LiteralControl("<br/>"));
            }
            rdr.Close();

            SqlCommand cmdPopular = new SqlCommand("", conn);
            cmdPopular.CommandType = CommandType.StoredProcedure;
            cmdPopular.CommandText = "spSelectSearchHistoryPopularTop5";
            DbDataReader rdrPopular = cmdPopular.ExecuteReader();
            while (rdrPopular.Read())
            {
                string searchText = (string)rdrPopular["SearchText"];
                //int searchCount = int.Parse(rdrPopular["SearchCount"].ToString());

                HyperLink searchHyperlink = new HyperLink();
                searchHyperlink.Text = searchText;
                searchHyperlink.NavigateUrl = "~/search2.aspx?Search=" + searchText;
                popularSearchesPlaceholder.Controls.Add(searchHyperlink);

                popularSearchesPlaceholder.Controls.Add(new LiteralControl("<br/>"));
            }
            rdrPopular.Close();

            SqlCommand cmdLatestEvents = new SqlCommand("", conn);
            cmdLatestEvents.CommandType = CommandType.StoredProcedure;
            cmdLatestEvents.CommandText = "spSelectLatestEvents";
            cmdLatestEvents.Parameters.Add("@LoggedInUserID", SqlDbType.Int).Value = userID;
            DbDataReader rdrLatestEvents = cmdLatestEvents.ExecuteReader();
            while (rdrLatestEvents.Read())
            {
                int eventID = int.Parse(rdrLatestEvents["EventID"].ToString());
                string eventName = (string)rdrLatestEvents["EventName"];

                HyperLink eventHyperlink = new HyperLink();
                eventHyperlink.Text = eventName;
                eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
                latestEventsPlaceholder.Controls.Add(eventHyperlink);

                latestEventsPlaceholder.Controls.Add(new LiteralControl("<br/>"));
            }
            rdrLatestEvents.Close();
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
    // Function: click_viewArchiveLink
    //===============================================================
    protected void click_viewArchiveLink(object sender, EventArgs e)
    {
        //Boolean viewArchivedEvents = false;
        if (Session["ViewArchivedEvents"] != null)
        {
            Session["ViewArchivedEvents"] = !(Boolean)Session["ViewArchivedEvents"];
        }
        else
        {
            Session["ViewArchivedEvents"] = true;
        }

        Response.Redirect(Request.Url.AbsolutePath);
    }
}

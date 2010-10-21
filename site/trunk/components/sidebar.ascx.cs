//===============================================================
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
using System.Collections.Generic;
using System.Linq;
using Sedogo.DAL;
using EventAlert = Sedogo.BusinessObjects.EventAlert;
using EventInvite = Sedogo.BusinessObjects.EventInvite;
using Message = Sedogo.BusinessObjects.Message;
using TrackedEvent = Sedogo.BusinessObjects.TrackedEvent;


public partial class sidebar : System.Web.UI.UserControl
{
    public Boolean viewArchivedEvents = false;
    public int userID = -1;
    public SedogoUser user;
    
    /// <summary>
    /// Gets or sets the event id.
    /// </summary>
    /// <value>The event id.</value>
    public int EventId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is similar visible.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is similar visible; otherwise, <c>false</c>.
    /// </value>
    public bool IsSimilarVisible { get; set; }

    //Changes By Chetan
    static readonly Random _random = new Random();

    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (userID > 0)
            {
                if (Session["ViewArchivedEvents"] != null)
                {
                    viewArchivedEvents = (Boolean)Session["ViewArchivedEvents"];
                }

                userNameLabel.Text = user.fullName;

                //profileTextLabel.Text = user.profileText.Replace("\n", "<br/>");

                if (user.profilePicThumbnail != "")
                {
                    profileImage.ImageUrl = "~/assets/profilePics/" + user.profilePicThumbnail;
                }
                else
                {
                    if (user.avatarNumber > 0)
                    {
                        profileImage.ImageUrl = "~/images/avatars/avatar" + user.avatarNumber.ToString() + "sm.gif";
                    }
                    else
                    {
                        if (user.gender == "M")
                        {
                            // 1,2,5
                            int avatarID = 5;
                            switch ((userID % 6))
                            {
                                case 0: case 1: avatarID = 1; break;
                                case 2: case 3: avatarID = 2; break;
                            }
                            profileImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                        }
                        else
                        {
                            // 3,4,6
                            int avatarID = 6;
                            switch ((userID % 6))
                            {
                                case 0: case 1: avatarID = 3; break;
                                case 2: case 3: avatarID = 4; break;
                            }
                            profileImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                        }
                        //profileImage.ImageUrl = "~/images/profile/blankProfile.jpg";
                    }
                    profileImage.Height = 50;
                    profileImage.Width = 50;
                }
                profileImage.ToolTip = user.fullName + "'s profile picture";
                myProfileTextLabel.NavigateUrl = "~/userProfile.aspx?UID=" + userID.ToString();

                int messageCount = Message.GetUnreadMessageCountForUser(userID);
                if (messageCount == 1)
                {
                    messageCountLink.Text = "<span>" + messageCount.ToString() + "</span> Message";
                }
                else
                {
                    messageCountLink.Text = "<span>" + messageCount.ToString() + "</span> Messages";
                }
                messageCountLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                messageCountLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                int pendingInviteCount = EventInvite.GetPendingInviteCountForUser(userID);
                if (pendingInviteCount == 1)
                {
                    inviteCountLink.Text = "<span>" + pendingInviteCount.ToString() + "</span> Invite";
                }
                else
                {
                    inviteCountLink.Text = "<span>" + pendingInviteCount.ToString() + "</span> Invites";
                }
                inviteCountLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                inviteCountLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                int pendingAlertCount = EventAlert.GetEventAlertCountPendingByUser(userID);
                if (pendingAlertCount == 1)
                {
                    alertCountLink.Text = "<span>" + pendingAlertCount.ToString() + "</span> Reminder";
                }
                else
                {
                    alertCountLink.Text = "<span>" + pendingAlertCount.ToString() + "</span> Reminders";
                }
                alertCountLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                alertCountLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                int trackedEventCount = TrackedEvent.GetTrackedEventCount(userID);
                if (trackedEventCount == 1)
                {
                    trackingCountLink.Text = "<span>" + trackedEventCount.ToString() + "</span> Goal followed";
                }
                else
                {
                    trackingCountLink.Text = "<span>" + trackedEventCount.ToString() + "</span> Goals followed";
                }
                trackingCountLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                trackingCountLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                int pendingRequestsCount = SedogoEvent.GetPendingMemberUserCountByUserID(userID);
                if (pendingRequestsCount == 1)
                {
                    goalJoinRequestsLink.Text = "<span>" + pendingRequestsCount.ToString() + "</span> Request";
                }
                else
                {
                    goalJoinRequestsLink.Text = "<span>" + pendingRequestsCount.ToString() + "</span> Requests";
                }
                goalJoinRequestsLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                goalJoinRequestsLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                int joinedEventCount = TrackedEvent.GetJoinedEventCount(userID);
                if (joinedEventCount == 1)
                {
                    groupGoalsLink.Text = "<span>" + joinedEventCount.ToString() + "</span> Group goal";
                }
                else
                {
                    groupGoalsLink.Text = "<span>" + joinedEventCount.ToString() + "</span> Group goals";
                }
                groupGoalsLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                groupGoalsLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                addressBookLink.Text = "Friends";
                addressBookLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                addressBookLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                int achievedEventCount = SedogoEvent.GetEventCountAchieved(userID);
                if (achievedEventCount == 1)
                {
                    achievedGoalsLink.Text = "<span>" + achievedEventCount.ToString() + "</span> Achieved goal";
                    goalsAchievedBelowProfileImageLabel.Text = "<span>" + achievedEventCount.ToString() + "</span> Achieved goal";
                }
                else
                {
                    achievedGoalsLink.Text = "<span>" + achievedEventCount.ToString() + "</span> Achieved goals";
                    goalsAchievedBelowProfileImageLabel.Text = "<span>" + achievedEventCount.ToString() + "</span> Achieved goals";
                }
                achievedGoalsLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                achievedGoalsLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                if (viewArchivedEvents == true)
                {
                    viewArchiveLink.Text = "Hide Past Goals";
                }
                else
                {
                    viewArchiveLink.Text = "Past Goals";
                }

                sidebarMenuItems.Visible = true;
                viewArchiveLink.Visible = true;
                addGoalLink.Visible = true;
                editProfileLink.Visible = true;
                profileImage.Visible = true;
                userNameLabel.Visible = true;
                //profileTextLabel.Visible = true;
                myProfileTextLabel.Visible = true;
                extraButtonsDiv.Visible = true;
                rotatorBackgroundDiv.Visible = true;
                myProfileDiv.Visible = true;
                profileImageDiv.Visible = true;
            }
            else
            {
                sidebarMenuItems.Visible = false;
                viewArchiveLink.Visible = false;
                addGoalLink.Visible = false;
                editProfileLink.Visible = false;
                profileImage.Visible = false;
                userNameLabel.Visible = false;
                //profileTextLabel.Visible = false;
                myProfileTextLabel.Visible = false;
                extraButtonsDiv.Visible = false;
                rotatorBackgroundDiv.Visible = false;
                myProfileDiv.Visible = false;
                profileImageDiv.Visible = false;
            }
        }

        PopulateLatestSearches();

        eventRotator.DataSource = GetRotatorDataSource();
        eventRotator.DataBind();
    }

    //===============================================================
    // Function: GetRotatorDataSource
    //===============================================================
    private string[] GetRotatorDataSource()
    {
        string[] images = { "go_brag", "go_fast", "go_high", "go_party", 
                               "go_sailing", "go_speechless", "go_swimming", "go_traveling", "go_watch" };
        //By Chetan
        //return images;
        string[] shuffle = RandomizeStrings(images);
        return shuffle;
    }

    //===============================================================
    // Function: PopulateLatestSearches
    //===============================================================
    private void PopulateLatestSearches()
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmdLatestEvents = new SqlCommand("", conn);
            cmdLatestEvents.CommandType = CommandType.StoredProcedure;
            cmdLatestEvents.CommandText = "spSelectLatestEvents";
            DbDataReader rdrLatestEvents = cmdLatestEvents.ExecuteReader();
            while (rdrLatestEvents.Read())
            {
                int eventID = int.Parse(rdrLatestEvents["EventID"].ToString());
                string eventName = (string)rdrLatestEvents["EventName"];

                HyperLink eventHyperlink = new HyperLink();
                eventHyperlink.Text = eventName;
                eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
                eventHyperlink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                eventHyperlink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                goalsAddedPlaceHolder.Controls.Add(eventHyperlink);

                goalsAddedPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
            }
            rdrLatestEvents.Close();

            HyperLink nHyperlink = new HyperLink();
            nHyperlink.Text = "<b>More ></b>";
            if (userID > 0)
            {
                nHyperlink.NavigateUrl = "~/MoreDetail.aspx?type=latest";
            }
            else
            {
                nHyperlink.NavigateUrl = "~/HomeMoreDetail.aspx?type=latest";
            }
            goalsAddedPlaceHolder.Controls.Add(nHyperlink);
            goalsAddedPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
            
            SqlCommand cmdUpdatedEvents = new SqlCommand("", conn);
            cmdUpdatedEvents.CommandType = CommandType.StoredProcedure;
            cmdUpdatedEvents.CommandText = "spSelectLatestUpdatedEvents";
            DbDataReader rdrUpdatedEvents = cmdUpdatedEvents.ExecuteReader();
            while (rdrUpdatedEvents.Read())
            {
                int eventID = int.Parse(rdrUpdatedEvents["EventID"].ToString());
                string eventName = (string)rdrUpdatedEvents["EventName"];

                HyperLink eventHyperlink = new HyperLink();
                eventHyperlink.Text = eventName;
                eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
                goalsUpdatedPlaceHolder.Controls.Add(eventHyperlink);

                goalsUpdatedPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
            }
            rdrUpdatedEvents.Close();

            HyperLink nUpdatedHyperlink = new HyperLink();
            nUpdatedHyperlink.Text = "<b>More ></b>";
            if (userID > 0)
            {
                nUpdatedHyperlink.NavigateUrl = "~/MoreDetail.aspx?type=updated";
            }
            else
            {
                nUpdatedHyperlink.NavigateUrl = "~/HomeMoreDetail.aspx?type=updated";
            }
            goalsUpdatedPlaceHolder.Controls.Add(nUpdatedHyperlink);
            goalsUpdatedPlaceHolder.Controls.Add(new LiteralControl("<br/>"));

            SqlCommand cmdNowEvents = new SqlCommand("", conn);
            cmdNowEvents.CommandType = CommandType.StoredProcedure;
            cmdNowEvents.CommandText = "spSelectHappeningNowEvents";
            DbDataReader rdrNowEvents = cmdNowEvents.ExecuteReader();
            while (rdrNowEvents.Read())
            {
                int eventID = int.Parse(rdrNowEvents["EventID"].ToString());
                string eventName = (string)rdrNowEvents["EventName"];

                HyperLink eventHyperlink = new HyperLink();
                eventHyperlink.Text = eventName;
                eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
                goalsHappeningNowPlaceHolder.Controls.Add(eventHyperlink);

                goalsHappeningNowPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
            }
            rdrNowEvents.Close();

            HyperLink nNowHyperlink = new HyperLink();
            nNowHyperlink.Text = "<b>More ></b>";
            if (userID > 0)
            {
                nNowHyperlink.NavigateUrl = "~/MoreDetail.aspx?type=now";
            }
            else
            {
                nNowHyperlink.NavigateUrl = "~/HomeMoreDetail.aspx?type=now";
            }
            goalsHappeningNowPlaceHolder.Controls.Add(nNowHyperlink);
            goalsHappeningNowPlaceHolder.Controls.Add(new LiteralControl("<br/>"));

            SqlCommand cmdAchievedEvents = new SqlCommand("", conn);
            cmdAchievedEvents.CommandType = CommandType.StoredProcedure;
            cmdAchievedEvents.CommandText = "spSelectLatestAchievedEvents";
            DbDataReader rdrAchievedEvents = cmdAchievedEvents.ExecuteReader();
            while (rdrAchievedEvents.Read())
            {
                int eventID = int.Parse(rdrAchievedEvents["EventID"].ToString());
                string eventName = (string)rdrAchievedEvents["EventName"];

                HyperLink eventHyperlink = new HyperLink();
                eventHyperlink.Text = eventName;
                eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
                goalsAchievedPlaceHolder.Controls.Add(eventHyperlink);

                goalsAchievedPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
            }
            rdrAchievedEvents.Close();

            HyperLink nAchievedHyperlink = new HyperLink();
            nAchievedHyperlink.Text = "<b>More ></b>";
            if (userID > 0)
            {
                nAchievedHyperlink.NavigateUrl = "~/MoreDetail.aspx?type=achieved";
            }
            else
            {
                nAchievedHyperlink.NavigateUrl = "~/HomeMoreDetail.aspx?type=achieved";
            }
            goalsAchievedPlaceHolder.Controls.Add(nAchievedHyperlink);
            goalsAchievedPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }
		CreateSimilarEvents();
        CreateOtherEvents();
    }

    /// <summary>
    /// Creates the similar events.
    /// </summary>
    private void CreateSimilarEvents()
    {
        similarPanel.Visible = IsSimilarVisible;
        if (!IsSimilarVisible)
        {
            return;
        }
        using (var sedogoDbEntities = new SedogoDbEntities())
        {
            var searchWord = sedogoDbEntities.Events.First(x => x.EventID == EventId).EventName;
            var events = sedogoDbEntities.SelectSimilarEvents(searchWord, EventId);
            if (events == null)
            {
                return;
            }
            foreach (var @event in events)
            {
                {
                    var eventID = int.Parse(@event.EventID.ToString());
                    var eventName = @event.EventName;

                    var eventHyperlink = new HyperLink
                                             {
                                                 Text = eventName,
                                                 NavigateUrl = "~/viewEvent.aspx?EID=" + eventID
                                             };
                    goalsSimilarPlaceHolder.Controls.Add(eventHyperlink);

                    goalsSimilarPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
                }

            }
        }
        var nSimilarHyperlink = new HyperLink
                                         {
                                             Text = "<b>More ></b>",
                                             NavigateUrl = userID > 0
                                                               ? "~/MoreDetail.aspx?type=similar&EID=" + EventId
                                                               : "~/HomeMoreDetail.aspx?type=similar&EID=" + EventId
                                         };
        goalsSimilarPlaceHolder.Controls.Add(nSimilarHyperlink);
        goalsSimilarPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
    }


    /// <summary>
    /// Creates the other events.
    /// </summary>
    private void CreateOtherEvents()
    {
        otherPanel.Visible = IsSimilarVisible;
        if (!IsSimilarVisible)
        {
            return;
        }
        using (var sedogoDbEntities = new SedogoDbEntities())
        {
            var events = sedogoDbEntities.SelectOtherEvents(EventId);
            if (events == null)
            {
                return;
            }
            foreach (var @event in events)
            {
                {
                    var eventID = int.Parse(@event.EventID.ToString());
                    var eventName = @event.EventName;

                    var eventHyperlink = new HyperLink
                    {
                        Text = eventName,
                        NavigateUrl = "~/viewEvent.aspx?EID=" + eventID
                    };
                    goalsOtherPlaceHolder.Controls.Add(eventHyperlink);

                    goalsOtherPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
                }

            }
        }
        var nSimilarHyperlink = new HyperLink
        {
            Text = "<b>More ></b>",
            NavigateUrl = userID > 0
                              ? "~/MoreDetail.aspx?type=other&EID=" + EventId
                              : "~/HomeMoreDetail.aspx?type=other&EID=" + EventId
        };
        goalsOtherPlaceHolder.Controls.Add(nSimilarHyperlink);
        goalsOtherPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
    }

    //===============================================================
    // Function: click_viewArchiveLink
    //===============================================================
    protected void ClickViewArchiveLink(object sender, EventArgs e)
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

        //Response.Redirect(Request.Url.PathAndQuery);
        Response.Redirect("~/profile.aspx");
    }


    //===============================================================
    // Function: Randomize Array By Chetan
    //===============================================================
    private string[] RandomizeStrings(string[] arr)
    {
        List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();

        foreach (string s in arr)
        {
            list.Add(new KeyValuePair<int, string>(_random.Next(), s));
        }

        var sorted = from item in list
                     orderby item.Key
                     select item;

        string[] result = new string[arr.Length];

        int index = 0;
        foreach (KeyValuePair<int, string> pair in sorted)
        {
            result[index] = pair.Value;
            index++;
        }

        return result;
    }

}

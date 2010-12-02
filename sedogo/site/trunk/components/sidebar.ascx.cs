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
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
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
    public Boolean viewArchivedEvents;
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
        IsSimilarVisible = Request.Path.EndsWith("viewEvent.aspx");
        if (!IsPostBack)
        {
            int eventID;
            if (Request.QueryString["EID"] != null && int.TryParse(Request.QueryString["EID"], out eventID) && eventID > 0)
            {
                EventId = eventID;   
            }

            int uid;
            if(Session["loggedInUserID"] != null && int.TryParse(Session["loggedInUserID"].ToString(), out uid) && uid > 0)
            {
                this.userID = uid;
                this.user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
            }
            else
            {
                this.userID = -1;
            } 
            
            if (userID > 0)
            {
                if (Session["ViewArchivedEvents"] != null)
                {
                    viewArchivedEvents = (Boolean)Session["ViewArchivedEvents"];
                }

                if (user.fullName.Trim() != "")
                {
                    userNameLabel.Text = user.fullName;
                }
                else
                {
                    userNameLabel.Text = "&nbsp;";
                }

                if (user.profilePicThumbnail != "")
                {
                    profileImage.ImageUrl = ImageHelper.GetRelativeImagePath(user.userID, user.GUID, ImageType.UserThumbnail);
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
                myProfileTextLabel.NavigateUrl = "~/userProfile.aspx?UID=" + userID;
                //myProfileTextLabel.Text = user.profileText.Replace("\n", "<br/>");

                int messageCount = Message.GetUnreadMessageCountForUser(userID);
                if (messageCount == 1)
                {
                    messageCountLink.Text = "<span>" + messageCount + "</span> Message";
                }
                else
                {
                    messageCountLink.Text = "<span>" + messageCount + "</span> Messages";
                }
                messageCountLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                messageCountLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                int pendingInviteCount = EventInvite.GetPendingInviteCountForUser(userID);
                if (pendingInviteCount == 1)
                {
                    inviteCountLink.Text = "<span>" + pendingInviteCount + "</span> Invite";
                }
                else
                {
                    inviteCountLink.Text = "<span>" + pendingInviteCount + "</span> Invites";
                }
                inviteCountLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                inviteCountLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                int pendingAlertCount = EventAlert.GetEventAlertCountPendingByUser(userID);
                if (pendingAlertCount == 1)
                {
                    alertCountLink.Text = "<span>" + pendingAlertCount + "</span> Reminder";
                }
                else
                {
                    alertCountLink.Text = "<span>" + pendingAlertCount + "</span> Reminders";
                }
                alertCountLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                alertCountLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                int trackedEventCount = TrackedEvent.GetTrackedEventCount(userID);
                if (trackedEventCount == 1)
                {
                    trackingCountLink.Text = "<span>" + trackedEventCount + "</span> Goal followed";
                }
                else
                {
                    trackingCountLink.Text = "<span>" + trackedEventCount + "</span> Goals followed";
                }
                trackingCountLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                trackingCountLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                int pendingRequestsCount = SedogoEvent.GetPendingMemberUserCountByUserID(userID);
                if (pendingRequestsCount == 1)
                {
                    goalJoinRequestsLink.Text = "<span>" + pendingRequestsCount + "</span> Request";
                }
                else
                {
                    goalJoinRequestsLink.Text = "<span>" + pendingRequestsCount + "</span> Requests";
                }
                goalJoinRequestsLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                goalJoinRequestsLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                int joinedEventCount = TrackedEvent.GetJoinedEventCount(userID);
                if (joinedEventCount == 1)
                {
                    groupGoalsLink.Text = "<span>" + joinedEventCount + "</span> Group goal";
                }
                else
                {
                    groupGoalsLink.Text = "<span>" + joinedEventCount + "</span> Group goals";
                }
                groupGoalsLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                groupGoalsLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                addressBookLink.Text = "Friends";
                addressBookLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                addressBookLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                int achievedEventCount = SedogoEvent.GetEventCountAchieved(userID);
                if (achievedEventCount == 1)
                {
                    achievedGoalsLink.Text = "<span>" + achievedEventCount + "</span> Achieved goal";
                    goalsAchievedBelowProfileImageLabel.Text = "<span>" + achievedEventCount + "</span> Achieved goal";
                }
                else
                {
                    achievedGoalsLink.Text = "<span>" + achievedEventCount + "</span> Achieved goals";
                    goalsAchievedBelowProfileImageLabel.Text = "<span>" + achievedEventCount + "</span> Achieved goals";
                }
                achievedGoalsLink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                achievedGoalsLink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                viewArchiveLink.Text = viewArchivedEvents ? "Hide Past Goals" : "Past Goals";

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

            var cmdLatestEvents = new SqlCommand("", conn)
                                      {
                                          CommandType = CommandType.StoredProcedure,
                                          CommandText = "spSelectLatestEvents"
                                      };
            DbDataReader rdrLatestEvents = cmdLatestEvents.ExecuteReader();
            if (rdrLatestEvents != null)
            {
                while (rdrLatestEvents.Read())
                {
                    int eventID = int.Parse(rdrLatestEvents["EventID"].ToString());
                    string eventName = (string)rdrLatestEvents["EventName"];

                    var eventHyperlink = new HyperLink
                                             {
                                                 Text = eventName,
                                                 NavigateUrl = "~/viewEvent.aspx?EID=" + eventID
                                             };
                    eventHyperlink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                    eventHyperlink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                    goalsAddedPlaceHolder.Controls.Add(eventHyperlink);

                    goalsAddedPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
                }
                rdrLatestEvents.Close();
            }

            var nHyperlink = new HyperLink
                                 {
                                     Text = "<b>More ></b>",
                                     NavigateUrl =
                                         userID > 0
                                             ? "~/MoreDetail.aspx?type=latest"
                                             : "~/HomeMoreDetail.aspx?type=latest"
                                 };
            goalsAddedPlaceHolder.Controls.Add(nHyperlink);
            goalsAddedPlaceHolder.Controls.Add(new LiteralControl("<br/>"));

            var cmdUpdatedEvents = new SqlCommand("", conn)
                                       {
                                           CommandType = CommandType.StoredProcedure,
                                           CommandText = "spSelectLatestUpdatedEvents"
                                       };
            DbDataReader rdrUpdatedEvents = cmdUpdatedEvents.ExecuteReader();
            if (rdrUpdatedEvents != null)
            {
                while (rdrUpdatedEvents.Read())
                {
                    int eventID = int.Parse(rdrUpdatedEvents["EventID"].ToString());
                    string eventName = (string)rdrUpdatedEvents["EventName"];

                    var eventHyperlink = new HyperLink
                                             {
                                                 Text = eventName,
                                                 NavigateUrl = "~/viewEvent.aspx?EID=" + eventID
                                             };
                    goalsUpdatedPlaceHolder.Controls.Add(eventHyperlink);

                    goalsUpdatedPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
                }
                rdrUpdatedEvents.Close();
            }

            var nUpdatedHyperlink = new HyperLink
                                        {
                                            Text = "<b>More ></b>",
                                            NavigateUrl =
                                                userID > 0
                                                    ? "~/MoreDetail.aspx?type=updated"
                                                    : "~/HomeMoreDetail.aspx?type=updated"
                                        };
            goalsUpdatedPlaceHolder.Controls.Add(nUpdatedHyperlink);
            goalsUpdatedPlaceHolder.Controls.Add(new LiteralControl("<br/>"));

            var cmdNowEvents = new SqlCommand("", conn)
                                   {
                                       CommandType = CommandType.StoredProcedure,
                                       CommandText = "spSelectHappeningNowEvents"
                                   };
            DbDataReader rdrNowEvents = cmdNowEvents.ExecuteReader();
            if (rdrNowEvents != null)
            {
                while (rdrNowEvents.Read())
                {
                    int eventID = int.Parse(rdrNowEvents["EventID"].ToString());
                    string eventName = (string)rdrNowEvents["EventName"];

                    var eventHyperlink = new HyperLink
                                             {
                                                 Text = eventName,
                                                 NavigateUrl = "~/viewEvent.aspx?EID=" + eventID
                                             };
                    goalsHappeningNowPlaceHolder.Controls.Add(eventHyperlink);

                    goalsHappeningNowPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
                }
                rdrNowEvents.Close();
            }

            var nNowHyperlink = new HyperLink
                                    {
                                        Text = "<b>More ></b>",
                                        NavigateUrl =
                                            userID > 0 ? "~/MoreDetail.aspx?type=now" : "~/HomeMoreDetail.aspx?type=now"
                                    };
            goalsHappeningNowPlaceHolder.Controls.Add(nNowHyperlink);
            goalsHappeningNowPlaceHolder.Controls.Add(new LiteralControl("<br/>"));

            CreateAchievedEvents(conn);
            CreateSimilarEvents();
            CreateOtherEvents();
        }
        finally
        {
            conn.Close();
        }
    }

    /// <summary>
    /// Creates the achieved events.
    /// </summary>
    /// <param name="conn">The conn.</param>
    private void CreateAchievedEvents(SqlConnection conn)
    {
        var cmdAchievedEvents = new SqlCommand("", conn)
                                    {
                                        CommandType = CommandType.StoredProcedure,
                                        CommandText = "spSelectLatestAchievedEvents"
                                    };
        DbDataReader rdrAchievedEvents = cmdAchievedEvents.ExecuteReader();
        if (rdrAchievedEvents != null)
        {
            while (rdrAchievedEvents.Read())
            {
                var eventID = int.Parse(rdrAchievedEvents["EventID"].ToString());
                var eventName = (string)rdrAchievedEvents["EventName"];

                var eventHyperlink = new HyperLink
                                         {
                                             Text = eventName,
                                             NavigateUrl = "~/viewEvent.aspx?EID=" + eventID
                                         };
                goalsAchievedPlaceHolder.Controls.Add(eventHyperlink);

                goalsAchievedPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
            }
            rdrAchievedEvents.Close();
        }

        var nAchievedHyperlink = new HyperLink
                                     {
                                         Text = "<b>More ></b>",
                                         NavigateUrl = userID > 0
                                                           ? "~/MoreDetail.aspx?type=achieved"
                                                           : "~/HomeMoreDetail.aspx?type=achieved"
                                     };
        goalsAchievedPlaceHolder.Controls.Add(nAchievedHyperlink);
        goalsAchievedPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
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
            var _event = sedogoDbEntities.Events.FirstOrDefault(x => x.EventID == EventId);
            if (_event != null)
            {
                var searchWord = _event.EventName;

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

        int uid;
        if (EventId > 0 && IsSimilarVisible && Session["loggedInUserID"] != null && int.TryParse(Session["loggedInUserID"].ToString(), out uid) && uid > 0)
        {
            var currentEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), EventId);
            otherPanel.Visible = IsSimilarVisible && userID != currentEvent.userID;
        }
        else
        {
            otherPanel.Visible = IsSimilarVisible;
        } 

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
            bool flag = true;
            foreach (var @event in events)
            {
                if (flag)
                {
                    var user = new SedogoUser("", @event.UserID);
                    otherGoalsTitle.Text = user.firstName + "'s goals";
                    flag = false;
                }
                
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
    private static string[] RandomizeStrings(string[] arr)
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

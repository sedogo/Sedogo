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


public partial class sidebar : System.Web.UI.UserControl
{
    public Boolean viewArchivedEvents = false;
    public int userID = -1;
    public SedogoUser user;

    //Changes By Chetan
    static Random _random = new Random();

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
                    profileImage.ImageUrl = "~/images/profile/blankProfile.jpg";
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
                    trackingCountLink.Text = "<span>" + trackedEventCount.ToString() + "</span> Goal Followed";
                }
                else
                {
                    trackingCountLink.Text = "<span>" + trackedEventCount.ToString() + "</span> Goals Followed";
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
                profileTextLabel.Visible = true;
                myProfileTextLabel.Visible = true;
                myLatestGoalsLabel.Visible = true;
            }
            else
            {
                sidebarMenuItems.Visible = false;
                viewArchiveLink.Visible = false;
                addGoalLink.Visible = false;
                editProfileLink.Visible = false;
                profileImage.Visible = false;
                userNameLabel.Visible = false;
                profileTextLabel.Visible = false;
                myProfileTextLabel.Visible = false;
                myLatestGoalsLabel.Visible = false;
            }

            PopulateLatestSearches();

            eventRotator.DataSource = GetRotatorDataSource();
            eventRotator.DataBind();
        }
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
                if (userID > 0)
                {
                    searchHyperlink.NavigateUrl = "~/search2.aspx?Search=" + searchText;
                }
                else
                {
                    searchHyperlink.NavigateUrl = "~/search.aspx?Search=" + searchText;
                }
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
                if (userID > 0)
                {
                    searchHyperlink.NavigateUrl = "~/search2.aspx?Search=" + searchText;
                }
                else
                {
                    searchHyperlink.NavigateUrl = "~/search.aspx?Search=" + searchText;
                }
                popularSearchesPlaceholder.Controls.Add(searchHyperlink);

                popularSearchesPlaceholder.Controls.Add(new LiteralControl("<br/>"));
            }
            rdrPopular.Close();

            if (userID > 0)
            {
                SqlCommand cmdLatestEvents = new SqlCommand("", conn);
                cmdLatestEvents.CommandType = CommandType.StoredProcedure;
                cmdLatestEvents.CommandText = "spSelectLatestEvents";
                cmdLatestEvents.Parameters.Add("@LoggedInUserID", SqlDbType.Int).Value = userID;
                DbDataReader rdrLatestEvents = cmdLatestEvents.ExecuteReader();
                int cnt = 0;
                while (rdrLatestEvents.Read())
                {
                    cnt += 1;
                    int eventID = int.Parse(rdrLatestEvents["EventID"].ToString());
                    string eventName = (string)rdrLatestEvents["EventName"];

                    HyperLink eventHyperlink = new HyperLink();
                    eventHyperlink.Text = eventName;
                    eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
                    latestEventsPlaceholder.Controls.Add(eventHyperlink);

                    latestEventsPlaceholder.Controls.Add(new LiteralControl("<br/>"));
                    if (cnt >= 4)
                        break;
                }
                rdrLatestEvents.Close();

                //Changes by Chetan
                HyperLink nHyperlink = new HyperLink();
                nHyperlink.Text = "<b>More ></b>";
                nHyperlink.NavigateUrl = "~/MoreDetail.aspx";
                latestEventsPlaceholder.Controls.Add(nHyperlink);
                latestEventsPlaceholder.Controls.Add(new LiteralControl("<br/>"));
                //

                SqlCommand cmdupComing = new SqlCommand("", conn);
                cmdupComing.CommandType = CommandType.Text;
                cmdupComing.CommandText = "select top 5 * from Events where userid=" + userID.ToString() + " and CONVERT(VARCHAR(10),rangestartdate,101) >CONVERT(VARCHAR(10), GETDATE(), 101) and Deleted=0 order by rangestartdate ";
                DbDataReader rdrupComing = cmdupComing.ExecuteReader();
                while (rdrupComing.Read())
                {
                    string searchText = (string)rdrupComing["EventID"].ToString();
                    HyperLink searchHyperlink = new HyperLink();
                    searchHyperlink.Text = rdrupComing["EventName"].ToString();
                    if (userID > 0)
                    {
                        searchHyperlink.NavigateUrl = "~/viewevent.aspx?eid=" + searchText;
                    }
                    else
                    {
                        searchHyperlink.NavigateUrl = "~/viewevent.aspx?eid=" + searchText;
                    }
                    goalupcomingPlaceHolder.Controls.Add(searchHyperlink);

                    goalupcomingPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
                }
                rdrupComing.Close();
            }
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

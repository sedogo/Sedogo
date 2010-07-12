//===============================================================
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
using System.IO;
using Sedogo.BusinessObjects;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public partial class HomeMoreDetail : System.Web.UI.Page
{
    //Code By Chetan
    static Random _random = new Random();
    int userID;
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["type"] != null)
            {
                if (Request["type"].ToString().ToLower() == "achieved")
                {
                    ltHeading.Text = "Latest achieved goals";
                }
                else if (Request["type"].ToString().ToLower() == "now")
                {
                    ltHeading.Text = "Goals happening now";

                    thisWeekHeader.Visible = false;
                    twoWeeksHeader.Visible = false;
                    lastMonthHeader.Visible = false;
                    sixMonthHeader.Visible = false;
                }
                else if (Request["type"].ToString().ToLower() == "latest")
                {
                    ltHeading.Text = "Goals added";
                }
                else if (Request["type"].ToString().ToLower() == "updated")
                {
                    ltHeading.Text = "Goals updated";
                }
            }

            timelineURL.Text = "timelineHomePageXML.aspx?G=" + Guid.NewGuid().ToString();

            DateTime timelineStartDate = DateTime.Now.AddMonths(8);

            timelineStartDate1.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");     // "Jan 08 2010 00:00:00 GMT"
            timelineStartDate2.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");

            GetToday();
            if (Request["type"].ToString().ToLower() != "now")
            {
                GetThisWeek();
                GetTwoWeekAgo();
                GetLastMonth();
                GetLastSixMonth();
            }            
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


    //===============================================================
    // Function: backButton_click
    //===============================================================

    protected void backButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }

    private void GetToday()
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        conn.Open();
        SqlCommand cmd = new SqlCommand("", conn);
        cmd.CommandType = CommandType.Text;

        if (Request["type"].ToString().ToLower() == "achieved")
        {
            cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where convert(datetime,convert(varchar(11),events.EventAchievedDate,102)) = convert(datetime,convert(varchar(11),getdate(),102)) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=1 "
                + " and events.PrivateEvent=0 "
                + " ORDER BY events.EventAchievedDate DESC ";
        }
        else if (Request["type"].ToString().ToLower() == "latest")
        {
            cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where convert(datetime,convert(varchar(11),events.CreatedDate,102)) = convert(datetime,convert(varchar(11),getdate(),102)) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=0 "
                + " and events.PrivateEvent=0 "
                + " ORDER BY events.CreatedDate DESC ";
        }
        else if (Request["type"].ToString().ToLower() == "updated")
        {
            cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where convert(datetime,convert(varchar(11),events.LastUpdatedDate,102)) = convert(datetime,convert(varchar(11),getdate(),102)) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=0 "
                + " and events.PrivateEvent=0 "
                + " and events.CreatedDate <> events.LastUpdatedDate "
                + " ORDER BY events.LastUpdatedDate DESC ";
        }
        else if (Request["type"].ToString().ToLower() == "now")
        {
            cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where ( ( RangeStartDate >= getdate() and RangeEndDate >= getdate() ) "
                + " or convert(varchar(11),StartDate,103) = convert(varchar(11),getdate(),103) )"
                + " and events.deleted=0 "
                + " and events.EventAchieved=0 "
                + " and events.PrivateEvent=0 "
                + " ORDER BY events.CreatedDate DESC ";
        }
        DbDataReader rdrToday = cmd.ExecuteReader();
        while (rdrToday.Read())
        {
            int eventID = int.Parse(rdrToday["EventID"].ToString());
            string eventName = (string)rdrToday["EventName"];
            string Fname = (string)rdrToday["FirstName"];
            string Lname = (string)rdrToday["LastName"];
            string MemCount =(string)rdrToday["MemberCount"].ToString();
            string FolCount = (string)rdrToday["FollowerCount"].ToString();
            HyperLink eventHyperlink = new HyperLink();
            eventHyperlink.Text = GetSubString(eventName, 100) + " <span style=color:grey>" + Fname + " " + Lname + "</span>";// <span style=color:#cccccc> -" + MemCount + " members " + FolCount + " followers";
            eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
            eventHyperlink.Attributes.Add("class", "event");
            PlaceHolderToday.Controls.Add(eventHyperlink);
            Literal ltEvent6 = new Literal();
            string memberFollowerString = " - ";
            if (MemCount != "0")
            {
                if (MemCount == "1")
                {
                    memberFollowerString += MemCount + " member ";
                }
                else
                {
                    memberFollowerString += MemCount + " members ";
                }
            }
            if (FolCount != "0")
            {
                if (MemCount == "1")
                {
                    memberFollowerString += FolCount + " follower ";
                }
                else
                {
                    memberFollowerString += FolCount + " followers ";
                }
            }
            ltEvent6.Text = "<span style=color:#cccccc>" + memberFollowerString + "</span>";
            PlaceHolderToday.Controls.Add(ltEvent6);
            PlaceHolderToday.Controls.Add(new LiteralControl("<br/>"));
        }

        rdrToday.Close();
    }

    private void GetThisWeek()
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        conn.Open();
        SqlCommand cmd = new SqlCommand("", conn);
        cmd.CommandType = CommandType.Text;

        if (Request["type"].ToString().ToLower() == "achieved")
        {
            cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where ( events.EventAchievedDate >= dateadd(day,datediff(day,0,getdate())- 7,0) "
                + " and events.EventAchievedDate <= dateadd(hh,-(datepart(hh,getdate())+1),getdate()) ) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=1 "
                + " and events.PrivateEvent=0 "
                + " ORDER BY events.EventAchievedDate DESC ";
        }
        else if (Request["type"].ToString().ToLower() == "latest")
        {
            cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where ( events.CreatedDate >= dateadd(day,datediff(day,0,getdate())- 7,0) "
                + " and events.CreatedDate <= dateadd(hh,-(datepart(hh,getdate())+1),getdate()) ) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=0 "
                + " and events.PrivateEvent=0 "
                + " ORDER BY events.CreatedDate DESC ";
        }
        else if (Request["type"].ToString().ToLower() == "updated")
        {
            cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where ( events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())- 7,0) "
                + " and events.LastUpdatedDate <= dateadd(hh,-(datepart(hh,getdate())+1),getdate()) ) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=0 "
                + " and events.PrivateEvent=0 "
                + " and events.CreatedDate <> events.LastUpdatedDate "
                + " ORDER BY events.LastUpdatedDate DESC ";
        }

        DbDataReader rdrThisWeek = cmd.ExecuteReader();

        while (rdrThisWeek.Read())
        {
            int eventID = int.Parse(rdrThisWeek["EventID"].ToString());
            string eventName = (string)rdrThisWeek["EventName"];
            string Fname = (string)rdrThisWeek["FirstName"];
            string Lname = (string)rdrThisWeek["LastName"];
            string MemCount = (string)rdrThisWeek["MemberCount"].ToString();
            string FolCount = (string)rdrThisWeek["FollowerCount"].ToString();
            HyperLink eventHyperlink = new HyperLink();
            eventHyperlink.Text = GetSubString(eventName, 100) + " <span style=color:grey>" + Fname + " " + Lname + "</span>";// <span style=color:#cccccc> -" + MemCount + " members " + FolCount + " followers</span>";
            eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
            eventHyperlink.Attributes.Add("class", "event");
            PlaceHolderThisWeek.Controls.Add(eventHyperlink);
            Literal ltEvent6 = new Literal();
            string memberFollowerString = " - ";
            if (MemCount != "0")
            {
                if (MemCount == "1")
                {
                    memberFollowerString += MemCount + " member ";
                }
                else
                {
                    memberFollowerString += MemCount + " members ";
                }
            }
            if (FolCount != "0")
            {
                if (MemCount == "1")
                {
                    memberFollowerString += FolCount + " follower ";
                }
                else
                {
                    memberFollowerString += FolCount + " followers ";
                }
            }
            ltEvent6.Text = "<span style=color:#cccccc>" + memberFollowerString + "</span>";
            PlaceHolderThisWeek.Controls.Add(ltEvent6);
            PlaceHolderThisWeek.Controls.Add(new LiteralControl("<br/>"));
        }

        rdrThisWeek.Close();
    }
    private void GetTwoWeekAgo()
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        conn.Open();
        SqlCommand cmd = new SqlCommand("", conn);
        cmd.CommandType = CommandType.Text;

        string dtAgo = DateTime.Now.Date.AddDays(-15).ToString("yyyy-MM-dd");

        if (Request["type"].ToString().ToLower() == "achieved")
        {
            cmd.CommandText = "select top 20  events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where ( events.EventAchievedDate >= dateadd(day,datediff(day,0,getdate())-14,0) "
                + " and events.EventAchievedDate <= dateadd(day,datediff(day,0,getdate())-7,0) ) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=1 "
                + " and events.PrivateEvent=0 "
                + " ORDER BY events.EventAchievedDate DESC ";
        }
        else if (Request["type"].ToString().ToLower() == "latest")
        {
            cmd.CommandText = "select top 20  events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where ( events.CreatedDate >= dateadd(day,datediff(day,0,getdate())-14,0) "
                + " and events.CreatedDate <= dateadd(day,datediff(day,0,getdate())-7,0) ) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=0 "
                + " and events.PrivateEvent=0 "
                + " ORDER BY events.CreatedDate DESC ";
        }
        else if (Request["type"].ToString().ToLower() == "updated")
        {
            cmd.CommandText = "select top 20  events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where ( events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())-14,0) "
                + " and events.LastUpdatedDate <= dateadd(day,datediff(day,0,getdate())-7,0) ) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=0 "
                + " and events.PrivateEvent=0 "
                + " and events.CreatedDate <> events.LastUpdatedDate "
                + " ORDER BY events.LastUpdatedDate DESC ";
        }

        DbDataReader rdrTwoWeekAgo = cmd.ExecuteReader();

        while (rdrTwoWeekAgo.Read())
        {
            int eventID = int.Parse(rdrTwoWeekAgo["EventID"].ToString());
            string eventName = (string)rdrTwoWeekAgo["EventName"];
            string Fname = (string)rdrTwoWeekAgo["FirstName"];
            string Lname = (string)rdrTwoWeekAgo["LastName"];
            string MemCount = (string)rdrTwoWeekAgo["MemberCount"].ToString();
            string FolCount = (string)rdrTwoWeekAgo["FollowerCount"].ToString();
            HyperLink eventHyperlink = new HyperLink();
            eventHyperlink.Text = GetSubString(eventName, 100) + " <span style=color:grey>" + Fname + " " + Lname + "</span>";// <span style=color:#cccccc> - " + MemCount + " members " + FolCount + " followers</span>";
            eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
            eventHyperlink.Attributes.Add("class", "event");
            PlaceHolderTwoWeekAgo.Controls.Add(eventHyperlink);
            Literal ltEvent6 = new Literal();
            string memberFollowerString = " - ";
            if (MemCount != "0")
            {
                if (MemCount == "1")
                {
                    memberFollowerString += MemCount + " member ";
                }
                else
                {
                    memberFollowerString += MemCount + " members ";
                }
            }
            if (FolCount != "0")
            {
                if (MemCount == "1")
                {
                    memberFollowerString += FolCount + " follower ";
                }
                else
                {
                    memberFollowerString += FolCount + " followers ";
                }
            }
            ltEvent6.Text = "<span style=color:#cccccc>" + memberFollowerString + "</span>";
            PlaceHolderTwoWeekAgo.Controls.Add(ltEvent6);
            PlaceHolderTwoWeekAgo.Controls.Add(new LiteralControl("<br/>"));
        }

        rdrTwoWeekAgo.Close();
    }

    private void GetLastMonth()
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        conn.Open();
        SqlCommand cmd = new SqlCommand("", conn);
        cmd.CommandType = CommandType.Text;
        string dtLMnth = DateTime.Now.Date.AddMonths(-1).ToString("yyyy-MM-dd");

        if (Request["type"].ToString().ToLower() == "achieved")
        {
            cmd.CommandText = "select top 20  events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where ( events.EventAchievedDate >= dateadd(day,datediff(day,0,getdate())-31,0) "
                + " and events.EventAchievedDate <= dateadd(day,datediff(day,0,getdate())-14,0) ) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=1 "
                + " and events.PrivateEvent=0 "
                + " ORDER BY events.EventAchievedDate DESC ";
        }
        else if (Request["type"].ToString().ToLower() == "latest")
        {
            cmd.CommandText = "select top 20  events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where ( events.CreatedDate >= dateadd(day,datediff(day,0,getdate())-31,0) "
                + " and events.CreatedDate <= dateadd(day,datediff(day,0,getdate())-14,0) ) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=0 "
                + " and events.PrivateEvent=0 "
                + " ORDER BY events.CreatedDate DESC ";
        }
        else if (Request["type"].ToString().ToLower() == "updated")
        {
            cmd.CommandText = "select top 20  events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where ( events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())-31,0) "
                + " and events.LastUpdatedDate <= dateadd(day,datediff(day,0,getdate())-14,0) ) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=0 "
                + " and events.PrivateEvent=0 "
                + " and events.CreatedDate <> events.LastUpdatedDate "
                + " ORDER BY events.LastUpdatedDate DESC ";
        }

        //+ " where month(events.lastupdateddate)= month('" + dtLMnth + "') and year(events.lastupdateddate)= year('" + dtLMnth + "') "

        DbDataReader rdrLastMonth = cmd.ExecuteReader();

        while (rdrLastMonth.Read())
        {
            int eventID = int.Parse(rdrLastMonth["EventID"].ToString());
            string eventName = (string)rdrLastMonth["EventName"];
            string Fname = (string)rdrLastMonth["FirstName"];
            string Lname = (string)rdrLastMonth["LastName"];
            string MemCount = (string)rdrLastMonth["MemberCount"].ToString();
            string FolCount = (string)rdrLastMonth["FollowerCount"].ToString();
            HyperLink eventHyperlink = new HyperLink();
            eventHyperlink.Text = GetSubString(eventName, 100) + " <span style=color:grey>" + Fname + " " + Lname + "</span> ";
            eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
            eventHyperlink.Attributes.Add("class", "event");
            PlaceHolderLastMonth.Controls.Add(eventHyperlink);
            Literal ltEvent = new Literal();
            string memberFollowerString = " - ";
            if (MemCount != "0")
            {
                if (MemCount == "1")
                {
                    memberFollowerString += MemCount + " member ";
                }
                else
                {
                    memberFollowerString += MemCount + " members ";
                }
            }
            if (FolCount != "0")
            {
                if (MemCount == "1")
                {
                    memberFollowerString += FolCount + " follower ";
                }
                else
                {
                    memberFollowerString += FolCount + " followers ";
                }
            }
            ltEvent.Text = "<span style=color:#cccccc>" + memberFollowerString + "</span>";
            PlaceHolderLastMonth.Controls.Add(ltEvent);
            PlaceHolderLastMonth.Controls.Add(new LiteralControl("<br/>"));
        }
        rdrLastMonth.Close();
    }

    private void GetLastSixMonth()
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        conn.Open();
        SqlCommand cmd = new SqlCommand("", conn);
        cmd.CommandType = CommandType.Text;

        string dtLMnth = DateTime.Now.Date.AddMonths(-6).ToString("yyyy-MM-dd");

        if (Request["type"].ToString().ToLower() == "achieved")
        {
            cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where ( events.EventAchievedDate >= dateadd(day,datediff(day,0,getdate())-182,0) "
                + " and events.EventAchievedDate <= dateadd(day,datediff(day,0,getdate())-31,0) ) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=1 "
                + " and events.PrivateEvent=0 "
                + " ORDER BY events.EventAchievedDate DESC ";
        }
        else if (Request["type"].ToString().ToLower() == "latest")
        {
            cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where ( events.CreatedDate >= dateadd(day,datediff(day,0,getdate())-182,0) "
                + " and events.CreatedDate <= dateadd(day,datediff(day,0,getdate())-31,0) ) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=0 "
                + " and events.PrivateEvent=0 "
                + " ORDER BY events.CreatedDate DESC ";
        }
        else if (Request["type"].ToString().ToLower() == "updated")
        {
            cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                + " from events inner join users on users.userid=events.userid  "
                + " where ( events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())-182,0) "
                + " and events.LastUpdatedDate <= dateadd(day,datediff(day,0,getdate())-31,0) ) "
                + " and events.deleted=0 "
                + " and events.EventAchieved=0 "
                + " and events.PrivateEvent=0 "
                + " and events.CreatedDate <> events.LastUpdatedDate "
                + " ORDER BY events.LastUpdatedDate DESC ";
        }

        DbDataReader rdrLastSixMonth = cmd.ExecuteReader();
        //    + " where convert(datetime,convert(varchar,events.lastupdateddate,102)) >=(select convert(datetime, convert(varchar,'" + dtLMnth + "',102))) "

        while (rdrLastSixMonth.Read())
        {
            int eventID = int.Parse(rdrLastSixMonth["EventID"].ToString());
            string eventName = (string)rdrLastSixMonth["EventName"];
            string Fname = (string)rdrLastSixMonth["FirstName"];
            string Lname = (string)rdrLastSixMonth["LastName"];
            string MemCount = (string)rdrLastSixMonth["MemberCount"].ToString();
            string FolCount = (string)rdrLastSixMonth["FollowerCount"].ToString();
            HyperLink eventHyperlink = new HyperLink();
            eventHyperlink.Text = GetSubString(eventName, 100) + " <span style=color:grey>" + Fname + " " + Lname + "</span>";// <span style=color:#cccccc> - " + MemCount + " members " + FolCount + " followers</span>";
            eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
            eventHyperlink.Attributes.Add("class", "event");
            PlaceHolderLastSixMonth.Controls.Add(eventHyperlink);
            Literal ltEvent6 = new Literal();
            string memberFollowerString = " - ";
            if (MemCount != "0")
            {
                if (MemCount == "1")
                {
                    memberFollowerString += MemCount + " member ";
                }
                else
                {
                    memberFollowerString += MemCount + " members ";
                }
            }
            if (FolCount != "0")
            {
                if (MemCount == "1")
                {
                    memberFollowerString += FolCount + " follower ";
                }
                else
                {
                    memberFollowerString += FolCount + " followers ";
                }
            }
            ltEvent6.Text = "<span style=color:#cccccc>" + memberFollowerString + "</span>";
            PlaceHolderLastSixMonth.Controls.Add(ltEvent6);
            PlaceHolderLastSixMonth.Controls.Add(new LiteralControl("<br/>"));
        }

        rdrLastSixMonth.Close();
    }
    private string GetSubString(string inputstr, Int32 length)
    {
        if (inputstr.Length > length)
        {
            return inputstr.Substring(0, length)+ "...";
        }
        else
        {
            return inputstr;
        }
        
    }
}

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
using Sedogo.DAL;

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
                switch (Request["type"].ToLower())
                {
                    case "achieved":
                        ltHeading.Text = "Latest achieved goals";
                        break;
                    case "now":
                        ltHeading.Text = "Goals happening now";
                        thisWeekHeader.Visible = false;
                        twoWeeksHeader.Visible = false;
                        lastMonthHeader.Visible = false;
                        sixMonthHeader.Visible = false;
                        break;
                    case "latest":
                        ltHeading.Text = "Goals added";
                        break;
                    case "updated":
                        ltHeading.Text = "Goals updated";
                        break;
                    case "similar":
                        ltHeading.Text = "Similar goals";
                        break;
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
        SqlCommand cmd;
        using (var conn = new SqlConnection((string)Application["connectionString"]))
        {
            conn.Open();
            cmd = new SqlCommand("", conn) {CommandType = CommandType.Text};

            switch (Request["type"].ToLower())
            {
                case "achieved":
                    cmd.CommandText = @"select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,
                                        (
                                            SELECT count(1)  
                                            FROM TrackedEvents T   
                                            JOIN Users U   ON T.UserID = U.UserID   
                                            WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1  
                                        ) as MemberCount, 
                                        (
                                            SELECT count(1)  
                                            FROM TrackedEvents T   
                                            JOIN Users U   ON T.UserID = U.UserID   
                                            WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=0 
                                        ) as FollowerCount  
                                        FROM events 
                                        INNER JOIN users on users.userid=events.userid   
                                        WHERE convert(datetime,convert(varchar(11),events.EventAchievedDate,102)) = convert(datetime,convert(varchar(11),getdate(),102))  
                                              and events.deleted=0  and events.EventAchieved=1  and events.PrivateEvent=0  
                                        ORDER BY events.EventAchievedDate DESC ";
                    break;
                case "latest":
                    cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                                      + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                                      + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                                      + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                                      + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                                      + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                                      + " from events inner join users on users.userid=events.userid  "
                                      +
                                      " where convert(datetime,convert(varchar(11),events.CreatedDate,102)) = convert(datetime,convert(varchar(11),getdate(),102)) "
                                      + " and events.deleted=0 "
                                      + " and events.EventAchieved=0 "
                                      + " and events.PrivateEvent=0 "
                                      + " ORDER BY events.CreatedDate DESC ";
                    break;
                case "updated":
                    cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                                      + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                                      + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                                      + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                                      + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                                      + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                                      + " from events inner join users on users.userid=events.userid  "
                                      +
                                      " where convert(datetime,convert(varchar(11),events.LastUpdatedDate,102)) = convert(datetime,convert(varchar(11),getdate(),102)) "
                                      + " and events.deleted=0 "
                                      + " and events.EventAchieved=0 "
                                      + " and events.PrivateEvent=0 "
                                      + " and events.CreatedDate <> events.LastUpdatedDate "
                                      + " ORDER BY events.LastUpdatedDate DESC ";
                    break;
                case "now":
                    cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                                      + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                                      + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                                      + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                                      + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                                      + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                                      + " from events inner join users on users.userid=events.userid  "
                                      + " where ( ( RangeStartDate <= getdate() and RangeEndDate >= getdate() ) "
                                      + " or convert(varchar(11),StartDate,103) = convert(varchar(11),getdate(),103) )"
                                      + " and events.deleted=0 "
                                      + " and events.EventAchieved=0 "
                                      + " and events.PrivateEvent=0 "
                                      + " ORDER BY events.StartDate DESC, events.RangeEndDate ASC ";
                    break;
                case "similar":
                    using (var sedogoDbEntities = new SedogoDbEntities())
                    {
                        var eventId = Convert.ToInt32(Request["EID"]);
                        var searchWord = sedogoDbEntities.Events.First(x => x.EventID == eventId).EventName;

                        cmd.CommandText =
                            @"select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,
                                    (	
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=1  
                                    ) as MemberCount, 
                                    (
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U   ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=0 
                                    ) as FollowerCount  
                                    from events 
                                    inner join users on users.userid=events.userid
                                    INNER JOIN FREETEXTTABLE([Events], EventName, '" + searchWord + @"') AS KEY_TBL ON events.EventID = KEY_TBL.[KEY]   
                                    where 
                                        convert(datetime,convert(varchar(11),events.LastUpdatedDate,102)) = convert(datetime,convert(varchar(11),getdate(),102))
                                        and events.deleted=0 and events.EventAchieved=0 and events.PrivateEvent=0
                                    ORDER BY KEY_TBL.RANK DESC ";
                    }
                    break;
                case "other":
                    var eId = Convert.ToInt32(Request["EID"]);
                    cmd.CommandText =
                        @"select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,
                                    (	
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=1  
                                    ) as MemberCount, 
                                    (
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U   ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=0 
                                    ) as FollowerCount  
                                    from events 
                                    inner join users on users.userid=events.userid   
                                    where 
                                        convert(datetime,convert(varchar(11),events.LastUpdatedDate,102)) = convert(datetime,convert(varchar(11),getdate(),102))
                                        and events.deleted=0 and events.EventAchieved=0 and events.PrivateEvent=0
                                        and events.deleted=0 and events.EventAchieved=0 and events.PrivateEvent=0  
                                      and events.UserId IN (SELECT UserID FROM Events WHERE EventId = " + eId + @")
                                    ORDER BY events.CreatedDate DESC ";
                    break;
            }
            DbDataReader rdrToday = cmd.ExecuteReader();
            if (rdrToday == null)
            {
                return;
            }
            while (rdrToday.Read())
            {
                var eventID = int.Parse(rdrToday["EventID"].ToString());
                var eventName = (string) rdrToday["EventName"];
                var fname = (string) rdrToday["FirstName"];
                var lname = (string) rdrToday["LastName"];
                var memCount = rdrToday["MemberCount"].ToString();
                var folCount = rdrToday["FollowerCount"].ToString();
                var eventHyperlink = new HyperLink
                                         {
                                             Text =
                                                 GetSubString(eventName, 100) + " <span style=color:grey>" + fname + " " +
                                                 lname + "</span>",
                                             NavigateUrl = "~/viewEvent.aspx?EID=" + eventID
                                         };
                eventHyperlink.Attributes.Add("class", "event");
                PlaceHolderToday.Controls.Add(eventHyperlink);
                var ltEvent6 = new Literal();
                var memberFollowerString = " - ";
                if (memCount != "0")
                {
                    memberFollowerString += memCount == "1" ? memCount + " member " : memCount + " members ";
                }
                if (folCount != "0")
                {
                    memberFollowerString += memCount == "1" ? folCount + " follower " : folCount + " followers ";
                }
                ltEvent6.Text = "<span style=color:#cccccc>" + memberFollowerString + "</span>";
                PlaceHolderToday.Controls.Add(ltEvent6);
                PlaceHolderToday.Controls.Add(new LiteralControl("<br/>"));
            }

            rdrToday.Close();
        }
    }

    private void GetThisWeek()
    {
        SqlCommand cmd;
        using (var conn = new SqlConnection((string)Application["connectionString"]))
        {
            conn.Open();
            cmd = new SqlCommand("", conn) {CommandType = CommandType.Text};

            switch (Request["type"].ToLower())
            {
                case "achieved":
                    cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1)  FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1  ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount  from events inner join users on users.userid=events.userid   where ( events.EventAchievedDate >= dateadd(day,datediff(day,0,getdate())- 7,0)  and events.EventAchievedDate <= dateadd(hh,-(datepart(hh,getdate())+1),getdate()) )  and events.deleted=0  and events.EventAchieved=1  and events.PrivateEvent=0  ORDER BY events.EventAchievedDate DESC ";
                    break;
                case "latest":
                    cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1)  FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1  ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount  from events inner join users on users.userid=events.userid   where ( events.CreatedDate >= dateadd(day,datediff(day,0,getdate())- 7,0)  and events.CreatedDate <= dateadd(hh,-(datepart(hh,getdate())+1),getdate()) )  and events.deleted=0  and events.EventAchieved=0  and events.PrivateEvent=0  ORDER BY events.CreatedDate DESC ";
                    break;
                case "updated":
                    cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1)  FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1  ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount  from events inner join users on users.userid=events.userid   where ( events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())- 7,0)  and events.LastUpdatedDate <= dateadd(hh,-(datepart(hh,getdate())+1),getdate()) )  and events.deleted=0  and events.EventAchieved=0  and events.PrivateEvent=0  and events.CreatedDate <> events.LastUpdatedDate  ORDER BY events.LastUpdatedDate DESC ";
                    break;
                case "similar":
                    using (var sedogoDbEntities = new SedogoDbEntities())
                    {
                        var eventId = Convert.ToInt32(Request["EID"]);
                        var searchWord = sedogoDbEntities.Events.First(x => x.EventID == eventId).EventName;

                        cmd.CommandText =
                            @"select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,
                                    (	
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=1  
                                    ) as MemberCount, 
                                    (
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U   ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=0 
                                    ) as FollowerCount  
                                    from events 
                                    inner join users on users.userid=events.userid
                                    INNER JOIN FREETEXTTABLE([Events], EventName, '" + searchWord + @"') AS KEY_TBL ON events.EventID = KEY_TBL.[KEY]      
                                    where 
                                    ( 
                                        events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())- 7,0) 
                                        and events.LastUpdatedDate <= dateadd(hh,-(datepart(hh,getdate())+1),getdate()) 
                                    )   and events.deleted=0 and events.EventAchieved=0 and events.PrivateEvent=0  
                                    ORDER BY KEY_TBL.RANK DESC ";
                    }
                    break;
                case "other":
                    var eId = Convert.ToInt32(Request["EID"]);

                    cmd.CommandText =
                        @"select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,
                                (	
	                                SELECT count(1)  
	                                FROM TrackedEvents T   
	                                JOIN Users U ON T.UserID = U.UserID   
	                                WHERE T.EventID = events.eventid   
		                                AND U.Deleted = 0 
		                                and T.showontimeline=1  
                                ) as MemberCount, 
                                (
	                                SELECT count(1)  
	                                FROM TrackedEvents T   
	                                JOIN Users U   ON T.UserID = U.UserID   
	                                WHERE T.EventID = events.eventid   
		                                AND U.Deleted = 0 
		                                and T.showontimeline=0 
                                ) as FollowerCount  
                                from events 
                                inner join users on users.userid=events.userid   
                                where 
                                ( 
                                    events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())- 7,0) 
                                    and events.LastUpdatedDate <= dateadd(hh,-(datepart(hh,getdate())+1),getdate()) 
                                )   and events.deleted=0 and events.EventAchieved=0 and events.PrivateEvent=0  
                                    and events.UserId IN (SELECT UserID FROM Events WHERE EventId = " + eId + @")
                                ORDER BY events.CreatedDate DESC ";
                    break;
            }

            DbDataReader rdrThisWeek = cmd.ExecuteReader();

            if (rdrThisWeek == null)
            {
                return;
            }
            while (rdrThisWeek.Read())
            {
                var eventID = int.Parse(rdrThisWeek["EventID"].ToString());
                var eventName = (string) rdrThisWeek["EventName"];
                var fname = (string) rdrThisWeek["FirstName"];
                var lname = (string) rdrThisWeek["LastName"];
                var memCount = rdrThisWeek["MemberCount"].ToString();
                var folCount = rdrThisWeek["FollowerCount"].ToString();
                var eventHyperlink = new HyperLink
                                         {
                                             Text =
                                                 GetSubString(eventName, 100) + " <span style=color:grey>" + fname + " " +
                                                 lname + "</span>",
                                             NavigateUrl = "~/viewEvent.aspx?EID=" + eventID
                                         };
                eventHyperlink.Attributes.Add("class", "event");
                PlaceHolderThisWeek.Controls.Add(eventHyperlink);
                var ltEvent6 = new Literal();
                var memberFollowerString = " - ";
                if (memCount != "0")
                {
                    memberFollowerString += memCount == "1" ? memCount + " member " : memCount + " members ";
                }
                if (folCount != "0")
                {
                    memberFollowerString += memCount == "1" ? folCount + " follower " : folCount + " followers ";
                }
                ltEvent6.Text = "<span style=color:#cccccc>" + memberFollowerString + "</span>";
                PlaceHolderThisWeek.Controls.Add(ltEvent6);
                PlaceHolderThisWeek.Controls.Add(new LiteralControl("<br/>"));
            }

            rdrThisWeek.Close();
        }
    }
    private void GetTwoWeekAgo()
    {
        SqlCommand cmd;
        using (var conn = new SqlConnection((string)Application["connectionString"]))
        {
            conn.Open();
            cmd = new SqlCommand("", conn) {CommandType = CommandType.Text};

            switch (Request["type"].ToLower())
            {
                case "achieved":
                    cmd.CommandText = "select top 20  events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                                      + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                                      + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                                      + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                                      + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                                      + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                                      + " from events inner join users on users.userid=events.userid  "
                                      +
                                      " where ( events.EventAchievedDate >= dateadd(day,datediff(day,0,getdate())-14,0) "
                                      + " and events.EventAchievedDate <= dateadd(day,datediff(day,0,getdate())-7,0) ) "
                                      + " and events.deleted=0 "
                                      + " and events.EventAchieved=1 "
                                      + " and events.PrivateEvent=0 "
                                      + " ORDER BY events.EventAchievedDate DESC ";
                    break;
                case "latest":
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
                    break;
                case "updated":
                    cmd.CommandText = "select top 20  events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                                      + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                                      + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                                      + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                                      + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                                      + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                                      + " from events inner join users on users.userid=events.userid  "
                                      +
                                      " where ( events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())-14,0) "
                                      + " and events.LastUpdatedDate <= dateadd(day,datediff(day,0,getdate())-7,0) ) "
                                      + " and events.deleted=0 "
                                      + " and events.EventAchieved=0 "
                                      + " and events.PrivateEvent=0 "
                                      + " and events.CreatedDate <> events.LastUpdatedDate "
                                      + " ORDER BY events.LastUpdatedDate DESC ";
                    break;
                case "similar":
                    using (var sedogoDbEntities = new SedogoDbEntities())
                    {
                        var eventId = Convert.ToInt32(Request["EID"]);
                        var searchWord = sedogoDbEntities.Events.First(x => x.EventID == eventId).EventName;

                        cmd.CommandText =
                            @"select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,
                                    (	
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=1  
                                    ) as MemberCount, 
                                    (
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U   ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=0 
                                    ) as FollowerCount  
                                    from events 
                                    inner join users on users.userid=events.userid 
                                    INNER JOIN FREETEXTTABLE([Events], EventName, '" + searchWord + @"') AS KEY_TBL ON events.EventID = KEY_TBL.[KEY]  
                                    where 
                                    ( 
                                        events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())-14,0) 
                                        and events.LastUpdatedDate <= dateadd(day,datediff(day,0,getdate())-7,0) 
                                    )   and events.deleted=0 and events.EventAchieved=0 and events.PrivateEvent=0  
                                    ORDER BY KEY_TBL.RANK DESC ";
                    }
                    break;
                case "other":
                    var eId = Convert.ToInt32(Request["EID"]);

                    cmd.CommandText =
                        @"select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,
                                    (	
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=1  
                                    ) as MemberCount, 
                                    (
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U   ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=0 
                                    ) as FollowerCount  
                                    from events 
                                    inner join users on users.userid=events.userid   
                                    where 
                                    ( 
                                        events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())-14,0) 
                                        and events.LastUpdatedDate <= dateadd(day,datediff(day,0,getdate())-7,0) 
                                    )   and events.deleted=0 and events.EventAchieved=0 and events.PrivateEvent=0  
                                        and events.UserId IN (SELECT UserID FROM Events WHERE EventId = " + eId + @")
                                    ORDER BY events.CreatedDate DESC ";
                    break;
            }

            DbDataReader rdrTwoWeekAgo = cmd.ExecuteReader();

            if (rdrTwoWeekAgo == null)
            {
                return;
            }
            while (rdrTwoWeekAgo.Read())
            {
                var eventID = int.Parse(rdrTwoWeekAgo["EventID"].ToString());
                var eventName = (string) rdrTwoWeekAgo["EventName"];
                var fname = (string) rdrTwoWeekAgo["FirstName"];
                var lname = (string) rdrTwoWeekAgo["LastName"];
                var memCount = rdrTwoWeekAgo["MemberCount"].ToString();
                var folCount = rdrTwoWeekAgo["FollowerCount"].ToString();
                var eventHyperlink = new HyperLink
                                         {
                                             Text =
                                                 GetSubString(eventName, 100) + " <span style=color:grey>" + fname + " " +
                                                 lname + "</span>",
                                             NavigateUrl = "~/viewEvent.aspx?EID=" + eventID
                                         };
                eventHyperlink.Attributes.Add("class", "event");
                PlaceHolderTwoWeekAgo.Controls.Add(eventHyperlink);
                var ltEvent6 = new Literal();
                var memberFollowerString = " - ";
                if (memCount != "0")
                {
                    memberFollowerString += memCount == "1" ? memCount + " member " : memCount + " members ";
                }
                if (folCount != "0")
                {
                    memberFollowerString += memCount == "1" ? folCount + " follower " : folCount + " followers ";
                }
                ltEvent6.Text = "<span style=color:#cccccc>" + memberFollowerString + "</span>";
                PlaceHolderTwoWeekAgo.Controls.Add(ltEvent6);
                PlaceHolderTwoWeekAgo.Controls.Add(new LiteralControl("<br/>"));
            }

            rdrTwoWeekAgo.Close();
        }
    }

    private void GetLastMonth()
    {
        SqlCommand cmd;
        using (var conn = new SqlConnection((string)Application["connectionString"]))
        {
            conn.Open();
            cmd = new SqlCommand("", conn) {CommandType = CommandType.Text};

            switch (Request["type"].ToLower())
            {
                case "achieved":
                    cmd.CommandText = "select top 20  events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                                      + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                                      + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                                      + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                                      + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                                      + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                                      + " from events inner join users on users.userid=events.userid  "
                                      +
                                      " where ( events.EventAchievedDate >= dateadd(day,datediff(day,0,getdate())-31,0) "
                                      +
                                      " and events.EventAchievedDate <= dateadd(day,datediff(day,0,getdate())-14,0) ) "
                                      + " and events.deleted=0 "
                                      + " and events.EventAchieved=1 "
                                      + " and events.PrivateEvent=0 "
                                      + " ORDER BY events.EventAchievedDate DESC ";
                    break;
                case "latest":
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
                    break;
                case "updated":
                    cmd.CommandText = "select top 20  events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                                      + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                                      + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                                      + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                                      + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                                      + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                                      + " from events inner join users on users.userid=events.userid  "
                                      +
                                      " where ( events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())-31,0) "
                                      + " and events.LastUpdatedDate <= dateadd(day,datediff(day,0,getdate())-14,0) ) "
                                      + " and events.deleted=0 "
                                      + " and events.EventAchieved=0 "
                                      + " and events.PrivateEvent=0 "
                                      + " and events.CreatedDate <> events.LastUpdatedDate "
                                      + " ORDER BY events.LastUpdatedDate DESC ";
                    break;
                case "similar":
                    using (var sedogoDbEntities = new SedogoDbEntities())
                    {
                        var eventId = Convert.ToInt32(Request["EID"]);
                        var searchWord = sedogoDbEntities.Events.First(x => x.EventID == eventId).EventName;

                        cmd.CommandText =
                            @"select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,
                                    (	
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=1  
                                    ) as MemberCount, 
                                    (
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U   ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=0 
                                    ) as FollowerCount  
                                    from events 
                                    inner join users on users.userid=events.userid 
                                    INNER JOIN FREETEXTTABLE([Events], EventName, '" + searchWord + @"') AS KEY_TBL ON events.EventID = KEY_TBL.[KEY]    
                                    where 
                                    ( 
                                        events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())-31,0)
                                        and events.LastUpdatedDate <= dateadd(day,datediff(day,0,getdate())-14,0) 
                                    )   and events.deleted=0 and events.EventAchieved=0 and events.PrivateEvent=0  
                                    ORDER BY KEY_TBL.RANK DESC ";
                    }
                    break;
                case "other":

                    var eId = Convert.ToInt32(Request["EID"]);

                    cmd.CommandText =
                        @"select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,
                        (	
	                        SELECT count(1)  
	                        FROM TrackedEvents T   
	                        JOIN Users U ON T.UserID = U.UserID   
	                        WHERE T.EventID = events.eventid   
		                        AND U.Deleted = 0 
		                        and T.showontimeline=1  
                        ) as MemberCount, 
                        (
	                        SELECT count(1)  
	                        FROM TrackedEvents T   
	                        JOIN Users U   ON T.UserID = U.UserID   
	                        WHERE T.EventID = events.eventid   
		                        AND U.Deleted = 0 
		                        and T.showontimeline=0 
                        ) as FollowerCount  
                        from events 
                        inner join users on users.userid=events.userid   
                        where 
                        ( 
                            events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())-31,0)
                            and events.LastUpdatedDate <= dateadd(day,datediff(day,0,getdate())-14,0) 
                        )   and events.deleted=0 and events.EventAchieved=0 and events.PrivateEvent=0  
                            and events.UserId IN (SELECT UserID FROM Events WHERE EventId = " + eId + @") 
                        ORDER BY events.CreatedDate DESC ";
                    break;
            }

            //+ " where month(events.lastupdateddate)= month('" + dtLMnth + "') and year(events.lastupdateddate)= year('" + dtLMnth + "') "

            DbDataReader rdrLastMonth = cmd.ExecuteReader();

            if (rdrLastMonth == null)
            {
                return;
            }
            while (rdrLastMonth.Read())
            {
                var eventID = int.Parse(rdrLastMonth["EventID"].ToString());
                var eventName = (string) rdrLastMonth["EventName"];
                var fname = (string) rdrLastMonth["FirstName"];
                var lname = (string) rdrLastMonth["LastName"];
                var memCount = rdrLastMonth["MemberCount"].ToString();
                var folCount = rdrLastMonth["FollowerCount"].ToString();
                var eventHyperlink = new HyperLink
                                         {
                                             Text =
                                                 GetSubString(eventName, 100) + " <span style=color:grey>" + fname + " " +
                                                 lname + "</span> ",
                                             NavigateUrl = "~/viewEvent.aspx?EID=" + eventID
                                         };
                eventHyperlink.Attributes.Add("class", "event");
                PlaceHolderLastMonth.Controls.Add(eventHyperlink);
                var ltEvent = new Literal();
                var memberFollowerString = " - ";
                if (memCount != "0")
                {
                    memberFollowerString += memCount == "1" ? memCount + " member " : memCount + " members ";
                }
                if (folCount != "0")
                {
                    memberFollowerString += memCount == "1" ? folCount + " follower " : folCount + " followers ";
                }
                ltEvent.Text = "<span style=color:#cccccc>" + memberFollowerString + "</span>";
                PlaceHolderLastMonth.Controls.Add(ltEvent);
                PlaceHolderLastMonth.Controls.Add(new LiteralControl("<br/>"));
            }
            rdrLastMonth.Close();
        }
    }

    private void GetLastSixMonth()
    {
        SqlCommand cmd;
        using (var conn = new SqlConnection((string)Application["connectionString"]))
        {
            conn.Open();
            cmd = new SqlCommand("", conn) {CommandType = CommandType.Text};

            switch (Request["type"].ToLower())
            {
                case "achieved":
                    cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                                      + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                                      + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                                      + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                                      + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                                      + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                                      + " from events inner join users on users.userid=events.userid  "
                                      +
                                      " where ( events.EventAchievedDate >= dateadd(day,datediff(day,0,getdate())-182,0) "
                                      +
                                      " and events.EventAchievedDate <= dateadd(day,datediff(day,0,getdate())-31,0) ) "
                                      + " and events.deleted=0 "
                                      + " and events.EventAchieved=1 "
                                      + " and events.PrivateEvent=0 "
                                      + " ORDER BY events.EventAchievedDate DESC ";
                    break;
                case "latest":
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
                    break;
                case "updated":
                    cmd.CommandText = "select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,(SELECT count(1) "
                                      + " FROM TrackedEvents T   JOIN Users U   ON T.UserID = U.UserID  "
                                      + " WHERE T.EventID = events.eventid   AND U.Deleted = 0 and T.showontimeline=1 "
                                      + " ) as MemberCount , (SELECT count(1)  FROM TrackedEvents T  "
                                      + " JOIN Users U   ON T.UserID = U.UserID   WHERE T.EventID = events.eventid  "
                                      + " AND U.Deleted = 0 and T.showontimeline=0 ) as FollowerCount "
                                      + " from events inner join users on users.userid=events.userid  "
                                      +
                                      " where ( events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())-182,0) "
                                      + " and events.LastUpdatedDate <= dateadd(day,datediff(day,0,getdate())-31,0) ) "
                                      + " and events.deleted=0 "
                                      + " and events.EventAchieved=0 "
                                      + " and events.PrivateEvent=0 "
                                      + " and events.CreatedDate <> events.LastUpdatedDate "
                                      + " ORDER BY events.LastUpdatedDate DESC ";
                    break;
                case "similar":
                    using (var sedogoDbEntities = new SedogoDbEntities())
                    {
                        var eventId = Convert.ToInt32(Request["EID"]);
                        var searchWord = sedogoDbEntities.Events.First(x => x.EventID == eventId).EventName;

                        cmd.CommandText =
                            @"select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,
                                    (	
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=1  
                                    ) as MemberCount, 
                                    (
	                                    SELECT count(1)  
	                                    FROM TrackedEvents T   
	                                    JOIN Users U   ON T.UserID = U.UserID   
	                                    WHERE T.EventID = events.eventid   
		                                    AND U.Deleted = 0 
		                                    and T.showontimeline=0 
                                    ) as FollowerCount  
                                    from events 
                                    inner join users on users.userid=events.userid 
                                    INNER JOIN FREETEXTTABLE([Events], EventName, '" + searchWord + @"') AS KEY_TBL ON events.EventID = KEY_TBL.[KEY]  
                                    where 
                                    ( 
                                        events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())-182,0) 
                                        and events.LastUpdatedDate <= dateadd(day,datediff(day,0,getdate())-31,0)
                                    )   and events.deleted=0 and events.EventAchieved=0 and events.PrivateEvent=0 
                                    ORDER BY KEY_TBL.RANK DESC ";
                    }
                    break;
                case "other":

                    var eId = Convert.ToInt32(Request["EID"]);
                    cmd.CommandText =
                        @"select top 20 events.eventid,events.eventname,users.FirstName,users.LastName,
                        (	
	                        SELECT count(1)  
	                        FROM TrackedEvents T   
	                        JOIN Users U ON T.UserID = U.UserID   
	                        WHERE T.EventID = events.eventid   
		                        AND U.Deleted = 0 
		                        and T.showontimeline=1  
                        ) as MemberCount, 
                        (
	                        SELECT count(1)  
	                        FROM TrackedEvents T   
	                        JOIN Users U   ON T.UserID = U.UserID   
	                        WHERE T.EventID = events.eventid   
		                        AND U.Deleted = 0 
		                        and T.showontimeline=0 
                        ) as FollowerCount  
                        from events 
                        inner join users on users.userid=events.userid   
                        where 
                        ( 
                            events.LastUpdatedDate >= dateadd(day,datediff(day,0,getdate())-182,0) 
                            and events.LastUpdatedDate <= dateadd(day,datediff(day,0,getdate())-31,0)
                        )   and events.deleted=0 and events.EventAchieved=0 and events.PrivateEvent=0  
                            and events.UserId IN (SELECT UserID FROM Events WHERE EventId = " + eId + @")
                        ORDER BY events.CreatedDate DESC ";
                    break;
            }

            DbDataReader rdrLastSixMonth = cmd.ExecuteReader();
            //    + " where convert(datetime,convert(varchar,events.lastupdateddate,102)) >=(select convert(datetime, convert(varchar,'" + dtLMnth + "',102))) "

            if (rdrLastSixMonth == null)
            {
                return;
            }
            while (rdrLastSixMonth.Read())
            {
                var eventID = int.Parse(rdrLastSixMonth["EventID"].ToString());
                var eventName = (string) rdrLastSixMonth["EventName"];
                var fname = (string) rdrLastSixMonth["FirstName"];
                var lname = (string) rdrLastSixMonth["LastName"];
                var memCount = rdrLastSixMonth["MemberCount"].ToString();
                var folCount = rdrLastSixMonth["FollowerCount"].ToString();
                var eventHyperlink = new HyperLink
                                         {
                                             Text =
                                                 GetSubString(eventName, 100) + " <span style=color:grey>" + fname + " " +
                                                 lname + "</span>",
                                             NavigateUrl = "~/viewEvent.aspx?EID=" + eventID
                                         };
                eventHyperlink.Attributes.Add("class", "event");
                PlaceHolderLastSixMonth.Controls.Add(eventHyperlink);
                var ltEvent6 = new Literal();
                var memberFollowerString = " - ";
                if (memCount != "0")
                {
                    memberFollowerString += memCount == "1" ? memCount + " member " : memCount + " members ";
                }
                if (folCount != "0")
                {
                    memberFollowerString += memCount == "1" ? folCount + " follower " : folCount + " followers ";
                }
                ltEvent6.Text = "<span style=color:#cccccc>" + memberFollowerString + "</span>";
                PlaceHolderLastSixMonth.Controls.Add(ltEvent6);
                PlaceHolderLastSixMonth.Controls.Add(new LiteralControl("<br/>"));
            }

            rdrLastSixMonth.Close();
        }
    }
    private static string GetSubString(string inputstr, Int32 length)
    {
        return inputstr.Length > length ? inputstr.Substring(0, length) + "..." : inputstr;
    }
}

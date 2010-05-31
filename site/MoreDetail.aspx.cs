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
using System.IO;
using Sedogo.BusinessObjects;
using System.Collections.Generic;
using System.Linq;

public partial class MoreDetail : SedogoPage
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
            userID = int.Parse(Session["loggedInUserID"].ToString());

            Boolean viewArchivedEvents = false;
            if (Session["ViewArchivedEvents"] != null)
            {
                viewArchivedEvents = (Boolean)Session["ViewArchivedEvents"];
            }

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
            sidebarControl.userID = userID;
            sidebarControl.user = user;
            bannerAddFindControl.userID = userID;

            //Changes By Rahul
            timelinebDate.Text = user.birthday.Year.ToString();

            timelineURL.Text = "timelineXML.aspx?G=" + Guid.NewGuid().ToString();

            if (Session["EventInviteGUID"] != null)
            {
                string inviteGUID = Session["EventInviteGUID"].ToString();

                if (inviteGUID != "")
                {
                    //int eventInviteID = EventInvite.GetEventInviteIDFromGUID(inviteGUID);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"invite.aspx\");", true);
                    Session["EventInviteGUID"] = "";
                    Session["EventInviteUserID"] = "";
                }
            }

            DateTime timelineStartDate = DateTime.Now.AddMonths(8);

            timelineStartDate1.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");     // "Jan 08 2010 00:00:00 GMT"
            timelineStartDate2.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");

            if (Session["DefaultRedirect"] != null && Session["DefaultRedirect"].ToString() != "")
            {
                string redir = (string)Session["DefaultRedirect"];
                Session["DefaultRedirect"] = "";
                if (redir == "Messages")
                {
                    Response.Redirect("message.aspx");
                }
                if (redir == "Requests")
                {
                    Response.Redirect("eventJoinRequests.aspx");
                }
            }
            if (Session["EventID"] != null && Session["EventID"].ToString() != "")
            {
                Session["EventID"] = "";
                Response.Redirect("viewEvent.aspx?EID=" + Session["EventID"].ToString());
            }

            keepAliveIFrame.Attributes.Add("src", this.ResolveClientUrl("~/keepAlive.aspx"));
            
            GetToday();
            GetThisWeek();
            GetTwoWeekAgo();
            GetLastMonth();
            GetLastSixMonth();
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
        Response.Redirect("profile.aspx");
    }

    private void GetToday()
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        conn.Open();
        SqlCommand cmd = new SqlCommand("", conn);
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select *,users.FirstName,users.LastName from events inner join users on users.userid=events.userid where convert(datetime,convert(varchar,events.lastupdateddate,102)) = convert(datetime,convert(varchar,getdate(),102)) and events.deleted=0 and events.userid=" + userID;

        DbDataReader rdrToday = cmd.ExecuteReader();

        while (rdrToday.Read())
        {
            int eventID = int.Parse(rdrToday["EventID"].ToString());
            string eventName = (string)rdrToday["EventName"];
            string Fname = (string)rdrToday["FirstName"];
            string Lname = (string)rdrToday["LastName"];

            HyperLink eventHyperlink = new HyperLink();
            eventHyperlink.Text = eventName + "- <span style=color:grey>" + Fname + " " + Lname + "</p> ";
            eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
            PlaceHolderToday.Controls.Add(eventHyperlink);
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
        cmd.CommandText = "select *,users.FirstName,users.LastName from events inner join users on users.userid=events.userid where convert(datetime,convert(varchar,events.lastupdateddate,102)) >= (select  convert(datetime,convert(varchar, DATEADD(wk, DATEDIFF(wk, 0, GETDATE()-1), 0),102))) and events.deleted=0 and events.userid=" + userID;

        DbDataReader rdrThisWeek = cmd.ExecuteReader();

        while (rdrThisWeek.Read())
        {
            int eventID = int.Parse(rdrThisWeek["EventID"].ToString());
            string eventName = (string)rdrThisWeek["EventName"];
            string Fname = (string)rdrThisWeek["FirstName"];
            string Lname = (string)rdrThisWeek["LastName"];

            HyperLink eventHyperlink = new HyperLink();
            eventHyperlink.Text = eventName + " - <span style=color:grey>" + Fname + " " + Lname + "</span> ";
            eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
            PlaceHolderThisWeek.Controls.Add(eventHyperlink);
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

        cmd.CommandText = "select *,users.FirstName,users.LastName from events inner join users on users.userid=events.userid where convert(datetime,convert(varchar,events.lastupdateddate,102)) >= convert(datetime, convert(varchar,'" + dtAgo + "',102)) and events.deleted=0 and events.userid=" + userID;

        DbDataReader rdrTwoWeekAgo = cmd.ExecuteReader();

        while (rdrTwoWeekAgo.Read())
        {
            int eventID = int.Parse(rdrTwoWeekAgo["EventID"].ToString());
            string eventName = (string)rdrTwoWeekAgo["EventName"];
            string Fname = (string)rdrTwoWeekAgo["FirstName"];
            string Lname = (string)rdrTwoWeekAgo["LastName"];

            HyperLink eventHyperlink = new HyperLink();
            eventHyperlink.Text = eventName + " - <span style=color:grey>" + Fname + " " + Lname + "</span> ";
            eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
            PlaceHolderTwoWeekAgo.Controls.Add(eventHyperlink);
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

        cmd.CommandText = "select *,users.FirstName,users.LastName from events inner join users on users.userid=events.userid where month(events.lastupdateddate)= month('" + dtLMnth + "') and year(events.lastupdateddate)= year('" + dtLMnth + "') and events.deleted=0  and events.userid=" + userID;

        DbDataReader rdrLastMonth = cmd.ExecuteReader();

        while (rdrLastMonth.Read())
        {
            int eventID = int.Parse(rdrLastMonth["EventID"].ToString());
            string eventName = (string)rdrLastMonth["EventName"];
            string Fname = (string)rdrLastMonth["FirstName"];
            string Lname = (string)rdrLastMonth["LastName"];

            HyperLink eventHyperlink = new HyperLink();
            eventHyperlink.Text = eventName + " - <span style=color:grey>" + Fname + " " + Lname + "</span> ";
            eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
            PlaceHolderLastMonth.Controls.Add(eventHyperlink);
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

        string dtLMnth = DateTime.Now.Date.AddMonths(-6).ToString("yyyy-MM-dd"); ;

        cmd.CommandText = "select *,users.FirstName,users.LastName from events inner join users on users.userid=events.userid where convert(datetime,convert(varchar,events.lastupdateddate,102)) >=(select convert(datetime, convert(varchar,'" + dtLMnth + "',102))) and events.deleted=0  and events.userid=" + userID;

        DbDataReader rdrLastSixMonth = cmd.ExecuteReader();

        while (rdrLastSixMonth.Read())
        {
            int eventID = int.Parse(rdrLastSixMonth["EventID"].ToString());
            string eventName = (string)rdrLastSixMonth["EventName"];
            string Fname = (string)rdrLastSixMonth["FirstName"];
            string Lname = (string)rdrLastSixMonth["LastName"];

            HyperLink eventHyperlink = new HyperLink();
            eventHyperlink.Text = eventName + " - <span style=color:grey>" + Fname + " " + Lname + "</span> ";
            eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
            PlaceHolderLastSixMonth.Controls.Add(eventHyperlink);
            PlaceHolderLastSixMonth.Controls.Add(new LiteralControl("<br/>"));
        }

        rdrLastSixMonth.Close();
    }
}

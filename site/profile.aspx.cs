//===============================================================
// Filename: profile.aspx.cs
// Date: 04/09/09
// --------------------------------------------------------------
// Description:
//   Profile
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 04/09/09
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

public partial class profile : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Session["loggedInUserID"].ToString());

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
            userNameLabel.Text = user.fullName;

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
                messageCountLink.Text = "You have " + messageCount.ToString() + " new message";
            }
            else
            {
                messageCountLink.Text = "You have " + messageCount.ToString() + " new messages";
            }
            inviteCountLink.Text = "You have 0 new invites";
            alertCountLink.Text = "You have 0 alerts";
            groupCountLink.Text = "You belong to 0 groups";

            PopulateEvents(user);
        }
    }

    //===============================================================
    // Function: PopulateEvents
    //===============================================================
    protected void PopulateEvents(SedogoUser user)
    {
        int userID = int.Parse(Session["loggedInUserID"].ToString());
        Boolean viewArchivedEvents = false;
        if (Session["ViewArchivedEvents"] != null)
        {
            viewArchivedEvents = (Boolean)Session["ViewArchivedEvents"];
        }

        if( viewArchivedEvents == true )
        {
            viewArchiveLink.Text = "hide archive";
        }
        else
        {
            viewArchiveLink.Text = "view archive";
        }

        DateTime todayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
            0, 0, 0);
        DateTime todayPlus1Day = todayStart.AddDays(1);
        DateTime todayPlus7Days = todayStart.AddDays(7);
        DateTime todayPlus1Month = todayStart.AddMonths(1);
        DateTime todayPlus1Year = todayStart.AddYears(1);
        DateTime todayPlus2Years = todayStart.AddYears(2);
        DateTime todayPlus3Years = todayStart.AddYears(3);
        DateTime todayPlus4Years = todayStart.AddYears(4);
        DateTime todayPlus5Years = todayStart.AddYears(5);
        DateTime todayPlus10Years = todayStart.AddYears(10);
        DateTime todayPlus20Years = todayStart.AddYears(20);
        DateTime todayPlus100Years = todayStart.AddYears(100);

        todaysDateLabel.Text = todayStart.ToString("dd/MM/yyyy");

        StringBuilder timelineItems1String = new StringBuilder();
        StringBuilder timelineItems2String = new StringBuilder();
        int timelineItemNumber = 1;
        int currentCategoryID = 0;

        decimal timelineScale = 1M;  // pixels per day on the timeline

        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (viewArchivedEvents == true)
            {
                cmd.CommandText = "spSelectFullEventListIncludingAchievedByCategory";
            }
            else
            {
                cmd.CommandText = "spSelectFullEventListByCategory";
            }
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int categoryID = 1;
                string dateType = "D";
                DateTime startDate = DateTime.MinValue;
                DateTime rangeStartDate = DateTime.MinValue;
                DateTime rangeEndDate = DateTime.MinValue;
                int beforeBirthday = -1;
                Boolean privateEvent = false;
                Boolean eventAchieved = false;

                DateTime timelineStartDate = DateTime.MinValue;
                DateTime timelineEndDate = DateTime.MinValue;

                int eventID = int.Parse(rdr["EventID"].ToString());
                string eventName = (string)rdr["EventName"];
                if (!rdr.IsDBNull(rdr.GetOrdinal("DateType")))
                {
                    dateType = (string)rdr["DateType"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("StartDate")))
                {
                    startDate = (DateTime)rdr["StartDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("RangeStartDate")))
                {
                    rangeStartDate = (DateTime)rdr["RangeStartDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("RangeEndDate")))
                {
                    rangeEndDate = (DateTime)rdr["RangeEndDate"];
                }
                eventAchieved = (Boolean)rdr["EventAchieved"];
                if (!rdr.IsDBNull(rdr.GetOrdinal("CategoryID")))
                {
                    categoryID = int.Parse(rdr["CategoryID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("BeforeBirthday")))
                {
                    beforeBirthday = int.Parse(rdr["BeforeBirthday"].ToString());
                }
                privateEvent = (Boolean)rdr["PrivateEvent"];

                if (dateType == "D")
                {
                    // Event occurs on a specific date

                    timelineStartDate = startDate;
                    timelineEndDate = startDate.AddDays(28);        // Add 28 days so it shows up
                }
                if (dateType == "R")
                {
                    // Event occurs in a date range - use the start date

                    timelineStartDate = rangeStartDate;
                    timelineEndDate = rangeEndDate;

                    TimeSpan ts = timelineEndDate - timelineStartDate;
                    if (ts.Days < 28)
                    {
                        timelineEndDate = startDate.AddDays(28);        // Add 28 days so it shows up
                    }

                    startDate = rangeStartDate;
                }
                if (dateType == "A")
                {
                    // Event occurs before birthday

                    timelineStartDate = DateTime.Now;
                    if (user.birthday > DateTime.MinValue)
                    {
                        timelineEndDate = user.birthday.AddYears(beforeBirthday);

                        TimeSpan ts = timelineEndDate - DateTime.Now;   // timelineStartDate.AddYears(beforeBirthday);
                        if (ts.Days < 0)
                        {
                            // Birthday was in the past
                            timelineStartDate = DateTime.Now;
                            timelineEndDate = timelineStartDate.AddDays(28);        // Add 28 days so it shows up

                            // Set start date so event is correctly placed below
                            startDate = DateTime.Now.AddDays(ts.Days);
                        }
                        else if (ts.Days >= 0 && ts.Days < 28)
                        {
                            // Birthday is within 28 days - extend the timeline a bit
                            timelineEndDate = timelineStartDate.AddDays(28);        // Add 28 days so it shows up

                            startDate = timelineStartDate;
                        }
                        else
                        {
                            startDate = timelineStartDate;
                        }
                    }
                    else
                    {
                        timelineEndDate = DateTime.Now.AddDays(28);
                    }
                }

                StringBuilder eventString = new StringBuilder();
                eventString.Append("<div class=\"event");
                if( categoryID > 0 )
                {
                    eventString.Append(" highlight-group-" + categoryID.ToString());
                }
                eventString.AppendLine("\">");
                eventString.AppendLine("<h3>" + eventName);
                if( eventAchieved == true )
                {
                    eventString.Append(" (achieved)");
                }
                if (privateEvent == true)
                {
                    eventString.AppendLine("<img src=\"./images/icons/16-security-lock.png\" alt=\"Private event\" />");
                }
                eventString.Append("</h3>");
                eventString.AppendLine("<p><a href=\"viewEvent.aspx?EID=" + eventID.ToString() + "\" title=\"\" class=\"modal\">View</a></p>");
                //eventString.AppendLine("<p class=\"warning\">Note to self: book tickets</p>");
                eventString.AppendLine("</div>");

                TimeSpan startTS = timelineStartDate - DateTime.Now;
                if (startTS.Days < 0)
                {
                    startTS = new TimeSpan(0);
                }
                TimeSpan durationTS = timelineEndDate - timelineStartDate;
                if (durationTS.Days < 0)
                {
                    startTS = new TimeSpan(0);
                }
                int lineWidth = (int)decimal.Truncate((durationTS.Days * timelineScale));
                int lineLeftOffset = (int)decimal.Truncate((startTS.Days * timelineScale));

                timelineItems1String.AppendLine("$(\"#" + timelineItemNumber.ToString() 
                    + "\").css(\"width\",\"" + lineWidth.ToString() 
                    + "px\").css(\"left\",\"" + lineLeftOffset.ToString() + "px\");");

                if( currentCategoryID != categoryID )
                {
                    if( currentCategoryID != 0 )
                    {
                        timelineItems2String.AppendLine("</div>");
                    }
                    timelineItems2String.AppendLine("<div class=\"row-container category-" + categoryID.ToString() + "\">");
                }
                timelineItems2String.AppendLine("<div class=\"row\">");
                timelineItems2String.AppendLine("<div class=\"tl\" id=\"" + timelineItemNumber.ToString() + "\">" + eventName);
                timelineItems2String.AppendLine("</div>");
                timelineItems2String.AppendLine("</div>");

                timelineItemNumber++;

                // Use the timeline start date as this has been adjusted above
                if (startDate < todayStart && startDate != DateTime.MinValue)
                {
                    overdueEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayStart && startDate < todayPlus1Day)
                {
                    todayEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus1Day && startDate < todayPlus7Days)
                {
                    thisWeekEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus7Days && startDate < todayPlus1Month)
                {
                    thisMonthEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus1Month && startDate < todayPlus1Year)
                {
                    nextYearEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus1Year && startDate < todayPlus2Years)
                {
                    next2YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus2Years && startDate < todayPlus3Years)
                {
                    next3YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus3Years && startDate < todayPlus4Years)
                {
                    next4YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus4Years && startDate < todayPlus5Years)
                {
                    next5YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus5Years && startDate < todayPlus10Years)
                {
                    next10YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus10Years && startDate < todayPlus20Years)
                {
                    next20YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus20Years && startDate < todayPlus100Years)
                {
                    next100YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate == DateTime.MinValue)
                {
                    notScheduledEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }

                currentCategoryID = categoryID;
            }
            rdr.Close();
            if( currentCategoryID != 0 )
            {
                timelineItems2String.AppendLine("</div>");
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

        timelineItems1.Text = timelineItems1String.ToString();
        timelineItems2.Text = timelineItems2String.ToString();
    }

    //===============================================================
    // Function: searchButton_click
    //===============================================================
    protected void searchButton_click(object sender, EventArgs e)
    {
        string searchText = what.Text;

        if (searchText.Trim() == "" || searchText.Trim() == "e.g. climb Everest")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a search term\");", true);
        }
        else
        {
            if (searchText.Length > 3)
            {
                Response.Redirect("search.aspx?Search=" + searchText.ToString());
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a longer search term\");", true);
            }
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

        SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), 
            int.Parse(Session["loggedInUserID"].ToString()));
        PopulateEvents(user);
    }
}

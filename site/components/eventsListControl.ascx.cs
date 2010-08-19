//===============================================================
// Filename: eventsListControl.ascx.cs
// Date: 06/04/10
// --------------------------------------------------------------
// Description:
//   Events list
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 06/04/10
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

public partial class components_eventsListControl : System.Web.UI.UserControl
{
    public int userID;
    public SedogoUser user;
    public Boolean viewArchivedEvents = false;

    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateEvents();
        }
    }

    //===============================================================
    // Function: PopulateEvents
    //===============================================================
    protected void PopulateEvents()
    {
        int userID = int.Parse(Session["loggedInUserID"].ToString());

        if (Session["ViewArchivedEvents"] != null)
        {
            viewArchivedEvents = (Boolean)Session["ViewArchivedEvents"];
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

        todaysDateLabel.Text = "Today: " + todayStart.ToString("ddd d MMMM yyyy");

        int numOverdueEvents = 0;
        int numTodayEvents = 0;
        int numThisWeekEvents = 0;
        int numThisMonthEvents = 0;
        int numThisYearEvents = 0;
        int numNext2YearsEvents = 0;
        int numNext3YearsEvents = 0;
        int numNext4YearsEvents = 0;
        int numNext5YearsEvents = 0;
        int numNext10YearsEvents = 0;
        int numNext20YearsEvents = 0;
        int numNext100YearsEvents = 0;
        int numNotScheduledEvents = 0;

        SearchHistory searchHistory = new SearchHistory("");
        searchHistory.searchDate = DateTime.Now;
        searchHistory.searchText = "";
        searchHistory.userID = userID;
        int rowCount = 0;

        StringBuilder stateString = new StringBuilder();

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
            if (viewArchivedEvents == false)
            {
                cmd.Parameters.Add("@ShowPrivate", SqlDbType.Bit).Value = true;
            }
            //cmd.CommandText = "spSearchEvents";
            //cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            //cmd.Parameters.Add("@SearchText", SqlDbType.NVarChar, 1000).Value = searchText;
            DbDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows == true)
            {
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
                    string eventPicThumbnail = "";

                    int eventID = int.Parse(rdr["EventID"].ToString());
                    int eventUserID = int.Parse(rdr["UserID"].ToString());
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
                    if (!rdr.IsDBNull(rdr.GetOrdinal("EventPicThumbnail")))
                    {
                        eventPicThumbnail = (string)rdr["EventPicThumbnail"];
                    }

                    string dateString = "";
                    if (dateType == "D")
                    {
                        // Event occurs on a specific date
                        dateString = startDate.ToString("ddd d MMMM yyyy");
                    }
                    if (dateType == "R")
                    {
                        // Event occurs in a date range - use the start date
                        dateString = rangeStartDate.ToString("ddd d MMMM yyyy") + " to " + rangeEndDate.ToString("ddd d MMMM yyyy");

                        startDate = rangeEndDate;
                        //startDate = rangeStartDate;
                    }
                    if (dateType == "A")
                    {
                        // Event occurs before birthday
                        string dateSuffix = "";
                        switch (beforeBirthday)
                        {
                            case 1: case 21: case 31: case 41: case 51: case 61: case 71: case 81: case 91: case 101:
                                dateSuffix = "st";
                                break;
                            case 2: case 22: case 32: case 42: case 52: case 62: case 72: case 82: case 92: case 102:
                                dateSuffix = "nd";
                                break;
                            case 3: case 23: case 33: case 43: case 53: case 63: case 73: case 83: case 93: case 103:
                                dateSuffix = "rd";
                                break;
                            case 4: case 5: case 6: case 7: case 8: case 9: case 10:
                            case 11: case 12: case 13: case 14: case 15: case 16: case 17: case 18: case 19: case 20:
                            case 24: case 25: case 26: case 27: case 28: case 29: case 30:
                            case 34: case 35: case 36: case 37: case 38: case 39: case 40:
                            case 44: case 45: case 46: case 47: case 48: case 49: case 50:
                            case 54: case 55: case 56: case 57: case 58: case 59: case 60:
                            case 64: case 65: case 66: case 67: case 68: case 69: case 70:
                            case 74: case 75: case 76: case 77: case 78: case 79: case 80:
                            case 84: case 85: case 86: case 87: case 88: case 89: case 90:
                            case 94: case 95: case 96: case 97: case 98: case 99: case 100:
                            case 104: case 105: case 106: case 107: case 108: case 109: case 110:
                                dateSuffix = "th";
                                break;
                            default:
                                break;
                        }
                        dateString = "Before " + beforeBirthday.ToString() + dateSuffix + " birthday";

                        startDate = DateTime.Now;
                        if (userID != eventUserID)
                        {
                            // Use the event users birthday
                            SedogoUser eventUser = new SedogoUser("", eventUserID);
                            if (eventUser.birthday > DateTime.MinValue)
                            {
                                startDate = eventUser.birthday.AddYears(beforeBirthday);
                            }
                        }
                        else
                        {
                            // My event/my birthday
                            if (user.birthday > DateTime.MinValue)
                            {
                                startDate = user.birthday.AddYears(beforeBirthday);
                            }
                        }
                    }

                    string timelineColour = "#cd3301";
                    switch (categoryID)
                    {
                        case 1:
                            timelineColour = "#cd3301";
                            break;
                        case 2:
                            timelineColour = "#ff0b0b";
                            break;
                        case 3:
                            timelineColour = "#ff6801";
                            break;
                        case 4:
                            timelineColour = "#ff8500";
                            break;
                        case 5:
                            timelineColour = "#d5b21a";
                            break;
                        case 6:
                            timelineColour = "#8dc406";
                            break;
                        case 7:
                            timelineColour = "#5b980c";
                            break;
                        case 8:
                            timelineColour = "#079abc";
                            break;
                        case 9:
                            timelineColour = "#5ab6cd";
                            break;
                        case 10:
                            timelineColour = "#8a67c1";
                            break;
                        case 11:
                            timelineColour = "#e54ecf";
                            break;
                        case 12:
                            timelineColour = "#a5369c";
                            break;
                        case 13:
                            timelineColour = "#a32672";
                            break;
                    }

                    int eventAlertCount = EventAlert.GetEventAlertCountPending(eventID);
                    int trackingUserCount = SedogoEvent.GetTrackingUserCount(eventID);
                    int joinedUserCount = SedogoEvent.GetMemberUserCount(eventID);

                    StringBuilder eventString = new StringBuilder();
                    eventString.Append("<div class=\"event");
                    //if( categoryID > 0 )  // Show border colour on event
                    //{
                    //    eventString.Append(" highlight-group-" + categoryID.ToString());
                    //}
                    eventString.AppendLine("\">");
                    eventString.AppendLine("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" ");
                    eventString.AppendLine("onMouseOver=\"setColor('colourBar_" + eventID.ToString() + "','" + timelineColour + "');\" ");
                    eventString.AppendLine("onMouseOut=\"setColor('colourBar_" + eventID.ToString() + "','#FFFFFF');\" ");
                    eventString.AppendLine("onclick=\"showhideGoal('goalDiv_" + eventID.ToString() + "');\" ");
                    eventString.AppendLine(">");
                    eventString.AppendLine("<tr style=\"background-color:#EEEEEE\" >");
                    eventString.AppendLine("<td>");
                        eventString.AppendLine("<p class=\"eventListText\"><b>");
                        //if (eventAchieved == true)
                        //{
                        //    eventString.AppendLine(" <img src=\"./images/acceptachieve.gif\" alt=\"Achieved\" />");
                        //}
                        eventString.Append(eventName);
                        eventString.Append("</b></p>");
                    eventString.AppendLine("</td>");
                    eventString.AppendLine("<td align=\"right\">");
                        if (eventAchieved == true)
                        {
                            eventString.AppendLine(" <img src=\"./images/acceptachieve.gif\" alt=\"Achieved\" />");
                        }
                        if (privateEvent == true)
                        {
                            eventString.AppendLine("<img src=\"./images/private.gif\" alt=\"Private event\" />");
                        }
                        if (eventAlertCount > 0)
                        {
                            eventString.AppendLine("<img src=\"./images/reminder.gif\" alt=\"Alert\" />");
                        }
                    eventString.AppendLine("</td>");
                    eventString.AppendLine("</tr>");
                    eventString.AppendLine("<tr height=\"6px\"><td colspan=\"2\" bgcolor=\"#FFFFFF\" id=\"colourBar_" + eventID.ToString() + "\" >");
                    eventString.AppendLine("<img src=\"/images/1x1trans.gif\" height=\"6px\" >");
                    eventString.AppendLine("</td></tr>");
                    eventString.AppendLine("</table>");
                    eventString.AppendLine("<div id=\"goalDiv_" + eventID.ToString() + "\" style=\"display:none\">");
                    eventString.AppendLine("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" >");
                    eventString.AppendLine("<tr>");
                    eventString.AppendLine("<td>");

                    eventString.AppendLine("<p>" + dateString + "</p>");
                    eventString.AppendLine("<p>");
                    if (joinedUserCount > 0)
                    {
                        eventString.AppendLine(joinedUserCount.ToString());
                        if (joinedUserCount == 1)
                        {
                            eventString.AppendLine(" member");
                        }
                        else
                        {
                            eventString.AppendLine(" members");
                        }
                    }
                    if (trackingUserCount > 0)
                    {
                        if (joinedUserCount > 0)
                        {
                            eventString.AppendLine("<br/>");
                        }
                        eventString.AppendLine(trackingUserCount.ToString());
                        eventString.AppendLine(" following");
                    }
                    eventString.AppendLine("</p>");
                    eventString.AppendLine("<a href=\"viewEvent.aspx?EID=" + eventID.ToString()
                        + "\" title=\"\">View</a>");

                    eventString.AppendLine("</td>");
                    eventString.AppendLine("<td align=\"right\" style=\"padding-top:5px\">");
                    if (eventPicThumbnail == "")
                    {
                        eventString.AppendLine("<img src=\"./images/eventThumbnailBlank.png\" />");
                    }
                    else
                    {
                        eventString.AppendLine("<img src=\"./assets/eventPics/" + eventPicThumbnail + "\" />");
                    }
                    eventString.AppendLine("</td>");
                    eventString.AppendLine("</tr></table>");
                    eventString.AppendLine("</div>");
                    eventString.AppendLine("</div>");

                    // Use the timeline start date as this has been adjusted above
                    if (startDate < todayStart && startDate != DateTime.MinValue)
                    {
                        overdueEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                        numOverdueEvents++;
                    }
                    if (startDate >= todayStart && startDate < todayPlus1Day)
                    {
                        todayEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                        numTodayEvents++;
                    }
                    if (startDate >= todayPlus1Day && startDate < todayPlus7Days)
                    {
                        thisWeekEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                        numThisWeekEvents++;
                    }
                    if (startDate >= todayPlus7Days && startDate < todayPlus1Month)
                    {
                        thisMonthEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                        numThisMonthEvents++;
                    }
                    if (startDate >= todayPlus1Month && startDate < todayPlus1Year)
                    {
                        nextYearEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                        numThisYearEvents++;
                    }
                    if (startDate >= todayPlus1Year && startDate < todayPlus2Years)
                    {
                        next2YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                        numNext2YearsEvents++;
                    }
                    if (startDate >= todayPlus2Years && startDate < todayPlus3Years)
                    {
                        next3YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                        numNext3YearsEvents++;
                    }
                    if (startDate >= todayPlus3Years && startDate < todayPlus4Years)
                    {
                        next4YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                        numNext4YearsEvents++;
                    }
                    if (startDate >= todayPlus4Years && startDate < todayPlus5Years)
                    {
                        next5YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                        numNext5YearsEvents++;
                    }
                    if (startDate >= todayPlus5Years && startDate < todayPlus10Years)
                    {
                        next10YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                        numNext10YearsEvents++;
                    }
                    if (startDate >= todayPlus10Years && startDate < todayPlus20Years)
                    {
                        next20YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                        numNext20YearsEvents++;
                    }
                    if (startDate >= todayPlus20Years && startDate < todayPlus100Years)
                    {
                        next100YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                        numNext100YearsEvents++;
                    }
                    if (startDate == DateTime.MinValue)
                    {
                        notScheduledEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                        numNotScheduledEvents++;
                    }
                }
                rdr.Close();
            }

            searchHistory.searchHits = rowCount;
            searchHistory.Add();

            overdueTitleLabel.Visible = true;
            todaysDateLabel.Visible = true;
            thisWeekTitleLabel.Visible = true;
            thisMonthTitleLabel.Visible = true;
            thisYearTitleLabel.Visible = true;
            next2YearsTitleLabel.Visible = true;
            next3YearsTitleLabel.Visible = true;
            next4YearsTitleLabel.Visible = true;
            next5YearsTitleLabel.Visible = true;
            fiveToTenYearsTitleLabel.Visible = true;
            tenToTwentyYearsTitleLabel.Visible = true;
            twentyPlusYearsTitleLabel.Visible = true;
            unknownDateTitleLabel.Visible = true;

            if (numOverdueEvents == 0)
            {
                overdueTitleLabel.Visible = false;
            }
            if (numTodayEvents == 0)
            {
                todaysDateLabel.Visible = false;
            }
            if (numThisWeekEvents == 0)
            {
                thisWeekTitleLabel.Visible = false;
            }
            if (numThisMonthEvents == 0)
            {
                thisMonthTitleLabel.Visible = false;
            }
            if (numThisYearEvents == 0)
            {
                thisYearTitleLabel.Visible = false;
            }
            if (numNext2YearsEvents == 0)
            {
                next2YearsTitleLabel.Visible = false;
            }
            if (numNext3YearsEvents == 0)
            {
                next3YearsTitleLabel.Visible = false;
            }
            if (numNext4YearsEvents == 0)
            {
                next4YearsTitleLabel.Visible = false;
            }
            if (numNext5YearsEvents == 0)
            {
                next5YearsTitleLabel.Visible = false;
            }
            if (numNext10YearsEvents == 0)
            {
                fiveToTenYearsTitleLabel.Visible = false;
            }
            if (numNext20YearsEvents == 0)
            {
                tenToTwentyYearsTitleLabel.Visible = false;
            }
            if (numNext100YearsEvents == 0)
            {
                twentyPlusYearsTitleLabel.Visible = false;
            }
            if (numNotScheduledEvents == 0)
            {
                unknownDateTitleLabel.Visible = false;
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
}

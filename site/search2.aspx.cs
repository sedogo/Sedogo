//===============================================================
// Filename: search2.aspx.cs
// Date: 28/09/09
// --------------------------------------------------------------
// Description:
//   Search results
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 28/09/09
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

public partial class search2 : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Session["loggedInUserID"].ToString());
            string searchText = "";
            if (Request.QueryString["Search"] != null)
            {
                searchText = (string)Request.QueryString["Search"];
            }
            string eventNameText = "";
            if (Request.QueryString["EvName"] != null)
            {
                eventNameText = (string)Request.QueryString["EvName"];
            }
            string eventVenue = "";
            if (Request.QueryString["EvVenue"] != null)
            {
                eventVenue = (string)Request.QueryString["EvVenue"];
            }
            string eventOwnerName = "";
            if (Request.QueryString["EvOwner"] != null)
            {
                eventOwnerName = (string)Request.QueryString["EvOwner"];
            }
            int eventCategoryID = -1;
            if (Request.QueryString["EvCategoryID"] != null)
            {
                eventCategoryID = int.Parse(Request.QueryString["EvCategoryID"].ToString());
            }
            string dateSearch = "R";
            if (Request.QueryString["EvDateSearch"] != null)
            {
                dateSearch = (string)Request.QueryString["EvDateSearch"];
            }
            int beforeBirthday = -1;
            if (Request.QueryString["EvDateBDay"] != null)
            {
                beforeBirthday = int.Parse(Request.QueryString["EvDateBDay"].ToString());
            }
            DateTime dateSearchStartDate = DateTime.MinValue;
            if (Request.QueryString["EvDateStart"] != null)
            {
                try
                {
                    string[] s = Request.QueryString["EvDateStart"].ToString().Split('-');
                    dateSearchStartDate = new DateTime(int.Parse(s[2]), int.Parse(s[1]), int.Parse(s[0]));
                }
                catch { }
            }
            DateTime dateSearchEndDate = DateTime.MinValue;
            if (Request.QueryString["EvDateEnd"] != null)
            {
                try
                {
                    string[] s = Request.QueryString["EvDateEnd"].ToString().Split('-');
                    dateSearchEndDate = new DateTime(int.Parse(s[2]), int.Parse(s[1]), int.Parse(s[0]));
                }
                catch { }
            }
            int recentlyAdded = -1;
            if (Request.QueryString["EvRecentlyAdded"] != null)
            {
                recentlyAdded = int.Parse(Request.QueryString["EvRecentlyAdded"].ToString());
            }
            int recentlyUpdated = -1;
            if (Request.QueryString["EvRecentlyUpdated"] != null)
            {
                recentlyUpdated = int.Parse(Request.QueryString["EvRecentlyUpdated"].ToString());
            }
            string definitlyDo = "A";
            if (Request.QueryString["EvDefinitlyDo"] != null)
            {
                definitlyDo = (string)Request.QueryString["EvDefinitlyDo"];
            }

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
            int userAgeYears = DateTime.Now.Year - user.birthday.Year;

            for (int age = userAgeYears; age <= 100; age++)
            {
                birthdayDropDownList.Items.Add(new ListItem(age.ToString(), age.ToString()));

                if (user.birthday > DateTime.MinValue)
                {
                    int currentAge = DateTime.Now.Year - user.birthday.Year;
                    birthdayDropDownList.SelectedValue = currentAge.ToString();
                }
            }

            eventNameTextBox.Text = eventNameText;
            venueTextBox.Text = eventVenue;
            eventOwnerNameTextBox.Text = eventOwnerName;
            categoryDropDownList.SelectedValue = eventCategoryID.ToString();
            if (dateSearch == "R")
            {
                betweenDatesRadioButton.Checked = true;
                CalendarRangeStartDate.SelectedDate = dateSearchStartDate;
                PickerRangeStartDate.SelectedDate = dateSearchStartDate;
                CalendarRangeEndDate.SelectedDate = dateSearchEndDate;
                PickerRangeEndDate.SelectedDate = dateSearchEndDate;
            }
            if (dateSearch == "B")
            {
                beforeBirthdayRadioButton.Checked = true;
                birthdayDropDownList.SelectedValue = beforeBirthday.ToString();
            }
            recentlyAddedDropDownList.SelectedValue = recentlyAdded.ToString();
            recentlyUpdatedDropDownList.SelectedValue = recentlyUpdated.ToString();
            definitlyDoDropDownList.SelectedValue = definitlyDo;

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

            groupCountLink.Text = "<span>0</span> Groups";

            int trackedEventCount = TrackedEvent.GetTrackedEventCount(userID);
            trackingCountLink.Text = "<span>" + trackedEventCount.ToString() + "</span> Following";
            int pendingRequestsCount = SedogoEvent.GetPendingMemberUserCountByUserID(userID);
            if (pendingRequestsCount == 1)
            {
                goalJoinRequestsLink.Text = "<span>" + pendingRequestsCount.ToString() + "</span> Request";
            }
            else
            {
                goalJoinRequestsLink.Text = "<span>" + pendingRequestsCount.ToString() + "</span> Requests";
            }

            PopulateLatestSearches();

            //what2.Text = searchText;
            if (eventNameText == "")
            {
                eventNameTextBox.Text = searchText;
            }
            else
            {
                eventNameTextBox.Text = eventNameText;
                venueTextBox.Text = eventVenue;
                eventOwnerNameTextBox.Text = eventOwnerName;
            }

            SearchHistory searchHistory = new SearchHistory("");
            searchHistory.searchDate = DateTime.Now;
            if (searchText == "")
            {
                searchText = eventNameText + ", " + eventVenue + ", " + eventOwnerName;
                if (eventCategoryID > 0)
                {
                    string category = "";
                    switch (eventCategoryID)
                    {
                        case 1:
                            category = "Personal";
                            break;
                        case 2:
                            category = "Travel";
                            break;
                        case 3:
                            category = "Friends";
                            break;
                        case 4:
                            category = "Family";
                            break;
                        case 5:
                            category = "General";
                            break;
                        case 6:
                            category = "Health";
                            break;
                        case 7:
                            category = "Money";
                            break;
                        case 8:
                            category = "Education";
                            break;
                        case 9:
                            category = "Hobbies";
                            break;
                        case 10:
                            category = "Culture";
                            break;
                        case 11:
                            category = "Charity";
                            break;
                        case 12:
                            category = "Green";
                            break;
                        case 13:
                            category = "Misc";
                            break;
                    }
                    searchText += ", " + category;
                }
                if (beforeBirthday > 0)
                {
                    searchText += ", Before birthday: " + beforeBirthday.ToString();
                }
                if (dateSearchStartDate > DateTime.MinValue)
                {
                    searchText += ", From date: " + dateSearchStartDate.ToString("dd/MM/yyyy");
                }
                if (dateSearchEndDate > DateTime.MinValue)
                {
                    searchText += ", To date: " + dateSearchEndDate.ToString("dd/MM/yyyy");
                }
                searchHistory.searchText = searchText;
                searchResultsLabel.Text = "Search results for: " + searchText;
                searchForLiteral1.Text = searchText;
                searchForLiteral2.Text = searchText;
            }
            else
            {
                searchHistory.searchText = searchText;
                searchResultsLabel.Text = "Search results for: " + searchText;
                searchForLiteral1.Text = searchText;
                searchForLiteral2.Text = searchText;
            }
            searchHistory.userID = userID;
            searchHistory.searchHits = 0;
            searchHistory.Add();

            PopulateEvents(user);

            timelineURL.Text = "timelineXML.aspx?G=" + Guid.NewGuid().ToString();
            searchTimelineURL.Text = "timelineSearch2XML.aspx?G=" + Guid.NewGuid().ToString();
            searchTimelineURL.Text += "&Search=" + searchText;
            searchTimelineURL.Text += "&EvName=" + eventNameText;
            searchTimelineURL.Text += "&EvVenue=" + eventVenue;
            searchTimelineURL.Text += "&EvOwner=" + eventOwnerName;
            searchTimelineURL.Text += "&EvCategoryID=" + eventCategoryID.ToString();
            searchTimelineURL.Text += "&EvDateSearch=" + dateSearch;
            searchTimelineURL.Text += "&EvDateStart=" + dateSearchStartDate.ToString("dd-MM-yyyy");
            searchTimelineURL.Text += "&EvDateEnd=" + dateSearchEndDate.ToString("dd-MM-yyyy");
            searchTimelineURL.Text += "&EvDateBDay=" + beforeBirthday.ToString();
            searchTimelineURL.Text += "&EvRecentlyAdded=" + recentlyAdded.ToString();
            searchTimelineURL.Text += "&EvRecentlyUpdated=" + recentlyUpdated.ToString();
            searchTimelineURL.Text += "&EvDefinitlyDo=" + definitlyDo;

            int searchCount = GetSearchResultCount(searchText, eventNameText, eventVenue, eventOwnerName,
                eventCategoryID, dateSearch, beforeBirthday, 
                dateSearchStartDate, dateSearchEndDate,
                recentlyAdded, recentlyUpdated, definitlyDo);
            if (searchCount >= 50)
            {
                moreThan50ResultsDiv.Visible = true;
            }
            else
            {
                moreThan50ResultsDiv.Visible = false;
            }
            if (searchCount == 0)
            {
                noSearchResultsDiv.Visible = true;
            }
            else
            {
                noSearchResultsDiv.Visible = false;
            }

            DateTime timelineStartDate = DateTime.Now.AddMonths(8);

            timelineStartDate1.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");     // "Jan 08 2010 00:00:00 GMT"
            timelineStartDate2.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");
            timelineStartDate3.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");     // "Jan 08 2010 00:00:00 GMT"
            timelineStartDate4.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");

            what.Attributes.Add("onkeypress", "checkAddButtonEnter(event);");
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

        PopulateLatestSearches();
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

        if (viewArchivedEvents == true)
        {
            viewArchiveLink.Text = "Hide Past Goals";
        }
        else
        {
            viewArchiveLink.Text = "Past Goals";
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

                        startDate = rangeStartDate;
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

                        //timelineStartDate = DateTime.Now;
                        if (user.birthday > DateTime.MinValue)
                        {
                            startDate = user.birthday.AddYears(beforeBirthday);
                        }
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
                    eventString.AppendLine("<table width=\"100%\"><tr>");
                    eventString.AppendLine("<td>");
                    eventString.AppendLine("<h3>");
                    if (eventAchieved == true)
                    {
                        eventString.Append(" (Achieved)");
                    }
                    eventString.Append(eventName);

                    if (privateEvent == true)
                    {
                        eventString.AppendLine(" <img src=\"./images/privateIcon.jpg\" alt=\"Private event\" />");
                    }
                    if (eventAlertCount > 0)
                    {
                        eventString.AppendLine(" <img src=\"./images/ico_alerts.gif\" alt=\"Alert\" />");
                    }
                    eventString.Append("</h3>");

                    eventString.AppendLine("<p>" + dateString + "</p>");
                    eventString.AppendLine("<p>");
                    if (trackingUserCount > 0)
                    {
                        eventString.AppendLine(trackingUserCount.ToString());
                        eventString.AppendLine(" following this goal");
                    }
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
                    eventString.AppendLine("</p>");
                    eventString.AppendLine("<a href=\"viewEvent.aspx?EID=" + eventID.ToString()
                        + "\" title=\"\" class=\"modal\">View</a>");

                    eventString.AppendLine("</td>");
                    eventString.AppendLine("<td align=\"right\">");
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
            if (searchText.Length >= 2)
            {
                Response.Redirect("search2.aspx?Search=" + searchText.ToString());
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a longer search term\");", true);
            }
        }
    }

    //===============================================================
    // Function: advSearchButton_click
    //===============================================================
    protected void advSearchButton_click(object sender, EventArgs e)
    {
        string eventName = eventNameTextBox.Text;
        string venue = venueTextBox.Text;
        string eventOwnerName = eventOwnerNameTextBox.Text;
        int eventCategoryID = int.Parse(categoryDropDownList.SelectedValue);
        string dateSearch = "R";
        int beforeBirthday = -1;
        DateTime dateSearchStartDate = DateTime.MinValue;
        DateTime dateSearchEndDate = DateTime.MinValue;
        if( betweenDatesRadioButton.Checked == true )
        {
            dateSearchStartDate = CalendarRangeStartDate.SelectedDate;
            dateSearchEndDate = CalendarRangeEndDate.SelectedDate;
        }
        if( beforeBirthdayRadioButton.Checked == true )
        {
            dateSearch = "B";
            beforeBirthday = int.Parse(birthdayDropDownList.SelectedValue);
        }
        int recentlyAdded = int.Parse(recentlyAddedDropDownList.SelectedValue);
        int recentlyUpdated = int.Parse(recentlyUpdatedDropDownList.SelectedValue);
        string definitlyDo = definitlyDoDropDownList.SelectedValue;

        string url = "search2.aspx";
        url = url + "?EvName=" + eventName;
        url = url + "&EvVenue=" + venue;
        url = url + "&EvOwner=" + eventOwnerName;
        url = url + "&EvCategoryID=" + eventCategoryID.ToString();
        url = url + "&EvDateSearch=" + dateSearch;
        url = url + "&EvDateStart=" + dateSearchStartDate.ToString("dd-MM-yyyy");
        url = url + "&EvDateEnd=" + dateSearchEndDate.ToString("dd-MM-yyyy");
        url = url + "&EvDateBDay=" + beforeBirthday.ToString();
        url = url + "&EvRecentlyAdded=" + recentlyAdded.ToString();
        url = url + "&EvRecentlyUpdated=" + recentlyUpdated.ToString();
        url = url + "&EvDefinitlyDo=" + definitlyDo;

        Response.Redirect(url);
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
                searchHyperlink.NavigateUrl = "search2.aspx?Search=" + searchText;
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
                searchHyperlink.NavigateUrl = "search2.aspx?Search=" + searchText;
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
                eventHyperlink.NavigateUrl = "viewEvent.aspx?EID=" + eventID.ToString();
                eventHyperlink.CssClass = "modal";
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
    // Function: GetSearchResultCount
    //===============================================================
    private int GetSearchResultCount(string searchText, string eventNameText,
        string eventVenue, string eventOwnerName, int eventCategoryID, string dateSearch, int beforeBirthday,
        DateTime dateSearchStartDate, DateTime dateSearchEndDate,
        int recentlyAdded, int recentlyUpdated, string definitlyDo)
    {
        int searchCount = 0;

        int userID = int.Parse(Session["loggedInUserID"].ToString());

        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (searchText != "")
            {
                cmd.CommandText = "spSearchEvents";
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                cmd.Parameters.Add("@SearchText", SqlDbType.NVarChar, 1000).Value = searchText;
            }
            else
            {
                cmd.CommandText = "spSearchEventsAdvanced";
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 1000).Value = eventNameText;
                cmd.Parameters.Add("@EventVenue", SqlDbType.NVarChar, 1000).Value = eventVenue;
                cmd.Parameters.Add("@OwnerName", SqlDbType.NVarChar, 1000).Value = eventOwnerName;
                cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = eventCategoryID;
                if( dateSearch == "R" )
                {
                    if (dateSearchStartDate == DateTime.MinValue)
                    {
                        cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = dateSearchStartDate;
                    }
                    if (dateSearchStartDate == DateTime.MinValue)
                    {
                        cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = dateSearchEndDate;
                    }
                }
                if( dateSearch == "B" )
                {
                    // Do not search on birthday, search between now and
                    // the date on which the birthday falls
                    SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
                    DateTime birthdayEndDate = user.birthday.AddYears(beforeBirthday);
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = birthdayEndDate;
                }
                cmd.Parameters.Add("@RecentlyAdded", SqlDbType.Int).Value = recentlyAdded;
                cmd.Parameters.Add("@RecentlyUpdated", SqlDbType.Int).Value = recentlyUpdated;
                cmd.Parameters.Add("@DefinitlyDo", SqlDbType.NChar, 1).Value = definitlyDo;
            }
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                searchCount++;
            }
            rdr.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }

        return searchCount;
    }

    //===============================================================
    // Function: backToProfileButton_click
    //===============================================================
    protected void backToProfileButton_click(object sender, EventArgs e)
    {
        string url = "profile.aspx";

        Response.Redirect(url);
    }

    //===============================================================
    // Function: searchButton2_click
    //===============================================================
    protected void searchButton2_click(object sender, EventArgs e)
    {
        string searchString = what2.Text;

        if (searchString.Trim() == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a search term\");", true);
        }
        else
        {
            if (searchString.Length >= 2)
            {
                Response.Redirect("search2.aspx?Search=" + searchString.ToString());
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a longer search term\");", true);

                int userID = int.Parse(Session["loggedInUserID"].ToString());
                SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);

                PopulateEvents(user);
                PopulateLatestSearches();
            }
        }
    }
}

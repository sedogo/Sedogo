//===============================================================
// Filename: search.aspx.cs
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

public partial class search : SedogoPage
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

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);

            what.Text = searchText;

            PopulateEvents(user, searchText);

            timelineURL.Text = "timelineSearchXML.aspx?Search=" + searchText;
        }
    }

    //===============================================================
    // Function: PopulateEvents
    //===============================================================
    protected void PopulateEvents(SedogoUser user, string searchText)
    {
        int userID = int.Parse(Session["loggedInUserID"].ToString());

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

        todaysDateLabel.Text = "Today: " + todayStart.ToString("dd/MM/yyyy");

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
        searchHistory.searchText = searchText;
        searchHistory.userID = userID;
        int rowCount = 0;

        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSearchEvents";
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            cmd.Parameters.Add("@SearchText", SqlDbType.NVarChar, 1000).Value = searchText;
            DbDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows == true)
            {
                noSearchResultsDiv.Visible = false;

                while (rdr.Read())
                {
                    if (rowCount <= 50)
                    {
                        int categoryID = 1;
                        string dateType = "D";
                        DateTime startDate = DateTime.MinValue;
                        DateTime rangeStartDate = DateTime.MinValue;
                        DateTime rangeEndDate = DateTime.MinValue;
                        int beforeBirthday = -1;
                        string emailAddress = "";
                        string firstName = "";
                        string lastName = "";
                        string gender = "M";
                        string homeTown = "";
                        string profilePicThumbnail = "";
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
                        if (!rdr.IsDBNull(rdr.GetOrdinal("CategoryID")))
                        {
                            categoryID = int.Parse(rdr["CategoryID"].ToString());
                        }
                        if (!rdr.IsDBNull(rdr.GetOrdinal("BeforeBirthday")))
                        {
                            beforeBirthday = int.Parse(rdr["BeforeBirthday"].ToString());
                        }
                        if (!rdr.IsDBNull(rdr.GetOrdinal("EmailAddress")))
                        {
                            emailAddress = (string)rdr["EmailAddress"];
                        }
                        if (!rdr.IsDBNull(rdr.GetOrdinal("FirstName")))
                        {
                            firstName = (string)rdr["FirstName"];
                        }
                        if (!rdr.IsDBNull(rdr.GetOrdinal("LastName")))
                        {
                            lastName = (string)rdr["LastName"];
                        }
                        if (!rdr.IsDBNull(rdr.GetOrdinal("Gender")))
                        {
                            gender = (string)rdr["Gender"];
                        }
                        if (!rdr.IsDBNull(rdr.GetOrdinal("HomeTown")))
                        {
                            homeTown = (string)rdr["HomeTown"];
                        }
                        if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicThumbnail")))
                        {
                            profilePicThumbnail = (string)rdr["ProfilePicThumbnail"];
                        }
                        if (!rdr.IsDBNull(rdr.GetOrdinal("EventPicThumbnail")))
                        {
                            eventPicThumbnail = (string)rdr["EventPicThumbnail"];
                        }

                        string dateString = "";
                        MiscUtils.GetDateStringStartDate(user, dateType, rangeStartDate,
                            rangeEndDate, beforeBirthday, ref dateString, ref startDate);

                        StringBuilder eventString = new StringBuilder();
                        eventString.Append("<div class=\"event");
                        //if (categoryID > 0)  // Show border colour on event
                        //{
                        //    eventString.Append(" highlight-group-" + categoryID.ToString());
                        //}
                        eventString.AppendLine("\">");
                        eventString.AppendLine("<table width=\"100%\"><tr>");
                        eventString.AppendLine("<td>");
                        eventString.AppendLine("<h3>" + eventName);
                        eventString.Append("</h3>");
                        eventString.AppendLine("<p><i>" + firstName + " " + lastName + "</i></p>");
                        eventString.AppendLine("<p>" + dateString + " <a href=\"viewEvent.aspx?EID=" + eventID.ToString() + "\" title=\"\" class=\"modal\">View</a></p>");
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
                    rowCount++;
                }
                rdr.Close();

                if (rowCount >= 50)
                {
                    moreThan50ResultsDiv.Visible = true;
                }
                else
                {
                    moreThan50ResultsDiv.Visible = false;
                }
            }
            else
            {
                moreThan50ResultsDiv.Visible = false;
                noSearchResultsDiv.Visible = true;
                rowCount = 0;
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
}

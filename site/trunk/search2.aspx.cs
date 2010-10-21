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
{   //*new
    protected String BYr;
    static int srhUId = 0;
    static string srhUName = "";
    //
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

            Boolean viewArchivedEvents = false;
            if (Session["ViewArchivedEvents"] != null)
            {
                viewArchivedEvents = (Boolean)Session["ViewArchivedEvents"];
            }

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);

            sidebarControl.userID = userID;
            sidebarControl.user = user;
            bannerAddFindControl.userID = userID;

            eventsListControl.userID = userID;
            eventsListControl.user = user;

            int userAgeYears = DateTime.Now.Year - user.birthday.Year;

            BYr = user.birthday.Year.ToString();

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
            //if (dateSearch == "R")
            //{
            //    betweenDatesRadioButton.Checked = true;
            //    CalendarRangeStartDate.SelectedDate = dateSearchStartDate;
            //    PickerRangeStartDate.SelectedDate = dateSearchStartDate;
            //    CalendarRangeEndDate.SelectedDate = dateSearchEndDate;
            //    PickerRangeEndDate.SelectedDate = dateSearchEndDate;
            //}
            //if (dateSearch == "B")
            //{
            //    beforeBirthdayRadioButton.Checked = true;
            //    birthdayDropDownList.SelectedValue = beforeBirthday.ToString();
            //}
            recentlyAddedDropDownList.SelectedValue = recentlyAdded.ToString();
            recentlyUpdatedDropDownList.SelectedValue = recentlyUpdated.ToString();
            definitlyDoDropDownList.SelectedValue = definitlyDo;            

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
                string displaySearchText = "";
                if (eventNameText != "")
                {
                    displaySearchText = eventNameText;
                }
                if (eventVenue != "")
                {
                    if (displaySearchText != "")
                    {
                        displaySearchText += ", ";
                    }
                    displaySearchText += eventVenue;
                }
                if (eventOwnerName != "")
                {
                    if (displaySearchText != "")
                    {
                        displaySearchText += ", ";
                    }
                    displaySearchText += eventOwnerName;
                }

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
                    if (displaySearchText != "")
                    {
                        displaySearchText += ", ";
                    }
                    displaySearchText += category;
                }
                if (beforeBirthday > 0)
                {
                    if (displaySearchText != "")
                    {
                        displaySearchText += ", ";
                    }
                    displaySearchText += "Before birthday: " + beforeBirthday.ToString();
                }
                if (dateSearchStartDate > DateTime.MinValue)
                {
                    displaySearchText += "From date: " + dateSearchStartDate.ToString("dd/MM/yyyy");
                }
                if (dateSearchEndDate > DateTime.MinValue)
                {
                    displaySearchText += "To date: " + dateSearchEndDate.ToString("dd/MM/yyyy");
                }
                searchHistory.searchText = displaySearchText;
                searchResultsLabel.Text = "Search results for: " + displaySearchText;
                searchForLiteral1.Text = displaySearchText;
                searchForLiteral2.Text = displaySearchText;
            }
            else
            {
                searchHistory.searchText = searchText;
                searchResultsLabel.Text = "Search results: " + searchText;
                searchForLiteral1.Text = searchText;
                searchForLiteral2.Text = searchText;
            }
            searchHistory.userID = userID;
            searchHistory.searchHits = 0;
            searchHistory.Add();

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

            //new
            if (srhUName == "")
            {
                searchResultsLabel.Text = "&nbsp;&nbsp;Search results: " + searchText + " - " + searchCount.ToString() + " results";
            }
            else
            {
                searchResultsLabel.Text = "&nbsp;&nbsp;<a href='javascript:viewProfile(" + srhUId + ")'>" + srhUName + "'s</a> timeline &nbsp;<a href='javascript:doSendMessage(" + srhUId + ")'><img src='images/messages.gif' title='Send Message' alt='Send Message'/></a>";
            }
            //searchResultsLabel.Text = "Search results: " + searchText + " - " + searchCount.ToString() + " results" + "                 <img style='cursor: pointer;' alt='Open/Close timeline' title='Open/Close timeline' src='images/T_Close.jpg' id='imgMngTN'>";

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

            //DateTime timelineStartDate = DateTime.Now.AddMonths(8);
            DateTime timelineStartDate = DateTime.Now.AddYears(4);

            timelineStartDate1.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");     // "Jan 08 2010 00:00:00 GMT"
            timelineStartDate2.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");
            timelineStartDate3.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");     // "Jan 08 2010 00:00:00 GMT"
            timelineStartDate4.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");
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
        //if (betweenDatesRadioButton.Checked == true)
        //{
        //    dateSearchStartDate = CalendarRangeStartDate.SelectedDate;
        //    dateSearchEndDate = CalendarRangeEndDate.SelectedDate;
        //}
        //if (beforeBirthdayRadioButton.Checked == true)
        //{
        //    dateSearch = "B";
        //    beforeBirthday = int.Parse(birthdayDropDownList.SelectedValue);
        //}
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
                if (dateSearch == "R")
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
                if (dateSearch == "B")
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

            //* New
            DataTable dtChk = new DataTable();
            dtChk.Load(cmd.ExecuteReader());
            ArrayList foundRows = new ArrayList();
            int UId = 0;
            //    Dim iSalary As Integer
            //Dim aSalary As New ArrayList()
            foreach (DataRow objRow in dtChk.Rows)
            {
                UId = Convert.ToInt32(objRow["UserId"]);
                if (!foundRows.Contains(UId))
                {
                    foundRows.Add(UId);
                }
            }
            int ChkUCnt = 0;
            ChkUCnt = foundRows.Count;
            if (ChkUCnt == 1)
            {
                srhUId = UId;
                SqlCommand cmd1 = new SqlCommand("select FirstName+' '+LastName as Name from users where UserId=" + foundRows[0], conn);
                srhUName = Convert.ToString(cmd1.ExecuteScalar());
            }
            else
            {
                srhUName = "";
            }
            //*

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
}

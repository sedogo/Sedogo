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
        }
    }

    //===============================================================
    // Function: PopulateEvents
    //===============================================================
    protected void PopulateEvents(SedogoUser user, string searchText)
    {
        int userID = int.Parse(Session["loggedInUserID"].ToString());
        Boolean viewArchivedEvents = false;
        if (Session["ViewArchivedEvents"] != null)
        {
            viewArchivedEvents = (Boolean)Session["ViewArchivedEvents"];
        }

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
            cmd.CommandText = "spSearchEvents";
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            cmd.Parameters.Add("@SearchText", SqlDbType.NVarChar, 1000).Value = searchText;
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int categoryID = 1;
                string dateType = "D";
                DateTime startDate = DateTime.MinValue;
                DateTime rangeStartDate = DateTime.MinValue;
                DateTime rangeEndDate = DateTime.MinValue;
                int beforeBirthday = -1;

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
                Boolean eventAchieved = (Boolean)rdr["EventAchieved"];
                if (!rdr.IsDBNull(rdr.GetOrdinal("CategoryID")))
                {
                    categoryID = int.Parse(rdr["CategoryID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("BeforeBirthday")))
                {
                    beforeBirthday = int.Parse(rdr["BeforeBirthday"].ToString());
                }

                if (dateType == "D")
                {
                    timelineStartDate = startDate;
                    timelineEndDate = startDate.AddDays(28);        // Add 28 days so it shows up
                }
                if (dateType == "R")
                {
                    timelineStartDate = rangeStartDate;
                    timelineEndDate = rangeEndDate;

                    TimeSpan ts = timelineEndDate - timelineStartDate;
                    if (ts.Days < 28)
                    {
                        timelineEndDate = startDate.AddDays(28);        // Add 28 days so it shows up
                    }
                }
                if (dateType == "A")
                {
                    timelineStartDate = DateTime.Now;
                    if (user.birthday > DateTime.MinValue)
                    {
                        timelineEndDate = user.birthday;

                        TimeSpan ts = timelineEndDate - timelineStartDate;
                        if (ts.Days < 28)
                        {
                            timelineEndDate = startDate.AddDays(28);        // Add 28 days so it shows up
                        }
                    }
                    else
                    {
                        timelineEndDate = DateTime.Now.AddDays(28);
                    }
                }

                StringBuilder eventString = new StringBuilder();
                eventString.Append("<div class=\"event");
                if (categoryID > 0)
                {
                    eventString.Append(" highlight-group-" + categoryID.ToString());
                }
                eventString.AppendLine("\">");
                eventString.AppendLine("<h3>" + eventName);
                if (eventAchieved == true)
                {
                    eventString.Append(" (achieved)");
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

                if (currentCategoryID != categoryID)
                {
                    if (currentCategoryID != 0)
                    {
                        timelineItems2String.AppendLine("</div>");
                    }
                    timelineItems2String.AppendLine("<div class=\"row-container category-" + categoryID.ToString() + "\">");
                }
                timelineItems2String.AppendLine("<div class=\"row\">");
                timelineItems2String.AppendLine("<div class=\"tl\" id=\"" + timelineItemNumber.ToString() + "\">" + eventName + "</div>");
                timelineItems2String.AppendLine("</div>");

                timelineItemNumber++;

                searchResultsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));

                currentCategoryID = categoryID;
            }
            rdr.Close();
            if (currentCategoryID != 0)
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
}

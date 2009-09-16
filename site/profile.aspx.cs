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
            SedogoUser user = new SedogoUser("", int.Parse(Session["loggedInUserID"].ToString()));
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

            messageCountLink.Text = "You have 0 new messages";
            inviteCountLink.Text = "You have 0 new invites";
            alertCountLink.Text = "You have 0 alerts";
            groupCountLink.Text = "You belong to 0 groups";

            PopulateEvents();
        }
    }

    //===============================================================
    // Function: PopulateEvents
    //===============================================================
    protected void PopulateEvents()
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

                int eventID = int.Parse(rdr["EventID"].ToString());
                string eventName = (string)rdr["EventName"];
                DateTime startDate = (DateTime)rdr["StartDate"];
                Boolean eventAchieved = (Boolean)rdr["EventAchieved"];
                if (!rdr.IsDBNull(rdr.GetOrdinal("CategoryID")))
                {
                    categoryID = int.Parse(rdr["CategoryID"].ToString());
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
                eventString.Append("</h3>");
                eventString.AppendLine("<p><a href=\"viewEvent.aspx?EID=" + eventID.ToString() + "\" title=\"\" class=\"modal\">View</a></p>");
                //eventString.AppendLine("<p class=\"warning\">Note to self: book tickets</p>");
                eventString.AppendLine("</div>");

                timelineItems1String.AppendLine("$(\"#" + timelineItemNumber.ToString() + "\").css(\"width\",\"500px\").css(\"left\",\"100px\");");

                if( currentCategoryID != categoryID )
                {
                    if( currentCategoryID != 0 )
                    {
                        timelineItems2String.AppendLine("</div>");
                    }
                    timelineItems2String.AppendLine("<div class=\"row-container category-" + categoryID.ToString() + "\">");
                }
                timelineItems2String.AppendLine("<div class=\"row\">");
                timelineItems2String.AppendLine("<div class=\"tl\" id=\"" + timelineItemNumber.ToString() + "\">" + eventName + "</div>");
                timelineItems2String.AppendLine("</div>");

                timelineItemNumber++;

                if (startDate <= todayStart)
                {
                    overdueEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayStart && startDate <= todayPlus1Day)
                {
                    todayEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus1Day && startDate <= todayPlus7Days)
                {
                    thisWeekEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus7Days && startDate <= todayPlus1Month)
                {
                    thisMonthEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus1Month && startDate <= todayPlus1Year)
                {
                    nextYearEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus1Year && startDate <= todayPlus2Years)
                {
                    next2YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus2Years && startDate <= todayPlus3Years)
                {
                    next3YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus3Years && startDate <= todayPlus4Years)
                {
                    next4YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus4Years && startDate <= todayPlus5Years)
                {
                    next5YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus5Years && startDate <= todayPlus10Years)
                {
                    next10YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus10Years && startDate <= todayPlus20Years)
                {
                    next20YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
                }
                if (startDate >= todayPlus20Years && startDate <= todayPlus100Years)
                {
                    next100YearsEventsPlaceHolder.Controls.Add(new LiteralControl(eventString.ToString()));
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
    // Function: click_viewArchiveLink
    //===============================================================
    protected void click_viewArchiveLink(object sender, EventArgs e)
    {
        Boolean viewArchivedEvents = false;
        if (Session["ViewArchivedEvents"] != null)
        {
            Session["ViewArchivedEvents"] = !(Boolean)Session["ViewArchivedEvents"];
        }
        else
        {
            Session["ViewArchivedEvents"] = true;
        }

        PopulateEvents();
    }
}

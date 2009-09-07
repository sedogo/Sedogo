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
        DateTime todayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
            0, 0, 0);
        DateTime todayEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
            23, 59, 59);
        DateTime sevenDaysEnd = todayEnd.AddDays(7);

        todaysDateLabel.Text = todayStart.ToString("dd/MM/yyyy");

        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectEventList";
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = todayStart;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = todayEnd;
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int eventID = int.Parse(rdr["EventID"].ToString());
                string eventName = (string)rdr["EventName"];
                DateTime startDate = (DateTime)rdr["StartDate"];
                DateTime endDate = (DateTime)rdr["EndDate"];

                StringBuilder eventString = new StringBuilder();

                eventString.AppendLine("<div class=\"event\">");
                eventString.AppendLine("<h3>See Hamlet</h3>");
                eventString.AppendLine("<p class=\"warning\">Note to self: book tickets</p>");
                eventString.AppendLine("</div>");

                todayEventsPlaceHolder.Controls.Add( new LiteralControl(eventString.ToString()) );
            }
            rdr.Close();

			//thisWeekEventsPlaceHolder
			//thisMonthEventsPlaceHolder

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

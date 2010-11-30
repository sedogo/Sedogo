//===============================================================
// Filename: goalsAchieved.aspx.cs
// Date: 16/05/10
// --------------------------------------------------------------
// Description:
//   Achieved events
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 16/05/10
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
using Sedogo.BusinessObjects;

public partial class goalsAchieved : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Session["loggedInUserID"].ToString());

            sidebarControl.userID = userID;
            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
            sidebarControl.user = user;
            bannerAddFindControl.userID = userID;

            PopulateCompletedGoalsList(userID);
        }
    }

    //===============================================================
    // Function: PopulateCompletedGoalsList
    //===============================================================
    private void PopulateCompletedGoalsList(int userID)
    {
        int trackedEventCount = TrackedEvent.GetJoinedEventCount(userID);

        if (trackedEventCount > 0)
        {
            noAchievedEventsDiv.Visible = false;
            achievedEventsDiv.Visible = true;
        }
        else
        {
            noAchievedEventsDiv.Visible = true;
            achievedEventsDiv.Visible = false;
        }

        try
        {
            SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

            SqlCommand cmd = new SqlCommand("spSelectAchievedEventList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

            cmd.CommandTimeout = 90;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            achievedEventsRepeater.DataSource = ds;
            achievedEventsRepeater.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //===============================================================
    // Function: achievedEventsRepeater_ItemDataBound
    //===============================================================
    protected void achievedEventsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null &&
            (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
        {
            DataRowView row = e.Item.DataItem as DataRowView;

            int eventID = int.Parse(row["EventID"].ToString());
            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
            SedogoUser eventOwner = new SedogoUser("", sedogoEvent.userID);
            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventOwner, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);

            HyperLink eventNameLabel = e.Item.FindControl("eventNameLabel") as HyperLink;
            eventNameLabel.NavigateUrl = "viewEvent.aspx?EID=" + row["EventID"].ToString();
            eventNameLabel.Text = row["EventName"].ToString();

            Label eventDateLabel = e.Item.FindControl("eventDateLabel") as Label;
            eventDateLabel.Text = dateString;

            Label dateAchievedLabel = e.Item.FindControl("dateAchievedLabel") as Label;
            if (sedogoEvent.eventAchievedDate > DateTime.MinValue)
            {
                dateAchievedLabel.Text = sedogoEvent.eventAchievedDate.ToString("dd MMM yyyy");
            }
            else
            {
                dateAchievedLabel.Text = "";
            }

            //HyperLink userNameLabel = e.Item.FindControl("userNameLabel") as HyperLink;
            //userNameLabel.Text = row["FirstName"].ToString() + " " + row["LastName"].ToString();
            //userNameLabel.NavigateUrl = "userTimeline.aspx?UID=" + sedogoEvent.userID.ToString();

            Image eventImage = e.Item.FindControl("eventImage") as Image;
            string eventPicThumbnail = row["EventPicThumbnail"].ToString();
            if (eventPicThumbnail == "")
            {
                eventImage.ImageUrl = "~/images/eventThumbnailBlank.png";
            }
            else
            {
                var _event = new SedogoEvent(string.Empty, eventID);
                eventImage.ImageUrl = ResolveUrl(ImageHelper.GetRelativeImagePath(_event.eventID, _event.eventGUID, ImageType.EventThumbnail));
            }
        }
    }

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        Response.Redirect("profile.aspx");
    }
}

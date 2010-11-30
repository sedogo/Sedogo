//===============================================================
// Filename: tracking.aspx.cs
// Date: 17/10/09
// --------------------------------------------------------------
// Description:
//   Tracked events
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 17/10/09
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

public partial class tracking : SedogoPage
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

            PopulateTrackedEventsList(userID);
        }
    }

    //===============================================================
    // Function: PopulateTrackedEventsList
    //===============================================================
    private void PopulateTrackedEventsList(int userID)
    {
        int trackedEventCount = TrackedEvent.GetTrackedEventCount(userID);

        if (trackedEventCount > 0)
        {
            noTrackedEventsDiv.Visible = false;
            trackedEventsDiv.Visible = true;
        }
        else
        {
            noTrackedEventsDiv.Visible = true;
            trackedEventsDiv.Visible = false;
        }

        try
        {
            SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

            SqlCommand cmd = new SqlCommand("spSelectTrackedEventListByUserID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

            cmd.CommandTimeout = 90;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            trackedEventsRepeater.DataSource = ds;
            trackedEventsRepeater.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //===============================================================
    // Function: trackedEventsRepeater_ItemDataBound
    //===============================================================
    protected void trackedEventsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null &&
            (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
        {
            DataRowView row = e.Item.DataItem as DataRowView;

            int eventID = int.Parse(row["EventID"].ToString());
            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

            HyperLink eventNameLabel = e.Item.FindControl("eventNameLabel") as HyperLink;
            eventNameLabel.NavigateUrl = "viewEvent.aspx?EID=" + row["EventID"].ToString();
            eventNameLabel.Text = row["EventName"].ToString();

            HyperLink userNameLabel = e.Item.FindControl("userNameLabel") as HyperLink;
            userNameLabel.Text = row["FirstName"].ToString() + " " + row["LastName"].ToString();
            userNameLabel.NavigateUrl = "userTimeline.aspx?UID=" + sedogoEvent.userID.ToString();

            Image eventImage = e.Item.FindControl("eventImage") as Image;
            string eventPicThumbnail = row["EventPicThumbnail"].ToString();
            if (eventPicThumbnail == "")
            {
                eventImage.ImageUrl = "~/images/eventThumbnailBlank.png";
            }
            else
            {
                var _event = new SedogoEvent(string.Empty, eventID);
                eventImage.ImageUrl = ImageHelper.GetRelativeImagePath(_event.eventID, _event.eventGUID, ImageType.EventThumbnail);
            }
        }
    }

    //===============================================================
    // Function:trackedEventsRepeater_ItemCommand
    //===============================================================
    protected void trackedEventsRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "stopTrackingButton")
        {
            int trackedEventID = int.Parse(e.CommandArgument.ToString());

            TrackedEvent trackedEvent = new TrackedEvent(Session["loggedInUserFullName"].ToString(), trackedEventID);
            trackedEvent.Delete();

            int userID = int.Parse(Session["loggedInUserID"].ToString());
            PopulateTrackedEventsList(userID);
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

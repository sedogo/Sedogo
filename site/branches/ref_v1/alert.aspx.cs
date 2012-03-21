//===============================================================
// Filename: alert.aspx.cs
// Date: 26/10/09
// --------------------------------------------------------------
// Description:
//   View alerts
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 26/10/09
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

public partial class alert : SedogoPage
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

            PopulateAlertsList(userID);
        }
    }

    //===============================================================
    // Function: PopulateAlertsList
    //===============================================================
    private void PopulateAlertsList(int userID)
    {
        int pendingAlertCount = EventAlert.GetEventAlertCountPendingByUser(userID);

        if (pendingAlertCount > 0)
        {
            noAlertsDiv.Visible = false;
            alertsDiv.Visible = true;
        }
        else
        {
            noAlertsDiv.Visible = true;
            alertsDiv.Visible = false;
        }

        try
        {
            SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

            SqlCommand cmd = new SqlCommand("spSelectEventAlertListPendingByUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

            cmd.CommandTimeout = 90;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            alertsRepeater.DataSource = ds;
            alertsRepeater.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //===============================================================
    // Function: alertsRepeater_ItemDataBound
    //===============================================================
    protected void alertsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null &&
            (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
        {
            DataRowView row = e.Item.DataItem as DataRowView;

            HyperLink eventNameLabel = e.Item.FindControl("eventNameLabel") as HyperLink;
            eventNameLabel.NavigateUrl = "viewEvent.aspx?EID=" + row["EventID"].ToString();
            eventNameLabel.Text = row["EventName"].ToString();

            HyperLink editAlertButton = e.Item.FindControl("editAlertButton") as HyperLink;
            editAlertButton.NavigateUrl = "eventAlerts.aspx?EID=" + row["EventID"].ToString();

            string alertText = row["AlertText"].ToString();
            alertText = alertText.Replace("\n", "<br/>");
            Literal alertTextLabel = e.Item.FindControl("alertTextLabel") as Literal;
            alertTextLabel.Text = alertText;

            DateTime alertDate = (DateTime)row["AlertDate"];
            string alertDateText = alertDate.ToString("ddd d MMMM yyyy");
            Literal alertDateLabel = e.Item.FindControl("alertDateLabel") as Literal;
            alertDateLabel.Text = alertDateText;

            Image eventPicThumbnailImage = e.Item.FindControl("eventPicThumbnailImage") as Image;
            string eventPicThumbnail = row["EventPicThumbnail"].ToString();
            if (eventPicThumbnail == "")
            {
                eventPicThumbnailImage.ImageUrl = "./images/eventThumbnailBlank.png";
            }
            else
            {
                eventPicThumbnailImage.ImageUrl = "./assets/eventPics/" + eventPicThumbnail;
            }
        }
    }

    //===============================================================
    // Function: alertsRepeater_ItemCommand
    //===============================================================
    protected void alertsRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int eventAlertID = int.Parse(e.CommandArgument.ToString());

        EventAlert eventAlert = new EventAlert(Session["loggedInUserFullName"].ToString(), eventAlertID);

        if (e.CommandName == "clearAlertButton")
        {
            EventAlert alertToDelete = new EventAlert(Session["loggedInUserFullName"].ToString(), eventAlertID);
            alertToDelete.Delete();
        }

        int userID = int.Parse(Session["loggedInUserID"].ToString());
        PopulateAlertsList(userID);
    }

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        Response.Redirect("profile.aspx");
    }
}

//===============================================================
// Filename: invite.aspx.cs
// Date: 23/10/09
// --------------------------------------------------------------
// Description:
//   View invite
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 23/10/09
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

public partial class eventJoinRequests : SedogoPage
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

            PopulateRequestList(userID);
        }
    }

    //===============================================================
    // Function: PopulateRequestList
    //===============================================================
    private void PopulateRequestList(int userID)
    {
        int pendingRequestsCount = SedogoEvent.GetPendingMemberUserCountByUserID(userID);

        if (pendingRequestsCount > 0)
        {
            noRequestsDiv.Visible = false;
            requestsDiv.Visible = true;
        }
        else
        {
            noRequestsDiv.Visible = true;
            requestsDiv.Visible = false;
        }

        try
        {
            SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

            SqlCommand cmd = new SqlCommand("spSelectPendingMemberRequestsByUserID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            cmd.CommandTimeout = 90;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            requestsRepeater.DataSource = ds;
            requestsRepeater.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //===============================================================
    // Function: requestsRepeater_ItemDataBound
    //===============================================================
    protected void requestsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

            HyperLink userNameLabel = e.Item.FindControl("userNameLabel") as HyperLink;
            userNameLabel.NavigateUrl = "userProfile.aspx?UID=" + row["UserID"].ToString();
            userNameLabel.Text = row["FirstName"].ToString() + " " + row["LastName"].ToString();

            Image eventPicThumbnailImage = e.Item.FindControl("eventPicThumbnailImage") as Image;
            string eventPicThumbnail = row["eventPicThumbnail"].ToString();
            if (eventPicThumbnail == "")
            {
                eventPicThumbnailImage.ImageUrl = "./images/eventThumbnailBlank.png";
            }
            else
            {
                //eventPicThumbnailImage.ImageUrl = "./assets/eventPics/" + eventPicThumbnail;
                eventPicThumbnailImage.ImageUrl =
                    ResolveUrl(ImageHelper.GetRelativeImagePath(sedogoEvent.eventID, sedogoEvent.eventGUID,
                                                                ImageType.EventThumbnail));
            }
        }
    }

    //===============================================================
    // Function: requestsRepeater_ItemCommand
    //===============================================================
    protected void requestsRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int trackedEventID = int.Parse(e.CommandArgument.ToString());

        TrackedEvent trackedEvent = new TrackedEvent(Session["loggedInUserFullName"].ToString(), trackedEventID);

        if (e.CommandName == "acceptButton")
        {
            trackedEvent.joinPending = false;
            trackedEvent.Update();

            trackedEvent.SendJoinAcceptedEmail();
        }
        if (e.CommandName == "declineButton")
        {
            trackedEvent.Delete();
        }

        int userID = int.Parse(Session["loggedInUserID"].ToString());
        PopulateRequestList(userID);
    }

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        Response.Redirect("profile.aspx");
    }
}

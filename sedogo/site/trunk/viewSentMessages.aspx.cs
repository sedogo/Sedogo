//===============================================================
// Filename: message.aspx.cs
// Date: 28/09/09
// --------------------------------------------------------------
// Description:
//   View event
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
using Sedogo.BusinessObjects;

public partial class viewSentMessages : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Session["loggedInUserID"].ToString());

            PopulateMessageList(userID);
        }
    }

    //===============================================================
    // Function: PopulateMessageList
    //===============================================================
    private void PopulateMessageList(int userID)
    {
        int sentMessageCount = Message.GetSentMessageCountForUser(userID);

        if (sentMessageCount > 0)
        {
            noSentMessagesDiv.Visible = false;
            messagesDiv.Visible = true;
        }
        else
        {
            noSentMessagesDiv.Visible = true;
            messagesDiv.Visible = false;
        }

        try
        {
            SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "spSelectSentMessageList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

            cmd.CommandTimeout = 90;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            messagesRepeater.DataSource = ds;
            messagesRepeater.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //===============================================================
    // Function: messagesRepeater_ItemDataBound
    //===============================================================
    protected void messagesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null &&
            (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
        {
            DataRowView row = e.Item.DataItem as DataRowView;

            int eventUserID = -1;
            if (row["UserID"].ToString() != "")
            {
                eventUserID = int.Parse(row["UserID"].ToString());
            }

            Literal eventNameLabel = e.Item.FindControl("eventNameLabel") as Literal;
            Literal userNameLabel = e.Item.FindControl("userNameLabel") as Literal;
            if (eventUserID < 0)
            {
                int userID = int.Parse(row["UserID"].ToString());
                SedogoUser messageToUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
                userNameLabel.Text = "To: <a href=\"userTimeline.aspx?UID=" + userID + "\" target=\"_top\">"
                    + messageToUser.firstName + " " + messageToUser.lastName + "</a> ";
                eventNameLabel.Text = "";
            }
            else
            {
                userNameLabel.Text = "To: <a href=\"userTimeline.aspx?UID=" + eventUserID + "\" target=\"_top\">"
                    + row["FirstName"] + " " + row["LastName"] + "</a> ";
                eventNameLabel.Text = "Goal: <a href=\"viewEvent.aspx?EID=" + row["EventID"] + "\">" 
                    + row["EventName"] + "</a>";
            }

            Image eventPicThumbnailImage = e.Item.FindControl("eventPicThumbnailImage") as Image;
            string eventPicThumbnail = row["eventPicThumbnail"].ToString();
            if (eventPicThumbnail == "")
            {
                eventPicThumbnailImage.ImageUrl = "./images/eventThumbnailBlank.png";
            }
            else
            {
                var eventID = int.Parse(row["EventID"].ToString());
                var sedogoEvent = new SedogoEvent(string.Empty, eventID);
                eventPicThumbnailImage.ImageUrl =
                    ResolveUrl(ImageHelper.GetRelativeImagePath(sedogoEvent.eventID, sedogoEvent.eventGUID,
                                                                ImageType.EventThumbnail));
                //eventPicThumbnailImage.ImageUrl = "./assets/eventPics/" + eventPicThumbnail;
            }

            Literal messageLabel = e.Item.FindControl("messageLabel") as Literal;
            messageLabel.Text = row["MessageText"].ToString();
        }
    }

    //===============================================================
    // Function: messagesRepeater_ItemCommand
    //===============================================================
    protected void messagesRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "sendReplyMessageButton")
        {
            int messageID = int.Parse(e.CommandArgument.ToString());

            Message message = new Message(Session["loggedInUserFullName"].ToString(), messageID);

            Response.Redirect("sendUserMessage.aspx?UID=" + message.postedByUserID.ToString()
                + "&EID=" + message.eventID.ToString());
        }
    }

    //===============================================================
    // Function: viewReceivedMessages_click
    //===============================================================
    protected void viewReceivedMessages_click(object sender, EventArgs e)
    {
        Response.Redirect("message.aspx");
    }
}

//===============================================================
// Filename: messageThread.aspx.cs
// Date: 04/12/10
// --------------------------------------------------------------
// Description:
//   View 
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 04/12/10
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

public partial class messageThread : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Session["loggedInUserID"].ToString());
            int parentMessageID = int.Parse(Request.QueryString["MID"].ToString());

            sidebarControl.userID = userID;
            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
            sidebarControl.user = user;
            bannerAddFindControl.userID = userID;

            PopulateMessageList(parentMessageID);
        }
    }

    //===============================================================
    // Function: PopulateMessageList
    //===============================================================
    private void PopulateMessageList(int parentMessageID)
    {
        try
        {
            SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "spSelectThreadedMessageList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ParentMessageID", SqlDbType.Int).Value = parentMessageID;

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
            try
            {
                DataRowView row = e.Item.DataItem as DataRowView;

                int eventUserID = -1;
                if (row["UserID"].ToString() != "")
                {
                    eventUserID = int.Parse(row["UserID"].ToString());
                }
                int eventID = -1;
                if (row["EventID"].ToString() != "")
                {
                    eventID = int.Parse(row["EventID"].ToString());
                }
                int parentMessageID = -1;
                if (row["ParentMessageID"].ToString() != "")
                {
                    parentMessageID = int.Parse(row["ParentMessageID"].ToString());
                }
                int messageID = -1;
                if (row["MessageID"].ToString() != "")
                {
                    messageID = int.Parse(row["MessageID"].ToString());
                }
                int currentUserID = int.Parse(Session["loggedInUserID"].ToString());
                Boolean messageRead = false;
                if (row["MessageRead"].ToString() != "")
                {
                    messageRead = (Boolean)row["MessageRead"];
                }

                int postedByUserID = int.Parse(row["PostedByUserID"].ToString());
                SedogoUser messageFromUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), postedByUserID);

                Literal eventNameLabel = e.Item.FindControl("eventNameLabel") as Literal;
                Literal userNameLabel = e.Item.FindControl("userNameLabel") as Literal;

                userNameLabel.Text = "From: <a href=\"userTimeline.aspx?UID=" + postedByUserID.ToString() + "\" target=\"_top\">"
                    + messageFromUser.firstName + " " + messageFromUser.lastName + "</a> ";
                if (eventUserID < 0)
                {
                    // This is a message which is not attached to an event
                    eventNameLabel.Text = "";
                }
                else
                {
                    //userNameLabel.Text = "From: <a href=\"userTimeline.aspx?UID=" + eventUserID.ToString() + "\" target=\"_top\">"
                    //    + row["FirstName"].ToString() + " " + row["LastName"].ToString() + "</a> ";
                    eventNameLabel.Text = "Goal: <a href=\"viewEvent.aspx?EID=" + row["EventID"].ToString() + "\">" 
                        + row["EventName"].ToString() + "</a>";
                }

                LinkButton markAsReadButton = e.Item.FindControl("markAsReadButton") as LinkButton;
                if (markAsReadButton != null && messageRead == true)
                {
                    markAsReadButton.Visible = false;
                }

                Image eventPicThumbnailImage = e.Item.FindControl("eventPicThumbnailImage") as Image;
                //string eventPicThumbnail = row["eventPicThumbnail"].ToString();
                //if (eventPicThumbnail == "")
                //{
                //    eventPicThumbnailImage.ImageUrl = "./images/eventThumbnailBlank.png";
                //}
                //else
                //{
                //    eventPicThumbnailImage.ImageUrl = "./assets/eventPics/" + eventPicThumbnail;
                //}
                if (messageFromUser.profilePicThumbnail != "")
                {
                    // PD 3/12/10 - Removed 
                    //eventPicThumbnailImage.ImageUrl = ImageHelper.GetRelativeImagePath(messageFromUser.userID, messageFromUser.GUID, ImageType.UserThumbnail);
                    eventPicThumbnailImage.ImageUrl = "~/assets/profilePics/" + messageFromUser.profilePicThumbnail;
                }
                else
                {
                    if (messageFromUser.avatarNumber > 0)
                    {
                        eventPicThumbnailImage.ImageUrl = "~/images/avatars/avatar" + messageFromUser.avatarNumber.ToString() + "sm.gif";
                    }
                    else
                    {
                        if (messageFromUser.gender == "M")
                        {
                            // 1,2,5
                            int avatarID = 5;
                            switch ((messageFromUser.userID % 6))
                            {
                                case 0: case 1: avatarID = 1; break;
                                case 2: case 3: avatarID = 2; break;
                            }
                            eventPicThumbnailImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                        }
                        else
                        {
                            // 3,4,6
                            int avatarID = 6;
                            switch ((messageFromUser.userID % 6))
                            {
                                case 0: case 1: avatarID = 3; break;
                                case 2: case 3: avatarID = 4; break;
                            }
                            eventPicThumbnailImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                        }
                        //profileImage.ImageUrl = "~/images/profile/blankProfile.jpg";
                    }
                    eventPicThumbnailImage.Height = 50;
                    eventPicThumbnailImage.Width = 50;
                }

                Literal messageLabel = e.Item.FindControl("messageLabel") as Literal;
                messageLabel.Text = row["MessageText"].ToString().Replace("\n", "<br/>");
                messageLabel.Text += "<br/>&nbsp;";

                HyperLink sendReplyMessageButton = e.Item.FindControl("sendReplyMessageButton") as HyperLink;
                if (currentUserID == postedByUserID)
                {
                    sendReplyMessageButton.Visible = false;
                }
                else
                {
                    sendReplyMessageButton.NavigateUrl = "sendUserMessage.aspx?UID=" + postedByUserID.ToString()
                        + "&PMID=" + parentMessageID.ToString() + "&MID=" + messageID.ToString()
                        + "&Redir=Messages";
                }
            }
            catch
            {
                // Do nothing
            }
        }
    }

    //===============================================================
    // Function: messagesRepeater_ItemCommand
    //===============================================================
    protected void messagesRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if( e.CommandName == "markAsReadButton" )
        {
            int messageID = int.Parse(e.CommandArgument.ToString());

            Message message = new Message(Session["loggedInUserFullName"].ToString(), messageID);
            message.messageRead = true;
            message.Update();

            int userID = int.Parse(Session["loggedInUserID"].ToString());
            PopulateMessageList(userID);
        }
        if (e.CommandName == "deleteButton")
        {
            int messageID = int.Parse(e.CommandArgument.ToString());

            Message message = new Message(Session["loggedInUserFullName"].ToString(), messageID);
            message.Delete();

            int userID = int.Parse(Session["loggedInUserID"].ToString());
            PopulateMessageList(userID);
        }
        if (e.CommandName == "viewThreadButton")
        {
            int messageID = int.Parse(e.CommandArgument.ToString());

            Message message = new Message(Session["loggedInUserFullName"].ToString(), messageID);

            if (message.parentMessageID > 0)
            {
                Response.Redirect("messageThread.aspx?MID=" + message.parentMessageID.ToString());
            }
            else
            {
                Response.Redirect("messageThread.aspx?MID=" + messageID.ToString());
            }
        }
    }

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        Response.Redirect("message.aspx");
    }
}

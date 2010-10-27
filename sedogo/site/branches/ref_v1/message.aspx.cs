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

public partial class message : SedogoPage
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

            PopulateMessageList(userID);

            string replyID = "";
            if (Request.QueryString["ReplyID"] != null)
            {
                replyID = (string)Request.QueryString["ReplyID"];
            }
            if (replyID != "")
            {
                SedogoEvent sEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), int.Parse(replyID));

                string url = "sendUserMessage.aspx?UID=" + sEvent.userID.ToString() + "&EID=" + replyID.ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"" + url + "\");", true);
            }
        }
    }

    //===============================================================
    // Function: PopulateMessageList
    //===============================================================
    private void PopulateMessageList(int userID)
    {
        if (Session["ViewArchivedMessages"] == null || (Boolean)Session["ViewArchivedMessages"] == false)
        {
            int unreadMessageCount = Message.GetUnreadMessageCountForUser(userID);
            if (unreadMessageCount > 0)
            {
                noUnreadMessagesDiv.Visible = false;
                messagesDiv.Visible = true;
            }
            else
            {
                noUnreadMessagesDiv.Visible = true;
                messagesDiv.Visible = false;
            }

            viewArchivedMessagesButton.Visible = true;
            hideArchivedMessagesButton.Visible = false;
        }
        else
        {
            messagesDiv.Visible = true;
            noUnreadMessagesDiv.Visible = false;

            viewArchivedMessagesButton.Visible = false;
            hideArchivedMessagesButton.Visible = true;
        }

        try
        {
            SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (Session["ViewArchivedMessages"] == null || (Boolean)Session["ViewArchivedMessages"] == false)
            {
                cmd.CommandText = "spSelectUnreadMessageList";
            }
            else
            {
                cmd.CommandText = "spSelectReadMessageList";
            }
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
            if ((Boolean)row["MessageRead"] == true)
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

            HyperLink sendReplyMessageButton = e.Item.FindControl("sendReplyMessageButton") as HyperLink;
            sendReplyMessageButton.NavigateUrl = "sendUserMessage.aspx?UID=" + postedByUserID.ToString()
                + "&EID=" + eventID.ToString() + "&PMID=" + parentMessageID.ToString() + "&MID=" + messageID.ToString()
                + "&Redir=Messages";

            Repeater threadMessagesRepeater = e.Item.FindControl("threadMessagesRepeater") as Repeater;
            try
            {
                SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "spSelectThreadedMessageList";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ParentMessageID", SqlDbType.Int).Value = messageID;
                cmd.CommandTimeout = 90;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                threadMessagesRepeater.DataSource = ds;
                threadMessagesRepeater.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
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
    }

    //===============================================================
    // Function: threadMessagesRepeater_ItemDataBound
    //===============================================================
    protected void threadMessagesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
            int eventID = -1;
            if (row["EventID"].ToString() != "")
            {
                eventID = int.Parse(row["EventID"].ToString());
            }

            int postedByUserID = int.Parse(row["PostedByUserID"].ToString());
            SedogoUser messageFromUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), postedByUserID);

            Image threadPicThumbnailImage = e.Item.FindControl("threadPicThumbnailImage") as Image;
            if (messageFromUser.profilePicThumbnail != "")
            {
                threadPicThumbnailImage.ImageUrl = "~/assets/profilePics/" + messageFromUser.profilePicThumbnail;
            }
            else
            {
                if (messageFromUser.avatarNumber > 0)
                {
                    threadPicThumbnailImage.ImageUrl = "~/images/avatars/avatar" + messageFromUser.avatarNumber.ToString() + "sm.gif";
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
                        threadPicThumbnailImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
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
                        threadPicThumbnailImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                    }
                    //profileImage.ImageUrl = "~/images/profile/blankProfile.jpg";
                }
                threadPicThumbnailImage.Height = 50;
                threadPicThumbnailImage.Width = 50;
            }

            Literal threadUserNameLabel = e.Item.FindControl("threadUserNameLabel") as Literal;

            threadUserNameLabel.Text = "From: <a href=\"userTimeline.aspx?UID=" + postedByUserID.ToString() + "\" target=\"_top\">"
                + messageFromUser.firstName + " " + messageFromUser.lastName + "</a> ";

            Literal threadMessageLabel = e.Item.FindControl("threadMessageLabel") as Literal;
            threadMessageLabel.Text = row["MessageText"].ToString().Replace("\n","<br/>");
        }
    }

    //===============================================================
    // Function: threadMessagesRepeater_ItemCommand
    //===============================================================
    protected void threadMessagesRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
    }

    //===============================================================
    // Function: viewArchivedMessagesButton_click
    //===============================================================
    protected void viewArchivedMessagesButton_click(object sender, EventArgs e)
    {
        Session["ViewArchivedMessages"] = true;

        int userID = int.Parse(Session["loggedInUserID"].ToString());

        PopulateMessageList(userID);
    }

    //===============================================================
    // Function: hideArchivedMessagesButton_click
    //===============================================================
    protected void hideArchivedMessagesButton_click(object sender, EventArgs e)
    {
        Session["ViewArchivedMessages"] = false;

        int userID = int.Parse(Session["loggedInUserID"].ToString());

        PopulateMessageList(userID);
    }

    //===============================================================
    // Function: viewSentMessagesButton_click
    //===============================================================
    protected void viewSentMessagesButton_click(object sender, EventArgs e)
    {
        Response.Redirect("viewSentMessages.aspx");
    }
    
    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        Response.Redirect("profile.aspx");
    }
}

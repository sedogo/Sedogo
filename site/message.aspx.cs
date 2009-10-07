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

            //SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
            PopulateMessageList(userID);
        }
    }

    //===============================================================
    // Function: PopulateMessageList
    //===============================================================
    private void PopulateMessageList(int userID)
    {
        int unreadMessageCount = Message.GetUnreadMessageCountForUser(userID);

        if( unreadMessageCount > 0 )
        {
            noUnreadMessagesDiv.Visible = false;
            messagesDiv.Visible = true;
        }
        else
        {
            noUnreadMessagesDiv.Visible = true;
            messagesDiv.Visible = false;
        }

        try
        {
            SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

            SqlCommand cmd = new SqlCommand("spSelectUnreadMessageList", conn);
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

            Literal messageLabel = e.Item.FindControl("messageLabel") as Literal;
            messageLabel.Text = row["MessageText"].ToString();
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
    }
}

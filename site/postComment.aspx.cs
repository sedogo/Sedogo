﻿//===============================================================
// Filename: postComment.aspx.cs
// Date: 28/09/09
// --------------------------------------------------------------
// Description:
//   Add comment
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
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Globalization;
using Sedogo.BusinessObjects;

public partial class postComment : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);
            int userID = int.Parse(Session["loggedInUserID"].ToString());

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
            sidebarControl.userID = userID;
            sidebarControl.user = user;

            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

            eventNameLabel.Text = sedogoEvent.eventName;

            SetFocus(commentTextBox);
        }
    }

    //===============================================================
    // Function: saveChangesButton_click
    //===============================================================
    protected void saveChangesButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        int loggedInUserID = int.Parse(Session["loggedInUserID"].ToString());

        string commentText = commentTextBox.Text;

        SedogoEventComment comment = new SedogoEventComment(Session["loggedInUserFullName"].ToString());
        comment.eventID = eventID;
        comment.postedByUserID = loggedInUserID;
        comment.commentText = commentText;
        comment.Add();

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
        sedogoEvent.SendEventUpdateEmail(loggedInUserID);

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }
}

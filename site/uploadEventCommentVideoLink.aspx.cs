//===============================================================
// Filename: uploadEventCommentVideoLink.aspx
// Date: 30/4/10
// --------------------------------------------------------------
// Description:
//   Upload event video link
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 30/4/10
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
using System.Net.Mail;
using System.Text;
using System.IO;
using Sedogo.BusinessObjects;

public partial class uploadEventCommentVideoLink : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

        eventNameLabel.Text = sedogoEvent.eventName;

        SetFocus(videoLinkText);
    }

    //===============================================================
    // Function: uploadVideoLinkButton_click
    //===============================================================
    protected void uploadVideoLinkButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEventComment comment = new SedogoEventComment(Session["loggedInUserFullName"].ToString());
        comment.eventID = eventID;
        comment.postedByUserID = int.Parse(Session["loggedInUserID"].ToString());
        comment.commentText = "";
        comment.eventVideoLink = videoLinkText.Text;
        comment.VideoLinkAdd();

        SedogoEvent sedogoEvent = new SedogoEvent("", eventID);
        sedogoEvent.SendEventUpdateEmail(int.Parse(Session["loggedInUserID"].ToString()));

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }
}

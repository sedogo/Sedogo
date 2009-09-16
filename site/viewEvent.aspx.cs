//===============================================================
// Filename: viewEvent.aspx.cs
// Date: 14/09/09
// --------------------------------------------------------------
// Description:
//   View event
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 08/09/09
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
using Sedogo.BusinessObjects;

public partial class viewEvent : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);

            SedogoEvent sedogoEvent = new SedogoEvent("", eventID);

            messagesLink.Text = "you have 0 new messages";
            invitesLink.Text = "you have 1 new invite";
            //alertsLink.Text = "book a campsite";
            //trackingLinksPlaceholder

            eventTitleLabel.Text = sedogoEvent.eventName;

            if (sedogoEvent.eventPicPreview == "")
            {
                eventImage.ImageUrl = "~/images/eventImageBlank.png";
            }
            else
            {
                eventImage.ImageUrl = "~/assets/eventPics/" + sedogoEvent.eventPicPreview;
            }

            editEventLink.NavigateUrl = "editEvent.aspx?EID=" + eventID.ToString();

            if (sedogoEvent.eventAchieved == true)
            {
                achievedEventLink.Text = "re-open";
            }
            else
            {
                achievedEventLink.Text = "achieved";
            }
        }
    }

    //===============================================================
    // Function: click_achievedEventLink
    //===============================================================
    protected void click_achievedEventLink(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEvent sedogoEvent = new SedogoEvent("", eventID);
        sedogoEvent.eventAchieved = !sedogoEvent.eventAchieved;
        sedogoEvent.Update();

        Response.Redirect("profileRedirect.aspx");
    }

    //===============================================================
    // Function: click_uploadEventImage
    //===============================================================
    protected void click_uploadEventImage(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("uploadEventImage.aspx?EID=" + eventID.ToString());
    }
}

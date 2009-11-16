//===============================================================
// Filename: sendMessage.aspx.cs
// Date: 28/09/09
// --------------------------------------------------------------
// Description:
//   Send message
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

public partial class sendMessage : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);

            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

            eventNameLabel.Text = sedogoEvent.eventName;

            SedogoUser eventOwner = new SedogoUser(Session["loggedInUserFullName"].ToString(), sedogoEvent.userID);
            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventOwner, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);

            messageToLabel.Text = eventOwner.firstName + " " + eventOwner.lastName;
            eventDateLabel.Text = dateString;
            eventVenueLabel.Text = sedogoEvent.eventVenue.Replace("\n", "<br/>");

            SetFocus(messageTextBox);
        }
    }

    //===============================================================
    // Function: saveChangesButton_click
    //===============================================================
    protected void saveChangesButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

        string messageText = messageTextBox.Text;

        Message message = new Message(Session["loggedInUserFullName"].ToString());
        message.userID = sedogoEvent.userID;
        message.eventID = eventID;
        message.postedByUserID = int.Parse(Session["loggedInUserID"].ToString());
        message.messageText = messageText;
        message.Add();

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

//===============================================================
// Filename: addEventReminders.aspx
// Date: 21/11/09
// --------------------------------------------------------------
// Description:
//   Add event reminders
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 21/11/09
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

public partial class addEventReminders : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

        eventNameLabel.Text = sedogoEvent.eventName;

        CalendarAlertDate.SelectedDate = DateTime.Now;
        PickerAlertDate.SelectedDate = DateTime.Now;
    }

    //===============================================================
    // Function: alertDatePickList_changed
    //===============================================================
    protected void alertDatePickList_changed(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        if (alertDatePickList.SelectedValue == "1D")
        {
            CalendarAlertDate.SelectedDate = DateTime.Now.AddDays(1);
            PickerAlertDate.SelectedDate = DateTime.Now.AddDays(1);
        }
        if (alertDatePickList.SelectedValue == "1W")
        {
            CalendarAlertDate.SelectedDate = DateTime.Now.AddDays(7);
            PickerAlertDate.SelectedDate = DateTime.Now.AddDays(7);
        }
        if (alertDatePickList.SelectedValue == "1M")
        {
            CalendarAlertDate.SelectedDate = DateTime.Now.AddMonths(1);
            PickerAlertDate.SelectedDate = DateTime.Now.AddMonths(1);
        }
        if (alertDatePickList.SelectedValue == "3M")
        {
            CalendarAlertDate.SelectedDate = DateTime.Now.AddMonths(3);
            PickerAlertDate.SelectedDate = DateTime.Now.AddMonths(3);
        }
        if (alertDatePickList.SelectedValue == "6M")
        {
            CalendarAlertDate.SelectedDate = DateTime.Now.AddMonths(6);
            PickerAlertDate.SelectedDate = DateTime.Now.AddMonths(6);
        }
        if (alertDatePickList.SelectedValue == "1Y")
        {
            CalendarAlertDate.SelectedDate = DateTime.Now.AddYears(1);
            PickerAlertDate.SelectedDate = DateTime.Now.AddYears(1);
        }
    }

    //===============================================================
    // Function: addReminderLink_click
    //===============================================================
    protected void addReminderLink_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        DateTime alertDate = CalendarAlertDate.SelectedDate;
        string alertText = newAlertTextBox.Text;

        EventAlert eventAlert = new EventAlert((string)Application["connectionString"]);
        eventAlert.alertDate = alertDate;
        eventAlert.eventID = eventID;
        eventAlert.alertText = alertText;
        eventAlert.Add();

        newAlertTextBox.Text = "";
        CalendarAlertDate.SelectedDate = DateTime.Now;
        PickerAlertDate.SelectedDate = DateTime.Now;

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }

    //===============================================================
    // Function: click_skipRemindersLink
    //===============================================================
    protected void click_skipRemindersLink(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }
}

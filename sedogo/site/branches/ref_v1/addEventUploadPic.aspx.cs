//===============================================================
// Filename: addEventUploadPic.aspx
// Date: 21/11/09
// --------------------------------------------------------------
// Description:
//   Upload event pic
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

public partial class addEventUploadPic : SedogoPage
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
            eventNameLabel2.Text = sedogoEvent.eventName;

            SedogoUser eventOwner = new SedogoUser(Session["loggedInUserFullName"].ToString(), sedogoEvent.userID);
            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventOwner, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);

            CalendarAlertDate.SelectedDate = DateTime.Now;
            PickerAlertDate.SelectedDate = DateTime.Now;

            alertDatePickList.Attributes.Add("onchange", "setReminderDate()");
            DateTime d = DateTime.Now.AddDays(1);
            Date1DValue1.Text = d.Year.ToString() + "," + (d.Month-1).ToString() + "," + d.Day.ToString();
            Date1DValue2.Text = d.Year.ToString() + "," + (d.Month - 1).ToString() + "," + d.Day.ToString();
            d = DateTime.Now.AddDays(7);
            Date1WValue1.Text = d.Year.ToString() + "," + (d.Month - 1).ToString() + "," + d.Day.ToString();
            Date1WValue2.Text = d.Year.ToString() + "," + (d.Month - 1).ToString() + "," + d.Day.ToString();
            d = DateTime.Now.AddMonths(1);
            Date1MValue1.Text = d.Year.ToString() + "," + (d.Month - 1).ToString() + "," + d.Day.ToString();
            Date1MValue2.Text = d.Year.ToString() + "," + (d.Month - 1).ToString() + "," + d.Day.ToString();
            d = DateTime.Now.AddMonths(3);
            Date3MValue1.Text = d.Year.ToString() + "," + (d.Month - 1).ToString() + "," + d.Day.ToString();
            Date3MValue2.Text = d.Year.ToString() + "," + (d.Month - 1).ToString() + "," + d.Day.ToString();
            d = DateTime.Now.AddMonths(6);
            Date6MValue1.Text = d.Year.ToString() + "," + (d.Month - 1).ToString() + "," + d.Day.ToString();
            Date6MValue2.Text = d.Year.ToString() + "," + (d.Month - 1).ToString() + "," + d.Day.ToString();
            d = DateTime.Now.AddYears(1);
            Date1YValue1.Text = d.Year.ToString() + "," + (d.Month - 1).ToString() + "," + d.Day.ToString();
            Date1YValue2.Text = d.Year.ToString() + "," + (d.Month - 1).ToString() + "," + d.Day.ToString();

            SetFocus(eventPicFileUpload);
        }
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
    // Function: saveChangesButton_click
    //===============================================================
    protected void saveChangesButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        if (eventPicFileUpload.PostedFile.ContentLength != 0)
        {
            int fileSizeBytes = eventPicFileUpload.PostedFile.ContentLength;

            GlobalData gd = new GlobalData((string)Session["loggedInUserFullName"]);
            string fileStoreFolder = gd.GetStringValue("FileStoreFolder") + @"\temp";

            string originalFileName = Path.GetFileName(eventPicFileUpload.PostedFile.FileName);
            string destPath = Path.Combine(fileStoreFolder, originalFileName);
            destPath = destPath.Replace(" ", "_");
            destPath = MiscUtils.GetUniqueFileName(destPath);
            string savedFilename = Path.GetFileName(destPath);

            eventPicFileUpload.PostedFile.SaveAs(destPath);

            MiscUtils.CreateEventPicPreviews(Path.GetFileName(destPath), eventID);
        }

        DateTime alertDate = CalendarAlertDate.SelectedDate;
        string alertText = newAlertTextBox.Text.Trim();

        if (alertText != "")
        {
            EventAlert eventAlert = new EventAlert((string)Application["connectionString"]);
            eventAlert.alertDate = alertDate;
            eventAlert.eventID = eventID;
            eventAlert.alertText = alertText;
            eventAlert.Add();

            newAlertTextBox.Text = "";
            CalendarAlertDate.SelectedDate = DateTime.Now;
            PickerAlertDate.SelectedDate = DateTime.Now;
        }

        Response.Redirect("addEventInvites.aspx?EID=" + eventID.ToString());
    }

    //===============================================================
    // Function: skipUploadButton_click
    //===============================================================
    protected void skipUploadButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("addEventInvites.aspx?EID=" + eventID.ToString());
    }
}

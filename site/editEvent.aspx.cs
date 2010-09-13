//===============================================================
// Filename: editEvent.aspx.cs
// Date: 08/09/09
// --------------------------------------------------------------
// Description:
//   Edit event
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
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Globalization;
using Sedogo.BusinessObjects;

public partial class editEvent : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if( !IsPostBack )
        {
            int eventID = int.Parse(Request.QueryString["EID"]);

            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), 
                int.Parse(Session["loggedInUserID"].ToString()));
            int userAgeYears = DateTime.Now.Year - user.birthday.Year;
            if (sedogoEvent.dateType == "A" && sedogoEvent.beforeBirthday < userAgeYears)
            {
                userAgeYears = sedogoEvent.beforeBirthday;
            }

            for (int age = userAgeYears; age <= 100; age++)
            {
                birthdayDropDownList.Items.Add(new ListItem(age.ToString(), age.ToString()));
            }

            CalendarStartDate.SelectedDate = DateTime.Now;
            PickerStartDate.SelectedDate = DateTime.Now;
            CalendarRangeStartDate.SelectedDate = DateTime.Now;
            PickerRangeStartDate.SelectedDate = DateTime.Now;
            CalendarRangeEndDate.SelectedDate = DateTime.Now;
            PickerRangeEndDate.SelectedDate = DateTime.Now;

            categoryDropDownList.SelectedValue = "1";
            startDateLI.Visible = true;
            dateRangeLI1.Visible = false;
            dateRangeLI2.Visible = false;
            birthdayLI.Visible = false;

            try
            {
                SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

                SqlCommand cmd = new SqlCommand("spSelectTimezoneList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                timezoneDropDownList.DataSource = ds;
                timezoneDropDownList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            eventNameTextBox.Text = sedogoEvent.eventName;
            eventDescriptionTextBox.Text = sedogoEvent.eventDescription;
            eventVenueTextBox.Text = sedogoEvent.eventVenue;
            timezoneDropDownList.SelectedValue = sedogoEvent.timezoneID.ToString();

            if (sedogoEvent.dateType == "D")
            {
                CalendarStartDate.SelectedDate = sedogoEvent.startDate;
                PickerStartDate.SelectedDate = sedogoEvent.startDate;
            }
            if (sedogoEvent.dateType == "R")
            {
                CalendarRangeStartDate.SelectedDate = sedogoEvent.rangeStartDate;
                PickerRangeStartDate.SelectedDate = sedogoEvent.rangeStartDate;
                CalendarRangeEndDate.SelectedDate = sedogoEvent.rangeEndDate;
                PickerRangeEndDate.SelectedDate = sedogoEvent.rangeEndDate;
            }
            if (sedogoEvent.dateType == "A")
            {
                birthdayDropDownList.SelectedValue = sedogoEvent.beforeBirthday.ToString();
            }

            dateTypeDropDownList.SelectedValue = sedogoEvent.dateType;
            categoryDropDownList.SelectedValue = sedogoEvent.categoryID.ToString();
            privateEventCheckbox.Checked = sedogoEvent.privateEvent;

            ShowHideDates(sedogoEvent.dateType);
            SetFocus(eventNameTextBox);
        }
    }

    //===============================================================
    // Function: dateTypeDropDownList_changed
    //===============================================================
    protected void dateTypeDropDownList_changed(object sender, EventArgs e)
    {
        string dateType = dateTypeDropDownList.SelectedValue;

        ShowHideDates(dateTypeDropDownList.SelectedValue);
    }

    //===============================================================
    // Function: datePickList_changed
    //===============================================================
    protected void datePickList_changed(object sender, EventArgs e)
    {
        if (datePickList.SelectedValue == "5")
        {
            CalendarStartDate.SelectedDate = DateTime.Now.AddYears(5);
            PickerStartDate.SelectedDate = DateTime.Now.AddYears(5);
        }
        if (datePickList.SelectedValue == "10")
        {
            CalendarStartDate.SelectedDate = DateTime.Now.AddYears(10);
            PickerStartDate.SelectedDate = DateTime.Now.AddYears(10);
        }
        if (datePickList.SelectedValue == "20")
        {
            CalendarStartDate.SelectedDate = DateTime.Now.AddYears(20);
            PickerStartDate.SelectedDate = DateTime.Now.AddYears(20);
        }
        datePickList.SelectedValue = "";
    }

    //===============================================================
    // Function: daterangePickList_changed
    //===============================================================
    protected void daterangePickList_changed(object sender, EventArgs e)
    {
        if (daterangePickList.SelectedValue == "1")
        {
            CalendarRangeEndDate.SelectedDate = DateTime.Now.AddYears(1).AddDays(-1);
            PickerRangeEndDate.SelectedDate = DateTime.Now.AddYears(1).AddDays(-1);
        }
        if (daterangePickList.SelectedValue == "5")
        {
            CalendarRangeEndDate.SelectedDate = DateTime.Now.AddYears(5).AddDays(-1);
            PickerRangeEndDate.SelectedDate = DateTime.Now.AddYears(5).AddDays(-1);
        }
        if (daterangePickList.SelectedValue == "10")
        {
            CalendarRangeEndDate.SelectedDate = DateTime.Now.AddYears(10).AddDays(-1);
            PickerRangeEndDate.SelectedDate = DateTime.Now.AddYears(10).AddDays(-1);
        }
        if (daterangePickList.SelectedValue == "20")
        {
            CalendarRangeEndDate.SelectedDate = DateTime.Now.AddYears(20).AddDays(-1);
            PickerRangeEndDate.SelectedDate = DateTime.Now.AddYears(20).AddDays(-1);
        }
        daterangePickList.SelectedValue = "";
    }

    //===============================================================
    // Function: ShowHideDates
    //===============================================================
    private void ShowHideDates(string dateType)
    {
        if (dateType == "D")
        {
            startDateLI.Visible = true;
            dateRangeLI1.Visible = false;
            dateRangeLI2.Visible = false;
            birthdayLI.Visible = false;
        }
        if (dateType == "R")
        {
            startDateLI.Visible = false;
            dateRangeLI1.Visible = true;
            dateRangeLI2.Visible = true;
            birthdayLI.Visible = false;
        }
        if (dateType == "A")
        {
            startDateLI.Visible = false;
            dateRangeLI1.Visible = false;
            dateRangeLI2.Visible = false;
            birthdayLI.Visible = true;
        }
    }

    //===============================================================
    // Function: saveChangesButton_click
    //===============================================================
    protected void saveChangesButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
        int loggedInUserID = int.Parse(Session["loggedInUserID"].ToString());

        string eventName = eventNameTextBox.Text;

        //sedogoEvent.userID = int.Parse(Session["loggedInUserID"].ToString());
        sedogoEvent.eventName = eventName;
        sedogoEvent.eventDescription = eventDescriptionTextBox.Text;
        sedogoEvent.eventVenue = eventVenueTextBox.Text;

        if (dateTypeDropDownList.SelectedValue == "D")
        {
            sedogoEvent.startDate = CalendarStartDate.SelectedDate;
        }
        if (dateTypeDropDownList.SelectedValue == "R")
        {
            sedogoEvent.rangeStartDate = CalendarRangeStartDate.SelectedDate;
            sedogoEvent.rangeEndDate = CalendarRangeEndDate.SelectedDate;
        }
        if (dateTypeDropDownList.SelectedValue == "A")
        {
            sedogoEvent.beforeBirthday = int.Parse(birthdayDropDownList.SelectedValue);
        }
        sedogoEvent.dateType = dateTypeDropDownList.SelectedValue;
        sedogoEvent.categoryID = int.Parse(categoryDropDownList.SelectedValue);
        sedogoEvent.privateEvent = privateEventCheckbox.Checked;
        sedogoEvent.mustDo = false;
        sedogoEvent.timezoneID = int.Parse(timezoneDropDownList.SelectedValue);
        sedogoEvent.Update();

        sedogoEvent.SendEventUpdateEmail(loggedInUserID);

        //Response.Redirect("profileRedirect.aspx");
        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }
}

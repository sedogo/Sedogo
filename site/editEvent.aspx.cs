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

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), 
                int.Parse(Session["loggedInUserID"].ToString()));

            for (int day = 1; day <= 31; day++)
            {
                startDateDay.Items.Add(new ListItem(day.ToString(), day.ToString()));
                dateRangeStartDay.Items.Add(new ListItem(day.ToString(), day.ToString()));
                dateRangeEndDay.Items.Add(new ListItem(day.ToString(), day.ToString()));
            }
            for (int month = 1; month <= 12; month++)
            {
                DateTime loopDate = new DateTime(DateTime.Now.Year, month, 1);
                startDateMonth.Items.Add(new ListItem(loopDate.ToString("MMMM", CultureInfo.InvariantCulture), month.ToString()));
                dateRangeStartMonth.Items.Add(new ListItem(loopDate.ToString("MMMM", CultureInfo.InvariantCulture), month.ToString()));
                dateRangeEndMonth.Items.Add(new ListItem(loopDate.ToString("MMMM", CultureInfo.InvariantCulture), month.ToString()));
            }
            for (int year = DateTime.Now.Year; year <= 2100; year++)
            {
                startDateYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                dateRangeStartYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                dateRangeEndYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            for (int age = 1; age <= 100; age++)
            {
                birthdayDropDownList.Items.Add(new ListItem(age.ToString(), age.ToString()));
            }

            hiddenStartDate.Attributes.Add("style", "display:none");
            startDateDay.Attributes.Add("onchange", "setHiddenStartDateField()");
            startDateMonth.Attributes.Add("onchange", "setHiddenStartDateField()");
            startDateYear.Attributes.Add("onchange", "setHiddenStartDateField()");

            startDateDay.SelectedValue = DateTime.Now.Day.ToString();
            startDateMonth.SelectedValue = DateTime.Now.Month.ToString();
            startDateYear.SelectedValue = DateTime.Now.Year.ToString();

            hiddenDateRangeStartDate.Attributes.Add("style", "display:none");
            dateRangeStartDay.Attributes.Add("onchange", "setHiddenRangeStartDateField()");
            dateRangeStartMonth.Attributes.Add("onchange", "setHiddenRangeStartDateField()");
            dateRangeStartYear.Attributes.Add("onchange", "setHiddenRangeStartDateField()");

            dateRangeStartDay.SelectedValue = DateTime.Now.Day.ToString();
            dateRangeStartMonth.SelectedValue = DateTime.Now.Month.ToString();
            dateRangeStartYear.SelectedValue = DateTime.Now.Year.ToString();

            hiddenDateRangeEndDate.Attributes.Add("style", "display:none");
            dateRangeEndDay.Attributes.Add("onchange", "setHiddenRangeEndDateField()");
            dateRangeEndMonth.Attributes.Add("onchange", "setHiddenRangeEndDateField()");
            dateRangeEndYear.Attributes.Add("onchange", "setHiddenRangeEndDateField()");

            dateRangeEndDay.SelectedValue = DateTime.Now.Day.ToString();
            dateRangeEndMonth.SelectedValue = DateTime.Now.Month.ToString();
            dateRangeEndYear.SelectedValue = DateTime.Now.Year.ToString();

            categoryDropDownList.SelectedValue = "1";
            startDateLI.Visible = true;
            dateRangeLI1.Visible = false;
            dateRangeLI2.Visible = false;
            birthdayLI.Visible = false;

            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

            eventNameTextBox.Text = sedogoEvent.eventName;

            if (sedogoEvent.dateType == "D")
            {
                startDateDay.SelectedValue = sedogoEvent.startDate.Day.ToString();
                startDateMonth.SelectedValue = sedogoEvent.startDate.Month.ToString();
                startDateYear.SelectedValue = sedogoEvent.startDate.Year.ToString();
            }
            if (sedogoEvent.dateType == "R")
            {
                dateRangeStartDay.SelectedValue = sedogoEvent.rangeStartDate.Day.ToString();
                dateRangeStartMonth.SelectedValue = sedogoEvent.rangeStartDate.Month.ToString();
                dateRangeStartYear.SelectedValue = sedogoEvent.rangeStartDate.Year.ToString();

                dateRangeEndDay.SelectedValue = sedogoEvent.rangeEndDate.Day.ToString();
                dateRangeEndMonth.SelectedValue = sedogoEvent.rangeEndDate.Month.ToString();
                dateRangeEndYear.SelectedValue = sedogoEvent.rangeEndDate.Year.ToString();
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
        ShowHideDates(dateTypeDropDownList.SelectedValue);
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

        string eventName = eventNameTextBox.Text;

        sedogoEvent.userID = int.Parse(Session["loggedInUserID"].ToString());
        sedogoEvent.eventName = eventName;
        if (dateTypeDropDownList.SelectedValue == "D")
        {
            DateTime startDate = new DateTime(int.Parse(startDateYear.SelectedValue),
                int.Parse(startDateMonth.SelectedValue), int.Parse(startDateDay.SelectedValue));
            sedogoEvent.startDate = startDate;
        }
        if (dateTypeDropDownList.SelectedValue == "R")
        {
            //sedogoEvent.startDate = DateTime.MinValue;
            DateTime dateRangeStart = new DateTime(int.Parse(dateRangeStartYear.SelectedValue),
                int.Parse(dateRangeStartMonth.SelectedValue), int.Parse(dateRangeStartDay.SelectedValue));
            DateTime dateRangeEnd = new DateTime(int.Parse(dateRangeEndYear.SelectedValue),
                int.Parse(dateRangeEndMonth.SelectedValue), int.Parse(dateRangeEndDay.SelectedValue));
            sedogoEvent.rangeStartDate = dateRangeStart;
            sedogoEvent.rangeEndDate = dateRangeEnd;
        }
        if (dateTypeDropDownList.SelectedValue == "A")
        {
            sedogoEvent.beforeBirthday = int.Parse(birthdayDropDownList.SelectedValue);
        }
        sedogoEvent.dateType = dateTypeDropDownList.SelectedValue;
        sedogoEvent.categoryID = int.Parse(categoryDropDownList.SelectedValue);
        sedogoEvent.privateEvent = privateEventCheckbox.Checked;
        sedogoEvent.Update();

        Response.Redirect("profileRedirect.aspx");
    }
}

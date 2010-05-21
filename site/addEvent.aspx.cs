//===============================================================
// Filename: addEvent.aspx.cs
// Date: 07/09/09
// --------------------------------------------------------------
// Description:
//   Add event
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 07/09/09
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

public partial class addEvent : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), 
                int.Parse(Session["loggedInUserID"].ToString()));
            int userAgeYears = DateTime.Now.Year - user.birthday.Year;

            CalendarStartDate.SelectedDate = DateTime.Now;
            PickerStartDate.SelectedDate = DateTime.Now;
            CalendarRangeStartDate.SelectedDate = DateTime.Now;
            PickerRangeStartDate.SelectedDate = DateTime.Now;
            CalendarRangeEndDate.SelectedDate = DateTime.Now;
            PickerRangeEndDate.SelectedDate = DateTime.Now;

            for (int age = userAgeYears; age <= 100; age++)
            {
                birthdayDropDownList.Items.Add(new ListItem(age.ToString(), age.ToString()));

                if( user.birthday > DateTime.MinValue )
                {
                    int currentAge = DateTime.Now.Year - user.birthday.Year;
                    birthdayDropDownList.SelectedValue = currentAge.ToString();
                }
            }

            dateTypeDropDownList.SelectedValue = "A";
            categoryDropDownList.SelectedValue = "1";
            startDateLI.Visible = false;
            dateRangeLI1.Visible = false;
            dateRangeLI2.Visible = false;
            birthdayLI.Visible = true;

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

                timezoneDropDownList.SelectedValue = user.timezoneID.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if( Request.QueryString["Name"] != null )
            {
                eventNameTextBox.Text = Request.QueryString["Name"].ToString();
            }

            SetFocus(eventNameTextBox);
        }
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
    // Function: dateTypeDropDownList_changed
    //===============================================================
    protected void dateTypeDropDownList_changed(object sender, EventArgs e)
    {
        if( dateTypeDropDownList.SelectedValue == "D" )
        {
            startDateLI.Visible = true;
            dateRangeLI1.Visible = false;
            dateRangeLI2.Visible = false;
            birthdayLI.Visible = false;
        }
        if( dateTypeDropDownList.SelectedValue == "R" )
        {
            startDateLI.Visible = false;
            dateRangeLI1.Visible = true;
            dateRangeLI2.Visible = true;
            birthdayLI.Visible = false;
        }
        if( dateTypeDropDownList.SelectedValue == "A" )
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
        string eventName = eventNameTextBox.Text;

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString());
        sedogoEvent.userID = int.Parse(Session["loggedInUserID"].ToString());
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
        sedogoEvent.Add();

        Response.Redirect("viewEvent.aspx?EID=" + sedogoEvent.eventID.ToString());
    }
}

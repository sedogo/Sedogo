﻿//===============================================================
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
            for (int day = 1; day <= 31; day++)
            {
                startDateDay.Items.Add(new ListItem(day.ToString(), day.ToString()));
            }
            for (int month = 1; month <= 12; month++)
            {
                DateTime loopDate = new DateTime(DateTime.Now.Year, month, 1);
                startDateMonth.Items.Add(new ListItem(loopDate.ToString("MMMM", CultureInfo.InvariantCulture), month.ToString()));
            }
            for (int year = DateTime.Now.Year; year <= 2100; year++)
            {
                startDateYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            hiddenStartDate.Attributes.Add("style", "display:none");
            startDateDay.Attributes.Add("onchange", "setHiddenStartDateField()");
            startDateMonth.Attributes.Add("onchange", "setHiddenStartDateField()");
            startDateYear.Attributes.Add("onchange", "setHiddenStartDateField()");

            startDateDay.SelectedValue = DateTime.Now.Day.ToString();
            startDateMonth.SelectedValue = DateTime.Now.Month.ToString();
            startDateYear.SelectedValue = DateTime.Now.Year.ToString();

            categoryDropDownList.SelectedValue = "1";

            SetFocus(eventNameTextBox);
        }
    }

    //===============================================================
    // Function: saveChangesButton_click
    //===============================================================
    protected void saveChangesButton_click(object sender, EventArgs e)
    {
        string eventName = eventNameTextBox.Text;

        DateTime startDate = new DateTime(int.Parse(startDateYear.SelectedValue),
            int.Parse(startDateMonth.SelectedValue), int.Parse(startDateDay.SelectedValue));

        SedogoEvent sedogoEvent = new SedogoEvent("");
        sedogoEvent.userID = int.Parse(Session["loggedInUserID"].ToString());
        sedogoEvent.eventName = eventName;
        sedogoEvent.startDate = startDate;
        sedogoEvent.categoryID = int.Parse(categoryDropDownList.SelectedValue);
        sedogoEvent.Add();

        Response.Redirect("profileRedirect.aspx");
    }
}

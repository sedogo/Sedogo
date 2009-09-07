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
using System.Linq;
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
            SetFocus(eventNameTextBox);

            for (int day = 1; day <= 31; day++)
            {
                startDateDay.Items.Add(new ListItem(day.ToString(), day.ToString()));
                endDateDay.Items.Add(new ListItem(day.ToString(), day.ToString()));
            }
            for (int month = 1; month <= 12; month++)
            {
                DateTime loopDate = new DateTime(DateTime.Now.Year, month, 1);
                startDateMonth.Items.Add(new ListItem(loopDate.ToString("MMMM", CultureInfo.InvariantCulture), month.ToString()));
                endDateMonth.Items.Add(new ListItem(loopDate.ToString("MMMM", CultureInfo.InvariantCulture), month.ToString()));
            }
            for (int year = DateTime.Now.Year; year <= 2100; year++)
            {
                startDateYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                endDateYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            hiddenStartDate.Attributes.Add("style", "display:none");
            hiddenEndDate.Attributes.Add("style", "display:none");
            startDateDay.Attributes.Add("onchange", "setHiddenStartDateField()");
            endDateDay.Attributes.Add("onchange", "setHiddenEndDateField()");
            startDateMonth.Attributes.Add("onchange", "setHiddenStartDateField()");
            endDateMonth.Attributes.Add("onchange", "setHiddenEndDateField()");
            startDateYear.Attributes.Add("onchange", "setHiddenStartDateField()");
            endDateYear.Attributes.Add("onchange", "setHiddenEndDateField()");

            startDateDay.SelectedValue = DateTime.Now.Day.ToString();
            endDateDay.SelectedValue = DateTime.Now.Day.ToString();
            startDateMonth.SelectedValue = DateTime.Now.Month.ToString();
            endDateMonth.SelectedValue = DateTime.Now.Month.ToString();
            startDateYear.SelectedValue = DateTime.Now.Year.ToString();
            endDateYear.SelectedValue = DateTime.Now.Year.ToString();
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
        DateTime endDate = new DateTime(int.Parse(endDateYear.SelectedValue),
            int.Parse(endDateMonth.SelectedValue), int.Parse(endDateDay.SelectedValue));

        SedogoEvent sedogoEvent = new SedogoEvent("");
        sedogoEvent.userID = int.Parse(Session["loggedInUserID"].ToString());
        sedogoEvent.eventName = eventName;
        sedogoEvent.startDate = startDate;
        sedogoEvent.endDate = endDate;
        sedogoEvent.Add();

        Response.Redirect("profileRedirect.aspx");
    }
}

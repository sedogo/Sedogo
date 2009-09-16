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

public partial class editEvent : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if( !IsPostBack )
        {
            int eventID = int.Parse(Request.QueryString["EID"]);

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

            SedogoEvent sedogoEvent = new SedogoEvent("", eventID);

            eventNameTextBox.Text = sedogoEvent.eventName;
            startDateDay.SelectedValue = sedogoEvent.startDate.Day.ToString();
            startDateMonth.SelectedValue = sedogoEvent.startDate.Month.ToString();
            startDateYear.SelectedValue = sedogoEvent.startDate.Year.ToString();
            categoryDropDownList.SelectedValue = sedogoEvent.categoryID.ToString();

            SetFocus(eventNameTextBox);
        }
    }

    //===============================================================
    // Function: saveChangesButton_click
    //===============================================================
    protected void saveChangesButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEvent sedogoEvent = new SedogoEvent("", eventID);

        string eventName = eventNameTextBox.Text;

        DateTime startDate = new DateTime(int.Parse(startDateYear.SelectedValue),
            int.Parse(startDateMonth.SelectedValue), int.Parse(startDateDay.SelectedValue));

        sedogoEvent.userID = int.Parse(Session["loggedInUserID"].ToString());
        sedogoEvent.eventName = eventName;
        sedogoEvent.startDate = startDate;
        sedogoEvent.categoryID = int.Parse(categoryDropDownList.SelectedValue);
        sedogoEvent.Update();

        Response.Redirect("profileRedirect.aspx");
    }
}

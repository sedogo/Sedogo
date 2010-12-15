//===============================================================
// Filename: eventAlerts.aspx.cs
// Date: 26/10/09
// --------------------------------------------------------------
// Description:
//   Event alerts
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 26/10/09
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
using System.Net.Mail;
using Sedogo.BusinessObjects;

public partial class eventAlerts : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);
            string action = "";
            int eventAlertID = -1;
            if (Request.QueryString["A"] != null && Request.QueryString["EAID"] != null)
            {
                action = Request.QueryString["A"].ToString();
                eventAlertID = int.Parse(Request.QueryString["EAID"]);

                if (action == "Delete")
                {
                    try
                    {
                        EventAlert alertToDelete = new EventAlert(Session["loggedInUserFullName"].ToString(), eventAlertID);
                        alertToDelete.Delete();
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert",
                            "alert(\"Error: " + ex.Message + "\");", true);
                    }
                }
            }

            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
            SedogoUser eventOwner = new SedogoUser("", sedogoEvent.userID);

            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventOwner, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);

            eventTitleLabel.Text = sedogoEvent.eventName;
            eventOwnersNameLabel.Text = eventOwner.firstName + " " + eventOwner.lastName;
            eventDateLabel.Text = dateString;
            eventDescriptionLabel.Text = sedogoEvent.eventDescription.Replace("\n", "<br/>");

            CalendarAlertDate.SelectedDate = DateTime.Now;
            PickerAlertDate.SelectedDate = DateTime.Now;

            PopulateAlerts(eventID);
        }
    }

    //===============================================================
    // Function: PopulateAlerts
    //===============================================================
    private void PopulateAlerts(int eventID)
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectEventAlertListPending";
            cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
            DbDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows == true)
            {
                while (rdr.Read())
                {
                    int eventAlertID = int.Parse(rdr["EventAlertID"].ToString());
                    DateTime alertDate = (DateTime)rdr["AlertDate"];
                    string alertText = (string)rdr["AlertText"];
                    DateTime createdDate = (DateTime)rdr["CreatedDate"];

                    string outputText = "<h3>" + alertDate.ToString("ddd d MMMM yyyy") + "</h3><p>";
                    outputText = outputText + "<i>" + alertText.Replace("\n","<br/>") + "</i>";
                    outputText = outputText + " (<a href=\"eventAlerts.aspx?EID=" + eventID.ToString()
                        + "&A=Delete&EAID=" + eventAlertID.ToString() + "\">Clear reminder</a>)";
                    outputText = outputText + "</p>";

                    currentAlertsPlaceholder.Controls.Add(new LiteralControl(outputText));
                }
            }
            else
            {
                string outputText = "<p>&nbsp;<br/>There are no reminders for this event</p>";

                currentAlertsPlaceholder.Controls.Add(new LiteralControl(outputText));
            }
            rdr.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }
    }

    //===============================================================
    // Function: createAlertLink_click
    //===============================================================
    protected void createAlertLink_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        DateTime alertDate = CalendarAlertDate.SelectedDate;
        string alertText = newAlertTextBox.Text;

        EventAlert eventAlert = new EventAlert(Session["loggedInUserFullName"].ToString());
        eventAlert.alertDate = alertDate;
        eventAlert.eventID = eventID;
        eventAlert.alertText = alertText;
        eventAlert.Add();

        newAlertTextBox.Text = "";
        CalendarAlertDate.SelectedDate = DateTime.Now;
        PickerAlertDate.SelectedDate = DateTime.Now;

        PopulateAlerts(eventID);
    }

    //===============================================================
    // Function: click_backToEventDetailsLink
    //===============================================================
    protected void click_backToEventDetailsLink(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
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
        if( alertDatePickList.SelectedValue == "1W" )
        {
            CalendarAlertDate.SelectedDate = DateTime.Now.AddDays(7);
            PickerAlertDate.SelectedDate = DateTime.Now.AddDays(7);
        }
        if( alertDatePickList.SelectedValue == "1M" )
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

        PopulateAlerts(eventID);
    }
}

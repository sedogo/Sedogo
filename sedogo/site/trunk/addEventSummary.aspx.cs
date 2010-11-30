//===============================================================
// Filename: addEventSummary.aspx
// Date: 02/12/09
// --------------------------------------------------------------
// Description:
//   Add event summary
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 02/12/09
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
using Sedogo.BusinessObjects;

public partial class addEventSummary : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
        SedogoUser eventOwner = new SedogoUser(Session["loggedInUserFullName"].ToString(), sedogoEvent.userID);

        string dateString = "";
        if (sedogoEvent.dateType == "D")
        {
            // Event occurs on a specific date
            dateString = sedogoEvent.startDate.ToString("ddd d MMMM yyyy");
        }
        if (sedogoEvent.dateType == "R")
        {
            // Event occurs in a date range - use the start date
            dateString = sedogoEvent.rangeStartDate.ToString("ddd d MMMM yyyy") + " to " + sedogoEvent.rangeEndDate.ToString("ddd d MMMM yyyy");
        }
        if (sedogoEvent.dateType == "A")
        {
            // Event occurs before birthday
            string dateSuffix = "";
            switch (sedogoEvent.beforeBirthday)
            {
                case 1: case 21: case 31: case 41: case 51: case 61: case 71: case 81: case 91: case 101:
                    dateSuffix = "st";
                    break;
                case 2: case 22: case 32: case 42: case 52: case 62: case 72: case 82: case 92: case 102:
                    dateSuffix = "nd";
                    break;
                case 3: case 23: case 33: case 43: case 53: case 63: case 73: case 83: case 93: case 103:
                    dateSuffix = "rd";
                    break;
                case 4: case 5: case 6: case 7: case 8: case 9: case 10:
                case 11: case 12: case 13: case 14: case 15: case 16: case 17: case 18: case 19: case 20:
                case 24: case 25: case 26: case 27: case 28: case 29: case 30:
                case 34: case 35: case 36: case 37: case 38: case 39: case 40:
                case 44: case 45: case 46: case 47: case 48: case 49: case 50:
                case 54: case 55: case 56: case 57: case 58: case 59: case 60:
                case 64: case 65: case 66: case 67: case 68: case 69: case 70:
                case 74: case 75: case 76: case 77: case 78: case 79: case 80:
                case 84: case 85: case 86: case 87: case 88: case 89: case 90:
                case 94: case 95: case 96: case 97: case 98: case 99: case 100:
                case 104: case 105: case 106: case 107: case 108: case 109: case 110:
                    dateSuffix = "th";
                    break;
                default:
                    break;
            }
            dateString = "Before " + sedogoEvent.beforeBirthday.ToString() + dateSuffix + " birthday";
        }
        int inviteCount = EventInvite.GetInviteCount(eventID);

        eventNameLabel.Text = sedogoEvent.eventName;
        whoLabel.Text = eventOwner.firstName + " " + eventOwner.lastName;
        goalNameLabel.Text = sedogoEvent.eventName;
        if (sedogoEvent.eventDescription == "")
        {
            descriptionLabel.Text = "&nbsp;";
        }
        else
        {
            descriptionLabel.Text = sedogoEvent.eventDescription.Replace("\n", "<br/>");
        }
        venueLabel.Text = sedogoEvent.eventVenue;
        dateLabel.Text = dateString;
        reminderLabel.Text = GetAlertsText(eventID);
        invitesLabel.Text = inviteCount.ToString() + " pending";

        if (sedogoEvent.eventPicPreview == "")
        {
            eventImage.ImageUrl = "~/images/eventImageBlank.png";
        }
        else
        {
            eventImage.ImageUrl = ImageHelper.GetRelativeImagePath(sedogoEvent.eventID, sedogoEvent.eventGUID, ImageType.EventPreview);
        }
    }

    //===============================================================
    // Function: GetAlertsText
    //===============================================================
    private string GetAlertsText(int eventID)
    {
        string alertsString = "";

        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectUsersWithLastName";
            cmd.Parameters.Add("@Letter", SqlDbType.Char, 1).Value = eventID;
            DbDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows == true)
            {
                while (rdr.Read())
                {
                    int eventAlertID = int.Parse(rdr["EventAlertID"].ToString());
                    DateTime alertDate = (DateTime)rdr["AlertDate"];
                    string alertText = (string)rdr["AlertText"];
                    DateTime createdDate = (DateTime)rdr["CreatedDate"];

                    if (alertsString != "")
                    {
                        alertsString = alertsString + ", ";
                    }

                    alertsString = alertsString + alertDate.ToString("ddd d MMMM yyyy");
                }
            }
            else
            {
                alertsString = "None";
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

        return alertsString;
    }

    //===============================================================
    // Function: click_viewEventLink
    //===============================================================
    protected void click_viewEventLink(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }
}

//===============================================================
// Filename: sendMessageToTrackers.aspx.cs
// Date: 01/11/09
// --------------------------------------------------------------
// Description:
//   Send message to event trackers
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 01/11/09
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

public partial class sendMessageToTrackers : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        if (!IsPostBack)
        {

            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

            eventNameLabel.Text = sedogoEvent.eventName;

            SetFocus(messageTextBox);
        }
        PopulateTrackingList(eventID);
    }

    //===============================================================
    // Function: PopulateTrackingList
    //===============================================================
    private void PopulateTrackingList(int eventID)
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            trackingLinksPlaceholder.Controls.Add(new LiteralControl("<p>"));

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectTrackingUsersByEventID";
            cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string profilePicThumbnail = "";

                int trackedEventID = int.Parse(rdr["TrackedEventID"].ToString());
                //int userID = int.Parse(rdr["UserID"].ToString());
                string firstName = (string)rdr["FirstName"];
                string lastName = (string)rdr["LastName"];
                //string gender = (string)rdr["Gender"];
                //string homeTown = (string)rdr["HomeTown"];
                //string emailAddress = (string)rdr["EmailAddress"];
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicThumbnail")))
                {
                    profilePicThumbnail = (string)rdr["ProfilePicThumbnail"];
                }
                //string profilePicPreview = (string)rdr["ProfilePicPreview"];

                string profileImagePath = "./images/profile/blankProfile.jpg";
                if (profilePicThumbnail != "")
                {
                    profileImagePath = "./assets/profilePics/" + profilePicThumbnail;
                }

                CheckBox trackerCheckBox = new CheckBox();
                trackerCheckBox.ID = "trackerCheckBox_" + trackedEventID.ToString();
                trackerCheckBox.Checked = true;

                string outputText = "<img src=\"" + profileImagePath + "\" />&nbsp;"
                    + firstName + " " + lastName;

                //trackingLinksPlaceholder.Controls.Add(new LiteralControl("<table><tr><td>"));
                trackingLinksPlaceholder.Controls.Add(trackerCheckBox);
                trackingLinksPlaceholder.Controls.Add(new LiteralControl("&nbsp;"));
                trackingLinksPlaceholder.Controls.Add(new LiteralControl(outputText));
                trackingLinksPlaceholder.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                //trackingLinksPlaceholder.Controls.Add(new LiteralControl("</td></tr></table>"));
            }
            rdr.Close();

            trackingLinksPlaceholder.Controls.Add(new LiteralControl("</p>"));
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
    // Function: saveChangesButton_click
    //===============================================================
    protected void saveChangesButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
        GlobalData gd = new GlobalData("");
        SedogoUser currentUser = new SedogoUser(Session["loggedInUserFullName"].ToString(),
            int.Parse(Session["loggedInUserID"].ToString()));

        string messageText = messageTextBox.Text;

        StringBuilder emailBodyCopy = new StringBuilder();

        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectTrackingUsersByEventID";
            cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int trackedEventID = int.Parse(rdr["TrackedEventID"].ToString());
                int userID = int.Parse(rdr["UserID"].ToString());
                string firstName = (string)rdr["FirstName"];
                string lastName = (string)rdr["LastName"];
                string emailAddress = (string)rdr["EmailAddress"];

                string fieldID = "trackerCheckBox_" + trackedEventID.ToString();
                CheckBox trackerCheckBox = (CheckBox)FindControl(fieldID);

                if (trackerCheckBox.Checked == true)
                {
                    Message message = new Message(Session["loggedInUserFullName"].ToString());
                    message.userID = userID;
                    message.eventID = eventID;
                    message.postedByUserID = int.Parse(Session["loggedInUserID"].ToString());
                    message.messageText = messageText;
                    message.Add();

                    emailBodyCopy.AppendLine("From: " + currentUser.firstName + " " + currentUser.lastName + "<br/>");
                    emailBodyCopy.AppendLine(messageText.Replace("\n", "<br/>"));

                    string emailSubject = "Sedogo message from " + currentUser.firstName + " regarding " + sedogoEvent.eventName;

                    string SMTPServer = gd.GetStringValue("SMTPServer");
                    string mailFromAddress = gd.GetStringValue("MailFromAddress");
                    string mailFromUsername = gd.GetStringValue("MailFromUsername");
                    string mailFromPassword = gd.GetStringValue("MailFromPassword");

                    MailMessage mailMessage = new MailMessage(mailFromAddress, emailAddress);
                    mailMessage.ReplyTo = new MailAddress(mailFromAddress);

                    mailMessage.Subject = emailSubject;
                    mailMessage.Body = emailBodyCopy.ToString();
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = SMTPServer;
                    if (mailFromPassword != "")
                    {
                        // If the password is blank, assume mail relay is permitted
                        smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
                    }
                    smtp.Send(mailMessage);
                }
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

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }
}

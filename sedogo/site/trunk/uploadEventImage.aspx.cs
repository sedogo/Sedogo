//===============================================================
// Filename: uploadEventImage.aspx
// Date: 14/09/09
// --------------------------------------------------------------
// Description:
//   Upload event pic
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 14/09/09
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

public partial class uploadEventImage : SedogoPage
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

            SedogoUser eventOwner = new SedogoUser(Session["loggedInUserFullName"].ToString(), sedogoEvent.userID);
            string dateString = "";
            DateTime startDate = sedogoEvent.startDate;
            MiscUtils.GetDateStringStartDate(eventOwner, sedogoEvent.dateType, sedogoEvent.rangeStartDate,
                sedogoEvent.rangeEndDate, sedogoEvent.beforeBirthday, ref dateString, ref startDate);

            eventDateLabel.Text = dateString;
            eventVenueLabel.Text = sedogoEvent.eventVenue.Replace("\n", "<br/>");

            deleteImageButton.Attributes.Add("onclick", "if(confirm('Are you sure you want to delete this picture?')){}else{return false}");

            if (sedogoEvent.eventPicFilename == "")
            {
                deleteImageButton.Visible = false;
            }

            SetFocus(eventPicFileUpload);
        }
    }

    //===============================================================
    // Function: uploadEventPicButton_click
    //===============================================================
    protected void uploadEventPicButton_click(object sender, EventArgs e)
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

            // Update event to set last updated date
            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
            sedogoEvent.Update();

            Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
        }
    }

    //===============================================================
    // Function: deleteImageButton_click
    //===============================================================
    protected void deleteImageButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);
        sedogoEvent.eventPicFilename = "";
        sedogoEvent.eventPicPreview = "";
        sedogoEvent.eventPicThumbnail = "";
        sedogoEvent.UpdateEventPic();

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }
}

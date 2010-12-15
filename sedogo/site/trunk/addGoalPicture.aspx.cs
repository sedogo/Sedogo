//===============================================================
// Filename: addGoalPicture.aspx
// Date: 05/07/10
// --------------------------------------------------------------
// Description:
//   Upload goal pic
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 05/07/10
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

public partial class addGoalPicture : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);
            int userID = -1;
            string loggedInUserName = "";
            if (Session["loggedInUserID"] != null)
            {
                userID = int.Parse(Session["loggedInUserID"].ToString());
                loggedInUserName = Session["loggedInUserFullName"].ToString();
            }

            sidebarControl.userID = userID;
            if (userID > 0)
            {
                SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
                sidebarControl.user = user;
                bannerAddFindControl.userID = userID;
            }
            else
            {
            }

            SedogoEvent sedogoEvent = new SedogoEvent(loggedInUserName, eventID);
            eventTitleLabel.Text = sedogoEvent.eventName;

            //pageTitleUserName.Text = sedogoEvent.eventName + " pictures : Sedogo : Create your future and connect with others to make it happen";
            string timelineColour = "#cd3301";
            switch (sedogoEvent.categoryID)
            {
                case 1:
                    timelineColour = "#cd3301";
                    break;
                case 2:
                    timelineColour = "#ff0b0b";
                    break;
                case 3:
                    timelineColour = "#ff6801";
                    break;
                case 4:
                    timelineColour = "#ff8500";
                    break;
                case 5:
                    timelineColour = "#d5b21a";
                    break;
                case 6:
                    timelineColour = "#8dc406";
                    break;
                case 7:
                    timelineColour = "#5b980c";
                    break;
                case 8:
                    timelineColour = "#079abc";
                    break;
                case 9:
                    timelineColour = "#5ab6cd";
                    break;
                case 10:
                    timelineColour = "#8a67c1";
                    break;
                case 11:
                    timelineColour = "#e54ecf";
                    break;
                case 12:
                    timelineColour = "#a5369c";
                    break;
                case 13:
                    timelineColour = "#a32672";
                    break;
            }
            pageBannerBarDiv.Style.Add("background-color", timelineColour);

            SetFocus(goalPicFileUpload);
        }
    }

    //===============================================================
    // Function: uploadProfilePicButton_click
    //===============================================================
    protected void uploadProfilePicButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        int userID = int.Parse(Session["loggedInUserID"].ToString());

        if (goalPicFileUpload.PostedFile.ContentLength != 0)
        {
            SedogoEvent sedogoEvent = new SedogoEvent((string)Session["loggedInUserFullName"], eventID);
            sedogoEvent.Update();

            int fileSizeBytes = goalPicFileUpload.PostedFile.ContentLength;

            GlobalData gd = new GlobalData((string)Session["loggedInUserFullName"]);
            string fileStoreFolder = gd.GetStringValue("FileStoreFolder") + @"\temp";

            string originalFileName = Path.GetFileName(goalPicFileUpload.PostedFile.FileName);
            string destPath = Path.Combine(fileStoreFolder, originalFileName);
            destPath = destPath.Replace(" ", "_");
            destPath = MiscUtils.GetUniqueFileName(destPath);
            string savedFilename = Path.GetFileName(destPath);

            goalPicFileUpload.PostedFile.SaveAs(destPath);

            int status = MiscUtils.CreateGoalPicPreviews(Path.GetFileName(destPath),
                eventID, (string)Session["loggedInUserFullName"], userID, captionTextBox.Text);

            if (status >= 0)
            {
                ImageHelper.GetRelativeImagePath(status, sedogoEvent.eventGUID, ImageType.EventPreview, true);
                ImageHelper.GetRelativeImagePath(status, sedogoEvent.eventGUID, ImageType.EventThumbnail, true);

                Response.Redirect("morePictures.aspx?EID=" + eventID.ToString());
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"This type of image is not supported, please choose another.\");", true);
            }
        }
    }

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("morePictures.aspx?EID=" + eventID.ToString());
    }
}

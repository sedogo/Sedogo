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
        int userID = int.Parse(Session["loggedInUserID"].ToString());
        int eventID = int.Parse(Request.QueryString["EID"]);

        SetFocus(goalPicFileUpload);
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

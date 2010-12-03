//===============================================================
// Filename: uploadProfilePic.aspx
// Date: 05/09/09
// --------------------------------------------------------------
// Description:
//   Upload profile pic
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 05/09/09
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

public partial class uploadProfilePic : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Session["loggedInUserID"].ToString());

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);

            if (user.profilePicThumbnail != "")
            {
                // PD 3/12/10 - Removed this because it shows the wrong image
                //profileImage.ImageUrl = ImageHelper.GetRelativeImagePath(user.userID, user.GUID, ImageType.UserPreview);
                profileImage.ImageUrl = "~/assets/profilePics/" + user.profilePicThumbnail;
            }
            else
            {
                profileImage.ImageUrl = "~/images/profile/blankProfilePreview.jpg";
            }
            profileImage.ToolTip = user.fullName + "'s profile picture";

            SetFocus(profilePicFileUpload);
        }
    }

    //===============================================================
    // Function: uploadProfilePicButton_click
    //===============================================================
    protected void uploadProfilePicButton_click(object sender, EventArgs e)
    {
        if (profilePicFileUpload.PostedFile.ContentLength != 0)
        {
            int fileSizeBytes = profilePicFileUpload.PostedFile.ContentLength;

            GlobalData gd = new GlobalData((string)Session["loggedInUserFullName"]);
            string fileStoreFolder = gd.GetStringValue("FileStoreFolder") + @"\temp";

            string originalFileName = Path.GetFileName(profilePicFileUpload.PostedFile.FileName);
            string destPath = Path.Combine(fileStoreFolder, originalFileName);
            destPath = destPath.Replace(" ", "_");
            destPath = MiscUtils.GetUniqueFileName(destPath);
            string savedFilename = Path.GetFileName(destPath);

            if(!Directory.Exists(Path.GetDirectoryName(destPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(destPath));
            }
            profilePicFileUpload.PostedFile.SaveAs(destPath);

            int status = MiscUtils.CreatePreviews(Path.GetFileName(destPath), int.Parse(Session["loggedInUserID"].ToString()));

            // PD 3/12/10 - Removed due to server error on file upload
            //var user = new SedogoUser(string.Empty, int.Parse(Session["loggedInUserID"].ToString()));
            //ImageHelper.GetRelativeImagePath(user.userID, user.GUID, ImageType.UserPreview, true);
            //ImageHelper.GetRelativeImagePath(user.userID, user.GUID, ImageType.UserThumbnail, true);
            
            if (status >= 0)
            {
                Response.Redirect("profileRedirect.aspx");
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
        Response.Redirect("profileRedirect.aspx");
    }
}

//===============================================================
// Filename: setProfileImage.aspx.cs
// Date: 28/09/10
// --------------------------------------------------------------
// Description:
//   Login
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 28/09/10
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
using Telerik.Web.UI;
using System.IO;
using Sedogo.BusinessObjects;

public partial class setProfileImage : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RadComboBoxItem avatarItem1 = new RadComboBoxItem("Avatar 1", "1");
            avatarItem1.ImageUrl = "/images/avatars/avatar1sm.gif";
            avatarComboBox.Items.Add(avatarItem1);
            RadComboBoxItem avatarItem2 = new RadComboBoxItem("Avatar 2", "2");
            avatarItem2.ImageUrl = "/images/avatars/avatar2sm.gif";
            avatarComboBox.Items.Add(avatarItem2);
            RadComboBoxItem avatarItem3 = new RadComboBoxItem("Avatar 3", "3");
            avatarItem3.ImageUrl = "/images/avatars/avatar3sm.gif";
            avatarComboBox.Items.Add(avatarItem3);
            RadComboBoxItem avatarItem4 = new RadComboBoxItem("Avatar 4", "4");
            avatarItem4.ImageUrl = "/images/avatars/avatar4sm.gif";
            avatarComboBox.Items.Add(avatarItem4);
            RadComboBoxItem avatarItem5 = new RadComboBoxItem("Avatar 5", "5");
            avatarItem5.ImageUrl = "/images/avatars/avatar5sm.gif";
            avatarComboBox.Items.Add(avatarItem5);
            RadComboBoxItem avatarItem6 = new RadComboBoxItem("Avatar 6", "6");
            avatarItem6.ImageUrl = "/images/avatars/avatar6sm.gif";
            avatarComboBox.Items.Add(avatarItem6);

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(),
                int.Parse(Session["loggedInUserID"].ToString()));
            if (user.avatarNumber > 0)
            {
                avatarComboBox.SelectedValue = user.avatarNumber.ToString();
            }
        }
    }

    //===============================================================
    // Function: saveButton_Click
    //===============================================================
    public void saveButton_Click(object sender, EventArgs e)
    {
        SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(),
            int.Parse(Session["loggedInUserID"].ToString()));

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

            profilePicFileUpload.PostedFile.SaveAs(destPath);

            int status = MiscUtils.CreatePreviews(Path.GetFileName(destPath),
                int.Parse(Session["loggedInUserID"].ToString()));

            if (status >= 0)
            {
                Response.Redirect("profileRedirect.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"This type of image is not supported, please choose another.\");", true);
            }
        }
        else
        {
            user.avatarNumber = int.Parse(avatarComboBox.SelectedValue);
            user.Update();

            Response.Redirect("profileRedirect.aspx");
        }
    }
}

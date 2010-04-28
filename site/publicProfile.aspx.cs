//===============================================================
// Filename: /d/publicProfile.aspx.cs
// Date: 24/04/10
// --------------------------------------------------------------
// Description:
//   
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 24/04/10
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
using Sedogo.BusinessObjects;

public partial class d_publicProfile : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Request.QueryString["UID"].ToString());

            SedogoUser user = new SedogoUser("", userID);

            int loggedInUserID = -1;
            if (Session["loggedInUserID"] != null)
            {
                loggedInUserID = int.Parse(Session["loggedInUserID"].ToString());
            }

            //SedogoUser loggedInUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), loggedInUserID);
            bannerAddFindControl.userID = userID;

            nameLabel.Text = user.fullName;
            pageTitleUserName.Text = user.fullName + " : Sedogo : Create your future and connect with others to make it happen";
            if (user.gender == "M")
            {
                genderLabel.Text = "Male";
            }
            else
            {
                genderLabel.Text = "Female";
            }
            homeTownLabel.Text = user.homeTown;
            profileTextLabel.Text = user.profileText.Replace("\n","<br/>");

            //user.birthday;
            //user.countryID;
            //user.emailAddress;
            //user.firstName;
            //user.lastName;

            if (user.profilePicThumbnail != "")
            {
                profileImage.ImageUrl = "~/assets/profilePics/" + user.profilePicPreview;
            }
            else
            {
                profileImage.ImageUrl = "~/images/profile/blankProfilePreview.jpg";
            }
            profileImage.ToolTip = user.fullName + "'s profile picture";

        }
    }
}

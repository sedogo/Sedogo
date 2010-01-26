//===============================================================
// Filename: userProfile.aspx.cs
// Date: 18/01/10
// --------------------------------------------------------------
// Description:
//   View profile
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 18/01/10
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
using System.Globalization;
using Sedogo.BusinessObjects;

public partial class userProfile : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        int userID = int.Parse(Request.QueryString["UID"].ToString());

        SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);

        firstNameLabel.Text = user.firstName;
        //lastNameLabel.Text = user.lastName;
        homeTownLabel.Text = user.homeTown;
        headlineLabel.Text = user.profileText.Replace("\n","<br/>");

        if (user.profilePicThumbnail != "")
        {
            profileImage.ImageUrl = "~/assets/profilePics/" + user.profilePicPreview;
        }
        else
        {
            profileImage.ImageUrl = "~/images/profile/blankProfilePreview.jpg";
        }
        profileImage.ToolTip = user.fullName + "'s profile picture";

        if( user.birthday > DateTime.MinValue )
        {
            birthdayLabel.Text = user.birthday.ToString("d MMMM yyyy");
        }
        else
        {
            birthdayLabel.Text = "";
        }

        userProfilePopupGoalsLabel.Text = SedogoEvent.GetEventCountNotAchieved(userID).ToString(); ;
        userProfilePopupGoalsAchievedLabel.Text = SedogoEvent.GetEventCountAchieved(userID).ToString();
        userProfilePopupGroupGoalsLabel.Text = TrackedEvent.GetJoinedEventCount(userID).ToString();
        userProfilePopupGoalsFollowedLabel.Text = TrackedEvent.GetTrackedEventCount(userID).ToString();

    }

    //===============================================================
    // Function: sendMessageToUserLink_click
    //===============================================================
    protected void sendMessageToUserLink_click(object sender, EventArgs e)
    {
        int userID = int.Parse(Request.QueryString["UID"].ToString());
        Response.Redirect("sendUserMessage.aspx?EID=-1&UID=" + userID.ToString());
    }
}

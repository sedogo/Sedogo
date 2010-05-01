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

public partial class components_userProfileControl : System.Web.UI.UserControl
{
    public int userID;
    public SedogoUser user;

    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);

        int loggedInUserID = int.Parse(Session["loggedInUserID"].ToString());

        SedogoUser loggedInUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), loggedInUserID);

        firstNameLabel.Text = user.firstName + " " + user.lastName;
        homeTownLabel.Text = user.homeTown;
        headlineLabel.Text = user.profileText.Replace("\n", "<br/>");

        if (user.profilePicThumbnail != "")
        {
            profileImage.ImageUrl = "~/assets/profilePics/" + user.profilePicPreview;
        }
        else
        {
            profileImage.ImageUrl = "~/images/profile/blankProfilePreview.jpg";
        }
        profileImage.ToolTip = user.fullName + "'s profile picture";

        if (user.birthday > DateTime.MinValue)
        {
            birthdayLabel.Text = user.birthday.ToString("d MMMM yyyy");
            int userAgeYears = DateTime.Now.Year - user.birthday.Year;
            DateTime testDate = new DateTime(DateTime.Now.Year, user.birthday.Month, user.birthday.Day);
            if (testDate > DateTime.Now)
            {
                userAgeYears--;
            }
            ageLabel.Text = userAgeYears.ToString();
        }
        else
        {
            birthdayLabel.Text = "";
            ageLabel.Text = "";
        }

        userProfilePopupGoalsLabel.Text = SedogoEvent.GetEventCountNotAchieved(userID).ToString(); ;
        userProfilePopupGoalsAchievedLabel.Text = SedogoEvent.GetEventCountAchieved(userID).ToString();
        userProfilePopupGroupGoalsLabel.Text = TrackedEvent.GetJoinedEventCount(userID).ToString();
        userProfilePopupGoalsFollowedLabel.Text = TrackedEvent.GetTrackedEventCount(userID).ToString();
    }
}

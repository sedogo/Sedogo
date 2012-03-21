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
    public int loggedInUserID;
    public SedogoUser user;

    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        firstNameLabel.Text = user.firstName + " " + user.lastName;
        homeTownLabel.Text = user.homeTown;
        headlineLabel.Text = user.profileText.Replace("\n", "<br/>");
        lastUpdatedDateLabel.Text = user.lastUpdatedDate.ToString("ddd d MMMM yyyy");

        if (user.profilePicThumbnail != "")
        {
            profileImage.ImageUrl = ImageHelper.GetRelativeImagePath(user.userID, user.GUID, ImageType.UserPreview);
        }
        else
        {
            if (user.avatarNumber > 0)
            {
                profileImage.ImageUrl = "~/images/avatars/avatar" + user.avatarNumber.ToString() + ".gif";
            }
            else
            {
                if (user.gender == "M")
                {
                    // 1,2,5
                    int avatarID = 5;
                    switch ((userID % 6))
                    {
                        case 0: case 1: avatarID = 1; break;
                        case 2: case 3: avatarID = 2; break;
                    }
                    profileImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + ".gif";
                }
                else
                {
                    // 3,4,6
                    int avatarID = 6;
                    switch ((userID % 6))
                    {
                        case 0: case 1: avatarID = 3; break;
                        case 2: case 3: avatarID = 4; break;
                    }
                    profileImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + ".gif";
                }
            }
        }
        profileImage.ToolTip = user.fullName + "'s profile picture";

        if (loggedInUserID > 0)
        {
            birthdayRow.Visible = true;
            ageRow.Visible = true;
            loginRow.Visible = false;

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

            usersProfileNameLabel.NavigateUrl = "~/userTimeline.aspx?uid=" + user.userID;
            messageLink.NavigateUrl = "javascript:sendMessage(" + user.userID + ")";

            if( userID == loggedInUserID )
            {
                messageLink.Visible = false;
                editProfileRow.Visible = true;
            }
        }
        else
        {
            ageRow.Visible = false;
            birthdayRow.Visible = false;
            loginRow.Visible = true;

            loginLink.NavigateUrl = "~/login.aspx?UID=" + userID.ToString();
            registerLink.NavigateUrl = "~/register.aspx";

            messageLink.NavigateUrl = "~/login.aspx?UID=" + userID.ToString();
            usersProfileNameLabel.NavigateUrl = "~/login.aspx?UID=" + userID.ToString();
            usersProfileNameLabel.CssClass = "blue";
        }

        userProfilePopupGoalsLabel.Text = SedogoEvent.GetEventCountNotAchieved(userID).ToString(); ;
        userProfilePopupGoalsAchievedLabel.Text = SedogoEvent.GetEventCountAchieved(userID).ToString();
        userProfilePopupGroupGoalsLabel.Text = TrackedEvent.GetJoinedEventCount(userID).ToString();
        userProfilePopupGoalsFollowedLabel.Text = TrackedEvent.GetTrackedEventCount(userID).ToString();

        ShowGoalPics();
    }

    //===============================================================
    // Function: ShowGoalPics
    //===============================================================
    private void ShowGoalPics()
    {
        Boolean showPrivate = false;
        if( userID == loggedInUserID )
        {
            showPrivate = true;
        }

        SedogoNewFun objSNFun = new SedogoNewFun();
        DataTable dtAllUsrs = new DataTable();
        dtAllUsrs = objSNFun.GetProfileGoalPicsDetails(userID, showPrivate);

        if (dtAllUsrs.Rows.Count > 0)
        {
            DataTable dt = dtAllUsrs.Copy();
            dlMember.DataSource = dtAllUsrs;
            dlMember.DataBind();
        }
        else
        {
            dlMember.DataSource = dtAllUsrs;
            dlMember.DataBind();
        }
    }

    protected void OnEventsDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.DataItem != null && e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            int eventID = (int)DataBinder.Eval(e.Item.DataItem, "EventID");

            var eventPic = e.Item.FindControl("eventPic") as HtmlImage;
            if (eventPic != null)
            {
                eventPic.Src = ImageHelper.GetRelativeImagePath(eventID, DataBinder.Eval(e.Item.DataItem, "EventGUID").ToString(), ImageType.EventThumbnail);
            }
            eventPic.Attributes.Add("onmouseover", "ShowHideDiv('" + eventID.ToString() + "')");
        }
    }
}

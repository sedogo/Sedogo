//===============================================================
// Filename: userTimeline.aspx.cs
// Date: 02/11/09
// --------------------------------------------------------------
// Description:
//   Users timelines
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 02/11/09
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
using Sedogo.BusinessObjects;

public partial class userTimeline : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Session["loggedInUserID"].ToString());
            string viewUserIDString = "";
            int viewUserID = -1;
            if (Request.QueryString["UID"] != null)
            {
                viewUserIDString = Request.QueryString["UID"].ToString();
                try
                {
                    viewUserID = int.Parse(viewUserIDString);
                }
                catch
                {
                    Response.Redirect("profile.aspx");
                }
            }
            else
            {
                Response.Redirect("profile.aspx");
            }

            if (userID == viewUserID)
            {
                Response.Redirect("profile.aspx");
            }

            Boolean viewArchivedEvents = false;
            if (Session["ViewArchivedEvents"] != null)
            {
                viewArchivedEvents = (Boolean)Session["ViewArchivedEvents"];
            }

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);

            sidebarControl.userID = userID;
            sidebarControl.user = user;
            bannerAddFindControl.userID = userID;

            SedogoUser viewUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), viewUserID);

            //userTimelineLabel.Text = user.firstName + " " + user.lastName + "'s timeline";
            //sendMessageToUserLink.Text = "Send message to " + viewUser.firstName + " " + viewUser.lastName;
            //sendMessageToUserLink.NavigateUrl = "sendUserMessage.aspx?EID=-1&UID=" + viewUserID.ToString();
            //timelineUserNameLiteral.Text = viewUser.firstName + " " + viewUser.lastName;
            //*By Chetan
            timelineUserNameLiteral.Text = "&nbsp;&nbsp;" + viewUser.firstName + " " + viewUser.lastName + "'s timeline";
            timelineUserNameLiteral.Text += "&nbsp;&nbsp;<a href='javascript:doSendMessage(" + viewUserID.ToString() + ")'><img src='images/messages.gif' title='Send Message' alt='Send Message'/></a>";

            // Populate the profile popup
            usersProfileLinkNameLabel.Text = viewUser.fullName + "'s profile";
            usersProfileNameLabel.NavigateUrl = "~/userTimeline.aspx?UID=" + viewUserID.ToString();
            //usersProfileNameLabel.Text = viewUser.fullName;
            usersProfileNameLabel.Text = "Timeline";
            userViewProfile.NavigateUrl = "~/userprofile.aspx?UID=" + viewUserID.ToString();
            userViewProfile.Text = "Profile";
            lblUName.Text = viewUser.fullName;
            if (viewUser.profileText.Replace("\n", "<br/>").Length > 120)
            {
                usersProfileDescriptionLabel.Text = viewUser.profileText.Replace("\n", "<br/>").Substring(0, 120) + "...";
            }
            else
            {
                usersProfileDescriptionLabel.Text = viewUser.profileText.Replace("\n", "<br/>");
            }
            //birthdayLabel.Text = viewUser.birthday.ToString("d MMMM yyyy");
            //homeTownLabel.Text = viewUser.homeTown;
            userProfilePopupGoalsLabel.Text = SedogoEvent.GetEventCountNotAchieved(viewUserID).ToString(); ;
            userProfilePopupGoalsAchievedLabel.Text = SedogoEvent.GetEventCountAchieved(viewUserID).ToString();
            userProfilePopupGroupGoalsLabel.Text = TrackedEvent.GetJoinedEventCount(viewUserID).ToString();
            userProfilePopupGoalsFollowedLabel.Text = TrackedEvent.GetTrackedEventCount(viewUserID).ToString();
            if (viewUser.profilePicThumbnail != "")
            {
                userProfileThumbnailPic.ImageUrl = ImageHelper.GetRelativeImagePath(viewUser.userID, viewUser.GUID, ImageType.UserThumbnail);
            }
            else
            {
                if (user.avatarNumber > 0)
                {
                    userProfileThumbnailPic.ImageUrl = "~/images/avatars/avatar" + user.avatarNumber.ToString() + "sm.gif";
                }
                else
                {
                    if (viewUser.gender == "M")
                    {
                        // 1,2,5
                        int avatarID = 5;
                        switch ((viewUserID % 6))
                        {
                            case 0: case 1: avatarID = 1; break;
                            case 2: case 3: avatarID = 2; break;
                        }
                        userProfileThumbnailPic.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                    }
                    else
                    {
                        // 3,4,6
                        int avatarID = 6;
                        switch ((viewUserID % 6))
                        {
                            case 0: case 1: avatarID = 3; break;
                            case 2: case 3: avatarID = 4; break;
                        }
                        userProfileThumbnailPic.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                    }
                }
            }
            userProfileThumbnailPic.Attributes.Add("style", "float: right; margin: 0 0 8px 12px");
            userProfilePopupMessageLink.NavigateUrl = "sendUserMessage.aspx?EID=-1&UID=" + viewUserID.ToString();

            sidebarControl.viewArchivedEvents = viewArchivedEvents;
            eventsListControl.userID = userID;
            eventsListControl.user = user;

            timelineURL.Text = "timelineXML.aspx?G=" + Guid.NewGuid().ToString();
            searchTimelineURL.Text = "timelineUserXML.aspx?UID=" + viewUserID.ToString();

            //DateTime timelineStartDate = DateTime.Now.AddMonths(8);
            DateTime timelineStartDate = DateTime.Now.AddYears(4);

            timelineStartDate1.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");     // "Jan 08 2010 00:00:00 GMT"
            timelineStartDate2.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");
            timelineStartDate3.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");     // "Jan 08 2010 00:00:00 GMT"
            timelineStartDate4.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");

            //what.Attributes.Add("onkeypress", "checkAddButtonEnter(event);");

            //searchButton1.Attributes.Add("onmouseover", "this.src='images/addButtonRollover.png'");
            //searchButton1.Attributes.Add("onmouseout", "this.src='images/addButton.png'");
            //searchButton2.Attributes.Add("onmouseover", "this.src='images/searchButtonRollover.png'");
            //searchButton2.Attributes.Add("onmouseout", "this.src='images/searchButton.png'");
        }
    }
}

//===============================================================
// Filename: profile.aspx.cs
// Date: 04/09/09
// --------------------------------------------------------------
// Description:
//   Profile
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 04/09/09
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

public partial class profile : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Session["loggedInUserID"].ToString());

            Boolean viewArchivedEvents = false;
            if (Session["ViewArchivedEvents"] != null)
            {
                viewArchivedEvents = (Boolean)Session["ViewArchivedEvents"];
            }

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
            sidebarControl.userID = userID;
            sidebarControl.user = user;
            bannerAddFindControl.userID = userID;

            eventsListControl.userID = userID;
            eventsListControl.user = user;

            //By Rahul
            timelinebDate.Text = user.birthday.Year.ToString();
            
            timelineURL.Text = "timelineXML.aspx?G=" + Guid.NewGuid().ToString();

            if (Session["EventID"] != null && Session["EventID"].ToString() != "")
            {
                string redirectEventID = Session["EventID"].ToString();
                Session["EventID"] = null;
                Response.Redirect("viewEvent.aspx?EID=" + redirectEventID);
            }

            if (Session["EventInviteGUID"] != null)
            {
                string inviteGUID = Session["EventInviteGUID"].ToString();

                if (inviteGUID != "")
                {
                    //int eventInviteID = EventInvite.GetEventInviteIDFromGUID(inviteGUID);

                    Session["EventInviteGUID"] = "";
                    Session["EventInviteUserID"] = "";
                    Response.Redirect("invite.aspx");
                }
            }

            //DateTime timelineStartDate = DateTime.Now.AddMonths(8);
            DateTime timelineStartDate = DateTime.Now.AddYears(4);

            timelineStartDate1.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");     // "Jan 08 2010 00:00:00 GMT"
            timelineStartDate2.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");

            if (Session["DefaultRedirect"] != null && Session["DefaultRedirect"].ToString() != "")
            {
                string redir = (string)Session["DefaultRedirect"];
                Session["DefaultRedirect"] = "";
                if (redir == "Messages")
                {
                    Response.Redirect("message.aspx");
                }
                if (redir == "Requests")
                {
                    Response.Redirect("eventJoinRequests.aspx");
                }
            }
            if (Session["EventID"] != null && Session["EventID"].ToString() != "")
            {
                Session["EventID"] = "";
                Response.Redirect("viewEvent.aspx?EID=" + Session["EventID"].ToString());
            }

            if (Session["AcheievedEventID"] != null)
            {
                int achievedEventID = int.Parse(Session["AcheievedEventID"].ToString());
                Session["AcheievedEventID"] = null;

                SedogoEvent achievedEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), achievedEventID);
                string message = "Congratulations on completing the goal: " + achievedEvent.eventName + ". Why not create another goal now?";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"" + message + "\");", true);
            }

            if (Session["PageRedirect"] != null)
            {
                string pageRedirect = Session["PageRedirect"].ToString();
                Session["PageRedirect"] = null;

                if (pageRedirect == "AddEvent")
                {
                    string url = "addEvent.aspx";
                    if (Session["PageRedirectDetails"] != null)
                    {
                        url += "?Name=" + (string)Session["PageRedirectDetails"];
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"" + url + "\");", true);
                }
            }

            int eventCountNotAchieved = SedogoEvent.GetEventCountNotAchieved(userID);
            int eventCountAchieved = SedogoEvent.GetEventCountAchieved(userID);
            if( eventCountNotAchieved == 0 && eventCountAchieved == 0 )
            {
                welcomeMessageDiv.Visible = true;
            }
            else
            {
                welcomeMessageDiv.Visible = false;
            }

            if (user.profilePicFilename == "" && user.avatarNumber < 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"setProfileImage.aspx\");", true);
            }

            keepAliveIFrame.Attributes.Add("src", this.ResolveClientUrl("~/keepAlive.aspx"));
        }
    }
}


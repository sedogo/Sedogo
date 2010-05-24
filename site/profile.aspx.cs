﻿//===============================================================
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

            timelinebDate.Text = user.birthday.Year.ToString();

            timelineURL.Text = "timelineXML.aspx?G=" + Guid.NewGuid().ToString();

            if (Session["EventInviteGUID"] != null)
            {
                string inviteGUID = Session["EventInviteGUID"].ToString();

                if (inviteGUID != "")
                {
                    //int eventInviteID = EventInvite.GetEventInviteIDFromGUID(inviteGUID);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"invite.aspx\");", true);
                    Session["EventInviteGUID"] = "";
                    Session["EventInviteUserID"] = "";
                }
            }

            DateTime timelineStartDate = DateTime.Now.AddMonths(8);

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

            keepAliveIFrame.Attributes.Add("src", this.ResolveClientUrl("~/keepAlive.aspx"));
        }
    }
}


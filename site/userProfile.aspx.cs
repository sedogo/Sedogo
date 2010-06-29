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

public partial class userProfile : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        int userID = int.Parse(Request.QueryString["UID"].ToString());
        int loggedInUserID = int.Parse(Session["loggedInUserID"].ToString());

        SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);

        SedogoUser loggedInUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), loggedInUserID);
        sidebarControl.userID = loggedInUserID;
        sidebarControl.user = loggedInUser;

        userProfileControl.loggedInUserID = loggedInUserID;
        userProfileControl.userID = userID;
        userProfileControl.user = user;

        eventsListControl.userID = userID;
        eventsListControl.user = user;

        if( userID == loggedInUserID )
        {
            //sendMessageToUserLink.Visible = false;
        }

        //sendMessageToUserLink.NavigateUrl = "sendUserMessage.aspx?EID=-1&UID=" + userID.ToString();
    }
}

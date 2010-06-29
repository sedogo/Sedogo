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
        //if (!IsPostBack)
        //{
            int userID = int.Parse(Request.QueryString["UID"].ToString());

            SedogoUser user = new SedogoUser("", userID);

            pageTitleUserName.Text = user.firstName + " " + user.lastName + " - Sedogo profile";

            int loggedInUserID = -1;
            if (Session["loggedInUserID"] != null)
            {
                loggedInUserID = int.Parse(Session["loggedInUserID"].ToString());
            }

            bannerAddFindControl.userID = userID;

            userProfileControl.loggedInUserID = loggedInUserID;
            userProfileControl.userID = userID;
            userProfileControl.user = user;
        //}
    }
}

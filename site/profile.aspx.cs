//===============================================================
// Filename: profile.aspx.cs
// Date: 04/09/09
// --------------------------------------------------------------
// Description:
//   Profile page
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
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Sedogo.BusinessObjects;

public partial class profile : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session.Add("loggedInUserID", user.userID);
            //Session.Add("loggedInUserFirstName", user.firstName);
            //Session.Add("loggedInUserLastName", user.lastName);
            //Session.Add("loggedInUserEmailAddress", user.emailAddress);

            SedogoUser user = new SedogoUser("", int.Parse(Session["loggedInUserID"].ToString()));
            userNameLabel.Text = user.fullName;

            profileImage.ImageUrl = "~/images/profile/blankProfile.jpg";
            profileImage.ToolTip = user.fullName + "'s profile picture";
            //<img src="images/profile/megan.jpg" title="Megan L" alt="Megan's profile picture" class="profile" />



        }
    }
}

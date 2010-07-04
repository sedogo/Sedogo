//===============================================================
// Filename: privacy.aspx
// Date: 20/12/09
// --------------------------------------------------------------
// Description:
//   Privacy
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 20/12/09
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
using System.Text;
using Sedogo.BusinessObjects;

public partial class privacy : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        int userID = -1;
        if (Session["loggedInUserID"] != null)
        {
            userID = int.Parse(Session["loggedInUserID"].ToString());

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
            sidebarControl.user = user;
        }
        sidebarControl.userID = userID;
    }
}

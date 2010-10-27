//===============================================================
// Filename: getInspired.aspx.cs
// Date: 11/01/2010
// --------------------------------------------------------------
// Description:
//   Default
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 11/01/2010
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

public partial class getInspired : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            eventRotator.DataSource = GetRotatorDataSource();
            eventRotator.DataBind();

            if (Session["loggedInUserID"] == null)
            {
                getStartedLink.NavigateUrl = "register.aspx";
            }
            else
            {
                getStartedLink.NavigateUrl = "profile.aspx";
            }
        }
    }

    //===============================================================
    // Function: GetRotatorDataSource
    //===============================================================
    private string[] GetRotatorDataSource()
    {
        string[] images = { "go_brag", "go_fast", "go_high", "go_party", 
                               "go_sailing", "go_speechless", "go_swimming", "go_traveling", "go_watch" };
        return images;
    }
}

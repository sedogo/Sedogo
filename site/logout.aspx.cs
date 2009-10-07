//===============================================================
// Filename: logout.aspx.cs
// Date: 19/08/09
// --------------------------------------------------------------
// Description:
//   Logout
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 19/08/09
// Revision history:
//===============================================================

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class logout : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime dtNow = DateTime.Now;

        HttpCookie passwordCookie = new HttpCookie("SedogoLoginPassword");
        
        // Set the cookies value
        passwordCookie.Value = "";

        // Set the cookie to expire in 1 year
        passwordCookie.Expires = dtNow.AddYears(1);

        // Add the cookie
        Response.Cookies.Add(passwordCookie);

        Session.Abandon();
    }
}

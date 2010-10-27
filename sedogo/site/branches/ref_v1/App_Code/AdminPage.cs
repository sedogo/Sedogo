using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AdminPage
/// </summary>
public class AdminPage : System.Web.UI.Page
{
    //===============================================================
    // Constructor: AdminPage
    //===============================================================
    public AdminPage()
    {
    }

    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected override void OnLoad(EventArgs e)
    {
        if (Session["loggedInAdministratorID"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        // Be sure to call the base class's OnLoad method!
        base.OnLoad(e);
    }
}

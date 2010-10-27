using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for SedogoPage
/// </summary>
public class SedogoPage : System.Web.UI.Page
{
    //===============================================================
    // Constructor: SedogoPage
    //===============================================================
    public SedogoPage()
    {
    }

    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected override void OnLoad(EventArgs e)
    {
        if (Session["loggedInUserID"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        // Be sure to call the base class's OnLoad method!
        base.OnLoad(e);
    }
}

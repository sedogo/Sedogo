//===============================================================
// Filename: popularGoals.aspx
// Date: 13/01/10
// --------------------------------------------------------------
// Description:
//   
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 13/01/10
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
using System.Net.Mail;
using System.Text;
using System.IO;
using Sedogo.BusinessObjects;

public partial class admin_popularGoals : AdminPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GlobalData gd = new GlobalData((string)Session["loggedInUserFullName"]);
            popularSearchString1TextBox.Text = gd.GetStringValue("PopularSearchString1");
            popularSearchString2TextBox.Text = gd.GetStringValue("PopularSearchString2");
            popularSearchString3TextBox.Text = gd.GetStringValue("PopularSearchString3");
            popularSearchString4TextBox.Text = gd.GetStringValue("PopularSearchString4");
            popularSearchString5TextBox.Text = gd.GetStringValue("PopularSearchString5");
        }
    }

    //===============================================================
    // Function: saveButton_Click
    //===============================================================
    protected void saveButton_Click(object sender, EventArgs e)
    {
        GlobalData gd = new GlobalData((string)Session["loggedInUserFullName"]);
        gd.UpdateStringValue("PopularSearchString1", popularSearchString1TextBox.Text.Trim());
        gd.UpdateStringValue("PopularSearchString2", popularSearchString2TextBox.Text.Trim());
        gd.UpdateStringValue("PopularSearchString3", popularSearchString3TextBox.Text.Trim());
        gd.UpdateStringValue("PopularSearchString4", popularSearchString4TextBox.Text.Trim());
        gd.UpdateStringValue("PopularSearchString5", popularSearchString5TextBox.Text.Trim());

        Response.Redirect("main.aspx");
    }

    //===============================================================
    // Function: cancelButton_Click
    //===============================================================
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("main.aspx");
    }
}

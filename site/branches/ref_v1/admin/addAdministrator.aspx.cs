//===============================================================
// Filename: addAdministrator.aspx.cs
// Date: 19/08/09
// --------------------------------------------------------------
// Description:
//   Add administrator
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 19/08/09
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

public partial class admin_addAdministrator : AdminPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if( !IsPostBack) 
        {
        }
    }

    //===============================================================
    // Function: saveButton_Click
    //===============================================================
    protected void saveButton_Click(object sender, EventArgs e)
    {
        Administrator adminUser = new Administrator(Session["loggedInAdministratorName"].ToString());
        adminUser.administratorName = nameTextBox.Text;
        adminUser.emailAddress = emailAddress.Text;
        adminUser.Add();

        adminUser.UpdatePassword(userPassword.Text);

        Response.Redirect("editAdministrator.aspx?AID=" + adminUser.administratorID.ToString());
    }

    //===============================================================
    // Function: cancelButton_Click
    //===============================================================
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("administratorsList.aspx");
    }
}

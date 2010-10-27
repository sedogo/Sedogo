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

public partial class admin_editAdministrator : AdminPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int administratorID = int.Parse(Request.QueryString["AID"]);

            Administrator adminUser = new Administrator(Session["loggedInAdministratorName"].ToString(), 
                administratorID);
            nameTextBox.Text = adminUser.administratorName;
            emailAddress.Text = adminUser.emailAddress;
        }
    }

    //===============================================================
    // Function: saveButton_Click
    //===============================================================
    protected void saveButton_Click(object sender, EventArgs e)
    {
        int administratorID = int.Parse(Request.QueryString["AID"]);

        Administrator adminUser = new Administrator(Session["loggedInAdministratorName"].ToString(), 
            administratorID);
        adminUser.administratorName = nameTextBox.Text;
        adminUser.emailAddress = emailAddress.Text;
        adminUser.Update();

        string newPassword = userPassword.Text.Trim();
        if (newPassword != "")
        {
            adminUser.UpdatePassword(userPassword.Text);
        }

        userPassword.Text = "";
    }

    //===============================================================
    // Function: cancelButton_Click
    //===============================================================
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("administratorsList.aspx");
    }
}

//===============================================================
// Filename: addUser.aspx.cs
// Date: 20/08/09
// --------------------------------------------------------------
// Description:
//   Add administrator
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 20/08/09
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

public partial class admin_addUser : AdminPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    //===============================================================
    // Function: saveButton_Click
    //===============================================================
    protected void saveButton_Click(object sender, EventArgs e)
    {
        SedogoUser sedogoUser = new SedogoUser(Session["loggedInAdministratorName"].ToString());
        sedogoUser.firstName = firstNameTextBox.Text;
        sedogoUser.lastName = lastNameTextBox.Text;
        sedogoUser.emailAddress = emailAddress.Text;
        sedogoUser.Add();

        sedogoUser.UpdatePassword(userPassword.Text);

        Response.Redirect("editUser.aspx?UID=" + sedogoUser.userID.ToString());
    }

    //===============================================================
    // Function: cancelButton_Click
    //===============================================================
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("usersList.aspx");
    }
}

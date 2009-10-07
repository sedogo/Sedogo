//===============================================================
// Filename: changePassword.aspx.cs
// Date: 04/09/09
// --------------------------------------------------------------
// Description:
//   Change password
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
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Globalization;
using Sedogo.BusinessObjects;

public partial class changePassword : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetFocus(currentPasswordTextBox);
        }
    }

    //===============================================================
    // Function: saveChangesButton_click
    //===============================================================
    protected void saveChangesButton_click(object sender, EventArgs e)
    {
        string currentPassword = currentPasswordTextBox.Text.Trim();
        string userPassword = passwordTextBox1.Text.Trim();

        SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), 
            int.Parse(Session["loggedInUserID"].ToString()));
        loginResults checkResult;
        checkResult = user.VerifyLogin((string)Session["loggedInUserEmailAddress"], currentPassword, false, true, "changePassword.aspx");
        if ((checkResult == loginResults.loginSuccess) || (checkResult == loginResults.passwordExpired))
        {
            user.UpdatePassword(userPassword);

            Response.Redirect("profileRedirect.aspx");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"The current password is not correct.\");", true);
            SetFocus(currentPasswordTextBox);
        }
    }
}

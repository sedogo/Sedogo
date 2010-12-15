using System;
using Sedogo.BusinessObjects;

public partial class Master : System.Web.UI.MasterPage
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        if (Session["ResetPassword"] != null)
        {
            var resetPassword = (string)Session["ResetPassword"];
            if (resetPassword == "Y")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert",
                                                        "alert(\"Your password has been reset and emailed to you.\");",
                                                        true);
                Session["ResetPassword"] = null;
            }
        }

        Session["EventID"] = "";
        Session["ReplyID"] = "";
        Session["EventInviteGUID"] = "";
        Session["EventInviteUserID"] = -1;
        Session["DefaultRedirect"] = "";
        if (Request.QueryString["EID"] != null)
        {
            Session["EventID"] = Request.QueryString["EID"];
        }
        if (Request.QueryString["ReplyID"] != null)
        {
            Session["ReplyID"] = Request.QueryString["ReplyID"];
        }
        if (Request.QueryString["EIG"] != null)
        {
            Session["EventInviteGUID"] = Request.QueryString["EIG"];
        }
        if (Request.QueryString["UID"] != null)
        {
            Session["EventInviteUserID"] = int.Parse(Request.QueryString["UID"]);
        }
        if (Request.QueryString["Redir"] != null)
        {
            Session["DefaultRedirect"] = Request.QueryString["Redir"];
        }

        var emailCookie = Request.Cookies["SedogoLoginEmailAddress"];
        var passwordCookie = Request.Cookies["SedogoLoginPassword"];
        // Check to make sure the cookie exists
        if (emailCookie != null && passwordCookie != null)
        {
            var loginEmailAddress = emailCookie.Value;
            var loginPassword = passwordCookie.Value;

            var user = new SedogoUser("");
            var checkResult = user.VerifyLogin(loginEmailAddress, loginPassword, false, true, "default.aspx cookie");

            if ((checkResult == loginResults.loginSuccess) || (checkResult == loginResults.passwordExpired))
            {
                Session.Add("loggedInUserID", user.userID);
                Session.Add("loggedInUserFirstName", user.firstName);
                Session.Add("loggedInUserLastName", user.lastName);
                Session.Add("loggedInUserEmailAddress", user.emailAddress);
                Session.Add("loggedInUserFullName", user.firstName + " " + user.lastName);

                if ((checkResult == loginResults.loginSuccess) || (checkResult == loginResults.passwordExpired))
                {
                    const string url = "./profile.aspx";
                    if (Request.Url.LocalPath.EndsWith("default.aspx"))
                    {
                        Response.Redirect(url);
                    }
                }
            }
        }

        if ((string)Session["EventInviteGUID"] != "")
        {
            if ((int)Session["EventInviteUserID"] > 0)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                Response.Redirect("register.aspx");
            }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert",
            //                                        (int)Session["EventInviteUserID"] > 0
            //                                            ? "openModal(\"login.aspx\");"
            //                                            : "openModal(\"register.aspx\");", true);
        }
        if ((string)Session["EventID"] != "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"login.aspx\");", true);
        }
        if ((string)Session["ReplyID"] != "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"login.aspx\");", true);
        }
        if ((string)Session["DefaultRedirect"] != "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"login.aspx\");", true);
        }
    }
}

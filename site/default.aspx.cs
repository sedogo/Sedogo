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

            Session["EventID"] = "";
            Session["ReplyID"] = "";
            Session["EventInviteGUID"] = "";
            Session["EventInviteUserID"] = -1;
            Session["DefaultRedirect"] = "";
            if (Request.QueryString["EID"] != null)
            {
                Session["EventID"] = (string)Request.QueryString["EID"];
            }
            if (Request.QueryString["ReplyID"] != null)
            {
                Session["ReplyID"] = (string)Request.QueryString["ReplyID"];
            }
            if (Request.QueryString["EIG"] != null)
            {
                Session["EventInviteGUID"] = (string)Request.QueryString["EIG"];
            }
            if (Request.QueryString["UID"] != null)
            {
                Session["EventInviteUserID"] = int.Parse(Request.QueryString["UID"].ToString());
            }
            if (Request.QueryString["Redir"] != null)
            {
                Session["DefaultRedirect"] = Request.QueryString["Redir"].ToString();
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

            PopulateEvents();

            timelineURL.Text = "timelineHomePageXML.aspx?G=" + Guid.NewGuid().ToString();

            if ((string)Session["EventInviteGUID"] != "")
            {
                if ((int)Session["EventInviteUserID"] > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"login.aspx\");", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"register.aspx\");", true);
                }
            }
            if ((string)Session["EventID"] != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"login.aspx\");", true);
            }
            if ((string)Session["DefaultRedirect"] != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"login.aspx\");", true);
            }

            DateTime timelineStartDate = DateTime.Now.AddMonths(8);

            timelineStartDate1.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");     // "Jan 08 2010 00:00:00 GMT"
            timelineStartDate2.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");

            what.Attributes.Add("onkeypress", "checkAddButtonEnter(event);");

            searchButton1.Attributes.Add("onmouseover", "this.src='images/addButtonRollover.png'");
            searchButton1.Attributes.Add("onmouseout", "this.src='images/addButton.png'");
            searchButton2.Attributes.Add("onmouseover", "this.src='images/searchButtonRollover.png'");
            searchButton2.Attributes.Add("onmouseout", "this.src='images/searchButton.png'");
        }
    }

    //===============================================================
    // Function: searchButton_click
    //===============================================================
    protected void searchButton_click(object sender, EventArgs e)
    {
        string searchText = what2.Text;

        if (searchText.Trim() == "" || searchText.Trim() == "e.g. climb Everest")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a search term\");", true);
        }
        else
        {
            if (searchText.Length >= 2)
            {
                Response.Redirect("search.aspx?Search=" + searchText.ToString());
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a longer search term\");", true);
            }
        }
    }

    //===============================================================
    // Function: PopulateEvents
    //===============================================================
    protected void PopulateEvents()
    {
    }
}

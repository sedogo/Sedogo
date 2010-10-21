//===============================================================
// Filename: bannerLogin.ascx.cs
// Date: 28/08/09
// --------------------------------------------------------------
// Description:
//   Banner login control
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 28/08/09
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

public partial class components_bannerLogin : System.Web.UI.UserControl
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if( !IsPostBack )
        {
            string script = "$.cookie('facebookLoginReturnUrl', '"+Request.Url.ToString()+"');"+
            "window.location='"+"https://graph.facebook.com/oauth/authorize?"+
                "client_id=" +ConfigurationManager.AppSettings["FacebookAppId"] +
                "&redirect_uri="+HttpUtility.UrlEncode(
                    MiscUtils.GetAbsoluteUrl("~/FacebookAuth.ashx")+"?ReturnUrl=http"
                    //Request.Url.ToString()
                    //HttpUtility.UrlEncode(Request.Url.ToString())
                    )+
                "&scope=email,user_birthday,user_hometown"+"'";
            facebookAuthLink.Attributes["onclick"] = script;
            facebookAuthLink.Attributes["href"] = "javascript:void(0)";

            int loggedInUserID = -1;
            if (Session["loggedInUserID"] != null && Page.Form.ID != "defaultForm")
            {
                loggedInUserID = int.Parse(Session["loggedInUserID"].ToString());
            }

            if (loggedInUserID > 0)
            {
                loginLI.Visible = false;
                signUpLI.Visible = false;
                facebookAuthLi.Visible = false;

                loggedInAsLi.Visible = true;
                logoutLi.Visible = true;

                loggedInAsLabel.Text = "Logged in as " + Session["loggedInUserFirstName"].ToString()
                    + " " + Session["loggedInUserLastName"].ToString();
            }
            else
            {
                loginLI.Visible = true;
                signUpLI.Visible = true;
                facebookAuthLi.Visible = true;

                loggedInAsLi.Visible = false;
                logoutLi.Visible = false;
            }

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            if (sPath == "/default.aspx")
            {
                strapLineText.Attributes.Add("class", "strapline2");
            }
            else
            {
                strapLineText.Attributes.Add("class", "strapline");
            }
        }
    }
}

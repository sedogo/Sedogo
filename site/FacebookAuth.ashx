<%@ WebHandler Language="C#" Class="FacebookAuth" %>

using System;
using System.Web;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json.Linq;
using Sedogo.BusinessObjects;
using System.Web.SessionState;

public class FacebookAuth : IHttpHandler, IRequiresSessionState {
    string client_secret = ConfigurationManager.AppSettings["FacebookAppSecret"];
    string client_id = ConfigurationManager.AppSettings["FacebookAppId"];
        
    public void ProcessRequest (HttpContext context) {
        try
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString["code"]))
            {
                OnUserAuthorizedThisApplication(context);
            }
            //check error_reason. facebook user might have refused to authenticate the application
            else
                context.Response.Redirect("~/default.aspx", false);
        }
        catch (Exception ex)
        {
            Sedogo.BusinessObjects.ErrorLog errorLog = new Sedogo.BusinessObjects.ErrorLog();
            errorLog.WriteLog("FacebookAuth", "ProcessRequest", ex.Message, 
                Sedogo.BusinessObjects.logMessageLevel.errorMessage);

            context.Response.Redirect("~/default.aspx", false);
        }
    }


    private void OnUserAuthorizedThisApplication(HttpContext context)
    {
        string code = context.Request.QueryString["code"];

        string reqUrl = "https://graph.facebook.com/oauth/access_token?" +
            "client_id=" + client_id +
            "&redirect_uri=" + HttpUtility.UrlEncode(
                MiscUtils.GetAbsoluteUrl("~/FacebookAuth.ashx") + "?ReturnUrl=http" 
                //context.Request.QueryString["ReturnUrl"]
                //HttpUtility.UrlEncode(context.Request.QueryString["ReturnUrl"])
                ) +
            "&client_secret=" + client_secret +
            "&code=" + HttpUtility.UrlEncode(code);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(reqUrl);
        
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Encoding enc = Encoding.UTF8;
        try
        {
            enc = Encoding.GetEncoding(response.CharacterSet);
        }
        catch (Exception ex)
        {
            
        }
        string body = (new StreamReader(response.GetResponseStream(),enc)).ReadToEnd();
        string access_token = string.Empty;
        Match m = Regex.Match(body, "access_token=(?<access_token>(.*?))(&|$)");
        if(m.Success)
        {
            access_token = m.Groups["access_token"].Value;
            GetUsersDetails(access_token,context);
        }
        
        
        
    }

    
    private void GetUsersDetails(string access_token, HttpContext context)
    {
        JObject fbuser = SedogoUser.GetFacebookUserDetails(access_token);
        if (fbuser == null)
            throw new NullReferenceException("No user found at facebook");
        long id = long.Parse((string)fbuser["id"]);
        context.Session.Add("facebookUserID", id);
        SedogoUser suser = new SedogoUser("");
        if (suser.ReadUserDetailsByFacebookUserID(id))
        {
            //there is a user with this facebook id
            var checkResult = suser.VerifyLogin(suser.emailAddress, suser.userPassword, true, true, "FacebookAuth.ashx");
            if ((checkResult == loginResults.loginSuccess) || (checkResult == loginResults.passwordExpired))
            {
                context.Session.Add("loggedInUserID", suser.userID);
                context.Session.Add("loggedInUserFirstName", suser.firstName);
                context.Session.Add("loggedInUserLastName", suser.lastName);
                context.Session.Add("loggedInUserEmailAddress", suser.emailAddress);
                context.Session.Add("loggedInUserFullName", suser.firstName + " " + suser.lastName);
                string returnUrl = "~/default.aspx";
                if(context.Request.Cookies["facebookLoginReturnUrl"]!=null)
                    returnUrl = HttpUtility.UrlDecode(context.Request.Cookies["facebookLoginReturnUrl"].Value);

                context.Response.Redirect(returnUrl, false);
            }
            else if (checkResult == loginResults.loginFailed)
            {
                context.Response.Write("<p>Username or password is not correct.</p>");
                context.Response.Write("<a href=\"" + MiscUtils.GetAbsoluteUrl("~/default.aspx") + "\">Sedogo</a>");
            }
            else if (checkResult == loginResults.loginNotActivated)
            {
                context.Response.Write("<p>This account has not yet been activated, please check your email.</p>");
                context.Response.Write("<a href=\"" + MiscUtils.GetAbsoluteUrl("~/default.aspx") + "\">Sedogo</a>");
            }
        }
        else
        {
            context.Session.Add("facebookUserAccessToken", access_token);
            context.Response.Redirect("~/register.aspx?from=facebook", false);    
        }
        
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
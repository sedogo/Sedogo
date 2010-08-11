<%@ WebHandler Language="C#" Class="FacebookAuth" %>

using System;
using System.Web;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

public class FacebookAuth : IHttpHandler {
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
        }
    }


    private void OnUserAuthorizedThisApplication(HttpContext context)
    {
        string code = context.Request.QueryString["code"];
        
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://graph.facebook.com/oauth/access_token?"+
            "client_id="+client_id+
            "&redirect_uri="+Sedogo.BusinessObjects.MiscUtils.GetAbsoluteUrl("~/FacebookAuth.ashx")+
            "&client_secret="+client_secret+
            "&code="+HttpUtility.UrlEncode(code));

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
            GetUsersDetails(access_token);
        }
        
        
        
    }

    private void GetUsersDetails(string access_token)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://graph.facebook.com/me?access_token=" + HttpUtility.UrlEncode(access_token));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Encoding enc = Encoding.UTF8;
        try
        {
            enc = Encoding.GetEncoding(response.CharacterSet);
        }
        catch (Exception ex)
        {

        }
        string body = (new StreamReader(response.GetResponseStream(), enc)).ReadToEnd();
        
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
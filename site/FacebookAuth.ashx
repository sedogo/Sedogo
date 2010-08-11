<%@ WebHandler Language="C#" Class="FacebookAuth" %>

using System;
using System.Web;
using System.Configuration;
using System.Net;

public class FacebookAuth : IHttpHandler {
    string client_secret = ConfigurationManager.AppSettings["FacebookAppSecret"];
    string client_id = ConfigurationManager.AppSettings["FacebookAppId"];
        
    public void ProcessRequest (HttpContext context) {
        if (!string.IsNullOrEmpty(context.Request.QueryString["code"]))
        {
            OnUserAuthorizedThisApplication(context);
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
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
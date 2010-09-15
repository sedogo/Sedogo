<%@ WebHandler Language="C#" Class="FacebookAuth" %>

using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Web;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using Sedogo.BusinessObjects;
using System.Web.SessionState;

public class FacebookAuth : IHttpHandler, IRequiresSessionState {
    readonly string _clientSecret = ConfigurationManager.AppSettings["FacebookAppSecret"];
    readonly string _clientID = ConfigurationManager.AppSettings["FacebookAppId"];
        
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

        string reqUrl =
            string.Format(
                "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}?ReturnUrl=http&client_secret={2}&code={3}",
                _clientID, HttpUtility.UrlEncode(MiscUtils.GetAbsoluteUrl("~/FacebookAuth.ashx")), _clientSecret,
                HttpUtility.UrlEncode(code));

        var request = (HttpWebRequest)WebRequest.Create(reqUrl);
        
        var response = (HttpWebResponse)request.GetResponse();
        var enc = Encoding.UTF8;
        try
        {
            enc = Encoding.GetEncoding(response.CharacterSet);
        }
        catch (Exception ex)
        {
            new ErrorLog().WriteLog("site", "FacebookAuth", ex.Message, logMessageLevel.errorMessage);
        }
        var responseBody = (new StreamReader(response.GetResponseStream(),enc)).ReadToEnd();
        var match = Regex.Match(responseBody, "access_token=(?<access_token>(.*?))(&|$)");
        if (!match.Success)
        {
            return;
        }
        var accessToken = match.Groups["access_token"].Value;
        GetUsersDetails(accessToken,context);
    }

    
    private static void GetUsersDetails(string accessToken, HttpContext context)
    {
        var fbuser = SedogoUser.GetFacebookUserDetails(accessToken);
        if (fbuser == null)
        {
            throw new NullReferenceException("No user found at facebook");
        }
        var id = long.Parse((string)fbuser["id"]);
        context.Session.Add("facebookUserID", id);
        var suser = new SedogoUser("");
        if (suser.ReadUserDetailsByFacebookUserID(id))
        {
            //there is a user with this facebook id
            var checkResult = suser.VerifyLogin(suser.emailAddress, suser.userPassword, true, true, "FacebookAuth.ashx");
            switch (checkResult)
            {
                case loginResults.passwordExpired:
                case loginResults.loginSuccess:
                    {
                        RedirectToHomePage(context, suser);
                    }
                    break;
                case loginResults.loginFailed:
                    context.Response.Write("<p>Username or password is not correct.</p>");
                    context.Response.Write("<a href=\"" + MiscUtils.GetAbsoluteUrl("~/default.aspx") + "\">Sedogo</a>");
                    break;
                case loginResults.loginNotActivated:
                    context.Response.Write("<p>This account has not yet been activated, please check your email.</p>");
                    context.Response.Write("<a href=\"" + MiscUtils.GetAbsoluteUrl("~/default.aspx") + "\">Sedogo</a>");
                    break;
            }
        }
        else
        {
            //Note: Old version. Made by Nikita. Redicrects to register page.
            context.Session.Add("facebookUserAccessToken", accessToken);
            context.Response.Redirect("~/register.aspx?from=facebook", false);    
            
            //Note: New version. Register user right here and redirect to the home page.
            //RegisterNewUser(accessToken, context, id);
        }
        
    }

    private static void RegisterNewUser(string accessToken, HttpContext context, long id)
    {
        string password;
        var user = CreateUser(accessToken, context, id, out password);
        SendRegisterEmail(user.emailAddress, string.Format("{0} {1}", user.firstName, user.lastName), password);
        RedirectToHomePage(context, user);
    }

    private static SedogoUser CreateUser(string accessToken, HttpContext context, long id, out string password)
    {
        var facebookUser = SedogoUser.GetFacebookUserDetails(accessToken);
        var user = new SedogoUser(string.Empty);
        if (facebookUser["first_name"] != null)
        {
            user.firstName = (string)facebookUser["first_name"];
        }
        if (facebookUser["last_name"] != null)
        {
            user.lastName = (string)facebookUser["last_name"];
        }
        if (facebookUser["email"] != null)
        {
            user.emailAddress = (string)facebookUser["email"];
        }
        if (facebookUser["birthday"] != null)
        {
            try
            {
                var birthday = DateTime.ParseExact((string)facebookUser["birthday"], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                user.birthday = birthday;
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteLog("site", "FacebookAuth - Register", ex.Message, logMessageLevel.errorMessage);
                user.birthday = DateTime.MinValue;
            }
        }
        else
        {
            user.birthday = DateTime.MinValue;
        }
        user.homeTown = facebookUser["hometown"] != null && facebookUser["hometown"]["name"] != null
                            ? (string) facebookUser["hometown"]["name"]
                            : string.Empty;
        user.gender = facebookUser["gender"] != null ? ((string) facebookUser["gender"] == "male" ? "M" : "F") : "M";
        if (facebookUser["timezone"] != null)
        {
            user.timezoneID = GetTimezoneId(context, Convert.ToInt32(facebookUser["timezone"]), user.fullName);
        }
        user.facebookUserID = id;
        user.Add();

        password = StringHelper.GenerateRandomPassword();
        user.UpdatePassword(password);
        return user;
    }

    private static void RedirectToHomePage(HttpContext context, SedogoUser suser)
    {
        context.Session.Add("loggedInUserID", suser.userID);
        context.Session.Add("loggedInUserFirstName", suser.firstName);
        context.Session.Add("loggedInUserLastName", suser.lastName);
        context.Session.Add("loggedInUserEmailAddress", suser.emailAddress);
        context.Session.Add("loggedInUserFullName", suser.firstName + " " + suser.lastName);
        var returnUrl = "~/profileRedirect.aspx";
        if(context.Request.Cookies["facebookLoginReturnUrl"]!=null)
        {
// ReSharper disable PossibleNullReferenceException
            returnUrl = HttpUtility.UrlDecode(context.Request.Cookies["facebookLoginReturnUrl"].Value);
// ReSharper restore PossibleNullReferenceException
        }

        context.Response.Redirect(returnUrl, false);
    }

    private static int GetTimezoneId(HttpContext context, int gmtOffset, string fullName)
    {
        var timezoneList = new Dictionary<int, int>();
        using (var conn = new SqlConnection((string) context.Application["connectionString"]))
        {
            conn.Open();
                    
            var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectTimezoneList";
            var dataReader = cmd.ExecuteReader();
            if (dataReader != null && dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var timezoneId = dataReader.IsDBNull(dataReader.GetOrdinal("TimezoneID")) ? 1 : Convert.ToInt32(dataReader["TimezoneID"]);
                    var offset = dataReader.IsDBNull(dataReader.GetOrdinal("GMTOffset")) ? 0 : Convert.ToInt32(dataReader["GMTOffset"]);
                    timezoneList.Add(timezoneId, offset);
                }
            }
        }
        if (timezoneList.Values.Contains(gmtOffset))
        {
            return timezoneList.ToList().Find(x => x.Value == gmtOffset).Key;
        }
        var timezone = GetTimezone(gmtOffset, fullName);
        return timezone.timezoneID;
    }

    private static SedogoTimezone GetTimezone(int gmtOffset, string fullName)
    {
        var timezone = new SedogoTimezone(fullName)
                           {
                               GMTOffset = gmtOffset,
                               description = TimeZoneInfo.GetSystemTimeZones().Where(x => x.BaseUtcOffset.TotalHours == gmtOffset).First().Id,
                               shortCode = "UTC" + (gmtOffset < 0 ? "-" + gmtOffset : (gmtOffset > 0 ? "+" + gmtOffset : string.Empty))
                           };
        timezone.Add();
        return timezone;
    }

    private static void SendRegisterEmail(string emailAddress, string fullName, string password)
    {
        var globalData = new GlobalData(fullName);

        var emailBodyCopy = new StringBuilder();

        emailBodyCopy.AppendLine("<html>");
        emailBodyCopy.AppendLine("<head><title></title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">");
        emailBodyCopy.AppendLine("<style type=\"text/css\">");
        emailBodyCopy.AppendLine("	body, td, p { font-size: 15px; color: #9B9885; font-family: Arial, Helvetica, Sans-Serif }");
        emailBodyCopy.AppendLine("	p { margin: 0 }");
        emailBodyCopy.AppendLine("	h1 { color: #00ccff; font-size: 18px; font-weight: bold; }");
        emailBodyCopy.AppendLine("	a, .blue { color: #00ccff; text-decoration: none; }");
        emailBodyCopy.AppendLine("	img { border: 0; }");
        emailBodyCopy.AppendLine("</style></head>");
        emailBodyCopy.AppendLine("<body bgcolor=\"#f0f1ec\">");
        emailBodyCopy.AppendLine("  <table width=\"692\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
        emailBodyCopy.AppendLine("	<tr><td style=\"background: #fff\" width=\"30\"></td>");
        emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"632\">");
        emailBodyCopy.AppendLine("			<h1>Thanks for registering with Sedogo</h1>");
        emailBodyCopy.AppendLine("			<p>Your password: " + password + "</p>");
        emailBodyCopy.AppendLine("			<br /><br />");
        emailBodyCopy.AppendLine("			<p>Regards</p><a href=\"http://www.sedogo.com\" class=\"blue\"><strong>The Sedogo Team.</strong></a><br />");
        emailBodyCopy.AppendLine("			<br /></td>");
        emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"30\"></td></tr></table></body></html>");

        string smtpServer = globalData.GetStringValue("SMTPServer");
        string mailFromAddress = globalData.GetStringValue("MailFromAddress");
        string mailFromPassword = globalData.GetStringValue("MailFromPassword");

        string mailToEmailAddress = emailAddress;

        var message = new MailMessage(mailFromAddress, mailToEmailAddress)
                          {
                              ReplyTo = new MailAddress("noreply@sedogo.com"),
                              Subject = "Sedogo registration",
                              Body = emailBodyCopy.ToString(),
                              IsBodyHtml = true
                          };

        var smtp = new SmtpClient {Host = smtpServer};
        if (mailFromPassword != string.Empty)
        {
            smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
        }

        try
        {
            smtp.Send(message);

            var emailHistory = new SentEmailHistory("")
                                   {
                                       subject = "Sedogo registration",
                                       body = emailBodyCopy.ToString(),
                                       sentFrom = mailFromAddress,
                                       sentTo = emailAddress
                                   };
            emailHistory.Add();
        }
        catch (Exception ex)
        {
            var emailHistory = new SentEmailHistory("")
                                   {
                                       subject = "Sedogo registration",
                                       body = ex.Message + " -------- " + emailBodyCopy,
                                       sentFrom = mailFromAddress,
                                       sentTo = emailAddress
                                   };
            emailHistory.Add();
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
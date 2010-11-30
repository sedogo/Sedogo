using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Net;
using Microsoft.Web.Mvc;
using System.Configuration;
using Sedogo.BusinessObjects;

namespace RestAPI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "API", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute("UsersConsumption", "{controller}/users/{id}/consumption", new { controller = "API", action="UsersConsumption", id = UrlParameter.Optional });
            routes.MapRoute("UsersEvents", "{controller}/users/{id}/events", new { controller = "API", action = "UsersEvents", id = UrlParameter.Optional });
            routes.MapRoute("UsersAchieved", "{controller}/users/{id}/achieved", new { controller = "API", action = "UsersAchieved", id = UrlParameter.Optional });
            routes.MapRoute("UsersFollowed", "{controller}/users/{id}/followed", new { controller = "API", action = "UsersFollowed", id = UrlParameter.Optional });
            routes.MapRoute("EventsComments", "{controller}/events/{id}/comments", new { controller = "API", action="EventsComments", id = UrlParameter.Optional });

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            //this line is used for Microsoft.Web.Mvc
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
            //copying Global.asax from Sedogo
            var environment = ConfigurationManager.AppSettings["EnvironmentName"];
            var connectionString = ConfigurationManager.ConnectionStrings[environment].ToString();
            var errorLogFile = ConfigurationManager.AppSettings["ErrorLogFile"];
            var errorLogLevel = ConfigurationManager.AppSettings["ErrorLogLevel"];

            Application.Add("connectionString", connectionString);
            Application.Add("ErrorLogFile", errorLogFile);
            Application.Add("ErrorLogLevel", errorLogLevel);

            GlobalSettings.connectionString = (string)Application["connectionString"];
            GlobalSettings.errorLogFile = (string)Application["ErrorLogFile"];
            GlobalSettings.errorLogLevel = (logMessageLevel)int.Parse(Application["ErrorLogLevel"].ToString());
            
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();
            Response.ContentType = Assistant.JsonMimeType;
            if (exception is HttpException)
            {

                HttpException httpException = exception as HttpException;

                if (httpException != null)
                {
                    int codeInt = httpException.GetHttpCode();
                    Response.StatusCode = codeInt;
                    HttpStatusCode code = (HttpStatusCode)codeInt;
                    switch (code)
                    {
                        case HttpStatusCode.Unauthorized:
                            Response.Write("{error\":\"unauthorized\"}");
                            break;

                        case HttpStatusCode.NotFound:
                            // page not found
                            Response.Write("{\"error\":\"not-found\"}");
                            break;
                        case HttpStatusCode.InternalServerError:
                            // server error
                            Response.Write("{\"error\":\"server-error\"}");
                            break;
                        default:
                            Response.Write("{\"error\":\"server-error\"}");
                            break;
                    }
                }
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Response.Write("{\"error\":\"some-error\"}");
            }
            // clear error on server
            Server.ClearError();
            Assistant.WriteLog(exception.Message + ". Stack=" + exception.StackTrace);
            // Response.Redirect(String.Format("~/Error/{0}/?message={1}", action, exception.Message));

        }

        /*protected void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (Response.StatusCode != 200)
            {
                Assistant.WriteLog("endrequest. status = " + Response.StatusCode.ToString());
                // http://www.west-wind.com/weblog/posts/745738.aspx
                Response.TrySkipIisCustomErrors = true;
            }
        }*/

         protected void Application_BeginRequest(object sender, EventArgs e)
         {
             HttpContext context = HttpContext.Current;
             //if (Request.HttpMethod == "POST" && string.IsNullOrEmpty(Request.ContentType))
             //    Request.ContentType = "application/json; charset=utf-8"; 
                
            
         } 
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;    
using System.Configuration;
using System.IO;
using Sedogo.BusinessObjects;

namespace RestAPI
{
    public enum UserRole { Admin, User, Any };
    
    public class Assistant  
    {
        /// <summary>
        /// Converts datetime into this format: %Y-%m-%dT%H:%M:%SZ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ConvertDateTime(DateTime dt)
        {
            return XmlConvert.ToString(dt, XmlDateTimeSerializationMode.Local);
        }

        /// <summary>
        /// Converts a string in this format %Y-%m-%dT%H:%M:%SZ into datetime
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime? ConvertDateTime(string dt)
        {
            return XmlConvert.ToDateTime(dt);
        }


        public static string MimeType
        {
            get { return "application/json"; }
        }


        
        /// <summary>
        /// Check user's or admin's authentication
        /// </summary>
        /// <param name="Request">HTTP request with Basic Authentication header</param>
        /// <param name="db">database access object</param>
        /// <param name="role">user role</param>
        /// <param name="email">email acts like a login</param>
        /// <param name="id">output user's identifier</param>
        /// <param name="fullName">output user's name</param>
        /// <returns>true if authentication is successful</returns>
        public static bool TryAuthenticate(HttpRequestBase Request, SedogoDBEntities db, UserRole role, out string email,out int? id, out string fullName)
        {
            email = null;
            string password = null;
            id = null;
            fullName = null;
            string authHeader = Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                string basic =  "basic ";
                if (authHeader.StartsWith(basic, StringComparison.InvariantCultureIgnoreCase))
                {
                    Encoding e;
                    if (Request.ContentEncoding != null)
                        e = Request.ContentEncoding;
                    else
                        e = Encoding.UTF8;
                    string userNameAndPassword = e.GetString(
                        Convert.FromBase64String(authHeader.Substring(basic.Length)));

                    string[] parts = userNameAndPassword.Split(':');
                    email = parts[0];
                    password = parts[1];
                    switch (role)
                    {
                        case UserRole.Admin:
                            return VerifyAdminLogin(email, password, db, out id, out fullName);
                            break;
                        case UserRole.User:
                            return VerifyUserLogin(email, password, db, out id, out fullName);
                            break;
                        case UserRole.Any:
                            bool v1 = VerifyUserLogin(email, password, db, out id, out fullName);
                            if (!v1)
                                return VerifyAdminLogin(email, password, db, out id, out fullName);
                            else
                                return v1;
                            break;
                        default:
                            break;
                    }
                                             
                }
            }

            return false;

        }


        /// <summary>
        /// Check the admin's password
        /// </summary>
        /// <param name="emailAddress">email is the login</param>
        /// <param name="password">password</param>
        /// <param name="db">database access object</param>
        /// <param name="adminID">output - admin id</param>    
        /// <param name="fullName">output - admin's name</param>
        /// <returns>authentication is successful</returns>
        public static bool VerifyAdminLogin(string emailAddress, string password, SedogoDBEntities db, out int? adminID,
            out string fullName)
        {
            adminID = null;
            fullName = null;
            Administrator admin = new Administrator("");
            loginResults lr = admin.VerifyLogin(emailAddress, password, false, true, "API. VerifyAdminLogin");
            if (lr == loginResults.loginSuccess)
            {
                adminID = admin.administratorID;
                fullName = admin.administratorName;
                return true;
            }
            return false;

            /*System.Data.Objects.ObjectResult<spVerifyAdministratorLogin_Result> lresult = db.spVerifyAdministratorLogin(emailAddress);
            spVerifyAdministratorLogin_Result loginResult = lresult.FirstOrDefault();
            if (loginResult != null && loginResult.AdministratorPassword == password)
            {
                adminID = loginResult.AdministratorID;
                return true;
            }
            return false;*/
        }

        
        /// <summary>
        /// Check the user's password
        /// </summary>
        /// <param name="emailAddress">email is the login</param>
        /// <param name="password">password</param>
        /// <param name="db">database access object</param>
        /// <param name="userID">output - user id</param>
        /// <param name="fullName">output - user's name</param>
        /// <returns>authentication is successful</returns>
        public static bool VerifyUserLogin(string emailAddress, string password, SedogoDBEntities db, out int? userID, out string fullName)
        {
            userID = null;
            fullName = null;
            SedogoUser user = new SedogoUser("");
            loginResults checkResult;
            checkResult = user.VerifyLogin(emailAddress, password, false, true, "API. VerifyUserLogin");
            if ((checkResult == loginResults.loginSuccess))
            {
                userID = user.userID;
                fullName = user.firstName + " " + user.lastName;
                return true;
            }
            return false;
            /*System.Data.Objects.ObjectResult<spVerifyUserLogin_Result> lresult = db.spVerifyUserLogin(emailAddress);
            spVerifyUserLogin_Result loginResult = lresult.FirstOrDefault();
            if (loginResult != null && loginResult.UserPassword == password)
            {
                userID = loginResult.UserID;
                return true;
            }
            return false;
              */
        }

        /// <summary>
        /// An object that is converted into JSON format. It is returned if no item is found
        /// </summary>
        public static Object ErrorNotFound
        {
            get { return new { error = "not-found" }; }
        }

        /// <summary>
        /// An object that is converted into JSON format. It is returned if the user is unauthorized
        /// </summary>
        public static Object ErrorUnauthorized
        {
            get { return new { error = "unauthorized" }; }
        }


        /// <summary>
        /// An object that is converted into JSON format. It is returned if the user cannot access a resource
        /// </summary>
        public static Object ErrorForbidden
        {
            get { return new { error="forbidden"}; }
        }

        public class WriteLongException
        {
            public string Ex { get; set; }
            public WriteLongException() { }
        }

        /// <summary>
        /// A function similair to the one from original site
        /// </summary>
        /// <param name="logText"></param>
        public static void WriteLog(string logText)
        {
            try
            {
                DateTime now = DateTime.Now;
                string strDate = now.Year + "/" + now.Month + "/" + now.Day + " ";
                if (now.Minute > 9)
                {
                    strDate += now.Hour + ":" + now.Minute;
                }
                else
                {
                    strDate += now.Hour + ":0" + now.Minute;
                }

                string logContents = strDate + " - " + logText;
                // All error messages are written to the event log
                //System.Diagnostics.EventLog appLog = new System.Diagnostics.EventLog();
                //appLog.Source = "Sedogo";
                //appLog.WriteEntry(logContents);
                StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["ErrorLogFile"], true);
                sw.WriteLine(logContents);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
            }
        }

    }
}
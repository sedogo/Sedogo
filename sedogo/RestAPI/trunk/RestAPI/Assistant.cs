using System;
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
            return XmlConvert.ToDateTime(dt, XmlDateTimeSerializationMode.Local);
        }


        /// <summary>
        /// Gets the type of the json MIME.
        /// </summary>
        /// <value>The type of the json MIME.</value>
        public static string JsonMimeType
        {
            get { return "application/json"; }
        }


        
        /// <summary>
        /// Check user's or admin's authentication
        /// </summary>
        /// <param name="request">HTTP request with Basic Authentication header</param>
        /// <param name="db">database access object</param>
        /// <param name="role">user role</param>
        /// <param name="email">email acts like a login</param>
        /// <param name="id">output user's identifier</param>
        /// <param name="fullName">output user's name</param>
        /// <returns>true if authentication is successful</returns>
        public static bool TryAuthenticate(HttpRequestBase request, SedogoDBEntities db, UserRole role, out string email,out int? id, out string fullName)
        {
            email = null;
            id = null;
            fullName = null;
            var authHeader = request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                const string basic = "basic ";
                if (authHeader.StartsWith(basic, StringComparison.InvariantCultureIgnoreCase))
                {
                    var e = request.ContentEncoding ?? Encoding.UTF8;
                    var userNameAndPassword = e.GetString(
                        Convert.FromBase64String(authHeader.Substring(basic.Length)));

                    var parts = userNameAndPassword.Split(':');
                    email = parts[0];
                    var password = parts[1];
                    switch (role)
                    {
                        case UserRole.Admin:
                            return VerifyAdminLogin(email, password, db, out id, out fullName);
                        case UserRole.User:
                            return VerifyUserLogin(email, password, db, out id, out fullName);
                        case UserRole.Any:
                            return VerifyUserLogin(email, password, db, out id, out fullName) ||
                                   VerifyAdminLogin(email, password, db, out id, out fullName);
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
        /// <param name="adminId">output - admin id</param>    
        /// <param name="fullName">output - admin's name</param>
        /// <returns>authentication is successful</returns>
        public static bool VerifyAdminLogin(string emailAddress, string password, SedogoDBEntities db, out int? adminId,
            out string fullName)
        {
            adminId = null;
            fullName = null;
            var admin = new Administrator("");
            var lr = admin.VerifyLogin(emailAddress, password, false, true, "API. VerifyAdminLogin");
            if (lr == loginResults.loginSuccess)
            {
                adminId = admin.administratorID;
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
        /// <param name="userId">output - user id</param>
        /// <param name="fullName">output - user's name</param>
        /// <returns>authentication is successful</returns>
        public static bool VerifyUserLogin(string emailAddress, string password, SedogoDBEntities db, out int? userId, out string fullName)
        {
            userId = null;
            fullName = null;
            var user = new SedogoUser("");
            var checkResult = user.VerifyLogin(emailAddress, password, false, true, "API. VerifyUserLogin");
            if ((checkResult == loginResults.loginSuccess))
            {
                userId = user.userID;
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
        }

        /// <summary>
        /// A function similair to the one from original site
        /// </summary>
        /// <param name="logText"></param>
        public static void WriteLog(string logText)
        {
            try
            {
                var now = DateTime.Now;
                var strDate = now.Year + "/" + now.Month + "/" + now.Day + " ";
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
                var sw = new StreamWriter(ConfigurationManager.AppSettings["ErrorLogFile"], true);
                sw.WriteLine(logContents);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Objects;
using System.Text;
using System.Xml;
using System.Net;
using RestAPI.Models;
using System.Data.EntityClient;

namespace RestAPI.Controllers
{
    public class APIController : Controller
    {
        protected SedogoDBEntities db = new SedogoDBEntities();

        string email;
        int? currentUserID;
        private bool CheckAuthentication(UserRole role)
        {
            return Assistant.TryAuthenticate(Request, db, role, out email, out currentUserID);
        }

        #region Item Methods

        /// <summary>
        /// GET /users/{id}.
        /// retrieves the profile of an individual User. "id" is User.id or User.email. authentication is required, but any authenticated user can access any profile in the system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Users(string id)
        {
            #region initial steps
            Response.ContentType = Assistant.MimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized,JsonRequestBehavior.AllowGet);
            }
            #endregion
            int userId = 0;

            if (string.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error="user-id-required"}, JsonRequestBehavior.AllowGet);
            }

            Sedogo.BusinessObjects.SedogoUser user = null;  
            if (!int.TryParse(id, out userId))
            {
                //id is the user's email
                string emailAddress = id;
                userId = Sedogo.BusinessObjects.SedogoUser.GetUserIDFromEmailAddress(emailAddress);
                if (userId > 0)
                {
                    user = new Sedogo.BusinessObjects.SedogoUser("", userId);
                }
               
            }
            else
            {
                //id is the user's id
                user = new Sedogo.BusinessObjects.SedogoUser("",userId);
            }
            
            //check if any object contains user details
            if(user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
            }
            else
            {
                SedogoUser suser = new SedogoUser { 
                    id = userId,
                    created= user.createdDate,
                    updated = user.lastUpdatedDate,
                    firstName = user.firstName,
                    lastName = user.lastName,
                    gender=user.gender,
                    image = user.profilePicFilename,
                    imageThumbnail = user.profilePicThumbnail,
                    imagePreview = user.profilePicPreview,
                    profile = user.profileText,
                    homeTown = user.homeTown,
                    birthday = user.birthday,
                    country = user.countryID,
                    timezone = user.timezoneID,
                    language = user.languageID
                };

                return Json(suser.GetDetails(), JsonRequestBehavior.AllowGet);

               
                
            }
        }


        /// <summary>
        /// POST /users. Registers a new user. The request body is the JSON representation of the User resource
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Users(string email, string password, string firstName, string lastName,
            string gender, string homeTown, string birthday, int? country, int? language, int? timezone, string profile, 
            string image, string imageThumbnail, string imagePreview)
        {
            #region initial steps
            Response.ContentType = Assistant.MimeType;
            if (!CheckAuthentication(UserRole.Admin))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized);
            }
            #endregion
            
            spSelectAdministratorDetails_Result adminDetails = db.spSelectAdministratorDetails(currentUserID).FirstOrDefault();
            string error;
             //let's try to create a user object
            SedogoUser modelUser = SedogoUser.CreateUser(email, password, firstName, lastName, gender, homeTown, birthday, country, language, timezone, profile, image,
            imageThumbnail, imagePreview, adminDetails.AdministratorName, adminDetails.AdministratorName, out error);

            if (!string.IsNullOrEmpty(error))
            {
                //some error occurred while attempting to create the user object
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = error });
            }
            //the object was created successfully, now we can add it to the database

            //check the unique email
            int testUserID = Sedogo.BusinessObjects.SedogoUser.GetUserIDFromEmailAddress(modelUser.email);

            if (testUserID > 0)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new{error="email-not-unique"});
            }
            Sedogo.BusinessObjects.SedogoUser dbUser = SedogoUser.CreateDBUser(modelUser);
            dbUser.Add();
            dbUser.UpdatePassword(modelUser.password);
            //if all image refereces are available, update the profile
            if(!string.IsNullOrEmpty(dbUser.profilePicFilename) && !string.IsNullOrEmpty(dbUser.profilePicThumbnail) &&
                !string.IsNullOrEmpty(dbUser.profilePicPreview))
                dbUser.UpdateUserProfilePic();

            return Json(new { id=dbUser.userID});
            
        }
        #endregion

    }
}

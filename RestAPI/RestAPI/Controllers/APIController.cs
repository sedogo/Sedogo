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
using Sedogo.BusinessObjects;

namespace RestAPI.Controllers
{
    public class APIController : Controller
    {
        protected SedogoDBEntities db = new SedogoDBEntities();

        string email;
        int? currentUserID;
        string fullName;
        private bool CheckAuthentication(UserRole role)
        {
            return Assistant.TryAuthenticate(Request, db, role, out email, out currentUserID, out fullName);
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
            JsonResult jr = null;
            int? ui = PrepareForUserMinimal(id, out jr);
            if (!ui.HasValue)
                return jr;
            userId = ui.Value;

            SedogoUser user = null;  
            //id is the user's id
            try
            {
                user = new SedogoUser(fullName, userId);
            }
            catch (Exception ex)
            {
                if (ex.TargetSite.Name.Contains("ReadUserDetails"))
                {
                    //reading attempt failed - it is likely that the user was not found
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
                }
                else
                    throw ex;
            }
            
            
            //check if any object contains user details
            if(user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
            }
            UserModel suser = new UserModel { 
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
                birthday = (user.birthday.Ticks>0)?user.birthday:(DateTime?)null,
                country = user.countryID,
                timezone = user.timezoneID,
                language = user.languageID
            };

            return Json(suser.GetDetails(), JsonRequestBehavior.AllowGet);

               
                
           
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
            UserModel modelUser = UserModel.CreateUserModel(email, password, firstName, lastName, gender, homeTown, birthday, country, language, timezone, profile, image,
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
            SedogoUser dbUser = UserModel.CreateUserBO(modelUser);
            dbUser.Add();
            dbUser.UpdatePassword(modelUser.password);
            //if all image refereces are available, update the profile
            if(!string.IsNullOrEmpty(dbUser.profilePicFilename) && !string.IsNullOrEmpty(dbUser.profilePicThumbnail) &&
                !string.IsNullOrEmpty(dbUser.profilePicPreview))
                dbUser.UpdateUserProfilePic();

            return Json(new { id=dbUser.userID});
            
        }



        /// <summary>
        /// GET /events/{id}. 
        /// • retrieves the profile of an individual Event
        /// • "id" is Event.id
        /// • authentication is required if this is not a public event - user must then be admin,
        /// member or follower
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Events(int? id)
        {
            #region initial steps
            Response.ContentType = Assistant.MimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion
            int eventId = 0;

            JsonResult jr = null;
            SedogoEvent sevent = null;
            int? e = PrepareForEvent(id, out jr, out sevent);
            if (!e.HasValue)
                return jr;
            eventId = e.Value;
            
            //the event is available. Let's check its details

            


            //everything is fine, let's create a model and return it in JSON
            EventModel model = new EventModel {
                id = sevent.eventID,
                created = sevent.createdDate,
                updated = sevent.lastUpdatedDate,
                name = sevent.eventName,
                user = sevent.userID,
                venue = sevent.eventVenue,
                description = sevent.eventDescription,
                mustDo = sevent.mustDo,
                dateType = sevent.dateType,
                start = (sevent.startDate.Ticks>0)?sevent.startDate : (DateTime?)null,
                rangeStart = (sevent.rangeStartDate.Ticks>0)?sevent.rangeStartDate:(DateTime?)null,
                rangeEnd = (sevent.rangeEndDate.Ticks>0)?sevent.rangeEndDate:(DateTime?)null,
                beforeBirthday = (sevent.beforeBirthday),   //optional!!!
                achieved = sevent.eventAchieved,
                Private = sevent.privateEvent,
                category = sevent.categoryID,   //optional!!
                createdFromEvent = sevent.createdFromEventID, //optional!!
                timezone = sevent.timezoneID,
                image = sevent.eventPicFilename,
                imageThumbnail = sevent.eventPicThumbnail,
                imagePreview = sevent.eventPicPreview

                   
            };
            return Json(model.GetDetails(), JsonRequestBehavior.AllowGet);
           
        }




        #endregion



        #region Collection methods

        /// <summary>
        /// GET /users/{id}/consumption
        /// • retrieves the recents feed, that is the collection of all the messages intended for
        /// this User
        /// • "id" is User.id or User.email
        /// • authentication is required, the authenticated User must be the one with the "id"
        /// specified in the URL
        /// </summary>
        public ActionResult UsersConsumption(string id)
        {
            #region initial steps
            Response.ContentType = Assistant.MimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion
            int userId = 0;

            JsonResult jr = null;
            int? ui = PrepareForUserMinimal(id, out jr);
            if (!ui.HasValue)
                return jr;
            
            userId = ui.Value;

            //now userID is the user's identifier
            if (userId != currentUserID)
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return Json(Assistant.ErrorForbidden, JsonRequestBehavior.AllowGet);
            }

            System.Data.Objects.ObjectResult<spSelectMessageList_Result> sr = db.spSelectMessageList(userId);

            
            List<Dictionary<string, object>> messages = sr.Select(m => new MessageModel
            {
                id = m.MessageID,
                created = m.CreatedDate,
                updated = m.LastUpdatedDate,
                message = m.MessageText,
                read = m.MessageRead,
                Event = m.EventID,
                author = m.PostedByUserID,
                user = userId
            }.GetDetails()).ToList();
            if (messages.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
            }

            
            return Json(messages,JsonRequestBehavior.AllowGet);
            
        }



        /// <summary>
        /// GET /events/{id}/comments
        /// • retrieves the comments for the specified event
        /// • "id" is Event.id
        /// • authentication is required if this is not a public event - user must then be admin,
        /// member or follower
        /// </summary>
        public ActionResult EventsComments(int? id)
        {
            #region initial steps
            Response.ContentType = Assistant.MimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion

            int eventId = 0;

            JsonResult jr = null;
            SedogoEvent sevent = null;
            int? e = PrepareForEvent(id, out jr, out sevent);
            if (!e.HasValue)
                return jr;
            eventId = e.Value;

            //the event is available. Let's check its details

            System.Data.Objects.ObjectResult<spSelectEventCommentsList_Result> sr = db.spSelectEventCommentsList(eventId);
            List<Dictionary<string, object>> comments = sr.Select(m => new CommentModel { 
                        id = m.EventCommentID,
                        created = m.CreatedDate,
                        updated = m.LastUpdatedDate,
                        text = m.CommentText,
                        Event = eventId,
                        user = m.PostedByUserID,
                        image = m.EventImageFilename,
                        imagePreview = m.EventImagePreview,
                        video = m.EventVideoFilename,
                        videoThumbnail = m.EventVideoLink,
                        link = m.EventLink

                    }.GetDetails()
                ).ToList();

            if (comments.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
            }


            return Json(comments, JsonRequestBehavior.AllowGet);
            
        }

        /// <summary>
        /// POST /events/{id}/comments
        /// • posts a new comment to the specified event
        /// • authenticated user must have necessary access rights to perform this action
        /// • the comment resource is passed in as the body of the request
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EventsComments(int? id, string text,int? user, string image, 
            string imagePreview, string video, string videoThumbnail, string link)
        {
            #region initial steps
            Response.ContentType = Assistant.MimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion

            int eventId = 0;

            JsonResult jr = null;
            SedogoEvent sevent = null;
            int? e = PrepareForEvent(id, out jr, out sevent);
            if (!e.HasValue)
                return jr;
            eventId = e.Value;

            //the event is available. Let's check its details

            string error = null;
            CommentModel model = CommentModel.CreateCommentModel(text,eventId,user,
                image, imagePreview,video, videoThumbnail,link,fullName, fullName,out error);

            if (!string.IsNullOrEmpty(error))
            {
                //some error occurred while attempting to create the user object
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = error });
            }
            //the object was created successfully, now we can add it to the database
            SedogoEventComment commentBO = CommentModel.CreateCommentBO(model);
            commentBO.Add();

            return Json(new { id = commentBO.eventCommentID });
        }

        /// <summary>
        ///  GET /users/{id}/events
        ///  • retrieves the list of events the specified user is part of and have not been
        ///  achieved
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        public ActionResult UsersEvents(string id)
        {
            #region initial steps
            Response.ContentType = Assistant.MimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion
            int userId = 0;

            JsonResult jr = null;
            int? ui = PrepareForUserMinimal(id, out jr);
            if (!ui.HasValue)
                return jr;

            userId = ui.Value;

            //now userID is the user's identifier

            System.Data.Objects.ObjectResult<spSelectNotAchievedEventList_Result> sr =
                db.spSelectNotAchievedEventList(userId);

            List<Dictionary<string, object>> events = sr.Select(m => new EventModel
            {
                id = m.EventID,
                created = m.CreatedDate,
                updated = m.LastUpdatedDate,
                name = m.EventName,
                user = userId,
                venue = m.EventVenue,
                description = m.EventDescription,
                mustDo = m.MustDo,
                dateType = m.DateType,
                start = m.StartDate,
                rangeStart = m.RangeStartDate,
                rangeEnd = m.RangeEndDate,
                beforeBirthday = m.BeforeBirthday,
                achieved = m.EventAchieved,
                Private = m.PrivateEvent,
                category = m.CategoryID,
                createdFromEvent = m.CreatedFromEventID,
                timezone = m.TimezoneID,
                image = m.EventPicFilename,
                imageThumbnail = m.EventPicThumbnail,
                imagePreview = m.EventPicPreview
            }.GetDetails()
            ).ToList();

            if (events.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
            }


            return Json(events, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// GET /users/{id}/achieved
        /// • retrieves the list of events the specified user is part of and have been achieved
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        public ActionResult UsersAchieved(string id)
        {
            #region initial steps
            Response.ContentType = Assistant.MimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion
            int userId = 0;

            JsonResult jr = null;
            int? ui = PrepareForUserMinimal(id, out jr);
            if (!ui.HasValue)
                return jr;

            userId = ui.Value;

            //now userID is the user's identifier

            System.Data.Objects.ObjectResult<spSelectAchievedEventList_Result> sr =
                db.spSelectAchievedEventList(userId);

            List<Dictionary<string, object>> events = sr.Select(m => new EventModel {
                    id = m.EventID,
                    created = m.CreatedDate,
                    updated = m.LastUpdatedDate,
                    name = m.EventName,
                    user = userId,
                    venue = m.EventVenue,
                    description = m.EventDescription,
                    mustDo = m.MustDo,
                    dateType = m.DateType,
                    start = m.StartDate ,
                    rangeStart = m.RangeStartDate,
                    rangeEnd = m.RangeEndDate,
                    beforeBirthday = m.BeforeBirthday,
                    achieved = m.EventAchieved,
                    Private = m.PrivateEvent,
                    category = m.CategoryID,   
                    createdFromEvent = m.CreatedFromEventID, 
                    timezone = m.TimezoneID,
                    image = m.EventPicFilename,
                    imageThumbnail = m.EventPicThumbnail,
                    imagePreview = m.EventPicPreview            
                }.GetDetails()
            ).ToList();

            if (events.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
            }


            return Json(events, JsonRequestBehavior.AllowGet);
            
        }


        /// <summary>
        /// GET /users/{id}/followed
        /// • retrieves the list of events the specified user is following
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UsersFollowed(string id)
        {
            #region initial steps
            Response.ContentType = Assistant.MimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion
            int userId = 0;

            JsonResult jr = null;
            int? ui = PrepareForUserMinimal(id, out jr);
            if (!ui.HasValue)
                return jr;

            userId = ui.Value;

            //now userID is the user's identifier

            System.Data.Objects.ObjectResult<spSelectTrackedEventListByUserID_Result> sr =
                db.spSelectTrackedEventListByUserID(userId);

            List<Dictionary<string, object>> events = sr.Select(m => new EventModel
            {
                id = m.EventID,
                created = m.CreatedDate,
                updated = m.LastUpdatedDate,
                name = m.EventName,
                user = userId,
                //venue = m.EventVenue,
                //description = m.EventDescription,
                mustDo = m.MustDo, 
                
                dateType = m.DateType,
                start = m.StartDate,
                rangeStart = m.RangeStartDate,
                rangeEnd = m.RangeEndDate,
                beforeBirthday = m.BeforeBirthday,
                achieved = m.EventAchieved,
                Private = m.PrivateEvent,
                category = m.CategoryID,
                //createdFromEvent = m.CreatedFromEventID,
                timezone = m.TimezoneID,
                image = m.EventPicFilename,
                imageThumbnail = m.EventPicThumbnail,
                imagePreview = m.EventPicPreview
            }.GetDetails()
            ).ToList();

            if (events.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
            }


            return Json(events, JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region Search Method

        public ActionResult Search()
        {
            #region initial steps
            Response.ContentType = Assistant.MimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion
            #region preparation
            string query = Request.QueryString["query"]; //is the search query
            string type = Request.QueryString["type"];  //the type of query
            string latStr = Request.QueryString["lat"];  //is the location in latitude/longitude, included if type is "location"
            string lngStr = Request.QueryString["lng"];  //is the location in latitude/longitude, included if type is "location"
            string radiusStr = Request.QueryString["radius"]; //is the radius to search within, in km, included if type is "location"
            string startStr = Request.QueryString["start"];  //the offset to start at (OFFSET in SQL), defaults to 0
            string countStr = Request.QueryString["count"];  //the total number of results to return (LIMIT in SQL), defaults to 10

            if (type != "text" && type != "location" && type != "random")
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "type-attribute-required" }, JsonRequestBehavior.AllowGet);
            }
            double lat = 0, lng = 0;
            int radius = 0;
            if (type == "location")
            {
                if (!double.TryParse(latStr, out lat))
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { error = "lat-attribute-required" }, JsonRequestBehavior.AllowGet);
                }
                if( !double.TryParse(lngStr, out lng))
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { error = "lng-attribute-required" }, JsonRequestBehavior.AllowGet);
                }
                if (!int.TryParse(radiusStr, out radius))
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { error = "radius-attribute-required" }, JsonRequestBehavior.AllowGet);
                }
            }
            int? start = null, count = null;
            int temp;
            if (!int.TryParse(startStr, out temp))
                start = null;
            else
                start = temp;

            if (!int.TryParse(countStr, out temp))
                count = null;
            else
                count = temp;
            #endregion

            System.Data.Objects.ObjectResult<AnySearchEventsProcedure_Result> searchResult = null;
            switch(type)
            {
                case "text":
                    searchResult = db.spSearchLimitedEvents(query, start, count);
                    break;
                case "location":
                    searchResult = db.spSearchEventsByLocation(query, lat, lng, radius, start, count);
                    break;
                case "random":
                    searchResult = db.spSearchLimitedRandomEvents(count);
                    break;
                default: break;
            }

            if (searchResult == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
            }

            List<Dictionary<string, object>> events = searchResult.Select(m => new EventModel
            {
                id = m.EventID,
                created = m.CreatedDate,
                updated = m.LastUpdatedDate,
                name = m.EventName,
                user = m.UserID,
                //venue = m.EventVenue,
                //description = m.EventDescription,
                mustDo = m.MustDo,

                dateType = m.DateType,
                start = m.StartDate,
                rangeStart = m.RangeStartDate,
                rangeEnd = m.RangeEndDate,
                beforeBirthday = m.BeforeBirthday,
                achieved = m.EventAchieved,
                Private = m.PrivateEvent,
                category = m.CategoryID,
                //createdFromEvent = m.CreatedFromEventID,
                timezone = m.TimezoneID,
                image = m.EventPicFilename,
                imageThumbnail = m.EventPicThumbnail,
                imagePreview = m.EventPicPreview
            }.GetDetails()
            ).ToList();

            if (events.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
            }


            return Json(events, JsonRequestBehavior.AllowGet);
        }      

        #endregion


        #region Events auxiliary methods

        /// <summary>
        /// Check if we can work with this event
        /// </summary>
        /// <param name="id">an object that should contain the event id</param>
        /// <param name="returnObject">if this is not null, then it is a Json object which should be returned by the calling function</param>
        /// <param name="sevent">if everything is ok, this object is the business object for the event</param>
        /// <returns>null, if we cannot resume work with this event, or event's id otherwise</returns>
        private int? PrepareForEvent(int? id, out JsonResult returnObject, out SedogoEvent sevent)
        {
            int eventId = 0;
            returnObject = null;
            sevent = null;
            if (!id.HasValue)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                returnObject =  Json(new { error = "event-id-required" }, JsonRequestBehavior.AllowGet);
                return null;
            }

            eventId = id.Value;
            
            //id is the event's id
            try
            {
                sevent = new SedogoEvent(fullName, eventId);
            }
            catch (Exception ex)
            {
                if (ex.TargetSite.Name.Contains("ReadEventDetails"))
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    returnObject =  Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
                    return null;
                }
                else
                    throw ex;
            }

            //check if the object contains event details
            if (sevent == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                returnObject =  Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
                return null;
            }

            //Copying business logic from viewEvent.aspx.cx
            if (!EventModel.CanContinueAccordingToBusinessLogic(sevent, currentUserID.Value, fullName, Response,
                Json, out returnObject))
            {
                //EventModel.CanContinueAccordingToBusinessLogic has already set the response code
                return null;
            }
            //everything is ok, we can resume work
            return eventId;
                
        }


        #endregion
        #region User auxiliary methods

        /// <summary>
        /// Implements some initial checks for whether we can resume working with the user object.
        /// </summary>
        /// <param name="id">a string that should be the user's id or email</param>
        /// <param name="returnObject">if some checks fail, this object is the Json object which a calling function
        /// has to return</param>
        /// <returns>null if checks fail, user's id otherwise</returns>
        private int? PrepareForUserMinimal(string id, out JsonResult returnObject)
        {
            int userId = 0;
            returnObject = null;
            if (string.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                returnObject =  Json(new { error = "user-id-required" }, JsonRequestBehavior.AllowGet);
                return null;
            }

            if (!int.TryParse(id, out userId))
            {
                //id is the user's email
                string emailAddress = id;
                userId = SedogoUser.GetUserIDFromEmailAddress(emailAddress);
            }

            return userId;
        }

        #endregion
    }
}

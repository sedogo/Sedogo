using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Objects;
using System.Text;
using System.Net;
using RestAPI.Models;
using Sedogo.BusinessObjects;

namespace RestAPI.Controllers
{
    public class APIController : Controller
    {
        private readonly SedogoDBEntities _db = new SedogoDBEntities();

        string email;
        int? currentUserID;
        string fullName;
        private bool CheckAuthentication(UserRole role)
        {
            return Assistant.TryAuthenticate(Request, _db, role, out email, out currentUserID, out fullName);
        }

        public ActionResult HelloWorld()
        {
            var e = new Assistant.WriteLongException();
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

                string logContents = strDate + " - Hello world";
                // All error messages are written to the event log
                //System.Diagnostics.EventLog appLog = new System.Diagnostics.EventLog();
               // appLog.Source = "Sedogo";
                //appLog.WriteEntry(logContents);
                var sw = new System.IO.StreamWriter(System.Configuration.ConfigurationManager.AppSettings["ErrorLogFile"], true);
                sw.WriteLine(logContents);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                e.Ex = ex.Message;
            }
            return View(e);
        }

        public ActionResult Index()
        {
            return View("Test");
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
            Response.ContentType = Assistant.JsonMimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized,JsonRequestBehavior.AllowGet);
            }
            #endregion

            JsonResult jr;
            int? ui = PrepareForUserMinimal(id, out jr);
            if (!ui.HasValue)
                return jr;
            int userId = ui.Value;

            SedogoUser user;  
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
                throw;
            }


            //check if any object contains user details
            var suser = new UserModel { 
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
            //no authentication for registration

            /*#region initial steps
            Response.ContentType = Assistant.JsonMimeType;
            if (!CheckAuthentication(UserRole.Admin))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized);
            }
            #endregion  */
            
            //spSelectAdministratorDetails_Result adminDetails = db.spSelectAdministratorDetails(currentUserID).FirstOrDefault();
            string error;
             //let's try to create a user object
            UserModel modelUser = UserModel.CreateUserModel(email, password, firstName, lastName, gender, homeTown, birthday, country, language, timezone, profile, image,
            imageThumbnail, imagePreview, "","", /*adminDetails.AdministratorName, adminDetails.AdministratorName, */out error);

            if (!string.IsNullOrEmpty(error))
            {
                //some error occurred while attempting to create the user object
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new {error });
            }
            //the object was created successfully, now we can add it to the database

            //check the unique email
            int testUserId = SedogoUser.GetUserIDFromEmailAddress(modelUser.email);

            if (testUserId > 0)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new{error="email-not-unique"});
            }
            SedogoUser dbUser = UserModel.CreateUserBO(modelUser);
            dbUser.Add();
            dbUser.UpdatePassword(modelUser.password);

            dbUser.loginEnabled = true;
            dbUser.Update();

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
            Response.ContentType = Assistant.JsonMimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion

            JsonResult jr;
            SedogoEvent sevent;
            int? e = PrepareForEvent(id, out jr, out sevent);
            if (!e.HasValue)
                return jr;


            //everything is fine, let's create a model and return it in JSON
            var model = new EventModel {
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

        /// <summary>
        /// Gets the Events action result.
        /// </summary>
        /// <param name="user">The user id.</param>
        /// <param name="created">The created date .</param>
        /// <param name="updated">The updated date .</param>
        /// <param name="name">The event name.</param>
        /// <param name="venue">The event venue.</param>
        /// <param name="description">The event description.</param>
        /// <param name="mustDo">must do.</param>
        /// <param name="dateType">Type of the date.</param>
        /// <param name="start">The start date.</param>
        /// <param name="rangeStart">The range start.</param>
        /// <param name="rangeEnd">The range end.</param>
        /// <param name="beforeBirthday">The before birthday.</param>
        /// <param name="privateEvent">if set to <c>true</c> the event is private.</param>
        /// <param name="category">The category id.</param>
        /// <param name="createdFromEvent">The created from event id.</param>
        /// <param name="timeZone">The time zone id.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Events(int? user, DateTime created, DateTime updated, string name, string venue, 
                                   string description, bool mustDo, string dateType, DateTime? start, DateTime? rangeStart,
                                   DateTime? rangeEnd, int? beforeBirthday, bool privateEvent, int? category,
                                   int? createdFromEvent, int timeZone)
        {
            Response.ContentType = Assistant.JsonMimeType;
            if (!user.HasValue)
            {
                return GetInvalidUserIdResult();
            }
            if (!CheckAuthentication(UserRole.Any))
            {
                return GetUnauthorizedActionResult();
            }
            if (currentUserID != user)
            {
                return GetForbiddenActionResult();
            }

            try
            {
                var newEvent = new SedogoEvent(fullName)
                                       {
                                           beforeBirthday = beforeBirthday ?? default(int),
                                           categoryID = category ?? default(int),
                                           createdFromEventID = createdFromEvent ?? default(int),
                                           dateType = dateType,
                                           eventDescription = description ?? string.Empty,
                                           mustDo = mustDo,
                                           startDate = start ?? DateTime.MinValue,
                                           rangeStartDate = rangeStart ?? DateTime.MinValue,
                                           rangeEndDate = rangeEnd ?? DateTime.MinValue,
                                           privateEvent = privateEvent,
                                           timezoneID = timeZone,
                                           eventVenue = venue ?? string.Empty,
                                           eventName = name,
                                           userID = user.Value,
                                       };
                newEvent.Add();
                return Json(new { id = newEvent.eventID });
            }
            catch (Exception ex)
            {
                return GetExceptionResult(ex, "POST Events (Create)");
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName == "Events" && HttpContext.Request.HttpMethod == "POST")
            {
                var valueProviderCollection = (ValueProviderCollection) (filterContext.Controller).ValueProvider;
                var dictionaryValueProvider =
                    (DictionaryValueProvider<object>)
                    valueProviderCollection.Where(x => x is DictionaryValueProvider<object> && !(x is RouteDataValueProvider)).First();
                if (dictionaryValueProvider.ContainsPrefix("private"))
                {
                    var isPrivate = dictionaryValueProvider.GetValue("private").RawValue;
                    filterContext.ActionParameters["privateEvent"] = Convert.ToBoolean(isPrivate);
                }
                else
                {
                    filterContext.ActionParameters["privateEvent"] = filterContext.ActionParameters["privateEvent"] ??
                                                                     false;
                }
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Userses the followed.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="eventId">The event id.</param>
        /// <param name="showOnTimeline">The show on timeline.</param>
        /// <param name="joinPending">The join pending.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UsersFollowed(int? id, int? eventId, bool? showOnTimeline, bool? joinPending)
        {
            Response.ContentType = Assistant.JsonMimeType;
            if (!id.HasValue)
            {
                return GetInvalidUserIdResult();
            }
            if (!eventId.HasValue)
            {
                return GetErrorActionResult("eventId is null");
            }
            if (!CheckAuthentication(UserRole.Any))
            {
                return GetUnauthorizedActionResult();
            }
            if (currentUserID != id)
            {
                return GetForbiddenActionResult();
            }

            try
            {
                var trackedEvent = new Sedogo.BusinessObjects.TrackedEvent(fullName)
                                       {
                                           eventID = eventId.Value,
                                           joinPending = joinPending ?? false,
                                           showOnTimeline = showOnTimeline ?? true
                                       };
                trackedEvent.Add();
                return Json(new { id = trackedEvent.trackedEventID });
            }
            catch (Exception ex)
            {
                return GetExceptionResult(ex, "POST users/{id}/follwed");
            }
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
            Response.ContentType = Assistant.JsonMimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion

            JsonResult jr;
            int? ui = PrepareForUserMinimal(id, out jr);
            if (!ui.HasValue)
                return jr;
            
            int userId = ui.Value;

            //now userID is the user's identifier
            if (userId != currentUserID)
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return Json(Assistant.ErrorForbidden, JsonRequestBehavior.AllowGet);
            }

            //System.Data.Objects.ObjectResult<spSelectMessageList_Result> sr = db.spSelectMessageList(userId);

            
            //List<Dictionary<string, object>> messages = sr.Select(m => new MessageModel
            //{
            //    id = m.MessageID,
            //    created = m.CreatedDate,
            //    updated = m.LastUpdatedDate,
            //    message = m.MessageText,
            //    read = m.MessageRead,
            //    Event = m.EventID,
            //    author = m.PostedByUserID,
            //    user = userId
            //}.GetDetails()).ToList();

            var messages =
                ((from m in _db.Messages
                  where !m.Deleted && m.UserID == userId
                  select
                      new
                          {
                              id = m.MessageID,
                              eventId = m.EventID,
                              user = m.PostedByUserID,
                              text = m.MessageText,
                              created = m.CreatedDate,
                              updated = m.LastUpdatedDate,
                              read = (bool?)m.MessageRead
                          }).Union
                    (from c in _db.EventComments
                     where c.Event.UserID == userId && !c.Deleted && !c.Event.Deleted
                     select new
                                {
                                    id = c.EventCommentID,
                                    eventId = (int?)c.EventID,
                                    user = c.PostedByUserID,
                                    text = c.CommentText, 
                                    created = c.CreatedDate,
                                    updated = c.LastUpdatedDate,
                                    read = (bool?)null
                                }).Union
                    (from c in _db.EventComments
                     join te in _db.TrackedEvents on c.EventID equals te.EventID
                     where te.Event.UserID == userId && !c.Deleted && !c.Event.Deleted
                     select new
                                {
                                    id = c.EventCommentID,
                                    eventId = (int?)c.EventID,
                                    user = c.PostedByUserID,
                                    text = c.CommentText, 
                                    created = c.CreatedDate, 
                                    updated = c.LastUpdatedDate,
                                    read = (bool?)null
                                }).Union
                    (from i in _db.EventInvites
                     where i.UserID == userId && !i.Deleted && !i.Event.Deleted
                     select new
                     {
                         id = i.EventInviteID,
                         eventId = (int?)i.EventID,
                         user = i.UserID ?? 0,
                         text = i.InviteAdditionalText,
                         created = i.CreatedDate,
                         updated = i.LastUpdatedDate,
                         read = (bool?) null
                     })).OrderByDescending(x => x.created).ToList();
            var result =
                messages.Select(
                    x =>
                    new
                        {
                            x.id,
                            created = x.created.ToString("u"),
                            updated = x.updated.ToString("u"),
                            message = x.text,
                            _event = x.eventId,
                            x.user,
                            x.read
                        }.ToDictionary());
            
            return Json(result,JsonRequestBehavior.AllowGet);
            
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
            Response.ContentType = Assistant.JsonMimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion

            JsonResult jr;
            SedogoEvent sevent;
            int? e = PrepareForEvent(id, out jr, out sevent);
            if (!e.HasValue)
                return jr;
            int eventId = e.Value;

            //the event is available. Let's check its details

            var sr = _db.spSelectEventCommentsList(eventId);
            var comments = sr.OrderByDescending(x => x.CreatedDate).Select(m => new CommentModel
            { 
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

            /*if (comments.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
            }  */


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
            Response.ContentType = Assistant.JsonMimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion

            JsonResult jr;
            SedogoEvent sevent;
            int? e = PrepareForEvent(id, out jr, out sevent);
            if (!e.HasValue)
                return jr;
            int eventId = e.Value;

            //the event is available. Let's check its details

            string error;
            CommentModel model = CommentModel.CreateCommentModel(text,eventId,user,
                image, imagePreview,video, videoThumbnail,link,fullName, fullName,out error);

            if (!string.IsNullOrEmpty(error))
            {
                //some error occurred while attempting to create the user object
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new {error});
            }
            //the object was created successfully, now we can add it to the database
            SedogoEventComment commentBo = CommentModel.CreateCommentBO(model);
            commentBo.Add();

            return Json(new { id = commentBo.eventCommentID });
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
            Response.ContentType = Assistant.JsonMimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion

            JsonResult jr;
            int? ui = PrepareForUserMinimal(id, out jr);
            if (!ui.HasValue)
                return jr;

            int userId = ui.Value;

            //now userID is the user's identifier

            var sr = _db.spSelectNotAchievedEventList(userId);

            var events = sr.OrderByDescending(x => x.CreatedDate).Select(m => new EventModel
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

           /* if (events.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
            }*/


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
            Response.ContentType = Assistant.JsonMimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion

            JsonResult jr;
            int? ui = PrepareForUserMinimal(id, out jr);
            if (!ui.HasValue)
                return jr;

            int userId = ui.Value;

            //now userID is the user's identifier

            var sr = _db.spSelectAchievedEventList(userId);

            var events = sr.OrderByDescending(x => x.CreatedDate).Select(m => new EventModel {
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

            /*if (events.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
            }*/


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
            Response.ContentType = Assistant.JsonMimeType;
            if (!CheckAuthentication(UserRole.Any))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
            }
            #endregion

            JsonResult jr;
            int? ui = PrepareForUserMinimal(id, out jr);
            if (!ui.HasValue)
                return jr;

            int userId = ui.Value;

            //now userID is the user's identifier

            var sr = _db.spSelectTrackedEventListByUserID(userId);

            var events = sr.OrderByDescending(x => x.CreatedDate).Select(m => new EventModel
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

            /*if (events.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
            } */


            return Json(events, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        public  ActionResult Messages(int? id)
        {
            try
            {
                #region initial steps
                Response.ContentType = Assistant.JsonMimeType;
                if (!CheckAuthentication(UserRole.Any))
                {
                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
                }
                #endregion

                new Sedogo.BusinessObjects.Message(fullName, id ?? 0) { messageRead = true }.Update();

                return Json(new {read = 1}, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return HelloWorld();
            }
        }

        /// <summary>
        /// POST /invites. creates a request to join a goal (an invitation)
        /// the request body is the JSON representation of the Invite resource
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="inviteAdditionalText">The invite additional text.</param>
        /// <param name="inviteEmailSent">if set to <c>true</c> [invite email sent].</param>
        /// <param name="inviteEmailSentEmailAddress">The invite email sent email address.</param>
        /// <param name="inviteEmailSentDate">The invite email sent date.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Invites(int? eventId, string inviteAdditionalText, bool? inviteEmailSent, string inviteEmailSentEmailAddress, DateTime? inviteEmailSentDate)
        {
            #region initial steps
            Response.ContentType = Assistant.JsonMimeType;
            if (!CheckAuthentication(UserRole.Admin))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(Assistant.ErrorUnauthorized);
            }
            #endregion

            try
            {
                var invite = new Sedogo.BusinessObjects.EventInvite(fullName)
                                 {
                                     emailAddress = email,
                                     eventID = eventId ?? 0,
                                     userID = currentUserID ?? 0,
                                     inviteAdditionalText = inviteAdditionalText,
                                     inviteEmailSent = inviteEmailSent ?? false,
                                     inviteEmailSentEmailAddress = inviteEmailSentEmailAddress,
                                     inviteEmailSentDate = inviteEmailSentDate ?? DateTime.Now,
                                 };
                invite.Add();

                return Json(new { id = invite.eventInviteID });
            }
            catch
            {
                return HelloWorld();
            }
        }


        #endregion


        #region Search Method

        public ActionResult Search()
        {
            #region initial steps
            Response.ContentType = Assistant.JsonMimeType;
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
            int temp;
            var start = !int.TryParse(startStr, out temp) ? (int?) null : temp;

            var count = !int.TryParse(countStr, out temp) ? (int?) null : temp;

            #endregion

            ObjectResult<AnySearchEventsProcedure_Result> searchResult = null;
            switch(type)
            {
                case "text":
                    searchResult = _db.spSearchLimitedEvents(query, start, count);
                    break;
                case "location":
                    searchResult = _db.spSearchEventsByLocation(query, lat, lng, radius, start, count);
                    break;
                case "random":
                    searchResult = _db.spSearchLimitedRandomEvents(count);
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
            returnObject = null;
            sevent = null;
            if (!id.HasValue)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                returnObject =  Json(new { error = "event-id-required" }, JsonRequestBehavior.AllowGet);
                return null;
            }

            var eventId = id.Value;
            
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
                throw;
            }

            //Copying business logic from viewEvent.aspx.cx
            if (!EventModel.CanContinueAccordingToBusinessLogic(sevent, currentUserID ?? 0, fullName, Response,
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
            int userId;
            returnObject = null;
            if (string.IsNullOrEmpty(id))
            {
                returnObject = GetInvalidUserIdResult();
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

        /// <summary>
        /// Gets the invalid user id result.
        /// </summary>
        /// <returns></returns>
        private JsonResult GetInvalidUserIdResult()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Json(new { error = "user-id-required" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the exception action result.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <param name="routineName">Name of the routine.</param>
        /// <returns></returns>
        private JsonResult GetExceptionResult(Exception ex, string routineName)
        {
            var message = new StringBuilder();
            var exception = ex;
            while (exception != null)
            {
                message.AppendFormat("Message: " + ex.Message);
                message.AppendLine();
                message.AppendFormat("StackTrace: " + ex.StackTrace);
                message.AppendLine();
                message.AppendFormat("Data: " + ex.Data);
                message.AppendLine();
                message.AppendFormat("Help Link: " + ex.HelpLink);
                message.AppendLine();
                exception = exception.InnerException;
            }

            new ErrorLog().WriteLog("APIController", routineName, message.ToString(),logMessageLevel.errorMessage);

            return GetErrorActionResult("internal server error");
        }

        /// <summary>
        /// Gets the error action result.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        private JsonResult GetErrorActionResult(string error)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return Json(new {error}, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// Gets the forbidden action result.
        /// </summary>
        /// <returns></returns>
        private ActionResult GetForbiddenActionResult()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Json(Assistant.ErrorForbidden, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the unauthorized action result.
        /// </summary>
        /// <returns></returns>
        private ActionResult GetUnauthorizedActionResult()
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Json(Assistant.ErrorUnauthorized, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sedogo.BusinessObjects;
using System.Net;
using System.Web.Mvc;

namespace RestAPI.Models
{

    /*Goals are the key concept in Sedogo and can be considered the social object type that
the service is based around. Internally, they are referred to as Events.
A goal can be considered to be a group which has users with the following rights:
• admin - can add, remove users, view the ; only one User can be admin at the
moment
• member - can view, can post messages
• follower - gets message updates, but don't see all the goal details, like the list of
members
Goals can be private or public. If private, only admins, members and followers can view
it. A goal contains also contains comments.
Included:
• id int
• created date
• updated date
• name string
• user int - admin User ID
• venue string (optional)
• description string (optional)
• mustDo boolean
• dateType string
• start date (optional)
• rangeStart date (optional)
• rangeEnd date (optional)
• beforeBirthday int (optional)
• achieved boolean
• private boolean
• category int (optional)
• createdFromEvent int (optional)
• timezone int
• image string (optional) - URL of the full sized image
• imageThumbnail string (optional) - URL of the thumbnail image
• imagePreview string (optional) - ?
Hidden:
• EventGUID string
• Deleted boolean
• LastUpdatedByFullName string
• CreatedByFullName string
• showDefault boolean*/
    
    /// <summary>
    /// A model for events
    /// </summary>
    public class EventModel: BaseModel
    {
        /*Included*/
        public string name { get; set; }
        /// <summary>
        /// Admin USER ID
        /// </summary>
        public int user { get; set; }
        public string venue { get; set; }
        public string description { get; set; }
        public bool mustDo { get; set; }
        public string dateType { get; set; }
        public DateTime? start { get; set; }
        public DateTime? rangeStart { get; set; }
        public DateTime? rangeEnd { get; set; }
        public int? beforeBirthday { get; set; }
        public bool achieved { get; set; }
        public bool Private { get; set; }
        public int? category { get; set; }
        public int? createdFromEvent { get; set; }
        public int timezone { get; set; }
        /// <summary>
        ///  URL of the full sized image
        /// </summary>
        public string image { get; set; }
        /// <summary>
        /// URL of the thumbnail image
        /// </summary>
        public string imageThumbnail { get; set; }
        public string imagePreview { get; set; }
        /*Hidden*/
        public string LastUpdatedByFullName { get; set; }
        public string CreatedByFullName { get; set; }


       

        public override Dictionary<string, object> GetDetails()
        {
            Dictionary<string, object> rv = InitializeDictionary();
            rv.Add("name", name);
            rv.Add("user", user);
            if (!string.IsNullOrEmpty(venue))
                rv.Add("venue", venue);
            if (!string.IsNullOrEmpty(description))
                rv.Add("description", description);
            rv.Add("mustDo", mustDo);
            rv.Add("dateType", dateType);
            if (start.HasValue)
                rv.Add("start", Assistant.ConvertToString(start.Value));
            if(rangeStart.HasValue)
                rv.Add("rangeStart", Assistant.ConvertToString(rangeStart.Value));
            if (rangeEnd.HasValue)
                rv.Add("rangeEnd", Assistant.ConvertToString(rangeEnd.Value));
            if (beforeBirthday.HasValue)
                rv.Add("beforeBirthday", beforeBirthday.Value);
            rv.Add("achived", achieved);
            rv.Add("private", Private);
            if (category.HasValue)
                rv.Add("category", category.Value);
            if (createdFromEvent.HasValue)
                rv.Add("createdFromEvent", createdFromEvent.Value);
            rv.Add("timezone", timezone);
            if (!string.IsNullOrEmpty(image))
                rv.Add("image", image);
            if (!string.IsNullOrEmpty(imageThumbnail))
                rv.Add("imageThumbnail", imageThumbnail);
            if (!string.IsNullOrEmpty(imagePreview))
                rv.Add("imagePreview", imagePreview);
            return rv;


        }

        public delegate JsonResult JsonDelegate(object data, JsonRequestBehavior behavior);

        /// <summary>
        /// When the event is received from the database, it is necessary to check business logic rules. If the check is successful,
        /// we can resume work with this event
        /// </summary>
        /// <param name="sevent">business object for the event</param>
        /// <param name="currentUserID">current user id. If the event is private, not all users are allowed to see it</param>
        /// <param name="fullName">the name of the logged in user. It is used for creating a TracedEvent business object</param>
        /// <param name="Response">the http response object. If the check is unsuccessful, the status code is set</param>
        /// <param name="Json">a delegate that refers to a function which creates a JSON object</param>
        /// <param name="returnObject">an object created by Json delegate. This object should be returned from the calling function</param>
        /// <returns>true or false</returns>
        public static bool CanContinueAccordingToBusinessLogic(SedogoEvent sevent, int currentUserID, string fullName,
            HttpResponseBase Response,JsonDelegate Json, out JsonResult returnObject)
        {
            returnObject = null;
            if (sevent.deleted)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                returnObject =  Json(Assistant.ErrorNotFound, JsonRequestBehavior.AllowGet);
                return false;
            }
            int trackedEventID = Sedogo.BusinessObjects.TrackedEvent.GetTrackedEventID(sevent.eventID, currentUserID);
            if (sevent.userID != currentUserID)
            {
                // Viewing someone elses event

                // For private events, you need to either own the event, be tracking it,
                // or have been invited to it
                if (sevent.privateEvent)
                {
                    int eventInviteCount = Sedogo.BusinessObjects.EventInvite.CheckUserEventInviteExists(sevent.eventID, currentUserID);
                    bool showOnTimeline = false;
                    if (trackedEventID > 0)
                    {
                        var trackedEvent = new Sedogo.BusinessObjects.TrackedEvent(fullName, trackedEventID);
                        showOnTimeline = trackedEvent.showOnTimeline;
                    }
                    if (eventInviteCount <= 0 && !showOnTimeline)
                    {
                        // Viewing private events is not permitted
                        Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        returnObject =  Json(Assistant.ErrorForbidden, JsonRequestBehavior.AllowGet);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Attempts to create a model using input parameters. If some parameters are missing, and a model can not be created, an output error parameter 
        /// contains an error message
        /// </summary>
        /// <param name="name"></param>
        /// <param name="user"></param>
        /// <param name="venue"></param>
        /// <param name="description"></param>
        /// <param name="mustDo"></param>
        /// <param name="dateType"></param>
        /// <param name="start"></param>
        /// <param name="rangeStart"></param>
        /// <param name="rangeEnd"></param>
        /// <param name="beforeBirthday"></param>
        /// <param name="achieved"></param>
        /// <param name="Private"></param>
        /// <param name="category"></param>
        /// <param name="createdFromEvent"></param>
        /// <param name="timezone"></param>
        /// <param name="image"></param>
        /// <param name="imageThumbnail"></param>
        /// <param name="imagePreview"></param>
        /// <param name="LastUpdatedByFullName"></param>
        /// <param name="CreatedByFullName"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static EventModel CreateEventModel(string name,
            int? user,string venue,string description,bool? mustDo,string dateType,DateTime? start, DateTime? rangeStart, DateTime? rangeEnd,
         int? beforeBirthday, bool? achieved, bool? Private, int? category,int? createdFromEvent, int? timezone,
         string image,string imageThumbnail, string imagePreview, string LastUpdatedByFullName,  string CreatedByFullName, out string error)
        {
            error = null;
            if (string.IsNullOrEmpty(name))
                error = "name-attribute-required";
            else if (!user.HasValue)
                error = "user-attribute-required";
            else if (!mustDo.HasValue)
                error = "mustDo-attribute-required";
            else if(string.IsNullOrEmpty(dateType))
                error = "dateType-attribute-required";
            else if( !achieved.HasValue)
                error="achieved-attribute-required";
            else if(!Private.HasValue)
                error = "private-attribute-required";
            else if(!timezone.HasValue)
                error = "timezone-attribute-required";

            if (!string.IsNullOrEmpty(error))
                return null;

            EventModel model = new EventModel { 
                created = DateTime.Now,
                updated = DateTime.Now,
                
                name = name,
                user = user.Value,
                venue = venue,
                description = description,
                mustDo = mustDo.Value,
                dateType = dateType,
                start = start,
                rangeStart = rangeStart,
                rangeEnd = rangeEnd,
                beforeBirthday = beforeBirthday,
                achieved = achieved.Value,
                Private = Private.Value,
                category = category,
                createdFromEvent = createdFromEvent,
                timezone = timezone.Value,
                image = image,
                imageThumbnail = imageThumbnail,
                imagePreview = imagePreview,
                LastUpdatedByFullName = LastUpdatedByFullName,
                CreatedByFullName = CreatedByFullName
            };

            return model;
        }


        /// <summary>
        /// Create a business object for events using the model as the source of details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SedogoEvent CreateEventBO(EventModel model)
        {
            SedogoEvent eventBO = new SedogoEvent(model.CreatedByFullName);
            eventBO.eventName = model.name;
            eventBO.userID = model.user;
            eventBO.eventVenue = model.venue;
            eventBO.eventDescription = model.description;
            eventBO.mustDo = model.mustDo;
            eventBO.dateType = model.dateType;
            if(model.start.HasValue) eventBO.startDate = model.start.Value; 
            if(model.rangeStart.HasValue) eventBO.rangeStartDate = model.rangeStart.Value;
            if(model.rangeEnd.HasValue) eventBO.rangeEndDate = model.rangeEnd.Value ;
            if(model.beforeBirthday.HasValue)eventBO.beforeBirthday = model.beforeBirthday.Value;
            eventBO.eventAchieved = model.achieved;
            eventBO.privateEvent = model.Private;
            if(model.category.HasValue) eventBO.categoryID = model.category.Value;
            if (model.createdFromEvent.HasValue) eventBO.createdFromEventID = model.createdFromEvent.Value;
            eventBO.timezoneID = model.timezone;
            if (!string.IsNullOrEmpty(model.image)) eventBO.eventPicFilename = model.image;
            if (!string.IsNullOrEmpty(model.imageThumbnail)) eventBO.eventPicThumbnail = model.imageThumbnail;
            if (!string.IsNullOrEmpty(model.imagePreview)) eventBO.eventPicPreview = model.imagePreview;

            return eventBO;
        }
    }
}
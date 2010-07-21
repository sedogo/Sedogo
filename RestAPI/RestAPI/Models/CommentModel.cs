using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestAPI.Models
{
    /*
     Comment is a type of message which is visible to all members and followers of an Event
or everybody if the Event is public.
Included:
• id int
• created date
• updated date
• text string (optional)
• event int
• user int
• image string (optional) - URL of the full sized image
• imagePreview string (optional) - ?
• video string (optional) - URL of the video (may be hosted by a third party, how
do we handle different formats?)
• videoThumbnail string (optional) - URL of the video thumnail image
• link string (optional)
Hidden:
• EventCommentGUID string
• Deleted boolean
• LastUpdatedByFullName string
• CreatedByFullName string
     
     */
    /// <summary>
    /// A model for a comment to an event
    /// </summary>
    public class CommentModel: BaseModel
    {
        /*included*/
        public string text { get; set; }
        public int Event  { get; set; }
        public int user { get; set; }
        /// <summary>
        /// URL of the full sized image
        /// </summary>
        public string image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imagePreview { get; set; }
        /// <summary>
        /// URL of the video (may be hosted by a third party, how do we handle different formats?)
        /// </summary>
        public string video { get; set; }
        /// <summary>
        /// URL of the video thumnail image
        /// </summary>
        public string videoThumbnail { get; set; }
        public string link { get; set; }

        /*hidden*/
        public string LastUpdatedByFullName { get; set; }
        public string CreatedByFullName { get; set; }

        /// <summary>
        /// Attempts to create a model using incoming parameters. If parameters are valid, the model is created, otherwise
        /// null is returned, and error parameter describes what went wrong
        /// </summary>
        /// <param name="text"></param>
        /// <param name="Event"></param>
        /// <param name="user"></param>
        /// <param name="image"></param>
        /// <param name="imagePreview"></param>
        /// <param name="video"></param>
        /// <param name="videoThumbnail"></param>
        /// <param name="link"></param>
        /// <param name="CreatedByFullName"></param>
        /// <param name="LastUpdatedByFullName"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static CommentModel CreateCommentModel(string text, int? Event, int? user, string image, 
            string imagePreview, string video, string videoThumbnail, string link,
            string CreatedByFullName, string LastUpdatedByFullName, out string error)
        {
            error = null;
            //let's check parameters

            //copying logic from viewEvent.aspx.cs
            if (text.Contains("<script"))
                error = "text-attribute-invalid";
            else if (!Event.HasValue)
                error = "event-attribute-required";
            else if (!user.HasValue)
                error = "user-attribute-required";

            if (!string.IsNullOrEmpty(error))
                return null;

            CommentModel model = new CommentModel { 
                created = DateTime.Now,
                updated = DateTime.Now,

                text = text,
                Event = Event.Value,
                user = user.Value,
                image = image,
                imagePreview = imagePreview,
                video = video,
                videoThumbnail = videoThumbnail,
                link= link,
                CreatedByFullName = CreatedByFullName,
                LastUpdatedByFullName = LastUpdatedByFullName

             
            };
            return model;
        }

        /// <summary>
        /// creates a businss object using a model
        /// </summary>
        /// <param name="model">a model with comment details</param>
        /// <returns>a business object for the comment</returns>
        public static Sedogo.BusinessObjects.SedogoEventComment CreateCommentBO(CommentModel model)
        {
            Sedogo.BusinessObjects.SedogoEventComment commentBO = new Sedogo.BusinessObjects.SedogoEventComment(model.CreatedByFullName);
            if(!string.IsNullOrEmpty(model.text)) commentBO.commentText =  model.text;
            commentBO.eventID = model.Event;
            commentBO.postedByUserID = model.user;
            if(!string.IsNullOrEmpty(model.image) ) commentBO.eventImageFilename =  model.image;
            if(!string.IsNullOrEmpty(model.imagePreview) ) commentBO.eventImagePreview =  model.imagePreview;
            if(!string.IsNullOrEmpty(model.video)) commentBO.eventVideoFilename =   model.video;
            if(!string.IsNullOrEmpty(model.videoThumbnail)) commentBO.eventVideoLink =  model.videoThumbnail;
            if(!string.IsNullOrEmpty(model.link) ) commentBO.eventLink =  model.link;
            return commentBO;
        }

              

        public override Dictionary<string, object> GetDetails()
        {
            Dictionary<string, object> rv = InitializeDictionary();
            if (!string.IsNullOrEmpty(text)) rv.Add("text", text);
            rv.Add("event", Event);
            rv.Add("user", user);
            if (!string.IsNullOrEmpty(image)) rv.Add("image", image);
            if (!string.IsNullOrEmpty(imagePreview)) rv.Add("imagePreview", imagePreview);
            if (!string.IsNullOrEmpty(video)) rv.Add("video", video);
            if (!string.IsNullOrEmpty(videoThumbnail)) rv.Add("videoThumbnail", videoThumbnail);
            if (!string.IsNullOrEmpty(link)) rv.Add("link", link);

            return rv;

        }
    }
}
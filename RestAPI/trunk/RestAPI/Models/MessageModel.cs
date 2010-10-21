using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sedogo.BusinessObjects;

namespace RestAPI.Models
{
    /*
     Message is a basic type of message, sent from one User to another, or from a user to an
Event. Sending a Message to an Event results in a the message being delivered to the
User (admin) associated with the event.
Included:
• id int
• created date
• updated date
• message string (optional)
• event int (optional)
• user int
• author int
• read boolean
Hidden:
• MessageGUID string
• Deleted boolean
• CreatedByFullName string
• LastUpdatedByFullName string
     */
    /// <summary>
    /// A model for messages
    /// </summary>
    public class MessageModel: BaseModel
    {
        /*included*/
        public string message{ get; set; }
        public int? Event{ get; set; }
        public int user{ get; set; }
        public int author{ get; set; }
        public bool read{ get; set; }
        
        /*hidden*/
        public string CreatedByFullName{ get; set; }
        public string LastUpdatedByFullName{ get; set; }
        
        
        //public MessageModel
        
        public override Dictionary<string, object> GetDetails()
        {
            Dictionary<string, object> rv = InitializeDictionary();
            if(!string.IsNullOrEmpty(message)) rv.Add("message", message);
            if (Event.HasValue) rv.Add("event", Event.Value);
            rv.Add("user", user);
            rv.Add("author", author);
            rv.Add("read", read);
            return rv;

        }


        /// <summary>
        /// Attempts to create a message model using input parameters. If the attempt is unsuccessful, the output error parameter
        /// contains the error message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="Event"></param>
        /// <param name="user"></param>
        /// <param name="author"></param>
        /// <param name="read"></param>
        /// <param name="CreatedByFullName"></param>
        /// <param name="LastUpdatedByFullName"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static MessageModel CreateMessageModel(string message, int? Event, int? user, int? author, bool? read,
            string CreatedByFullName, string LastUpdatedByFullName, out string error)
        {
            error = null;
            if (!user.HasValue)
                error = "user-attribute-required";
            else if (!author.HasValue)
                error = "author-attribute-required";
            else if (!read.HasValue)
                error = "read-attribute-required";

            if (!string.IsNullOrEmpty(error))
                return null;

            MessageModel model = new MessageModel { 
                created = DateTime.Now,
                updated = DateTime.Now,
                message = message,
                Event =Event,
                user = user.Value,
                author = author.Value,
                read = read.Value,
                CreatedByFullName = CreatedByFullName,
                LastUpdatedByFullName = LastUpdatedByFullName
            };
            return model;
        }



        /// <summary>
        /// Creates a business object using a model as a source
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Sedogo.BusinessObjects.Message CreateMessageBO(MessageModel model)
        {
            var msg = new Sedogo.BusinessObjects.Message(model.CreatedByFullName);
            
            if(!string.IsNullOrEmpty(model.message)) msg.messageText = model.message;
            if(model.Event.HasValue) msg.eventID = model.Event.Value;
            msg.userID = model.user;
            msg.postedByUserID = model.author;
            msg.messageRead = model.read;

            return msg;
        }
    }


}
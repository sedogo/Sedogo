using System;
using System.Collections.Generic;

namespace RestAPI.Models
{
    /*
     Invite
Invite is a type of message signifying a request to join an Event. Sending an invite
results in a message being delivered to the User (admin) associated with the event.
Included:
• id int
• created date
• updated date
• text string (optional)
• event int
• user int (optional)
• emailSent boolean
• accepted boolean
• acceptedOn date (optional)
• declined boolean
• declinedOn date (optional)
Write only:
• email string (optional)
Hidden:
• GUID string
• Deleted boolean
• CreatedByFullName string
• LastUpdatedByFullName string
• emailDate date (optional)
     */

    /// <summary>
    /// model of an invite
    /// </summary>
    [Serializable]
    public class InviteModel  : BaseModel
    {
        /*included*/
        public string text { get; set; }
        public int Event { get; set; }
        public int? user { get; set; }
        public bool emailSent { get; set; }
        public bool accepted { get; set; }
        public DateTime? acceptedOn { get; set; }
        public bool declined { get; set; }
        public DateTime? declinedOn { get; set; }
        
        /*hidden*/
        public string CreatedByFullName {get;set;}
        public string LastUpdatedByFullName{get;set;}

        /*write only*/
        public string email{get;set;}

        public override Dictionary<string, object> GetDetails()
        {
            Dictionary<string, object> rv = InitializeDictionary();
            if(!string.IsNullOrEmpty(text)) rv.Add("text", text);
            rv.Add("event", Event);
            if (user.HasValue) rv.Add("user", user.Value);
            rv.Add("emailSent", emailSent);
            rv.Add("accepted", accepted);
            if (acceptedOn.HasValue) rv.Add("acceptedOn", Assistant.ConvertToString(acceptedOn.Value));
            rv.Add("declined", declined);
            if (declinedOn.HasValue) rv.Add("declinedOn", Assistant.ConvertToString(declinedOn.Value));
            return rv;
        }


        /// <summary>
        /// attempts to create an InviteModel using input parameters. If if fails, the output error parameter contains the error description
        /// </summary>
        /// <param name="email"></param>
        /// <param name="text"></param>
        /// <param name="Event"></param>
        /// <param name="user"></param>
        /// <param name="emailSent"></param>
        /// <param name="accepted"></param>
        /// <param name="acceptedOn"></param>
        /// <param name="declined"></param>
        /// <param name="declinedOn"></param>
        /// <param name="CreatedByFullName"></param>
        /// <param name="LastUpdatedByFullName"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static InviteModel CreateInviteModel(string email, string text, int? Event, int? user, bool? emailSent, 
            bool? accepted, DateTime? acceptedOn, bool? declined, DateTime? declinedOn, string CreatedByFullName, string LastUpdatedByFullName, out string error)
        {
            error = null;
            if (!Event.HasValue)
                error = "event-attribute-required";
            else if (!emailSent.HasValue)
                error = "emailSent-attribute-required";
            else if (!accepted.HasValue)
                error = "accepted-attribute-required";
            else if (!declined.HasValue)
                error = "declined-attribute-required";

            if (!string.IsNullOrEmpty(error))
                return null;

            InviteModel model = new InviteModel { 
                created = DateTime.Now,
                updated = DateTime.Now,
                text =text,
                Event = Event.Value,
                user = user,
                emailSent = emailSent.Value,
                email = email,
                accepted = accepted.Value,
                acceptedOn = acceptedOn,
                declined = declined.Value,
                declinedOn = declinedOn  ,
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
        public static Sedogo.BusinessObjects.EventInvite CreateInviteBO(InviteModel model)
        {
            var inviteBO = new Sedogo.BusinessObjects.EventInvite(model.CreatedByFullName);
            if(!string.IsNullOrEmpty(model.text)) inviteBO.inviteAdditionalText = model.text;
            inviteBO.eventID = model.Event;
            if (model.user.HasValue) inviteBO.userID = model.user.Value;
            inviteBO.inviteEmailSent = model.emailSent;
            inviteBO.inviteAccepted = model.accepted;
            if (model.acceptedOn.HasValue) inviteBO.inviteAcceptedDate = model.acceptedOn.Value;
            inviteBO.inviteDeclined = model.declined;
            if (model.declinedOn.HasValue) inviteBO.inviteDeclinedDate = model.declinedOn.Value;
            if (!string.IsNullOrEmpty(model.email)) inviteBO.emailAddress = model.email;

            return inviteBO;
        }
    }
}
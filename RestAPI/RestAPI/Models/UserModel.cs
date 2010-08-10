using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace RestAPI.Models
{
    /*
     User
Included:
• id int
• created date
• updated date
• firstName string (optional)
• lastName string (optional)
• gender string
• image string (optional)
• imageThumbnail string (optional)
• imagePreview string (optional)
• profile string (optional)
• homeTown string (optional)
• birthday date (optional)
• country int
• timezone int
• language int
Write only:
• email string
• password string
     * Hidden:
• GUID string
• LoginEnabled boolean
• UserPassword string (optional)
• FailedLoginCount int
• PasswordExpiryDate date (optional)
• LastLoginDate date (optional)
• EnableSendEmails boolean
• CreatedByFullName string ?
• LastUpdatedByFullName string
     */
    /// <summary>
    /// A class for JSON representation of the User
    /// </summary>
    public class UserModel  : BaseModel
    {
       public string firstName{get;set;}
        public string lastName{get;set;}
        public string gender{get;set;}
        public string image{get;set;}
        public string imageThumbnail{get;set;}
        public string imagePreview{get;set;}
        public string profile{get;set;}
        public string homeTown{get;set;}
        public DateTime? birthday{get;set;}
        public int country{get;set;}
        public int timezone{get;set;}
        public int language{get;set;}

        //write only
        public string email { get; set; }
        public string password { get; set; }

        //hidden
        public string CreatedByFullName { get; set; }
        public string LastUpdatedByFullName { get; set; }

        


        public override Dictionary<string, object> GetDetails()
        {
            Dictionary<string, object> rv = InitializeDictionary();
            if (!string.IsNullOrEmpty(firstName))
                rv.Add("firstName", firstName);
            if (!string.IsNullOrEmpty(lastName))
                rv.Add("lastName", lastName);
            rv.Add("gender", gender);
            if (!string.IsNullOrEmpty(image))
                rv.Add("image", image);
            if (!string.IsNullOrEmpty(imageThumbnail))
                rv.Add("imageThumbnail", imageThumbnail);
            if (!string.IsNullOrEmpty(imagePreview))
                rv.Add("imagePreview", imagePreview);
            if (!string.IsNullOrEmpty(profile))
                rv.Add("profile", profile);
            if (!string.IsNullOrEmpty(homeTown))
                rv.Add("homeTown", homeTown);
            if (birthday.HasValue)
                rv.Add("birthday", Assistant.ConvertDateTime(birthday.Value));
            rv.Add("country", country);
            rv.Add("timezone", timezone);
            rv.Add("language", language);

            return rv;
        }


        /// <summary>
        /// checks if all required parameters are present, and if so, creates a user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="gender"></param>
        /// <param name="homeTown"></param>
        /// <param name="birthday"></param>
        /// <param name="country"></param>
        /// <param name="language"></param>
        /// <param name="timezone"></param>
        /// <param name="profile"></param>
        /// <param name="image"></param>
        /// <param name="imageThumbnail"></param>
        /// <param name="imagePreview"></param>
        /// <param name="createdByFullName"></param>
        /// <param name="updatedByFullName"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static UserModel CreateUserModel(string email, string password, string firstName, string lastName,
            string gender, string homeTown, string birthday, int? country, int? language, int? timezone, string profile,
            string image, string imageThumbnail, string imagePreview, string createdByFullName, string updatedByFullName, out string error)
        {
            error = null;
            //let's check that all necessary parameters have values
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password) && string.IsNullOrEmpty(gender) &&
                !country.HasValue && !language.HasValue && !timezone.HasValue)
                error = "body-required";
            //now let's check individual parameters
            else if (string.IsNullOrEmpty(email))
                error = "email-attribute-required";
            else if(!Regex.IsMatch(email,@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase))
                error = "email-does-not-match-regex";
            else if (string.IsNullOrEmpty(password))
                error = "password-attribute-required";
            else if (string.IsNullOrEmpty(gender))
                error = "gender-attribute-required";
            else if (!country.HasValue)
                error = "country-attibute-required";
            else if (!language.HasValue)
                error = "language-attibute-required";
            else if (!timezone.HasValue)
                error = "timezone-attibute-required";

            //now exclusively for images
            else if (!   (string.IsNullOrEmpty(image) && string.IsNullOrEmpty(imageThumbnail) &&
                string.IsNullOrEmpty(imagePreview)))
            {
                if (string.IsNullOrEmpty(image))
                    error = "image-attribute-required";
                else if (string.IsNullOrEmpty(imageThumbnail))
                    error = "image-thumbnail-attribute-required";
                else if (string.IsNullOrEmpty(imagePreview))
                    error = "image-preview-attribute-required";
            }
            
            //now explicitlty for the birthday
            DateTime? Birthday = null;
            if(!string.IsNullOrEmpty(birthday))
            {
                try
                {
                    Birthday = Assistant.ConvertDateTime(birthday);
                }
                catch(Exception ex)
                {   
                    error = "birthday-attribute-invalid";
                }

            }



            if (!string.IsNullOrEmpty(error))
                return null;

            UserModel user = new UserModel
            {
                created = DateTime.Now,
                updated = DateTime.Now,
                
                
                firstName = string.IsNullOrEmpty(firstName)?null:firstName,
                lastName = string.IsNullOrEmpty(lastName)?null:lastName,
                gender = string.IsNullOrEmpty(gender)?null:gender,
                homeTown = string.IsNullOrEmpty(homeTown)?null:homeTown,
                birthday = Birthday,

                country = country.Value,
                language = language.Value,
                timezone = timezone.Value,

                profile = string.IsNullOrEmpty(profile)?null:profile,
                image = string.IsNullOrEmpty(image)?null:image,
                imageThumbnail = string.IsNullOrEmpty(imageThumbnail)?null:imageThumbnail,
                imagePreview = string.IsNullOrEmpty(imagePreview)?null:imagePreview,

                email = email,
                password = password,
                
                CreatedByFullName = createdByFullName,
                LastUpdatedByFullName = updatedByFullName,
            };

            return user;

        }

        /// <summary>
        /// Creates a business object for user
        /// </summary>
        /// <param name="user">a basic set of user's properties</param>
        /// <returns></returns>
        public static Sedogo.BusinessObjects.SedogoUser CreateUserBO(UserModel user)
        {
            Sedogo.BusinessObjects.SedogoUser dbUser = new Sedogo.BusinessObjects.SedogoUser(user.CreatedByFullName);
            if(!string.IsNullOrEmpty(user.firstName)) dbUser.firstName = user.firstName;
            if(!string.IsNullOrEmpty(user.lastName)) dbUser.lastName = user.lastName;
            dbUser.gender = user.gender;
            if (user.birthday.HasValue) dbUser.birthday = user.birthday.Value;
            
            dbUser.languageID = user.language;
            dbUser.countryID = user.country;
            dbUser.timezoneID = user.timezone;

            dbUser.emailAddress = user.email;
            
            if(!string.IsNullOrEmpty(user.profile)) dbUser.profileText = user.profile;
            if(!string.IsNullOrEmpty(user.image)) dbUser.profilePicFilename = user.image;
            if(!string.IsNullOrEmpty(user.imageThumbnail)) dbUser.profilePicThumbnail = user.imageThumbnail;
            if(!string.IsNullOrEmpty(user.imagePreview)) dbUser.profilePicPreview = user.imagePreview;

            if (!string.IsNullOrEmpty(user.homeTown)) dbUser.homeTown = user.homeTown; 

            return dbUser;

        }
    }
}
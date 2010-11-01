//===============================================================
// Filename: Users.cs
// Date: 19/08/09
// --------------------------------------------------------------
// Description:
//   Users class
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 19/08/09
// Revision history:
//===============================================================

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace Sedogo.BusinessObjects
{
    public enum loginResults
    {
        loginSuccess,
        loginFailed,
        loginNotActivated,
        passwordExpired,
        loginAccountLocked
    }
    public enum passwordChangeResults
    {
        passwordOK = 0,
        passwordTooShort = 1,
        passwordTooLong = 2,
        passwordMixedCase = 4,
        passwordNeedsDigit = 8,
        passwordSpecialCharacter = 16,
        passwordReused = 32
    }

    //===============================================================
    // Class: SedogoUser
    //===============================================================
    public class SedogoUser
    {
        private int         m_userID = -1;
        private long        m_facebookUserID = -1;
        private string      m_GUID = "";
        private string      m_emailAddress = "";
        private string      m_firstName = "";
        private string      m_lastName = "";
        private string      m_homeTown = "";
        private DateTime    m_birthday = DateTime.MinValue;
        private string      m_profilePicFilename = "";
        private string      m_profilePicThumbnail = "";
        private string      m_profilePicPreview = "";
        private int         m_avatarNumber = -1;
        private string      m_gender = "U";
        private Boolean     m_deleted = false;
        private DateTime    m_deletedDate = DateTime.MinValue;
        private int         m_countryID = -1;
        private int         m_languageID = -1;
        private int         m_timezoneID = -1;
        private Boolean     m_loginEnabled = false;
        private string      m_userPassword = "";
        private int         m_failedLoginCount = 0;
        private string      m_profileText = "";
        private DateTime    m_passwordExpiryDate = DateTime.MinValue;
        private DateTime    m_lastLoginDate = DateTime.MinValue;
        private Boolean     m_enableSendEmails = true;
        private Boolean     m_firstLogin = false;
        private DateTime    m_createdDate = DateTime.MinValue;
        private string      m_createdByFullName = "";
        private DateTime    m_lastUpdatedDate = DateTime.MinValue;
        private string      m_lastUpdatedByFullName = "";

        private string      m_loggedInUser = "";

        public int userID
        {
            get { return m_userID; }
        }
        public long facebookUserID
        {
            get { return m_facebookUserID; }
            set { m_facebookUserID = value; }
        }
        public string GUID
        {
            get { return m_GUID; }
        }
        public string emailAddress
        {
            get { return m_emailAddress; }
            set { m_emailAddress = value; }
        }
        public string firstName
        {
            get { return m_firstName; }
            set { m_firstName = value; }
        }
        public string lastName
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }
        public string fullName
        {
            get { return m_firstName + " " + m_lastName; }
        }
        public string homeTown
        {
            get { return m_homeTown; }
            set { m_homeTown = value; }
        }
        public DateTime birthday
        {
            get { return m_birthday; }
            set { m_birthday = value; }
        }
        public string profilePicFilename
        {
            get { return m_profilePicFilename; }
            set { m_profilePicFilename = value; }
        }
        public string profilePicThumbnail
        {
            get { return m_profilePicThumbnail; }
            set { m_profilePicThumbnail = value; }
        }
        public string profilePicPreview
        {
            get { return m_profilePicPreview; }
            set { m_profilePicPreview = value; }
        }
        public int avatarNumber
        {
            get { return m_avatarNumber; }
            set { m_avatarNumber = value; }
        }
        public string gender
        {
            get { return m_gender; }
            set { m_gender = value; }
        }
        public Boolean deleted
        {
            get { return m_deleted; }
        }
        public DateTime deletedDate
        {
            get { return m_deletedDate; }
        }
        public int countryID
        {
            get { return m_countryID; }
            set { m_countryID = value; }
        }
        public int languageID
        {
            get { return m_languageID; }
            set { m_languageID = value; }
        }
        public int timezoneID
        {
            get { return m_timezoneID; }
            set { m_timezoneID = value; }
        }
        public Boolean loginEnabled
        {
            get { return m_loginEnabled; }
            set { m_loginEnabled = value; }
        }
        public Boolean enableSendEmails
        {
            get { return m_enableSendEmails; }
            set { m_enableSendEmails = value; }
        }
        public Boolean firstLogin
        {
            get { return m_firstLogin; }
            set { m_firstLogin = value; }
        }
        public string userPassword
        {
            get { return m_userPassword; }
        }
        public int failedLoginCount
        {
            get { return m_failedLoginCount; }
        }
        public string profileText
        {
            get { return m_profileText; }
            set { m_profileText = value; }
        }
        public DateTime passwordExpiryDate
        {
            get { return m_passwordExpiryDate; }
        }
        public DateTime lastLoginDate
        {
            get { return m_lastLoginDate; }
        }
        public DateTime createdDate
        {
            get { return m_createdDate; }
        }
        public string createdByFullName
        {
            get { return m_createdByFullName; }
        }
        public DateTime lastUpdatedDate
        {
            get { return m_lastUpdatedDate; }
        }
        public string lastUpdatedByFullName
        {
            get { return m_lastUpdatedByFullName; }
        }
    
        //===============================================================
        // Function: SedogoUser (Constructor)
        //===============================================================
        public SedogoUser(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: SedogoUser (Constructor)
        //===============================================================
        public SedogoUser(string loggedInUser, int userID)
        {
            m_loggedInUser = loggedInUser;
            m_userID = userID;

            ReadUserDetails();
        }

        //===============================================================
        // Function: ReadUserDetails
        //===============================================================
        public void ReadUserDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectUserDetails";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@UserID";
                param.Value = m_userID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                if (!rdr.IsDBNull(rdr.GetOrdinal("GUID")))
                {
                    m_GUID = (string)rdr["GUID"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EmailAddress")))
                {
                    m_emailAddress = (string)rdr["EmailAddress"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("FirstName")))
                {
                    m_firstName = (string)rdr["FirstName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LastName")))
                {
                    m_lastName = (string)rdr["LastName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("HomeTown")))
                {
                    m_homeTown = (string)rdr["HomeTown"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Birthday")))
                {
                    m_birthday = (DateTime)rdr["Birthday"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicFilename")))
                {
                    m_profilePicFilename = (string)rdr["ProfilePicFilename"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicThumbnail")))
                {
                    m_profilePicThumbnail = (string)rdr["ProfilePicThumbnail"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicPreview")))
                {
                    m_profilePicPreview = (string)rdr["ProfilePicPreview"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("AvatarNumber")))
                {
                    m_avatarNumber = int.Parse(rdr["AvatarNumber"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Gender")))
                {
                    m_gender = (string)rdr["Gender"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Deleted")))
                {
                    m_deleted = (Boolean)rdr["Deleted"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("DeletedDate")))
                {
                    m_deletedDate = (DateTime)rdr["DeletedDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("CountryID")))
                {
                    m_countryID = int.Parse(rdr["CountryID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LanguageID")))
                {
                    m_languageID = int.Parse(rdr["LanguageID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("TimezoneID")))
                {
                    m_timezoneID = int.Parse(rdr["TimezoneID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfileText")))
                {
                    m_profileText = (string)rdr["ProfileText"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LoginEnabled")))
                {
                    m_loginEnabled = (Boolean)rdr["LoginEnabled"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EnableSendEmails")))
                {
                    m_enableSendEmails = (Boolean)rdr["EnableSendEmails"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("UserPassword")))
                {
                    m_userPassword = (string)rdr["UserPassword"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("FailedLoginCount")))
                {
                    m_failedLoginCount = int.Parse(rdr["FailedLoginCount"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("PasswordExpiryDate")))
                {
                    m_passwordExpiryDate = (DateTime)rdr["PasswordExpiryDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LastLoginDate")))
                {
                    m_lastLoginDate = (DateTime)rdr["LastLoginDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedDate")))
                {
                    m_createdDate = (DateTime)rdr["CreatedDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedByFullName")))
                {
                    m_createdByFullName = (string)rdr["CreatedByFullName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LastUpdatedDate")))
                {
                    m_lastUpdatedDate = (DateTime)rdr["LastUpdatedDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LastUpdatedByFullName")))
                {
                    m_lastUpdatedByFullName = (string)rdr["LastUpdatedByFullName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("FacebookUserID")))
                {
                    m_facebookUserID = long.Parse(rdr["FacebookUserID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("FirstLogin")))
                {
                    m_firstLogin = (Boolean)rdr["FirstLogin"];
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "ReadUserDetails", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: ReadUserDetailsByFacebookID
        //===============================================================
        public bool ReadUserDetailsByFacebookUserID(long facebookUserId)
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectUserDetailsByFacebookID";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@FacebookUserID";
                param.Value = facebookUserId;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (!rdr.HasRows)
                    return false;
                rdr.Read();
                if (!rdr.IsDBNull(rdr.GetOrdinal("UserID")))
                {
                    m_userID = int.Parse(rdr["UserID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("GUID")))
                {
                    m_GUID = (string)rdr["GUID"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EmailAddress")))
                {
                    m_emailAddress = (string)rdr["EmailAddress"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("FirstName")))
                {
                    m_firstName = (string)rdr["FirstName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LastName")))
                {
                    m_lastName = (string)rdr["LastName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("HomeTown")))
                {
                    m_homeTown = (string)rdr["HomeTown"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Birthday")))
                {
                    m_birthday = (DateTime)rdr["Birthday"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicFilename")))
                {
                    m_profilePicFilename = (string)rdr["ProfilePicFilename"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicThumbnail")))
                {
                    m_profilePicThumbnail = (string)rdr["ProfilePicThumbnail"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicPreview")))
                {
                    m_profilePicPreview = (string)rdr["ProfilePicPreview"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("AvatarNumber")))
                {
                    m_avatarNumber = int.Parse(rdr["AvatarNumber"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Gender")))
                {
                    m_gender = (string)rdr["Gender"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Deleted")))
                {
                    m_deleted = (Boolean)rdr["Deleted"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("DeletedDate")))
                {
                    m_deletedDate = (DateTime)rdr["DeletedDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("CountryID")))
                {
                    m_countryID = int.Parse(rdr["CountryID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LanguageID")))
                {
                    m_languageID = int.Parse(rdr["LanguageID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("TimezoneID")))
                {
                    m_timezoneID = int.Parse(rdr["TimezoneID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("ProfileText")))
                {
                    m_profileText = (string)rdr["ProfileText"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LoginEnabled")))
                {
                    m_loginEnabled = (Boolean)rdr["LoginEnabled"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EnableSendEmails")))
                {
                    m_enableSendEmails = (Boolean)rdr["EnableSendEmails"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("UserPassword")))
                {
                    m_userPassword = (string)rdr["UserPassword"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("FailedLoginCount")))
                {
                    m_failedLoginCount = int.Parse(rdr["FailedLoginCount"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("PasswordExpiryDate")))
                {
                    m_passwordExpiryDate = (DateTime)rdr["PasswordExpiryDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LastLoginDate")))
                {
                    m_lastLoginDate = (DateTime)rdr["LastLoginDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedDate")))
                {
                    m_createdDate = (DateTime)rdr["CreatedDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedByFullName")))
                {
                    m_createdByFullName = (string)rdr["CreatedByFullName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LastUpdatedDate")))
                {
                    m_lastUpdatedDate = (DateTime)rdr["LastUpdatedDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LastUpdatedByFullName")))
                {
                    m_lastUpdatedByFullName = (string)rdr["LastUpdatedByFullName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("FacebookUserID")))
                {
                    m_facebookUserID = long.Parse(rdr["FacebookUserID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("FirstLogin")))
                {
                    m_firstLogin = (Boolean)rdr["FirstLogin"];
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "ReadUserDetailsByFacebookUserID", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return true;
        }

        //===============================================================
        // Function: Add
        //===============================================================
        public void Add()
        {
            m_GUID = System.Guid.NewGuid().ToString();

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spAddUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@GUID", SqlDbType.NVarChar, 50).Value = m_GUID;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 200).Value = m_emailAddress;
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 200).Value = m_firstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 200).Value = m_lastName;
                cmd.Parameters.Add("@HomeTown", SqlDbType.NVarChar, 200).Value = m_homeTown;
                if (m_birthday == DateTime.MinValue)
                {
                    cmd.Parameters.Add("@Birthday", SqlDbType.DateTime).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Birthday", SqlDbType.DateTime).Value = m_birthday;
                }
                cmd.Parameters.Add("@Gender", SqlDbType.NChar, 1).Value = m_gender;
                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = m_countryID;
                cmd.Parameters.Add("@LanguageID", SqlDbType.Int).Value = m_languageID;
                cmd.Parameters.Add("@TimezoneID", SqlDbType.Int).Value = m_timezoneID;
                cmd.Parameters.Add("@AvatarNumber", SqlDbType.Int).Value = m_avatarNumber;
                cmd.Parameters.Add("@ProfileText", SqlDbType.NVarChar, 200).Value = m_profileText;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@CreatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.Parameters.Add("@FacebookUserID", SqlDbType.BigInt).Value = (m_facebookUserID == -1 ? (object)DBNull.Value : (object)m_facebookUserID);

                SqlParameter paramUserID = cmd.CreateParameter();
                paramUserID.ParameterName = "@UserID";
                paramUserID.SqlDbType = SqlDbType.Int;
                paramUserID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramUserID);

                cmd.ExecuteNonQuery();
                m_userID = (int)paramUserID.Value;

                //ReadUserDetails();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "Add", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: Update
        //===============================================================
        public void Update()
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = m_userID;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 200).Value = m_emailAddress;
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 200).Value = m_firstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 200).Value = m_lastName;
                cmd.Parameters.Add("@HomeTown", SqlDbType.NVarChar, 200).Value = m_homeTown;
                if (m_birthday == DateTime.MinValue)
                {
                    cmd.Parameters.Add("@Birthday", SqlDbType.DateTime).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Birthday", SqlDbType.DateTime).Value = m_birthday;
                }
                cmd.Parameters.Add("@Gender", SqlDbType.NChar, 1).Value = m_gender;
                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = m_countryID;
                cmd.Parameters.Add("@LanguageID", SqlDbType.Int).Value = m_languageID;
                cmd.Parameters.Add("@TimezoneID", SqlDbType.Int).Value = m_timezoneID;
                cmd.Parameters.Add("@LoginEnabled", SqlDbType.Bit).Value = m_loginEnabled;
                cmd.Parameters.Add("@EnableSendEmails", SqlDbType.Bit).Value = m_enableSendEmails;
                cmd.Parameters.Add("@AvatarNumber", SqlDbType.Int).Value = m_avatarNumber;
                cmd.Parameters.Add("@ProfileText", SqlDbType.NVarChar, 200).Value = m_profileText;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.Parameters.Add("@FacebookUserID", SqlDbType.BigInt).Value = (m_facebookUserID == -1 ? (object)DBNull.Value : (object)m_facebookUserID);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "Update", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: UpdateFirstLogin
        //===============================================================
        public void UpdateFirstLogin()
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateUserFirstLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = m_userID;
                cmd.Parameters.Add("@FirstLogin", SqlDbType.Bit).Value = true;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "UpdateFirstLogin", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: UpdateUserProfilePic
        //===============================================================
        public void UpdateUserProfilePic()
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateUserProfilePic", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = m_userID;
                cmd.Parameters.Add("@ProfilePicFilename", SqlDbType.NVarChar, 200).Value = m_profilePicFilename;
                cmd.Parameters.Add("@ProfilePicThumbnail", SqlDbType.NVarChar, 200).Value = m_profilePicThumbnail;
                cmd.Parameters.Add("@ProfilePicPreview", SqlDbType.NVarChar, 200).Value = m_profilePicPreview;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "UpdateUserProfilePic", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: Delete
        //===============================================================
        public void Delete()
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spDeleteUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = m_userID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "Delete", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: VerifyLogin
        //===============================================================
        public loginResults VerifyLogin(string emailAddress, string testPassword,
            Boolean passwordIsEncrypted, Boolean recordInLoginHistory, string source)
        {
            loginResults returnValue = loginResults.loginFailed;

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);

            try
            {
                conn.Open();

                // Get contact info
                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spVerifyUserLogin";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@EmailAddress";
                param.Value = emailAddress;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == false)
                {
                    // Update the DB with a failed login attempt (email address not recognised)
                    UpdateLoginHistory(-1, "U", source);     // Unknown user
                    returnValue = loginResults.loginFailed;
                }
                else
                {
                    // Email address exists, now check the password is OK
                    int userID;
                    Boolean loginEnabled = false;
                    string userPassword = "";
                    int failedLoginCount = 0;
                    DateTime passwordExpiryDate = DateTime.MinValue;
                    
                    rdr.Read();
                    userID = (int)rdr["UserID"];
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LoginEnabled")))
                    {
                        loginEnabled = (Boolean)rdr["LoginEnabled"];
                    }
                    if (!rdr.IsDBNull(rdr.GetOrdinal("UserPassword")))
                    {
                        userPassword = (string)rdr["UserPassword"];
                    }
                    if (!rdr.IsDBNull(rdr.GetOrdinal("FailedLoginCount")))
                    {
                        failedLoginCount = (int)rdr["FailedLoginCount"];
                    }
                    if (!rdr.IsDBNull(rdr.GetOrdinal("PasswordExpiryDate")))
                    {
                        passwordExpiryDate = (DateTime)rdr["PasswordExpiryDate"];
                    }
                    rdr.Close();

                    if (DateTime.Compare(passwordExpiryDate, DateTime.Now) > 0)
                    {
                        // Update the DB with a failed login attempt (password expired)
                        if (recordInLoginHistory == true)
                        {
                            UpdateLoginHistory(userID, "E", source);     // Password expired
                        }
                        returnValue = loginResults.passwordExpired;

                        m_userID = userID;
                        ReadUserDetails();
                    }

                    PasswordEncrypt pe = new PasswordEncrypt();
                    string encryptedTestPassword = "";
                    if (passwordIsEncrypted == false)
                    {
                        encryptedTestPassword = pe.EncryptPassword(testPassword);
                    }
                    else
                    {
                        encryptedTestPassword = testPassword;
                    }

                    if ((userPassword != encryptedTestPassword) || (loginEnabled == false))
                    {
                        // Update the DB with a failed login attempt (invalid password)
                        UpdateLoginHistory(userID, "P", source);     // Invalid Password
                        if (loginEnabled == false)
                        {
                            returnValue = loginResults.loginNotActivated;
                        }
                        else
                        {
                            returnValue = loginResults.loginFailed;
                        }
                    }
                    else
                    {
                        // Update the DB with a successful login attempt
                        if (recordInLoginHistory == true)
                        {
                            UpdateLoginHistory(userID, "S", source);      // Success
                        }
                        returnValue = loginResults.loginSuccess;

                        m_userID = userID;
                        ReadUserDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "VerifyLogin", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return returnValue;
        }

        //===============================================================
        // Function: VerifyPassword
        //===============================================================
        public Boolean VerifyPassword(string testPassword)
        {
            Boolean returnStatus = false;

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectUserPassword";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@UserID";
                param.Value = m_userID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows != false)
                {
                    string userPassword;

                    rdr.Read();
                    userPassword = (string)rdr["UserPassword"];
                    rdr.Close();

                    PasswordEncrypt pe = new PasswordEncrypt();
                    string encryptedTestPassword = pe.EncryptPassword(testPassword);

                    // note that passwords are case sensitive
                    if (userPassword == encryptedTestPassword)
                    {
                        returnStatus = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "VerifyPassword", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return returnStatus;
        }

        //===============================================================
        // Function: GetUserIDFromEmailAddress
        //===============================================================
        public static int GetUserIDFromEmailAddress(string emailAddress)
        {
            int userID = -1;

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);

            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetUserIDFromEmailAddress";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@EmailAddress";
                param.Value = emailAddress;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows != false)
                {
                    rdr.Read();
                    userID = (int)rdr["UserID"];
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "GetUserIDFromEmailAddress", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return userID;
        }
        
        //===============================================================
        // Function: GetUserIDFromGUID
        //===============================================================
        public static int GetUserIDFromGUID(string GUID)
        {
            int userID = -1;

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);

            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetUserIDFromGUID";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@GUID";
                param.Value = GUID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows != false)
                {
                    rdr.Read();
                    userID = (int)rdr["UserID"];
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "GetUserIDFromGUID", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return userID;
        }

        //===============================================================
        // Function: UpdateLoginHistory
        //===============================================================
        public void UpdateLoginHistory(int userID, string status, string source)
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                // Update the DB with a failed login attempt (email address not recognised)
                DbCommand cmdLoginHistory = conn.CreateCommand();
                cmdLoginHistory.CommandType = CommandType.StoredProcedure;
                cmdLoginHistory.CommandText = "spInsertUserLoginHistory";
                DbParameter paramLoginHistory1 = cmdLoginHistory.CreateParameter();
                paramLoginHistory1.ParameterName = "@UserID";
                if (userID < 0)
                {
                    paramLoginHistory1.Value = DBNull.Value;
                }
                else
                {
                    paramLoginHistory1.Value = userID;
                }
                cmdLoginHistory.Parameters.Add(paramLoginHistory1);

                DbParameter paramLoginHistory2 = cmdLoginHistory.CreateParameter();
                paramLoginHistory2.ParameterName = "@LoginStatus";
                paramLoginHistory2.Value = status;
                cmdLoginHistory.Parameters.Add(paramLoginHistory2);

                DbParameter paramLoginHistorySource = cmdLoginHistory.CreateParameter();
                paramLoginHistorySource.ParameterName = "@Source";
                paramLoginHistorySource.Value = source;
                cmdLoginHistory.Parameters.Add(paramLoginHistorySource);

                cmdLoginHistory.ExecuteNonQuery();

                if ((status == "L") || (status == "P") && userID > 0)
                {
                    DbCommand cmdIncrementFailedLoginCount = conn.CreateCommand();
                    cmdIncrementFailedLoginCount.CommandType = CommandType.StoredProcedure;
                    cmdIncrementFailedLoginCount.CommandText = "spIncrementFailedLoginCount";
                    DbParameter paramIncrementFailedLoginCount = cmdIncrementFailedLoginCount.CreateParameter();
                    paramIncrementFailedLoginCount.ParameterName = "@UserID";
                    paramIncrementFailedLoginCount.Value = userID;
                    cmdIncrementFailedLoginCount.Parameters.Add(paramIncrementFailedLoginCount);

                    cmdIncrementFailedLoginCount.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "UpdateLoginHistory", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: UpdatePassword
        //===============================================================
        public void UpdatePassword(string newPassword)
        {
            PasswordEncrypt pe = new PasswordEncrypt();
            string encryptedPassword = pe.EncryptPassword(newPassword);

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                // Update users password
                SqlCommand cmd = new SqlCommand("spUpdateUserPassword", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = m_userID;
                cmd.Parameters.Add("@UserPassword", SqlDbType.NVarChar, 50).Value = encryptedPassword;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "UpdatePassword", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: ResetUserPassword
        //===============================================================
        public Boolean ResetUserPassword(string emailAddress, ref string newPassword)
        {
            // Lookup the Contact ID of the supplied email address
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spVerifyUserLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 200).Value = emailAddress;
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == false)
                {
                    // Email address supplied not found
                    return false;
                }
                else
                {
                    // Email address exists, now check the password is OK
                    int userID;
                    Boolean loginEnabled;

                    rdr.Read();
                    userID = (int)rdr["UserID"];
                    loginEnabled = (Boolean)rdr["LoginEnabled"];
                    rdr.Close();

                    m_userID = userID;
                    ReadUserDetails();
                    GenerateNewPassword(ref newPassword);
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "ResetUserPassword", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return true;
        }

        //===============================================================
        // Function: GenerateNewPassword
        //===============================================================
        public Boolean GenerateNewPassword(ref string newPassword)
        {
            // Regardless of the password rules, you will always get a password that obeys the
            // strictest security policy and is of the form XX12xx34!
            // where X is a random upper case character
            // x is a random lowercase character
            // 1234 are random numbers
            // ! is a random punctuation character from the following: !$%()@#-=+:<>.,/
            Random random = new Random();

            int num1 = random.Next(10);
            int num2 = random.Next(10);
            int num3 = random.Next(10);
            int num4 = random.Next(10);
            string c1 = Convert.ToChar(random.Next(97, 122)).ToString().ToUpper();
            string c2 = Convert.ToChar(random.Next(97, 122)).ToString().ToUpper();
            string c3 = Convert.ToChar(random.Next(97, 122)).ToString();
            string c4 = Convert.ToChar(random.Next(97, 122)).ToString();
            string c5 = Convert.ToChar(random.Next(97, 122)).ToString();
            //string p1 = Convert.ToChar(random.Next(58, 64)).ToString();

            newPassword = c1 + c2 + c3 + num1 + num2 + c4 + c5 + num3 + num4;

            // Save the new password to the DB
            UpdatePassword(newPassword);

            return true;
        }

        //===============================================================
        // Function: CheckChangePassword
        //
        // This function checks the password is valid for this contact
        // It does NOT change the password in the database
        // The following rules can be checked depending on system settings
        // - Min password length
        // - Max password length
        // - Mixed case
        // - Force alphanumeric (password must have letters and numbers)
        // - Force non-alphanumerix (password must have punctuation characters in it)
        // - Password history check, cannot re-use any of the last x passwords
        //===============================================================
        public Boolean CheckChangePassword(string password, out Byte reason)
        {
            // passwordChangeResults { passwordOK, passwordTooShort, passwordTooLong, passwordMixedCase,
            // passwordNeedsDigit, passwordSpecialCharacter, passwordReused }
            Boolean returnStatus = true;
            reason = 0;

            string pwdCheckMinLengthEnabled = "N";
            int pwdCheckMinLength = 1;
            string pwdCheckMaxLengthEnabled = "N";
            int pwdCheckMaxLength = 999;
            string pwdCheckMixedCaseEnabled = "N";
            string pwdCheckNeedsDigitEnabled = "N";
            string pwdCheckSpecialCharacterEnabled = "N";
            string pwdCheckNeedsReusedEnabled = "N";
            int pwdCheckReusedNumber = 1;

            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";
            string allChars = lower + upper + digits;

            try
            {
                GlobalData globalData = new GlobalData(m_loggedInUser);
                pwdCheckMinLengthEnabled = globalData.GetStringValue("PwdCheckMinLengthEnabled");
                pwdCheckMinLength = globalData.GetIntegerValue("PwdCheckMinLength");
                pwdCheckMaxLengthEnabled = globalData.GetStringValue("PwdCheckMaxLengthEnabled");
                pwdCheckMaxLength = globalData.GetIntegerValue("PwdCheckMaxLength");
                pwdCheckMixedCaseEnabled = globalData.GetStringValue("PwdCheckMixedCaseEnabled");
                pwdCheckNeedsDigitEnabled = globalData.GetStringValue("PwdCheckNeedsDigitEnabled");
                pwdCheckSpecialCharacterEnabled = globalData.GetStringValue("PwdCheckSpecialCharacterEnabled");
                pwdCheckNeedsReusedEnabled = globalData.GetStringValue("PwdCheckNeedsReusedEnabled");
                pwdCheckReusedNumber = globalData.GetIntegerValue("PwdCheckReusedNumber");
            }
            catch (Exception)
            {
                throw (new Exception("Error getting global data values for password checks"));
            }

            if (pwdCheckMinLengthEnabled == "Y")
            {
                if (password.Length < pwdCheckMinLength)
                {
                    reason = reason |= (int)passwordChangeResults.passwordTooShort;
                    returnStatus = false;
                }
            }
            if (pwdCheckMaxLengthEnabled == "Y")
            {
                if (password.Length > pwdCheckMaxLength)
                {
                    reason = reason |= (int)passwordChangeResults.passwordTooLong;
                    returnStatus = false;
                }
            }
            if (pwdCheckMixedCaseEnabled == "Y")
            {
                if (!((password.IndexOfAny(lower.ToCharArray()) >= 0) && (password.IndexOfAny(upper.ToCharArray()) >= 0)))
                {
                    reason = reason |= (int)passwordChangeResults.passwordMixedCase;
                    returnStatus = false;
                }
            }
            if (pwdCheckNeedsDigitEnabled == "Y")
            {
                if (!(password.IndexOfAny(digits.ToCharArray()) >= 0))
                {
                    reason = reason |= (int)passwordChangeResults.passwordNeedsDigit;
                    returnStatus = false;
                }
            }
            if (pwdCheckSpecialCharacterEnabled == "Y")
            {
                if (!(password.Trim(allChars.ToCharArray()).Length > 0))
                {
                    reason = reason |= (int)passwordChangeResults.passwordSpecialCharacter;
                    returnStatus = false;
                }
            }
            if (pwdCheckNeedsReusedEnabled == "Y")
            {
                // pwdCheckReusedNumber
            }

            return returnStatus;
        }

        /// <summary>
        /// loads details from facebook
        /// </summary>
        /// <param name="access_token">facebook access token</param>
        /// <returns>a json object</returns>
        public static JObject GetFacebookUserDetails(string access_token)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://graph.facebook.com/me?access_token=" + HttpUtility.UrlEncode(access_token));
                request.Headers[HttpRequestHeader.AcceptLanguage] = "en-us,en;";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Encoding enc = Encoding.UTF8;
                try
                {
                    enc = Encoding.GetEncoding(response.CharacterSet);
                }
                catch (Exception ex)
                {

                }
                string body = (new StreamReader(response.GetResponseStream(), enc)).ReadToEnd();
                JObject fbuser = JObject.Parse(body);
                return fbuser;
            }
            catch (Exception ex)
            {
                Sedogo.BusinessObjects.ErrorLog errorLog = new Sedogo.BusinessObjects.ErrorLog();
                errorLog.WriteLog("User", "GetFacebookUserDetails", ex.Message,
                    Sedogo.BusinessObjects.logMessageLevel.errorMessage);

            }
            return null;

        }
    }

    //===============================================================
    // Class: PasswordEncrypt
    //===============================================================
    public class PasswordEncrypt
    {
        private string m_encryptionMethod;

        //===============================================================
        // Function: PasswordEncrypt (Constructor)
        //===============================================================
        public PasswordEncrypt()
        {
            m_encryptionMethod = "SHA256";
        }

        //===============================================================
        // Function: PasswordEncrypt (Constructor)
        //===============================================================
        public PasswordEncrypt(string encryptionMethod)
        {
            m_encryptionMethod = encryptionMethod;
        }

        //===============================================================
        // Function: EncryptPassword
        //===============================================================
        public string EncryptPassword(string password)
        {
            string encryptedPassword = "";
            if (m_encryptionMethod == "SHA256")
            {
                encryptedPassword = SHA256HashPassword(password);
            }
            if (m_encryptionMethod == "SHA1")
            {
                FormsAuthentication.HashPasswordForStoringInConfigFile(encryptedPassword, "SHA1");
            }
            if (m_encryptionMethod == "MD5")
            {
                FormsAuthentication.HashPasswordForStoringInConfigFile(encryptedPassword, "MD5");
            }
            return encryptedPassword;
        }

        //===============================================================
        // Function: SHA256HashPassword
        //===============================================================
        private string SHA256HashPassword(string password)
        {
            SHA256Managed hashProvider;
            Byte[] passwordBytes;
            //Byte[] hashBytes;

            passwordBytes = System.Text.Encoding.Unicode.GetBytes(password);
            hashProvider = new SHA256Managed();
            hashProvider.Initialize();
            passwordBytes = hashProvider.ComputeHash(passwordBytes);
            string hashedPassword = Convert.ToBase64String(passwordBytes);

            return hashedPassword;
        }

        //===============================================================
        // Function: SHA1HashPassword
        //===============================================================
        private string SHA1HashPassword(string password)
        {
            SHA1Managed hashProvider;
            Byte[] passwordBytes;
            //Byte[] hashBytes;

            passwordBytes = System.Text.Encoding.Unicode.GetBytes(password);
            hashProvider = new SHA1Managed();
            hashProvider.Initialize();
            passwordBytes = hashProvider.ComputeHash(passwordBytes);
            string hashedPassword = Convert.ToBase64String(passwordBytes);

            return hashedPassword;
        }

        //===============================================================
        // Function: MD5HashPassword
        //===============================================================
        private string MD5HashPassword(string password)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(password));
            StringBuilder hashedPassword = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                hashedPassword.Append(data[i].ToString("x2"));
            }
            return hashedPassword.ToString();
        }

        
    
    }
}

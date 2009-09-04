//===============================================================
// Filename: Administrators.cs
// Date: 19/08/09
// --------------------------------------------------------------
// Description:
//   Administrators class
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

namespace Sedogo.BusinessObjects
{
    //===============================================================
    // Class: Administrator
    //===============================================================
    public class Administrator
    {
        private int         m_administratorID = -1;
        private string      m_emailAddress = "";
        private string      m_administratorName = "";
        private Boolean     m_deleted = false;
        private DateTime    m_deletedDate = DateTime.MinValue;
        private Boolean     m_loginEnabled = false;
        private string      m_administratorPassword = "";
        private int         m_failedLoginCount = 0;
        private DateTime    m_passwordExpiryDate = DateTime.MinValue;
        private DateTime    m_lastLoginDate = DateTime.MinValue;
        private DateTime    m_createdDate = DateTime.MinValue;
        private string      m_createdByFullName = "";
        private DateTime    m_lastUpdatedDate = DateTime.MinValue;
        private string      m_lastUpdatedByFullName = "";

        private string      m_loggedInUser = "";

        public int administratorID
        {
            get { return m_administratorID; }
        }
        public string emailAddress
        {
            get { return m_emailAddress; }
            set { m_emailAddress = value; }
        }
        public string administratorName
        {
            get { return m_administratorName; }
            set { m_administratorName = value; }
        }
        public Boolean deleted
        {
            get { return m_deleted; }
        }
        public DateTime deletedDate
        {
            get { return m_deletedDate; }
        }
        public Boolean loginEnabled
        {
            get { return m_loginEnabled; }
            set { m_loginEnabled = value; }
        }
        public string administratorPassword
        {
            get { return m_administratorPassword; }
        }
        public int failedLoginCount
        {
            get { return m_failedLoginCount; }
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
        // Function: Administrator (Constructor)
        //===============================================================
        public Administrator(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: Administrator (Constructor)
        //===============================================================
        public Administrator(string loggedInUser, int administratorID)
        {
            m_loggedInUser = loggedInUser;
            m_administratorID = administratorID;

            ReadAdministratorDetails();
        }

        //===============================================================
        // Function: ReadAdministratorDetails
        //===============================================================
        public void ReadAdministratorDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectAdministratorDetails";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@AdministratorID";
                param.Value = m_administratorID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                if (!rdr.IsDBNull(rdr.GetOrdinal("EmailAddress")))
                {
                    m_emailAddress = (string)rdr["EmailAddress"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("AdministratorName")))
                {
                    m_administratorName = (string)rdr["AdministratorName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Deleted")))
                {
                    m_deleted = (Boolean)rdr["Deleted"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("DeletedDate")))
                {
                    m_deletedDate = (DateTime)rdr["DeletedDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LoginEnabled")))
                {
                    m_loginEnabled = (Boolean)rdr["LoginEnabled"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("AdministratorPassword")))
                {
                    m_administratorPassword = (string)rdr["AdministratorPassword"];
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
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Administrator", "ReadAdministratorDetails", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: Add
        //===============================================================
        public void Add()
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spAddAdministrator", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 200).Value = m_emailAddress;
                cmd.Parameters.Add("@AdministratorName", SqlDbType.NVarChar, 200).Value = m_administratorName;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@CreatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                SqlParameter paramAdministratorID = cmd.CreateParameter();
                paramAdministratorID.ParameterName = "@AdministratorID";
                paramAdministratorID.SqlDbType = SqlDbType.Int;
                paramAdministratorID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramAdministratorID);

                cmd.ExecuteNonQuery();
                m_administratorID = (int)paramAdministratorID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Administrator", "Add", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spUpdateAdministrator", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@AdministratorID", SqlDbType.Int).Value = m_administratorID;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 200).Value = m_emailAddress;
                cmd.Parameters.Add("@AdministratorName", SqlDbType.NVarChar, 200).Value = m_administratorName;
                cmd.Parameters.Add("@LoginEnabled", SqlDbType.Bit).Value = m_loginEnabled;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Administrator", "Update", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spDeleteAdministrator", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdministratorID", SqlDbType.Int).Value = m_administratorID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Administrator", "Delete", ex.Message, logMessageLevel.errorMessage);
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
                cmd.CommandText = "spVerifyAdministratorLogin";
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
                    int administratorID;
                    Boolean loginEnabled = false;
                    string administratorPassword = "";
                    int failedLoginCount = 0;
                    DateTime passwordExpiryDate = DateTime.MinValue;

                    rdr.Read();
                    administratorID = (int)rdr["AdministratorID"];
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LoginEnabled")))
                    {
                        loginEnabled = (Boolean)rdr["LoginEnabled"];
                    }
                    if (!rdr.IsDBNull(rdr.GetOrdinal("AdministratorPassword")))
                    {
                        administratorPassword = (string)rdr["AdministratorPassword"];
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
                            UpdateLoginHistory(administratorID, "E", source);     // Password expired
                        }
                        returnValue = loginResults.passwordExpired;

                        m_administratorID = administratorID;
                        ReadAdministratorDetails();
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

                    if ((administratorPassword != encryptedTestPassword) || (loginEnabled == false))
                    {
                        // Update the DB with a failed login attempt (invalid password)
                        UpdateLoginHistory(administratorID, "P", source);     // Invalid Password
                        returnValue = loginResults.loginFailed;
                    }
                    else
                    {
                        // Update the DB with a successful login attempt
                        if (recordInLoginHistory == true)
                        {
                            UpdateLoginHistory(administratorID, "S", source);      // Success
                        }
                        returnValue = loginResults.loginSuccess;

                        m_administratorID = administratorID;
                        ReadAdministratorDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Administrator", "VerifyLogin", ex.Message, logMessageLevel.errorMessage);
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
                cmd.CommandText = "spSelectAdministratorPassword";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@AdministratorID";
                param.Value = m_administratorID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows != false)
                {
                    string administratorPassword;

                    rdr.Read();
                    administratorPassword = (string)rdr["AdministratorPassword"];
                    rdr.Close();

                    PasswordEncrypt pe = new PasswordEncrypt();
                    string encryptedTestPassword = pe.EncryptPassword(testPassword);

                    // note that passwords are case sensitive
                    if (administratorPassword == encryptedTestPassword)
                    {
                        returnStatus = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Administrator", "VerifyPassword", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return returnStatus;
        }

        //===============================================================
        // Function: GetAdministratorIDFromEmailAddress
        //===============================================================
        public static int GetAdministratorIDFromEmailAddress(string emailAddress)
        {
            int administratorID = -1;

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);

            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetAdministratorIDFromEmailAddress";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@EmailAddress";
                param.Value = emailAddress;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows != false)
                {
                    rdr.Read();
                    administratorID = (int)rdr["AdministratorID"];
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Administrator", "GetAdministratorIDFromEmailAddress", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return administratorID;
        }

        //===============================================================
        // Function: UpdateLoginHistory
        //===============================================================
        private void UpdateLoginHistory(int administratorID, string status, string source)
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                // Update the DB with a failed login attempt (email address not recognised)
                DbCommand cmdLoginHistory = conn.CreateCommand();
                cmdLoginHistory.CommandType = CommandType.StoredProcedure;
                cmdLoginHistory.CommandText = "spInsertAdministratorLoginHistory";
                DbParameter paramLoginHistory1 = cmdLoginHistory.CreateParameter();
                paramLoginHistory1.ParameterName = "@AdministratorID";
                if (administratorID < 0)
                {
                    paramLoginHistory1.Value = DBNull.Value;
                }
                else
                {
                    paramLoginHistory1.Value = administratorID;
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

                if ((status == "L") || (status == "P") && administratorID > 0)
                {
                    DbCommand cmdIncrementFailedLoginCount = conn.CreateCommand();
                    cmdIncrementFailedLoginCount.CommandType = CommandType.StoredProcedure;
                    cmdIncrementFailedLoginCount.CommandText = "spIncrementAdministratorFailedLoginCount";
                    DbParameter paramIncrementFailedLoginCount = cmdIncrementFailedLoginCount.CreateParameter();
                    paramIncrementFailedLoginCount.ParameterName = "@AdministratorID";
                    paramIncrementFailedLoginCount.Value = administratorID;
                    cmdIncrementFailedLoginCount.Parameters.Add(paramIncrementFailedLoginCount);

                    cmdIncrementFailedLoginCount.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Administrator", "UpdateLoginHistory", ex.Message, logMessageLevel.errorMessage);
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

                // Update administrator password
                SqlCommand cmd = new SqlCommand("spUpdateAdministratorPassword", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@AdministratorID", SqlDbType.Int).Value = m_administratorID;
                cmd.Parameters.Add("@AdministratorPassword", SqlDbType.NVarChar, 50).Value = encryptedPassword;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Administrator", "UpdatePassword", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: ResetAdministratorPassword
        //===============================================================
        public Boolean ResetAdministratorPassword(string emailAddress, ref string newPassword)
        {
            // Lookup the Contact ID of the supplied email address
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spVerifyAdministratorLogin", conn);
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
                    int administratorID;
                    Boolean loginEnabled;

                    rdr.Read();
                    administratorID = (int)rdr["AdministratorID"];
                    loginEnabled = (Boolean)rdr["LoginEnabled"];
                    rdr.Close();

                    m_administratorID = administratorID;
                    ReadAdministratorDetails();
                    GenerateNewPassword(ref newPassword);
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Administrator", "ResetUserPassword", ex.Message, logMessageLevel.errorMessage);
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
            string p1 = Convert.ToChar(random.Next(58, 64)).ToString();

            newPassword = c1 + c2 + p1 + num1 + num2 + c3 + c4 + num3 + num4;

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
    }
}

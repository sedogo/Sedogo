//===============================================================
// Filename: Languages.cs
// Date: 12/08/09
// --------------------------------------------------------------
// Description:
//   Languages class
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 12/08/09
// Revision history:
//===============================================================

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Sedogo.BusinessObjects
{
    //===============================================================
    // Class: Language
    //===============================================================
    public class Language
    {
        private int m_languageID = -1;
        private string m_languageCode = "";
        private string m_languageName = "";
        private Boolean m_deleted = false;
        private Boolean m_defaultLanguage = false;

        private string m_loggedInUser = "";

        public int languageID
        {
            get { return m_languageID; }
        }
        public string languageCode
        {
            get { return m_languageCode; }
            set { m_languageCode = value; }
        }
        public string languageName
        {
            get { return m_languageName; }
            set { m_languageName = value; }
        }
        public Boolean deleted
        {
            get { return m_deleted; }
        }
        public Boolean defaultLanguage
        {
            get { return m_defaultLanguage; }
            set { m_defaultLanguage = value; }
        }

        //===============================================================
        // Function: Language (Constructor)
        //===============================================================
        public Language(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: Language (Constructor)
        //===============================================================
        public Language(string loggedInUser, int languageID)
        {
            m_loggedInUser = loggedInUser;
            m_languageID = languageID;

            ReadLanguageDetails();
        }

        //===============================================================
        // Function: ReadLanguageDetails
        //===============================================================
        public void ReadLanguageDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmdLanguageDetails = conn.CreateCommand();
                cmdLanguageDetails.CommandType = CommandType.StoredProcedure;
                cmdLanguageDetails.CommandText = "spSelectLanguageDetails";
                DbParameter param = cmdLanguageDetails.CreateParameter();
                param.ParameterName = "@LanguageID";
                param.Value = m_languageID;
                cmdLanguageDetails.Parameters.Add(param);
                DbDataReader rdrLanguageDetails = cmdLanguageDetails.ExecuteReader();
                rdrLanguageDetails.Read();
                if (!rdrLanguageDetails.IsDBNull(rdrLanguageDetails.GetOrdinal("LanguageCode")))
                {
                    m_languageCode = (string)rdrLanguageDetails["LanguageCode"];
                }
                if (!rdrLanguageDetails.IsDBNull(rdrLanguageDetails.GetOrdinal("LanguageName")))
                {
                    m_languageName = (string)rdrLanguageDetails["LanguageName"];
                }
                if (!rdrLanguageDetails.IsDBNull(rdrLanguageDetails.GetOrdinal("DefaultLanguage")))
                {
                    m_defaultLanguage = (Boolean)rdrLanguageDetails["DefaultLanguage"];
                }
                rdrLanguageDetails.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Language", "ReadLanguageDetails", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spAddLanguage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@LanguageCode", SqlDbType.NVarChar, 10).Value = m_languageCode;
                cmd.Parameters.Add("@LanguageName", SqlDbType.NVarChar, 150).Value = m_languageName;
                cmd.Parameters.Add("@DefaultLanguage", SqlDbType.Bit).Value = m_defaultLanguage;

                SqlParameter paramLanguageID = cmd.CreateParameter();
                paramLanguageID.ParameterName = "@LanguageID";
                paramLanguageID.SqlDbType = SqlDbType.Int;
                paramLanguageID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramLanguageID);

                cmd.ExecuteNonQuery();
                m_languageID = (int)paramLanguageID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Language", "Add", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spUpdateLanguage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@LanguageID", SqlDbType.Int).Value = m_languageID;
                cmd.Parameters.Add("@LanguageCode", SqlDbType.NVarChar, 10).Value = m_languageCode;
                cmd.Parameters.Add("@LanguageName", SqlDbType.NVarChar, 150).Value = m_languageName;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Language", "Update", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spDeleteLanguage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@LanguageID", SqlDbType.Int).Value = m_languageID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Language", "Delete", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: GetDefaultLanguage
        //===============================================================
        public static int GetDefaultLanguage()
        {
            int languageID = -1;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetDefaultLanguage";

                SqlParameter paramLanguageID = cmd.CreateParameter();
                paramLanguageID.ParameterName = "@LanguageID";
                paramLanguageID.SqlDbType = SqlDbType.Int;
                paramLanguageID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramLanguageID);

                cmd.ExecuteNonQuery();
                languageID = (int)paramLanguageID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Language", "GetDefaultLanguage", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return languageID;
        }

        //===============================================================
        // Function: GetLanguageIDFromName
        //===============================================================
        public static int GetLanguageIDFromName(string languageName)
        {
            int languageID = -1;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetLanguageIDFromName";
                cmd.Parameters.Add("@LanguageName", SqlDbType.NVarChar, 150).Value = languageName;

                SqlParameter paramLanguageID = cmd.CreateParameter();
                paramLanguageID.ParameterName = "@LanguageID";
                paramLanguageID.SqlDbType = SqlDbType.Int;
                paramLanguageID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramLanguageID);

                cmd.ExecuteNonQuery();

                if (paramLanguageID.Value != System.DBNull.Value)
                {
                    languageID = (int)paramLanguageID.Value;
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Language", "GetLanguageIDFromName", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return languageID;
        }
    }
}

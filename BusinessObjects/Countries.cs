//===============================================================
// Filename: Countries.cs
// Date: 17/10/06
// --------------------------------------------------------------
// Description:
//   Countries class
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 17/10/06
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
    // Class: Country
    //===============================================================
    public class Country
    {
        private int             m_countryID = -1;
        private string          m_countryCode = "";
        private string          m_countryName = "";
        private Boolean         m_deleted = false;
        private Boolean         m_defaultCountry = false;

        private string          m_loggedInUser = "";

        public int countryID
        {
            get { return m_countryID; }
        }
        public string countryCode
        {
            get { return m_countryCode; }
            set { m_countryCode = value; }
        }
        public string countryName
        {
            get { return m_countryName; }
            set { m_countryName = value; }
        }
        public Boolean deleted
        {
            get { return m_deleted; }
        }
        public Boolean defaultCountry
        {
            get { return m_defaultCountry; }
            set { m_defaultCountry = value; }
        }

        //===============================================================
        // Function: Country (Constructor)
        //===============================================================
        public Country(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: Country (Constructor)
        //===============================================================
        public Country(string loggedInUser, int countryID)
        {
            m_loggedInUser = loggedInUser;
            m_countryID = countryID;

            ReadCountryDetails();
        }

        //===============================================================
        // Function: ReadCountryDetails
        //===============================================================
        public void ReadCountryDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmdCountryDetails = conn.CreateCommand();
                cmdCountryDetails.CommandType = CommandType.StoredProcedure;
                cmdCountryDetails.CommandText = "spSelectCountryDetails";
                DbParameter param = cmdCountryDetails.CreateParameter();
                param.ParameterName = "@CountryID";
                param.Value = m_countryID;
                cmdCountryDetails.Parameters.Add(param);
                DbDataReader rdrCountryDetails = cmdCountryDetails.ExecuteReader();
                rdrCountryDetails.Read();
                if (!rdrCountryDetails.IsDBNull(rdrCountryDetails.GetOrdinal("CountryCode")))
                {
                    m_countryCode = (string)rdrCountryDetails["CountryCode"];
                }
                if (!rdrCountryDetails.IsDBNull(rdrCountryDetails.GetOrdinal("CountryName")))
                {
                    m_countryName = (string)rdrCountryDetails["CountryName"];
                }
                if (!rdrCountryDetails.IsDBNull(rdrCountryDetails.GetOrdinal("DefaultCountry")))
                {
                    m_defaultCountry = (Boolean)rdrCountryDetails["DefaultCountry"];
                }
                rdrCountryDetails.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Country", "ReadCountryDetails", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spAddCountry", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CountryCode", SqlDbType.NVarChar, 10).Value = m_countryCode;
                cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar, 150).Value = m_countryName;
                cmd.Parameters.Add("@DefaultCountry", SqlDbType.Bit).Value = m_defaultCountry;

                SqlParameter paramCountryID = cmd.CreateParameter();
                paramCountryID.ParameterName = "@CountryID";
                paramCountryID.SqlDbType = SqlDbType.Int;
                paramCountryID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramCountryID);

                cmd.ExecuteNonQuery();
                m_countryID = (int)paramCountryID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Country", "Add", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spUpdateCountry", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = m_countryID;
                cmd.Parameters.Add("@CountryCode", SqlDbType.NVarChar, 10).Value = m_countryCode;
                cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar, 150).Value = m_countryName;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Country", "Update", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spDeleteCountry", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = m_countryID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Country", "Delete", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: GetDefaultCountry
        //===============================================================
        public static int GetDefaultCountry()
        {
            int countryID = -1;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetDefaultCountry";

                SqlParameter paramCountryID = cmd.CreateParameter();
                paramCountryID.ParameterName = "@CountryID";
                paramCountryID.SqlDbType = SqlDbType.Int;
                paramCountryID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramCountryID);

                cmd.ExecuteNonQuery();
                countryID = (int)paramCountryID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Country", "GetDefaultCountry", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return countryID;
        }

        //===============================================================
        // Function: GetCountryIDFromName
        //===============================================================
        public static int GetCountryIDFromName(string countryName)
        {
            int countryID = -1;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetCountryIDFromName";
                cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar, 150).Value = countryName;

                SqlParameter paramCountryID = cmd.CreateParameter();
                paramCountryID.ParameterName = "@CountryID";
                paramCountryID.SqlDbType = SqlDbType.Int;
                paramCountryID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramCountryID);

                cmd.ExecuteNonQuery();

                if (paramCountryID.Value != System.DBNull.Value)
                {
                    countryID = (int)paramCountryID.Value;
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("Country", "GetCountryIDFromName", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return countryID;
        }
    }
}

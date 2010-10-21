//===============================================================
// Filename: Timezone.cs
// Date: 09/11/09
// --------------------------------------------------------------
// Description:
//   Timezone class
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 09/11/09
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
    public class SedogoTimezone
    {
        private int         m_timezoneID = -1;
        private string      m_shortCode = "";
        private string      m_description = "";
        private int         m_GMTOffset = 0;

        private string      m_loggedInUser = "";

        public int timezoneID
        {
            get { return m_timezoneID; }
        }
        public string shortCode
        {
            get { return m_shortCode; }
            set { m_shortCode = value; }
        }
        public string description
        {
            get { return m_description; }
            set { m_description = value; }
        }
        public int GMTOffset
        {
            get { return m_GMTOffset; }
            set { m_GMTOffset = value; }
        }

        //===============================================================
        // Function: SedogoTimezone (Constructor)
        //===============================================================
        public SedogoTimezone(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: SedogoTimezone (Constructor)
        //===============================================================
        public SedogoTimezone(string loggedInUser, int timezoneID)
        {
            m_loggedInUser = loggedInUser;
            m_timezoneID = timezoneID;

            ReadTimezoneDetails();
        }
    
        //===============================================================
        // Function: ReadTimezoneDetails
        //===============================================================
        public void ReadTimezoneDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectTimezoneDetails";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@TimezoneID";
                param.Value = m_timezoneID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                if (!rdr.IsDBNull(rdr.GetOrdinal("ShortCode")))
                {
                    m_shortCode = (string)rdr["ShortCode"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Description")))
                {
                    m_description = (string)rdr["Description"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("GMTOffset")))
                {
                    m_GMTOffset = int.Parse(rdr["GMTOffset"].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoTimezone", "ReadTimezoneDetails", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spAddTimezone", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ShortCode", SqlDbType.NVarChar, 10).Value = m_shortCode;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = m_description;
                cmd.Parameters.Add("@GMTOffset", SqlDbType.Int).Value = m_GMTOffset;

                SqlParameter paramTimezoneID = cmd.CreateParameter();
                paramTimezoneID.ParameterName = "@TimezoneID";
                paramTimezoneID.SqlDbType = SqlDbType.Int;
                paramTimezoneID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramTimezoneID);

                cmd.ExecuteNonQuery();
                m_timezoneID = (int)paramTimezoneID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoTimezone", "Add", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spUpdateTimezone", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TimezoneID", SqlDbType.Int).Value = m_timezoneID;
                cmd.Parameters.Add("@ShortCode", SqlDbType.NVarChar, 10).Value = m_shortCode;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = m_description;
                cmd.Parameters.Add("@GMTOffset", SqlDbType.Int).Value = m_GMTOffset;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoTimezone", "Update", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }



    }
}

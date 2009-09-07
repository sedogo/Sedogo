﻿//===============================================================
// Filename: Events.cs
// Date: 07/09/09
// --------------------------------------------------------------
// Description:
//   Events class
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 07/09/09
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
    public class SedogoEvent
    {
        private int         m_eventID = -1;
        private int         m_userID = -1;
        private string      m_eventName = "";
        private DateTime    m_startDate = DateTime.MinValue;
        private DateTime    m_endDate = DateTime.MinValue;
        private Boolean     m_deleted = false;
        private DateTime    m_createdDate = DateTime.MinValue;
        private string      m_createdByFullName = "";
        private DateTime    m_lastUpdatedDate = DateTime.MinValue;
        private string      m_lastUpdatedByFullName = "";

        private string      m_loggedInUser = "";

        public int eventID
        {
            get { return m_eventID; }
        }
        public int userID
        {
            get { return m_userID; }
            set { m_userID = value; }
        }
        public string eventName
        {
            get { return m_eventName; }
            set { m_eventName = value; }
        }
        public DateTime startDate
        {
            get { return m_startDate; }
            set { m_startDate = value; }
        }
        public DateTime endDate
        {
            get { return m_endDate; }
            set { m_endDate = value; }
        }
        public Boolean deleted
        {
            get { return m_deleted; }
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
        // Function: SedogoEvent (Constructor)
        //===============================================================
        public SedogoEvent(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: SedogoEvent (Constructor)
        //===============================================================
        public SedogoEvent(string loggedInUser, int eventID)
        {
            m_loggedInUser = loggedInUser;
            m_eventID = eventID;

            ReadEventDetails();
        }

        //===============================================================
        // Function: ReadEventDetails
        //===============================================================
        public void ReadEventDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectEventDetails";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@EventID";
                param.Value = m_eventID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                if (!rdr.IsDBNull(rdr.GetOrdinal("UserID")))
                {
                    m_userID = int.Parse(rdr["UserID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventName")))
                {
                    m_eventName = (string)rdr["EventName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("StartDate")))
                {
                    m_startDate = (DateTime)rdr["StartDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EndDate")))
                {
                    m_endDate = (DateTime)rdr["EndDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("Deleted")))
                {
                    m_deleted = (Boolean)rdr["Deleted"];
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
                errorLog.WriteLog("SedogoEvent", "ReadEventDetails", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spAddEvent", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = m_userID;
                cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 200).Value = m_eventName;
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = m_startDate;
                cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = m_endDate;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@CreatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                SqlParameter paramEventID = cmd.CreateParameter();
                paramEventID.ParameterName = "@EventID";
                paramEventID.SqlDbType = SqlDbType.Int;
                paramEventID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramEventID);

                cmd.ExecuteNonQuery();
                m_eventID = (int)paramEventID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "Add", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spUpdateEvent", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = m_eventID;
                cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 200).Value = m_eventName;
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = m_startDate;
                cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = m_endDate;
                cmd.Parameters.Add("@LastUpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@LastUpdatedByFullName", SqlDbType.NVarChar, 200).Value = m_loggedInUser;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "Update", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spDeleteEvent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = m_eventID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoEvent", "Delete", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

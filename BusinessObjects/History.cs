//===============================================================
// Filename: History.cs
// Date: 27/10/09
// --------------------------------------------------------------
// Description:
//   History class
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 27/10/09
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
    // Class: SearchHistory
    //===============================================================
    public class SearchHistory
    {
        private int         m_searchHistoryID = -1;
        private DateTime    m_searchDate = DateTime.MinValue;
        private int         m_userID = -1;
        private string      m_searchText = "";
        private int         m_searchHits = -1;

        private string      m_loggedInUser = "";

        public int searchHistoryID
        {
            get { return m_searchHistoryID; }
        }
        public DateTime searchDate
        {
            get { return m_searchDate; }
            set { m_searchDate = value; }
        }
        public int userID
        {
            get { return m_userID; }
            set { m_userID = value; }
        }
        public string searchText
        {
            get { return m_searchText; }
            set { m_searchText = value; }
        }
        public int searchHits
        {
            get { return m_searchHits; }
            set { m_searchHits = value; }
        }

        //===============================================================
        // Function: SearchHistory (Constructor)
        //===============================================================
        public SearchHistory(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: SearchHistory (Constructor)
        //===============================================================
        public SearchHistory(string loggedInUser, int searchHistoryID)
        {
            m_loggedInUser = loggedInUser;
            m_searchHistoryID = searchHistoryID;

            ReadSearchHistoryDetails();
        }

        //===============================================================
        // Function: ReadSearchHistoryDetails
        //===============================================================
        public void ReadSearchHistoryDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectSearchHistoryDetails";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@SearchHistoryID";
                param.Value = m_searchHistoryID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                if (!rdr.IsDBNull(rdr.GetOrdinal("SearchDate")))
                {
                    m_searchDate = (DateTime)rdr["SearchDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("UserID")))
                {
                    m_userID = int.Parse(rdr["UserID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("SearchText")))
                {
                    m_searchText = (string)rdr["SearchText"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("SearchHits")))
                {
                    m_searchHits = int.Parse(rdr["SearchHits"].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SearchHistory", "ReadSearchHistoryDetails", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spAddSearchHistory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add("@SearchDate", SqlDbType.DateTime).Value = m_searchDate;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = m_userID;
                cmd.Parameters.Add("@SearchText", SqlDbType.NVarChar, 200).Value = m_searchText;
                cmd.Parameters.Add("@SearchHits", SqlDbType.Int).Value = m_searchHits;

                SqlParameter paramSearchHistoryID = cmd.CreateParameter();
                paramSearchHistoryID.ParameterName = "@SearchHistoryID";
                paramSearchHistoryID.SqlDbType = SqlDbType.Int;
                paramSearchHistoryID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramSearchHistoryID);

                cmd.ExecuteNonQuery();
                m_searchHistoryID = (int)paramSearchHistoryID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SearchHistory", "Add", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

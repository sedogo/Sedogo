//===============================================================
// Filename: AddressBook.cs
// Date: 27/03/10
// --------------------------------------------------------------
// Description:
//   AddressBook class
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 27/03/10
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
    // Class: AddressBook
    //===============================================================
    public class AddressBook
    {
        private int         m_addressBookID = -1;
        private int         m_userID = -1;
        private string      m_firstName = "";
        private string      m_lastName = "";
        private string      m_emailAddress = "";

        private string m_loggedInUser = "";

        public int addressBookID
        {
            get { return m_addressBookID; }
        }
        public int userID
        {
            get { return m_userID; }
            set { m_userID = value; }
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
        public string emailAddress
        {
            get { return m_emailAddress; }
            set { m_emailAddress = value; }
        }

        //===============================================================
        // Function: AddressBook (Constructor)
        //===============================================================
        public AddressBook(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: AddressBook (Constructor)
        //===============================================================
        public AddressBook(string loggedInUser, int addressBookID)
        {
            m_loggedInUser = loggedInUser;
            m_addressBookID = addressBookID;

            ReadAddressBookDetails();
        }

        //===============================================================
        // Function: ReadAddressBookDetails
        //===============================================================
        public void ReadAddressBookDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectAddressBookDetails";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@AddressBookID";
                param.Value = m_addressBookID;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                if (!rdr.IsDBNull(rdr.GetOrdinal("UserID")))
                {
                    m_userID = int.Parse(rdr["UserID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("FirstName")))
                {
                    m_firstName = (string)rdr["FirstName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("LastName")))
                {
                    m_lastName = (string)rdr["LastName"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("EmailAddress")))
                {
                    m_emailAddress = (string)rdr["EmailAddress"];
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("AddressBook", "ReadAddressBookDetails", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spAddAddressBook", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = m_userID;
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 200).Value = m_firstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 200).Value = m_lastName;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 200).Value = m_emailAddress;

                SqlParameter paramAddressBookID = cmd.CreateParameter();
                paramAddressBookID.ParameterName = "@AddressBookID";
                paramAddressBookID.SqlDbType = SqlDbType.Int;
                paramAddressBookID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramAddressBookID);

                cmd.ExecuteNonQuery();
                m_addressBookID = (int)paramAddressBookID.Value;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("AddressBook", "Add", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spUpdateAddressBook", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@AddressBookID", SqlDbType.Int).Value = m_addressBookID;
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 200).Value = m_firstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 200).Value = m_lastName;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 200).Value = m_emailAddress;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("AddressBook", "Update", ex.Message, logMessageLevel.errorMessage);
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

                SqlCommand cmd = new SqlCommand("spDeleteAddressBook", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AddressBookID", SqlDbType.Int).Value = m_addressBookID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("AddressBook", "Delete", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: GetAddressBookCountByUser
        //===============================================================
        public static int GetAddressBookCountByUser(int userID)
        {
            int alertCount = 0;

            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spSelectAddressBookCountByUser";
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                alertCount = int.Parse(rdr[0].ToString());
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("AddressBook", "GetAddressBookCountByUser", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return alertCount;
        }
    }
}

//===============================================================
// Filename: GlobalData.cs
// Date: 19/08/09
// --------------------------------------------------------------
// Description:
//   Process login
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 19/08/09
// Revision history:
//===============================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Sedogo.BusinessObjects
{
    sealed public class GlobalDataNullException : Exception
    {
    }
    sealed public class GlobalDataMissingException : Exception
    {
    }

    //===============================================================
    // Class: GlobalData
    //===============================================================
    public class GlobalData
    {
        private string m_loggedInUser;

        //===============================================================
        // Function: GlobalData (Constructor)
        //===============================================================
        public GlobalData(string loggedInUser)
        {
            m_loggedInUser = loggedInUser;
        }

        //===============================================================
        // Function: CheckValueExists
        //===============================================================
        public Boolean CheckValueExists(string keyName)
        {
            Boolean returnValue = false;

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGlobalDataGetStringValue";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@KeyName";
                param.Value = keyName;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == true)
                {
                    returnValue = true;
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("GlobalData", "CheckValueExists", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return returnValue;
        }

        //===============================================================
        // Function: GetStringValue
        //===============================================================
        public string GetStringValue(string keyName)
        {
            string returnString = "";

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGlobalDataGetStringValue";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@KeyName";
                param.Value = keyName;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == false)
                {
                    // GlobalData value not found - throw an exception
                    GlobalDataMissingException ex = new GlobalDataMissingException();
                    throw ex;
                }
                else
                {
                    rdr.Read();
                    if (rdr.IsDBNull(rdr.GetOrdinal("StringValue")))
                    {
                        // GlobalData value is null - throw an exception
                        GlobalDataNullException ex = new GlobalDataNullException();
                        throw ex;
                    }
                    else
                    {
                        returnString = (string)rdr["StringValue"];
                    }
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("GlobalData", "GetStringValue", "Keyname: " + keyName + ", Error: " + ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return returnString;
        }

        //===============================================================
        // Function: GetIntegerValue
        //===============================================================
        public int GetIntegerValue(string keyName)
        {
            int returnInt;

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGlobalDataSelectIntegerValue";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@KeyName";
                param.Value = keyName;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == false)
                {
                    // GlobalData value not found - throw an exception
                    GlobalDataMissingException ex = new GlobalDataMissingException();
                    throw ex;
                }
                else
                {
                    rdr.Read();
                    if (rdr.IsDBNull(rdr.GetOrdinal("IntegerValue")))
                    {
                        // GlobalData value is null - throw an exception
                        GlobalDataNullException ex = new GlobalDataNullException();
                        throw ex;
                    }
                    else
                    {
                        returnInt = int.Parse(rdr["IntegerValue"].ToString());
                    }
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("GlobalData", "GetIntegerValue", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return returnInt;
        }

        //===============================================================
        // Function: GetNumericValue
        //===============================================================
        public decimal GetNumericValue(string keyName)
        {
            decimal returnValue;

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGlobalDataGetNumericValue";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@KeyName";
                param.Value = keyName;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == false)
                {
                    // GlobalData value not found - throw an exception
                    GlobalDataMissingException ex = new GlobalDataMissingException();
                    throw ex;
                }
                else
                {
                    rdr.Read();
                    if (rdr.IsDBNull(rdr.GetOrdinal("NumericValue")))
                    {
                        // GlobalData value is null - throw an exception
                        GlobalDataNullException ex = new GlobalDataNullException();
                        throw ex;
                    }
                    else
                    {
                        returnValue = decimal.Parse(rdr["NumericValue"].ToString());
                    }
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("GlobalData", "GetNumericValue", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return returnValue;
        }

        //===============================================================
        // Function: GetDateValue
        //===============================================================
        public DateTime GetDateValue(string keyName)
        {
            DateTime returnDate = DateTime.MinValue;

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGlobalDataGetDateValue";
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@KeyName";
                param.Value = keyName;
                cmd.Parameters.Add(param);
                DbDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == false)
                {
                    // GlobalData value not found - throw an exception
                    GlobalDataMissingException ex = new GlobalDataMissingException();
                    throw ex;
                }
                else
                {
                    rdr.Read();
                    if (rdr.IsDBNull(rdr.GetOrdinal("DateValue")))
                    {
                        // GlobalData value is null - throw an exception
                        GlobalDataNullException ex = new GlobalDataNullException();
                        throw ex;
                    }
                    else
                    {
                        returnDate = (DateTime)rdr["DateValue"];
                    }
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("GlobalData", "GetDateValue", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return returnDate;
        }

        //===============================================================
        // Function: AddStringValue
        //===============================================================
        public void AddStringValue(string keyName, string value)
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spGlobalDataAddStringValue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@KeyName", SqlDbType.NVarChar, 50).Value = keyName;
                cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 1000).Value = value;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("GlobalData", "AddStringValue", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: AddIntegerValue
        //===============================================================
        public void AddIntegerValue(string keyName, int value)
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spGlobalDataAddIntegerValue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@KeyName", SqlDbType.NVarChar, 50).Value = keyName;
                cmd.Parameters.Add("@Value", SqlDbType.Int).Value = value;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("GlobalData", "AddIntegerValue", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: UpdateStringValue
        //===============================================================
        public void UpdateStringValue(string keyName, string value)
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spGlobalDataUpdateStringValue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@KeyName", SqlDbType.NVarChar, 50).Value = keyName;
                cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 1000).Value = value;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("GlobalData", "UpdateStringValue", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: UpdateIntegerValue
        //===============================================================
        public void UpdateIntegerValue(string keyName, int value)
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spGlobalDataUpdateIntegerValue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@KeyName", SqlDbType.NVarChar, 50).Value = keyName;
                cmd.Parameters.Add("@Value", SqlDbType.Int).Value = value;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("GlobalData", "UpdateIntegerValue", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: UpdateNumericValue
        //===============================================================
        public void UpdateNumericValue(string keyName, decimal value)
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spGlobalDataUpdateNumericValue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@KeyName", SqlDbType.NVarChar, 50).Value = keyName;
                cmd.Parameters.Add("@Value", SqlDbType.Decimal).Value = value;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("GlobalData", "UpdateNumericValue", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: UpdateDateValue
        //===============================================================
        public void UpdateDateValue(string keyName, DateTime value)
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spGlobalDataUpdateDateValue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@KeyName", SqlDbType.NVarChar, 50).Value = keyName;
                cmd.Parameters.Add("@Value", SqlDbType.DateTime).Value = value;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("GlobalData", "UpdateDateValue", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //===============================================================
        // Function: DeleteValue
        //===============================================================
        public void DeleteValue(string keyName)
        {
            SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spGlobalDataDeleteValue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@KeyName", SqlDbType.NVarChar, 50).Value = keyName;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("GlobalData", "DeleteValue", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

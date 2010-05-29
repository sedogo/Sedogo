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

/// <summary>
/// Summary description for SedogoNewFun
/// </summary>
/// 
namespace Sedogo.BusinessObjects
{
    public class SedogoNewFun : System.Web.UI.Page
    {
        public SedogoNewFun()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //===============================================================
        // Function: ReadUserDetails
        //===============================================================
        public DataTable GetAllEnableUserDetails()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();
                DataTable dtUsers = new DataTable();
                DbCommand cmd = conn.CreateCommand();
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "spGetAllEnableUserDetails";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT top 24 userid,[guid],(select count(*) as gcount from events where userid = u.userid group by userid)gcount, EmailAddress, FirstName, LastName, Gender, Deleted, DeletedDate,"
        + "HomeTown, Birthday, ProfilePicFilename, ProfilePicThumbnail, ProfilePicPreview,"
        + "ProfileText, CountryID, LanguageID, TimezoneID, EnableSendEmails,"
        + "LoginEnabled, UserPassword, FailedLoginCount, PasswordExpiryDate, LastLoginDate,"
        + "CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName "
    + "FROM Users u"
    + " WHERE LoginEnabled = 1 and deleted = 0";

                DbDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(dtUsers);
                return dtUsers;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "GetAllEnableUserDetails", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable GetLatestAchievedGoals()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();
                DataTable dtLGoal = new DataTable();
                DbCommand cmd = conn.CreateCommand();
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "spGetLatestAchievedGoals";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from events where EventAchieved = 1 order by LastUpdatedDate desc";                	
                DbDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(dtLGoal);
                return dtLGoal;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "GetLatestAchievedGoals", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable GetGoalsHappeningToday()
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();
                DataTable dtGoalsToday = new DataTable();
                DbCommand cmd = conn.CreateCommand();
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "spGetGoalsHappeningToday";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from events where convert(varchar(10),RangeEndDate,103)> = convert(varchar(10),getdate(),103)";

                DbDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(dtGoalsToday);
                return dtGoalsToday;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "GetGoalsHappeningToday", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }


    }
}
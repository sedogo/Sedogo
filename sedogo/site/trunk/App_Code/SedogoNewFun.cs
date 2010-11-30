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
        // Function: GetAllEnableUserDetails
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
                cmd.CommandText = "SELECT top 24 userid,[guid],(select count(*) as gcount from events where deleted = 0 and userid = u.userid group by userid)gcount,(select count(*) as mcount from Users WHERE LoginEnabled = 1 and deleted = 0)mcount, "
                + "EmailAddress, FirstName, LastName, Gender, Deleted, DeletedDate,"
        + "HomeTown, Birthday, ProfilePicFilename, ProfilePicThumbnail, ProfilePicPreview,"
        + "ProfileText, CountryID, LanguageID, TimezoneID, EnableSendEmails,"
        + "LoginEnabled, UserPassword, FailedLoginCount, PasswordExpiryDate, LastLoginDate,"
        + "CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName "
    + "FROM Users u"
    + " WHERE LoginEnabled = 1 and deleted = 0 order by createddate desc";

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

        //===============================================================
        // Function: GetProfileGoalPicsDetails
        //===============================================================
        public DataTable GetProfileGoalPicsDetails(int userID, Boolean showPrivate)
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
                cmd.CommandText = "SELECT top 24 EventID,EventName, EventPicThumbnail, EventPicPreview, EventGUID "
                    + "FROM Events "
                    + " WHERE UserID = " + userID.ToString() + " and Deleted = 0 ";
                if( showPrivate == false )
                {
                    cmd.CommandText += "and PrivateEvent = 0 ";
                }
                cmd.CommandText += "order by CreatedDate desc ";

                DbDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(dtUsers);
                return dtUsers;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("SedogoUser", "GetProfileGoalPicsDetails", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /*
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
                cmd.CommandText = "select * from events where EventAchieved = 1 order by EventAchievedDate desc";                	
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
        */

        /*
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
                cmd.CommandText = "select * from events ";
                cmd.CommandText += "where ( convert(varchar(10),RangeEndDate,103) >= convert(varchar(10),getdate(),103)";
                cmd.CommandText += "or convert(varchar(10),StartDate,103) = convert(varchar(10),getdate(),103) )";
                cmd.CommandText += "AND EventAchieved = 0 ";
                cmd.CommandText += "AND Deleted = 0 ";
                cmd.CommandText += "AND PrivateEvent = 0 ";

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
        */
    }
}
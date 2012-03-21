//===============================================================
// Filename: homePageContent.aspx
// Date: 14/07/10
// --------------------------------------------------------------
// Description:
//   
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 14/07/10
// Revision history:
//===============================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;
using System.Text;
using System.IO;
using Sedogo.BusinessObjects;

public partial class admin_homePageContent : AdminPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT HomePageContent FROM HomePageContent ";
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                homePageContentTextBox.Text = (string)rdr["HomePageContent"];
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("", "", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    //===============================================================
    // Function: saveButton_Click
    //===============================================================
    protected void saveButton_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
        try
        {
            conn.Open();

            string sql = "DELETE HomePageContent ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            string sql2 = "INSERT INTO HomePageContent ( HomePageContent ) VALUES ( @HomePageContent ) ";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.Add("@HomePageContent", SqlDbType.NVarChar, -1).Value = homePageContentTextBox.Text.Trim();
            cmd2.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrorLog errorLog = new ErrorLog();
            errorLog.WriteLog("", "", ex.Message, logMessageLevel.errorMessage);
            throw ex;
        }
        finally
        {
            conn.Close();
        }
    }

    //===============================================================
    // Function: cancelButton_Click
    //===============================================================
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("main.aspx");
    }
}

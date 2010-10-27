using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sedogo.BusinessObjects;

public partial class admin_allGoals : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string action = "";
            int eventID = -1;
            if (Request.QueryString["A"] != null && Request.QueryString["EID"] != null)
            {
                action = Request.QueryString["A"].ToString();
                eventID = int.Parse(Request.QueryString["EID"]);

                if (action == "Delete")
                {
                    try
                    {
                        SedogoEvent sedogoEvent = new SedogoEvent("", eventID);
                        sedogoEvent.Delete();
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert",
                            "alert(\"Error: " + ex.Message + "\");", true);
                    }
                }
            }

            DbConnection conn2 = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn2.Open();

                DbCommand cmdCount = conn2.CreateCommand();
                cmdCount.CommandType = CommandType.StoredProcedure;
                cmdCount.CommandText = "spSelectAdministratorsEventCount";
                DbDataReader rdrCount = cmdCount.ExecuteReader();
                rdrCount.Read();
                int goalCount = int.Parse(rdrCount[0].ToString());
                rdrCount.Close();

                goalCountLabel.Text = goalCount.ToString();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("admin_allGoals", "Page_Load", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn2.Close();
            }

            try
            {
                SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

                SqlCommand cmd = new SqlCommand("spSelectAdministratorsEventList", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandTimeout = 90;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                allGoalsRepeater.DataSource = ds;
                allGoalsRepeater.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    //===============================================================
    // Function: allGoalsRepeater_ItemDataBound
    //===============================================================
    protected void allGoalsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null &&
            (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
        {
            DataRowView row = e.Item.DataItem as DataRowView;

            HyperLink nameLabel = e.Item.FindControl("nameLabel") as HyperLink;
            nameLabel.NavigateUrl = "../viewEvent.aspx?EID=" + row["EventID"].ToString();
            nameLabel.Text = row["EventName"].ToString() + " - " + row["FirstName"].ToString() + " " + row["LastName"].ToString();
            if( row["PrivateEvent"].ToString() == "True" )
            {
                nameLabel.Text += " - Private";
            }

            /*
	SELECT E.EventID, E.EventName, E.DateType, E.StartDate, E.RangeStartDate, E.RangeEndDate,
		E.BeforeBirthday, E.CategoryID, E.TimezoneID, E.EventAchieved, E.EventAchievedDate, 
		E., E.CreatedFromEventID,
		E.EventDescription, E.EventVenue, E.MustDo, E.UserID, E.ShowOnDefaultPage,
		E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview,
		E.CreatedDate, E.CreatedByFullName, E.LastUpdatedDate, E.LastUpdatedByFullName,
		U.FirstName, U.LastName, U.EmailAddress
            */

            HyperLink deleteGoalButton = e.Item.FindControl("deleteGoalButton") as HyperLink;
            deleteGoalButton.NavigateUrl = "allGoals.aspx?A=Delete&EID=" + row["EventID"].ToString();
        }
    }

    //===============================================================
    // Function: allGoalsRepeater_ItemCommand
    //===============================================================
    protected void allGoalsRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        //int addressBookID = int.Parse(e.CommandArgument.ToString());
    }

    //===============================================================
    // Function: cancelButton_Click
    //===============================================================
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("main.aspx");
    }
}

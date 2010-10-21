//===============================================================
// Filename: search.aspx.cs
// Date: 28/09/09
// --------------------------------------------------------------
// Description:
//   Search results
//   This page is only used for anonymous user searches
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 28/09/09
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
using System.Text;
using Sedogo.BusinessObjects;

public partial class search : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = -1;
            if (Session["loggedInUserID"] != null)
            {
                userID = int.Parse(Session["loggedInUserID"].ToString());
            }

            string searchText = "";
            if (Request.QueryString["Search"] != null)
            {
                searchText = (string)Request.QueryString["Search"];
            }

            //SedogoUser user = new SedogoUser("", userID);
            bannerAddFindControl.userID = userID;

            timelineURL.Text = "timelineSearchXML.aspx?Search=" + searchText;

            //DateTime timelineStartDate = DateTime.Now.AddMonths(8);
            DateTime timelineStartDate = DateTime.Now.AddYears(4);

            timelineStartDate1.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");     // "Jan 08 2010 00:00:00 GMT"
            timelineStartDate2.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");

            int searchCount = GetSearchResultCount(searchText);
            if (searchCount >= 50)
            {
                moreThan50ResultsDiv.Visible = true;
            }
            else
            {
                moreThan50ResultsDiv.Visible = false;
            }
            if (searchCount == 0)
            {
                noSearchResultsDiv.Visible = true;
            }
            else
            {
                noSearchResultsDiv.Visible = false;
            }

            //what.Attributes.Add("onkeypress", "checkAddButtonEnter(event);");

            //searchButton1.Attributes.Add("onmouseover", "this.src='images/addButtonRollover.png'");
            //searchButton1.Attributes.Add("onmouseout", "this.src='images/addButton.png'");
            //searchButton2.Attributes.Add("onmouseover", "this.src='images/searchButtonRollover.png'");
            //searchButton2.Attributes.Add("onmouseout", "this.src='images/searchButton.png'");
        }
    }

    //===============================================================
    // Function: GetSearchResultCount
    //===============================================================
    private int GetSearchResultCount(string searchText)
    {
        int searchCount = 0;
        int userID = -1;

        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSearchEvents";
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            cmd.Parameters.Add("@SearchText", SqlDbType.NVarChar, 1000).Value = searchText;
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                searchCount++;
            }
            rdr.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }

        return searchCount;
    }

    //===============================================================
    // Function: searchButton_click
    //===============================================================
    //protected void searchButton_click(object sender, EventArgs e)
    //{
    //    string searchText = what2.Text;

    //    if (searchText.Trim() == "" || searchText.Trim() == "name your goal")
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a search term\");", true);
    //    }
    //    else
    //    {
    //        if (searchText.Length >= 2)
    //        {
    //            Response.Redirect("search.aspx?Search=" + searchText.ToString());
    //        }
    //        else
    //        {
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a longer search term\");", true);
    //        }
    //    }
    //}
}

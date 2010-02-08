//===============================================================
// Filename: defaultPageTimelineList.aspx.cs
// Date: 13/01/10
// --------------------------------------------------------------
// Description:
//   Goal list
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 13/01/10
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

public partial class admin_defaultPageTimelineList : AdminPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
                SqlCommand cmd = new SqlCommand("spSelectAdministratorsEventList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                goalsListGrid.DataSource = ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    //===============================================================
    // Function: onRowSelect
    //===============================================================
    protected void onRowSelect(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
        Response.Redirect("editEvent.aspx?EID=" + e.Item["EventID"]);
    }
}

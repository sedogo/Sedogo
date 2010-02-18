//===============================================================
// Filename: getInspired.aspx.cs
// Date: 11/01/2010
// --------------------------------------------------------------
// Description:
//   Default
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 11/01/2010
// Revision history:
//===============================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Sedogo.BusinessObjects;

public partial class getInspired : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            what.Attributes.Add("onkeypress", "checkAddButtonEnter(event);");

            searchButton1.Attributes.Add("onmouseover", "this.src='images/addButtonRollover.png'");
            searchButton1.Attributes.Add("onmouseout", "this.src='images/addButton.png'");
            searchButton1.Attributes.Add("onclick", "doAddEvent();");
            searchButton2.Attributes.Add("onmouseover", "this.src='images/searchButtonRollover.png'");
            searchButton2.Attributes.Add("onmouseout", "this.src='images/searchButton.png'");
        }
    }

    //===============================================================
    // Function: searchButton_click
    //===============================================================
    protected void searchButton_click(object sender, EventArgs e)
    {
        string searchText = what2.Text;

        if (searchText.Trim() == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a search term\");", true);
        }
        else
        {
            if (searchText.Length >= 2)
            {
                Response.Redirect("search.aspx?Search=" + searchText.ToString());
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a longer search term\");", true);
            }
        }
    }
}

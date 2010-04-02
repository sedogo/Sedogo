//===============================================================
// Filename: bannerAddFindControl.aspx.cs
// Date: 22/03/10
// --------------------------------------------------------------
// Description:
//   
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 22/03/10
// Revision history:
//===============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class components_bannerAddFindControl : System.Web.UI.UserControl
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        what.Attributes.Add("onkeypress", "checkAddButtonEnter(event);");

        searchButton1.Attributes.Add("onmouseover", "this.src='images/addButtonRollover.png'");
        searchButton1.Attributes.Add("onmouseout", "this.src='images/addButton.png'");
        searchButton2.Attributes.Add("onmouseover", "this.src='images/searchButtonRollover.png'");
        searchButton2.Attributes.Add("onmouseout", "this.src='images/searchButton.png'");
    }

    //===============================================================
    // Function: searchButton_click
    //===============================================================
    protected void searchButton_click(object sender, EventArgs e)
    {
        string searchText = what.Text;

        if (searchText.Trim() == "" || searchText.Trim() == "e.g. climb Everest")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a search term\");", true);
        }
        else
        {
            if (searchText.Length >= 2)
            {
                Response.Redirect("search2.aspx?Search=" + searchText.ToString());
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a longer search term\");", true);
            }
        }
    }

    //===============================================================
    // Function: searchButton2_click
    //===============================================================
    protected void searchButton2_click(object sender, EventArgs e)
    {
        string searchString = what2.Text;

        Response.Redirect("search2.aspx?Search=" + searchString);
    }
}

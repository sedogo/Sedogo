﻿//===============================================================
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
using Sedogo.BusinessObjects;

public partial class components_bannerAddFindControl : System.Web.UI.UserControl
{
    public int          userID = -1;

    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (userID > 0)
        {
            addLink.NavigateUrl = "javascript:doAddEvent()";
            searchButton1.NavigateUrl = "javascript:doAddEvent()";
            keyPressActionScript.Text = "javascript:doAddEvent()";
        }
        else
        {
            addLink.NavigateUrl = "javascript:doLogin()";
            searchButton1.NavigateUrl = "javascript:doLogin()";
            keyPressActionScript.Text = "javascript:doLogin()";
        }
        what.Attributes.Add("onkeypress", "checkAddButtonEnter(event);");

        searchButton1.Attributes.Add("onmouseover", "this.src='/images/addButtonRollover.png'");
        searchButton1.Attributes.Add("onmouseout", "this.src='/images/addButton.png'");
        searchButton2.Attributes.Add("onmouseover", "this.src='/images/searchButtonRollover.png'");
        searchButton2.Attributes.Add("onmouseout", "this.src='/images/searchButton.png'");
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
                if (userID > 0)
                {
                    Response.Redirect("search2.aspx?Search=" + searchText.ToString());
                }
                else
                {
                    Response.Redirect("search.aspx?Search=" + searchText.ToString());
                }
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

        if (userID > 0)
        {
            Response.Redirect("search2.aspx?Search=" + searchString);
        }
        else
        {
            Response.Redirect("search.aspx?Search=" + searchString);
        }
    }
}

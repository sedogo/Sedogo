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

public partial class components_bannerAddFindControl : System.Web.UI.UserControl
{
    public int          userID = -1;

    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        int uid;
        if (Session["loggedInUserID"] != null && int.TryParse(Session["loggedInUserID"].ToString(), out uid) && uid > 0)
        {
            this.userID = uid;
        }
        else
        {
            this.userID = -1;
        } 
        if (userID > 0)
        {
            addLink.NavigateUrl = "javascript:doAddEvent()";
            addButtonLink.NavigateUrl = "javascript:doAddEvent()";
            keyPressActionScript.Text = "javascript:doAddEvent()";
        }
        else
        {
            addLink.NavigateUrl = "javascript:doLogin()";
            addButtonLink.NavigateUrl = "javascript:doLogin()";
            keyPressActionScript.Text = "javascript:doLogin()";
        }
        what.Attributes.Add("onkeypress", "return checkAddButtonEnter(event);");

        what.Attributes.Add("onfocus", "clearWhat();");
        what2.Attributes.Add("onfocus", "clearWhat2();");

        addButtonImage.Attributes.Add("onmouseover", "this.src='" + ResolveUrl("~/images/addButtonRollover.png") + "'");
        addButtonImage.Attributes.Add("onmouseout", "this.src='" + ResolveUrl("~/images/addButton.png") + "'");
        searchButton2.Attributes.Add("onmouseover", "this.src='" + ResolveUrl("~/images/searchButtonRollover.png") + "'");
        searchButton2.Attributes.Add("onmouseout", "this.src='" + ResolveUrl("~/images/searchButton.png") + "'");
    }

    //===============================================================
    // Function: searchButton_click
    //===============================================================
    protected void searchButton_click(object sender, EventArgs e)
    {
        int userID = -1;
        if (Session["loggedInUserID"] != null)
        {
            userID = int.Parse(Session["loggedInUserID"].ToString());
        }

        string searchText = what.Text;

        if (searchText.Trim() == "" || searchText.Trim() == "name your goal")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a search term\");", true);
        }
        else
        {
            if (searchText.Length >= 2)
            {
                if (userID > 0)
                {
                    Response.Redirect("~/search2.aspx?Search=" + searchText.ToString());
                }
                else
                {
                    Response.Redirect("~/search.aspx?Search=" + searchText.ToString());
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
        int uid = -1;
        if (Session["loggedInUserID"] != null)
        {
            uid = int.Parse(Session["loggedInUserID"].ToString());
        }

        string searchString = what2.Text;

        if (uid > 0)
        {
            Response.Redirect("~/search2.aspx?Search=" + searchString);
        }
        else
        {
            Response.Redirect("~/search.aspx?Search=" + searchString);
        }
    }
}

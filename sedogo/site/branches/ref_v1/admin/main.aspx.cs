using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class admin_main : AdminPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["loggedInAdministratorName"] != null)
            {
                loggedInAsLabel.Text = Session["loggedInAdministratorName"].ToString();
            }
        }
    }

    //===============================================================
    // Function: logoutLink_click
    //===============================================================
    protected void logoutLink_click(object sender, EventArgs e)
    {
        Response.Redirect("logout.aspx");
    }

    //===============================================================
    // Function: administratorsLink_click
    //===============================================================
    protected void administratorsLink_click(object sender, EventArgs e)
    {
        Response.Redirect("administratorsList.aspx");
    }

    //===============================================================
    // Function: usersLink_click
    //===============================================================
    protected void usersLink_click(object sender, EventArgs e)
    {
        Response.Redirect("usersList.aspx");
    }

    //===============================================================
    // Function: timezonesLink_click
    //===============================================================
    protected void timezonesLink_click(object sender, EventArgs e)
    {
        Response.Redirect("timezonesList.aspx");
    }

    //===============================================================
    // Function: popularGoalsLink_click
    //===============================================================
    protected void popularGoalsLink_click(object sender, EventArgs e)
    {
        Response.Redirect("popularGoals.aspx");
    }

    //===============================================================
    // Function: defaultPageTimelineLink_click
    //===============================================================
    protected void defaultPageTimelineLink_click(object sender, EventArgs e)
    {
        Response.Redirect("defaultPageTimelineList.aspx");
    }

    //===============================================================
    // Function: homePageContentLink_click
    //===============================================================
    protected void homePageContentLink_click(object sender, EventArgs e)
    {
        Response.Redirect("homePageContent.aspx");
    }

    //===============================================================
    // Function: broadcastEmailLink_click
    //===============================================================
    protected void broadcastEmailLink_click(object sender, EventArgs e)
    {
        Response.Redirect("broadcastEmailContent.aspx");
    }

    //===============================================================
    // Function: allGoalsLink_click
    //===============================================================
    protected void allGoalsLink_click(object sender, EventArgs e)
    {
        Response.Redirect("allGoals.aspx");
    }
}

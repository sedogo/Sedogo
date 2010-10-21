using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userTimelineRedirect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int userID = int.Parse(Request.QueryString["UID"].ToString());
        redirectScript.Text = "top.location.href = \"userTimeline.aspx?UID=" + userID.ToString() + "\";";
    }
}

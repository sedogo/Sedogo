using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class viewEventRedirect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"].ToString());
        redirectScript.Text = "top.location.href = \"viewEvent.aspx?EID=" + eventID.ToString() + "\";";
    }
}

//===============================================================
// Filename: editEvent.aspx.cs
// Date: 13/01/10
// --------------------------------------------------------------
// Description:
//   editEvent
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
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Sedogo.BusinessObjects;

public partial class admin_editEvent : AdminPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);

            SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInAdministratorName"].ToString(),
                eventID);
            eventNameLabel.Text = sedogoEvent.eventName;
            showOnDefaultPageCheckbox.Checked = sedogoEvent.showOnDefaultPage;
        }
    }

    //===============================================================
    // Function: saveButton_Click
    //===============================================================
    protected void saveButton_Click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInAdministratorName"].ToString(),
            eventID);
        sedogoEvent.showOnDefaultPage = showOnDefaultPageCheckbox.Checked;
        sedogoEvent.Update();
    }

    //===============================================================
    // Function: cancelButton_Click
    //===============================================================
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("defaultPageTimelineList.aspx");
    }
}

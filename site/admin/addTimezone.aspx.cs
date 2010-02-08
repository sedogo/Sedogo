//===============================================================
// Filename: addTimezone.aspx.cs
// Date: 09/11/09
// --------------------------------------------------------------
// Description:
//   Add timezone
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 09/11/09
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

public partial class admin_addTimezone : AdminPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    //===============================================================
    // Function: saveButton_Click
    //===============================================================
    protected void saveButton_Click(object sender, EventArgs e)
    {
        SedogoTimezone timezone = new SedogoTimezone(Session["loggedInAdministratorName"].ToString());
        timezone.shortCode = shortCodeTextBox.Text;
        timezone.description = descriptionTextBox.Text;
        timezone.GMTOffset = int.Parse(offsetTextBox.Text);
        timezone.Add();

        Response.Redirect("editTimezone.aspx?TID=" + timezone.timezoneID.ToString());
    }

    //===============================================================
    // Function: cancelButton_Click
    //===============================================================
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("timezonesList.aspx");
    }
}

//===============================================================
// Filename: editTimezone.aspx.cs
// Date: 09/11/09
// --------------------------------------------------------------
// Description:
//   Edit timezone
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

public partial class admin_editTimezone : AdminPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int timezoneID = int.Parse(Request.QueryString["TID"]);

            SedogoTimezone timezone = new SedogoTimezone(Session["loggedInAdministratorName"].ToString(),
                timezoneID);
            shortCodeTextBox.Text = timezone.shortCode;
            descriptionTextBox.Text = timezone.description;
            offsetTextBox.Text = timezone.GMTOffset.ToString();
        }
    }

    //===============================================================
    // Function: saveButton_Click
    //===============================================================
    protected void saveButton_Click(object sender, EventArgs e)
    {
        int timezoneID = int.Parse(Request.QueryString["TID"]);

        SedogoTimezone timezone = new SedogoTimezone(Session["loggedInAdministratorName"].ToString(),
            timezoneID);
        timezone.shortCode = shortCodeTextBox.Text;
        timezone.description = descriptionTextBox.Text;
        timezone.GMTOffset = int.Parse(offsetTextBox.Text);
        timezone.Update();
    }

    //===============================================================
    // Function: cancelButton_Click
    //===============================================================
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("timezonesList.aspx");
    }
}

//===============================================================
// Filename: broadcastEmailContent.aspx
// Date: 18/07/10
// --------------------------------------------------------------
// Description:
//   
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 18/07/10
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
using System.Net.Mail;
using System.Text;
using System.IO;
using Sedogo.BusinessObjects;

public partial class admin_broadcastEmailContent : AdminPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GlobalData gd = new GlobalData((string)Session["loggedInUserFullName"]);
            emailContentTextBox.Text = gd.GetStringValue("BroadcastEmailContent");
            emailSubjectTextBox.Text = gd.GetStringValue("BroadcastEmailSubject");
        }
    }

    //===============================================================
    // Function: saveButton_Click
    //===============================================================
    protected void saveButton_Click(object sender, EventArgs e)
    {
        GlobalData gd = new GlobalData((string)Session["loggedInUserFullName"]);
        gd.UpdateStringValue("BroadcastEmailContent", emailContentTextBox.Text.Trim());
        gd.UpdateStringValue("BroadcastEmailSubject", emailSubjectTextBox.Text.Trim());

        gd.UpdateStringValue("BroadcastEmailWaiting", "Y");

        Response.Redirect("main.aspx");
    }
    
    //===============================================================
    // Function: sendTestEmailButton_Click
    //===============================================================
    protected void sendTestEmailButton_Click(object sender, EventArgs e)
    {
        GlobalData gd = new GlobalData((string)Session["loggedInUserFullName"]);
        gd.UpdateStringValue("BroadcastEmailContent", emailContentTextBox.Text.Trim());
        gd.UpdateStringValue("BroadcastEmailSubject", emailSubjectTextBox.Text.Trim());

        gd.UpdateStringValue("BroadcastEmailWaiting", "N");

        MiscUtils.SendBroadcastTestEmail(testEmailAddressTextBox.Text);

        Response.Redirect("main.aspx");
    }

    //===============================================================
    // Function: cancelButton_Click
    //===============================================================
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("main.aspx");
    }
}

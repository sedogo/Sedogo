//===============================================================
// Filename: feedbackThanks.aspx
// Date: 10/05/10
// --------------------------------------------------------------
// Description:
//   Send feedback
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 10/05/10
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
using Sedogo.BusinessObjects;

public partial class feedbackThanks : System.Web.UI.Page
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
    // Function: continueButton_click
    //===============================================================
    protected void continueButton_click(object sender, EventArgs e)
    {
        Response.Redirect("profile.aspx");
    }
}

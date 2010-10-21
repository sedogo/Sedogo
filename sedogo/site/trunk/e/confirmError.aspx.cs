//===============================================================
// Filename: confirmError.aspx.cs
// Date: 28/08/09
// --------------------------------------------------------------
// Description:
//   
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 28/08/09
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

public partial class e_confirmError : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if( !IsPostBack )
        {
            string sedogoConfirmError = "";
            if( Session["SedogoConfirmError"] != null )
            {
                sedogoConfirmError = Session["SedogoConfirmError"].ToString();
            }
            
            switch(sedogoConfirmError)
            {
                case "AlreadyActivated":
                    confirmErrorText.Text = "This account has already been activated";
                    break;
                case "InvalidUser":
                    confirmErrorText.Text = "Account not found";
                    break;
                default:
                    confirmErrorText.Text = "Unknown error";
                    break;
            }
        }
    }
}

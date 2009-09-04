//===============================================================
// Filename: /e/default.aspx.cs
// Date: 20/08/09
// --------------------------------------------------------------
// Description:
//   
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 20/08/09
// Revision history:
//===============================================================

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
using Sedogo.BusinessObjects;

public partial class e_default : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string GUID = "";
            if (Request.QueryString["G"] != null)
            {
                GUID = (string)Request.QueryString["G"];
            }

            int userID = -1;
            if (GUID != "")
            {
                userID = SedogoUser.GetUserIDFromGUID(GUID);
            }

            if (userID > 0)
            {
                SedogoUser newUser = new SedogoUser("", userID);
                if (newUser.loginEnabled == true)
                {
                    Session["SedogoConfirmError"] = "AlreadyActivated";
                    Response.Redirect("confirmError.aspx");
                }
                else
                {
                    newUser.loginEnabled = true;
                    newUser.Update();
                }
            }
            else
            {
                Session["SedogoConfirmError"] = "InvalidUser";
                Response.Redirect("confirmError.aspx");
            }
        }
    }
}

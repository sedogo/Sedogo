﻿//===============================================================
// Filename: editUser.aspx.cs
// Date: 20/08/09
// --------------------------------------------------------------
// Description:
//   Edit user
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
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Sedogo.BusinessObjects;

public partial class admin_editUser : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Request.QueryString["UID"]);

            SedogoUser sedogoUser = new SedogoUser(Session["loggedInAdministratorName"].ToString(),
                userID);
            firstNameTextBox.Text = sedogoUser.firstName;
            lastNameTextBox.Text = sedogoUser.lastName;
            emailAddress.Text = sedogoUser.emailAddress;
        }
    }

    //===============================================================
    // Function: saveButton_Click
    //===============================================================
    protected void saveButton_Click(object sender, EventArgs e)
    {
        int userID = int.Parse(Request.QueryString["UID"]);

        SedogoUser sedogoUser = new SedogoUser(Session["loggedInAdministratorName"].ToString(),
            userID);
        sedogoUser.firstName = firstNameTextBox.Text;
        sedogoUser.lastName = lastNameTextBox.Text;
        sedogoUser.emailAddress = emailAddress.Text;
        sedogoUser.Update();

        string newPassword = userPassword.Text.Trim();
        if (newPassword != "")
        {
            sedogoUser.UpdatePassword(userPassword.Text);
        }
        userPassword.Text = "";
    }

    //===============================================================
    // Function: cancelButton_Click
    //===============================================================
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("usersList.aspx");
    }
}

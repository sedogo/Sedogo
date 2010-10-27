//===============================================================
// Filename: addAddressBook.aspx.cs
// Date: 01/05/10
// --------------------------------------------------------------
// Description:
//   Add administrator
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 01/05/10
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

public partial class addAddressBook : SedogoPage
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
        int userID = int.Parse(Session["loggedInUserID"].ToString());

        AddressBook addressBookEntry = new AddressBook(Session["loggedInUserFullName"].ToString());
        addressBookEntry.userID = userID;
        addressBookEntry.firstName = firstNameTextBox.Text;
        addressBookEntry.lastName = lastNameTextBox.Text;
        addressBookEntry.emailAddress = emailAddress.Text;
        addressBookEntry.Add();

        //Response.Redirect("editAddressBook.aspx?ABID=" + addressBookEntry.addressBookID.ToString());
        Response.Redirect("addressBook.aspx");
    }

    //===============================================================
    // Function: cancelButton_Click
    //===============================================================
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("addressBook.aspx");
    }
}

//===============================================================
// Filename: editAddressBook.aspx.cs
// Date: 01/05/10
// --------------------------------------------------------------
// Description:
//   Edit address book
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

public partial class editAddressBook : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int addressBookID = int.Parse(Request.QueryString["ABID"].ToString());

            AddressBook addressBookEntry = new AddressBook(Session["loggedInUserFullName"].ToString(),
                addressBookID);

            firstNameTextBox.Text = addressBookEntry.firstName;
            lastNameTextBox.Text = addressBookEntry.lastName;
            emailAddress.Text = addressBookEntry.emailAddress;
        }
    }

    //===============================================================
    // Function: saveButton_Click
    //===============================================================
    protected void saveButton_Click(object sender, EventArgs e)
    {
        int addressBookID = int.Parse(Request.QueryString["ABID"].ToString());
        int userID = int.Parse(Session["loggedInUserID"].ToString());

        AddressBook addressBookEntry = new AddressBook(Session["loggedInUserFullName"].ToString(),
            addressBookID);
        addressBookEntry.firstName = firstNameTextBox.Text;
        addressBookEntry.lastName = lastNameTextBox.Text;
        addressBookEntry.emailAddress = emailAddress.Text;
        addressBookEntry.Update();

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

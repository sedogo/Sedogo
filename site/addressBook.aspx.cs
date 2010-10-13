//===============================================================
// Filename: addressBook.aspx.cs
// Date: 27/03/10
// --------------------------------------------------------------
// Description:
//   addressBook
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 27/03/10
// Revision history:
//===============================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Sedogo.BusinessObjects;

public partial class addressBook : SedogoPage
{
    private ArrayList dupEmailArrayList = new ArrayList();

    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Session["loggedInUserID"].ToString());

            string action = "";
            if (Request.QueryString["A"] != null)
            {
                action = Request.QueryString["A"].ToString();
                int addressBookID = -1;
                if (Request.QueryString["ABID"] != null)
                {
                    addressBookID = int.Parse(Request.QueryString["ABID"]);
                }
                int addressBookUserID = -1;
                if (Request.QueryString["UID"] != null)
                {
                    addressBookUserID = int.Parse(Request.QueryString["UID"]);
                }

                if (action == "Delete")
                {
                    try
                    {
                        AddressBook addressBook = new AddressBook(Session["loggedInUserFullName"].ToString(),
                            addressBookID);
                        addressBook.Delete();
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert",
                            "alert(\"Error: " + ex.Message + "\");", true);
                    }
                }
                if (action == "Add")
                {
                    SedogoUser newAddressBookUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), addressBookUserID);

                    AddressBook addressBookEntry = new AddressBook(Session["loggedInUserFullName"].ToString());
                    addressBookEntry.userID = userID;
                    addressBookEntry.firstName = newAddressBookUser.firstName;
                    addressBookEntry.lastName = newAddressBookUser.lastName;
                    addressBookEntry.emailAddress = newAddressBookUser.emailAddress;
                    addressBookEntry.Add();
                }
            }

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
            sidebarControl.userID = userID;
            sidebarControl.user = user;
            bannerAddFindControl.userID = userID;

            PopulateAddressBook(userID);
        }
    }

    private struct AddressBookEntry
    {
        public int _addressBookUserID;
        public int _addressBookID;
        public string _firstName;
        public string _lastName;
        public string _emailAddress;

        public AddressBookEntry(int addressBookUserID, int addressBookID, string firstName,
            string lastName, string emailAddress)
        {
            _addressBookUserID = addressBookUserID;
            _addressBookID = addressBookID;
            _firstName = firstName;
            _lastName = lastName;
            _emailAddress = emailAddress;
        }
    }

    //===============================================================
    // Function: PopulateAddressBook
    //===============================================================
    private void PopulateAddressBook(int userID)
    {
        int contactCount = AddressBook.GetAddressBookCountByUser(userID);

        if (contactCount > 0)
        {
            noContactsDiv.Visible = false;
            contactsDiv.Visible = true;
        }
        else
        {
            noContactsDiv.Visible = true;
            contactsDiv.Visible = false;
        }

        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            ArrayList addressBookList = new ArrayList();

            SqlCommand cmd = new SqlCommand("spSelectAddressBookList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int addressBookUserID = int.Parse(rdr["UserID"].ToString());
                int addressBookID = int.Parse(rdr["AddressBookID"].ToString());
                string firstName = (string)rdr["FirstName"];
                string lastName = (string)rdr["LastName"];
                string emailAddress = (string)rdr["EmailAddress"];

                addressBookList.Add(new AddressBookEntry(addressBookUserID, addressBookID, firstName, lastName, emailAddress));
            }
            rdr.Close();

            addressBookRepeater.DataSource = addressBookList;
            addressBookRepeater.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //===============================================================
    // Function: addressBookRepeater_ItemDataBound
    //===============================================================
    protected void addressBookRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null &&
            (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink nameLabel = e.Item.FindControl("nameLabel") as HyperLink;
            nameLabel.NavigateUrl = "editAddressBook.aspx?ABID=" + ((AddressBookEntry)e.Item.DataItem)._addressBookID.ToString();
            nameLabel.Text = ((AddressBookEntry)e.Item.DataItem)._firstName + " " + ((AddressBookEntry)e.Item.DataItem)._lastName;

            HyperLink emailLabel = e.Item.FindControl("emailLabel") as HyperLink;
            emailLabel.NavigateUrl = "editAddressBook.aspx?ABID=" + ((AddressBookEntry)e.Item.DataItem)._addressBookID.ToString();
            emailLabel.Text = ((AddressBookEntry)e.Item.DataItem)._emailAddress;

            HyperLink editContactButton = e.Item.FindControl("editContactButton") as HyperLink;
            editContactButton.NavigateUrl = "editAddressBook.aspx?ABID=" + ((AddressBookEntry)e.Item.DataItem)._addressBookID.ToString();

            HyperLink deleteContactButton = e.Item.FindControl("deleteContactButton") as HyperLink;
            deleteContactButton.NavigateUrl = "addressBook.aspx?ABID=" + ((AddressBookEntry)e.Item.DataItem)._addressBookID.ToString() + "&A=Delete";

            HyperLink sendMessageButton = e.Item.FindControl("sendMessageButton") as HyperLink;
            HyperLink viewProfileButton = e.Item.FindControl("viewProfileButton") as HyperLink;
            HyperLink addToAddressBookButton = e.Item.FindControl("addToAddressBookButton") as HyperLink;

            Label seperator1 = e.Item.FindControl("seperator1") as Label;
            Label seperator2 = e.Item.FindControl("seperator2") as Label;
            Label seperator3 = e.Item.FindControl("seperator3") as Label;
            Label seperator4 = e.Item.FindControl("seperator4") as Label;

            if (((AddressBookEntry)e.Item.DataItem)._addressBookID < 0)
            {
                // From the tracker list, and not in my address book
                viewProfileButton.Visible = false;
                sendMessageButton.Visible = false;
                emailLabel.Text = "(Hidden)";
                emailLabel.NavigateUrl = "#";
                emailLabel.CssClass = "";
                emailLabel.Enabled = false;
                editContactButton.Visible = false;
                deleteContactButton.Visible = false;

                addToAddressBookButton.Visible = false;
                addToAddressBookButton.NavigateUrl = "addressBook.aspx?UID=" + ((AddressBookEntry)e.Item.DataItem)._addressBookUserID.ToString() + "&A=Add";
            }
            else
            {
                // From address book
                addToAddressBookButton.Visible = false;
            }

            int userID = SedogoUser.GetUserIDFromEmailAddress(((AddressBookEntry)e.Item.DataItem)._emailAddress.ToString());
            if (userID > 0)
            {
                viewProfileButton.NavigateUrl = "userProfile.aspx?UID=" + userID.ToString();
                viewProfileButton.Visible = true;
            }
            else
            {
                viewProfileButton.Visible = false;
            }

            if (userID > 0)
            {
                sendMessageButton.NavigateUrl = "sendUserMessage.aspx?UID=" + userID.ToString() + "&EID=-1";
                sendMessageButton.Visible = true;
            }
            else
            {
                sendMessageButton.Visible = false;
            }

            if( sendMessageButton.Visible == true && viewProfileButton.Visible == true )
            {
                seperator1.Visible = true;
            }
            if( viewProfileButton.Visible == true && editContactButton.Visible == true )
            {
                seperator2.Visible = true;
            }
            if( editContactButton.Visible == true && deleteContactButton.Visible == true )
            {
                seperator3.Visible = true;
            }
            if( deleteContactButton.Visible == true && addToAddressBookButton.Visible == true )
            {
                seperator4.Visible = true;
            }
        }
    }

    //===============================================================
    // Function: addressBookRepeater_ItemCommand
    //===============================================================
    protected void addressBookRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int addressBookID = int.Parse(e.CommandArgument.ToString());
    }

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        Response.Redirect("profile.aspx");
    }
}

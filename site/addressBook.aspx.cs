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
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Session["loggedInUserID"].ToString());

            SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
            sidebarControl.userID = userID;
            sidebarControl.user = user;
            bannerAddFindControl.userID = userID;

            PopulateAddressBook(userID);
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

        try
        {
            SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

            SqlCommand cmd = new SqlCommand("spSelectAddressBookList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

            cmd.CommandTimeout = 90;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            addressBookRepeater.DataSource = ds;
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
            DataRowView row = e.Item.DataItem as DataRowView;

            HyperLink nameLabel = e.Item.FindControl("nameLabel") as HyperLink;
            nameLabel.NavigateUrl = "viewAddressBook.aspx?ABID=" + row["AddressBookID"].ToString();
            nameLabel.Text = row["FirstName"].ToString() + " " + row["LastName"].ToString();

            HyperLink emailLabel = e.Item.FindControl("emailLabel") as HyperLink;
            emailLabel.NavigateUrl = "viewAddressBook.aspx?ABID=" + row["AddressBookID"].ToString();
            emailLabel.Text = row["EmailAddress"].ToString();

            HyperLink editContactButton = e.Item.FindControl("editContactButton") as HyperLink;
            editContactButton.NavigateUrl = "editAddressBook.aspx?ABID=" + row["AddressBookID"].ToString();

            /*

            Image eventPicThumbnailImage = e.Item.FindControl("eventPicThumbnailImage") as Image;
            string eventPicThumbnail = row["EventPicThumbnail"].ToString();
            if (eventPicThumbnail == "")
            {
                eventPicThumbnailImage.ImageUrl = "./images/eventThumbnailBlank.png";
            }
            else
            {
                eventPicThumbnailImage.ImageUrl = "./assets/eventPics/" + eventPicThumbnail;
            }
            */
        }
    }

    //===============================================================
    // Function: addressBookRepeater_ItemCommand
    //===============================================================
    protected void addressBookRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int addressBookID = int.Parse(e.CommandArgument.ToString());

    }
}

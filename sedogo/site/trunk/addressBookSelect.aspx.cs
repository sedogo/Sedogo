//===============================================================
// Filename: addressBookSelect.aspx.cs
// Date: 24/06/10
// --------------------------------------------------------------
// Description:
//   addressBook
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 24/06/10
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

public partial class addressBookSelect : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userID = int.Parse(Session["loggedInUserID"].ToString());
            string parentFieldName = Request.QueryString["F"];

            PopulateAddressBook(userID);

            clickScript.Text = "window.parent.document.form1." + parentFieldName + ".value = emailAddress;";
        }
    }

    //===============================================================
    // Function: PopulateAddressBook
    //===============================================================
    private void PopulateAddressBook(int userID)
    {
        int contactCount = AddressBook.GetAddressBookCountByUser(userID);

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
            nameLabel.NavigateUrl = "javascript:clickEntry('" + row["EmailAddress"].ToString() + "')";
            nameLabel.Text = row["FirstName"].ToString() + " " + row["LastName"].ToString();

            Image picThumbnailImage = e.Item.FindControl("picThumbnailImage") as Image;

            int userID = SedogoUser.GetUserIDFromEmailAddress(row["EmailAddress"].ToString());
            if (userID > 0)
            {
                SedogoUser addressBookUser = new SedogoUser("", userID);

                if (addressBookUser.profilePicThumbnail != "")
                {
                    picThumbnailImage.ImageUrl = "~/assets/profilePics/" + addressBookUser.profilePicThumbnail;
                }
                else
                {
                    if (addressBookUser.avatarNumber > 0)
                    {
                        picThumbnailImage.ImageUrl = "~/images/avatars/avatar" + addressBookUser.avatarNumber.ToString() + "sm.gif";
                    }
                    else
                    {
                        if (addressBookUser.gender == "M")
                        {
                            // 1,2,5
                            int avatarID = 5;
                            switch ((addressBookUser.userID % 6))
                            {
                                case 0: case 1: avatarID = 1; break;
                                case 2: case 3: avatarID = 2; break;
                            }
                            picThumbnailImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                        }
                        else
                        {
                            // 3,4,6
                            int avatarID = 6;
                            switch ((addressBookUser.userID % 6))
                            {
                                case 0: case 1: avatarID = 3; break;
                                case 2: case 3: avatarID = 4; break;
                            }
                            picThumbnailImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                        }
                        //profileImage.ImageUrl = "~/images/profile/blankProfile.jpg";
                    }
                    picThumbnailImage.Height = 50;
                    picThumbnailImage.Width = 50;
                }
            }
            else
            {
                picThumbnailImage.ImageUrl = "~/images/avatars/avatar1sm.gif";
            }

            //HyperLink emailLabel = e.Item.FindControl("emailLabel") as HyperLink;
            //emailLabel.NavigateUrl = "javascript:clickEntry('" + row["EmailAddress"].ToString() + "')";
            //emailLabel.Text = row["EmailAddress"].ToString();

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
}

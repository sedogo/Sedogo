//===============================================================
// Filename: editProfile.aspx.cs
// Date:04/09/09
// --------------------------------------------------------------
// Description:
//   Edit profile
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 04/09/09
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
using System.Globalization;
using Telerik.Web.UI;
using Sedogo.BusinessObjects;

public partial class editProfile : SedogoPage
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

            for (int day = 1; day <= 31; day++)
            {
                dateOfBirthDay.Items.Add(new ListItem(day.ToString(), day.ToString()));
            }
            for (int month = 1; month <= 12; month++)
            {
                DateTime loopDate = new DateTime(DateTime.Now.Year, month, 1);
                dateOfBirthMonth.Items.Add(new ListItem(loopDate.ToString("MMMM", CultureInfo.InvariantCulture), month.ToString()));
            }
            for (int year = 1900; year <= DateTime.Now.Year; year++)
            {
                dateOfBirthYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            dateOfBirthDay.Items.Insert(0,new ListItem("", ""));
            dateOfBirthMonth.Items.Insert(0,new ListItem("", ""));
            dateOfBirthYear.Items.Insert(0, new ListItem("", ""));
            hiddenDateOfBirth.Attributes.Add("style","display:none");
            dateOfBirthDay.Attributes.Add("onchange", "setHiddenDateField()");
            dateOfBirthMonth.Attributes.Add("onchange", "setHiddenDateField()");
            dateOfBirthYear.Attributes.Add("onchange", "setHiddenDateField()");

            try
            {
                SqlConnection conn = new SqlConnection((string)Application["connectionString"]);

                SqlCommand cmd = new SqlCommand("spSelectTimezoneList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                timezoneDropDownList.DataSource = ds;
                timezoneDropDownList.DataBind();

                timezoneDropDownList.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            RadComboBoxItem avatarItem1 = new RadComboBoxItem("Avatar 1", "1");
            avatarItem1.ImageUrl = "/images/avatars/avatar1sm.gif";
            avatarComboBox.Items.Add(avatarItem1);
            RadComboBoxItem avatarItem2 = new RadComboBoxItem("Avatar 2", "2");
            avatarItem2.ImageUrl = "/images/avatars/avatar2sm.gif";
            avatarComboBox.Items.Add(avatarItem2);
            RadComboBoxItem avatarItem3 = new RadComboBoxItem("Avatar 3", "3");
            avatarItem3.ImageUrl = "/images/avatars/avatar3sm.gif";
            avatarComboBox.Items.Add(avatarItem3);
            RadComboBoxItem avatarItem4 = new RadComboBoxItem("Avatar 4", "4");
            avatarItem4.ImageUrl = "/images/avatars/avatar4sm.gif";
            avatarComboBox.Items.Add(avatarItem4);
            RadComboBoxItem avatarItem5 = new RadComboBoxItem("Avatar 5", "5");
            avatarItem5.ImageUrl = "/images/avatars/avatar5sm.gif";
            avatarComboBox.Items.Add(avatarItem5);
            RadComboBoxItem avatarItem6 = new RadComboBoxItem("Avatar 6", "6");
            avatarItem6.ImageUrl = "/images/avatars/avatar6sm.gif";
            avatarComboBox.Items.Add(avatarItem6);

            firstNameTextBox.Text = user.firstName;
            lastNameTextBox.Text = user.lastName;
            homeTownTextBox.Text = user.homeTown;
            if( user.gender == "M" )
            {
                genderMaleRadioButton.Checked = true;
            }
            else
            {
                genderFemaleRadioButton.Checked = true;
            }
            emailAddressTextBox.Text = user.emailAddress;
            if (user.birthday > DateTime.MinValue)
            {
                dateOfBirthDay.SelectedValue = user.birthday.Day.ToString();
                dateOfBirthMonth.SelectedValue = user.birthday.Month.ToString();
                dateOfBirthYear.SelectedValue = user.birthday.Year.ToString();
            }
            headlineTextBox.Text = user.profileText;
            timezoneDropDownList.SelectedValue = user.timezoneID.ToString();
            enableEmailCheckbox.Checked = user.enableSendEmails;
            if (user.avatarNumber > 0)
            {
                avatarComboBox.SelectedValue = user.avatarNumber.ToString();
            }
            else
            {
                avatarComboBox.SelectedValue = "1";
            }

            if (user.profilePicThumbnail != "")
            {
                // PD 3/12/10 - Removed this because it shows the wrong image
                //profileImage.ImageUrl = ImageHelper.GetRelativeImagePath(user.userID, user.GUID, ImageType.UserPreview); ;
                profileImage.ImageUrl = "assets/profilePics/" + user.profilePicThumbnail;
            }
            else
            {
                if (user.avatarNumber > 0)
                {
                    profileImage.ImageUrl = "~/images/avatars/avatar" + user.avatarNumber + "sm.gif";
                }
                else
                {
                    if (user.gender == "M")
                    {
                        // 1,2,5
                        int avatarID = 5;
                        switch ((userID % 6))
                        {
                            case 0: case 1: avatarID = 1; break;
                            case 2: case 3: avatarID = 2; break;
                        }
                        profileImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                    }
                    else
                    {
                        // 3,4,6
                        int avatarID = 6;
                        switch ((userID % 6))
                        {
                            case 0: case 1: avatarID = 3; break;
                            case 2: case 3: avatarID = 4; break;
                        }
                        profileImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                    }
                }
            }
            profileImage.ToolTip = user.fullName + "'s profile picture";

            if (Request.QueryString["Pwd"] != null)
            {
                if (Request.QueryString["Pwd"].ToString() == "N")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"changePassword.aspx?Pwd=N\");", true);
                }
            }

            SetFocus(firstNameTextBox);
        }
    }

    //===============================================================
    // Function: saveChangesButton_click
    //===============================================================
    protected void saveChangesButton_click(object sender, EventArgs e)
    {
        string emailAddress = emailAddressTextBox.Text;
        emailAddress = emailAddress.Trim().ToLower();

        DateTime dateOfBirth;
        if (dateOfBirthYear.SelectedIndex > 0 && dateOfBirthMonth.SelectedIndex > 0 
            && dateOfBirthDay.SelectedIndex > 0)
        {
            dateOfBirth = new DateTime(int.Parse(dateOfBirthYear.SelectedValue), 
                int.Parse(dateOfBirthMonth.SelectedValue), int.Parse(dateOfBirthDay.SelectedValue) );
        }
        else
        {
            dateOfBirth = DateTime.MinValue;
        }

        SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), 
            int.Parse(Session["loggedInUserID"].ToString()));

        user.firstName = firstNameTextBox.Text;
        user.lastName = lastNameTextBox.Text;
        user.emailAddress = emailAddress;
        if (genderMaleRadioButton.Checked == true)
        {
            user.gender = "M";
        }
        else
        {
            user.gender = "F";
        }
        user.homeTown = homeTownTextBox.Text;
        if (dateOfBirth > DateTime.MinValue)
        {
            user.birthday = dateOfBirth;
        }
        user.profileText = headlineTextBox.Text;
        user.timezoneID = int.Parse(timezoneDropDownList.SelectedValue);
        user.enableSendEmails = enableEmailCheckbox.Checked;
        user.avatarNumber = int.Parse(avatarComboBox.SelectedValue);
        user.Update();

        Session["loggedInUserFirstName"] = user.firstName;
        Session["loggedInUserLastName"] = user.lastName;
        Session["loggedInUserEmailAddress"] = user.emailAddress;
        Session["loggedInUserFullName"] = user.firstName + " " + user.lastName;

        Response.Redirect("profileRedirect.aspx");
    }

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        Response.Redirect("profile.aspx");
    }
}

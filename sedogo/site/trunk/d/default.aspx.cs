//===============================================================
// Filename: /d/default.aspx.cs
// Date: 08/04/10
// --------------------------------------------------------------
// Description:
//   
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 08/04/10
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
using Sedogo.BusinessObjects;

public partial class d_default : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string letterFilter = "A";
            if( Request.QueryString["L"] != null )
            {
                letterFilter = Request.QueryString["L"].ToString();
            }

            if (letterFilter != "")
            {
                PopulatePeopleNames(letterFilter);
            }

            switch (letterFilter)
            {
                case "A":
                    letterALink.CssClass = "publicPageSelectedLetter";
                    break;
                case "B":
                    letterBLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "C":
                    letterCLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "D":
                    letterDLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "E":
                    letterELink.CssClass = "publicPageSelectedLetter";
                    break;
                case "F":
                    letterFLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "G":
                    letterGLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "H":
                    letterHLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "I":
                    letterILink.CssClass = "publicPageSelectedLetter";
                    break;
                case "J":
                    letterJLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "K":
                    letterKLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "L":
                    letterLLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "M":
                    letterMLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "N":
                    letterNLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "O":
                    letterOLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "P":
                    letterPLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "Q":
                    letterQLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "R":
                    letterRLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "S":
                    letterSLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "T":
                    letterTLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "U":
                    letterULink.CssClass = "publicPageSelectedLetter";
                    break;
                case "V":
                    letterVLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "W":
                    letterWLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "X":
                    letterXLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "Y":
                    letterYLink.CssClass = "publicPageSelectedLetter";
                    break;
                case "Z":
                    letterZLink.CssClass = "publicPageSelectedLetter";
                    break;
            }

            if (Session["loggedInUserID"] != null)
            {
                int userID = int.Parse(Session["loggedInUserID"].ToString());

                sidebarControl.userID = userID;
                SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
                sidebarControl.user = user;
            }
            else
            {
            }
        }
    }

    //===============================================================
    // Function: PopulatePeopleNames
    //===============================================================
    private void PopulatePeopleNames(string letterFilter)
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectUsersWithLastName";
            cmd.Parameters.Add("@LetterFilter", SqlDbType.Char, 1).Value = letterFilter;
            DbDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows == true)
            {
                while (rdr.Read())
                {
                    string firstName = "";
                    string lastName = "";
                    string profilePicThumbnail = "";
                    //string birthday = "";
                    string homeTown = "";
                    int avatarNumber = -1;

                    int userID = int.Parse(rdr["UserID"].ToString());
                    if (!rdr.IsDBNull(rdr.GetOrdinal("FirstName")))
                    {
                        firstName = (string)rdr["FirstName"];
                    }
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LastName")))
                    {
                        lastName = (string)rdr["LastName"];
                    }
                    string gender = (string)rdr["Gender"];
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ProfilePicThumbnail")))
                    {
                        profilePicThumbnail = (string)rdr["ProfilePicThumbnail"];
                    }
                    //if (!rdr.IsDBNull(rdr.GetOrdinal("Birthday")))
                    //{
                    //    birthday = (string)rdr["Birthday"];
                    //}
                    if (!rdr.IsDBNull(rdr.GetOrdinal("HomeTown")))
                    {
                        homeTown = (string)rdr["HomeTown"];
                    }
                    if (!rdr.IsDBNull(rdr.GetOrdinal("AvatarNumber")))
                    {
                        avatarNumber = int.Parse(rdr["AvatarNumber"].ToString());
                    }

                    Literal userLink = new Literal();

                    userLink.Text = "<table border=\"0\" cellspacing=\"2\" cellpadding=\"0\" >";
                    userLink.Text += "<tr><td width=\"50\">";
                    if (profilePicThumbnail == "")
                    {
                        if (avatarNumber > 0)
                        {
                            userLink.Text += "<img src=\"../images/avatars/avatar" + avatarNumber.ToString() + "sm.gif\" />";
                        }
                        else
                        {
                            if (gender == "M")
                            {
                                // 1,2,5
                                int avatarID = 5;
                                switch ((userID % 6))
                                {
                                    case 0: case 1: avatarID = 1; break;
                                    case 2: case 3: avatarID = 2; break;
                                }
                                userLink.Text += "<img src=\"../images/avatars/avatar" + avatarID.ToString() + "sm.gif\" />";
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
                                userLink.Text += "<img src=\"../images/avatars/avatar" + avatarID.ToString() + "sm.gif\" />";
                            }
                        }
                    }
                    else
                    {
                        userLink.Text += "<img src=\"../assets/profilePics/" + profilePicThumbnail + "\" style=\"margin:0 1px 0 1px\" />";
                    }
                    userLink.Text += "</td><td>&nbsp;</td><td>&nbsp;<br/>";
                    userLink.Text += "<a href=\"/publicProfile.aspx?UID=" + userID.ToString() + "\">" + firstName + " " + lastName + "</a>";
                    userLink.Text += "</td></tr></table>";
                    // userProfile.aspx userTimeline.aspx

                    peopleNamePlaceHolder.Controls.Add(userLink);
                }
                noUsersWithThisLastNameLetterDiv.Visible = false;
            }
            else
            {
                noUsersWithThisLastNameLetterDiv.Visible = true;
            }
            rdr.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }
    }
}

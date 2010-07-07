//===============================================================
// Filename: morePictures.aspx.cs
// Date: 05/07/10
// --------------------------------------------------------------
// Description:
//   More pictures
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 05/07/10
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
using System.IO;
using Sedogo.BusinessObjects;

public partial class morePictures : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int eventID = int.Parse(Request.QueryString["EID"]);
            int userID = -1;
            string loggedInUserName = "";
            if (Session["loggedInUserID"] != null)
            {
                userID = int.Parse(Session["loggedInUserID"].ToString());
                loggedInUserName = Session["loggedInUserFullName"].ToString();
            }

            sidebarControl.userID = userID;
            if (userID > 0)
            {
                SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);
                sidebarControl.user = user;
                bannerAddFindControl.userID = userID;
            }
            else
            {
            }

            SedogoEvent sedogoEvent = new SedogoEvent(loggedInUserName, eventID);
            eventTitleLabel.Text = sedogoEvent.eventName;

            if (sedogoEvent.privateEvent == true)
            {
                privateIcon.Visible = true;
            }

            if (sedogoEvent.deleted == true)
            {
                Response.Redirect("~/profile.aspx");
            }

            pageTitleUserName.Text = sedogoEvent.eventName + " pictures : Sedogo : Create your future and connect with others to make it happen";
            string timelineColour = "#cd3301";
            switch (sedogoEvent.categoryID)
            {
                case 1:
                    timelineColour = "#cd3301";
                    break;
                case 2:
                    timelineColour = "#ff0b0b";
                    break;
                case 3:
                    timelineColour = "#ff6801";
                    break;
                case 4:
                    timelineColour = "#ff8500";
                    break;
                case 5:
                    timelineColour = "#d5b21a";
                    break;
                case 6:
                    timelineColour = "#8dc406";
                    break;
                case 7:
                    timelineColour = "#5b980c";
                    break;
                case 8:
                    timelineColour = "#079abc";
                    break;
                case 9:
                    timelineColour = "#5ab6cd";
                    break;
                case 10:
                    timelineColour = "#8a67c1";
                    break;
                case 11:
                    timelineColour = "#e54ecf";
                    break;
                case 12:
                    timelineColour = "#a5369c";
                    break;
                case 13:
                    timelineColour = "#a32672";
                    break;
            }
            pageBannerBarDiv.Style.Add("background-color", timelineColour);

            if (userID > 0)
            {
                if (sedogoEvent.userID != userID)
                {
                    // Viewing someone elses event
                }
                else
                {
                    // Viewing own event

                    addPictureLiteral.Text = "var url = 'addGoalPicture.aspx?EID=" + eventID.ToString() + "';";
                }
            }
            else
            {
                // Setup the window for a user who is not registered/logged in
                if (sedogoEvent.privateEvent == true)
                {
                    // Viewing private events is not permitted
                    Response.Redirect("profile.aspx");
                }
            }

            PopulateImages(eventID);
        }
    }

    //===============================================================
    // Function: PopulateImages
    //===============================================================
    private void PopulateImages(int eventID)
    {
        DateTime loopDate = DateTime.MinValue;
        int columnNumber = 1;

        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectEventPictureList";
            cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int eventPictureID = int.Parse(rdr["EventPictureID"].ToString());
                int postedByUserID = int.Parse(rdr["PostedByUserID"].ToString());
                string imageFilename = (string)rdr["ImageFilename"];
                string imagePreview = (string)rdr["ImagePreview"];
                string imageThumbnail = (string)rdr["ImageThumbnail"];
                DateTime createdDate = (DateTime)rdr["CreatedDate"];

                if (createdDate.DayOfYear != loopDate.DayOfYear)
                {
                    imagesPlaceHolder.Controls.Add(new LiteralControl("<i>" + createdDate.ToString("ddd d MMMM yyyy") + "</i><br/>"));
                }
                loopDate = createdDate;

                imagesPlaceHolder.Controls.Add(new LiteralControl("<img src=\"/assets/eventPics/" + imageThumbnail + "\"/>"));
                imagesPlaceHolder.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));

                columnNumber++;
                if (columnNumber > 4)
                {
                    imagesPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
                }
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

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        Response.Redirect("profile.aspx");
    }
}

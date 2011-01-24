﻿//===============================================================
// Filename: eventPicDetails.aspx
// Date: 07/07/10
// --------------------------------------------------------------
// Description:
//   View pic
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 07/07/10
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
using System.Net.Mail;
using System.Text;
using System.IO;
using Sedogo.BusinessObjects;

public partial class eventPicDetails : System.Web.UI.Page
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

            int imageCount = 0;
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
                    string imageFilename = "";
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ImageFilename")))
                    {
                        imageFilename = (string)rdr["ImageFilename"];
                    }
                    string imagePreview = "";
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ImagePreview")))
                    {
                        imagePreview = (string)rdr["ImagePreview"];
                    }
                    string imageThumbnail = "";
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ImageThumbnail")))
                    {
                        imageThumbnail = (string)rdr["ImageThumbnail"];
                    }
                    string caption = "";
                    if (!rdr.IsDBNull(rdr.GetOrdinal("Caption")))
                    {
                        caption = (string)rdr["Caption"];
                    }

                    var @event = new SedogoEvent(string.Empty, eventID);

                    imagePreview = ResolveUrl(ImageHelper.GetRelativeImagePath(eventPictureID, @event.eventGUID,ImageType.EventPictureThumbnailSlideShow));//  ImageType.EventPicturePreview));

                    sliderImagesLiteral.Text += "slidesX[" + imageCount.ToString() + "] = [\"" + imagePreview + "\", \"" + caption.Replace('"',' ') + "\"];\n";
                    imageCount++;
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

    //===============================================================
    // Function: backButton_click
    //===============================================================
    protected void backButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("morePictures.aspx?EID=" + eventID.ToString());
    }
}

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

            uploadEventImage.Visible = false;
            if (userID > 0)
            {
                if (sedogoEvent.userID != userID)
                {
                    // Viewing someone elses event
                    SedogoUser eventUser = new SedogoUser(Session["loggedInUserFullName"].ToString(), sedogoEvent.userID);
                    goalOwnerNameLabel.Text = eventUser.firstName + " " + eventUser.lastName + "'s goal albums";
                }
                else
                {
                    // Viewing own event
                    addPictureLiteral.Text = "var url = 'addGoalPicture.aspx?EID=" + eventID.ToString() + "';";
                    uploadEventImage.Visible = true;
                    editPicsButton.Visible = true;
                    goalOwnerNameLabel.Text = "My goal albums";
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
                goalOwnerNameLabel.Text = "My goal albums";
            }

            PopulateImages(eventID, sedogoEvent.userID);
        }
    }

    //===============================================================
    // Function: PopulateImages
    //===============================================================
    private void PopulateImages(int eventID, int userID)
    {
        DateTime loopDate = DateTime.MinValue;
        Boolean firstRow = true;
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
                DateTime createdDate = (DateTime)rdr["CreatedDate"];

                if (createdDate.DayOfYear != loopDate.DayOfYear)
                {
                    if (firstRow == false)
                    {
                        imagesPlaceHolder.Controls.Add(new LiteralControl("<div style=\"clear: both;\" />"));
                    }
                    imagesPlaceHolder.Controls.Add(new LiteralControl("<i><span style=\"font-size:80%\">" + createdDate.ToString("ddd d MMMM yyyy") + "</span></i><br/>"));
                    firstRow = false;
                }
                loopDate = createdDate;

                imagesPlaceHolder.Controls.Add(new LiteralControl("<div style=\"width:110px; float:left; margin:0 10px 20px 0\">"));
                imagesPlaceHolder.Controls.Add(new LiteralControl("<a href=\"eventPicDetails.aspx?EID=" + eventID.ToString()
                    + "&EPID=" + eventPictureID.ToString() + "\"><img src=\"/assets/eventPics/" + imageThumbnail + "\"/></a>"));
                if( caption != "" )
                {
                    if (caption.Length > 30)
                    {
                        caption = caption.Substring(0, 30) + "...";
                    }
                    imagesPlaceHolder.Controls.Add(new LiteralControl("<br/>" + caption));
                }
                imagesPlaceHolder.Controls.Add(new LiteralControl("</div>"));

                if ((columnNumber % 6) == 0)
                {
                    imagesPlaceHolder.Controls.Add(new LiteralControl("<div style=\"clear: both;\" />"));
                }
                columnNumber++;
            }
            rdr.Close();

            if( userID > 0 )
            {
                columnNumber = 1;

                SqlCommand cmdEventsWithPics = new SqlCommand("", conn);
                cmdEventsWithPics.CommandType = CommandType.StoredProcedure;
                cmdEventsWithPics.CommandText = "spSelectEventsWithPicturesList";
                cmdEventsWithPics.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                DbDataReader rdrEventsWithPics = cmdEventsWithPics.ExecuteReader();
                while (rdrEventsWithPics.Read())
                {
                    int loopEventID = int.Parse(rdrEventsWithPics["EventID"].ToString());

                    SedogoEvent loopEvent = new SedogoEvent("", loopEventID);

                    string timelineColour = "#cd3301";
                    switch (loopEvent.categoryID)
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

                    albumnsPlaceHolder.Controls.Add(new LiteralControl("<div style=\"width:150px; float:left; margin:0 10px 10px 0\" "));
                    albumnsPlaceHolder.Controls.Add(new LiteralControl("onMouseOver=\"setColor('colourBar_" + loopEventID.ToString() + "','" + timelineColour + "'); "
                        + "setColor('colourBar2_" + loopEventID.ToString() + "','#EEEEEE');\" "));
                    albumnsPlaceHolder.Controls.Add(new LiteralControl("onMouseOut=\"setColor('colourBar_" + loopEventID.ToString() + "','#FFFFFF');"
                        + "setColor('colourBar2_" + loopEventID.ToString() + "','#FFFFFF');\" "));
                    albumnsPlaceHolder.Controls.Add(new LiteralControl("> "));
                    string displayName = loopEvent.eventName;
                    if (displayName != "")
                    {
                        if (displayName.Length > 30)
                        {
                            displayName = displayName.Substring(0, 30) + "...";
                        }
                    }
                    albumnsPlaceHolder.Controls.Add(new LiteralControl("<div id=\"colourBar2_" + loopEventID.ToString() + "\" width=\"150px\">"));
                    albumnsPlaceHolder.Controls.Add(new LiteralControl("<a href=\"morePictures.aspx?EID=" + loopEventID.ToString() + "\">" + displayName + "</a>"));
                    albumnsPlaceHolder.Controls.Add(new LiteralControl("</div>"));
                    albumnsPlaceHolder.Controls.Add(new LiteralControl("<a href=\"morePictures.aspx?EID=" + loopEventID.ToString()
                        + "\"><img width=\"100\" src=\"/assets/eventPics/" + loopEvent.eventPicPreview + "\"/></a>"));
                    albumnsPlaceHolder.Controls.Add(new LiteralControl("<span id=\"colourBar_" + loopEventID.ToString() + "\"><img src=\"/images/1x1trans.gif\" height=\"6px\" width=\"100px\" ></span> "));
                    if (loopEvent.eventAchieved == true)
                    {
                        albumnsPlaceHolder.Controls.Add(new LiteralControl("<a href=\"morePictures.aspx?EID=" + loopEventID.ToString()
                            + "\">Achieved <img src=\"images/acceptachieve.gif\" /></a>"));
                    }
                    albumnsPlaceHolder.Controls.Add(new LiteralControl("</div>"));

                    if ((columnNumber % 4) == 0)
                    {
                        albumnsPlaceHolder.Controls.Add(new LiteralControl("<div style=\"clear: both;\" />"));
                    }
                    columnNumber++;
                }
                rdrEventsWithPics.Close();
            }
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
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
    }

    //===============================================================
    // Function: editPicsButton_click
    //===============================================================
    protected void editPicsButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        Response.Redirect("editEventPics.aspx?EID=" + eventID.ToString());
    }
}

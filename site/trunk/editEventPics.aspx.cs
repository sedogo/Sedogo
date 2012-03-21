//===============================================================
// Filename: editEventPics.aspx.cs
// Date: 19/07/10
// --------------------------------------------------------------
// Description:
//   More pictures
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 19/07/10
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

public partial class editEventPics : System.Web.UI.Page
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
            addPictureLiteral.Text = "var url = 'addGoalPicture.aspx?EID=" + eventID.ToString() + "';";

            PopulateImages(eventID, userID);
        }
    }

    //===============================================================
    // Function: PopulateImages
    //===============================================================
    private void PopulateImages(int eventID, int userID)
    {
        DateTime loopDate = DateTime.MinValue;
        int itemCount = 0;

        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("spSelectEventPictureList", conn);
            cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 90;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            imagesRepeater.DataSource = ds;
            imagesRepeater.DataBind();

            itemCount = imagesRepeater.Items.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }

        if (itemCount == 0)
        {
            noPicsRow.Visible = true;
        }
        else
        {
            noPicsRow.Visible = false;
        }
    }

    //===============================================================
    // Function: imagesRepeater_ItemDataBound
    //===============================================================
    protected void imagesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null &&
            (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
        {
            DataRowView row = e.Item.DataItem as DataRowView;

            int eventPictureID = int.Parse(row["EventPictureID"].ToString());
            int postedByUserID = int.Parse(row["PostedByUserID"].ToString());

            int eventID = int.Parse(row["EventID"].ToString());

            string imageFilename = "";
            if (row["ImageFilename"] != "")
            {
                imageFilename = (string)row["ImageFilename"];
            }
            string imagePreview = "";
            if (row["ImagePreview"] != "")
            {
                imagePreview = (string)row["ImagePreview"];
            }
            string imageThumbnail = "";
            if (row["ImageThumbnail"] != "")
            {
                imageThumbnail = (string)row["ImageThumbnail"];
            }
            string caption = "";
            if (row["Caption"] != "")
            {
                caption = (string)row["Caption"];
            }
            DateTime createdDate = (DateTime)row["CreatedDate"];

            Image eventImage = e.Item.FindControl("eventImage") as Image;
            if (imageThumbnail == "")
            {
                eventImage.ImageUrl = "./images/eventThumbnailBlank.png";
            }
            else
            {
                var _event = new SedogoEvent(string.Empty, eventID);
                eventImage.ImageUrl = ResolveUrl(ImageHelper.GetRelativeImagePath(eventPictureID, _event.eventGUID, ImageType.EventPictureThumbnail));
            }

            TextBox imageCaptionTextBox = e.Item.FindControl("imageCaptionTextBox") as TextBox;
            imageCaptionTextBox.Text = caption;
        }
    }

    //===============================================================
    // Function: imagesRepeater_ItemCommand
    //===============================================================
    protected void imagesRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        int eventPictureID = int.Parse(e.CommandArgument.ToString());

        if (e.CommandName == "deleteButton")
        {
            SedogoEvent sedogoEvent = new SedogoEvent((string)Session["loggedInUserFullName"], eventID);
            sedogoEvent.Update();

            SedogoEventPicture eventPic = new SedogoEventPicture((string)Application["connectionString"], eventPictureID);
            ImageHelper.DeleteImage(eventPictureID, sedogoEvent.eventGUID, ImageType.EventPictureThumbnail);
            eventPic.Delete();

            Response.Redirect("editEventPics.aspx?EID=" + eventID.ToString());
        }
        if (e.CommandName == "saveButton")
        {
            SedogoEvent sedogoEvent = new SedogoEvent((string)Session["loggedInUserFullName"], eventID);
            sedogoEvent.Update();

            SedogoEventPicture eventPic = new SedogoEventPicture((string)Application["connectionString"], eventPictureID);

            TextBox imageCaptionTextBox = e.Item.FindControl("imageCaptionTextBox") as TextBox;
            eventPic.caption = imageCaptionTextBox.Text;
            eventPic.Update();

            Response.Redirect("editEventPics.aspx?EID=" + eventID.ToString());
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

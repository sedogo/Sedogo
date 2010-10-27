//===============================================================
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
            int eventPictureID = int.Parse(Request.QueryString["EPID"].ToString());

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


            SedogoEventPicture eventPic = new SedogoEventPicture((string)Application["connectionString"], eventPictureID);

            eventImage.ImageUrl = "~/assets/eventPics/" + eventPic.eventImagePreview;

            GlobalData gd = new GlobalData("");
            string imageFile = gd.GetStringValue("FileStoreFolder") + @"\eventPics\" + eventPic.eventImagePreview;
            //FileInfo imageFileInfo = new FileInfo(imageFile);
            System.Drawing.Image sourceImage = System.Drawing.Image.FromFile(imageFile);
            if (sourceImage.Height > 370)
            {
                eventImage.Height = 370;
            }
            captionLabel.Text = eventPic.caption;

            /*
            if (userID > 0)
            {
                SedogoEvent sedogoEvent = new SedogoEvent("", eventID);
                if (sedogoEvent.userID != userID)
                {
                    // Viewing someone elses event
                    captionTextBox.Visible = false;
                    saveButton.Visible = false;
                    deleteButton.Visible = false;
                }
                else
                {
                    // Viewing own event
                    captionLabel.Text = "Caption: ";
                    captionTextBox.Visible = true;
                    captionTextBox.Text = eventPic.caption;
                    saveButton.Visible = true;
                    deleteButton.Visible = true;

                    deleteButton.Attributes.Add("onclick", "if(confirm('Are you sure you want to delete this picture?')){document.forms[0].target = '_top';return true;}else{return false}");
                }
            }
            else
            {
                // Not logged in
                captionLabel.Text = eventPic.caption;
                captionTextBox.Visible = false;
                saveButton.Visible = false;
                deleteButton.Visible = false;
            }
            */
        }
    }

    //===============================================================
    // Function: previousButton_click
    //===============================================================
    protected void previousButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        int eventPictureID = int.Parse(Request.QueryString["EPID"].ToString());

        int previousEventPicID = -1;

        SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectEventPicturePrevious";
            cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
            cmd.Parameters.Add("@EventPictureID", SqlDbType.Int).Value = eventPictureID;
            DbDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows == true)
            {
                rdr.Read();
                if (!rdr.IsDBNull(0))
                {
                    previousEventPicID = int.Parse(rdr[0].ToString());
                }
            }
            rdr.Close();

            if (previousEventPicID < 0)
            {
                SqlCommand cmdFirst = new SqlCommand("", conn);
                cmdFirst.CommandType = CommandType.StoredProcedure;
                cmdFirst.CommandText = "spSelectEventPictureLast";
                cmdFirst.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                DbDataReader rdrFirst = cmdFirst.ExecuteReader();
                if (rdrFirst.HasRows == true)
                {
                    rdrFirst.Read();
                    if (!rdrFirst.IsDBNull(0))
                    {
                        previousEventPicID = int.Parse(rdrFirst[0].ToString());
                    }
                }
                rdrFirst.Close();
            }
        }
        catch (Exception ex)
        {
            ErrorLog errorLog = new ErrorLog();
            errorLog.WriteLog("eventPicDetails", "previousButton_click", ex.Message, logMessageLevel.errorMessage);
            throw ex;
        }
        finally
        {
            conn.Close();
        }

        if (previousEventPicID > 0)
        {
            Response.Redirect("eventPicDetails.aspx?EID=" + eventID.ToString() + "&EPID=" + previousEventPicID.ToString());
        }
    }

    //===============================================================
    // Function: nextButton_click
    //===============================================================
    protected void nextButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);
        int eventPictureID = int.Parse(Request.QueryString["EPID"].ToString());

        int nextPicID = -1;

        SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectEventPictureNext";
            cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
            cmd.Parameters.Add("@EventPictureID", SqlDbType.Int).Value = eventPictureID;
            DbDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows == true)
            {
                rdr.Read();
                if (!rdr.IsDBNull(0))
                {
                    nextPicID = int.Parse(rdr[0].ToString());
                }
            }
            rdr.Close();

            if (nextPicID < 0)
            {
                SqlCommand cmdFirst = new SqlCommand("", conn);
                cmdFirst.CommandType = CommandType.StoredProcedure;
                cmdFirst.CommandText = "spSelectEventPictureFirst";
                cmdFirst.Parameters.Add("@EventID", SqlDbType.Int).Value = eventID;
                DbDataReader rdrFirst = cmdFirst.ExecuteReader();
                if (rdrFirst.HasRows == true)
                {
                    rdrFirst.Read();
                    if (!rdrFirst.IsDBNull(0))
                    {
                        nextPicID = int.Parse(rdrFirst[0].ToString());
                    }
                }
                rdrFirst.Close();
            }
        }
        catch (Exception ex)
        {
            ErrorLog errorLog = new ErrorLog();
            errorLog.WriteLog("eventPicDetails", "nextButton_click", ex.Message, logMessageLevel.errorMessage);
            throw ex;
        }
        finally
        {
            conn.Close();
        }

        if (nextPicID > 0)
        {
            Response.Redirect("eventPicDetails.aspx?EID=" + eventID.ToString() + "&EPID=" + nextPicID.ToString());
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

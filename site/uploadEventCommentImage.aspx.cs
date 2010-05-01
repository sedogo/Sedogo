﻿//===============================================================
// Filename: uploadEventCommentImage.aspx
// Date: 30/4/10
// --------------------------------------------------------------
// Description:
//   Upload event pic
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 30/4/10
// Revision history:
//===============================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
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

public partial class uploadEventCommentImage : SedogoPage
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        SedogoEvent sedogoEvent = new SedogoEvent(Session["loggedInUserFullName"].ToString(), eventID);

        eventNameLabel.Text = sedogoEvent.eventName;

        SetFocus(eventPicFileUpload);
    }

    //===============================================================
    // Function: uploadEventPicButton_click
    //===============================================================
    protected void uploadEventPicButton_click(object sender, EventArgs e)
    {
        int eventID = int.Parse(Request.QueryString["EID"]);

        if (eventPicFileUpload.PostedFile.ContentLength != 0)
        {
            int fileSizeBytes = eventPicFileUpload.PostedFile.ContentLength;

            GlobalData gd = new GlobalData((string)Session["loggedInContactName"]);
            string fileStoreFolder = gd.GetStringValue("FileStoreFolder") + @"\temp";

            string originalFileName = Path.GetFileName(eventPicFileUpload.PostedFile.FileName);
            string destPath = Path.Combine(fileStoreFolder, originalFileName);
            destPath = destPath.Replace(" ", "_");
            destPath = MiscUtils.GetUniqueFileName(destPath);
            string savedFilename = Path.GetFileName(destPath);

            eventPicFileUpload.PostedFile.SaveAs(destPath);

            MiscUtils.CreateEventCommentImagePreviews(Path.GetFileName(destPath), eventID, "",
                int.Parse(Session["loggedInUserID"].ToString()), Session["loggedInUserFullName"].ToString());

            Response.Redirect("viewEvent.aspx?EID=" + eventID.ToString());
        }
    }
}

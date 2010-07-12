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
            int eventPictureID = int.Parse(Request.QueryString["EPID"].ToString());

            SedogoEventPicture eventPic = new SedogoEventPicture((string)Application["connectionString"], eventPictureID);

            eventImage.ImageUrl = "~/assets/eventPics/" + eventPic.eventImagePreview;
        }
    }
}

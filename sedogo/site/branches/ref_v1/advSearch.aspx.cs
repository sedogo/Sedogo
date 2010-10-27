//===============================================================
// Filename: advSearch.aspx.cs
// Date: 19/11/09
// --------------------------------------------------------------
// Description:
//   Advanced search
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 19/11/09
// Revision history:
//===============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class advSearch : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        SetFocus(eventNameTextBox);
    }

    //===============================================================
    // Function: searchButton_click
    //===============================================================
    protected void searchButton_click(object sender, EventArgs e)
    {
        string eventName = eventNameTextBox.Text;
        string venue = venueTextBox.Text;
        string eventOwnerName = eventOwnerNameTextBox.Text;

        string url = "search2.aspx";
        url = url + "?EvName=" + eventName;
        url = url + "&EvVenue=" + venue;
        url = url + "&EvOwner=" + eventOwnerName;

        Response.Redirect(url);
    }
}

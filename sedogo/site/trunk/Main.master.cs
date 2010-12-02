﻿using System;
using Sedogo.BusinessObjects;

public partial class Main : System.Web.UI.MasterPage
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        sidebarControl.userID = -1;
    }
}

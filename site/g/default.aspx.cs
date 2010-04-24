﻿//===============================================================
// Filename: /g/default.aspx.cs
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

public partial class g_default : System.Web.UI.Page
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
                PopulateGoalNames(letterFilter);
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
        }
    }

    //===============================================================
    // Function: PopulateGoalNames
    //===============================================================
    private void PopulateGoalNames(string letterFilter)
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectEventListByFirstLetter";
            cmd.Parameters.Add("@LetterFilter", SqlDbType.Char, 1).Value = letterFilter;
            DbDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows == true)
            {
                while (rdr.Read())
                {
                    string eventName = "";

                    int eventID = int.Parse(rdr["EventID"].ToString());
                    if (!rdr.IsDBNull(rdr.GetOrdinal("EventName")))
                    {
                        eventName = (string)rdr["EventName"];
                    }
                    //SELECT , , DateType, StartDate, RangeStartDate, RangeEndDate,
                    //BeforeBirthday, CategoryID, TimezoneID, EventAchieved, PrivateEvent, CreatedFromEventID,
                    //EventDescription, EventVenue, MustDo,
                    //EventPicFilename, EventPicThumbnail, EventPicPreview,
                    //CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName

                    Literal userLink = new Literal();

                    userLink.Text = "<div class=\"directory\"><a href=\"/viewEvent.aspx?EID=" + eventID.ToString() + "\">" + eventName + "</a></div>";

                    goalPlaceHolder.Controls.Add(userLink);
                }
                noGoalsWithThisLetterDiv.Visible = false;
            }
            else
            {
                noGoalsWithThisLetterDiv.Visible = true;
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

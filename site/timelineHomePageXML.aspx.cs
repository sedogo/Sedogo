﻿//===============================================================
// Filename: timelineHomePageXML.aspx.cs
// Date: 15/10/09
// --------------------------------------------------------------
// Description:
//   
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 15/10/09
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
using System.Text;
using System.Xml;
using Sedogo.BusinessObjects;

public partial class timelineHomePageXML : System.Web.UI.Page
{
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        string searchText = "";
        if (Request.QueryString["Search"] != null)
        {
            searchText = (string)Request.QueryString["Search"];
        }

        XmlTextWriter writer = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
        writer.WriteStartDocument();
        writer.WriteStartElement("data");

        CreateXMLContent(writer, searchText);

        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Close();
    }

    //===============================================================
    // Function: CreateXMLContent
    //===============================================================
    private void CreateXMLContent(XmlTextWriter writer, string searchText)
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelectHomePageEvents";
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int categoryID = 1;
                string dateType = "D";
                DateTime startDate = DateTime.MinValue;
                DateTime rangeStartDate = DateTime.MinValue;
                DateTime rangeEndDate = DateTime.MinValue;
                int beforeBirthday = -1;
                Boolean privateEvent = false;
                Boolean eventAchieved = false;
                int userID = -1;

                DateTime timelineStartDate = DateTime.MinValue;
                DateTime timelineEndDate = DateTime.MinValue;

                //*New
                string eventPicThumbnail = "";
                //

                int eventID = int.Parse(rdr["EventID"].ToString());
                string eventName = (string)rdr["EventName"];
                if (!rdr.IsDBNull(rdr.GetOrdinal("DateType")))
                {
                    dateType = (string)rdr["DateType"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("StartDate")))
                {
                    startDate = (DateTime)rdr["StartDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("RangeStartDate")))
                {
                    rangeStartDate = (DateTime)rdr["RangeStartDate"];
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("RangeEndDate")))
                {
                    rangeEndDate = (DateTime)rdr["RangeEndDate"];
                }
                eventAchieved = (Boolean)rdr["EventAchieved"];
                if (!rdr.IsDBNull(rdr.GetOrdinal("CategoryID")))
                {
                    categoryID = int.Parse(rdr["CategoryID"].ToString());
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("BeforeBirthday")))
                {
                    beforeBirthday = int.Parse(rdr["BeforeBirthday"].ToString());
                }
                privateEvent = (Boolean)rdr["PrivateEvent"];
                if (!rdr.IsDBNull(rdr.GetOrdinal("UserID")))
                {
                    userID = int.Parse(rdr["UserID"].ToString());
                }

                //*New
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventPicThumbnail")))
                {
                    eventPicThumbnail = (string)rdr["EventPicThumbnail"];
                }
                //

                if (dateType == "D")
                {
                    // Event occurs on a specific date

                    timelineStartDate = startDate;
                    timelineEndDate = startDate.AddDays(28);        // Add 28 days so it shows up
                }
                if (dateType == "R")
                {
                    // Event occurs in a date range - use the start date

                    timelineStartDate = rangeStartDate;
                    timelineEndDate = rangeEndDate;

                    TimeSpan ts = timelineEndDate - timelineStartDate;
                    if (ts.Days < 28)
                    {
                        timelineEndDate = timelineStartDate.AddDays(28);        // Add 28 days so it shows up
                    }

                    startDate = rangeStartDate;
                }
                if (dateType == "A")
                {
                    // Event occurs before birthday
                    timelineStartDate = DateTime.Now;
                    if (userID > 0)
                    {
                        SedogoUser user = new SedogoUser("", userID);

                        if (user.birthday > DateTime.MinValue)
                        {
                            timelineEndDate = user.birthday.AddYears(beforeBirthday);

                            TimeSpan ts = timelineEndDate - DateTime.Now;   // timelineStartDate.AddYears(beforeBirthday);
                            if (ts.Days < 0)
                            {
                                // Birthday was in the past
                                timelineStartDate = DateTime.Now;
                                timelineEndDate = timelineStartDate.AddDays(28);        // Add 28 days so it shows up

                                // Set start date so event is correctly placed below
                                startDate = DateTime.Now.AddDays(ts.Days);
                            }
                            else if (ts.Days >= 0 && ts.Days < 28)
                            {
                                // Birthday is within 28 days - extend the timeline a bit
                                timelineEndDate = timelineStartDate.AddDays(28);        // Add 28 days so it shows up

                                startDate = timelineStartDate;
                            }
                            else
                            {
                                startDate = timelineStartDate;
                            }
                        }
                        else
                        {
                            timelineEndDate = DateTime.Now.AddDays(28);
                        }
                    }
                    else
                    {
                        // If the userID is not set for any reason
                        timelineEndDate = timelineStartDate.AddMonths(1);
                    }
                }

                string timelineColour = "#cd3301";
                string category = "";
                switch (categoryID)
                {
                    case 1:
                        timelineColour = "#cd3301";
                        category = "Personal";
                        break;
                    case 2:
                        timelineColour = "#ff0b0b";
                        category = "Travel";
                        break;
                    case 3:
                        timelineColour = "#ff6801";
                        category = "Friends";
                        break;
                    case 4:
                        timelineColour = "#ff8500";
                        category = "Family";
                        break;
                    case 5:
                        timelineColour = "#d5b21a";
                        category = "General";
                        break;
                    case 6:
                        timelineColour = "#8dc406";
                        category = "Health";
                        break;
                    case 7:
                        timelineColour = "#5b980c";
                        category = "Money";
                        break;
                    case 8:
                        timelineColour = "#079abc";
                        category = "Education";
                        break;
                    case 9:
                        timelineColour = "#5ab6cd";
                        category = "Hobbies";
                        break;
                    case 10:
                        timelineColour = "#8A67C1";
                        category = "Work";
                        break;
                    case 11:
                        timelineColour = "#E54ECF";
                        category = "Culture";
                        break;
                    case 12:
                        timelineColour = "#A5369C";
                        category = "Charity";
                        break;
                    case 13:
                        timelineColour = "#A32672";
                        category = "Green";
                        break;
                    case 14:
                        timelineColour = "#669";
                        category = "Misc";
                        break;
                }
                int messageCount = SedogoEvent.GetCommentCount(eventID);
                int trackingUserCount = SedogoEvent.GetTrackingUserCount(eventID);
                int memberUserCount = SedogoEvent.GetMemberUserCount(eventID);

                //string linkURL = "&lt;a href=\"viewEvent.aspx?EID=" + eventID.ToString() + "\" class=\"modal\" title=\"\"&gt;Full details&lt;/a&gt;";


                //string linkURL = trackingUserCount.ToString() + " following this goal<br/>";
                //linkURL = linkURL + memberUserCount.ToString() + " members<br/>";
                //linkURL = linkURL + messageCount.ToString() + " comments<br/>";
                //linkURL = linkURL + "&lt;a href=\"javascript:openEvent(" + eventID.ToString() + ")\" title=\"\"&gt;Full details&lt;/a&gt;";

                //* New
                string linkURL = timelineStartDate.ToString("MMM dd yyyy HH:mm:ss 'GMT'") + "<br/><br/>";
                linkURL = linkURL + trackingUserCount.ToString() + " following this goal<br/>";
                linkURL = linkURL + memberUserCount.ToString() + " members<br/>";
                linkURL = linkURL + messageCount.ToString() + " comments<br/>";
                linkURL = linkURL + "&lt;a href=\"javascript:openEvent(" + eventID.ToString() + ")\" title=\"\"&gt;Goal details&lt;/a&gt;";
                //*

                writer.WriteStartElement("event");      // Time format: Feb 27 2009 09:00:00 GMT
                writer.WriteAttributeString("start", timelineStartDate.ToString("MMM dd yyyy HH:mm:ss 'GMT'"));
                writer.WriteAttributeString("end", timelineEndDate.ToString("MMM dd yyyy HH:mm:ss 'GMT'"));
                writer.WriteAttributeString("isDuration", "true");
                writer.WriteAttributeString("title", eventName);
                
                //* New
                if (eventPicThumbnail == "")
                {
                    writer.WriteAttributeString("image", "./images/eventThumbnailBlank.png");                    
                }
                else
                {
                    writer.WriteAttributeString("image", "./assets/eventPics/" + eventPicThumbnail);
                }

                //writer.WriteAttributeString("image", "http://simile.mit.edu/images/csail-logo.gif");
                //*
                writer.WriteAttributeString("color", timelineColour);
                writer.WriteAttributeString("category", category);
                writer.WriteString(linkURL + " &lt;br /&gt;");
                writer.WriteEndElement();
            }
            rdr.Close();
        }
        catch (Exception ex)
        {
            ErrorLog errorLog = new ErrorLog();
            errorLog.WriteLog("timelineHomePageXML", "Page_Load", ex.Message, logMessageLevel.errorMessage);
            //throw ex;
        }
        finally
        {
            conn.Close();
        }
    }
}

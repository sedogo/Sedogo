//===============================================================
// Filename: timelineSearch2XML.aspx.cs
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

public partial class timelineSearch2XML : System.Web.UI.Page
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
        string eventNameText = "";
        if (Request.QueryString["EvName"] != null)
        {
            eventNameText = (string)Request.QueryString["EvName"];
        }
        string eventVenue = "";
        if (Request.QueryString["EvVenue"] != null)
        {
            eventVenue = (string)Request.QueryString["EvVenue"];
        }
        string eventOwnerName = "";
        if (Request.QueryString["EvOwner"] != null)
        {
            eventOwnerName = (string)Request.QueryString["EvOwner"];
        }
        int eventCategoryID = -1;
        if (Request.QueryString["EvCategoryID"] != null)
        {
            eventCategoryID = int.Parse(Request.QueryString["EvCategoryID"].ToString());
        }
        string dateSearch = "R";
        if (Request.QueryString["EvDateSearch"] != null)
        {
            dateSearch = (string)Request.QueryString["EvDateSearch"];
        }
        int beforeBirthday = -1;
        if (Request.QueryString["EvDateBDay"] != null)
        {
            beforeBirthday = int.Parse(Request.QueryString["EvDateBDay"].ToString());
        }
        DateTime dateSearchStartDate = DateTime.MinValue;
        if (Request.QueryString["EvDateStart"] != null)
        {
            try
            {
                string[] s = Request.QueryString["EvDateStart"].ToString().Split('-');
                dateSearchStartDate = new DateTime(int.Parse(s[2]), int.Parse(s[1]), int.Parse(s[0]));
            }
            catch { }
        }
        DateTime dateSearchEndDate = DateTime.MinValue;
        if (Request.QueryString["EvDateEnd"] != null)
        {
            try
            {
                string[] s = Request.QueryString["EvDateEnd"].ToString().Split('-');
                dateSearchEndDate = new DateTime(int.Parse(s[2]), int.Parse(s[1]), int.Parse(s[0]));
            }
            catch { }
        }
        int recentlyAdded = -1;
        if (Request.QueryString["EvRecentlyAdded"] != null)
        {
            recentlyAdded = int.Parse(Request.QueryString["EvRecentlyAdded"].ToString());
        }
        int recentlyUpdated = -1;
        if (Request.QueryString["EvRecentlyUpdated"] != null)
        {
            recentlyUpdated = int.Parse(Request.QueryString["EvRecentlyUpdated"].ToString());
        }
        string definitlyDo = "A";
        if (Request.QueryString["EvDefinitlyDo"] != null)
        {
            definitlyDo = (string)Request.QueryString["EvDefinitlyDo"];
        }

        XmlTextWriter writer = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
        writer.WriteStartDocument();
        writer.WriteStartElement("data");

        CreateXMLContent(writer, searchText, eventNameText, eventVenue, eventOwnerName,
            eventCategoryID, dateSearch, beforeBirthday, dateSearchStartDate, dateSearchEndDate,
            recentlyAdded, recentlyUpdated, definitlyDo);

        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Close();
    }

    //===============================================================
    // Function: CreateXMLContent
    //===============================================================
    private void CreateXMLContent(XmlTextWriter writer, string searchText,
        string eventNameText, string eventVenue, string eventOwnerName,
        int eventCategoryID, string dateSearch, int beforeBirthday,
        DateTime dateSearchStartDate, DateTime dateSearchEndDate,
        int recentlyAdded, int recentlyUpdated, string definitlyDo)
    {
        int userID = int.Parse(Session["loggedInUserID"].ToString());

        SedogoUser user = new SedogoUser(Session["loggedInUserFullName"].ToString(), userID);

        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (searchText != "")
            {
                cmd.CommandText = "spSearchEvents";
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                cmd.Parameters.Add("@SearchText", SqlDbType.NVarChar, 1000).Value = searchText;
            }
            else
            {
                cmd.CommandText = "spSearchEventsAdvanced";
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 1000).Value = eventNameText;
                cmd.Parameters.Add("@EventVenue", SqlDbType.NVarChar, 1000).Value = eventVenue;
                cmd.Parameters.Add("@OwnerName", SqlDbType.NVarChar, 1000).Value = eventOwnerName;
                cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = eventCategoryID;
                if (dateSearch == "R")
                {
                    if (dateSearchStartDate == DateTime.MinValue)
                    {
                        cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = dateSearchStartDate;
                    }
                    if (dateSearchStartDate == DateTime.MinValue)
                    {
                        cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = dateSearchEndDate;
                    }
                }
                if (dateSearch == "B")
                {
                    // Do not search on birthday, search between now and
                    // the date on which the birthday falls
                    DateTime birthdayEndDate = user.birthday.AddYears(beforeBirthday);
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = birthdayEndDate;
                }
                cmd.Parameters.Add("@RecentlyAdded", SqlDbType.Int).Value = recentlyAdded;
                cmd.Parameters.Add("@RecentlyUpdated", SqlDbType.Int).Value = recentlyUpdated;
                cmd.Parameters.Add("@DefinitlyDo", SqlDbType.NChar, 1).Value = definitlyDo;
            }
            DbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int categoryID = 1;
                string dateType = "D";
                DateTime startDate = DateTime.MinValue;
                DateTime rangeStartDate = DateTime.MinValue;
                DateTime rangeEndDate = DateTime.MinValue;
                int beforeBirthdayLoop = -1;
                Boolean privateEvent = false;
                Boolean eventAchieved = false;
                int eventUserID = -1;

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
                    beforeBirthdayLoop = int.Parse(rdr["BeforeBirthday"].ToString());
                }
                privateEvent = (Boolean)rdr["PrivateEvent"];
                if (!rdr.IsDBNull(rdr.GetOrdinal("UserID")))
                {
                    eventUserID = int.Parse(rdr["UserID"].ToString());
                }

                SedogoUser eventUser = new SedogoUser("", eventUserID);

                //*New
                if (!rdr.IsDBNull(rdr.GetOrdinal("EventPicThumbnail")))
                {
                    eventPicThumbnail = (string)rdr["EventPicThumbnail"];
                }

                string EUserName = string.Empty;
                if (!rdr.IsDBNull(rdr.GetOrdinal("FirstName")))
                {
                    EUserName = (string)rdr["FirstName"];
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LastName")))
                    {
                        EUserName = EUserName + " " + (string)rdr["LastName"];
                    }
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
                    if (eventUser.birthday > DateTime.MinValue)
                    {
                        timelineEndDate = eventUser.birthday.AddYears(beforeBirthdayLoop);

                        TimeSpan ts = timelineEndDate - DateTime.Now;   // timelineStartDate.AddYears(beforeBirthdayLoop);
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
                        timelineColour = "#8a67c1";
                        category = "Culture";
                        break;
                    case 12:
                        timelineColour = "#e54ecf";
                        category = "Charity";
                        break;
                    case 13:
                        timelineColour = "#a5369c";
                        category = "Green";
                        break;
                    case 14:
                        timelineColour = "#a32672";
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
                //linkURL = linkURL + "&lt;a href=\"javascript:openEvent(" + eventID.ToString() + ")\" title=\"\"&gt;Goal details&lt;/a&gt;";
                //linkURL += " - &lt;a href=\"javascript:viewProfile(" + eventUserID.ToString() + ")\" title=\"\"&gt;Profile&lt;/a&gt;";
                //linkURL += " - &lt;a href=\"javascript:viewUserTimeline(" + eventUserID.ToString() + ")\" title=\"\"&gt;Timeline&lt;/a&gt;";

                //* New
                string linkURL = timelineStartDate.ToString("ddd dd MMM yyyy") + "<br/><br/>";
                linkURL = linkURL + trackingUserCount.ToString() + " Followers<br/>";
                linkURL = linkURL + memberUserCount.ToString() + " Members<br/>";
                linkURL = linkURL + messageCount.ToString() + " Comments<br/>";
                linkURL = linkURL + "&lt;a style=\"text-decoration:underline;\" href=\"javascript:openEvent(" + eventID.ToString() + ")\" title=\"\"&gt;Goal details&lt;/a&gt;";
                linkURL += "  &lt;a style=\"text-decoration:underline;\" href=\"javascript:viewUserTimeline(" + eventUserID.ToString() + ")\" title=\"\"&gt;Timeline&lt;/a&gt;";
                linkURL += "  &lt;a style=\"text-decoration:underline;\" href=\"javascript:viewProfile(" + eventUserID.ToString() + ")\" title=\"\"&gt;Profile&lt;/a&gt;";

                string ImgLink = "";
                if (userID == eventUserID)
                {
                    ImgLink = "|" + EUserName;
                }
                else
                {
                    ImgLink = "|" + EUserName + " &lt;a href=\"javascript:doSendMessage(" + eventUserID.ToString() + ")\"&gt;&lt;img src=\"images/ico_messages.gif\" title=\"Send Message\" alt=\"Send Message\" /&gt;&lt;/a&gt;";
                }

                writer.WriteStartElement("event");      // Time format: Feb 27 2009 09:00:00 GMT
                writer.WriteAttributeString("start", timelineStartDate.ToString("MMM dd yyyy HH:mm:ss 'GMT'"));
                writer.WriteAttributeString("end", timelineEndDate.ToString("MMM dd yyyy HH:mm:ss 'GMT'"));
                writer.WriteAttributeString("isDuration", "true");
                writer.WriteAttributeString("title", eventName);

                var _event = new SedogoEvent(string.Empty, eventID);

                //* New
                if (eventPicThumbnail == "")
                {
                    writer.WriteAttributeString("image", "./images/eventThumbnailBlank.png");
                }
                else
                {
                    writer.WriteAttributeString("image", ResolveUrl(ImageHelper.GetRelativeImagePath(_event.eventID, _event.eventGUID, ImageType.EventThumbnail)));
                }
                //*

                //writer.WriteAttributeString("image", "http://simile.mit.edu/images/csail-logo.gif");
                writer.WriteAttributeString("color", timelineColour);
                writer.WriteAttributeString("category", category);
                writer.WriteString(linkURL + " &lt;br /&gt;" + ImgLink);
                writer.WriteEndElement();
            }
            rdr.Close();
        }
        catch (Exception ex)
        {
            ErrorLog errorLog = new ErrorLog();
            errorLog.WriteLog("timelineSearch2XML", "Page_Load", ex.Message, logMessageLevel.errorMessage);
            //throw ex;
        }
        finally
        {
            conn.Close();
        }
    }
}

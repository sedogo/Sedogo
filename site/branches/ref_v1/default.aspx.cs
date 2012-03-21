//===============================================================
// Filename: default.aspx.cs
// Date: 12/08/09
// --------------------------------------------------------------
// Description:
//   Default
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 12/08/09
// Revision history:
//===============================================================

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sedogo.BusinessObjects;
using System.Collections.Generic;
using System.Linq;

public partial class _default : System.Web.UI.Page
{
    //Changes By Chetan
    protected Int64 TGoals = 0;
    static readonly Random _random = new Random();
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            PopulateEvents();

            timelineURL.Text = "timelineHomePageXML.aspx?G=" + Guid.NewGuid();

            var timelineStartDate = DateTime.Now.AddYears(4);

            timelineStartDate1.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");     // "Jan 08 2010 00:00:00 GMT"
            timelineStartDate2.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");

            eventRotator.DataSource = GetRotatorDataSource();
            eventRotator.DataBind();

            BindLatestMembers();
            PopulateLatestSearches();

            DbConnection conn = new SqlConnection(GlobalSettings.connectionString);
            try
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT HomePageContent FROM HomePageContent ";
                var rdr = cmd.ExecuteReader();
                rdr.Read();
                homePageContent.Text = (string)rdr["HomePageContent"];
                rdr.Close();
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog();
                errorLog.WriteLog("", "", ex.Message, logMessageLevel.errorMessage);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    //===============================================================
    // Function: PopulateLatestSearches
    //===============================================================
    private void PopulateLatestSearches()
    {
        SqlConnection conn = new SqlConnection((string)Application["connectionString"]);
        try
        {
            conn.Open();

            SqlCommand cmdLatestEvents = new SqlCommand("", conn);
            cmdLatestEvents.CommandType = CommandType.StoredProcedure;
            cmdLatestEvents.CommandText = "spSelectLatestEventsDefaultPage";
            DbDataReader rdrLatestEvents = cmdLatestEvents.ExecuteReader();
            while (rdrLatestEvents.Read())
            {
                int eventID = int.Parse(rdrLatestEvents["EventID"].ToString());
                string eventName = (string)rdrLatestEvents["EventName"];

                HyperLink eventHyperlink = new HyperLink();
                eventHyperlink.Text = eventName;
                eventHyperlink.CssClass = "blue";
                eventHyperlink.NavigateUrl = "~/viewEvent.aspx?EID=" + eventID.ToString();
                eventHyperlink.Attributes.Add("onmouseover", "changeClass(this.id, 'sideBarBGHighlight')");
                eventHyperlink.Attributes.Add("onmouseout", "changeClass(this.id, 'sideBarBGNormal')");

                goalsAddedPlaceHolder.Controls.Add(eventHyperlink);

                goalsAddedPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
                goalsAddedPlaceHolder.Controls.Add(new LiteralControl("<div style=\"height:5px\"></div>"));
            }
            rdrLatestEvents.Close();
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

    private void BindLatestMembers()
    {
        var objSnFun = new SedogoNewFun();
        var dtAllUsrs = objSnFun.GetAllEnableUserDetails();


        if (dtAllUsrs.Rows.Count > 0)
        {
            DataTable dt = dtAllUsrs.Copy();
            dlMember.DataSource = dtAllUsrs;
            dlMember.DataBind();

            TGoals = Convert.ToInt64(dtAllUsrs.Rows[0]["mcount"]);
        }
        else
        {
            TGoals = 0;
            dlMember.DataSource = dtAllUsrs;
            dlMember.DataBind();
        }
    }

    protected void dlMember_ItemDataBound(Object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            // Retrieve the Label control in the current DataListItem.
            Image profilePicImage = (Image)e.Item.FindControl("profilePicImage");

            string profilePicThumbnail = ((DataRowView)e.Item.DataItem).Row.ItemArray[13].ToString();
            //string profilePicThumbnail = ((DataRowView)e.Item.DataItem).Row.ItemArray[12].ToString();
            int userID = int.Parse(((DataRowView)e.Item.DataItem).Row.ItemArray[0].ToString());

            if (profilePicThumbnail != "")
            {
                profilePicImage.ImageUrl = "assets/profilePics/" + profilePicThumbnail;
            }
            else
            {
                SedogoUser user = new SedogoUser("", userID);
                if (user.gender == "M")
                {
                    // 1,2,5
                    int avatarID = 5;
                    switch ((userID % 6))
                    {
                        case 0: case 1: avatarID = 1; break;
                        case 2: case 3: avatarID = 2; break;
                    }
                    profilePicImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                }
                else
                {
                    // 3,4,6
                    int avatarID = 6;
                    switch ((userID % 6))
                    {
                        case 0: case 1: avatarID = 3; break;
                        case 2: case 3: avatarID = 4; break;
                    }
                    profilePicImage.ImageUrl = "~/images/avatars/avatar" + avatarID.ToString() + "sm.gif";
                }
            }
            profilePicImage.Height = 33;
            profilePicImage.Width = 33;
            profilePicImage.Style.Add("cursor", "pointer");
            profilePicImage.Style.Add("padding-bottom", "6px");
            profilePicImage.Style.Add("padding-right", "6px");
            profilePicImage.Attributes.Add("onmouseover", "ShowHideDiv(" + userID.ToString() + ")");
        }
    }

    //===============================================================
    // Function: GetRotatorDataSource
    //===============================================================
    private static string[] GetRotatorDataSource()
    {
        string[] images = {"go_brag", "go_fast", "go_high", "go_party", 
                               "go_sailing", "go_speechless", "go_swimming", "go_traveling", "go_watch" };
        //return images;
        string[] shuffle = RandomizeStrings(images);
        return shuffle;
    }

    //===============================================================
    // Function: PopulateEvents
    //===============================================================
    protected void PopulateEvents()
    {
    }

    private static string[] RandomizeStrings(string[] arr)
    {
        var list = arr.Select(s => new KeyValuePair<int, string>(_random.Next(), s)).ToList();

        var sorted = from item in list
                     orderby item.Key
                     select item;

        var result = new string[arr.Length];

        var index = 0;
        foreach (var pair in sorted)
        {
            result[index] = pair.Value;
            index++;
        }

        return result;
    }

}

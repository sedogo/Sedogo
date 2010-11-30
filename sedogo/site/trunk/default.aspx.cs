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
using System.Text;
using System.Collections.Generic;
using System.Linq;

public partial class _default : System.Web.UI.Page
{
    //Changes By Chetan
    protected Int64 TGoals = 0;
    static Random _random = new Random();
    //===============================================================
    // Function: Page_Load
    //===============================================================
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["ResetPassword"] != null)
            {
                string resetPassword = (string)Session["ResetPassword"];
                if (resetPassword == "Y")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Your password has been reset and emailed to you.\");", true);
                    Session["ResetPassword"] = null;
                }
            }

            Session["EventID"] = "";
            Session["ReplyID"] = "";
            Session["MessageID"] = "";
            Session["EventInviteGUID"] = "";
            Session["EventInviteUserID"] = -1;
            Session["DefaultRedirect"] = "";
            if (Request.QueryString["EID"] != null)
            {
                Session["EventID"] = (string)Request.QueryString["EID"];
            }
            if (Request.QueryString["ReplyID"] != null)
            {
                Session["ReplyID"] = (string)Request.QueryString["ReplyID"];
            }
            if (Request.QueryString["MID"] != null)
            {
                Session["MessageID"] = (string)Request.QueryString["MID"];
            }
            if (Request.QueryString["EIG"] != null)
            {
                Session["EventInviteGUID"] = (string)Request.QueryString["EIG"];
            }
            if (Request.QueryString["UID"] != null)
            {
                Session["EventInviteUserID"] = int.Parse(Request.QueryString["UID"].ToString());
            }
            if (Request.QueryString["Redir"] != null)
            {
                Session["DefaultRedirect"] = Request.QueryString["Redir"].ToString();
            }

            HttpCookie emailCookie = Request.Cookies["SedogoLoginEmailAddress"];
            HttpCookie passwordCookie = Request.Cookies["SedogoLoginPassword"];
            // Check to make sure the cookie exists
            if (emailCookie != null && passwordCookie != null)
            {
                string loginEmailAddress = emailCookie.Value.ToString();
                string loginPassword = passwordCookie.Value.ToString();

                SedogoUser user = new SedogoUser("");
                loginResults checkResult;
                checkResult = user.VerifyLogin(loginEmailAddress, loginPassword, false, true, "default.aspx cookie");

                if ((checkResult == loginResults.loginSuccess) || (checkResult == loginResults.passwordExpired))
                {
                    Session.Add("loggedInUserID", user.userID);
                    Session.Add("loggedInUserFirstName", user.firstName);
                    Session.Add("loggedInUserLastName", user.lastName);
                    Session.Add("loggedInUserEmailAddress", user.emailAddress);
                    Session.Add("loggedInUserFullName", user.firstName + " " + user.lastName);

                    if ((checkResult == loginResults.loginSuccess) || (checkResult == loginResults.passwordExpired))
                    {
                        string url = "./profile.aspx";
                        Response.Redirect(url);
                    }
                }
            }

            PopulateEvents();

            timelineURL.Text = "timelineHomePageXML.aspx?G=" + Guid.NewGuid().ToString();

            if ((string)Session["EventInviteGUID"] != "")
            {
                if ((int)Session["EventInviteUserID"] > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"login.aspx\");", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"register.aspx\");", true);
                }
            }
            if ((string)Session["EventID"] != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"login.aspx\");", true);
            }
            if ((string)Session["ReplyID"] != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"login.aspx\");", true);
            }
            if ((string)Session["DefaultRedirect"] != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"login.aspx\");", true);
            }

            //DateTime timelineStartDate = DateTime.Now.AddMonths(8);
            DateTime timelineStartDate = DateTime.Now.AddYears(4);

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

                DbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT HomePageContent FROM HomePageContent ";
                DbDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                homePageContent.Text = (string)rdr["HomePageContent"];
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.WriteLog("", "", ex.Message, logMessageLevel.errorMessage);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            if (Request.QueryString["Tour"] != null)
            {
                if (Request.QueryString["Tour"].ToString() == "Y")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert",
                        "$(document).ready(function() { showTour1(); });", true);
                }
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
        //int cnt = 0;
        SedogoNewFun objSNFun = new SedogoNewFun();
        DataTable dtAllUsrs = new DataTable();
        dtAllUsrs = objSNFun.GetAllEnableUserDetails();


        if (dtAllUsrs.Rows.Count > 0)
        {
            DataTable dt = dtAllUsrs.Copy();
            dlMember.DataSource = dtAllUsrs;
            dlMember.DataBind();

            //for (cnt = 0; cnt < dtAllUsrs.Rows.Count; cnt++)
            //{
            //    if (Convert.ToString(dtAllUsrs.Rows[cnt]["gcount"]) != null && Convert.ToString(dtAllUsrs.Rows[cnt]["gcount"]) != "")
            //    {
            //        TGoals = TGoals + Convert.ToInt64(dtAllUsrs.Rows[cnt]["gcount"]);
            //    }
            //}

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
            string userGuid = (string)((DataRowView)e.Item.DataItem).Row.ItemArray[1];

            if (profilePicThumbnail != "")
            {
                profilePicImage.ImageUrl = ImageHelper.GetRelativeImagePath(userID, userGuid, ImageType.UserThumbnail);
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
    private string[] GetRotatorDataSource()
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

    private string[] RandomizeStrings(string[] arr)
    {
        List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();

        foreach (string s in arr)
        {
            list.Add(new KeyValuePair<int, string>(_random.Next(), s));
        }

        var sorted = from item in list
                     orderby item.Key
                     select item;

        string[] result = new string[arr.Length];

        int index = 0;
        foreach (KeyValuePair<int, string> pair in sorted)
        {
            result[index] = pair.Value;
            index++;
        }

        return result;
    }

}

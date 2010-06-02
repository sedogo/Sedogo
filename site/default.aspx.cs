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
            if ((string)Session["DefaultRedirect"] != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "openModal(\"login.aspx\");", true);
            }

            DateTime timelineStartDate = DateTime.Now.AddMonths(8);

            timelineStartDate1.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");     // "Jan 08 2010 00:00:00 GMT"
            timelineStartDate2.Text = timelineStartDate.ToString("MMM dd yyyy HH:MM:ss 'GMT'");

            //what.Attributes.Add("onkeypress", "checkAddButtonEnter(event);");

            //searchButton1.Attributes.Add("onmouseover", "this.src='images/addButtonRollover.png'");
            //searchButton1.Attributes.Add("onmouseout", "this.src='images/addButton.png'");
            //searchButton2.Attributes.Add("onmouseover", "this.src='images/searchButtonRollover.png'");
            //searchButton2.Attributes.Add("onmouseout", "this.src='images/searchButton.png'");

            BindLatestMembers();

            eventRotator.DataSource = GetRotatorDataSource();
            eventRotator.DataBind();
        }
    }

    //===============================================================
    // Function: searchButton_click
    //===============================================================
    //protected void searchButton_click(object sender, EventArgs e)
    //{
    //    string searchText = what2.Text;

    //    if (searchText.Trim() == "" || searchText.Trim() == "e.g. climb Everest")
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a search term\");", true);
    //    }
    //    else
    //    {
    //        if (searchText.Length >= 2)
    //        {
    //            Response.Redirect("search.aspx?Search=" + searchText.ToString());
    //        }
    //        else
    //        {
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"Please enter a longer search term\");", true);
    //        }
    //    }
    //}

    //===============================================================
    // Function: PopulateEvents
    //===============================================================
    protected void PopulateEvents()
    {
    }

    private void BindLatestMembers()
    {
        int cnt = 0;
        SedogoNewFun objSNFun = new SedogoNewFun();
        DataTable dtAllUsrs = new DataTable();
        dtAllUsrs = objSNFun.GetAllEnableUserDetails();


        if (dtAllUsrs.Rows.Count > 0)
        {
            DataTable dt = dtAllUsrs.Copy();
            dlMember.DataSource = dtAllUsrs;
            dlMember.DataBind();
            for (cnt = 0; cnt < dtAllUsrs.Rows.Count; cnt++)
            {
                if (Convert.ToString(dtAllUsrs.Rows[cnt]["gcount"]) != null && Convert.ToString(dtAllUsrs.Rows[cnt]["gcount"]) != "")
                {
                    TGoals = TGoals + Convert.ToInt64(dtAllUsrs.Rows[cnt]["gcount"]);
                }
            }
        }
        else
        {
            TGoals = 0;
            dlMember.DataSource = dtAllUsrs;
            dlMember.DataBind();
        }
    }

    protected string BindLatestAchievedGoals()
    {
        SedogoNewFun objSNFun = new SedogoNewFun();
        DataTable dtLGoal = objSNFun.GetLatestAchievedGoals();

        StringBuilder LGoal = new StringBuilder();
        int i;

        if (dtLGoal.Rows.Count > 0)
        {
            string GName = string.Empty;

            LGoal.Append("<ul>");

            if (dtLGoal.Rows.Count > 3)
            {
                for (i = 0; i < 3; i++)
                {
                    GName = Convert.ToString(dtLGoal.Rows[i]["EventName"]);
                    if (GName.Length > 27)
                    {
                        GName = GName.Substring(0, 27) + "...";
                    }
                    LGoal.Append("<li style='line-height:25px;font-size:14px;'>" + GName + "</li>");
                }
                LGoal.Append("<li style='line-height:25px;font-size:14px;font-weight:bold;'><a href='HomeMoreDetail.aspx?type=achieved' title='Click here to view more achieved Goals' style='font-weight:bold;font-size:13px;'>More ></a></li>");
            }
            else
            {
                for (i = 0; i < dtLGoal.Rows.Count; i++)
                {
                    GName = Convert.ToString(dtLGoal.Rows[i]["EventName"]);
                    if (GName.Length > 27)
                    {
                        GName = GName.Substring(0, 27) + "...";
                    }
                    LGoal.Append("<li style='line-height:25px;font-size:14px;font-weight:bold;'>" + GName + "</li>");
                }
            }
            LGoal.Append("</ul>");
        }
        return Convert.ToString(LGoal);
    }

    protected string BindGoalsHappeningToday()
    {
        SedogoNewFun objSNFun = new SedogoNewFun();
        DataTable dtHGoal = objSNFun.GetGoalsHappeningToday();

        StringBuilder HGoal = new StringBuilder();
        int i;

        if (dtHGoal.Rows.Count > 0)
        {
            string GName = string.Empty;

            HGoal.Append("<ul>");
            if (dtHGoal.Rows.Count > 3)
            {
                for (i = 0; i < 3; i++)
                {
                    GName = Convert.ToString(dtHGoal.Rows[i]["EventName"]);
                    if (GName.Length > 27)
                    {
                        GName = GName.Substring(0, 27) + "...";
                    }
                    HGoal.Append("<li style='line-height:25px;font-size:14px;'>" + GName + "</li>");
                }
                HGoal.Append("<li style='line-height:25px;font-size:14px;font-weight:bold;'><a href='HomeMoreDetail.aspx?type=happening' title='Click here to view more achieved Goals' style='font-weight:bold;font-size:13px;'>More ></a></li>");
            }
            else
            {
                for (i = 0; i < dtHGoal.Rows.Count; i++)
                {
                    GName = Convert.ToString(dtHGoal.Rows[i]["EventName"]);
                    if (GName.Length > 27)
                    {
                        GName = GName.Substring(0, 27) + "...";
                    }

                    HGoal.Append("<li style='line-height:25px;font-size:14px;'>" + GName + "</li>");
                }
            }
            HGoal.Append("</ul>");
        }
        return Convert.ToString(HGoal);
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

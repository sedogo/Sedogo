<%@ Control Language="C#" AutoEventWireup="true" CodeFile="userProfileControl.ascx.cs"
    Inherits="components_userProfileControl" %>

<script type="text/javascript">	
    function ShowHideDiv(divId)
     {
        var shdiv = document.getElementsByTagName('div');
        for(i=0; i < shdiv.length; i++)
        {
            if(shdiv.item(i).id.indexOf("dmpop") > -1)
            {
                document.getElementById(shdiv.item(i).id).style.display='none';
            }
        }
		document.getElementById('dmpop' + divId).style.display='block';
	 }
	 
    function CloseDiv()
     {
        var shdiv = document.getElementsByTagName('div');
        for(i=0; i < shdiv.length; i++)
        {
            if(shdiv.item(i).id.indexOf("dmpop") > -1)
            {
                document.getElementById(shdiv.item(i).id).style.display='none';
            }
        }		
	 }
</script>

<div class="userProfileBox">
    <table width="100%" border="0" cellspacing="2" cellpadding="2">
        <tr>
            <td>
                <h1 class="blue">
                    <asp:Label runat="server" ID="firstNameLabel" />'s profile<br />
                </h1>
                <asp:HyperLink ID="usersProfileNameLabel" Text="View Timeline" runat="server" CssClass="blue" />
            </td>
            <td align="right">
                <table border="0" cellspacing="2" cellpadding="2">
                    <tr>
                        <td style="padding: 4px 0">
                            <a href="/profile.aspx" class="grey">Close</a>
                        </td>
                        <td>
                            <a href="/profile.aspx">
                                <img src="/images/close-modal.gif" /></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <table border="0" cellspacing="2" cellpadding="2">
                    <tr>
                        <td>
                            Home town:
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label runat="server" ID="homeTownLabel" />
                        </td>
                    </tr>
                    <tr id="birthdayRow" runat="server">
                        <td>
                            Birthday:
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label runat="server" ID="birthdayLabel" />
                        </td>
                    </tr>
                    <tr id="ageRow" runat="server">
                        <td>
                            Age:
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label runat="server" ID="ageLabel" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br />
                            <asp:Label runat="server" ID="headlineLabel" />
                        </td>
                    </tr>
                </table>
            </td>
            <td align="right">
                <p style="display: block">
                    <asp:Image ID="profileImage" runat="server" CssClass="profile" /></p>
            </td>
        </tr>
    </table>
    <table border="0" cellspacing="2" cellpadding="2" width="100%">
        <tr>
            <td width="262">
                &nbsp;
            </td>
            <td width="262">
                <div style="margin-left: 2px; margin-top: 30px; clear: both; visibility: hidden;">
                    <asp:DataList ID="dlMember" runat="server" RepeatColumns="6" RepeatDirection="Horizontal"
                        DataKeyField="UserId">
                        <ItemTemplate>
                            <div>
                                <div class="misc-pop-up" id="dmpop<%# DataBinder.Eval(Container.DataItem, "userId") %>"
                                    style="display: none; position: relative; z-index: 10;">
                                    <div class="simileAjax-bubble-container simileAjax-bubble-container-pngTranslucent"
                                        style="width: 160px; height: 95px; left: 0px; bottom: 35px;">
                                        <div class="simileAjax-bubble-innerContainer simileAjax-bubble-innerContainer-pngTranslucent">
                                            <div class="simileAjax-bubble-border-top-left simileAjax-bubble-border-top-left-pngTranslucent">
                                            </div>
                                            <div class="simileAjax-bubble-border-top-right simileAjax-bubble-border-top-right-pngTranslucent">
                                            </div>
                                            <div class="simileAjax-bubble-border-bottom-left simileAjax-bubble-border-bottom-left-pngTranslucent">
                                            </div>
                                            <div class="simileAjax-bubble-border-bottom-right simileAjax-bubble-border-bottom-right-pngTranslucent">
                                            </div>
                                            <div class="simileAjax-bubble-border-left simileAjax-bubble-border-left-pngTranslucent">
                                            </div>
                                            <div class="simileAjax-bubble-border-right simileAjax-bubble-border-right-pngTranslucent">
                                            </div>
                                            <div class="simileAjax-bubble-border-top simileAjax-bubble-border-top-pngTranslucent">
                                            </div>
                                            <div class="simileAjax-bubble-border-bottom simileAjax-bubble-border-bottom-pngTranslucent">
                                            </div>
                                            <div class="simileAjax-bubble-contentContainer simileAjax-bubble-contentContainer-pngTranslucent">
                                                <div style="position: static; width: 160px; font-size: 14px;">
                                                    <div style="float: left; width: 160px;">
                                                        <img width="25" height="25" alt="" src="assets/profilePics/<%# DataBinder.Eval(Container.DataItem, "ProfilePicThumbnail") %>" /><br />
                                                        <span class="blue" style="line-height: 27px;">
                                                            <%# DataBinder.Eval(Container.DataItem, "FirstName") %>
                                                        </span>
                                                        <br />
                                                        <%# DataBinder.Eval(Container.DataItem, "GCount")+ " Goals" %><br />
                                                    </div>
                                                    <%--<div style="float: left; width: 50px;">
                                                        <img width="25" height="25" src="assets/profilePics/<%# DataBinder.Eval(Container.DataItem, "ProfilePicThumbnail") %>" />
                                                    </div>--%>
                                                    <div style="width: 160px; float: left;">
                                                        <span class="blue" style="line-height: 27px;"><a href='userTimeline.aspx?UID=<%# DataBinder.Eval(Container.DataItem, "UserId") %>'>
                                                            View Timeline</a></span></div>
                                                </div>
                                            </div>
                                            <div class="simileAjax-bubble-close simileAjax-bubble-close-pngTranslucent misc-pop-up-link-close">
                                            </div>
                                            <div class="simileAjax-bubble-arrow-point-down simileAjax-bubble-arrow-point-down-pngTranslucent"
                                                style="left: 1px;">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <img src="assets/profilePics/<%# DataBinder.Eval(Container.DataItem, "ProfilePicThumbnail") %>"
                                    alt="" height="33" width="33" style="cursor: pointer; padding-bottom: 6px; padding-right: 6px;"
                                    onerror="this.src='images/eventThumbnailBlank.png'" onmouseover="ShowHideDiv(<%# DataBinder.Eval(Container.DataItem, "userId") %>)" /></div>
                        </ItemTemplate>
                    </asp:DataList></div>
            </td>
            <td width="206">
                <br />
                <br />
                <div class="userProfileBox" style="height: 145px;">
                    <fieldset>
                        <ol style="line-height: 20px;">
                            <li><span class="blue">
                                <asp:Label runat="server" ID="userProfilePopupGoalsLabel" /></span> Goals </li>
                            <li><span class="blue">
                                <asp:Label runat="server" ID="userProfilePopupGoalsAchievedLabel" /></span> Goals
                                achieved </li>
                            <li><span class="blue">
                                <asp:Label runat="server" ID="userProfilePopupGroupGoalsLabel" /></span> Group goals
                            </li>
                            <li><span class="blue">
                                <asp:Label runat="server" ID="userProfilePopupGoalsFollowedLabel" /></span> Goals
                                followed </li>
                        </ol>
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>
</div>

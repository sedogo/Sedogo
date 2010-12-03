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
	 function sendMessage(userID)
	 {
	     openModal("sendUserMessage.aspx?EID=-1&UID=" + userID);
	 } 
</script>

<div class="userProfileBox">
    <table width="100%" border="0" cellspacing="2" cellpadding="2">
        <tr>
            <td>
                <table border="0">
                    <tr>
                        <td><h2 class="blue"><asp:Label runat="server" ID="firstNameLabel" />'s profile</h2></td>
                        <td>&nbsp;&nbsp;</td>
                        <td style="padding-top:5px"><asp:Hyperlink ID="messageLink" runat="server" ImageUrl="~/images/messages.gif" /></td>
                        <td>&nbsp;&nbsp;</td>
                        <td style="padding-top:5px"><asp:HyperLink ID="usersProfileNameLabel" Text="View Timeline" runat="server" CssClass="blue" /></td>
                    </tr>
                </table>
                
                <p style="font-style: italic; color: #ccc; margin: 10px 0 4px 0; font-size: 11px;
                width: 485px;">Edited <asp:Label ID="lastUpdatedDateLabel" runat="server"></asp:Label></p>
                
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
    <table width="100%" border="0">
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
                    <tr id="editProfileRow" runat="server" visible="false">
                        <td colspan="3">
                            <br />
                            <asp:HyperLink ID="editProfileLink" NavigateUrl="~/editProfile.aspx" ToolTip="Edit profile"
                                Text="Edit" runat="server" />
                        </td>
                    </tr>
                    <tr id="loginRow" runat="server">
                        <td colspan="3">
                            &nbsp;<br />
                            To see more, <asp:Hyperlink runat="server" CssClass="modal" ID="loginLink"
                                Text="click here to login" /><br />or if you don't have a Sedogo account
                                yet, <asp:Hyperlink runat="server" CssClass="modal" ID="registerLink"
                                Text="click here to sign up" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
            <td align="right">
                <p style="display: block">
                    <asp:Image ID="profileImage" runat="server" CssClass="profile" /></p>
            </td>
        </tr>
        <tr>
            <td>
                <div style="margin-left: 2px;">
                    <asp:DataList ID="dlMember" runat="server" RepeatColumns="12" RepeatDirection="Horizontal"
                        DataKeyField="EventID" OnItemDataBound="OnEventsDataBound">
                        <ItemTemplate>
                            <div>
                                <div class="misc-pop-up" id="dmpop<%# DataBinder.Eval(Container.DataItem, "EventID") %>"
                                    style="display: none; position: relative; z-index: 10;">
                                    <div class="simileAjax-bubble-container simileAjax-bubble-container-pngTranslucent"
                                        style="width: 160px; height: 70px; left: 0px; bottom: 35px;">
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
                                                        <%--<img width="25" height="25" alt="" src="assets/profilePics/<%# DataBinder.Eval(Container.DataItem, "ProfilePicThumbnail") %>"
                                                            onerror="this.src='images/profile/blankProfile.jpg'" /><br />--%>
                                                        <span class="blue" style="line-height: 22px;">
                                                            <a href='viewEvent.aspx?EID=<%# DataBinder.Eval(Container.DataItem, "EventID") %>'>
                                                            <%# DataBinder.Eval(Container.DataItem, "EventName")%></a>
                                                        </span>
                                                        <br />
                                                    </div>
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
                                <img  runat="server" id="eventPic"
                                    alt="" height="33" width="33" style="cursor: pointer; padding-bottom: 6px; padding-right: 6px;"
                                    onerror="this.src='images/grayRect.jpg'" /></div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </td>
            <td></td>
            <td align="right">
                <div class="userProfileBox" style="text-align:left; width:192px">
                    <fieldset>
                        <ol style="line-height: 20px;">
                            <li><asp:Label runat="server" ID="userProfilePopupGoalsLabel" /> Goals</li>
                            <li><asp:Label runat="server" ID="userProfilePopupGoalsAchievedLabel" /> Goals achieved </li>
                            <li><asp:Label runat="server" ID="userProfilePopupGroupGoalsLabel" /> Group goals</li>
                            <li><asp:Label runat="server" ID="userProfilePopupGoalsFollowedLabel" /> Goals followed </li>
                        </ol>
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>
</div>

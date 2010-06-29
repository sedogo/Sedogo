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
                        <td><h2 class="blue"><asp:Label runat="server" ID="firstNameLabel" />'s profile</h2</td>
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
            <td align="right">
                <p style="display: block">
                    <asp:Image ID="profileImage" runat="server" CssClass="profile" /></p>
                    
                <div class="clear-float"></div>
                
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

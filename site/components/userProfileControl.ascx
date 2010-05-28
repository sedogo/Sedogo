<%@ Control Language="C#" AutoEventWireup="true" CodeFile="userProfileControl.ascx.cs" 
    Inherits="components_userProfileControl" %>

<div class="userProfileBox">

    <table width="100%" border="0" cellspacing="2" cellpadding="2">
        <tr>
            <td><h1 class="blue"><asp:Label runat="server" ID="firstNameLabel" />'s profile</h1></td>
            <td align="right">
                <table border="0" cellspacing="2" cellpadding="2">
                    <tr>
                        <td style="padding:4px 0"><a href="/profile.aspx" class="grey">Close</a></td>
                        <td><a href="/profile.aspx"><img src="/images/close-modal.gif" /></a></td>
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
                        <td>Home town:</td>
                        <td>&nbsp;</td>
                        <td><asp:Label runat="server" ID="homeTownLabel" /></td>
                    </tr>
                </table>
            
                <br />
                <asp:Label runat="server" ID="headlineLabel" />
                <br />
            
            </td>
            <td align="right">
                <p style="display: block"><asp:Image ID="profileImage" runat="server" CssClass="profile" /></p>
            </td>
        </tr>
    </table>
    
    <table border="0" cellspacing="2" cellpadding="2" width="100%">
        <tr>
            <td width="262">&nbsp;</td>
            <td width="262">&nbsp;</td>
            <td width="206">
            
                <div class="userProfileBox">
                <fieldset>
                    <ol>
                        <li>
                            <span class="blue"><asp:Label runat="server" ID="userProfilePopupGoalsLabel" /></span> Goals
                        </li>
                        <li>
                            <span class="blue"><asp:Label runat="server" ID="userProfilePopupGoalsAchievedLabel" /></span> Goals achieved
                        </li>
                        <li>
                            <span class="blue"><asp:Label runat="server" ID="userProfilePopupGroupGoalsLabel" /></span> Group goals
                        </li>
                        <li>
                           <span class="blue"><asp:Label runat="server" ID="userProfilePopupGoalsFollowedLabel" /></span> Goals followed
                        </li>
                    </ol>
                </fieldset>
                </div>
            
            </td>
        </tr>
    </table>

</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="userProfileControl.ascx.cs" 
    Inherits="components_userProfileControl" %>

<div class="userProfileBox">

    <h1 class="blue"><asp:Label runat="server" ID="firstNameLabel" />'s profile</h1>

    <table width="100%">
        <tr>
            <td>

                <table border="0" cellspacing="2" cellpadding="2">
                    <tr>
                        <td>Age</td>
                        <td>&nbsp;&nbsp;</td>
                        <td><asp:Label runat="server" ID="ageLabel" /></td>
                    </tr>
                    <tr>
                        <td>Birthday</td>
                        <td></td>
                        <td><asp:Label runat="server" ID="birthdayLabel" /></td>
                    </tr>
                    <tr>
                        <td>Home town</td>
                        <td></td>
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
            <td width="33%">&nbsp;</td>
            <td width="33%">&nbsp;</td>
            <td width="33%">
            
                <div class="userProfileBox">
                <fieldset>
                    <ol class="width-constrain">
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

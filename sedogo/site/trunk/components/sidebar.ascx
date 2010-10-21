<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sidebar.ascx.cs" Inherits="sidebar" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript">
function changeClass(id, newClass)
{
    document.getElementById(id).className = newClass; //.setAttribute("class", newClass);

    //sidebarControl_messageCountLink
    //sidebarControl_alertCountLink
    //sidebarControl_inviteCountLink
    //sidebarControl_goalJoinRequestsLink
    //sidebarControl_trackingCountLink
    //sidebarControl_groupGoalsLink
    //sidebarControl_addressBookLink
     
    
    //alert("Params: " + id + ":" + newClass);
    //element = document.getElementById(id);
    //alert(element);
    //event.cancelBubble = true;
    //element.style.className = newClass;
}
</script>

<script type="text/JavaScript">
        DD_roundies.addRule('.button-sml', '5px', true);
</script>

<div class="one-col">
    <div style="width: 100%; float: left; border-bottom: 2px solid #0cf;" id="myProfileDiv" runat="server">
        <div style="float: left;">
            <h2>
                <asp:HyperLink ID="myProfileTextLabel" runat="server" Text="My profile" /></h2>
        </div>
        <div style="padding: 4px; text-align: right; padding-top: 8px;">
            <asp:HyperLink ID="editProfileLink" NavigateUrl="~/editProfile.aspx" ToolTip="Edit profile"
                Text="Edit" runat="server" />
        </div>
    </div>
    <div style="width: 100%; float: left; padding-bottom: 4px;">
    </div>
    <div id="profileImageDiv" runat="server">
    <asp:Image ID="profileImage" runat="server" CssClass="profile" />
    <span style="font-size: 14px; font-weight: bold; color: Black">
        <asp:Label ID="userNameLabel" runat="server" /></span><br />
    <br />
    </div>
    <div id="extraButtonsDiv" runat="server">
    <p class="extra-buttons">
        <asp:LinkButton ID="viewArchiveLink" runat="server" Text="view archive" CssClass="button-sml"
            OnClick="ClickViewArchiveLink" />
        <asp:HyperLink ID="addGoalLink" NavigateUrl="~/addEvent.aspx" ToolTip="add goal"
            CssClass="button-sml modal" Text="+ Goal" runat="server" />
    </p>
    </div>
    <asp:Label ID="goalsAchievedBelowProfileImageLabel" runat="server" CssClass="goalsAchievedBelowProfile"
        Visible="false" />
    <div id="sidebarMenuItems" runat="server">
        <ol class="items">
            <li class="messages">
                <asp:HyperLink ID="messageCountLink" runat="server" NavigateUrl="~/message.aspx" /></li>
            <li class="alerts">
                <asp:HyperLink ID="alertCountLink" NavigateUrl="~/alert.aspx" runat="server" /></li>
            <li class="invites">
                <asp:HyperLink ID="inviteCountLink" NavigateUrl="~/invite.aspx" runat="server" /></li>
            <li class="requests">
                <asp:HyperLink ID="goalJoinRequestsLink" NavigateUrl="~/eventJoinRequests.aspx" runat="server" /></li>
            <li class="following">
                <asp:HyperLink ID="trackingCountLink" NavigateUrl="~/tracking.aspx" runat="server" /></li>
            <li class="goal-groups">
                <asp:HyperLink ID="groupGoalsLink" NavigateUrl="~/groupGoals.aspx" runat="server" /></li>
            <li class="achievedgoals">
                <asp:HyperLink ID="achievedGoalsLink" NavigateUrl="~/goalsAchieved.aspx" runat="server" /></li>
            <li class="addressbook">
                <asp:HyperLink ID="addressBookLink" NavigateUrl="~/addressBook.aspx" runat="server" /></li>
        </ol>
    </div>
    <div class="latestGoals">
        
        <h2><asp:Label ID="Label1" runat="server" Text="Added" /></h2>
        <asp:PlaceHolder ID="goalsAddedPlaceHolder" runat="server" />
        <div class="pinstripe-divider"></div>

        <h2><asp:Label ID="Label2" runat="server" Text="Updated" /></h2>
        <asp:PlaceHolder ID="goalsUpdatedPlaceHolder" runat="server" />
        <div class="pinstripe-divider"></div>

        <h2><asp:Label ID="Label3" runat="server" Text="Happening now" /></h2>
        <asp:PlaceHolder ID="goalsHappeningNowPlaceHolder" runat="server" />
        <div class="pinstripe-divider"></div>

        <h2><asp:Label ID="Label4" runat="server" Text="Achieved" /></h2>
        <asp:PlaceHolder ID="goalsAchievedPlaceHolder" runat="server" />
        <div class="pinstripe-divider"></div>
        
        <asp:Panel runat="server" ID="similarPanel">
            <h2><asp:Label ID="Label5" runat="server" Text="Similar" /></h2>
            <asp:PlaceHolder ID="goalsSimilarPlaceHolder" runat="server" />
            <div class="pinstripe-divider"></div>
        </asp:Panel>
        <asp:Panel runat="server" ID="otherPanel">
            <h2><asp:Label ID="Label6" runat="server" Text="Other" /></h2>
            <asp:PlaceHolder ID="goalsOtherPlaceHolder" runat="server" />
            <div class="pinstripe-divider"></div>
        </asp:Panel>
    </div>
    <p>
        &nbsp;</p>
    <div class="rotatorBackground" id="rotatorBackgroundDiv" runat="server">
        <telerik:RadRotator ID="eventRotator" runat="server" Width="232px" Height="216px"
            CssClass="horizontalRotator" ScrollDuration="500" FrameDuration="5000" ItemHeight="216"
            ItemWidth="232" RotatorType="FromCode">
            <ItemTemplate>
                <div class="itemTemplate">
                    <a href="getInspired.aspx">
                        <img src='<%# Page.ResolveUrl("~/images/") + Container.DataItem %>.png' alt="Customer Image" /></a>
                </div>
            </ItemTemplate>
        </telerik:RadRotator>
    </div>
</div>

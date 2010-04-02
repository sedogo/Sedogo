<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sidebar.ascx.cs" Inherits="sidebar" %>

<div class="one-col">
    <h2 class="col-header">
        My profile <span><asp:HyperLink NavigateUrl="~/editProfile.aspx" ToolTip="Edit profile" Text="Edit" runat="server" /></span></h2>
    <asp:Image ID="profileImage" runat="server" CssClass="profile" />
    <p class="profile-name">
        <span style="font-size: 14px; font-weight: bold">
            <asp:Label ID="userNameLabel" runat="server" /></span><br />
        <p class="profile-intro">
            <asp:Label ID="profileTextLabel" runat="server" /></p>
        <br />
        <p class="extra-buttons">
            <asp:LinkButton ID="viewArchiveLink" runat="server" Text="view archive" CssClass="button-sml"
                OnClick="click_viewArchiveLink" />
            <asp:HyperLink NavigateUrl="~/addEvent.aspx" ToolTip="add goal" CssClass="button-sml modal" 
                Text="+ Goal" runat="server" /></a>
        </p>
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
            <li class="goal-groups">
                <asp:HyperLink ID="addressBookLink" NavigateUrl="~/addressBook.aspx" runat="server" /></li>
        </ol>
        <div class="alerts">
            <h3>
                My latest goals</h3>
            <p>
                <asp:PlaceHolder ID="latestEventsPlaceholder" runat="server" />
            </p>
            <div class="pinstripe-divider">
            </div>
            <!--<h3>Latest searches</h3>-->
            <!--<p><asp:PlaceHolder id="latestSearchesPlaceholder" runat="server" /></p>-->
            <!--<div class="pinstripe-divider"></div>-->
            <h3>
                Popular goals</h3>
            <p>
                <asp:PlaceHolder ID="popularSearchesPlaceholder" runat="server" />
            </p>
        </div>
</div>

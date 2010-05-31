<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sidebar.ascx.cs" Inherits="sidebar" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript" src="js/DD_roundies_0.0.2a-min.js"></script>
<script type="text/javascript" src="js/DD_roundies_0.0.2a-min.js"></script>
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
    <div style="width: 100%; float: left; border-bottom: 2px solid #0cf;">
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
    <%--<h2 class="col-header">
        <asp:HyperLink ID="myProfileTextLabel" runat="server" Text="My profile" />
        <span>
            <asp:HyperLink ID="editProfileLink" NavigateUrl="~/editProfile.aspx" ToolTip="Edit profile"
                Text="Edit" runat="server" /></span></h2>--%>
    <asp:Image ID="profileImage" runat="server" CssClass="profile" />
    <span style="font-size: 14px; font-weight: bold; color: Black">
        <asp:Label ID="userNameLabel" runat="server" /></span><br />
    <!--<p class="profile-intro">-->
    <!--<asp:Label ID="profileTextLabel" runat="server" /></p>-->
    <br />
    <p class="extra-buttons">
        <asp:LinkButton ID="viewArchiveLink" runat="server" Text="view archive" CssClass="button-sml"
            OnClick="click_viewArchiveLink" />
        <asp:HyperLink ID="addGoalLink" NavigateUrl="~/addEvent.aspx" ToolTip="add goal"
            CssClass="button-sml modal" Text="+ Goal" runat="server" />
    </p>
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
        <h3>
            <asp:Label ID="myLatestGoalsLabel" runat="server" Text="My latest goals" /></h3>
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
        <div class="pinstripe-divider">
        </div>
        <h3>
            <asp:Label ID="goalupcomingLabel" runat="server" Text="Goals up and coming" /></h3>
        <p>
            <asp:PlaceHolder ID="goalupcomingPlaceHolder" runat="server" />
        </p>
    </div>
    <p>
        &nbsp;</p>
    <div class="rotatorBackground">
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

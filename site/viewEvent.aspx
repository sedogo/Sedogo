<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewEvent.aspx.cs" Inherits="viewEvent"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="0" />
	<meta http-equiv="pragma" content="no-cache" />

	<title>View event : Sedogo : Create your future and connect with others to make it happen</title>

	<meta name="keywords" content="" />
	<meta name="description" content="" />
	<meta name="author" content="" />
	<meta name="copyright" content="" />
	<meta name="robots" content="" />
	<meta name="MSSmartTagsPreventParsing" content="true" />

	<meta http-equiv="imagetoolbar" content="no" />
	<meta http-equiv="Cleartype" content="Cleartype" />

	<link rel="stylesheet" href="css/main.css" />
	<!--[if IE]>
		<link rel="stylesheet" href="css/main_ie.css" />
	<![endif]-->
	<!--[if gte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->

	<script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="js/jquery.corner.js"></script>
	<script type="text/javascript" src="js/main.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>

	    <div id="modal">
	    
    	    <div id="modalContents">

                <div id="event-detail">
                    <div class="right-col">
						<div style="width: 170px; padding-top: 70px; overflow: hidden">
	                        <asp:Image ID="eventImage" runat="server" Width="170" />
                            <asp:LinkButton ID="uploadEventImage" runat="server" OnClick="click_uploadEventImage" Text="Upload image" />
                        </div>
                        <!--<h3 ID="messagesHeader" runat="server">Messages</h3>-->
                        <!--<p><asp:HyperLink ID="messagesLink" runat="server" NavigateUrl="~/message.aspx" /></p>-->
                        <h3 ID="alertsHeader" runat="server">Reminders</h3>
                        <asp:PlaceHolder ID="alertsPlaceHolder" runat="server" />
                        <p><asp:LinkButton ID="alertsLink" runat="server" OnClick="click_alertsLink" Text="Edit" /></p>
                        <h3 ID="trackingHeader" runat="server" style="color: #0cf">Members</h3>
                        <asp:PlaceHolder ID="trackingLinksPlaceholder" runat="server" />
                        <img src="./images/ico_messages.gif" /><asp:LinkButton ID="messageTrackingUsersLink" runat="server" Text="Message All" 
                            OnClick="click_messageTrackingUsersLink" />
                        <h3 ID="H1" runat="server">Followers</h3>
                        <asp:PlaceHolder ID="followersLinksPlaceholder" runat="server" />
                        <h3 ID="invitesHeader" runat="server">Invites:</h3>
                        <p><asp:Label ID="inviteCountLabel" runat="server"></asp:Label><br />
                        <asp:LinkButton ID="invitesLink" runat="server" OnClick="click_inviteUsersLink" Text="Invite People" /></p>
                        
                        &nbsp;<br />&nbsp;<br />&nbsp;<br />&nbsp;
                    </div>
                    <div class="left-col">
		                <h1 style="color: #0cf; margin: 0 0 4px 0"><asp:Literal ID="eventTitleLabel" runat="server" /></h1>
						<p style="font-style: italic; color: #ccc; margin: 0 0 4px 0; font-size: 11px">Edited <asp:Label ID="lastUpdatedDateLabel" runat="server"></asp:Label></p>
		                <table class="summary">
							<tbody>
								<tr>
									<td colspan="2"><asp:Label ID="eventDescriptionLabel" runat="server" /></td>
								</tr>
								<tr>
									<th>Who:</th>
									<td class="blue"><asp:HyperLink ID="eventOwnersNameLabel" runat="server" Target="_top" /></td>
								</tr>
								<tr>
									<th>Goal:</th>
									<td><asp:Label ID="eventLabel1" runat="server" /></td>
								</tr>
								<tr>
									<th>Where:</th>
									<td><asp:Label ID="eventVenueLabel" runat="server" /></td>
								</tr>
								<tr>
									<th>Before:</th>
									<td><asp:Label ID="eventDateLabel" runat="server" /></td>
								</tr>
							</tbody>
		                </table>
                        <asp:HyperLink ID="editEventLink" runat="server" Text="Edit" ToolTip="Edit" />
		                
		                <div id="sendMessageDiv" runat="server">
		                <asp:LinkButton id="sendMessageButton" runat="server" OnClick="sendMessageButton_click">Send Message</asp:LinkButton>
        		        </div>
        		        
				<div class="pinstripe-divider">&nbsp;</div>
        		        
        		        <h3 style="font-size: 13px; color: #0cf">Posts about <asp:Label ID="eventLabel2" runat="server" /></h3>

                        <div style="margin:10px 0 10px 0">
                        <asp:LinkButton ID="postCommentButton" runat="server" 
                            ToolTip="post a comment" Text="Post a comment" 
                            OnClick="postCommentButton_click" CssClass="button-sml" />
                        </div>

		                <div id="invitedPanel" runat="server">
                        <asp:LinkButton 
                            ID="invitedButton" runat="server" ToolTip="join now" Text="You have been invited to join this goal - join now" 
                            OnClick="invitedButton_click" CssClass="button-sml" />
		                </div>
        		        
		                <div id="loginRegisterPanel" runat="server">
		                <p>You must be logged in to view the full details or to post comments on this event.<br />
		                <a href="login.aspx">Click here to login</a><br />
		                or <a href="register.aspx">click here to register</a> if you are a new user</p>
		                </div>
        		        
		                <asp:PlaceHolder ID="commentsPlaceHolder" runat="server" />
		            </div>
		        </div>

            </div>

        </div>

        <div class="buttons">
            <asp:LinkButton ID="achievedEventLink" runat="server" CssClass="button-lrg" Text="Done" ToolTip="Achieved" OnClick="click_achievedEventLink" />
            <asp:LinkButton ID="deleteEventButton" runat="server" ToolTip="Delete goal" Text="Delete goal" OnClick="deleteEventButton_click" CssClass="button-lrg" />
            <asp:LinkButton ID="createSimilarEventLink" runat="server" Text="Copy Goal" OnClick="createSimilarEventLink_click" CssClass="button-lrg" />
			<asp:LinkButton ID="trackThisEventLink" runat="server" Text="Follow this goal" OnClick="trackThisEventLink_click" CssClass="button-lrg" />
			<asp:LinkButton ID="joinThisEventLink" runat="server" Text="Join" OnClick="joinThisEventLink_click" CssClass="button-lrg" />
        </div>
    
    </div>
    </form>
</body>
</html>

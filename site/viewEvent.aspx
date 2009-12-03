<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewEvent.aspx.cs" Inherits="viewEvent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
	<!--[if gte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->
</head>
<body>
    <form id="form1" runat="server">
    <div>

	    <div id="modal">
	    
    	    <div id="modalContents">

                <div id="event-detail">
                    <div class="right-col">
                        <asp:Image ID="eventImage" runat="server" />
                        <!--<h3 ID="messagesHeader" runat="server">Messages</h3>-->
                        <!--<p><asp:HyperLink ID="messagesLink" runat="server" NavigateUrl="~/message.aspx" /></p>-->
                        <h3 ID="invitesHeader" runat="server">Invite people</h3>
                        <p><asp:LinkButton ID="invitesLink" runat="server" OnClick="click_inviteUsersLink" /></p>
                        <h3 ID="alertsHeader" runat="server">Alerts</h3>
                        <p><asp:LinkButton ID="alertsLink" runat="server" OnClick="click_alertsLink" /></p>
                        <h3 ID="trackingHeader" runat="server">Members</h3>
                        <asp:PlaceHolder ID="trackingLinksPlaceholder" runat="server" />
                        <h3 ID="H1" runat="server">Followers</h3>
                        <asp:PlaceHolder ID="followersLinksPlaceholder" runat="server" />
                    </div>
                    <div class="left-col">
		                <h1 style=""><asp:Literal ID="eventTitleLabel" runat="server" /></h1>
		                <p><asp:Label ID="eventDescriptionLabel" runat="server" /><br />
		                <i>Owner: <asp:HyperLink ID="eventOwnersNameLabel" runat="server" Target="_top" /></i><br />
		                Where: <asp:Label ID="eventVenueLabel" runat="server" /><br />
		                <asp:Label ID="eventDateLabel" runat="server" /></p>
        		        
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
            <asp:LinkButton ID="uploadEventImage" runat="server" OnClick="click_uploadEventImage"
                Text="Upload image" CssClass="button-sml" />
            <asp:LinkButton ID="trackThisEventLink" runat="server" Text="Track this goal"
                OnClick="trackThisEventLink_click" />
            <asp:LinkButton ID="createSimilarEventLink" runat="server" Text="Create a goal like this for me"
                OnClick="createSimilarEventLink_click" CssClass="button-sml" />
            <asp:LinkButton ID="messageTrackingUsersLink" runat="server" Text="send message to trackers" 
                OnClick="click_messageTrackingUsersLink" CssClass="button-sml" />

            <asp:HyperLink ID="editEventLink" runat="server" CssClass="button-sml" Text="edit" ToolTip="edit" />
            <asp:LinkButton ID="achievedEventLink" runat="server" CssClass="button-sml" Text="done" 
                ToolTip="achieved" OnClick="click_achievedEventLink" />
            <asp:LinkButton ID="sendMessageButton" runat="server" ToolTip="send a message" 
                Text="send a message" OnClick="sendMessageButton_click" CssClass="button-sml" />
            <asp:LinkButton 
                ID="postCommentButton" runat="server" ToolTip="post a comment" Text="post a comment" 
                OnClick="postCommentButton_click" CssClass="button-sml" />
            <asp:LinkButton ID="joinThisEventLink" runat="server" Text="Join this goal"
                OnClick="joinThisEventLink_click" CssClass="button-sml" />
                <asp:Label ID="joinThisEventLabel" runat="server" Text="Joined" />
            <asp:LinkButton 
                ID="deleteEventButton" runat="server" ToolTip="delete goal" Text="delete goal" 
                OnClick="deleteEventButton_click" CssClass="button-sml" />
        </div>
    
    </div>
    </form>
</body>
</html>

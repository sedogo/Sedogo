<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewEvent.aspx.cs" Inherits="viewEvent"
    EnableEventValidation="false" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="favicon.ico" >  
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="0" />
	<meta http-equiv="pragma" content="no-cache" />

	<title><asp:Literal ID="pageTitleUserName" runat="server" /></title>

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
	<!--[if lte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->

	<script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="js/jquery-ui-1.7.2.custom.min.js"></script>
	<script type="text/javascript" src="js/ui.dialog.js"></script>
	<script type="text/javascript" src="js/jquery.cookie.js"></script>
	<script type="text/javascript" src="js/jquery.livequery.js"></script>
	<script type="text/javascript" src="js/jquery.corner.js"></script>
	<script type="text/javascript" src="js/main.js"></script>
    <script type="text/javascript" src="utils/validationFunctions.js"></script>

	<script type="text/javascript">
    function confirmDelete(eventID, trackedEventID)
	{
	    if (confirm("Are you sure you want to delete this member?") == true)
	    {
	        location.href = "viewEvent.aspx?A=RemoveTracker&EID=" + eventID + "&TEID=" + trackedEventID;
	    }
	}
	function uploadEventImage()
	{
	    <asp:Literal id="uploadEventImageLiteral" runat="server" />
	    openModal(url);
	}
	function editEvent()
	{
	    <asp:Literal id="editEventLiteral" runat="server" />
	    openModal(url);
	}
	function eventAlerts()
	{
	    <asp:Literal id="eventAlertsLiteral" runat="server" />
	    openModal(url);
	}
	function messageTrackingUsers()
	{
	    <asp:Literal id="messageTrackingUsersLiteral" runat="server" />
	    openModal(url);
	}
	function messageFollowingUsers()
	{
	    <asp:Literal id="messageFollowingUsersLiteral" runat="server" />
	    openModal(url);
	}
	function sendMessage()
	{
	    <asp:Literal id="sendMessageLiteral" runat="server" />
	    openModal(url);
	}
	</script>
	
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>    
    
	    <div id="container">
	        <Sedogo:BannerLoginControl ID="bannerLogin" runat="server" />
	        <Sedogo:BannerAddFindControl ID="bannerAddFindControl" runat="server" />

		    <div id="other-content">
                <Sedogo:SidebarControl ID="sidebarControl" runat="server" />

			    <div class="three-col">

                    <div class="page-banner-content" id="pageBannerBarDiv" runat="server">
                        <div class="page-banner-header"><asp:Literal ID="eventTitleLabel" runat="server" /></div>
                        <div class="page-banner-backbutton"><asp:LinkButton id="backButton" runat="server" Text="Back" 
                            CssClass="page-banner-linkstyle" OnClick="backButton_click" CausesValidation="false" /></div>
                    </div>

                    <table width="100%" border="0" cellspacing="0" cellpadding="2">
                        <tr>
                            <td>
                            
				                <p style="font-style: italic; color: #ccc; margin: 0 0 4px 0; font-size: 11px">Edited <asp:Label ID="lastUpdatedDateLabel" runat="server"></asp:Label></p>
                                <table class="summary">
					                <tbody>
						                <tr>
							                <td colspan="2"><asp:Label ID="eventDescriptionLabel" runat="server" /></td>
						                </tr>
						                <tr>
							                <th colspan="2">&nbsp;</th>
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
							                <th><asp:Label ID="dateLabel" runat="server" />:</th>
							                <td><asp:Label ID="eventDateLabel" runat="server" /></td>
						                </tr>
					                </tbody>
                                </table>
                                <asp:Hyperlink ID="editEventLink" runat="server" NavigateUrl="javascript:editEvent()" 
                                    Text="Edit" CssClass="underline-bold" />
            	                
                                <div id="sendMessageDiv" runat="server">
                                <asp:Hyperlink id="sendMessageButton" runat="server" Visible="false"
                                    NavigateUrl="javascript:sendMessage();">Send Message</asp:Hyperlink>
		                        </div>
                		        
		                        <div class="pinstripe-divider">&nbsp;</div>

                		        <div class="eventCommentBox" id="eventCommentBox" runat="server">
                		        <table width="100%" cellspacing="2" cellpadding="2">
                		            <tr>
                		                <td colspan="2">
                		                    <asp:TextBox id="commentTextBox" runat="server" 
                		                        TextMode="MultiLine" Rows="3" ValidationGroup="addCommentGroup"
                		                        Width="100%"></asp:TextBox>
                		                </td>
                		            </tr>
                		            <tr>
                		                <td colspan="2"><asp:RequiredFieldValidator 
            		                        ID="eventNameTextBoxValidator" runat="server"
                                            ControlToValidate="commentTextBox" ValidationGroup="addCommentGroup"
                                            ErrorMessage="A comment is required" Display="Dynamic">
                                            </asp:RequiredFieldValidator></td>
                		            </tr>
                		            <tr>
                		                <td></td>
                		                <td align="right">
                		                    <div style="padding-top:5px; padding-bottom:5px"><asp:LinkButton ID="postCommentButton" runat="server" 
                                            ToolTip="Add comment" Text="Add comment" ValidationGroup="addCommentGroup" 
                                            OnClick="postCommentButton_click" CssClass="button-sml" /></div></td>
                		            </tr>
                		        </table>
                		        </div>
                		        
		                        <h3 style="font-size: 12px; color: #0cf">Comments about <asp:Label ID="eventLabel2" runat="server" /></h3>

                                <div id="invitedPanel" runat="server">
                                <asp:LinkButton 
                                    ID="invitedButton" runat="server" ToolTip="join now" Text="You have been invited to join this goal - join now" 
                                    OnClick="invitedButton_click" CssClass="button-sml" />
                                </div>
                		        
                                <div id="loginRegisterPanel" runat="server" style="border:solid 1px #CCCCCC; padding:10px; margin-bottom:10px">
                                <p>You must be logged in to view the full details or to add comments to this event.<br />
                                <a href="login.aspx" class="modal">Click here to login</a><br />
                                or <a href="register.aspx" class="modal">click here to register</a> if you are a new user</p>
                                </div>
                		        
                                <asp:PlaceHolder ID="commentsPlaceHolder" runat="server" />

                            </td>
                            <td>&nbsp;</td>
                            <td>

				                <div style="width: 170px; padding-top: 70px; overflow: hidden">
                                    <asp:Image ID="eventImage" runat="server" Width="170" />
                                    <asp:Hyperlink ID="uploadEventImage" runat="server" NavigateUrl="javascript:uploadEventImage()" 
                                    Text="Edit picture" CssClass="underline-bold" />
                                </div>
                                <!--<h3 ID="messagesHeader" runat="server">Messages</h3>-->
                                <!--<p><asp:HyperLink ID="messagesLink" runat="server" NavigateUrl="~/message.aspx" /></p>-->
                                <h3 ID="alertsHeader" runat="server" class="reminders-header">Reminders</h3>
                                <p><asp:Hyperlink ID="alertsLink" runat="server" NavigateUrl="javascript:eventAlerts();"
                                    Text="Edit" CssClass="underline-bold" /></p>
                                <asp:PlaceHolder ID="alertsPlaceHolder" runat="server" />
                                <div class="pinstripe-divider">&nbsp;</div>
                                <h3 ID="trackingHeader" runat="server" style="color: #0cf">Members:</h3>
                                <asp:PlaceHolder ID="trackingLinksPlaceholder" runat="server" />
                                <asp:Image ID="messageTrackingImage" runat="server" ImageUrl="./images/messages.gif" /><asp:Hyperlink 
                                    ID="messageTrackingUsersLink" runat="server" Text="Message All" 
                                    NavigateUrl="javascript:messageTrackingUsers();" />
                                <div class="pinstripe-divider">&nbsp;</div>
                                <h3 ID="H1" runat="server">Following:</h3>
                                <asp:PlaceHolder ID="followersLinksPlaceholder" runat="server" />
                                <asp:Image ID="followersTrackingImage" runat="server" ImageUrl="./images/messages.gif" /><asp:Hyperlink 
                                    ID="followersTrackingUsersLink" runat="server" Text="Message All" 
                                    NavigateUrl="javascript:messageFollowingUsers();" CssClass="underline-bold" />
                                <div class="pinstripe-divider">&nbsp;</div>
                                <h3 ID="invitesHeader" runat="server">Invites:</h3>
                                <p><asp:LinkButton ID="inviteCountLabel" runat="server" OnClick="click_inviteUsersLink"></asp:LinkButton><br />
                                <img src="images/invite.gif" /><asp:LinkButton ID="invitesLink" runat="server" OnClick="click_inviteUsersLink" Text="Invite People" CssClass="underline-bold" /></p>
                                <div class="pinstripe-divider">&nbsp;</div>
                                <h3 ID="H2" runat="server">Requests:</h3>
                                <asp:PlaceHolder ID="requestsLinksPlaceholder" runat="server" />
                                
                                <br /><br />
                                <p style="height: 28px"><asp:LinkButton ID="achievedEventLink" runat="server" CssClass="button-sml" Text="Achieved" ToolTip="Achieved" OnClick="click_achievedEventLink" /></p>
				                <p><asp:LinkButton ID="deleteEventButton" runat="server" ToolTip="Delete Goal" Text="Delete goal" OnClick="deleteEventButton_click" CssClass="button-sml" /></p>

                                <div class="buttons">
                                    <asp:LinkButton ID="createSimilarEventLink" runat="server" Text="Copy Goal" OnClick="createSimilarEventLink_click" CssClass="button-lrg" Visible="false" />
			                        <asp:LinkButton ID="trackThisEventLink" runat="server" Text="Follow this goal" OnClick="trackThisEventLink_click" CssClass="button-lrg" />
			                        <asp:LinkButton ID="joinThisEventLink" runat="server" Text="Join" OnClick="joinThisEventLink_click" CssClass="button-lrg" />
                                </div>
                            
                            </td>
                        </tr>
                    </table>

		        </div>
    			
		    </div>
		    <Sedogo:FooterControl ID="footerControl" runat="server" />
	    </div>
        <div id="modal-container">
			<a href="#" class="close-modal"><img src="images/close-modal.gif" title="Close window" alt="Close window" /></a>
            <iframe frameborder="0"></iframe>
        </div>
        <div id="modal-background"></div>
    
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>
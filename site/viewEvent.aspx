<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewEvent.aspx.cs" Inherits="viewEvent"
    EnableEventValidation="false" ValidateRequest="false" %>
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
    <script language="javascript">
    var state = 'none';
    function showhide(layer_ref)
    {
        if (state == 'block')
        {
            state = 'none';
        }
        else
        {
            state = 'block';
        }
        if (document.all)
        { //IS IE 4 or 5 (or 6 beta)
            eval("document.all." + layer_ref + ".style.display = state");
        }
        if (document.layers)
        { //IS NETSCAPE 4 or below
            document.layers[layer_ref].display = state;
        }
        if (document.getElementById && !document.all)
        {
            hza = document.getElementById(layer_ref);
            hza.style.display = state;
        }
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
                        <div class="page-banner-backbutton"><asp:LinkButton id="backButton" runat="server" Text="Home" 
                            CssClass="page-banner-linkstyle" OnClick="backButton_click" CausesValidation="false" /></div>
                    </div>

                    <table width="100%" border="0" cellspacing="0" cellpadding="2">
                        <tr>
                            <td width="300px">
                            
				                <p style="font-style: italic; color: #ccc; margin: 0 0 4px 0; font-size: 11px; margin-top:10px">Edited <asp:Label ID="lastUpdatedDateLabel" runat="server"></asp:Label></p>
                                <table class="summary">
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
                                </table>
                                
                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td><asp:Hyperlink ID="editEventLink" runat="server" NavigateUrl="javascript:editEvent()" 
                                            Text="Edit" CssClass="underline-bold" /></td>
                                        <td align="right">
                                            <table>
                                                <tr>
                                                    <td><asp:LinkButton ID="achievedEventLink" runat="server" CssClass="button-sml" Text="Achieved" 
                                                        ToolTip="Achieved" OnClick="click_achievedEventLink" Visible="false" /><asp:ImageButton ID="reOpenEventLink" 
                                                        runat="server" ImageUrl="~/images/achieved_button.gif" ToolTip="Achieved" 
                                                        OnClick="click_achievedEventLink" Visible="false" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                
                                <div id="sendMessageDiv" runat="server">
                                <asp:Hyperlink id="sendMessageButton" runat="server" Visible="false"
                                    NavigateUrl="javascript:sendMessage();">Send Message</asp:Hyperlink>
		                        </div>
                		        
		                        <div class="pinstripe-divider">&nbsp;</div>

                		        <div class="eventCommentBox" id="eventCommentBox" runat="server">
                		        <table width="100%" border="0" cellspacing="2" cellpadding="2">
                		            <tr>
                		                <td colspan="2"><asp:Label ID="addCommentLabel" runat="server"></asp:Label></td>
                		            </tr>
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
                		                <td>
                		                    <div style="padding-top:5px; padding-bottom:5px; border:solid 1px #999999; background-color:white">
                		                    <table border="0" cellspacing="2" cellpadding="2">
                		                        <tr>
                		                            <td>Attach</td>
                		                            <td>&nbsp;&nbsp;</td>
                		                            <td><asp:HyperLink id="uploadEventCommentImageLink"
                		                                ImageUrl="~/images/addpicture.gif" runat="server" NavigateUrl="javascript:showhide('uploadImageDiv');" /></td>
                		                            <td>&nbsp;</td>
                		                            <td><asp:HyperLink id="uploadEventCommentVideoLinkLink"
                		                                ImageUrl="~/images/addvideo.gif" runat="server" NavigateUrl="javascript:showhide('uploadVideoLinkDiv');" /></td>
                		                            <td>&nbsp;</td>
                		                            <td><asp:HyperLink id="uploadEventLinkLink"
                		                                ImageUrl="~/images/addlink.gif" runat="server" NavigateUrl="javascript:showhide('uploadLinkDiv');" /></td>
                		                        </tr>
                		                    </table>
                		                    </div>
                		                    <div id="uploadImageDiv" style="display:none;padding-top:5px; padding-bottom:5px; margin-top: 5px; padding-left:4px; border:solid 1px #999999">
                		                        <p>Browse to add an image</p>
                                                <asp:FileUpload ID="eventPicFileUpload" runat="server" /><br />
                                                <asp:RegularExpressionValidator 
                                                id="eventPicFileUploadValidator1" runat="server" 
                                                ErrorMessage="Only .jpg .jpeg .gif and .png files are allowed" 
                                                ValidationExpression="(.*\.jpg)|(.*\.jpeg)|(.*\.gif)|(.*\.png)|(.*\.JPG)|(.*\.JPEG)|(.*\.GIF)|(.*\.PNG)" 
                                                ControlToValidate="eventPicFileUpload"></asp:RegularExpressionValidator>
                                                <p class="smallprint">I certify that I have the right to distribute these photos</p>
                		                    </div>
                		                    <div id="uploadVideoLinkDiv" style="display:none;padding-top:5px; padding-bottom:5px; margin-top: 5px; padding-left:4px; border:solid 1px #999999">
                		                        <p>Enter code to embed a video</p>
                                                <asp:TextBox ID="videoLinkText" runat="server" Width="400px" 
                                                    TextMode="MultiLine" Rows="8"></asp:TextBox>
                		                    </div>
                		                    <div id="uploadLinkDiv" style="display:none;padding-top:5px; padding-bottom:5px; margin-top: 5px; padding-left:4px; border:solid 1px #999999">
                		                        <p>Enter code to insert a link</p>
                                                <asp:TextBox ID="linkTextBox" runat="server" Width="400px">http://</asp:TextBox>
                		                    </div>
                		                </td>
                		                <td align="right">
                		                    <div style="padding-top:5px; padding-bottom:5px"><asp:LinkButton ID="postCommentButton" runat="server" 
                                            ToolTip="Add comment" Text="Comment" ValidationGroup="addCommentGroup" 
                                            OnClick="postCommentButton_click" CssClass="button-sml" /></div></td>
                		            </tr>
                		        </table>
                		        </div>
                		        
                                <div id="invitedPanel" runat="server" style="margin-top:10px">
                                <asp:LinkButton 
                                    ID="invitedButton" runat="server" ToolTip="join now" Text="You have been invited to join this goal - join now" 
                                    OnClick="invitedButton_click" CssClass="button-sml" />
                                </div>
                		        
                                <div id="loginRegisterPanel" runat="server" style="border:solid 1px #CCCCCC; padding-top:10px; margin-bottom:10px">
                                <p>You must be logged in to view the full details or to add comments to this event.<br />
                                <a href="login.aspx" class="modal">Click here to login</a><br />
                                or <a href="register.aspx">click here to register</a> if you are a new user</p>
                                </div>
                		        
                                <asp:PlaceHolder ID="commentsPlaceHolder" runat="server" />

                            </td>
                            <td width="16px">&nbsp;</td>
                            <td width="170px">

				                <div style="width: 170px; padding-top: 10px; overflow: hidden">
                                    <asp:Image ID="eventImage" runat="server" Width="170" />
                                    <asp:Hyperlink ID="uploadEventImage" runat="server" NavigateUrl="javascript:uploadEventImage()" 
                                    Text="Edit picture" CssClass="underline-bold" />
                                </div>
                                
                                <div id="eventLinksDiv" runat="server">
                                    <div class="eventRightColBox" ID="actionsBox" runat="server">
                                    <p><img src="images/share.gif" /> <asp:LinkButton ID="shareButton" runat="server" 
                                        Text="Share" CssClass="underline-bold" OnClick="click_shareButton" /></p>
                                    </div>

                                    <div class="eventRightColBox" ID="invitesBox" runat="server">
                                    <h3 ID="invitesHeader" runat="server" class="invites-header">Invites:</h3>
                                    <p><asp:LinkButton ID="inviteCountLabel" runat="server" OnClick="click_inviteUsersLink"></asp:LinkButton><br />
                                    <img src="images/invite.gif" /> <asp:LinkButton ID="invitesLink" runat="server" OnClick="click_inviteUsersLink" Text="Invite People" CssClass="underline-bold" /></p>
                                    </div>

                                    <h3 ID="trackingHeader" runat="server" class="requests-header">Members:</h3>
                                    <asp:PlaceHolder ID="trackingLinksPlaceholder" runat="server" />
                                    <asp:Image ID="messageTrackingImage" runat="server" ImageUrl="./images/messages.gif" /><asp:Hyperlink 
                                        ID="messageTrackingUsersLink" runat="server" Text="Message All" 
                                        NavigateUrl="javascript:messageTrackingUsers();" />
                                    <div class="pinstripe-divider">&nbsp;</div>

                                    <h3 ID="H1" runat="server" class="following-header"> Following:</h3>
                                    <asp:PlaceHolder ID="followersLinksPlaceholder" runat="server" />
                                    <asp:Image ID="followersTrackingImage" runat="server" ImageUrl="./images/messages.gif" /><asp:Hyperlink 
                                        ID="followersTrackingUsersLink" runat="server" Text="Message All" 
                                        NavigateUrl="javascript:messageFollowingUsers();" CssClass="underline-bold" />
                                    <div class="pinstripe-divider">&nbsp;</div>
                                    
                                    <!--<h3 ID="messagesHeader" runat="server">Messages</h3>-->
                                    <!--<p><asp:HyperLink ID="messagesLink" runat="server" NavigateUrl="~/message.aspx" /></p>-->
                                    <h3 ID="alertsHeader" runat="server" class="reminders-header">Reminders</h3>
                                    <asp:PlaceHolder ID="alertsPlaceHolder" runat="server" />
                                    <div style="margin-top:10px"><asp:Hyperlink ID="alertsLink" runat="server" NavigateUrl="javascript:eventAlerts();"
                                        Text="Edit reminders" CssClass="underline-bold" /></div>
                                    <div class="pinstripe-divider" ID="alertsSeperator" runat="server">&nbsp;</div>
                                    
                                    <h3 ID="requestsHeader" runat="server" class="requests-header">Requests:</h3>
                                    <asp:PlaceHolder ID="requestsLinksPlaceholder" runat="server" />
                                    <div class="pinstripe-divider">&nbsp;</div>

                                    <div>
                                        <asp:LinkButton ID="createSimilarEventLink" runat="server" Text="Copy Goal" OnClick="createSimilarEventLink_click" CssClass="button-sml" Visible="false" /><br />&nbsp;<br />&nbsp;
			                            <asp:LinkButton ID="trackThisEventLink" runat="server" Text="Follow this goal" OnClick="trackThisEventLink_click" CssClass="button-sml" /><br />&nbsp;<br />&nbsp;
			                            <asp:LinkButton ID="joinThisEventLink" runat="server" Text="Join" OnClick="joinThisEventLink_click" CssClass="button-sml" /><br />&nbsp;<br />&nbsp;
                                        <asp:LinkButton ID="deleteEventButton" runat="server" ToolTip="Delete Goal" Text="Delete goal" OnClick="deleteEventButton_click" CssClass="button-sml" /><br />&nbsp;<br />&nbsp;
                                    </div>
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
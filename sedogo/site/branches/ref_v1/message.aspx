<%@ Page Language="C#" AutoEventWireup="true" CodeFile="message.aspx.cs" Inherits="message" %>
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

	<title>Messages : Sedogo : Create your future and connect with others to make it happen</title>

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

<script language="JavaScript" type="text/javascript">
    function confirmDeleteMessage()
    {
        if (confirm('Are you sure you want to delete this message?'))
        {
            return true;
        }
        else
        {
            return false;
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

                    <div class="page-banner-content">
                        <div class="page-banner-header">Messages</div>
                        <div class="page-banner-backbutton"><asp:LinkButton id="backButton" runat="server" Text="Home" 
                            CssClass="page-banner-linkstyle" OnClick="backButton_click" CausesValidation="false" /></div>
                    </div>
            
                    <div class="page-banner-buttons">
                        <asp:LinkButton ID="viewArchivedMessagesButton" runat="server" CssClass="button-sml" 
                            Text="View read messages" OnClick="viewArchivedMessagesButton_click" />
                        <asp:LinkButton ID="hideArchivedMessagesButton" runat="server" CssClass="button-sml" 
                            Text="Hide read messages" OnClick="hideArchivedMessagesButton_click" />
                    </div>
                    <div class="clear-float"></div>
                    
                    <div id="noUnreadMessagesDiv" runat="server">
                    <p>You have no unread messages.</p>
                    </div>
                    
                    <div id="messagesDiv" runat="server">
                    <asp:Repeater ID="messagesRepeater" runat="server" OnItemDataBound="messagesRepeater_ItemDataBound"
                        OnItemCommand="messagesRepeater_ItemCommand">
                        <ItemTemplate>
                        
                            <table width="600" border="0" cellspacing="2" cellpadding="0">
                                <tr>
                                    <td width="360">
                                        <table>
                                            <tr>
                                                <td valign="top" width="60"><asp:Image id="eventPicThumbnailImage" runat="server" /></td>
                                                <td width="300">
                                                    <p><i><asp:Literal ID="userNameLabel" runat="server" /></i><br />
                                                    <asp:Literal ID="eventNameLabel" runat="server" /><br />
                                                    <asp:Literal ID="messageLabel" runat="server" /></p>
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="threadMessagesRepeater" runat="server" OnItemDataBound="threadMessagesRepeater_ItemDataBound"
                                                OnItemCommand="threadMessagesRepeater_ItemCommand">
                                                <ItemTemplate>
                                                <tr>
                                                    <td valign="top" width="60"><asp:Image id="threadPicThumbnailImage" runat="server" /></td>
                                                    <td width="300">
                                                        <p style="margin-left:20px"><i><asp:Literal ID="threadUserNameLabel" runat="server" /></i><br />
                                                        <asp:Literal ID="threadMessageLabel" runat="server" /></p>
                                                    </td>
                                                </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </td>
                                    <td align="right" width="240">
                                        <p style="text-align:right"><asp:LinkButton ID="markAsReadButton" runat="server" CssClass="button-sml" 
                                            Text="mark as read" CommandName="markAsReadButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' />
                                            <asp:Hyperlink ID="sendReplyMessageButton" runat="server" CssClass="button-sml modal" 
                                            Text="send reply" />
                                            <asp:LinkButton ID="deleteButton" runat="server" CssClass="button-sml" OnClientClick="return confirmDeleteMessage()" 
                                            Text="delete" CommandName="deleteButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' /></p>
                                    </td>
                                </tr>
                            </table>
                            
                            <div class="pinstripe-divider" style="margin: 5px 0 5px 0; width: 430px">&nbsp;</div>

                        </ItemTemplate>
                    </asp:Repeater>
                    </div>
                    
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addressBook.aspx.cs" Inherits="addressBook" %>

<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="favicon.ico">
    <meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
    <meta http-equiv="content-script-type" content="text/javascript" />
    <meta http-equiv="content-wstyle-type" content="text/css" />
    <meta http-equiv="expires" content="never" />
    <title>Profile : Sedogo : Create your future and connect with others to make it happen
    </title>
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <meta name="copyright" content="" />
    <meta name="robots" content="" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <meta http-equiv="imagetoolbar" content="no" />
    <meta http-equiv="Cleartype" content="Cleartype" />
    <link rel="stylesheet" href="css/main.css" />
    <!--[if lte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->
    <!--[if IE]>
		<link rel="stylesheet" href="css/main_ie.css" />
	<![endif]-->

    <script type="text/javascript" src="js/DD_roundies_0.0.2a-min.js"></script>
	<script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="js/jquery-ui-1.7.2.custom.min.js"></script>
	<script type="text/javascript" src="js/ui.dialog.js"></script>
	<script type="text/javascript" src="js/jquery.cookie.js"></script>
	<script type="text/javascript" src="js/jquery.livequery.js"></script>
	<script type="text/javascript" src="js/jquery.corner.js"></script>
	<script type="text/javascript" src="js/main.js"></script>
    <script type="text/javascript" src="js/validationFunctions.js"></script>

    <script type="text/javascript" src="js/examples_modified.js"></script>
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
                        <div class="page-banner-header">Friends</div>
                        <div class="page-banner-backbutton"><asp:LinkButton id="backButton" runat="server" Text="Home" 
                            CssClass="page-banner-linkstyle" OnClick="backButton_click" CausesValidation="false" /></div>
                    </div>
                
                    <div style="margin-top:10px">
                    <asp:Hyperlink ID="addContactButton" runat="server" CssClass="button-sml modal"
                        Text="Add friend" NavigateUrl="addAddressBook.aspx" />
                    </div>       
                
                    <div id="noContactsDiv" runat="server">
                    <p>You have no entries in your friends list.</p>
                    </div>
                    
                    <div id="contactsDiv" runat="server">
                    <asp:Repeater ID="addressBookRepeater" runat="server" 
                        OnItemDataBound="addressBookRepeater_ItemDataBound"
                        OnItemCommand="addressBookRepeater_ItemCommand">
                        <ItemTemplate>
                        
                            <table width="80%" border="0" cellspacing="2" cellpadding="0">
                                <tr>
                                    <td valign="top" width="60"><asp:Image id="picThumbnailImage" runat="server" /></td>
                                    <td>
                                        <asp:HyperLink ID="nameLabel" runat="server" CssClass="modal" /><br />
                                        <asp:HyperLink ID="emailLabel" runat="server" CssClass="modal" />
                                    </td>
                                    <td align="right">
                                        <p>
                                        <asp:Hyperlink ID="sendMessageButton"
                                        runat="server" CssClass="modal" Text="Send message" />
                                        <asp:Label ID="seperator1" runat="server" Text=" | " Visible="false" />
                                        <asp:Hyperlink ID="viewProfileButton" 
                                        runat="server" CssClass="" Text="View profile" />
                                        <asp:Label ID="seperator2" runat="server" Text=" | " Visible="false" />
                                        <asp:Hyperlink ID="editContactButton" 
                                        runat="server" CssClass="modal" Text="Edit" />
                                        <asp:Label ID="seperator3" runat="server" Text=" | " Visible="false" />
                                        <asp:Hyperlink ID="deleteContactButton" 
                                        runat="server" CssClass="" Text="Delete" />
                                        <asp:Label ID="seperator4" runat="server" Text=" | " Visible="false" />
                                        <asp:Hyperlink ID="addToAddressBookButton" 
                                        runat="server" CssClass="" Text="Add to address book" />
                                        </p>
                                    </td>
                                </tr>
                            </table>
                            
                            <div class="pinstripe-divider" style="margin: 5px 0 5px 0; width: 430px">&nbsp;</div>

                        </ItemTemplate>
                    </asp:Repeater>
                    </div>
                    
                    <br />&nbsp;
                    
                </div>

            </div>
		    <Sedogo:FooterControl ID="footerControl" runat="server" />
        </div>
        <div id="modal-container">
			<a href="#" class="close-modal"><img src="images/close-modal.gif" title="Close window" alt="Close window" /></a>
            <iframe frameborder="0"></iframe>
        </div>
        <div id="modal-background"></div>

        <iframe id="keepAliveIFrame" runat="server" height="0" width="0" style="border: 0">
        </iframe>
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />
</body>
</html>






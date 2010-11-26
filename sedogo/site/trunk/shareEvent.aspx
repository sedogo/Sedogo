<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shareEvent.aspx.cs" Inherits="shareEvent" ValidateRequest="false" %>
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

	<title>Share goal : Sedogo : Create your future and connect with others to make it happen</title>

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
    <script type="text/javascript" src="js/validationFunctions.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    
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

                    <div id="event-detail">
                        <div class="right-col">

                        </div>
                        <div class="left-col">
		                    <p style="font-style: italic; color: #ccc; margin: 0 0 4px 0; font-size: 11px">Edited <asp:Label ID="editedDateLabel" runat="server" /></p>
		                    <p><asp:Label ID="eventDescriptionLabel" runat="server" /></p>

				            <table class="summary">
					            <tbody>
						            <tr>
							            <th>Who:</th>
							            <td class="blue"><asp:Label ID="eventOwnersNameLabel" runat="server" /></td>
						            </tr>
						            <tr>
							            <th>Goal:</th>
							            <td><asp:Label ID="goalNameLabel" runat="server" /></td>
						            </tr>
						            <tr>
							            <th>Where:</th>
							            <td><asp:Label ID="goalVenueLabel" runat="server" /></td>
						            </tr>
						            <tr>
							            <th>Before:</th>
							            <td><asp:Label ID="eventDateLabel" runat="server" /></td>
						            </tr>
					            </tbody>
				            </table>

				            <div class="pinstripe-divider">&nbsp;</div>

                            <h3 class="blue" style="font-size: 12px; margin-bottom: 12px">Who do you want to share <asp:Label ID="eventNameLabel" runat="server" /> with?</h3>
                            <p>Type in friends or family email addresses or Sedogo account names.</p>

                            <table border="0" cellpadding="2" cellspacing="4">
                                <tr>
                                    <td><img src="images/profile/miniProfile.jpg" /></td>
                                    <td><asp:TextBox ID="inviteTextBox1" runat="server" Width="210px" /></td>
                                    <td><asp:HyperLink ID="addressBookLink1" runat="server" NavigateUrl="addressBookSelect.aspx?F=inviteTextBox1" CssClass="modal" ImageUrl="images/friends.gif" /></td>
                                </tr>
                                <tr>
                                    <td><img src="images/profile/miniProfile.jpg" /></td>
                                    <td><asp:TextBox ID="inviteTextBox2" runat="server" Width="210px" /></td>
                                    <td><asp:HyperLink ID="addressBookLink2" runat="server" NavigateUrl="addressBookSelect.aspx?F=inviteTextBox2" CssClass="modal" ImageUrl="images/friends.gif" /></td>
                                </tr>
                                <tr>
                                    <td><img src="images/profile/miniProfile.jpg" /></td>
                                    <td><asp:TextBox ID="inviteTextBox3" runat="server" Width="210px" /></td>
                                    <td><asp:HyperLink ID="addressBookLink3" runat="server" NavigateUrl="addressBookSelect.aspx?F=inviteTextBox3" CssClass="modal" ImageUrl="images/friends.gif" /></td>
                                </tr>
                                <tr>
                                    <td><img src="images/profile/miniProfile.jpg" /></td>
                                    <td><asp:TextBox ID="inviteTextBox4" runat="server" Width="210px" /></td>
                                    <td><asp:HyperLink ID="addressBookLink4" runat="server" NavigateUrl="addressBookSelect.aspx?F=inviteTextBox4" CssClass="modal" ImageUrl="images/friends.gif" /></td>
                                </tr>
                                <tr>
                                    <td><img src="images/profile/miniProfile.jpg" /></td>
                                    <td><asp:TextBox ID="inviteTextBox5" runat="server" Width="210px" /></td>
                                    <td><asp:HyperLink ID="addressBookLink5" runat="server" NavigateUrl="addressBookSelect.aspx?F=inviteTextBox5" CssClass="modal" ImageUrl="images/friends.gif" /></td>
                                </tr>
                            </table>
                            
				            <div class="pinstripe-divider" style="margin: 20px 0 14px 0">&nbsp;</div>
                            <p style="margin-bottom: 8px">Additional message <em>(optional)</em></p>

                            <asp:TextBox ID="additionalInviteTextTextBox" runat="server" TextMode="MultiLine"
                                Width="233px" Rows="3" />
                            <p style="font-size: 11px"><br />Please note that anyone without a sedogo account <br />will be invited to join to connect with your goal</p>
		                </div>
		            </div>

                    <div>
                    <p>
	                    <asp:LinkButton ID="sendInvitesLink" runat="server" Text="Share" OnClick="sendInvitesLink_click" CssClass="button-lrg" />
                    </p>
                    </div>
                    
                    <br />
                    &nbsp;

		        </div>
		    </div>
		    <Sedogo:FooterControl ID="footerControl" runat="server" />
	    </div>
        <div id="modal-container">
			<a href="#" class="close-modal"><img src="images/close-modal.gif" title="Close window" alt="Close window" /></a>
            <iframe frameborder="0"></iframe>
        </div>
        <div id="modal-background"></div>
    
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl1" runat="server" />

</body>
</html>
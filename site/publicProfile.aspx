<%@ Page Language="C#" AutoEventWireup="true" CodeFile="publicProfile.aspx.cs" Inherits="d_publicProfile" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="../favicon.ico" >  
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="expires" content="never" />

	<title><asp:Literal ID="pageTitleUserName" runat="server" /></title>

	<meta name="keywords" content="" />
	<meta name="description" content="" />
	<meta name="author" content="" />
	<meta name="copyright" content="" />
	<meta name="robots" content="" />
	<meta name="MSSmartTagsPreventParsing" content="true" />

	<meta http-equiv="imagetoolbar" content="no" />
	<meta http-equiv="Cleartype" content="Cleartype" />

	<link rel="stylesheet" href="../css/main.css" />
	<!--[if gte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->
	<!--[if IE]>
		<link rel="stylesheet" href="css/main_ie.css" />
	<![endif]-->

	<script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="../js/jquery-ui-1.7.2.custom.min.js"></script>
	<script type="text/javascript" src="../js/ui.dialog.js"></script>
	<script type="text/javascript" src="../js/jquery.cookie.js"></script>
	<script type="text/javascript" src="../js/jquery.livequery.js"></script>
	<script type="text/javascript" src="../js/jquery.corner.js"></script>
	<script type="text/javascript" src="../js/main.js"></script>
    <script type="text/javascript" src="../utils/validationFunctions.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>    
    
        <div id="container">
	        <Sedogo:BannerLoginControl ID="BannerLoginControl1" runat="server" />
	        <Sedogo:BannerAddFindControl ID="bannerAddFindControl" runat="server" />

			<div id="other-content">
				<div class="three-col">

                    <h1 style="margin-bottom: 2px">User profile</h1>

                    <div>
                        <p style="display: block"><asp:Image ID="profileImage" runat="server" CssClass="profile" /></p>
                    </div>

                    <fieldset>
                        <ol class="width-constrain">
                             <div class="pinstripe-divider">&nbsp;</div>
                            <li>
                                <label for="">Name: </label>
                                <asp:Label runat="server" ID="nameLabel" />
                            </li>
                            <div class="pinstripe-divider">&nbsp;</div>
                            <li>
                                <label for="">Gender: </label>
                                <asp:Label runat="server" ID="genderLabel" />
                            </li>
                            <div class="pinstripe-divider">&nbsp;</div>
                            <li>
                                <label for="">Home town: </label>
                                <asp:Label runat="server" ID="homeTownLabel" />
                            </li>
                            <div class="pinstripe-divider">&nbsp;</div>
                            <li>
                                <label for="">Profile: </label>
                                <asp:Label runat="server" ID="profileTextLabel" />
                            </li>
                        </ol>
                    </fieldset>

				</div>
			</div>
			
		    <Sedogo:FooterControl ID="footerControl" runat="server" />
		</div>
        <div id="modal-container">
			<a href="#" class="close-modal"><img src="../images/close-modal.gif" title="Close window" alt="Close window" /></a>
            <iframe frameborder="0"></iframe>
        </div>
        <div id="modal-background"></div>
            
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />
    </form>
</body>
</html>

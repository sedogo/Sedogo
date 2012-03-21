<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgotPassword.aspx.cs" Inherits="forgotPassword" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
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

	<title>Forgot password : Sedogo : Create your future and connect with others to make it happen</title>

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
	<script type="text/javascript" src="js/jquery.livequery.js"></script>
	<script type="text/javascript" src="js/jquery.corner.js"></script>
	<script type="text/javascript" src="js/main.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>    
    
	<div id="container">
	    <Sedogo:BannerLoginControl ID="bannerLogin" runat="server" />
	    <Sedogo:BannerAddFindControl ID="bannerAddFindControl" runat="server" />

		<div id="other-content">
            
			<div class="three-col">

                <h2>Forgot your password</h2>
                <br />&nbsp;
                <p>Enter your email address below and we will re-send your activation message</p>
                <p>&nbsp;</p>
                <p><asp:TextBox runat="server"
                    ID="emailAddressTextBox" Width="200px" MaxLength="200" />
                    <asp:RegularExpressionValidator id="emailAddressTextBoxValidator2" 
                    runat="server" ControlToValidate="emailAddressTextBox" 
                    ErrorMessage="The email address is not valid" 
                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></p>
                <p>&nbsp;</p>
                            
                <p><asp:LinkButton 
                    ID="forgotPasswordButton" runat="server" ToolTip="Reset password" Text="Reset password" 
                    OnClick="forgotPasswordButton_click" CssClass="button-lrg" /></p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                                    
                <a href="default.aspx">Return to home page</a>

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

</body>
</html>

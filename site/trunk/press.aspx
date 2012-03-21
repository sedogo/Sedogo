<%@ Page Language="C#" AutoEventWireup="true" CodeFile="press.aspx.cs" Inherits="press" %>
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

	<title>Press : Sedogo : Create your future and connect with others to make it happen</title>

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

<object width="640" height="385"><param name="movie" value="http://www.youtube.com/v/Jxq9I-5QzPk?fs=1&amp;hl=en_GB&amp;rel=0"></param><param name="allowFullScreen" value="true"></param><param name="allowscriptaccess" value="always"></param><embed src="http://www.youtube.com/v/Jxq9I-5QzPk?fs=1&amp;hl=en_GB&amp;rel=0" type="application/x-shockwave-flash" allowscriptaccess="always" allowfullscreen="true" width="640" height="385"></embed></object>
<p>&nbsp;</p>

                <h2>For press enquiries please contact:</h2>
                <h2><b>Jacqueline Culleton</b> | 
                Email: <a href="mailto:sedogo@forster.co.uk">sedogo@forster.co.uk</a> |
                Telephone: 020 7403 2230</h2>
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

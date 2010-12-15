<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eventPicDetails.aspx.cs" Inherits="eventPicDetails" %>

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
    <meta http-equiv="content-style-type" content="text/css" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <title>
        <asp:Literal ID="pageTitleUserName" runat="server" /></title>
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

    <script type="text/javascript">
    function addPicture()
    {
	    <asp:Literal id="addPictureLiteral" runat="server" />
	    openModal(url);
    }
    </script>
    
<style type="text/css">

/* All Styles Optional */

* {
font-family:arial;
font-size:10pt;
}
div#show3 {
width:500px;
text-align:left;
}
div#show3 table td, div#show4 table td {
height:24px;
}
div#show3 table input,  div#show4 table input {
outline-style:none;
}
.imageCaption
{
	position:relative;
	top:-540px;
	left:0px;
}
</style>
<!--[if IE]>
<style type="text/css">
div#show3 table td, div#show4 table td {
height:21px;
}
</style>
<![endif]-->

<script type="text/javascript">
//If using image buttons as controls, Set image buttons' image preload here true
//(use false for no preloading and for when using no image buttons as controls):
var preload_ctrl_images=true;

//And configure the image buttons' images here:
var previmg='left.gif';
var stopimg='stop.gif';
var playimg='play.gif';
var nextimg='right.gif';

var slidesX=[]; //SECOND SLIDESHOW
//configure the below images and descriptions to your own. 
<asp:Literal id="sliderImagesLiteral" runat="server" />
//optional properties for these images:
slidesX.desc_prefix=''; //string prefix for image descriptions display
slidesX.controls_top=1; //use for top controls
slidesX.counter=0; //use to show image count
slidesX.width=500; //use to set width of widest image if dimensions vary
slidesX.height=500; //use to set height of tallest image if dimensions vary
slidesX.no_auto=1; //use to make show completely user operated (no play button, starts in stopped mode)
slidesX.use_alt=1; //use for descriptions as images alt attributes
slidesX.use_title=1; //use for descriptions as images title attributes
slidesX.nofade=1; //use for no fade-in, fade-out effect for this show
slidesX.border=0; //set border width for images
slidesX.border_color='#FFFFFF'; //set border color for images
slidesX.no_controls=1;
</script>
<script src="js/swissarmy.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        
        <div id="container">
            <Sedogo:BannerLoginControl ID="bannerLogin" runat="server" />
            <Sedogo:BannerAddFindControl ID="bannerAddFindControl" runat="server" />
            <div id="other-content">
                <Sedogo:SidebarControl ID="sidebarControl" runat="server" />
                <div class="three-col">
                    <div class="page-banner-content" id="pageBannerBarDiv" runat="server">
                        <div class="page-banner-header">
                            <asp:Literal ID="eventTitleLabel" runat="server" /></div>
                        <div class="page-banner-backbutton">
                            <asp:LinkButton ID="backButton" runat="server" Text="Back" CssClass="page-banner-linkstyle"
                                OnClick="backButton_click" CausesValidation="false" /></div>
                    </div>
                    <table width="100%" border="0" cellspacing="0" cellpadding="2">
                        <tr>
                            <td width="100%">
                                <div style="float:right;margin:5px 5px"><asp:Image ID="privateIcon" 
                                    runat="server" ImageUrl="~/images/private.gif" Visible="false" /></div>
                                <table width="100%" class="summary">
                                    <tr>
                                        <td>
                                        
                                            <div id="nextBackButtons">
                                            <a href="#" onclick="iss[0].changeimg(false, 'nav');">
                                            Previous</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                                            <a href="#" onclick="iss[0].changeimg(true, 'nav');">
                                            Next</a>
                                            </div>
                                            <p>&nbsp;</p>
        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        
                                            <div id="show3"><script type="text/javascript">
                                            new inter_slide(slidesX)
                                            </script>
                                            </div>
                                        
                                        </td>
                                    </tr>    
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <Sedogo:FooterControl ID="footerControl" runat="server" />
        </div>
        <div id="modal-container">
            <a href="#" class="close-modal">
                <img src="images/close-modal.gif" title="Close window" alt="Close window" /></a>
            <iframe frameborder="0"></iframe>
        </div>
        <div id="modal-background">
        </div>
    </div>
    </form>
    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getInspired.aspx.cs" Inherits="getInspired" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="favicon.ico" >  
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="expires" content="never" />

	<title>Home : Sedogo : Create your future timeline.  Connect, track and interact with like minded people.</title>

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

	<script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="js/jquery-ui-1.7.2.custom.min.js"></script>
	<script type="text/javascript" src="js/ui.dialog.js"></script>
	<script type="text/javascript" src="js/jquery.cookie.js"></script>
	<script type="text/javascript" src="js/jquery.livequery.js"></script>
	<script type="text/javascript" src="js/jquery.corner.js"></script>
	<script type="text/javascript" src="js/main.js"></script>
    <!--<script type="text/javascript" src="utils/validationFunctions.js"></script>-->

	<script type="text/javascript">
    function doAddEvent()
    {
        openModal("login.aspx");
    }
    function checkAddButtonEnter(e)
    {
        var characterCode;
        if (e && e.which) // NN4 specific code
        {
            e = e;
            characterCode = e.which;
        }
        else
        {
            e = event;
            characterCode = e.keyCode; // IE specific code
        }
        if (characterCode == 13) //// Enter key is 13
        {
            e.returnValue = false;
            e.cancelBubble = true;
            doAddEvent();
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
    
        <div id="container">
	        <Sedogo:BannerLoginControl ID="BannerLoginControl1" runat="server" />
	        <Sedogo:BannerAddFindControl ID="bannerAddFindControl" runat="server" />
    
			<div id="other-content">
				<div class="one-col">
					&nbsp;
				</div>
				<div class="one-col">
					<h2 class="col-header" style="margin-top: 12px; width: 160px">get inspired</h2>
					<p class="teaser" style="font-size: 22px; line-height: 30px">What have you often vowed to do before you die? Travel the world? 
                        Learn a language?<br />
                        Write that novel?<br />
                        Ghandi once said the future 
                        depends on what we do in the present.</p>
                    <br />
                    <br />
					<p><a href="register.aspx" title="get started" class="button">get started</a></p>
				</div>
				<div class="one-col">
					<h2 class="col-header" style="margin-top: 12px; border-color: #fff">&nbsp;</h2>
					<p class="teaser" style="font-size: 13px; width: 220px">But it's not always easy, is it? Life often gets in the way. Well, 
                        that's exactly why we launched Sedogo.  It's more than a diary of 
                        planned goals, Sedogo is a way of connecting with people who can be 
                        part of your future hopes and dreams, however big or small. Whether 
                        it's bringing family and friends together to celebrate your next 
                        birthday, or meeting strangers who, a year from now, you'll be 
                        trusting with your life on the slopes of K2, sedogo really is the of 
                        the future of social networking. Your future.</p>
				</div>
				<div class="one-col-end">
					<h2 class="col-header" style="margin-top: 12px; width: 160px">goal suggestions</h2>
					<p class="teaser" style="font-size: 13px"><strong>Need some help getting started? Here are a few ideas that might grab 
                        you. Add as many as take your fancy. After all, you only get one life.</strong><br />
                        &nbsp;<br />
                        <span style="color: #0cf">
	                        &gt; Fly the double decker airbus A380<br />
                            &gt; Own an exotic pet<br />
                            &gt; Try zorbing<br />
                            &gt; Go on a Murder Mystery Weekend<br />
                            &gt; Form the ultimate Pub Quiz team<br />
                            &gt; Learn to ballroom waltz, Jane Austen style<br />
                            &gt; Go skinning dipping in a natural spa<br />
                            &gt; Experiment with a faddy diet<br />
                            &gt; Rent a remote cabin<br />
                            &gt; Sleep under the stars<br />
                            &gt; Watch a play in the West End every month
                        </span>
                    </p>
				</div>
			</div>
		    <Sedogo:FooterControl ID="footerControl" runat="server" />
		</div>
            
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

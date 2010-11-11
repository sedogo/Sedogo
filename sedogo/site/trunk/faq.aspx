<%@ Page Language="C#" AutoEventWireup="true" CodeFile="faq.aspx.cs" Inherits="faq" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

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

	<title>FAQ : Sedogo : Create your future and connect with others to make it happen</title>

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
                <Sedogo:SidebarControl ID="sidebarControl" runat="server" />
			    <div class="three-col">

                    <h1>FAQ</h1>
                    <h2>What goals can I create?</h2>
                    <p>You can create any goal you like. Sedogo automatically matches you to people with similar goals to you. To make planning your goals easier, we've added categories for you to choose from.</p>
                    <h2>Can I connect with more than one person to achieve a goal?</h2>
                    <p>Absolutely. You can invite as many or few people as you like to be part of any one goal. The shared goal will appear as part of everyone's timeline.</p>
                    <h2>Can I follow someone's else's goal?</h2>
                    <p>Yes. Simply search for them or the goal they've created and click 'follow goal'.</p>
                    <h2>How much does it cost to subscribe to Sedogo? </h2>
                    <p>Nothing! Belonging to Sedogo is free.</p>
                    <h2>Can everyone see the goals I create?</h2>
                    <p>Only if you want them to. You can choose which goals you want others to see and which you don't.</p>
                    <h2>How do I create a goal that only I can see?</h2>
                    <p>Add your goal. On the 'Create goal' page, you'll see a 'Nature of goal' option. Select 'private'. You can also change the privacy setting on existing goals. Just select the goal you want to change and choose 'edit'.</p>
                    <h2>How do I create a goal that only the people I invite can see?</h2>
                    <p>Make sure you select 'private' when you create your goal (see above). After that, only those you invite to join the goal will be able to see it. You can change the privacy settings of existing goals by selecting the goal and choosing 'edit'.</p>
                    <h2>Can I add a picture to go with my goal?</h2>
                    <p>Yes. Just select your goal (or create a new one) and click 'add picture'. You can also create your very own photo album by clicking 'more pictures'.</p>
                    <h2>How do I add more photos or links?</h2>
                    <p>Simply use the 'comment' box on your goal page to add photos, video or links — click on the little blue icons and you're away! Any friends who comment on your goal are free to add whatever they like too!</p>
                    <h2>Can I add video?</h2>
                    <p>Yes, all you need is a YouTube account. Just upload your video there and you can embed it into your 'comment' box on your goal page by clicking the blue video icon. Friends can add video this way as well.</p>
                    <h2>How do I change my password?</h2>
                    <p>Just log in with the one we sent you, then click 'Edit' next to your profile (top left). Then, underneath your profile picture, click 'change password'. It’s as easy as that.</p>
                    <p>&nbsp;<br /><a href="default.aspx?Tour=Y">Take the tour</a></p>
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
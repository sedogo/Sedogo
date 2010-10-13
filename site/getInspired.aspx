<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getInspired.aspx.cs" Inherits="getInspired" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

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
    <script type="text/javascript" src="utils/validationFunctions.js"></script>

    <style type="text/css">
        .itemTemplate
        {
            border: 0px;    /*solid 1px #dff3ff;*/
            height: 216px;
            width: 232px;
            margin: 0px;   /*5px;*/
        }
        .rotatorBackground
        {
            float: left;
            margin-left: 0px;   /*50px;*/
            margin-top: 0px;    /*15px;*/
            width: 696px;
            height: 216px;
            border: 0;  /*solid 2px #dedede;*/
            font-family: Arial;
            -moz-border-radius: 0px;    /*15px;*/
            -webkit-border-radius: 0px;    /*15px;*/
            _margin-left: 0px;    /*25px;*/ /* IE6 hack*/
            _margin-top: 0px;    /*7px;*/ /* IE6 hack*/
        }
        .horizontalRotator
        {
            margin-top: 0px;    /*110px;*/
            margin-left: auto;
            margin-right: auto;
            width: 696px;
            height: 216px;
        }
    </style>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>    
    
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
					<p><asp:HyperLink ID="getStartedLink" runat="server" ToolTip="get started" 
					    Text="get started" CssClass="button" /></p>
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
	                        &gt; <a href="viewEvent.aspx?EID=395">Flight of fancy</a><br />
                            &gt; <a href="viewEvent.aspx?EID=658">Own an exotic pet</a><br />
                            &gt; <a href="viewEvent.aspx?EID=607">Watch the whole movie</a><br />
                            &gt; <a href="viewEvent.aspx?EID=15">Get high</a><br />
                            &gt; <a href="viewEvent.aspx?EID=638">Get behind the wheel</a><br />
                            &gt; <a href="viewEvent.aspx?EID=650">Cycle the round trip</a><br />
                            &gt; <a href="viewEvent.aspx?EID=34">Snowboard Mont Blanc</a><br />
                            &gt; <a href="viewEvent.aspx?EID=557">Do the Duke of Edinburgh</a><br />
                            &gt; <a href="viewEvent.aspx?EID=657">Do some DIY</a><br />
                            &gt; <a href="viewEvent.aspx?EID=578">Sweet music</a><br />
                            &gt; <a href="viewEvent.aspx?EID=741">Get into the swing</a><br />
                            &gt; <a href="viewEvent.aspx?EID=523">Cycle to work</a>
                        </span>
                        
                    </p>
				</div>
				
				<div class="three-col" style="float:left">
                    <div class="rotatorBackground">
                        <telerik:RadRotator ID="eventRotator" runat="server" Width="720px" Height="216px"
                            CssClass="horizontalRotator" ScrollDuration="500" 
                            FrameDuration="5000" ItemHeight="216" ItemWidth="240">
                            <ItemTemplate>
                                <div class="itemTemplate">
                                    <a href="getInspired.aspx"><img src='<%# Page.ResolveUrl("~/images/") + Container.DataItem %>.png'
                                    alt="Customer Image" /></a>
                                </div>
                            </ItemTemplate>
                        </telerik:RadRotator>
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

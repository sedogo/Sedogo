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
    <script type="text/javascript" src="js/validationFunctions.js"></script>

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
					<p class="teaser" style="font-size: 13px">Life's short. Why put off all the things you've always wanted to try?
                        Whether you fancy <a target="_blank" class="blue sedogorollover" href="http://www.worldexpoditions.co.uk">going somewhere you've never been before</a>,
                        <a target="_blank" class="blue sedogorollover" href="http://www.adventurecompay.co.uk">embarking on a new adventure</a>,
                        <a target="_blank" class="blue sedogorollover" href="http://www.hotcourses.com">learning a new language</a> 
                        or <a target="_blank" class="blue sedogorollover" href="http://www.justgiving.com">doing something worthwhile for charity</a>, it's never too late to get started.
                        </p>
                    <br />
                    <br />
					<p><asp:HyperLink ID="getStartedLink" runat="server" ToolTip="Sign up" 
					    CssClass="signuprollover"><span 
                        class="displace">Signup</span></asp:HyperLink></p>
				</div>
				<div class="one-col">
					<h2 class="col-header" style="margin-top: 12px; border-color: #fff">&nbsp;</h2>
					<p class="teaser" style="font-size: 13px; width: 220px">
					Why not have a look at these links too? Once you've found a goal, simply <asp:Hyperlink ID="registerSignUpLink" runat="server" Text="register it here" CssClass="blue" /> and you're away.<br />&nbsp;<br />
					
<a target="_blank" class="blue sedogorollover" href="http://www.actionforchildren.org.uk/events.aspx">Action for Children</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.ndcschallenges.org.uk/index.html">NDCS Challenges</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.ageuk.org.uk/get-involved/volunteer">Age UK</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.do-it.org.uk/wanttovolunteer">Do-it.org</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.volunteering.org.uk/IWantToVolunteer">Volunteering England</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.vso.org.uk/volunteer">VSO</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.barnardos.org.uk/get_involved/volunteering.htm">Banardos</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.justgiving.com">Justgiving</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.charitychallenge.com/index.html">Charity Challenge</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.adventurecompany.co.uk">Adventure Company</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.worldexpeditions.co.uk">World Expeditions</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.lastminute.com">Lastminute</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.open.ac.uk">The Open University</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.hotcourses.com">Hotcourses</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.redletterdays.co.uk">Redletterdays</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.musicskills.co.uk/play">Music Skills</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.bda.org.uk/Fun_Resources-i-661.html">BADA UK</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.sewingsupport.com/sewing-how-to/learn-to-sew.html">Sewing Support</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.sports-clubs.net">Sports Clubs</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.trailfinders.com">Trail Finders</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.british-sign.co.uk">British-Sign</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.learntodance.com">Learntodance</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.centrestageuk.com/da/104792">Centrestage</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.circusspace.co.uk">Circus Space</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.explore.co.uk">Explore</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.virginballoonflights.co.uk/main.asp">Virgin Balloon Flights</a>
| <a target="_blank" class="blue sedogorollover" href="http://learntodive.co.uk">Learn to Dive</a>
| <a target="_blank" class="blue sedogorollover" href="http://www.diabeteschallenge.org.uk">Diabetes UK</a>
					</p>
                        
				</div>
				<div class="one-col-end">
					<h2 class="col-header" style="margin-top: 12px; width: 160px">goal suggestions</h2>
					<p class="teaser" style="font-size: 13px">
                        <span style="color: #0cf">
	                        <a class="sedogorollover" href="viewEvent.aspx?EID=395">Flight of fancy</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=607">Watch the whole movie</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=15">Get high</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=638">Get behind the wheel</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=650">Cycle the round trip</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=34">Snowboard Mont Blanc</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=557">Do the Duke of Edinburgh</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=509">Do some DIY</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=578">Sweet music</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=741">Get into the swing</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=523">Cycle to work</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=901">See the Inca Ruins</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=658">Own an exotic pet</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=897">Learn to sign</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=845">Find an old friend</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=232">Climb a Munroe</a><br />
                            <a class="sedogorollover" href="viewEvent.aspx?EID=853">Add another string to your bow</a>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userTimeline.aspx.cs" Inherits="userTimeline" %>

<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="EventsListControl" Src="~/components/eventsListControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="IE=7" http-equiv="X-UA-Compatible">
    <link rel="shortcut icon" href="favicon.ico">
    <meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
    <meta http-equiv="content-script-type" content="text/javascript" />
    <meta http-equiv="content-style-type" content="text/css" />
    <meta http-equiv="expires" content="never" />
    <title>Home : Sedogo : Create your future and connect with others to make it happen
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
    <link rel="stylesheet" href="css/style.css" />
    <!--[if lte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->
    <!--[if IE]>
		<link rel="stylesheet" href="css/main_ie.css" />
	<![endif]-->
    <link href="css/flexcrollstyles.css" rel="stylesheet" />
    <link href="css/tutorsty.css" rel="stylesheet" />

    <script type="text/javascript" src="js/DD_roundies_0.0.2a-min.js"></script>
    
    <script type="text/javascript" src="js/dom-drag.js"></script>

    <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>

    <script type="text/javascript" src="js/jquery-ui-1.7.2.custom.min.js"></script>

    <script type="text/javascript" src="js/ui.dialog.js"></script>

    <script type="text/javascript" src="js/jquery.cookie.js"></script>

    <script type="text/javascript" src="js/jquery.livequery.js"></script>

    <script type="text/javascript" src="js/jquery.corner.js"></script>

    <script type="text/javascript" src="js/main.js"></script>

    <script type="text/javascript" src="utils/validationFunctions.js"></script>

    <script type="text/javascript">
		Timeline_ajax_url = "js/timeline/timeline_ajax/simile-ajax-api.js";
		Timeline_urlPrefix = 'js/timeline/timeline_js/';
		Timeline_parameters = 'bundle=true';
    </script>

    <script src="js/timeline/timeline_js/timeline-api.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/examples_modified.js"></script>

    <script type="text/javascript">
			var tl;
			var theme1;

			var today = new Date();
			var tomorrow = new Date();
			var sixMonthsAgo = new Date();
			tomorrow.setDate(today.getDate()+1);
			sixMonthsAgo.setDate(today.getDate()-182)

			function onLoad() {
				//Initiate timeline
				initiateTimeline();
			
				//Set up filter controls
				setupFilterHighlightControls(document.getElementById("controls"), tl, [1, 2, 3, 4, 5, 6], theme1);

				//Filter based on current cookie values after timelines load
				performFiltering(tl, [1, 2, 3, 4, 5, 6], document.getElementById("table-filter"));

				//Bug fix: scroll timeline programatically to trigger correct auto-height
				tl.getBand(0)._autoScroll(1);
				
			}

			function initiateTimeline() {
				var eventSource = new Timeline.DefaultEventSource();
				var eventSourceSearch = new Timeline.DefaultEventSource();

				theme1 = Timeline.ClassicTheme.create();
				theme1.autoWidth = true; // Set the Timeline's "width" automatically. Setting autoWidth on the timelines first bands theme, will affect all bands.

				var bandInfos = [
					Timeline.createBandInfo({
						width: "0",
						theme: theme1
					}),
					Timeline.createBandInfo({
						date: "<asp:Literal id="timelineStartDate1" runat="server" />",
						width: "235",
						intervalUnit: Timeline.DateTime.DECADE,
						intervalPixels: 98,
						theme: theme1,
						eventSource: eventSource,
						zoomIndex: 14,
						zoomSteps: new Array(
						    { pixelsPerInterval: 280, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 140, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 70, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 35, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 400, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 200, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 100, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 50, unit: Timeline.DateTime.DAY },							
							{ pixelsPerInterval: 40, unit: Timeline.DateTime.DAY },//New	
							{ pixelsPerInterval: 400, unit: Timeline.DateTime.MONTH },
							{ pixelsPerInterval: 200, unit: Timeline.DateTime.MONTH },
							{ pixelsPerInterval: 100, unit: Timeline.DateTime.MONTH },// DEFAULT zoomIndex old						
							{ pixelsPerInterval: 50, unit: Timeline.DateTime.MONTH },//New
							{ pixelsPerInterval: 100, unit: Timeline.DateTime.YEAR },//New
							{ pixelsPerInterval: 98, unit: Timeline.DateTime.DECADE} //New DEFAULT zoomIndex
						)
					}),
					
					Timeline.createBandInfo({
						date: "<asp:Literal id="timelineStartDate2" runat="server" />",
						width: "70",
						intervalUnit: Timeline.DateTime.DECADE,
						intervalPixels: 500,
						showEventText: false,
						trackHeight: 0.5,
						trackGap: 0.2,
						theme: theme1,
						eventSource: eventSource,
						overview: true
					}),
					Timeline.createBandInfo({
						date: today,
						width: "10",
						intervalUnit: Timeline.DateTime.YEAR,
						intervalPixels: 980,
						showEventText: false,
						trackHeight: 0,
						trackGap: 0,
						theme: theme1,
						eventSource: eventSource,
						overview: true
					}),
					Timeline.createBandInfo({
						date: today,
						width: "38",
						intervalUnit: Timeline.DateTime.YEAR,
						intervalPixels: 980,
						showEventText: false,
						trackHeight: 0,
						trackGap: 0,
						theme: theme1,
						eventSource: eventSource,
						overview: true
					}),
					Timeline.createBandInfo({
						date: "<asp:Literal id="timelineStartDate3" runat="server" />",
						width: "235",
						intervalUnit: Timeline.DateTime.MONTH,
						intervalPixels: 50,
						theme: theme1,
						eventSource: eventSourceSearch
					}),
					Timeline.createBandInfo({
						date: "<asp:Literal id="timelineStartDate4" runat="server" />",
						width: "70",
						intervalUnit: Timeline.DateTime.YEAR,
						intervalPixels: 100,
						showEventText: false,
						trackHeight: 0.5,
						trackGap: 0.2,
						theme: theme1,
						eventSource: eventSourceSearch,
						overview: true
					})
				];

				//Synchronise band scrolling
				bandInfos[2].syncWith = 1;
				bandInfos[5].syncWith = 1;
				bandInfos[6].syncWith = 1;
				bandInfos[2].highlight = true;
				bandInfos[6].highlight = true;

				//Generate marker for today
				for (var i = 0; i < bandInfos.length; i++) {
					bandInfos[i].decorators = [
						new Timeline.SpanHighlightDecorator({
							startDate: today,
							endDate: tomorrow,
                    		color: "#CCCCCC",
                    		opacity: 50,
                    		startLabel: "",
                    		// theme:      theme,
                    		cssClass: 't-highlight1'
						})
					];
				}
/*
				bandInfos[0].decorators = [
					new Timeline.SpanHighlightDecorator({
						startDate: sixMonthsAgo,
						endDate: sixMonthsAgo,
                   		color: "#FF0000",
                   		opacity: 50,
                   		endLabel: "My timeline",
                   		// theme:      theme,
                   		cssClass: 't-highlight2'
					})
				];
				bandInfos[3].decorators = [
					new Timeline.SpanHighlightDecorator({
						startDate: sixMonthsAgo,
						endDate: sixMonthsAgo,
                   		opacity: 50,
                   		endLabel: "You have searched for...",
                   		// theme:      theme,
                   		cssClass: 't-highlight3'
					})
				];
*/
				bandInfos[4].decorators = [
					new Timeline.SpanHighlightDecorator({
						startDate: sixMonthsAgo,
						endDate: sixMonthsAgo,
                   		opacity: 50,
                   		endLabel: "<asp:Literal id="timelineUserNameLiteral" runat="server" />'s timeline",
                   		// theme:      theme,
                   		cssClass: 't-highlight2'
					})
				];


				//Create timeline and load data
				tl = Timeline.create(document.getElementById("my-timeline"), bandInfos);
				var url = "<asp:Literal id="timelineURL" runat="server" />";
				Timeline.loadXML(url, function(xml, url) { eventSource.loadXML(xml, url); });
				var urlSearch = "<asp:Literal id="searchTimelineURL" runat="server" />";
				Timeline.loadXML(urlSearch, function(xml2, urlSearch) { eventSourceSearch.loadXML(xml2, urlSearch); });
			}

			var resizeTimerID = null;
			function onResize() {
				if (resizeTimerID == null) {
					resizeTimerID = window.setTimeout(function() {
						resizeTimerID = null;
						tl.layout();
					}, 500);
				}
			}
    </script>

    <script type="text/javascript">
	$(document).ready(function() {
	});
	function breakout_of_frame() {
		if (top.location != location) {
			top.location.href = document.location.href;
		}
	}
	function getElementID(name) {
		var form = document.forms[0];
		var nID = -1;
		for (i = 0; i < form.elements.length; i++) {
			if (form.elements[i].name == name) {
				nID = i;
			}
		}
		return nID;
	}
	function openEvent(eventID)
	{
		location.href = "viewEvent.aspx?EID=" + eventID;
	}
	
	function viewProfile(eventUserID)
    {
        location.href = "userProfile.aspx?UID=" + eventUserID;
    }
	function doAddEvent()
	{
	    var form = document.forms[0];
	    location.href = "addEvent.aspx?Name=" + form.what.value;
	}
	
	function doSendMessage(srhUserID)
    {        
        openModal("sendUserMessage.aspx?EID=-1&UID=" + srhUserID);
    } 
	
	function ShowDiv()
     {         
		document.getElementById ('divPSummary').style.display='block';
	 }
	 
	function HideDiv()
	{
	    document.getElementById ('divPSummary').style.display='none';
	} 
		
    </script>

    <script type="text/JavaScript">
        DD_roundies.addRule('.timeline-event-tape', '15px', true);
    </script>

</head>
<body onload="breakout_of_frame();onLoad();scrl();scrl1();CloseMyTimeline();" onresize="onResize();">

    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server">
    </asp:ScriptManager>
    <div>
        <div id="container">
            <sedogo:bannerlogincontrol id="bannerLogin" runat="server" />
            <sedogo:banneraddfindcontrol id="bannerAddFindControl" runat="server" />
            <div id="timelines" style="margin-bottom: 6px">
                <div id="tools">
                    <ul class="timeline-options">
                    </ul>
                    <div id="buttons">
                        <%--<a href="#" title="Scroll left" class="left" id="scroll-back">
                            <img src="images/left.gif" title="Scroll left" alt="Left arrow" /></a><a href="#"
                                title="Scroll right" class="right" id="scroll-forward"><img src="images/right.gif"
                                    title="Scroll right" alt="Left arrow" /></a>--%>
                    </div>
                </div>
                <div class="outer_div">
                    <div class="goal_cat_bg">
                        <div class="goal_cat_bg_col1">
                            <a href="#" title="Show goal categories" id="show-categories">
                                <img src="images/goalcategoryclosed.png" alt="" width="137" height="33" /></a></div>
                        <div class="goal_cat_bg_col2">
                            <div class="zoom_minus">
                                <img id="ZoomOut" src="images/minus.png" alt="" width="26" height="26" onmouseover="this.src='images/minus_rollover.png'; this.style.cursor='pointer';"
                                    onmouseout="this.src='images/minus.png'" /></div>
                            <div class="zoom_text">
                                <img src="images/zoom.jpg" alt="" /></div>
                            <div class="zoom_plus">
                                <img id="ZoomIn" src="images/plus.png" onmouseover="this.src='images/plus_rollover.png'; this.style.cursor='pointer';"
                                    onmouseout="this.src='images/plus.png'" alt="" width="26" height="26" /></div>
                        </div>
                        <div class="goal_cat_bg_col3">
                            <div class="arrow_strip">
                                <div class="arrow_previous">
                                    <a href="#" title="Scroll left" class="left" id="scroll-back">
                                        <img src="images/arrow_previous.jpg" alt="" /></a></div>
                                <div class="arrow_midbg">
                                    <div id="smallScroll" class="inner_arrow_midbg">
                                    </div>
                                </div>
                                <div class="arrow_next">
                                    <a href="#" title="Scroll right" class="right" id="scroll-forward">
                                        <img src="images/arrow_next.jpg" alt="" /></a></div>
                                <div id="dateRange" style="text-align: center;">
                                </div>
                            </div>
                        </div>
                        <div class="goal_cat_bg_co14">
                            <a href="help.aspx" title="help">
                                <img src="images/help.png" onmouseover="this.src='images/help_rollover.png'; this.style.cursor='pointer';"
                                    onmouseout="this.src='images/help.png'" alt="" /></a></div>
                    </div>
                    <div class="timeline">
                        <div class="float_left">
                            My timeline
                        </div>
                        <div class="float_right">
                            <img id="imgMngT" src="images/T_Close.jpg" title="Open/Close timeline" alt="Open/Close timeline"
                                style="cursor: pointer;" /></div>
                    </div>
                </div>
                <div class="tl-container" id="my-container" style="clear: both;">
                    <div id="my-timeline">
                    </div>
                    <noscript>
                        This page uses Javascript to show you a Timeline. Please enable Javascript in your
                        browser to see the full page. Thank you.
                    </noscript>
                </div>
            </div>
            <div style="height: 34px; position: relative">
                <div class="misc-pop-up" id="divPSummary">
                    <div class="simileAjax-bubble-container simileAjax-bubble-container-pngTranslucent"
                        style="width: 260px; height: 190px; left: 0px; bottom: 68px;">
                        <div class="simileAjax-bubble-innerContainer simileAjax-bubble-innerContainer-pngTranslucent">
                            <div class="simileAjax-bubble-border-top-left simileAjax-bubble-border-top-left-pngTranslucent">
                            </div>
                            <div class="simileAjax-bubble-border-top-right simileAjax-bubble-border-top-right-pngTranslucent">
                            </div>
                            <div class="simileAjax-bubble-border-bottom-left simileAjax-bubble-border-bottom-left-pngTranslucent">
                            </div>
                            <div class="simileAjax-bubble-border-bottom-right simileAjax-bubble-border-bottom-right-pngTranslucent">
                            </div>
                            <div class="simileAjax-bubble-border-left simileAjax-bubble-border-left-pngTranslucent">
                            </div>
                            <div class="simileAjax-bubble-border-right simileAjax-bubble-border-right-pngTranslucent">
                            </div>
                            <div class="simileAjax-bubble-border-top simileAjax-bubble-border-top-pngTranslucent">
                            </div>
                            <div class="simileAjax-bubble-border-bottom simileAjax-bubble-border-bottom-pngTranslucent">
                            </div>
                            <div class="simileAjax-bubble-contentContainer simileAjax-bubble-contentContainer-pngTranslucent">
                                <div style="position: static; width: 260px;">
                                    <div class="timeline-event-bubble-title">
                                        <asp:Label ID="lblUName" runat="server" />&nbsp;&nbsp;<asp:HyperLink ID="userProfilePopupMessageLink"
                                            ImageUrl="~/images/ico_messages.gif" runat="server" CssClass="modal" /></div>
                                    <div class="timeline-event-bubble-body">
                                        <asp:Image ID="userProfileThumbnailPic" runat="server" />
                                        <p style="font-size: 12px; color: #666; margin-bottom: 8px">
                                            <asp:Label ID="usersProfileDescriptionLabel" runat="server" /></p>
                                        <p style="font-size: 12px;">
                                            <%--Birthday: <span class="blue">
                                                <asp:Label ID="birthdayLabel" runat="server" /></span><br />
                                            Home town: <span class="blue">
                                                <asp:Label ID="homeTownLabel" runat="server" /></span><br />
                                            <span class="blue">--%>
                                            <asp:Label ID="userProfilePopupGoalsLabel" runat="server" /></span> Goals<br />
                                            <span class="blue">
                                                <asp:Label ID="userProfilePopupGoalsAchievedLabel" runat="server" /></span>
                                            Goals Achieved<br />
                                            <span class="blue">
                                                <asp:Label ID="userProfilePopupGroupGoalsLabel" runat="server" /></span> Group
                                            Goals<br />
                                            <span class="blue">
                                                <asp:Label ID="userProfilePopupGoalsFollowedLabel" runat="server" /></span>
                                            Goals Followed
                                        </p>
                                    </div>
                                    <div style="clear: both;">
                                        <br />
                                        <asp:HyperLink ID="usersProfileNameLabel" runat="server" />&nbsp;&nbsp;
                                        <asp:HyperLink ID="userViewProfile" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="simileAjax-bubble-close simileAjax-bubble-close-pngTranslucent misc-pop-up-link-close">
                            </div>
                            <div class="simileAjax-bubble-arrow-point-down simileAjax-bubble-arrow-point-down-pngTranslucent"
                                style="left: 45px;">
                            </div>
                        </div>
                    </div>
                </div>
                <a href="#" title="" class="misc-pop-up-link" onmouseover="ShowDiv()">
                    <asp:Label ID="usersProfileLinkNameLabel" runat="server" /></a>
            </div>
            <div class="controls" id="controls" style="top: 432px">
                <a href="#" class="close-controls">
                    <img src="images/close-controls.gif" title="Close controls" alt="Close controls" /></a>
            </div>
            <div id="other-content">
                <sedogo:sidebarcontrol id="sidebarControl" runat="server" />
                <sedogo:eventslistcontrol id="eventsListControl" runat="server" />
            </div>
            <sedogo:footercontrol id="footerControl" runat="server" />
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
    <sedogo:googleanalyticscontrol id="googleAnalyticsControl" runat="server" />
</body>
</html>

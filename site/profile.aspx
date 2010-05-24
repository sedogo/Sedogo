<%@ Page Language="C#" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="profile" %>

<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="EventsListControl" Src="~/components/eventsListControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="favicon.ico">
    <meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
    <meta http-equiv="content-script-type" content="text/javascript" />
    <meta http-equiv="content-wstyle-type" content="text/css" />
    <meta http-equiv="expires" content="never" />
    <title>Profile : Sedogo : Create your future and connect with others to make it happen</title>
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
				setupFilterHighlightControls(document.getElementById("controls"), tl, [1, 2], theme1);

				//Filter based on current cookie values after timelines load
				performFiltering(tl, [1, 2], document.getElementById("table-filter"));

				//Bug fix: scroll timeline programatically to trigger correct auto-height
				tl.getBand(0)._autoScroll(1);
			}

			function initiateTimeline() {
				var eventSource = new Timeline.DefaultEventSource();

				theme1 = Timeline.ClassicTheme.create();
				theme1.autoWidth = true; // Set the Timeline's "width" automatically. Setting autoWidth on the timelines first bands theme, will affect all bands.

				var bandInfos = [
					Timeline.createBandInfo({
						width: "0",
						theme: theme1						
					}),
					
					Timeline.createBandInfo({					    
						date: "<asp:Literal id="timelineStartDate1" runat="server" />",
						width: "250",
						intervalUnit: Timeline.DateTime.MONTH,
						intervalPixels: 50,
						theme: theme1,
						eventSource: eventSource,
						zoomIndex: 10,
						zoomSteps: new Array(
							{ pixelsPerInterval: 280, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 140, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 70, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 35, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 400, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 200, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 100, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 50, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 400, unit: Timeline.DateTime.MONTH },
							{ pixelsPerInterval: 200, unit: Timeline.DateTime.MONTH },
							{ pixelsPerInterval: 100, unit: Timeline.DateTime.MONTH} // DEFAULT zoomIndex
						)
					}),
					Timeline.createBandInfo({
					    bdate: "<asp:Literal id="timelinebDate" runat="server" />",
						date: "<asp:Literal id="timelineStartDate2" runat="server" />",
						width: "70",
						intervalUnit: Timeline.DateTime.YEAR,
						intervalPixels: 100,
						showEventText: false,
						trackHeight: 0.5,
						trackGap: 0.2,
						theme: theme1,
						eventSource: eventSource,
						overview: true
					})
				];
				//Synchronise band scrolling
				bandInfos[2].syncWith = 1;
				bandInfos[2].highlight = true;

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
*/
				//Create timeline and load data
				tl = Timeline.create(document.getElementById("my-timeline"), bandInfos);
				var url = "<asp:Literal id="timelineURL" runat="server" />";
				Timeline.loadXML(url, function(xml, url) { eventSource.loadXML(xml, url); });
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
		
   //(function() {  
   //Timeline.DefaultEventSource.Event.prototype.fillInfoBubble = function (elmt, theme, labeller) {  
    //alert("test");
    //labeller.closeBubble();
   //};  
   //})();  
   		
    </script>

    <script type="text/javascript">
	$(document).ready(function(){
	});
    function breakout_of_frame()
    {
      if (top.location != location)
      {
        top.location.href = document.location.href ;
      }
    }
    </script>

</head>
<body onload="breakout_of_frame();onLoad();" onresize="onResize();">
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>    
    
        <div id="container">
	        <Sedogo:BannerLoginControl ID="bannerLogin" runat="server" />
	        <Sedogo:BannerAddFindControl ID="bannerAddFindControl" runat="server" />
            <div id="timelines">
                <div id="tools">
                    <ul class="timeline-options">
                        <li class="first"><a href="#" title="View all" id="view-all">View all</a></li>
                        <li class="last"><a href="#" title="Show goal categories" id="show-categories">Show
                            goal categories</a></li>
                    </ul>
                    <div id="buttons">
                        <a href="#" title="Scroll left" class="left" id="scroll-back">
                            <img src="images/left.gif" title="Scroll left" alt="Left arrow" /></a><a href="#"
                                title="Scroll right" class="right" id="scroll-forward"><img src="images/right.gif"
                                    title="Scroll right" alt="Left arrow" /></a>&nbsp;&nbsp;<a href="#" title="Close timeline"
                                        class="off"><img src="images/minus.gif" title="Close timeline" alt="Close timeline" /></a><a
                                            href="#" title="Open timeline" class="on"><img src="images/plus.gif" title="Open timeline"
                                                alt="Open timeline" /></a>
                    </div>
                </div>
                <div class="tl-container">
                    <div id="my-timeline">
                    </div>
                    <noscript>
                        This page uses Javascript to show you a Timeline. Please enable Javascript in your
                        browser to see the full page. Thank you.
                    </noscript>
                </div>
            </div>
            <div class="controls" id="controls">
                <a href="#" class="close-controls">
                    <img src="images/close-controls.gif" title="Close controls" alt="Close controls" /></a>
            </div>
            <div id="other-content">
                <Sedogo:SidebarControl ID="sidebarControl" runat="server" />
                
                <Sedogo:EventsListControl ID="eventsListControl" runat="server" />
            </div>
		    <Sedogo:FooterControl ID="footerControl" runat="server" />
        </div>
        <div id="modal-container">
			<a href="#" class="close-modal"><img src="images/close-modal.gif" title="Close window" alt="Close window" /></a>
            <iframe frameborder="0"></iframe>
        </div>
        <div id="modal-background"></div>

        <iframe id="keepAliveIFrame" runat="server" height="0" width="0" style="border: 0">
        </iframe>
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />
</body>
</html>
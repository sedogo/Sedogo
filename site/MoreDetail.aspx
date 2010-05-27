<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoreDetail.aspx.cs" Inherits="MoreDetail"
    EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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
    <meta http-equiv="content-wstyle-type" content="text/css" />
    <meta http-equiv="expires" content="never" />
    <title>More Detail : Sedogo : Create your future and connect with others to make it
        happen </title>
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
				
				$('#imgMngT').click();
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

    <script type="text/javascript">
    function changeClass(id, newClass)
    {
        document.getElementById(id).className = newClass; 
    }
    </script>

    <script type="text/JavaScript">
        DD_roundies.addRule('.timeline-event-tape', '15px', true);
    </script>

</head>
<body onload="breakout_of_frame();onLoad();" onresize="onResize();">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="scriptManager" runat="server">
        </asp:ScriptManager>
        <div id="container">
            <Sedogo:BannerLoginControl ID="bannerLogin" runat="server" />
            <Sedogo:BannerAddFindControl ID="bannerAddFindControl" runat="server" />
            <div id="timelines">
                <div id="tools">
                    <ul class="timeline-options">
                    </ul>
                    <div id="buttons">
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
            <div class="controls" id="controls">
                <a href="#" class="close-controls">
                    <img src="images/close-controls.gif" title="Close controls" alt="Close controls" /></a>
            </div>
            <div id="other-content">
                <div class="one-col">
                    <h2 class="col-header">
                        <asp:HyperLink ID="myProfileTextLabel" runat="server" Text="My profile" />
                        <span>
                            <asp:HyperLink ID="editProfileLink" NavigateUrl="~/editProfile.aspx" ToolTip="Edit profile"
                                Text="Edit" runat="server" /></span></h2>
                    <asp:Image ID="profileImage" runat="server" CssClass="profile" />
                    <span style="font-size: 14px; font-weight: bold; color: Black">
                        <asp:Label ID="userNameLabel" runat="server" /></span><br />
                    <!--<p class="profile-intro">-->
                    <!--<asp:Label ID="profileTextLabel" runat="server" /></p>-->
                    <br />
                    <p class="extra-buttons">
                        <asp:LinkButton ID="viewArchiveLink" runat="server" Text="view archive" CssClass="button-sml"
                            OnClick="click_viewArchiveLink" />
                        <asp:HyperLink ID="addGoalLink" NavigateUrl="~/addEvent.aspx" ToolTip="add goal"
                            CssClass="button-sml modal" Text="+ Goal" runat="server" />
                    </p>
                    <div id="sidebarMenuItems" runat="server">
                        <ol class="items">
                            <li class="messages">
                                <asp:HyperLink ID="messageCountLink" runat="server" NavigateUrl="~/message.aspx" /></li>
                            <li class="alerts">
                                <asp:HyperLink ID="alertCountLink" NavigateUrl="~/alert.aspx" runat="server" /></li>
                            <li class="invites">
                                <asp:HyperLink ID="inviteCountLink" NavigateUrl="~/invite.aspx" runat="server" /></li>
                            <li class="requests">
                                <asp:HyperLink ID="goalJoinRequestsLink" NavigateUrl="~/eventJoinRequests.aspx" runat="server" /></li>
                            <li class="following">
                                <asp:HyperLink ID="trackingCountLink" NavigateUrl="~/tracking.aspx" runat="server" /></li>
                            <li class="goal-groups">
                                <asp:HyperLink ID="groupGoalsLink" NavigateUrl="~/groupGoals.aspx" runat="server" /></li>
                            <li class="addressbook">
                                <asp:HyperLink ID="addressBookLink" NavigateUrl="~/addressBook.aspx" runat="server" /></li>
                        </ol>
                    </div>
                    <div class="latestGoals">
                        <h3>
                            <asp:Label ID="myLatestGoalsLabel" runat="server" Text="My latest goals" /></h3>
                        <p>
                            <asp:PlaceHolder ID="latestEventsPlaceholder" runat="server" />
                        </p>
                        <div class="pinstripe-divider">
                        </div>
                        <!--<h3>Latest searches</h3>-->
                        <!--<p><asp:PlaceHolder id="latestSearchesPlaceholder" runat="server" /></p>-->
                        <!--<div class="pinstripe-divider"></div>-->
                        <h3>
                            Popular goals</h3>
                        <p>
                            <asp:PlaceHolder ID="popularSearchesPlaceholder" runat="server" />
                        </p>
                    </div>
                    <p>
                        &nbsp;</p>
                    <div class="rotatorBackground">
                        <telerik:RadRotator ID="eventRotator" runat="server" Width="232px" Height="216px"
                            CssClass="horizontalRotator" ItemHeight="216" ItemWidth="232" RotatorType="FromCode">
                            <ItemTemplate>
                                <div class="itemTemplate">
                                    <a href="getInspired.aspx">
                                        <img src='<%# Page.ResolveUrl("~/images/") + Container.DataItem %>.png' alt="Customer Image" /></a>
                                </div>
                            </ItemTemplate>
                        </telerik:RadRotator>
                    </div>
                </div>
                <div style="width: 720px; float: left;">
                    <div style="width: 100%; height: 15px; background-color: #00CDFF; padding: 5px; padding-top: 2px;">
                        <div style="color: White; font-size: 14px; font-weight: bold; width: 50%; float: left;">
                            Latest updated goals</div>
                        <div style="text-align: right; width: 50%; padding: 0px; float: left;">
                            <asp:Button runat="server" BackColor="white" ID="backButton" BorderStyle="None" OnClick="backButton_Click"
                                Text="Back" /></div>
                    </div>
                    <div style="width: 100%">
                    </div>
                    <div style="width: 100%">
                        <h3 style="color: #00CDFF; margin: 0px; margin-top: 10px; margin-bottom: 5px;">
                            Today</h3>
                        <asp:PlaceHolder ID="PlaceHolderToday" runat="server" />
                        <h3 style="color: #00CDFF; margin: 0px; margin-top: 10px; margin-bottom: 5px;">
                            This Week</h3>
                        <asp:PlaceHolder ID="PlaceHolderThisWeek" runat="server" />
                        <h3 style="color: #00CDFF; margin: 0px; margin-top: 10px; margin-bottom: 5px;">
                            Two Weeks Ago</h3>
                        <asp:PlaceHolder ID="PlaceHolderTwoWeekAgo" runat="server" />
                        <h3 style="color: #00CDFF; margin: 0px; margin-top: 10px; margin-bottom: 5px;">
                            Last Month</h3>
                        <asp:PlaceHolder ID="PlaceHolderLastMonth" runat="server" />
                        <h3 style="color: #00CDFF; margin: 0px; margin-top: 10px; margin-bottom: 5px;">
                            Last Six Month</h3>
                        <asp:PlaceHolder ID="PlaceHolderLastSixMonth" runat="server" />
                    </div>
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
        <iframe id="keepAliveIFrame" runat="server" height="0" width="0" style="border: 0">
        </iframe>
    </div>
    </form>
    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />
</body>
</html>
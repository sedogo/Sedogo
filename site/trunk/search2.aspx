<%@ Page Language="C#" AutoEventWireup="true" CodeFile="search2.aspx.cs" Inherits="search2" %>

<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="EventsListControl" Src="~/components/eventsListControl.ascx" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link rel="shortcut icon" href="favicon.ico" />
    <meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
    <meta http-equiv="content-script-type" content="text/javascript" />
    <meta http-equiv="content-style-type" content="text/css" />
    <meta http-equiv="expires" content="never" />
    <title>Home : Sedogo : Create your future timeline. Connect, track and interact with
        like minded people.</title>
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
    <link rel="stylesheet" href="css/calendarStyle.css" />
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
                var bdate = '<%=BYr %>';

				theme1 = Timeline.ClassicTheme.create();
				theme1.autoWidth = true; // Set the Timeline's "width" automatically. Setting autoWidth on the timelines first bands theme, will affect all bands.

				var bandInfos = [
					Timeline.createBandInfo({
					    bdate: bdate,
						width: "0",
						theme: theme1
					}),
					Timeline.createBandInfo({
					    bdate: bdate,
						date: "<asp:Literal id="timelineStartDate1" runat="server" />",
						width: "235",
						intervalUnit: Timeline.DateTime.YEAR,
						intervalPixels: 100,
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
							{ pixelsPerInterval: 100, unit: Timeline.DateTime.YEAR }						
						)
					}),
					Timeline.createBandInfo({
					    bdate: bdate,
						date: "<asp:Literal id="timelineStartDate2" runat="server" />",
						width: "70",
						intervalUnit: Timeline.DateTime.YEAR,
						intervalPixels: 35,
						showEventText: false,
						trackHeight: 0.5,
						trackGap: 0.2,
						theme: theme1,
						eventSource: eventSource,
						overview: true
					}),
					Timeline.createBandInfo({
					    bdate: bdate,
						width: "0",
						theme: theme1
/*
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
*/
					}),
					Timeline.createBandInfo({
					    bdate: bdate,
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
					    bdate: bdate,
						date: "<asp:Literal id="timelineStartDate3" runat="server" />",
						width: "235",
						intervalUnit: Timeline.DateTime.YEAR,
						intervalPixels: 100,
						theme: theme1,
						eventSource: eventSourceSearch
					}),
					Timeline.createBandInfo({
					    bdate: bdate,
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
                   		endLabel: "You have searched for <asp:Literal ID="searchForLiteral1" runat="server" />",
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
                   		endLabel: "<asp:Literal id="searchResultsLabel" runat="server" />",
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
	$(document).ready(function(){
	});
    function breakout_of_frame()
    {
      if (top.location != location)
      {
        top.location.href = document.location.href ;
      }
    }
    function openEvent(eventID)
    {
        location.href = "viewEvent.aspx?EID=" + eventID;
    }    
    function viewProfile(eventUserID)
    {
        location.href = "userProfile.aspx?UID=" + eventUserID;
    }
    function viewUserTimeline(eventUserID)
    {
        location.href = "userTimeline.aspx?UID=" + eventUserID;
    }   
    function doSendMessage(srhUserID)
    {        
        openModal("sendUserMessage.aspx?EID=-1&UID=" + srhUserID);
    }    
    
    </script>

    <script language="javascript" type="text/javascript">
var state = 'none';
function showhide(layer_ref)
{
    if (state == 'block')
    {
        state = 'none';
    }
    else
    {
        state = 'block';
    }
    if (document.all)
    { //IS IE 4 or 5 (or 6 beta)
        eval("document.all." + layer_ref + ".style.display = state");
    }
    if (document.layers)
    { //IS NETSCAPE 4 or below
        document.layers[layer_ref].display = state;
    }
    if (document.getElementById && !document.all)
    {
        hza = document.getElementById(layer_ref);
        hza.style.display = state;
    }
}
    </script>

    <script type="text/JavaScript">
        DD_roundies.addRule('.timeline-event-tape', '15px', true);
    </script>
    
</head>
<body class="search2" onload="breakout_of_frame();onLoad();scrl();scrl1();CloseMyTimeline();" onresize="onResize();">

    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server">
    </asp:ScriptManager>
    <div>
        <div id="container">
            <sedogo:bannerlogincontrol id="bannerLogin" runat="server" />
            <sedogo:banneraddfindcontrol id="bannerAddFindControl" runat="server" />
            <div id="advSearchCriteria">
                <table border="0" cellspacing="10" cellpadding="0" width="100%" class="advanced-search-table">
                    <tr>
                        <td colspan="2">
                            <div id="noSearchResultsDiv" runat="server" class="errorMessage">
                                <p>
                                    There were no results found, please try again or refine your search</p>
                            </div>
                            <div id="moreThan50ResultsDiv" runat="server" class="errorMessage">
                                <p>
                                    There were more than 50 matching results found, please refine your search</p>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h3 class="noTopMargin blue">
                                <a href="#" onclick="showhide('searchDiv');">Advanced Search</a></h3>
                        </td>
                        <td align="right">
                            <a href="#" onclick="showhide('searchDiv');">
                                <img src="/images/search_toggle.gif" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="searchDiv" style="display: none">
                                <table border="0" cellspacing="4" cellpadding="2">
                                    <tr>
                                        <td>
                                            Goal name
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="eventNameTextBox" Width="200px" MaxLength="200" />
                                        </td>
                                        <td>
                                            Category
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="categoryDropDownList" runat="server">
                                                <asp:ListItem Text="All" Value="-1" />
                                                <asp:ListItem Text="Personal" Value="1" />
                                                <asp:ListItem Text="Travel" Value="2" />
                                                <asp:ListItem Text="Friends" Value="3" />
                                                <asp:ListItem Text="Family" Value="4" />
                                                <asp:ListItem Text="General" Value="5" />
                                                <asp:ListItem Text="Health" Value="6" />
                                                <asp:ListItem Text="Money" Value="7" />
                                                <asp:ListItem Text="Education" Value="8" />
                                                <asp:ListItem Text="Hobbies" Value="9" />
                                                <asp:ListItem Text="Work" Value="10" />
                                                <asp:ListItem Text="Culture" Value="11" />
                                                <asp:ListItem Text="Charity" Value="12" />
                                                <asp:ListItem Text="Green" Value="13" />
                                                <asp:ListItem Text="Misc" Value="14" />
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Recently added
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="recentlyAddedDropDownList" runat="server">
                                                <asp:ListItem Text="All" Value="-1" />
                                                <asp:ListItem Text="Last Day" Value="1" />
                                                <asp:ListItem Text="Last Week" Value="7" />
                                                <asp:ListItem Text="Last Month" Value="31" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Where
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="venueTextBox" Width="200px" MaxLength="200" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            Recently updated
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="recentlyUpdatedDropDownList" runat="server">
                                                <asp:ListItem Text="All" Value="-1" />
                                                <asp:ListItem Text="Last Day" Value="1" />
                                                <asp:ListItem Text="Last Week" Value="7" />
                                                <asp:ListItem Text="Last Month" Value="31" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Goal owner name
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="eventOwnerNameTextBox" Width="200px" MaxLength="200" />
                                        </td>
                                        <td valign="top">
                                            <asp:RadioButton ID="beforeBirthdayRadioButton" Text="Before age" GroupName="dateRadioGroup"
                                                runat="server" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="birthdayDropDownList" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Definitly do
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="definitlyDoDropDownList" runat="server">
                                                <asp:ListItem Text="All" Value="A" />
                                                <asp:ListItem Text="Definitly do" Value="D" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                    </tr>
                    <td colspan="6" style="padding-top: 5px">
                        <asp:LinkButton ID="advSearchButton" runat="server" Text="Search" OnClick="advSearchButton_click"
                            CssClass="button-sml" />
                        <asp:LinkButton ID="backToProfileButton" runat="server" Text="Back to profile" OnClick="backToProfileButton_click"
                            CssClass="button-sml" />
                    </td>
                    <tr>
                </table>
            </div>
            </td> </tr> </table>
        </div>
        <h2 class="plain">
            You have searched for
            <asp:Literal ID="searchForLiteral2" runat="server" /></h2>
        <p>
            Search results are shown below your timeline</p>
        <div id="timelines">
            <div id="tools">
                <ul class="timeline-options">
                    <%--<li class="first"><a href="#" title="View all" id="view-all">View all</a></li>--%>
                    <%--<li class="last"><a href="#" title="Show goal categories" id="show-categories">Show
                            goal categories</a></li>--%>
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
                                    <img src="images/arrow_previous1.jpg" onmouseover="this.src='images/arrow_previous.jpg'; this.style.cursor='pointer';"
                                    onmouseout="this.src='images/arrow_previous1.jpg'" alt="" /></a></div>
                            <div class="arrow_midbg">
                                <div id="smallScroll" class="inner_arrow_midbg">
                                </div>
                            </div>
                            <div class="arrow_next">
                                <a href="#" title="Scroll right" class="right" id="scroll-forward">
                                    <img src="images/arrow_next1.jpg" onmouseover="this.src='images/arrow_next.jpg'; this.style.cursor='pointer';"
                                    onmouseout="this.src='images/arrow_next1.jpg'" alt="" /></a></div>
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
    </form>
    <sedogo:googleanalyticscontrol id="googleAnalyticsControl" runat="server" />
</body>
</html>

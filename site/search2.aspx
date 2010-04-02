<%@ Page Language="C#" AutoEventWireup="true" CodeFile="search2.aspx.cs" Inherits="search2" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
	<link rel="stylesheet" href="css/calendarStyle.css" />
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
						width: "200",
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
					}),
					Timeline.createBandInfo({
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
						width: "200",
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
    </script>
    <script language="JavaScript" type="text/javascript">
    function PickerRangeStartDate_OnChange()
    {
        <%= CalendarRangeStartDate.ClientID.Replace("$","_").Replace(":","_") %>.SetSelectedDate(<%= PickerRangeStartDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate());
    }
    function CalendarRangeStartDate_OnChange()
    {
        <%= PickerRangeStartDate.ClientID.Replace("$","_").Replace(":","_") %>.SetSelectedDate(<%= CalendarRangeStartDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate());
    }
    function popupCalendarRangeStartDate(image)
    {
        if( <%= CalendarRangeStartDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate() == null )
        {
            <%= CalendarRangeStartDate.ClientID.Replace("$","_").Replace(":","_") %>.ClearSelectedDate();
        }
        <%=CalendarRangeStartDate.ClientObjectId%>.Show(image);    
    }
    function PickerRangeEndDate_OnChange()
    {
        <%= CalendarRangeEndDate.ClientID.Replace("$","_").Replace(":","_") %>.SetSelectedDate(<%= PickerRangeEndDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate());
    }
    function CalendarRangeEndDate_OnChange()
    {
        <%= PickerRangeEndDate.ClientID.Replace("$","_").Replace(":","_") %>.SetSelectedDate(<%= CalendarRangeEndDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate());
    }
    function popupCalendarRangeEndDate(image)
    {
        if( <%= CalendarRangeEndDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate() == null )
        {
            <%= CalendarRangeEndDate.ClientID.Replace("$","_").Replace(":","_") %>.ClearSelectedDate();
        }
        <%=CalendarRangeEndDate.ClientObjectId%>.Show(image);    
    }
    </script>
</head>
<body class="search2" onload="breakout_of_frame();onLoad();" onresize="onResize();">
    <form id="form1" runat="server">
    <div>
    
	    <div id="container">
	        <Sedogo:BannerLoginControl ID="bannerLogin" runat="server" />
	        <Sedogo:BannerAddFindControl ID="bannerAddFindControl" runat="server" />
		    <div id="advSearchCriteria">
		        <table border="0" cellspacing="10" cellpadding="0" width="100%" class="advanced-search-table">
				    <tr>
					    <td>
						    <div id="noSearchResultsDiv" runat="server" class="errorMessage">
							    <p>There were no results found, please try again or refine your search</p>
						    </div>
						    <div id="moreThan50ResultsDiv" runat="server" class="errorMessage">
							    <p>There were more than 50 matching results found, please refine your search</p>
						    </div>
					    </td>
				    </tr>
		            <tr>
		                <td><h3 class="noTopMargin blue">Advanced Search</h3></td>
		            </tr>    
		            <tr>
		                <td><p class="blue">Refine your search:</p></td>
		            </tr>
		            <tr>
		                <td>
		                    <table>
		                        <tr>
		                            <td>Goal name</td>
		                            <td><asp:TextBox runat="server"
                                        ID="eventNameTextBox" Width="200px" MaxLength="200" /></td>
		                            <td>Category</td>
		                            <td><asp:DropDownList ID="categoryDropDownList" runat="server">
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
		                            </asp:DropDownList></td>
		                            <td>Recently added</td>
		                            <td><asp:DropDownList ID="recentlyAddedDropDownList" runat="server">
                                        <asp:ListItem Text="All" Value="-1" />
                                        <asp:ListItem Text="Last Day" Value="1" />
                                        <asp:ListItem Text="Last Week" Value="7" />
                                        <asp:ListItem Text="Last Month" Value="31" />
		                            </asp:DropDownList></td>
		                        </tr>
		                        <tr>
		                            <td>Where</td>
		                            <td><asp:TextBox runat="server"
                                        ID="venueTextBox" Width="200px" MaxLength="200" /></td>
                                    <td><asp:RadioButton id="betweenDatesRadioButton"
                                       Text="Between dates" Checked="true" GroupName="dateRadioGroup" 
                                       runat="server" /></td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <table border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td><ComponentArt:Calendar ID="PickerRangeStartDate" 
                                                                runat="server" 
                                                                ClientSideOnSelectionChanged="PickerRangeStartDate_OnChange"
                                                                ControlType="Picker" 
                                                                PickerCssClass="picker" 
                                                                ImagesBaseUrl="./images/calendarImages/"
                                                                PickerCustomFormat="dd/MM/yyyy" PickerFormat="Custom" 
                                                                PopUpExpandControlId="calendarButton">
                                                            </ComponentArt:Calendar>
                                                            <ComponentArt:Calendar ID="CalendarRangeStartDate" 
                                                                runat="server" 
                                                                CalendarCssClass="calendar"
                                                                TitleCssClass="title" 
                                                                ControlType="Calendar"
                                                                DayCssClass="day" 
                                                                DayHeaderCssClass="dayheader" 
                                                                DayHoverCssClass="dayhover" 
                                                                DayNameFormat="FirstTwoLetters"
                                                                ImagesBaseUrl="./images/calendarImages/"
                                                                MonthCssClass="month"
                                                                NextImageUrl="cal_nextMonth.gif"
                                                                NextPrevCssClass="nextprev" 
                                                                OtherMonthDayCssClass="othermonthday" 
                                                                PrevImageUrl="cal_prevMonth.gif" 
                                                                SelectedDayCssClass="selectedday" 
                                                                SelectMonthCssClass="selector"
                                                                SelectMonthText="&curren;" 
                                                                SelectWeekCssClass="selector"
                                                                SelectWeekText="&raquo;" 
                                                                SwapDuration="300"
                                                                SwapSlide="Linear"
                                                                ClientSideOnSelectionChanged="CalendarRangeStartDate_OnChange" 
                                                                PopUp="Custom" 
                                                                PopUpExpandControlId="calendarButton">
                                                            </ComponentArt:Calendar></td>
                                                            <td><img alt="Click for pop-up calendar" 
                                                                id="Img1" 
                                                                onclick="popupCalendarRangeStartDate(this)" 
                                                                class="calendar_button" 
                                                                src="./images/calendarImages/btn_calendar.gif" 
                                                                width="25" height="22" /></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>To</td>
                                                <td>
                                                    <table border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td><ComponentArt:Calendar ID="PickerRangeEndDate" 
                                                                runat="server" 
                                                                ClientSideOnSelectionChanged="PickerRangeEndDate_OnChange"
                                                                ControlType="Picker" 
                                                                PickerCssClass="picker" 
                                                                ImagesBaseUrl="./images/calendarImages/"
                                                                PickerCustomFormat="dd/MM/yyyy" PickerFormat="Custom" 
                                                                PopUpExpandControlId="calendarButton">
                                                            </ComponentArt:Calendar>
                                                            <ComponentArt:Calendar ID="CalendarRangeEndDate" 
                                                                runat="server" 
                                                                CalendarCssClass="calendar"
                                                                TitleCssClass="title" 
                                                                ControlType="Calendar"
                                                                DayCssClass="day" 
                                                                DayHeaderCssClass="dayheader" 
                                                                DayHoverCssClass="dayhover" 
                                                                DayNameFormat="FirstTwoLetters"
                                                                ImagesBaseUrl="./images/calendarImages/"
                                                                MonthCssClass="month"
                                                                NextImageUrl="cal_nextMonth.gif"
                                                                NextPrevCssClass="nextprev" 
                                                                OtherMonthDayCssClass="othermonthday" 
                                                                PrevImageUrl="cal_prevMonth.gif" 
                                                                SelectedDayCssClass="selectedday" 
                                                                SelectMonthCssClass="selector"
                                                                SelectMonthText="&curren;" 
                                                                SelectWeekCssClass="selector"
                                                                SelectWeekText="&raquo;" 
                                                                SwapDuration="300"
                                                                SwapSlide="Linear"
                                                                ClientSideOnSelectionChanged="CalendarRangeEndDate_OnChange" 
                                                                PopUp="Custom" 
                                                                PopUpExpandControlId="calendarButton">
                                                            </ComponentArt:Calendar></td>
                                                            <td><img alt="Click for pop-up calendar" 
                                                                id="Img2" 
                                                                onclick="popupCalendarRangeEndDate(this)" 
                                                                class="calendar_button" 
                                                                src="./images/calendarImages/btn_calendar.gif" 
                                                                width="25" height="22" /></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
		                            <td>Recently updated</td>
		                            <td><asp:DropDownList ID="recentlyUpdatedDropDownList" runat="server">
                                        <asp:ListItem Text="All" Value="-1" />
                                        <asp:ListItem Text="Last Day" Value="1" />
                                        <asp:ListItem Text="Last Week" Value="7" />
                                        <asp:ListItem Text="Last Month" Value="31" />
		                            </asp:DropDownList></td>
		                        </tr>
		                        <tr>
		                            <td>Goal owner name</td>
		                            <td><asp:TextBox runat="server"
                                        ID="eventOwnerNameTextBox" Width="200px" MaxLength="200" /></td>
                                    <td valign="top"><asp:RadioButton id="beforeBirthdayRadioButton"
                                       Text="Before age" GroupName="dateRadioGroup" 
                                       runat="server" /></td>
                                    <td><asp:DropDownList ID="birthdayDropDownList" runat="server">
                                        </asp:DropDownList></td>
		                            <td>Definitly do</td>
		                            <td><asp:DropDownList ID="definitlyDoDropDownList" runat="server">
                                        <asp:ListItem Text="All" Value="A" />
                                        <asp:ListItem Text="Definitly do" Value="D" />
		                            </asp:DropDownList></td>
		                        </tr>
		                        </tr>
		                            <td colspan="6">
		                                <asp:LinkButton ID="advSearchButton" runat="server" Text="Search" 
                                            OnClick="advSearchButton_click" CssClass="button-sml" />
                                        <asp:LinkButton ID="backToProfileButton" runat="server" Text="Back to profile" 
                                            OnClick="backToProfileButton_click" CssClass="button-sml" />
		                            </td>
		                        <tr>
		                    </table>
		                </td>
		            </tr>
		        </table>
		    </div>
		    <h2 class="plain">You have searched for <asp:Literal ID="searchForLiteral2" runat="server" /></h2>
		    <p>Search results are shown below your timeline</p>
		    <div id="timelines">
			    <div id="tools">
				    <ul class="timeline-options">
					    <li class="first"><a href="#" title="View all" id="view-all">View all</a></li>
					    <li class="last"><a href="#" title="Show goal categories" id="show-categories">Show goal categories</a></li>
				    </ul>
				    <div id="buttons">
					    <a href="#" title="Scroll left" class="left" id="scroll-back"><img src="images/left.gif" title="Scroll left" alt="Left arrow" /></a><a href="#" title="Scroll right" class="right" id="scroll-forward"><img src="images/right.gif" title="Scroll right" alt="Left arrow" /></a>&nbsp;&nbsp;<a href="#" title="Close timeline" class="off"><img src="images/minus.gif" title="Close timeline" alt="Close timeline" /></a><a href="#" title="Open timeline" class="on"><img src="images/plus.gif" title="Open timeline" alt="Open timeline" /></a>
				    </div>
			    </div>
			    <div style="background: #f0f1ec; color: #0cf; font-size: 18px; font-weight: bold; padding: 1px 0 0 1px; height: 24px">My timeline</div>	
			    <div class="tl-container">
				    <div id="my-timeline"></div>
				    <noscript>
					    This page uses Javascript to show you a Timeline. Please enable Javascript in your browser to see the full page. Thank you.
				    </noscript>
			    </div>
		    </div>
		    <div class="controls" id="controls" style="top: 432px">
			    <a href="#" class="close-controls"><img src="images/close-controls.gif" title="Close controls" alt="Close controls" /></a>
		    </div>
		    <div id="other-content">
                <Sedogo:SidebarControl ID="sidebarControl" runat="server" />
			    <div class="one-col">
				    <h2 class="col-header">This month</h2>
				    <div class="events">
					    <asp:Label ID="overdueTitleLabel" runat="server" Text="Overdue" />
					    <asp:PlaceHolder ID="overdueEventsPlaceHolder" runat="server" />
					    <asp:Label ID="todaysDateLabel" runat="server" />
					    <asp:PlaceHolder ID="todayEventsPlaceHolder" runat="server" />
					    <asp:Label ID="thisWeekTitleLabel" runat="server" Text="This week" />
					    <asp:PlaceHolder ID="thisWeekEventsPlaceHolder" runat="server" />
					    <asp:Label ID="thisMonthTitleLabel" runat="server" Text="This month" />
					    <asp:PlaceHolder ID="thisMonthEventsPlaceHolder" runat="server" />
				    </div>
			    </div>
			    <div class="one-col">
				    <h2 class="col-header">Next 5 years</h2>
				    <div class="events">
					    <asp:Label ID="thisYearTitleLabel" runat="server" Text="This year" />
					    <asp:PlaceHolder ID="nextYearEventsPlaceHolder" runat="server" />
					    <asp:Label ID="next2YearsTitleLabel" runat="server" Text="Next 2 years" />
					    <asp:PlaceHolder ID="next2YearsEventsPlaceHolder" runat="server" />
					    <asp:Label ID="next3YearsTitleLabel" runat="server" Text="Next 3 years" />
					    <asp:PlaceHolder ID="next3YearsEventsPlaceHolder" runat="server" />
					    <asp:Label ID="next4YearsTitleLabel" runat="server" Text="Next 4 years" />
					    <asp:PlaceHolder ID="next4YearsEventsPlaceHolder" runat="server" />
					    <asp:Label ID="next5YearsTitleLabel" runat="server" Text="Next 5 years" />
					    <asp:PlaceHolder ID="next5YearsEventsPlaceHolder" runat="server" />
				    </div>
			    </div>
			    <div class="one-col-end">
				    <h2 class="col-header">5 years+</h2>
				    <div class="events">
					    <asp:Label ID="fiveToTenYearsTitleLabel" runat="server" Text="5-10 years" />
					    <asp:PlaceHolder ID="next10YearsEventsPlaceHolder" runat="server" />
					    <asp:Label ID="tenToTwentyYearsTitleLabel" runat="server" Text="10-20 years" />
					    <asp:PlaceHolder ID="next20YearsEventsPlaceHolder" runat="server" />
					    <asp:Label ID="twentyPlusYearsTitleLabel" runat="server" Text="20+ years" />
					    <asp:PlaceHolder ID="next100YearsEventsPlaceHolder" runat="server" />
					    <asp:Label ID="unknownDateTitleLabel" runat="server" Text="Unknown" />
					    <asp:PlaceHolder ID="notScheduledEventsPlaceHolder" runat="server" />
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
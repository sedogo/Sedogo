<%@ Page Language="C#" AutoEventWireup="true" CodeFile="search2.aspx.cs" Inherits="search2"
    MasterPageFile="Main.master" %>

<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="EventsListControl" Src="~/components/eventsListControl.ascx" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content runat="server" ContentPlaceHolderID="title">
    Home : Sedogo : Create your future timeline. Connect, track and interact with like
    minded people.</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="head">
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
        $(document).ready(function () {
        });
        function breakout_of_frame() {
            if (top.location != location) {
                top.location.href = document.location.href;
            }
        }
        function openEvent(eventID) {
            location.href = "viewEvent.aspx?EID=" + eventID;
        }
        function viewProfile(eventUserID) {
            location.href = "userProfile.aspx?UID=" + eventUserID;
        }
        function viewUserTimeline(eventUserID) {
            location.href = "userTimeline.aspx?UID=" + eventUserID;
        }
        function doSendMessage(srhUserID) {
            openModal("sendUserMessage.aspx?EID=-1&UID=" + srhUserID);
        }    
    
    </script>
    <script language="javascript" type="text/javascript">
        var state = 'none';
        function showhide(layer_ref) {
            if (state == 'block') {
                state = 'none';
            }
            else {
                state = 'block';
            }
            if (document.all) { //IS IE 4 or 5 (or 6 beta)
                eval("document.all." + layer_ref + ".style.display = state");
            }
            if (document.layers) { //IS NETSCAPE 4 or below
                document.layers[layer_ref].display = state;
            }
            if (document.getElementById && !document.all) {
                hza = document.getElementById(layer_ref);
                hza.style.display = state;
            }
        }
    </script>
    <script type="text/JavaScript">
        DD_roundies.addRule('.timeline-event-tape', '15px', true);
        $(document).ready(function () {
            breakout_of_frame(); onLoad(); scrl(); scrl1(); CloseMyTimeline();
            $('body').addClass('search2');
        });
        $('body').resize(function () {
            onResize();
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="header">
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
                        <img src="images/search_toggle.gif" /></a>
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
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="padding-top: 5px">
                    <asp:LinkButton ID="advSearchButton" runat="server" Text="Search" OnClick="advSearchButton_click"
                        CssClass="button-sml" />
                    <asp:LinkButton ID="backToProfileButton" runat="server" Text="Back to profile" OnClick="backToProfileButton_click"
                        CssClass="button-sml" />
                </td>
            </tr>
        </table>
    </div>
    <h2 class="plain">
        You have searched for
        <asp:Literal ID="searchForLiteral2" runat="server" /></h2>
    <p>
        Search results are shown below your timeline</p>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <Sedogo:EventsListControl ID="eventsListControl" runat="server" />
</asp:Content>

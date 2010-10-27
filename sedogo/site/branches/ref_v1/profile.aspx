<%@ Page Language="C#" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="profile"
    MasterPageFile="Main.master" %>

<%@ Register TagPrefix="Sedogo" TagName="EventsListControl" Src="~/components/eventsListControl.ascx" %>
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
					    bdate: "<asp:Literal id="timelinebDate" runat="server" />",
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
        $(document).ready(function () {
        });
        function breakout_of_frame() {
            if (top.location != location) {
                top.location.href = document.location.href;
            }
        }

        function doAddEvent() {
            var form = document.forms[0];
            location.href = "addEvent.aspx?Name=" + form.what.value;
        }
        function openEvent(eventID) {
            location.href = "viewEvent.aspx?EID=" + eventID;
        }

        function viewProfile(eventUserID) {
            location.href = "userProfile.aspx?UID=" + eventUserID;
        }
        function doSendMessage(srhUserID) {
            openModal("sendUserMessage.aspx?EID=-1&UID=" + srhUserID);
        }
        function viewUserTimeline(eventUserID) {
            location.href = "userTimeline.aspx?UID=" + eventUserID;
        }
    
    </script>
    <script type="text/JavaScript">
        DD_roundies.addRule('.timeline-event-tape', '15px', true);
        $(document).ready(function () {
            breakout_of_frame(); onLoad(); scrl();
        });
        $('body').resize(function () {
            onResize();
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="content">
    <div id="welcomeMessageDiv" runat="server"><h2>Welcome to your lifes timeline</h2>
                <p class="teaser">Now you've joined, it's time to add your first goal. It can be big or small. 
                Just click 'Add goal' at the top and you're away. If you can't think of one, why not 
                make 'Adding a goal' your goal. It could be the very first goal you achieve 
                (and the beginning of much greater things…).</p>
                <a href="addEvent.aspx" class="modal">Add you're first goal here</a><p>&nbsp;</p></div>
    <Sedogo:EventsListControl ID="eventsListControl" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="footer">
    <iframe id="keepAliveIFrame" runat="server" height="0" width="0" style="border: 0">
    </iframe>
</asp:Content>

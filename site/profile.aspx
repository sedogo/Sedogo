<%@ Page Language="C#" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="profile" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
	<!--[if gte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->

	<script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="js/jquery-ui-1.7.2.custom.min.js"></script>
	<script type="text/javascript" src="js/ui.dialog.js"></script>
	<script type="text/javascript" src="js/jquery.cookie.js"></script>
	<script type="text/javascript" src="js/jquery.livequery.js"></script>
	<script type="text/javascript" src="js/main.js"></script>

	<script type="text/javascript">
		Timeline_ajax_url = "js/timeline/timeline_ajax/simile-ajax-api.js";
		Timeline_urlPrefix = 'js/timeline/timeline_js/';
		Timeline_parameters = 'bundle=true';
	</script>
	<script src="js/timeline/timeline_js/timeline-api.js" type="text/javascript"></script>
	<script type="text/javascript">
		var tl;

		function onLoad() {
			var eventSource = new Timeline.DefaultEventSource();
			var bandInfos = [
				Timeline.createBandInfo({
					date: "Oct 08 2009 00:00:00 GMT",
					width: "85%",
					intervalUnit: Timeline.DateTime.MONTH,
					intervalPixels: 50,
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
					date: "Oct 08 2009 00:00:00 GMT",
					width: "15%",
					intervalUnit: Timeline.DateTime.YEAR,
					intervalPixels: 100,
					showEventText: false,
					trackHeight: 0.5,
					trackGap: 0.2,
					eventSource: eventSource,
					overview: true
				})
			];
			bandInfos[1].syncWith = 0;
			bandInfos[1].highlight = true;

			tl = Timeline.create(document.getElementById("my-timeline"), bandInfos);
			Timeline.loadXML("example1.xml", function(xml, url) { eventSource.loadXML(xml, url); });
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
		//Set widths and left positions of timelines based on info held in database (hard-coded for now)
		<asp:Literal id="timelineItems1" runat="server" />
	});
    function breakout_of_frame()
    {
      if (top.location != location)
      {
        top.location.href = document.location.href ;
      }
    }
    function getElementID( name )
    {
	    var form = document.forms[0];
        var nID = -1;
        for( i=0 ; i < form.elements.length ; i++ )
        {
            if( form.elements[i].name == name )
            {
                nID = i;
            }
        }
        return nID;
    }
    function searchClick()
    {
	    //var form = document.forms[0];
	    //alert(form.aim[1].checked);
	    //if( form.aim[1].checked == "true" )
	    //{
	    //    location.href = "addEvent.aspx";
        //    return false;
	    //}
	    //else
	    //{
	        return true;
        //}
    }
    </script>
</head>
<body onload="breakout_of_frame();onLoad();" onresize="onResize();">
    <form id="form1" runat="server">
    <div>
    
	<div id="container">
		<ul id="account-options">
		    <Sedogo:BannerLoginControl ID="bannerLogin" runat="server" />
		</ul>
		<div class="one-col">
			<a href="default.aspx" title="sedogo : home"><img src="images/sedogo.gif" title="sedogo" alt="sedogo logo" id="logo" /></a>
			<p class="strapline">
				Create your future timeline.<br />
				Connect, track and interact with like minded people
			</p>
		</div>
		<div class="three-col">
			<label for="what" class="what">what are you going to do?</label>
			<asp:TextBox ID="what" runat="server" Text="e.g. climb Everest" MaxLength="1000" />
			<asp:ImageButton ID="searchButton" runat="server" OnClick="searchButton_click" 
			    ImageUrl="images/go.gif" ToolTip="go" CssClass="go" OnClientClick="return searchClick();" />
			<ol>
				<li>
					<input type="radio" name="aim" class="radio" id="find-people" checked="checked" /> <label for="find-people" class="radio-label">Find people to do this with</label>
				</li>
				<li>
					<input type="radio" name="aim" class="radio" id="add-to" disabled="disabled" /> <label for="add-to" class="radio-label">add to your todo list</label>
				</li>
			</ol>
			<p class="advanced-search"><a href="#" title="advanced search">advanced search</a></p>
		</div>
		<div id="timelines">
			<div id="tools">
				<ul class="timeline-options">
					<li class="first"><a href="#" title="View all" id="view-all">View all</a></li>
					<li class="last"><a href="#" title="Show categories" id="show-categories">Show categories</a></li>
				</ul>
				<div id="category-selector-container">
					<ul id="category-selector">
						<li class="category-btn-1"><a href="#" title="Personal">Personal</a></li>
						<li class="category-btn-2"><a href="#" title="Travel">Travel</a></li>
						<li class="category-btn-3"><a href="#" title="Friends">Friends</a></li>
						<li class="category-btn-4"><a href="#" title="Family">Family</a></li>
						<li class="category-btn-5"><a href="#" title="General">General</a></li>
						<li class="category-btn-6"><a href="#" title="Health">Health</a></li>
						<li class="category-btn-7"><a href="#" title="Money">Money</a></li>
						<li class="category-btn-8"><a href="#" title="Education">Education</a></li>
						<li class="category-btn-9"><a href="#" title="Hobbies">Hobbies</a></li>
						<li class="category-btn-10"><a href="#" title="Culture">Culture</a></li>
						<li class="category-btn-11"><a href="#" title="Charity">Charity</a></li>
						<li class="category-btn-12"><a href="#" title="Green">Green</a></li>
						<li class="category-btn-13"><a href="#" title="Misc">Misc</a></li>
					</ul>
				</div>
				<div id="buttons">
					<a href="#" title="Scroll left" class="left" id="scroll-back"><img src="images/left.gif" title="Scroll left" alt="Left arrow" /></a><a href="#" title="Scroll right" class="right" id="scroll-forward"><img src="images/right.gif" title="Scroll right" alt="Left arrow" /></a>
					&nbsp;&nbsp;
					<a href="#" title="Zoom in" class="plus" id="zoom-in"><img src="images/plus.gif" title="Zoom in" alt="Zoom in icon" /></a><a href="#" title="Zoom out" class="minus" id="zoom-out"><img src="images/minus.gif" title="Zoom out" alt="Zoom out icon" /></a>
				</div>
			</div>		
			<div class="tl-container">
				<div id="my-timeline" style="height: 360px"></div>
				<noscript>
					This page uses Javascript to show you a Timeline. Please enable Javascript in your browser to see the full page. Thank you.
				</noscript>
			</div>
		</div>
		<div id="other-content">
			<div class="one-col">
				<asp:Image ID="profileImage" runat="server" CssClass="profile" />
				<p class="profile-name"><asp:Label id="userNameLabel" runat="server" /><br />
				<a href="editProfile.aspx" title="Edit profile" class="modal">Edit profile</a><br />
				<a href="changePassword.aspx" title="Change password" class="modal">Change password</a><br />
				<a href="uploadProfilePic.aspx" title="Upload profile picture" class="modal">Upload profile picture</a></p>
				<p class="profile-intro">Lorem ipsum dolor sit amit consectetur adipiscing elit.</p>
				<div class="alerts">
					<h3>Messages</h3>
					<p><asp:HyperLink id="messageCountLink" runat="server" NavigateUrl="message.aspx" CssClass="modal" /></p>
					<p><asp:HyperLink id="inviteCountLink" NavigateUrl="invite.aspx" runat="server" CssClass="modal" /></p>
					<h3>Alerts</h3>
					<p><asp:HyperLink id="alertCountLink" NavigateUrl="alert.aspx" runat="server" CssClass="modal" /></p>
					<h3>Groups</h3>
					<p><asp:HyperLink id="groupCountLink" NavigateUrl="group.aspx" runat="server" CssClass="modal" /></p>
					<h3>Latest events added</h3>
					<h3>Latest searches</h3>
					<h3>Most popular searches</h3>
				</div>
			</div>
			<div class="one-col">
				<p class="extra-buttons">
				<a href="addEvent.aspx" title="add todo" class="button-sml modal">add todo</a>
				<asp:LinkButton ID="viewArchiveLink" runat="server" Text="view archive" CssClass="button-sml"
				    OnClick="click_viewArchiveLink" />
				</p>
				<div class="events">
					<h2>Overdue</h2>
					<asp:PlaceHolder ID="overdueEventsPlaceHolder" runat="server" />
					<h2>This month</h2>
					<p>Today <asp:Label ID="todaysDateLabel" runat="server" /></p>
					<asp:PlaceHolder ID="todayEventsPlaceHolder" runat="server" />
					<p>This week</p>
					<asp:PlaceHolder ID="thisWeekEventsPlaceHolder" runat="server" />
					<p>This month</p>
					<asp:PlaceHolder ID="thisMonthEventsPlaceHolder" runat="server" />
				</div>
			</div>
			<div class="one-col">
				<p class="extra-buttons"></p>
				<div class="events">
					<h2>Next 5 yrs</h2>
					<p>This year</p>
					<asp:PlaceHolder ID="nextYearEventsPlaceHolder" runat="server" />
					<p>Next 2 years</p>
					<asp:PlaceHolder ID="next2YearsEventsPlaceHolder" runat="server" />
					<p>Next 3 years</p>
					<asp:PlaceHolder ID="next3YearsEventsPlaceHolder" runat="server" />
					<p>Next 4 years</p>
					<asp:PlaceHolder ID="next4YearsEventsPlaceHolder" runat="server" />
					<p>Next 5 years</p>
					<asp:PlaceHolder ID="next5YearsEventsPlaceHolder" runat="server" />
				</div>
			</div>
			<div class="one-col-end">
				<p class="extra-buttons"></p>
				<div class="events">
					<h2>5 yrs +</h2>
					<p>5-10 years</p>
					<asp:PlaceHolder ID="next10YearsEventsPlaceHolder" runat="server" />
					<p>10-20 years</p>
					<asp:PlaceHolder ID="next20YearsEventsPlaceHolder" runat="server" />
					<p>20+ years</p>
					<asp:PlaceHolder ID="next100YearsEventsPlaceHolder" runat="server" />
					<h2>Not scheduled</h2>
					<asp:PlaceHolder ID="notScheduledEventsPlaceHolder" runat="server" />
				</div>
			</div>
		</div>
		<div id="footer">
			<ul>
				<li class="first">&copy; Sedogo</li>
				<li><a href="about.aspx" title="About" class="modal">About</a></li>
				<li><a href="faq.aspx" title="FAQ" class="modal">FAQ</a></li>
				<li class="last"><a href="feedback.aspx" title="Feedback" class="modal">Feedback</a></li>
			</ul>				
		</div>
	</div>
    <div id="modal-container">
        <iframe></iframe>
    </div>
    <div id="modal-background"></div>
    
    </div>
    </form>
</body>
</html>

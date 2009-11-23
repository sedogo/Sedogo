<%@ Page Language="C#" AutoEventWireup="true" CodeFile="search2.aspx.cs" Inherits="search2" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <script type="text/javascript" src="utils/validationFunctions.js"></script>

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
			var eventSourceSearch = new Timeline.DefaultEventSource();

			var bandInfos = [
				Timeline.createBandInfo({
					width: "5%"
				}),
				Timeline.createBandInfo({
					date: "<asp:Literal id="timelineStartDate1" runat="server" />",
					width: "38%",
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
					date: "<asp:Literal id="timelineStartDate2" runat="server" />",
					width: "7%",
					intervalUnit: Timeline.DateTime.YEAR,
					intervalPixels: 100,
					showEventText: false,
					trackHeight: 0.5,
					trackGap: 0.2,
					eventSource: eventSource,
					overview: true
				}),
				Timeline.createBandInfo({
					width: "5%"
				}),
				Timeline.createBandInfo({
					date: "<asp:Literal id="timelineStartDate3" runat="server" />",
					width: "38%",
					intervalUnit: Timeline.DateTime.MONTH,
					intervalPixels: 50,
					eventSource: eventSourceSearch
				}),
				Timeline.createBandInfo({
					date: "<asp:Literal id="timelineStartDate4" runat="server" />",
					width: "7%",
					intervalUnit: Timeline.DateTime.YEAR,
					intervalPixels: 100,
					showEventText: false,
					trackHeight: 0.5,
					trackGap: 0.2,
					eventSource: eventSourceSearch,
					overview: true
				})
			];
			bandInfos[2].syncWith = 1;
			bandInfos[4].syncWith = 1;
			bandInfos[5].syncWith = 1;
			bandInfos[2].highlight = true;
			bandInfos[5].highlight = true;

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
	    var form = document.forms[0];
        var searchString = form.what.value;
	    if( form.aim[1].checked == true )
	    {
	        doAddEvent(searchString);
	    }
	    else
	    {
	        if( isEmpty(searchString) || searchString.length < 3 )
	        {
	            alert("Please enter a longer search string");
	        }
	        else
	        {
	            location.href = "search2.aspx?Search=" + searchString;
	        }
        }
    }
    function openEvent(eventID)
    {
        openModal("viewEvent.aspx?EID=" + eventID);
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
			<a href="profile.aspx" title="sedogo : home"><img src="images/sedogo.gif" title="sedogo" alt="sedogo logo" id="logo" /></a>
			<p class="strapline">
			    Create your future and connect<br />
			    with others to make it happen
			</p>
		</div>
		<div class="three-col">
			<label for="what" class="what">what is your goal?</label>
			<asp:TextBox ID="what" runat="server" />
			<asp:ImageButton ID="searchButton" runat="server" OnClick="searchButton_click" 
			    ImageUrl="images/go.gif" ToolTip="go" CssClass="go" />
			<ol>
				<li>
					<input type="radio" name="aim" class="radio" id="find-people" checked="checked" /> <label for="find-people" class="radio-label">Find people to do this with</label>
				</li>
				<li>
					<input type="radio" name="aim" class="radio" id="add-to" /> <label for="add-to" class="radio-label">add to your goal list</label>
				</li>
			</ol>
		</div>
		<div id="noSearchResultsDiv" runat="server" class="errorMessage">
		    <p><b>&nbsp;<br />There were no results found, please try again or refine your search<br />&nbsp;</b></p>
		</div>
		<div id="moreThan50ResultsDiv" runat="server" class="errorMessage">
		    <p><b>&nbsp;<br />There were more than 50 matching results found, please refine your search<br />&nbsp;</b></p>
		</div>
		<div id="advSearchCriteria">
		    Refine your search:<br />
		    
            <label for="">Goal name</label>
            <asp:TextBox runat="server"
                ID="eventNameTextBox" Width="200px" MaxLength="200" /><br />

            <label for="">Where</label>
            <asp:TextBox runat="server"
                ID="venueTextBox" Width="200px" MaxLength="200" /><br />

            <label for="">Goal owner name</label>
            <asp:TextBox runat="server"
                ID="eventOwnerNameTextBox" Width="200px" MaxLength="200" />
                
            <asp:LinkButton 
                ID="advSearchButton" runat="server" Text="Search" 
                OnClick="advSearchButton_click" CssClass="button-sml" />
                
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
				<div id="buttons"></div>
			</div>		
			<div class="tl-container">
				<div id="my-timeline" style="height: 500px;"></div>
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
				<p class="profile-intro"><asp:Label ID="profileTextLabel" runat="server" /></p>
				<div class="alerts">
					<h3>Groups</h3>
					<p><asp:HyperLink id="groupCountLink" NavigateUrl="group.aspx" runat="server" CssClass="modal" /></p>
					<h3>Latest goals added</h3>
					<p><asp:PlaceHolder id="latestEventsPlaceholder" runat="server" /></p>
					<h3>Latest searches</h3>
					<p><asp:PlaceHolder id="latestSearchesPlaceholder" runat="server" /></p>
					<h3>Most popular searches</h3>
					<p><asp:PlaceHolder id="popularSearchesPlaceholder" runat="server" /></p>
				</div>
			</div>
			<div class="one-col">
				<p class="extra-buttons"></p>
				<div class="events">
					<h2>This month</h2>
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
				<p class="extra-buttons">
					<asp:HyperLink id="messageCountLink" runat="server" NavigateUrl="message.aspx" CssClass="modal" />
					&nbsp;
					<asp:HyperLink id="inviteCountLink" NavigateUrl="invite.aspx" runat="server" CssClass="modal" />
				</p>
				<div class="events">
					<h2>Next 5 yrs</h2>
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
				<p class="extra-buttons">
					<asp:HyperLink id="alertCountLink" NavigateUrl="alert.aspx" runat="server" CssClass="modal" />
					&nbsp;
					<asp:HyperLink id="trackingCountLink" NavigateUrl="tracking.aspx" runat="server" CssClass="modal" />
				</p>
				<div class="events">
					<h2>5 yrs +</h2>
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

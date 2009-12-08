<%@ Page Language="C#" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="profile" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-wstyle-type" content="text/css" />
	<meta http-equiv="expires" content="never" />

	<title>Home : Sedogo : Create your future and connect with others to make it happen</title>

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
			var bandInfos = [
				Timeline.createBandInfo({
					date: "<asp:Literal id="timelineStartDate1" runat="server" />",
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
					date: "<asp:Literal id="timelineStartDate2" runat="server" />",
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
	    var addString = form.what.value;
	    var searchString = form.what2.value;
	    if (!isEmpty(addString))
        {
            doAddEvent(addString);
        }
        else
        {
            if (isEmpty(searchString) || searchString.length < 3)
            {
                alert("Please enter a longer search string");
            }
            else
            {
                location.href = "search2.aspx?Search=" + searchString;
            }
        }
    }
    function doAddEvent(searchString)
    {
        openModal("addEvent.aspx?Name=" + searchString);
    }    
    function openEvent(eventID)
    {
        openModal("viewEvent.aspx?EID=" + eventID);
    }    
    function checkEnter(e)
    {
        var characterCode;
        if(e && e.which) // NN4 specific code
        {
            e = e;
            characterCode = e.which;
        }
        else
        {
            e = event;
            characterCode = e.keyCode; // IE specific code
        }
        if (characterCode == 13) //// Enter key is 13
        {
            e.returnValue=false;
            e.cancelBubble = true;
            searchClick();
            //document.getElementById(btn).click();
        }
        else
        {
            return false;
        }
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
			    Create your future and connect<br />with others to make it happen
			</p>
		</div>
		<div class="three-col">
		    <table border="0" cellspacing="10" cellpadding="0" width="100%" style="background-color:#EEEEEE">
		        <tr>
		            <td><h3 class="blue">Add</h3>
		            <p class="blue">to my goal list</p>
		            <asp:TextBox ID="what" runat="server" Text="" MaxLength="1000" /></td>
		            <td><h3 class="blue">Find</h3>
		            <p class="blue">people with your goals</p>
		            <asp:TextBox ID="what2" runat="server" Text="" MaxLength="1000" /></td>
		            <td valign="bottom"><a href="javascript:searchClick()"><img src="images/go.gif" alt="go" class="go" /></a></td>
		            <td valign="bottom"><p class="advanced-search"><a href="advSearch.aspx" title="advanced search" class="modal">advanced search</a></p></td>
		        </tr>    
		    </table>
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
				<div id="my-timeline" style="height: 360px;"></div>
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
					<h3>Latest goal's added</h3>
					<p><asp:PlaceHolder id="latestEventsPlaceholder" runat="server" /></p>
					<h3>Latest searches</h3>
					<p><asp:PlaceHolder id="latestSearchesPlaceholder" runat="server" /></p>
					<h3>Most popular searches</h3>
					<p><asp:PlaceHolder id="popularSearchesPlaceholder" runat="server" /></p>
				</div>
			</div>
			<div class="one-col">
				<p class="extra-buttons">
				    <a href="addEvent.aspx" title="add goal" class="button-sml modal">add goal</a>
				    <asp:LinkButton ID="viewArchiveLink" runat="server" Text="view archive" CssClass="button-sml"
				        OnClick="click_viewArchiveLink" />
				</p>
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
        <iframe frameborder="0"></iframe>
    </div>
    <div id="modal-background"></div>
    
    </div>
    </form>
</body>
</html>

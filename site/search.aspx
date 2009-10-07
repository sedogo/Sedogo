<%@ Page Language="C#" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="search" %>
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
    </script>
</head>
<body onload="breakout_of_frame()">
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
			<asp:TextBox ID="what" runat="server" />
			<asp:ImageButton ID="searchButton" runat="server" OnClick="searchButton_click" 
			    ImageUrl="images/go.gif" ToolTip="go" CssClass="go" />
			<ol>
				<li>
					<input type="radio" name="aim" class="radio" id="find-people" checked="checked" /> <label for="find-people" class="radio-label">Find people to do this with</label>
				</li>
				<li>
					<input type="radio" name="aim" class="radio" id="add-to" /> <label for="add-to" class="radio-label">add to your todo list</label>
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
				<ul class="tl-scale"></ul>
				<div class="x-axis-tracker"></div>
				<div class="row-master-container">
			        <asp:Literal id="timelineItems2" runat="server" />
				</div>
			</div>
		</div>
		<div id="other-content">

		    <p class="advanced-search"><a href="profile.aspx" title="return to profile">return to profile</a></p>
			
			<div class="three-col">
				<div class="events">
					<h2>Results</h2>
					<asp:PlaceHolder ID="searchResultsPlaceHolder" runat="server" />
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

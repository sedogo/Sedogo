<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
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
	    $(document).ready(function() {
			//Set widths and left positions of timelines based on info held in database (hard-coded for now)
			$("#1").css("width","300px").css("left","100px");
			$("#2").css("width","200px").css("left","20px");
			$("#3").css("width","420px").css("left","60px");
			$("#4").css("width","200px").css("left","200px");

			//Show selected on page load
			$(".category-2").css("display","block");
			$(".category-4").css("display","block");
			$(".category-12").css("display","block");
		});
	</script>
    <script language="JavaScript" type="text/javascript">
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
			<div class="two-col">
				<label for="what" class="what">what are you going to do?</label>
				<input type="text" value="e.g. climb Everest" id="what" />
				<ol>
					<li>
						<input type="radio" name="aim" class="radio" id="find-people" checked="checked" /> <label for="find-people" class="radio-label">Find people to do this with</label>
					</li>
					<li>
						<input type="radio" name="aim" class="radio" id="add-to" /> <label for="add-to" class="radio-label">add to your todo list</label>
					</li>
				</ol>
				<input type="image" src="images/go.gif" title="go" value="go" class="go" />
				<p class="advanced-search"><a href="#" title="advanced search">advanced search</a></p>
			</div>
			<div id="timelines">
				<div id="tools">
					<ul class="timeline-options">
						<li class="first"><a href="#" title="Max">Max</a></li>
						<li><a href="#" title="Min">Min</a></li>
						<li class="last"><a href="#" title="Off">Off</a></li>
					</ul>
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
						<div class="row-container category-2">
							<div class="row">
								<div class="tl" id="1">Camping with kids this summer</div>
							</div>
							<div class="row">
								<div class="tl" id="2">Camping with kids this summer</div>
							</div>			
						</div>
						<div class="row-container category-4">
							<div class="row">
								<div class="tl" id="3">Camping with kids this summer</div>
							</div>
						</div>
						<div class="row-container category-12">
							<div class="row">
								<div class="tl" id="4">Camping with kids this summer</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div id="other-content">
				<div class="one-col">
					&nbsp;
				</div>
				<div class="one-col">
					<h2>how does it work?</h2>
					<p class="teaser">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis lacus sed est tincidunt aliquet blandit nec orci.</p>
					<p><a href="tour.aspx" title="take a tour" class="button modal">take a tour</a></p>
				</div>
				<div class="one-col">
					<h2>join in today</h2>
					<p class="teaser">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis lacus sed est tincidunt aliquet blandit nec orci.</p>
				</div>
				<div class="one-col-end">
					<h2>get inspired</h2>
					<p class="teaser">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis lacus sed est tincidunt aliquet blandit nec orci.</p>
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

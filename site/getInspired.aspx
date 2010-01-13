﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getInspired.aspx.cs" Inherits="getInspired" %>
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
    <!--<script type="text/javascript" src="utils/validationFunctions.js"></script>-->

	<script type="text/javascript">
    function doAddEvent()
    {
        openModal("login.aspx");
    }
    function checkAddButtonEnter(e)
    {
        var characterCode;
        if (e && e.which) // NN4 specific code
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
            e.returnValue = false;
            e.cancelBubble = true;
            doAddEvent();
        }
        else
        {
            return false;
        }
    }
    </script>
</head>
<body>
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
		    <table border="0" cellspacing="10" cellpadding="0" width="100%" class="add-find">
		        <tr>
		            <td><h3 class="blue"><a href="javascript:doAddEvent()">Add</a></h3>
		                <p class="blue">to my goal list</p>
		                <asp:Panel ID="Panel1" runat="server" DefaultButton="searchButton1">
                        <asp:TextBox ID="what" runat="server" Text="" MaxLength="1000" />
		                <asp:ImageButton ID="searchButton1" runat="server" Enabled="false"
		                    ImageUrl="~/images/1x1trans.gif" />
                        </asp:Panel>
		            </td>
		            <td><h3 class="blue"><asp:LinkButton ID="findButton" runat="server" Text="Find" OnClick="searchButton_click" /></h3>
		                <p class="blue">people with my goals</p>
		                <asp:Panel ID="Panel2" DefaultButton="searchButton2" runat="server">
		                <asp:TextBox ID="what2" runat="server" Text="" MaxLength="1000" ValidationGroup="what2Group" />
                        <asp:RegularExpressionValidator
                            id="what2Validator"
                            runat="server"
                            ErrorMessage="Goal name must have at least 2 characters"
                            ControlToValidate="what2" ValidationGroup="what2Group"
                            ValidationExpression="[\S\s]{2,200}" />                        
		                <asp:ImageButton ID="searchButton2" runat="server" OnClick="searchButton_click" 
		                    ImageUrl="~/images/1x1trans.gif" />
		                </asp:Panel>
		            </td>
		        </tr>    
		    </table>
		</div>
			<div id="other-content">
				<div class="one-col">
					&nbsp;
				</div>
				<div class="one-col">
					<h2 class="col-header">Get inspired</h2>
					<p class="teaser">What have you often vowed to do before you die? Travel the world? 
                        Learn a language? Write that novel? Ghandi once said the future 
                        depends on what we do in the present.</p>
					<p><a href="register.aspx" title="get started" class="button modal">get started</a></p>
				</div>
				<div class="one-col">
					<h2 class="col-header">&nbsp;</h2>
					<p class="teaser">But it’s not always easy, is it? Life often gets in the way. Well, 
                        that's exactly why we launched Sedogo.  It's more than a diary of 
                        planned goals, Sedogo is a way of connecting with people who can be 
                        part of your future hopes and dreams, however big or small. Whether 
                        it’s bringing family and friends together to celebrate your next 
                        birthday, or meeting strangers who, a year from now, you'll be 
                        trusting with your life on the slopes of K2, sedogo really is the of 
                        the future of social networking. Your future.</p>
				</div>
				<div class="one-col-end">
					<h2 class="col-header">Goal suggestions</h2>
					<p class="teaser">Need some help getting started? Here are a few ideas that might grab 
                        you. Add as many as take your fancy. After all, you only get one life.<br />
                        &nbsp;<br />
                        Fly the double decker airbus A380<br />
                        Own an exotic pet<br />
                        Try zorbing<br />
                        Go on a Murder Mystery Weekend<br />
                        Form the ultimate Pub Quiz team<br />
                        Learn to ballroom waltz, Jane Austen style<br />
                        Go skinning dipping in a natural spa<br />
                        Experiment with a faddy diet<br />
                        Rent a remote cabin<br />
                        Sleep under the stars<br />
                        Watch a play in the West End every month</p>
				</div>
			</div>
			<div id="footer">
				<ul>
					<li class="first">&copy; Sedogo Ltd 2008-2010</li>
					<li><a href="about.aspx" title="About" class="modal">About</a></li>
					<li><a href="faq.aspx" title="FAQ" class="modal">FAQ</a></li>
				    <li><a href="privacy.aspx" title="Privacy Policy" class="modal">Privacy Policy</a></li>
					<li class="last"><a href="feedback.aspx" title="Feedback" class="modal">Feedback</a></li>
				</ul>
			</div>
		</div>
        <div id="modal-background"></div>
            
    </div>
    </form>

<script type="text/javascript">
    var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
    document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
</script>
<script type="text/javascript">
    try
    {
        var pageTracker = _gat._getTracker("UA-12373356-1");
        pageTracker._trackPageview();
    } catch (err) { }
</script>

</body>
</html>

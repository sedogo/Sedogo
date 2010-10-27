<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tour5.aspx.cs" Inherits="tour5" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="favicon.ico" >  
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="0" />
	<meta http-equiv="pragma" content="no-cache" />

	<title>Tour : Sedogo : Create your future and connect with others to make it happen</title>

	<meta name="keywords" content="" />
	<meta name="description" content="" />
	<meta name="author" content="" />
	<meta name="copyright" content="" />
	<meta name="robots" content="" />
	<meta name="MSSmartTagsPreventParsing" content="true" />

	<meta http-equiv="imagetoolbar" content="no" />
	<meta http-equiv="Cleartype" content="Cleartype" />

	<link rel="stylesheet" href="css/main.css" />
	<!--[if IE]>
		<link rel="stylesheet" href="css/main_ie.css" />
	<![endif]-->
	<!--[if lte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->

	<script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="js/jquery.livequery.js"></script>
	<script type="text/javascript" src="js/jquery.corner.js"></script>
	<script type="text/javascript" src="js/main.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
	    <div id="modal">
            <p><a href="tour.aspx"><b>1</b></a>&nbsp;&nbsp;<a href="tour2.aspx"><b>2</b></a>&nbsp;&nbsp;<a href="tour3.aspx"><b>3</b></a>&nbsp;&nbsp;<a href="tour4.aspx"><b>4</b></a>&nbsp;&nbsp;<b>5</b></p> 
            <div style="padding:10px 50px">
            <h2>Share pictures and videos of your goals<br />as you achieve them.</h2>
            <img src="images/Tour5image.jpg" />
            </div>
		</div>
    
        <div style="position:absolute;left:350px;top:370px;">
        <h2><a href="register.aspx" target="_top">Sign up now</a> <span class="grey">to get started</span></h2>
        </div>
        <div id="tourButtonLeft">
        <a href="tour4.aspx"><img src="images/buttonL.jpg" /></a>
        </div>
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

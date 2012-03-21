<%@ Page Language="C#" AutoEventWireup="true" CodeFile="advSearch.aspx.cs" Inherits="advSearch" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="favicon.ico" >  
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="expires" content="never" />

	<title>Advanced search : Sedogo : Create your future and connect with others to make it happen</title>

	<meta name="keywords" content="" />
	<meta name="description" content="" />
	<meta name="author" content="" />
	<meta name="copyright" content="" />
	<meta name="robots" content="" />
	<meta name="MSSmartTagsPreventParsing" content="true" />

	<meta http-equiv="imagetoolbar" content="no" />
	<meta http-equiv="Cleartype" content="Cleartype" />

	<link rel="stylesheet" href="css/main.css" />
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
    <script type="text/javascript" src="js/validationFunctions.js"></script>

	<script type="text/javascript">
		$(document).ready(function() {
		});
		function breakout_of_frame() {
			if (top.location != location) {
				top.location.href = document.location.href;
			}
		}
		function getElementID(name) {
			var form = document.forms[0];
			var nID = -1;
			for (i = 0; i < form.elements.length; i++) {
				if (form.elements[i].name == name) {
					nID = i;
				}
			}
			return nID;
		}
		function searchClick() {
			var form = document.forms[0];
			var searchString = form.what.value;
			if (form.aim[1].checked == true) {
				doAddEvent(searchString);
			}
			else {
				if (isEmpty(searchString) || searchString.length < 2) {
					alert("Please enter a longer search string");
				}
				else {
					location.href = "search2.aspx?Search=" + searchString;
				}
			}
		}
		function checkEnter(e) {
			var characterCode;
			if (e && e.which) // NN4 specific code
			{
				e = e;
				characterCode = e.which;
			}
			else {
				e = event;
				characterCode = e.keyCode; // IE specific code
			}
			if (characterCode == 13) //// Enter key is 13
			{
				e.returnValue = false;
				e.cancelBubble = true;
				searchClick();
				//document.getElementById(btn).click();
			}
			else {
				return false;
			}
		}
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
	    <div id="modal">
            <h1>Advanced search</h1>

            <fieldset>
                <ol class="width-constrain">
                    <li>
                        <label for="">Goal name</label>
                        <asp:TextBox runat="server"
                            ID="eventNameTextBox" Width="200px" MaxLength="200" />
                    </li>
                     <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">Where</label>
                        <asp:TextBox runat="server"
                            ID="venueTextBox" Width="200px" MaxLength="200" />
                    </li>
                     <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">Goal owner name</label>
                        <asp:TextBox runat="server"
                            ID="eventOwnerNameTextBox" Width="200px" MaxLength="200" />
                    </li>
                    <li>
                    </li>
                    <li>
                    </li>
                </ol>
            </fieldset>
		</div>

        <div class="buttons">
            <asp:LinkButton 
                ID="searchButton" runat="server" Text="Search" 
                OnClick="searchButton_click" CssClass="button-lrg" />
        </div>
    
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>
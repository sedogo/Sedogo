<%@ Page Language="C#" AutoEventWireup="true" CodeFile="feedback.aspx.cs" Inherits="feedback" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
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

	<title>Feedback : Sedogo : Create your future and connect with others to make it happen</title>

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

        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>    

	    <div id="container">
	        <Sedogo:BannerLoginControl ID="bannerLogin" runat="server" />
	        <Sedogo:BannerAddFindControl ID="bannerAddFindControl" runat="server" />

		    <div id="other-content">
			    <div class="three-col">

                    <h1>Feedback</h1>
                    <p>Comments? Suggestions? We'd love to hear them.</p>
                    <fieldset>
                        <ol>
                            <li>
                                <label for="">Your email address</label>
                                <asp:Label id="yourEmailAddressLabel" runat="server" />
                                <asp:TextBox ID="emailAddressTextBox" runat="server" Width="250" MaxLength="200" />
                                <asp:RequiredFieldValidator ID="emailAddressTextBoxValidator" runat="server"
                                ControlToValidate="emailAddressTextBox" ErrorMessage="Your email address is required" 
                                Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <br />
                            </li>
                            <li>
                                <label for="">Feedback</label>
                            </li>
                            <li>
                                <asp:TextBox ID="feedbackTextBox" runat="server" TextMode="MultiLine" 
                                    Width="250" Rows="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="feedbackTextBoxValidator" runat="server"
                                ControlToValidate="feedbackTextBox" ErrorMessage="Some feedback text is required" 
                                Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </li>
                        </ol>
                    </fieldset>

                    <div class="buttonsfullpage">
                        <asp:LinkButton ID="sendFeedbackButton" runat="server" Text="Send to Sedogo team"
                            OnClick="sendFeedbackButton_click" CssClass="button-lrg"></asp:LinkButton>
                    </div>

		        </div>

		    </div>
		    <Sedogo:FooterControl ID="footerControl" runat="server" />
	    </div>
    
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userProfile.aspx.cs" Inherits="userProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="favicon.ico" >  
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache"/>
	<meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>

	<title>View user profile : Sedogo : Create your future and connect with others to make it happen</title>

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
            <h1><asp:Label runat="server" ID="firstNameLabel" />'s profile</h1>

            <table width="100%">
                <tr>
                    <td>
                    
            <fieldset>
                <ol class="width-constrain">
                    <li>
                        <label for="">&nbsp;</label><br />
                        <asp:Label runat="server" ID="headlineLabel" />
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">Birthday</label><br />
                        <asp:Label runat="server" ID="birthdayLabel" />
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">Home town</label><br />
                        <asp:Label runat="server" ID="homeTownLabel" />
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <span class="blue"><asp:Label runat="server" ID="userProfilePopupGoalsLabel" /></span> Goals
                    </li>
                    <li>
                        <span class="blue"><asp:Label runat="server" ID="userProfilePopupGoalsAchievedLabel" /></span> Goals achieved
                    </li>
                    <li>
                        <span class="blue"><asp:Label runat="server" ID="userProfilePopupGroupGoalsLabel" /></span> Group goals
                    </li>
                    <li>
                       <span class="blue"> <asp:Label runat="server" ID="userProfilePopupGoalsFollowedLabel" /></span> Goals followed
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                </ol>
            </fieldset>

                    </td>
                    <td align="right">
                        <p style="display: block"><asp:Image ID="profileImage" runat="server" CssClass="profile" /></p>
                    </td>
                </tr>
            </table>
    
            <div class="buttons">
                <asp:LinkButton 
                    ID="sendMessageToUserLink" runat="server" ToolTip="save" Text="Send message" 
                    OnClick="sendMessageToUserLink_click" CssClass="button-lrg" />
            </div>

		</div>
    
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

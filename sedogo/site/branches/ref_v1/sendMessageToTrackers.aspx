<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sendMessageToTrackers.aspx.cs" Inherits="sendMessageToTrackers" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>

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

	<title>Send message to trackers : Sedogo : Create your future and connect with others to make it happen</title>

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
	<script type="text/javascript" src="js/jquery.corner.js"></script>
	<script type="text/javascript" src="js/main.js"></script>

<script language="JavaScript" type="text/javascript">
    function preSaveClick()
    {
        document.forms[0].target = "_top";
    }
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>    
    
        <div id="event-detail">
            <div class="right-col">
            </div>
            <div class="left-col">

                <h1>send message</h1>

                <p>Event name: <asp:Label ID="eventNameLabel" runat="server" /></p>

                <asp:PlaceHolder ID="trackingLinksPlaceholder" runat="server" />

                <p>Message<br />
                <asp:TextBox runat="server" TextMode="MultiLine" Rows="8"
                    ID="messageTextBox" Width="400px" />
                    <asp:RequiredFieldValidator ID="messageTextBoxValidator" runat="server"
                    ControlToValidate="messageTextBox" ErrorMessage="A message is required" Display="Dynamic">
                    </asp:RequiredFieldValidator></p>

                <telerik:RadCaptcha ID="registerCaptcha" runat="server"
                    ErrorMessage="The code entered is not valid"
                    CaptchaImage-EnableCaptchaAudio="false" Display="Dynamic"
                    CaptchaImage-TextChars="LettersAndNumbers"
                    CaptchaImage-TextColor="Green"
                >
                </telerik:RadCaptcha>

                <div class="buttons">
                <asp:LinkButton id="saveChangesButton" runat="server" OnClick="saveChangesButton_click" 
                    Text="Send" CssClass="button-lrg" OnClientClick="javascript:preSaveClick()" />
                </div>
                
		    </div>
		</div>
    
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

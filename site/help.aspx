<%@ Page Language="C#" AutoEventWireup="true" CodeFile="help.aspx.cs" Inherits="help" %>
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

	<title>Help : Sedogo : Create your future and connect with others to make it happen</title>

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
            <h1>Help</h1>
            <p>If you have any questions, just ask. We aim to reply to all queries within 48 hours.</p>
            
            <fieldset>
                <ol>
                    <li>
                        <label for="">Message</label>
                        <asp:TextBox ID="feedbackTextBox" runat="server" TextMode="MultiLine" 
                            Width="400" Rows="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="feedbackTextBoxValidator" runat="server"
                        ControlToValidate="feedbackTextBox" ErrorMessage="Some feedback text is required" 
                        Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </li>
                </ol>
            </fieldset>
            
		</div>
   
        <div class="buttons">
			<asp:LinkButton ID="sendFeedbackButton" runat="server" Text="Ask question" OnClick="sendFeedbackButton_click" CssClass="button-lrg"></asp:LinkButton>
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

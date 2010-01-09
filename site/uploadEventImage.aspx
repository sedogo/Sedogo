<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadEventImage.aspx.cs" Inherits="uploadEventImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="0" />
	<meta http-equiv="pragma" content="no-cache" />

	<title>Upload event image : Sedogo : Create your future and connect with others to make it happen</title>

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
	<!--[if gte IE 6]>
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

        <asp:ValidationSummary runat="server" ID="validationSummary" 
            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
            HeaderText="Please review the following errors:" />

	    <div id="modal">
            <h1>Upload picture for <asp:Label ID="eventNameLabel" runat="server" /></h1>

			<table class="summary">
				<tbody>
					<tr>
						<th>When:</th>
						<td><asp:Label ID="eventDateLabel" runat="server" /></td>
					</tr>
					<tr>
						<th>Where:</th>
						<td><asp:Label ID="eventVenueLabel" runat="server" /></td>
					</tr>
				</tbody>
			</table>

			<img src="images/popupLine.png" alt="" class="line-divider" />
            <br />
            <br />
            <fieldset>
                <ol>
                    <li>
                        <label for="">Select file</label>
                        <asp:FileUpload ID="eventPicFileUpload" runat="server" />
                        <asp:RequiredFieldValidator ID="eventPicFileUploadValidator" runat="server"
                            ControlToValidate="eventPicFileUpload" ErrorMessage="A file is required"
                            Display="None" SetFocusOnError="true">
                            </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <div class="buttons">
                        <asp:LinkButton id="uploadEventPicButton" runat="server" OnClick="uploadEventPicButton_click" 
                            Text="Upload picture" CssClass="button-lrg" />
                        <asp:LinkButton id="backButton" runat="server" OnClick="backButton_click" 
                            Text="Back to event details" CssClass="button-lrg" CausesValidation="false" />
                        </div>
                    </li>
                </ol>
            </fieldset>
		</div>
    
    </div>
    </form>

<script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://
ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-
analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
</script>
<script type="text/javascript">
    try
    {
        var pageTracker = _gat._getTracker("UA-12373356-1");
        pageTracker._trackPageview();
    } catch (err) { }</script>

</body>
</html>

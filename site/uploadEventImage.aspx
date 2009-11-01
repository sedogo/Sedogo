<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadEventImage.aspx.cs" Inherits="uploadEventImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">
	<meta http-equiv="pragma" content="no-cache">

	<title>Edit event title & details : Sedogo : Create your future timeline.  Connect, track and interact with like minded people.</title>

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
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <asp:ValidationSummary runat="server" ID="validationSummary" 
            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
            HeaderText="Please review the following errors:" />

	    <div id="modal">
            <h1>upload event picture</h1>
            <p>Text to be added</p>
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
                            Text="Upload picture" CssClass="button-sml" />
                        <asp:LinkButton id="backButton" runat="server" OnClick="backButton_click" 
                            Text="Back to event details" CssClass="button-sml" CausesValidation="false" />
                        </div>
                    </li>
                </ol>
            </fieldset>
		</div>
    
    </div>
    </form>
</body>
</html>

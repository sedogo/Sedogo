<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addEventUploadPic.aspx.cs" Inherits="addEventUploadPic" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache"/>
	<meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>

	<title>Create goal : Sedogo : Create your future and connect with others to make it happen</title>

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
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
        <asp:ValidationSummary runat="server" ID="validationSummary" 
            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
            HeaderText="Please review the following errors:" />
    
	    <div id="modal">
            <h1>Do you want to add a picture with <asp:Label ID="eventNameLabel" runat="server" /></h1>

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
                </ol>
            </fieldset>
		</div>
    
        <div class="buttons">
            <asp:LinkButton id="saveChangesButton" runat="server" OnClick="saveChangesButton_click" 
                Text="Add image" CssClass="button-sml" />
            <asp:LinkButton 
                ID="skipUploadButton" runat="server" ToolTip="Skip" Text="Skip" 
                OnClick="skipUploadButton_click" CssClass="button-sml" CausesValidation="false" />
        </div>

    </div>
    </form>
</body>
</html>

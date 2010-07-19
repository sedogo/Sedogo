<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addGoalPicture.aspx.cs" Inherits="addGoalPicture" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="favicon.ico" >  
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache"/>
	<meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>

	<title>Add goal picture : Sedogo : Create your future and connect with others to make it happen</title>

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

    <script type="text/javascript" language="javascript">
    function closeWindow()
    {
        window.parent.closeModal();
    } 
    </script>

</head>
<body>
    <form id="form1" runat="server" target="_top">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
        <asp:ValidationSummary runat="server" ID="validationSummary" 
            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
            HeaderText="Please review the following errors:" />
    
	    <div id="modal">
            <h1>Add a goal picture</h1>
            
            <table width="100%">
                <tr>
                    <td valign="top">
                    
            <fieldset style="padding-top:30px;">
                <ol>
                    <li>
                        <label for="">Select file</label>
                        <asp:FileUpload ID="goalPicFileUpload" runat="server" />
                        <asp:RequiredFieldValidator ID="goalPicFileUploadValidator" runat="server"
                            ControlToValidate="goalPicFileUpload" ErrorMessage="A file is required"
                            Display="None" SetFocusOnError="true">
                            </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="">Caption</label>
                        <asp:TextBox ID="captionTextBox" runat="server"></asp:TextBox>
                    </li>
                </ol>
            </fieldset>

                    </td>
                </tr>
            </table>

            <div class="buttons">
                <asp:LinkButton 
                    ID="uploadProfilePicButton" runat="server" ToolTip="Upload picture" Text="Upload picture" 
                    OnClick="uploadProfilePicButton_click" CssClass="button-sml" />
                <asp:Hyperlink 
                    ID="backButton" runat="server" ToolTip="Back to goal pictures" Text="Back to goal pictures" 
                    NavigateUrl="javascript:closeWindow()" CssClass="button-sml" CausesValidation="false" />
            </div>

		</div>
    
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

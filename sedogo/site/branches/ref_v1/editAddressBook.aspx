<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editAddressBook.aspx.cs" Inherits="editAddressBook" %>
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

	<title>Friends : Sedogo : Create your future and connect with others to make it happen</title>

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
    <form id="form2" runat="server" target="_top">
    <div>
    
        <asp:ValidationSummary runat="server" ID="validationSummary" 
            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
            HeaderText="Please review the following errors:" />
    
	    <div id="modal">
            <h1>Edit friend</h1>

            <table border="0" cellpadding="2" cellspacing="2">
                <tr>
                    <td>Name:</td>
                    <td>
                        <table border="0" cellpadding="2" cellspacing="0">
                            <tr>
                                <td><asp:TextBox runat="server" ID="firstNameTextBox" Width="100px" /> 
                                    <asp:RequiredFieldValidator ID="firstNameTextBoxValidator" runat="server"
                                    ControlToValidate="firstNameTextBox" ErrorMessage="A first name is required"
                                    Display="None" SetFocusOnError="true">
                                    </asp:RequiredFieldValidator></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox runat="server" ID="lastNameTextBox" Width="100px" />
                                    <asp:RequiredFieldValidator ID="lastNameTextBoxValidator" runat="server"
                                    ControlToValidate="lastNameTextBox" ErrorMessage="A last name is required"
                                    Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>Email address:</td>
                    <td><asp:TextBox runat="server" ID="emailAddress" Width="200px" />
                        <asp:RequiredFieldValidator ID="emailAddressValidator" runat="server"
                        ControlToValidate="emailAddress" ErrorMessage="An email address is required"
                        Display="None" SetFocusOnError="true">
                        </asp:RequiredFieldValidator></td>
                </tr>
            </table>

		</div>
    
        <div class="buttons">
            <asp:LinkButton 
                ID="saveButton" runat="server" ToolTip="Save" Text="Save" 
                OnClick="saveButton_Click" CssClass="button-lrg" />
        </div>    

    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

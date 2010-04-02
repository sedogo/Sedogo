<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changePassword.aspx.cs" Inherits="changePassword" %>
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

	<title>Change password : Sedogo : Create your future and connect with others to make it happen</title>

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
    <div class="Dialog">
    
	    <div id="modal">

            <h1>Change Password</h1>
	    
	        <div style="padding-top:30px;"></div>

    	    
            <p>Please enter your current password, then new password below</p>
            
            <table border="0" cellspacing="5" cellpadding="0" width="400">
                <tr>
                    <td colspan="2"><img src="images/popupLine.png" alt="" class="line-divider" /></td>
                </tr>
                <tr>
                    <td width="200">Current password</td>
                    <td width="200"><asp:TextBox runat="server"
                        ID="currentPasswordTextBox" Width="200px" TextMode="Password" MaxLength="30" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="currentPasswordTextBox" ErrorMessage="A password is required" Display="Dynamic">
                        </asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td width="200">Password</td>
                    <td width="200"><asp:TextBox runat="server"
                        ID="passwordTextBox1" Width="200px" TextMode="Password" MaxLength="30" />
                        <asp:RequiredFieldValidator ID="passwordTextBox1Validator" runat="server"
                        ControlToValidate="passwordTextBox1" ErrorMessage="A password is required" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="passwordTextBox1Validator2" runat="server"
                        ControlToValidate="passwordTextBox1" Display="Dynamic"
                        ErrorMessage="Password must be over 6 characters long." 
                        ValidationExpression="[^\s]{6,30}" /></td>
                </tr>
                <tr>
                    <td width="200">Re-type password</td>
                    <td width="200"><asp:TextBox runat="server"
                        ID="passwordTextBox2" Width="200px" TextMode="Password" MaxLength="30" />
                        <asp:RequiredFieldValidator ID="passwordTextBox2Validator" runat="server"
                        ControlToValidate="passwordTextBox1" ErrorMessage="Please verify your password" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator id="passwordTextBox2Validator2" runat="server" 
                        ErrorMessage="The two passwords do not match" 
                        ControlToValidate="passwordTextBox1" Display="Dynamic" 
                        ControlToCompare="passwordTextBox2"></asp:CompareValidator></td>
                </tr>
                <tr>
                    <td colspan="2"><img src="images/popupLine.png" alt="" class="line-divider" /></td>
                </tr>
            </table>
                        
		</div>
    
        <div class="buttons">
            <asp:LinkButton 
                ID="saveChangesButton" runat="server" ToolTip="save" Text="Save" 
                OnClick="saveChangesButton_click" CssClass="button-lrg" />
        </div>    
    
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

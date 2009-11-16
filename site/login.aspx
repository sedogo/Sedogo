<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
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

	<title>Log in : Sedogo : Create your future and connect with others to make it happen</title>

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
            <h1>log-in</h1>
            <p>Enter your email address and password below to log in to your Sedogo account</p>
            <fieldset>
                <ol>
                    <li>
                        <label for="">Email address</label>
                        <asp:TextBox runat="server" ID="emailAddress" Width="200px" />
                        <asp:RequiredFieldValidator ID="emailAddressValidator" runat="server"
                            ControlToValidate="emailAddress" ErrorMessage="An email address is required"
                            Display="None" SetFocusOnError="true">
                            </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="">Password</label>
                        <asp:TextBox runat="server" ID="userPassword" TextMode="password" Width="200px" />
	                    <asp:RequiredFieldValidator ID="userPasswordValidator" runat="server"
                            ControlToValidate="userPassword" ErrorMessage="A password is required"
                            Display="None" SetFocusOnError="true">
                            </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="">Remember me</label>
                        <asp:CheckBox ID="rememberMeCheckbox" runat="server" />
                    </li>
                </ol>
            </fieldset>
		</div>
    
        <div class="buttons">
            <asp:LinkButton 
                ID="loginButton" runat="server" ToolTip="Login" Text="Login" 
                OnClick="loginButton_Click" CssClass="button-sml" />
            <asp:LinkButton ID="forgotPasswordButton" runat="server" Text="forgotten password?" 
                OnClick="forgotPasswordButton_click"
                CausesValidation="false" CssClass="button-sml" />
        </div>
    
        <p>&nbsp;<br />If you don't have a Sedogo account and wish to 
            register, <a href="register.aspx">please click here</a></p>
    
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
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
    
        <asp:ValidationSummary runat="server" ID="validationSummary" 
            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
            HeaderText="Please review the following errors:" />
    
	    <div id="modal">
            <h1>Log in</h1>
            <p>Enter your email address and password below to log in to your sedogo account</p>
             <br />
           <fieldset>
                <ol class="width-constrain">
                    <li>
                        <label for="">Email address</label>
                        <asp:TextBox runat="server" ID="emailAddress" Width="200px" />
                        <asp:RequiredFieldValidator ID="emailAddressValidator" runat="server"
                            ControlToValidate="emailAddress" ErrorMessage="An email address is required"
                            Display="None" SetFocusOnError="true">
                            </asp:RequiredFieldValidator>
                    </li>
                     <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">Password</label>
                        <asp:TextBox runat="server" ID="userPassword" TextMode="password" Width="200px" />
	                    <asp:RequiredFieldValidator ID="userPasswordValidator" runat="server"
                            ControlToValidate="userPassword" ErrorMessage="A password is required"
                            Display="None" SetFocusOnError="true">
                            </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="">&nbsp;</label><p>passwords are case sensitive</p>
                    </li>
                     <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">Remember me</label>
                        <asp:CheckBox ID="rememberMeCheckbox" runat="server" />
                    </li>
                </ol>
            </fieldset>
		</div>
    
        <div class="buttons">
            <asp:LinkButton 
                ID="loginButton" runat="server" ToolTip="Log in" Text="Log in" 
                OnClick="loginButton_Click" CssClass="button-lrg" />
        </div>
    
        <p><br />If you don't have a Sedogo account and wish to 
            register, <a href="register.aspx" target="_top">please click here</a></p>
            <br />
		<p><asp:LinkButton ID="forgotPasswordButton" runat="server" Text="Forgotten password?" 
                OnClick="forgotPasswordButton_click"
                CausesValidation="false" CssClass="underline-bold" /></p>
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

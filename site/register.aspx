<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
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

	<title>Sign up : Sedogo : Create your future and connect with others to make it happen</title>

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
	<script type="text/javascript" src="js/jquery-ui-1.7.2.custom.min.js"></script>
	<script type="text/javascript" src="js/ui.dialog.js"></script>
	<script type="text/javascript" src="js/jquery.cookie.js"></script>
	<script type="text/javascript" src="js/jquery.livequery.js"></script>
	<script type="text/javascript" src="js/jquery.corner.js"></script>
	<script type="text/javascript" src="js/main.js"></script>
    <script type="text/javascript" src="utils/validationFunctions.js"></script>

<script language="JavaScript" type="text/javascript">
function setHiddenDateField()
{
	var form = document.forms[0];
	var d = form.dateOfBirthDay.options[form.dateOfBirthDay.selectedIndex].value;
	var m = form.dateOfBirthMonth.options[form.dateOfBirthMonth.selectedIndex].value;
	var y = form.dateOfBirthYear.options[form.dateOfBirthYear.selectedIndex].value;
	
	if( d == "" && m == "" && y == "" )
	{
        form.hiddenDateOfBirth.value = "";
	}
	else
	{
        form.hiddenDateOfBirth.value = <asp:Literal id="dateString1" runat="server" />;
    }
}
function termsClientValidation(source, args)
{
    var control = document.getElementById("termsCheckbox");
    args.IsValid = control.checked;
} 
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>    
    
    <div>
        <div id="container">
	    <Sedogo:BannerLoginControl ID="bannerLogin" runat="server" />
        <Sedogo:BannerAddFindControl ID="bannerAddFindControl" runat="server" />
    
	    <div id="other-content">
		    <div class="three-col">

            <asp:ValidationSummary runat="server" ID="validationSummary" 
                ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
                HeaderText="Please review the following errors:" />
        
            <h1 style="margin-bottom: 2px">Sign-up to Sedogo</h1>
            <p>Already a Sedogo member? <a href="login.aspx" title="Log in here" class="modal">Log in here</a></p>
            <h3 class="blue" style="font-size: 14px">Enter your details below to start using Sedogo today</h3>
            <fieldset>
                <ol class="width-constrain">
                     <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">First name</label>
                        <asp:TextBox runat="server"
                            ID="firstNameTextBox" Width="200px" MaxLength="200" />
                            <asp:RequiredFieldValidator ID="firstNameTextBoxValidator" runat="server"
                            ControlToValidate="firstNameTextBox" ErrorMessage="A first name is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                    </li>
                     <div class="pinstripe-divider">&nbsp;</div>
                   <li>
                        <label for="">Surname</label>
                        <asp:TextBox runat="server"
                            ID="lastNameTextBox" Width="200px" MaxLength="200" />
                            <asp:RequiredFieldValidator ID="lastNameTextBoxValidator" runat="server"
                            ControlToValidate="lastNameTextBox" ErrorMessage="A last name is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">Avatar image</label><br />
                        <telerik:RadComboBox ID="avatarComboBox" CssClass="ComboBox" runat="server" 
                            Height="200px" Width="200px">
                            </telerik:RadComboBox>
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">Date of birth</label>
                        <asp:DropDownList ID="dateOfBirthDay" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="dateOfBirthMonth" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="dateOfBirthYear" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="hiddenDateOfBirth" runat="server" />
                        <asp:RequiredFieldValidator ID="dateOfBirthValidator2" runat="server"
                            ControlToValidate="hiddenDateOfBirth" ErrorMessage="A date of birth is required" 
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="dateOfBirthValidator" runat="server"
                           ControlToValidate="hiddenDateOfBirth" ErrorMessage="Select a valid date"
                           Operator="DataTypeCheck" Type="Date" />
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">Gender</label>
                        <asp:RadioButton ID="genderMaleRadioButton" GroupName="gender" runat="server" Checked="true" /> 
                        Male &nbsp;&nbsp;&nbsp; <asp:RadioButton ID="genderFemaleRadioButton" GroupName="gender" runat="server" /> Female
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">Home town</label>
                        <asp:TextBox runat="server"
                            ID="homeTownTextBox" Width="200px" MaxLength="200" />
                            <asp:RequiredFieldValidator ID="homeTownTextBoxValidator" runat="server"
                            ControlToValidate="homeTownTextBox" ErrorMessage="A home town is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">Timezone</label>
                        <asp:DropDownList ID="timezoneDropDownList" runat="server"
                            DataTextField="Description" DataValueField="TimezoneID">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="timezoneRequiredFieldValidator" runat="server"
                        ControlToValidate="timezoneDropDownList" ErrorMessage="A timezone is required" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">Email address</label>
                        <asp:TextBox runat="server"
                            ID="emailAddressTextBox" Width="200px" MaxLength="200" />
                            <asp:RequiredFieldValidator ID="emailAddressTextBoxValidator" runat="server"
                            ControlToValidate="emailAddressTextBox" ErrorMessage="An email address is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator id="emailAddressTextBoxValidator2" 
                            runat="server" ControlToValidate="emailAddressTextBox" 
                            ErrorMessage="The email address is not valid" Display="Dynamic"
                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for="">Password</label>
                        <asp:TextBox runat="server"
                            ID="passwordTextBox1" Width="200px" TextMode="Password" MaxLength="30" />
                            <asp:RequiredFieldValidator ID="passwordTextBox1Validator" runat="server"
                            ControlToValidate="passwordTextBox1" ErrorMessage="A password is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="passwordTextBox1Validator2" runat="server"
                            ControlToValidate="passwordTextBox1" Display="Dynamic"
                            ErrorMessage="Password must be over 6 characters long." 
                            ValidationExpression="[^\s]{6,30}" />
                    </li>
                    <li>
                        <label for="">Re-type password</label>
                        <asp:TextBox runat="server"
                            ID="passwordTextBox2" Width="200px" TextMode="Password" MaxLength="30" />
                            <asp:RequiredFieldValidator ID="passwordTextBox2Validator" runat="server"
                            ControlToValidate="passwordTextBox1" ErrorMessage="Please verify your password" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator id="passwordTextBox2Validator2" runat="server" 
                            ErrorMessage="The two passwords do not match" 
                            ControlToValidate="passwordTextBox1" Display="Dynamic" 
                            ControlToCompare="passwordTextBox2"></asp:CompareValidator>
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <telerik:RadCaptcha ID="registerCaptcha" runat="server"
                            ErrorMessage="The code entered is not valid"
                            CaptchaImage-EnableCaptchaAudio="false" Display="Dynamic"
                            CaptchaImage-TextChars="LettersAndNumbers"
                            CaptchaImage-TextColor="Green"
                        >
                        </telerik:RadCaptcha>
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                    <li>
                        <label for=""><asp:HyperLink ID="termsLink" NavigateUrl="terms.aspx" CssClass="modal" 
                            Text="Terms and conditions" ToolTip="Terms and conditions" runat="server" /></label>
                        <asp:Checkbox runat="server" ID="termsCheckbox"  />
                        <asp:CustomValidator runat="server" ErrorMessage="You must accept the terms and conditions" 
                            ClientValidationFunction="termsClientValidation" ID="termsCheckboxValidator" /> 
                    </li>
                </ol>
            </fieldset>

            <div class="buttonsfullpage">
                <asp:LinkButton 
                    ID="registerUserButton" runat="server" Text="Sign Up" 
                    OnClick="registerUserButton_click" CssClass="button-lrg" />
            </div>

    		</div>
		</div>
        <div id="modal-container">
			<a href="#" class="close-modal"><img src="images/close-modal.gif" title="Close window" alt="Close window" /></a>
            <iframe frameborder="0"></iframe>
        </div>
        <div id="modal-background"></div>
    
    </div>
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

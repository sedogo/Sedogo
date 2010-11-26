<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editProfile.aspx.cs" Inherits="editProfile" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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

	<title>Edit profile : Sedogo : Create your future and connect with others to make it happen</title>

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
    <script type="text/javascript" src="js/validationFunctions.js"></script>
	
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
        form.hiddenDateOfBirth.value = d + "/" + m + "/" + y;
    }
}
</script>
	
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>    
    
	    <div id="container">
	        <Sedogo:BannerLoginControl ID="bannerLogin" runat="server" />
	        <Sedogo:BannerAddFindControl ID="bannerAddFindControl" runat="server" />

		    <div id="other-content">
                <Sedogo:SidebarControl ID="sidebarControl" runat="server" />

			    <div class="three-col">

                    <div class="page-banner-content">
                        <div class="page-banner-header">Change your profile</div>
                        <div class="page-banner-backbutton"><asp:LinkButton id="backButton" runat="server" Text="Home" 
                            CssClass="page-banner-linkstyle" OnClick="backButton_click" CausesValidation="false" /></div>
                    </div>

                    <p>Review and change your details below then submit to save</p>

                    <asp:ValidationSummary runat="server" ID="validationSummary" 
                        ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
                        HeaderText="Please review the following errors:" />

                    <table width="100%">
                        <tr>
                            <td>
                            
                                <fieldset>
                                    <ol class="width-constrain">
                                        <li>
                                            <label for="">First name</label><br />
                                            <asp:TextBox runat="server"
                                                ID="firstNameTextBox" Width="200px" MaxLength="200" />
                                                <asp:RequiredFieldValidator ID="firstNameTextBoxValidator" runat="server"
                                                ControlToValidate="firstNameTextBox" ErrorMessage="A first name is required" Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                        </li>
                                        <div class="pinstripe-divider">&nbsp;</div>
                                        <li>
                                            <label for="">Surname</label><br />
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
                                            <label for="">Introduction</label><br />
                                            <asp:TextBox runat="server" TextMode="MultiLine" Rows="4"
                                                ID="headlineTextBox" Width="200px" MaxLength="200" />
                                        </li>
                                        <div class="pinstripe-divider">&nbsp;</div>
                                        <li>
                                            <label for="">Date of birth</label><br />
                                            <asp:DropDownList ID="dateOfBirthDay" runat="server">
                                            </asp:DropDownList><asp:DropDownList ID="dateOfBirthMonth" runat="server">
                                            </asp:DropDownList><asp:DropDownList ID="dateOfBirthYear" runat="server">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="hiddenDateOfBirth" runat="server" />
                                            <asp:CompareValidator ID="dateOfBirthValidator" runat="server"
                                               ControlToValidate="hiddenDateOfBirth" ErrorMessage="Select a valid date"
                                               Operator="DataTypeCheck" Type="Date" />
                                        </li>
                                        <div class="pinstripe-divider">&nbsp;</div>
                                        <li>
                                            <label for="" style="width: 80px">Gender</label>
                                            <asp:RadioButton ID="genderMaleRadioButton" GroupName="gender" runat="server" Checked="true" /> 
                                            Male &nbsp;&nbsp;&nbsp; <asp:RadioButton ID="genderFemaleRadioButton" GroupName="gender" runat="server" /> Female
                                        </li>
                                        <div class="pinstripe-divider">&nbsp;</div>
                                        <li>
                                            <label for="">Home town</label><br />
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
                                            <label for="">Email address</label><br />
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
                                            <label for="">Enable email notifications</label><br />
                                            <asp:CheckBox runat="server" ID="enableEmailCheckbox" />
                                            <p>Enable email notifications of invitations to goals, updates and alerts about goals you've joined.......</p>
                                        </li>
                                        <div class="pinstripe-divider">&nbsp;</div>
                                    </ol>
                                </fieldset>

                            </td>
                            <td align="right">
                                <p style="display: block"><asp:Image ID="profileImage" runat="server" CssClass="profile" /></p>
                                <p style="display: block; clear: both; margin-top:10px">
                                <asp:Hyperlink 
                                    ID="uploadProfilePicButton" runat="server" 
                                    ToolTip="save" Text="Upload profile picture" 
                                    NavigateUrl="~/uploadProfilePic.aspx"
                                    CssClass="button-sml modal" />
                                <p style="display: block; clear: both; margin-top:10px">
                                <asp:Hyperlink 
                                    ID="changePasswordButton" runat="server" ToolTip="save" Text="Change password" 
                                    NavigateUrl="~/changePassword.aspx" CssClass="button-sml modal" />
                            </p>
                            </td>
                        </tr>
                    </table>
            
                    <div class="buttonsfullpage">
                        <asp:LinkButton 
                            ID="saveChangesButton" runat="server" ToolTip="save" Text="Save" 
                            OnClick="saveChangesButton_click" CssClass="button-lrg" />
                    </div>

		        </div>
    			
		    </div>
		    <Sedogo:FooterControl ID="footerControl" runat="server" />
	    </div>
        <div id="modal-container">
			<a href="#" class="close-modal"><img src="images/close-modal.gif" title="Close window" alt="Close window" /></a>
            <iframe frameborder="0"></iframe>
        </div>
        <div id="modal-background"></div>
    
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editProfile.aspx.cs" Inherits="editProfile" %>
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
	<!--[if gte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->
	
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
    <form id="form1" runat="server" target="_top">
    <div>
    
	    <div id="modal">
            <h1>edit profile</h1>
            <p>Review and change your details below then submit to save</p>
            <fieldset>
                <ol>
                    <li>
                        <label for="">First name</label>
                        <asp:TextBox runat="server"
                            ID="firstNameTextBox" Width="200px" MaxLength="200" />
                            <asp:RequiredFieldValidator ID="firstNameTextBoxValidator" runat="server"
                            ControlToValidate="firstNameTextBox" ErrorMessage="A first name is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="">Surname</label>
                        <asp:TextBox runat="server"
                            ID="lastNameTextBox" Width="200px" MaxLength="200" />
                            <asp:RequiredFieldValidator ID="lastNameTextBoxValidator" runat="server"
                            ControlToValidate="lastNameTextBox" ErrorMessage="A last name is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="">Introduction</label>
                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="4"
                            ID="headlineTextBox" Width="200px" MaxLength="200" />
                    </li>
                    <li>
                        <label for="">Date of birth</label>
                        <asp:DropDownList ID="dateOfBirthDay" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="dateOfBirthMonth" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="dateOfBirthYear" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="hiddenDateOfBirth" runat="server" />
                        <asp:CompareValidator ID="dateOfBirthValidator" runat="server"
                           ControlToValidate="hiddenDateOfBirth" ErrorMessage="Select a valid date"
                           Operator="DataTypeCheck" Type="Date" />
                    </li>
                    <li>
                        <label for="">Gender</label>
                        <asp:RadioButton ID="genderMaleRadioButton" GroupName="gender" runat="server" Checked="true" /> 
                        Male &nbsp;&nbsp;&nbsp; <asp:RadioButton ID="genderFemaleRadioButton" GroupName="gender" runat="server" /> Female
                    </li>
                    <li>
                        <label for="">Home town</label>
                        <asp:TextBox runat="server"
                            ID="homeTownTextBox" Width="200px" MaxLength="200" />
                            <asp:RequiredFieldValidator ID="homeTownTextBoxValidator" runat="server"
                            ControlToValidate="homeTownTextBox" ErrorMessage="A home town is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="">Timezone</label>
                        <asp:DropDownList ID="timezoneDropDownList" runat="server"
                            DataTextField="Description" DataValueField="TimezoneID">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="timezoneRequiredFieldValidator" runat="server"
                        ControlToValidate="timezoneDropDownList" ErrorMessage="A timezone is required" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </li>
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
                    <li>
                        <label for="">Confirm email address</label>
                        <asp:TextBox runat="server"
                            ID="confirmEmailAddressTextBox" Width="200px" MaxLength="200" />
                            <asp:RequiredFieldValidator ID="confirmEmailAddressTextBoxValidator" runat="server"
                            ControlToValidate="confirmEmailAddressTextBox" ErrorMessage="Please confirm the email address" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator id="confirmEmailAddressTextBoxValidator2" 
                            runat="server" ControlToValidate="confirmEmailAddressTextBox" 
                            ControlToCompare="emailAddressTextBox"
                            ErrorMessage="The two email addresses do not match" Display="Dynamic"
                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:CompareValidator>
                    </li>
                </ol>
            </fieldset>
		</div>
    
        <div class="buttons">
            <asp:LinkButton 
                ID="saveChangesButton" runat="server" ToolTip="save" Text="Save" 
                OnClick="saveChangesButton_click" CssClass="button-sml" />
        </div>
    
    </div>
    </form>
</body>
</html>

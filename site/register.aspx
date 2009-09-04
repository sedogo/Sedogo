<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">
	<meta http-equiv="pragma" content="no-cache">

	<title>Sign up : Sedogo : Create your future timeline.  Connect, track and interact with like minded people.</title>

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
    
	    <div id="modal">
            <h1>sign-up</h1>
            <p>Enter your details below to start using Sedogo immediately</p>
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
                        <label for="">Date of birth</label>
                        <asp:TextBox runat="server"
                            ID="dateOfBirthTextBox" Width="200px" MaxLength="200" />
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
                    <li>
                        <label for=""></label>
                        <asp:Button id="registerUserButton" runat="server" OnClick="registerUserButton_click" Text="Register" />
                    </li>
                </ol>
            </fieldset>
		</div>
    
    </div>
    </form>
</body>
</html>

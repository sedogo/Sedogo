<%@ Page Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" 
    CodeFile="editUser.aspx.cs" Inherits="admin_editUser" Theme="AdminTheme" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ValidationSummary runat="server" ID="validationSummary" 
        ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
        HeaderText="Please review the following errors:" />

    Name: <asp:TextBox runat="server" ID="firstNameTextBox" Width="200px" /> 
        <asp:RequiredFieldValidator ID="firstNameTextBoxValidator" runat="server"
            ControlToValidate="firstNameTextBox" ErrorMessage="A first name address is required"
            Display="None" SetFocusOnError="true">
            </asp:RequiredFieldValidator> <asp:TextBox runat="server" ID="lastNameTextBox" Width="200px" />
            <asp:RequiredFieldValidator ID="lastNameTextBoxValidator" runat="server"
            ControlToValidate="lastNameTextBox" ErrorMessage="A last name address is required"
            Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator><br />
    Email address: <asp:TextBox runat="server" ID="emailAddress" Width="200px" />
        <asp:RequiredFieldValidator ID="emailAddressValidator" runat="server"
            ControlToValidate="emailAddress" ErrorMessage="An email address is required"
            Display="None" SetFocusOnError="true">
            </asp:RequiredFieldValidator><br />
    Password: <asp:TextBox runat="server" ID="userPassword" TextMode="password" Width="200px" />
		<asp:RequiredFieldValidator ID="userPasswordValidator" runat="server"
            ControlToValidate="userPassword" ErrorMessage="A password is required"
            Display="None" SetFocusOnError="true">
            </asp:RequiredFieldValidator><br />
            
    <asp:Button runat="server" ID="saveButton" text="Save" OnClick="saveButton_Click" />
    <asp:Button runat="server" ID="cancelButton" text="Back" OnClick="cancelButton_Click"
        CausesValidation="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button runat="server" ID="deleteButton" text="Delete" OnClick="deleteButton_Click"
        CausesValidation="false" />


</asp:Content>


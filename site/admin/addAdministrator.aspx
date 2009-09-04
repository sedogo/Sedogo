<%@ Page Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" 
    CodeFile="addAdministrator.aspx.cs" Inherits="admin_addAdministrator" Theme="AdminTheme" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ValidationSummary runat="server" ID="validationSummary" 
        ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
        HeaderText="Please review the following errors:" />

    Name: <asp:TextBox runat="server" ID="nameTextBox" Width="200px" />
        <asp:RequiredFieldValidator ID="nameTextBoxValidator" runat="server"
            ControlToValidate="nameTextBox" ErrorMessage="A name address is required"
            Display="None" SetFocusOnError="true">
            </asp:RequiredFieldValidator><br />
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
        CausesValidation="false" /><br />

</asp:Content>


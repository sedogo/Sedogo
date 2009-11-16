<%@ Page Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" 
    CodeFile="addTimezone.aspx.cs" Inherits="admin_addTimezone" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ValidationSummary runat="server" ID="validationSummary" 
        ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
        HeaderText="Please review the following errors:" />

    Code: <asp:TextBox runat="server" ID="shortCodeTextBox" Width="200px" /> 
        <asp:RequiredFieldValidator ID="shortCodeTextBoxValidator" runat="server"
            ControlToValidate="shortCodeTextBox" ErrorMessage="A short code is required"
            Display="None" SetFocusOnError="true">
            </asp:RequiredFieldValidator><br />
    Description: <asp:TextBox runat="server" ID="descriptionTextBox" Width="200px" />
        <asp:RequiredFieldValidator ID="descriptionTextBoxValidator" runat="server"
            ControlToValidate="descriptionTextBox" ErrorMessage="A description is required"
            Display="None" SetFocusOnError="true">
            </asp:RequiredFieldValidator><br />
    Offset: <asp:TextBox runat="server" ID="offsetTextBox" Width="200px" />
		<asp:RequiredFieldValidator ID="offsetTextBoxValidator" runat="server"
            ControlToValidate="offsetTextBox" ErrorMessage="An offset is required"
            Display="None" SetFocusOnError="true">
            </asp:RequiredFieldValidator><br />
            
    <asp:Button runat="server" ID="saveButton" text="Save" OnClick="saveButton_Click" />
    <asp:Button runat="server" ID="cancelButton" text="Back" OnClick="cancelButton_Click"
        CausesValidation="false" /><br />

</asp:Content>


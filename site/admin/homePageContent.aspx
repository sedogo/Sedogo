<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" 
    CodeFile="homePageContent.aspx.cs" Inherits="admin_homePageContent" ValidateRequest="false" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:TextBox runat="server" ID="homePageContentTextBox" Width="600px" Rows="25" TextMode="MultiLine" /><br />

    <asp:Button runat="server" ID="saveButton" text="Save" OnClick="saveButton_Click" />
    <asp:Button runat="server" ID="cancelButton" text="Back" OnClick="cancelButton_Click"
        CausesValidation="false" /><br />

</asp:Content>


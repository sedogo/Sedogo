<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" 
    CodeFile="broadcastEmailContent.aspx.cs" Inherits="admin_broadcastEmailContent" ValidateRequest="false" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <p>Send email to ALL sedogo users</p>
    <p>Message below will be formatted into a standard Sedogo style email</p>
    
    Subject:<br /><asp:TextBox runat="server" ID="emailSubjectTextBox" Width="600px" /><br />&nbsp;<br />
    Message:<br /><asp:TextBox runat="server" ID="emailContentTextBox" Width="600px" Rows="10" TextMode="MultiLine" /><br />

    <asp:Button runat="server" ID="saveButton" text="Send email to all users" OnClick="saveButton_Click" />
    <asp:Button runat="server" ID="cancelButton" text="Back" OnClick="cancelButton_Click"
        CausesValidation="false" /><br />
    <br />
    Send test mail to: <asp:TextBox ID="testEmailAddressTextBox" runat="server" Width="300px" />
    <asp:Button runat="server" ID="sendTestEmailButton" text="Send test email" OnClick="sendTestEmailButton_Click" />
</asp:Content>


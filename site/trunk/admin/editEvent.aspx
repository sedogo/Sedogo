<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" 
    CodeFile="editEvent.aspx.cs" Inherits="admin_editEvent" Theme="AdminTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    Name: <asp:Label runat="server" ID="eventNameLabel" /><br />
    Show on default page: <asp:Checkbox runat="server" ID="showOnDefaultPageCheckbox" /><br />
            
    <asp:Button runat="server" ID="saveButton" text="Save" OnClick="saveButton_Click" />
    <asp:Button runat="server" ID="cancelButton" text="Back" OnClick="cancelButton_Click"
        CausesValidation="false" /><br />


</asp:Content>

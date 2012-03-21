<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" 
    CodeFile="popularGoals.aspx.cs" Inherits="admin_popularGoals" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    Search string 1: <asp:TextBox runat="server" ID="popularSearchString1TextBox" Width="200px" /><br />
    Search string 2: <asp:TextBox runat="server" ID="popularSearchString2TextBox" Width="200px" /><br />
    Search string 3: <asp:TextBox runat="server" ID="popularSearchString3TextBox" Width="200px" /><br />
    Search string 4: <asp:TextBox runat="server" ID="popularSearchString4TextBox" Width="200px" /><br />
    Search string 5: <asp:TextBox runat="server" ID="popularSearchString5TextBox" Width="200px" /><br />

    <asp:Button runat="server" ID="saveButton" text="Save" OnClick="saveButton_Click" />
    <asp:Button runat="server" ID="cancelButton" text="Back" OnClick="cancelButton_Click"
        CausesValidation="false" /><br />

</asp:Content>


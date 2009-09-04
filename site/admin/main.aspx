<%@ Page Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" 
    CodeFile="main.aspx.cs" Inherits="admin_main" Theme="AdminTheme" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:LinkButton ID="logoutLink" runat="server" OnClick="logoutLink_click" />
    
    <asp:Label ID="loggedInAsLabel" runat="server" /><br /><br />
    
    Admin menu
    
    <asp:LinkButton ID="administratorsLink" runat="server" Text="Administrators" OnClick="administratorsLink_click" /><br />
    <asp:LinkButton ID="usersLink" runat="server" Text="Users" OnClick="usersLink_click" /><br />

</asp:Content>


<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bannerLogin.ascx.cs" Inherits="components_bannerLogin" %>

	<li class="login" id="loggedInAsLi" runat="server"><asp:Label ID="loggedInAsLabel" runat="server"></asp:Label></li>
	<li class="login" id="logoutLi" runat="server"><asp:HyperLink ID="logoutLink" runat="server" 
	    NavigateUrl="~/logout.aspx" ToolTip="Logout" CssClass="" Text="Logout" /></li>

	<li class="login" id="loginLI" runat="server"><asp:HyperLink ID="loginLink" runat="server" 
	    NavigateUrl="~/login.aspx" ToolTip="Login" CssClass="modal" Text="Login" /></li>
	<li class="sign-up" id="signUpLI" runat="server"><asp:HyperLink ID="registerLink" runat="server" 
	    NavigateUrl="~/register.aspx" ToolTip="Sign up" CssClass="modal" Text="Sign up" /></li>
	<li class="help"><asp:HyperLink ID="helpLink" runat="server" 
	    NavigateUrl="~/help.aspx" ToolTip="Help" CssClass="modal" Text="Help" /></li>

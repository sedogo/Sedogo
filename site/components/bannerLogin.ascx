<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bannerLogin.ascx.cs" Inherits="components_bannerLogin" %>

<ul id="account-options">
    <li class="login" id="loggedInAsLi" runat="server"><asp:Label ID="loggedInAsLabel" runat="server"></asp:Label></li>
    <li class="login" id="logoutLi" runat="server"><asp:HyperLink ID="logoutLink" runat="server" 
        NavigateUrl="~/logout.aspx" ToolTip="Logout" Text="Logout" /></li>

    <li class="login" id="loginLI" runat="server"><asp:HyperLink ID="loginLink" runat="server" 
        NavigateUrl="~/login.aspx" ToolTip="Login" Text="Login" CssClass="modal" /></li>
    <li class="sign-up" id="signUpLI" runat="server"><asp:HyperLink ID="registerLink" runat="server" 
        NavigateUrl="~/register.aspx" ToolTip="Sign up" Text="Sign up" /></li>
    <li class="help"><asp:HyperLink ID="helpLink" runat="server" 
        NavigateUrl="~/help.aspx" ToolTip="Help" Text="Help" /></li>
</ul>
<div class="one-col">
	<asp:HyperLink ID="profileLink" runat="server" NavigateUrl="~/profile.aspx" 
	    ToolTip="sedogo : home" ImageUrl="~/images/sedogo.gif" />
	<p class="strapline">
	    Create your future and connect<br />
	    with others to make it happen
	</p>
</div>


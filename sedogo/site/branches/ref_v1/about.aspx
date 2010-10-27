<%@ Page Language="C#" AutoEventWireup="true" CodeFile="about.aspx.cs" Inherits="about" 
    MasterPageFile="Master.master"%>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="Timelines" Src="~/components/Timelines.ascx" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<asp:Content runat="server" ContentPlaceHolderID="meta">
    <meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="0" />
	<meta http-equiv="pragma" content="no-cache" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="title">
	About : Sedogo : Create your future and connect with others to make it happen
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="content">
	<div class="three-col">
        <h1>about</h1>
        <p>Sedogo is the future of social networking. Literally.</p>
        <p>Create tomorrow's goals for your life and connect with others to achieve them.</p>
        <p>&nbsp;</p>
        <a href="default.aspx">Return to home page</a>
	</div>
</asp:Content>  
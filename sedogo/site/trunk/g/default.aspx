<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="g_default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="../favicon.ico" >  
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="expires" content="never" />

	<title>Create your future and connect with others to make it happen</title>

	<meta name="keywords" content="" />
	<meta name="description" content="" />
	<meta name="author" content="" />
	<meta name="copyright" content="" />
	<meta name="robots" content="" />
	<meta name="MSSmartTagsPreventParsing" content="true" />

	<meta http-equiv="imagetoolbar" content="no" />
	<meta http-equiv="Cleartype" content="Cleartype" />

	<link rel="stylesheet" href="../css/main.css" />
	<!--[if gte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->
	<!--[if IE]>
		<link rel="stylesheet" href="css/main_ie.css" />
	<![endif]-->

	<script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="../js/jquery-ui-1.7.2.custom.min.js"></script>
	<script type="text/javascript" src="../js/ui.dialog.js"></script>
	<script type="text/javascript" src="../js/jquery.cookie.js"></script>
	<script type="text/javascript" src="../js/jquery.livequery.js"></script>
	<script type="text/javascript" src="../js/jquery.corner.js"></script>
	<script type="text/javascript" src="../js/main.js"></script>
    <script type="text/javascript" src="../utils/validationFunctions.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>    
    
        <div id="container">
	        <Sedogo:BannerLoginControl ID="BannerLoginControl1" runat="server" />
	        <Sedogo:BannerAddFindControl ID="bannerAddFindControl" runat="server" />

			<div id="other-content">
                <Sedogo:SidebarControl ID="sidebarControl" runat="server" />

				<div class="three-col">
					<div>
					Sedogo directory
					</div>
					
					<div>
					Browse by goal name<br />
					<table border="0" cellpadding="2" cellspacing="4">
					    <tr>
					        <td><asp:HyperLink ID="letterALink" runat="server" NavigateUrl="/g/?L=A" Text="A" /></td>
					        <td><asp:HyperLink ID="letterBLink" runat="server" NavigateUrl="/g/?L=B" Text="B" /></td>
					        <td><asp:HyperLink ID="letterCLink" runat="server" NavigateUrl="/g/?L=C" Text="C" /></td>
					        <td><asp:HyperLink ID="letterDLink" runat="server" NavigateUrl="/g/?L=D" Text="D" /></td>
					        <td><asp:HyperLink ID="letterELink" runat="server" NavigateUrl="/g/?L=E" Text="E" /></td>
					        <td><asp:HyperLink ID="letterFLink" runat="server" NavigateUrl="/g/?L=F" Text="F" /></td>
					        <td><asp:HyperLink ID="letterGLink" runat="server" NavigateUrl="/g/?L=G" Text="G" /></td>
					        <td><asp:HyperLink ID="letterHLink" runat="server" NavigateUrl="/g/?L=H" Text="H" /></td>
					        <td><asp:HyperLink ID="letterILink" runat="server" NavigateUrl="/g/?L=I" Text="I" /></td>
					        <td><asp:HyperLink ID="letterJLink" runat="server" NavigateUrl="/g/?L=J" Text="J" /></td>
					        <td><asp:HyperLink ID="letterKLink" runat="server" NavigateUrl="/g/?L=K" Text="K" /></td>
					        <td><asp:HyperLink ID="letterLLink" runat="server" NavigateUrl="/g/?L=L" Text="L" /></td>
					        <td><asp:HyperLink ID="letterMLink" runat="server" NavigateUrl="/g/?L=M" Text="M" /></td>
					        <td><asp:HyperLink ID="letterNLink" runat="server" NavigateUrl="/g/?L=N" Text="N" /></td>
					        <td><asp:HyperLink ID="letterOLink" runat="server" NavigateUrl="/g/?L=O" Text="O" /></td>
					        <td><asp:HyperLink ID="letterPLink" runat="server" NavigateUrl="/g/?L=P" Text="P" /></td>
					        <td><asp:HyperLink ID="letterQLink" runat="server" NavigateUrl="/g/?L=Q" Text="Q" /></td>
					        <td><asp:HyperLink ID="letterRLink" runat="server" NavigateUrl="/g/?L=R" Text="R" /></td>
					        <td><asp:HyperLink ID="letterSLink" runat="server" NavigateUrl="/g/?L=S" Text="S" /></td>
					        <td><asp:HyperLink ID="letterTLink" runat="server" NavigateUrl="/g/?L=T" Text="T" /></td>
					        <td><asp:HyperLink ID="letterULink" runat="server" NavigateUrl="/g/?L=U" Text="U" /></td>
					        <td><asp:HyperLink ID="letterVLink" runat="server" NavigateUrl="/g/?L=V" Text="V" /></td>
					        <td><asp:HyperLink ID="letterWLink" runat="server" NavigateUrl="/g/?L=W" Text="W" /></td>
					        <td><asp:HyperLink ID="letterXLink" runat="server" NavigateUrl="/g/?L=X" Text="X" /></td>
					        <td><asp:HyperLink ID="letterYLink" runat="server" NavigateUrl="/g/?L=Y" Text="Y" /></td>
					        <td><asp:HyperLink ID="letterZLink" runat="server" NavigateUrl="/g/?L=Z" Text="Z" /></td>
					    </tr>
					</table>
					</div>
					
					<div id="goalByLetterDiv" runat="server">
					
					    <asp:PlaceHolder ID="goalPlaceHolder" runat="server" />
					    <div id="noGoalsWithThisLetterDiv" runat="server">
					    <p>There are no goals with this letter</p>
					    </div>
					
					</div>
				</div>
			</div>
		    <Sedogo:FooterControl ID="footerControl" runat="server" />
		</div>
        <div id="modal-container">
			<a href="#" class="close-modal"><img src="../images/close-modal.gif" title="Close window" alt="Close window" /></a>
            <iframe frameborder="0"></iframe>
        </div>
        <div id="modal-background"></div>
            
    </div>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />
    </form>
</body>
</html>

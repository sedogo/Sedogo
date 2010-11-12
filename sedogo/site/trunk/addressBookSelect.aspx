<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addressBookSelect.aspx.cs" Inherits="addressBookSelect" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="favicon.ico" >  
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="0" />
	<meta http-equiv="pragma" content="no-cache" />

	<title>Upload profile picture : Sedogo : Create your future and connect with others to make it happen</title>

	<meta name="keywords" content="" />
	<meta name="description" content="" />
	<meta name="author" content="" />
	<meta name="copyright" content="" />
	<meta name="robots" content="" />
	<meta name="MSSmartTagsPreventParsing" content="true" />

	<meta http-equiv="imagetoolbar" content="no" />
	<meta http-equiv="Cleartype" content="Cleartype" />

	<link rel="stylesheet" href="css/main.css" />
	<!--[if IE]>
		<link rel="stylesheet" href="css/main_ie.css" />
	<![endif]-->
	<!--[if lte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->

	<script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="js/jquery.livequery.js"></script>
	<script type="text/javascript" src="js/jquery.corner.js"></script>
	<script type="text/javascript" src="js/main.js"></script>
    
    <script type="text/javascript" language="javascript">
    function clickEntry(emailAddress)
    {
        <asp:Literal id="clickScript" runat="server" />
        window.parent.closeModal();
    } 
    </script>
 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
	    <div id="modal">
            <h1>Select address</h1>
    
            <div id="contactsDiv" runat="server">
            <table border="0" cellspacing="2" cellpadding="0">
            <asp:Repeater ID="addressBookRepeater" runat="server" 
                OnItemDataBound="addressBookRepeater_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><p>Name: <asp:HyperLink ID="nameLabel" runat="server" /></p></td>
                    </tr>
                    <tr>
                        <td><div class="pinstripe-divider" style="margin: 5px 0 5px 0; width: 430px">&nbsp;</div></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
    
        </div>
        </form>

        <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />
    
    </div>
    </form>
</body>
</html>

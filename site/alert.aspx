<%@ Page Language="C#" AutoEventWireup="true" CodeFile="alert.aspx.cs" Inherits="alert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">
	<meta http-equiv="pragma" content="no-cache">

	<title>Edit to do title & details : Sedogo : Create your future and connect with others to make it happen</title>

	<meta name="keywords" content="" />
	<meta name="description" content="" />
	<meta name="author" content="" />
	<meta name="copyright" content="" />
	<meta name="robots" content="" />
	<meta name="MSSmartTagsPreventParsing" content="true" />

	<meta http-equiv="imagetoolbar" content="no" />
	<meta http-equiv="Cleartype" content="Cleartype" />

	<link rel="stylesheet" href="css/main.css" />
	<!--[if gte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
	    <div id="modal">
            <h1>alerts</h1>

            <div id="noAlertsDiv" runat="server">
            <p>You have no alerts.</p>
            </div>
            
            <div id="alertsDiv" runat="server">
            <asp:Repeater ID="alertsRepeater" runat="server" OnItemDataBound="alertsRepeater_ItemDataBound"
                OnItemCommand="alertsRepeater_ItemCommand">
                <ItemTemplate>
                
                    <table>
                        <tr>
                            <td valign="top"><asp:Image id="eventPicThumbnailImage" runat="server" /></td>
                            <td>
                                <p>To do name: <asp:Literal ID="eventNameLabel" runat="server" /></p>
                                <p><i><asp:Literal ID="alertDateLabel" runat="server" /><br />
                                <asp:Literal ID="alertTextLabel" runat="server" /></i></p>
                            </td>
                        </tr>
                    </table>
                    
                    <p><asp:LinkButton ID="clearAlertButton" runat="server" CssClass="button-sml" 
                        Text="clear alert" CommandName="clearAlertButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EventAlertID") %>' /></p>
                    <br />

                </ItemTemplate>
            </asp:Repeater>
            </div>
		</div>
    
    </div>
    </form>
</body>
</html>

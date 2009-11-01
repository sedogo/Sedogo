<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tracking.aspx.cs" Inherits="tracking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">
	<meta http-equiv="pragma" content="no-cache">

	<title>Help : Sedogo : Create your future timeline.  Connect, track and interact with like minded people.</title>

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
            <h1>events</h1>

            <div id="noTrackedEventsDiv" runat="server">
            <p>You are not tracking any events.</p>
            </div>
            
            <div id="trackedEventsDiv" runat="server">
            <asp:Repeater ID="trackedEventsRepeater" runat="server" 
                OnItemDataBound="trackedEventsRepeater_ItemDataBound"
                OnItemCommand="trackedEventsRepeater_ItemCommand">
                <ItemTemplate>
                
                    <table>
                        <tr>
                            <td><asp:Image ID="eventImage" runat="server" /></td>
                            <td>
                                <p><asp:Literal ID="eventNameLabel" runat="server" /><br />
                                <asp:HyperLink ID="eventHyperlink" runat="server" Text="View event details" /><br />
                                <i><asp:Literal ID="userNameLabel" runat="server" /></i></p>
                            </td>
                        </tr>
                    </table>
                    <p><asp:LinkButton ID="stopTrackingButton" runat="server" CssClass="button-sml" 
                        Text="stop tracking" CommandName="stopTrackingButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TrackedEventID") %>' /></p>
                    <br />

                </ItemTemplate>
            </asp:Repeater>
            </div>
		</div>
    
    </div>
    </form>
</body>
</html>

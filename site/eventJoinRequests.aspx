<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eventJoinRequests.aspx.cs" Inherits="eventJoinRequests" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="0" />
	<meta http-equiv="pragma" content="no-cache" />

	<title>Goal invites : Sedogo : Create your future and connect with others to make it happen</title>

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
            <h1>Requests</h1>

            <div id="noRequestsDiv" runat="server">
            <p>You have no requests.</p>
            </div>
            
            <div id="requestsDiv" runat="server">
            <asp:Repeater ID="requestsRepeater" runat="server" OnItemDataBound="requestsRepeater_ItemDataBound"
                OnItemCommand="requestsRepeater_ItemCommand">
                <ItemTemplate>
                
                    <table>
                        <tr>
                            <td>
                                <asp:Image id="eventPicThumbnailImage" runat="server" />
                            </td>
                            <td>
                                <p>Who: <asp:Label ID="userNameLabel" runat="server" CssClass="blue" /><br />
                                Goal requested: <asp:Label ID="eventNameLabel" runat="server" CssClass="blue" /><br />
                                <asp:HyperLink ID="eventHyperlink" runat="server" Text="View event details" /><br />
                                Date: <asp:Label ID="eventDateLabel" runat="server" CssClass="blue" /><br /></p>
                            </td>
                        </tr>
                    </table>
                    
                    <p><asp:LinkButton ID="acceptButton" runat="server" CssClass="button-sml" 
                        Text="accept" CommandName="acceptButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TrackedEventID") %>' />
                        <asp:LinkButton ID="declineButton" runat="server" CssClass="button-sml" 
                        Text="decline" CommandName="declineButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TrackedEventID") %>' /></p>
                    <br />

                </ItemTemplate>
            </asp:Repeater>
            </div>
		</div>
    
    </div>
    </form>
</body>
</html>

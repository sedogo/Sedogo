<%@ Page Language="C#" AutoEventWireup="true" CodeFile="message.aspx.cs" Inherits="message" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="0" />
	<meta http-equiv="pragma" content="no-cache" />

	<title>Messages : Sedogo : Create your future and connect with others to make it happen</title>

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
            <h1>messages</h1>

            <div id="noUnreadMessagesDiv" runat="server">
            <p>You have no unread messages.</p>
            </div>
            
            <div id="messagesDiv" runat="server">
            <asp:Repeater ID="messagesRepeater" runat="server" OnItemDataBound="messagesRepeater_ItemDataBound"
                OnItemCommand="messagesRepeater_ItemCommand">
                <ItemTemplate>
                
                    <table>
                        <tr>
                            <td>
                                <asp:Image id="eventPicThumbnailImage" runat="server" />
                            </td>
                            <td>
                                <p><asp:Literal ID="eventNameLabel" runat="server" /><br />
                                <i><asp:Literal ID="userNameLabel" runat="server" /></i><br />
                                <asp:Literal ID="messageLabel" runat="server" /></p>
                            </td>
                        </tr>
                    </table>
                    
                    <p><asp:LinkButton ID="markAsReadButton" runat="server" CssClass="button-sml" 
                        Text="mark as read" CommandName="markAsReadButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' />
                        <asp:LinkButton ID="sendReplyMessageButton" runat="server" CssClass="button-sml" 
                        Text="send reply" CommandName="sendReplyMessageButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MessageID") %>' /></p>
                    <br />

                </ItemTemplate>
            </asp:Repeater>
            </div>
		</div>
    
    </div>
    </form>
</body>
</html>

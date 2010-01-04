<%@ Page Language="C#" AutoEventWireup="true" CodeFile="groupGoals.aspx.cs" Inherits="groupGoals" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="0" />
	<meta http-equiv="pragma" content="no-cache" />

	<title>Tracking : Sedogo : Create your future and connect with others to make it happen</title>

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
	<!--[if gte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->

	<script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="js/jquery.livequery.js"></script>
	<script type="text/javascript" src="js/jquery.corner.js"></script>
	<script type="text/javascript" src="js/main.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
	    <div id="modal">
            <h1>Group goals</h1>

            <div id="noTrackedEventsDiv" runat="server">
            <p>You have not joined any goals.</p>
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
                                <p>Goal: <asp:HyperLink ID="eventNameLabel" runat="server" /><br />
                                <i><asp:HyperLink ID="userNameLabel" runat="server" Target="_top" /></i></p>
                            </td>
                        </tr>
                    </table>
                    <p style="padding-left: 54px"><asp:LinkButton ID="stopTrackingButton" runat="server" CssClass="button-sml" 
                        Text="Leave goal" CommandName="stopTrackingButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TrackedEventID") %>' /></p>
                    <br />

                </ItemTemplate>
            </asp:Repeater>
            </div>
		</div>
    
    </div>
    </form>
</body>
</html>

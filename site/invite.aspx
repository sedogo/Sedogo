<%@ Page Language="C#" AutoEventWireup="true" CodeFile="invite.aspx.cs" Inherits="invite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="0" />
	<meta http-equiv="pragma" content="no-cache" />

	<title>Invites : Sedogo : Create your future and connect with others to make it happen</title>

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
            <h1>Invites</h1>

            <div id="noInvitesDiv" runat="server">
            <p>You have no invitations.</p>
            </div>
            
            <div id="invitesDiv" runat="server">
            <asp:Repeater ID="invitesRepeater" runat="server" OnItemDataBound="invitesRepeater_ItemDataBound"
                OnItemCommand="invitesRepeater_ItemCommand">
                <ItemTemplate>
                
                    <table>
                        <tr>
                            <td>
                                <asp:Image id="eventPicThumbnailImage" runat="server" />
                            </td>
                            <td>
								<p>
									Goal creator: <span class="blue"><asp:Hyperlink ID="userNameLabel" runat="server" /></span><br />
									Goal: <span class="blue"><asp:Hyperlink ID="eventNameLabel" runat="server" /></span><br />
									Date: <span class="blue"><asp:Hyperlink ID="eventDateLabel" runat="server" /></span>
								</p>
                                <!--<asp:HyperLink ID="eventHyperlink" runat="server" Text="View" />-->
                            </td>
                        </tr>
                    </table>
                    
                    <p style="padding-left: 54px"><asp:LinkButton ID="acceptButton" runat="server" CssClass="button-sml-extra-padding" 
                        Text="Accept" CommandName="acceptButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EventInviteID") %>' />
                        <asp:LinkButton ID="declineButton" runat="server" CssClass="button-sml-extra-padding" 
                        Text="Decline" CommandName="declineButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EventInviteID") %>' /></p>
                    <div class="pinstripe-divider" style="margin: 20px 0 12px 0; width: 368px">&nbsp;</div>
                </ItemTemplate>
            </asp:Repeater>
            </div>
		</div>
    
    </div>
    </form>

<script type="text/javascript">
    var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
    document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
</script>
<script type="text/javascript">
    try
    {
        var pageTracker = _gat._getTracker("UA-12373356-1");
        pageTracker._trackPageview();
    } catch (err) { }
</script>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eventInvites.aspx.cs" Inherits="eventInvites" %>

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
    
        <div id="event-detail">
            <div class="right-col">
                <h3 ID="currentInvitesHeader" runat="server">Current invitations</h3>
                <asp:PlaceHolder ID="currentInvitesPlaceholder" runat="server" />
            </div>
            <div class="left-col">
		        <h1 style=""><asp:Literal ID="eventTitleLabel" runat="server" /></h1>
		        <p><asp:Label ID="eventDescriptionLabel" runat="server" /><br />
		        <i><asp:Label ID="eventOwnersNameLabel" runat="server" /></i><br />
		        <asp:Label ID="eventDateLabel" runat="server" /></p>

                <p><b>Invite new people</b><br /> type in their email address(es) in the box below, one email address per line:</p>

                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" /></td>
                        <td><asp:TextBox ID="inviteTextBox1" runat="server" Width="300px" /></td>
                    </tr>
                </table>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" /></td>
                        <td><asp:TextBox ID="inviteTextBox2" runat="server" Width="300px" /></td>
                    </tr>
                </table>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" /></td>
                        <td><asp:TextBox ID="inviteTextBox3" runat="server" Width="300px" /></td>
                    </tr>
                </table>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" /></td>
                        <td><asp:TextBox ID="inviteTextBox4" runat="server" Width="300px" /></td>
                    </tr>
                </table>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" /></td>
                        <td><asp:TextBox ID="inviteTextBox5" runat="server" Width="300px" /></td>
                    </tr>
                </table>

                <p>&nbsp;</p>
                <p>message (optional)</p>

                <asp:TextBox ID="additionalInviteTextTextBox" runat="server" TextMode="MultiLine"
                    Width="300px" Rows="3" />

                <div class="buttons">
                <p><asp:LinkButton ID="sendInvitesLink" runat="server" Text="Send invites"
                    OnClick="sendInvitesLink_click" CssClass="button-sml" />
	            <asp:LinkButton ID="backToEventDetailsLink" runat="server" CssClass="button-sml" Text="back to goal details" 
	                ToolTip="back to event details" OnClick="click_backToEventDetailsLink" CausesValidation="false" />
                </p>
                </div>

                <p>&nbsp;<br />Note that if a person does not have a Sedogo account, they will
                be invited to create one.</p>
		    </div>
		</div>
    
    </div>
    </form>
</body>
</html>

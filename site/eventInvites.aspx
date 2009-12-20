<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eventInvites.aspx.cs" Inherits="eventInvites" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
	<!--[if IE]>
		<link rel="stylesheet" href="css/main_ie.css" />
	<![endif]-->
	<!--[if gte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->

	<script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="js/jquery.corner.js"></script>
	<script type="text/javascript" src="js/main.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <div id="event-detail">
            <div class="right-col">
				<!--***PAUL*** this needs to show pending invitations and accepted invitations as per graphic 'inviteothers.jpg'-->
                <h3 ID="currentInvitesHeader" runat="server" style="color: #0cf; margin: 50px 0 0 0">Pending invitations</h3>
                
                <h3 id="H1" runat="server" style="color: #0cf; margin: 50px 0 0 0">Accepted invitations</h3>
                <asp:PlaceHolder ID="currentInvitesPlaceholder" runat="server" />
            </div>
            <div class="left-col">
		        <h1 style="color: #0cf; margin: 0 0 4px 0"><asp:Literal ID="eventTitleLabel" runat="server" /></h1>
		        <!--***PAUL*** needs the following line to be dynamically generated-->
		        <p style="font-style: italic; color: #ccc; margin: 0 0 4px 0; font-size: 11px">Edited 26th November 2009</p>
		        <p><asp:Label ID="eventDescriptionLabel" runat="server" /></p>

				<table class="summary">
					<tbody>
						<tr>
							<th>Who:</th>
							<td class="blue"><asp:Label ID="eventOwnersNameLabel" runat="server" /></td>
						</tr>
						<tr>
							<th>Goal:</th>
							<td></td>
						</tr>
						<tr>
							<th>Where:</th>
							<td></td>
						</tr>
						<!--***PAUL*** the output of the 'Before' section below needs to use better english so it always makes sense-->
						<tr>
							<th>Before:</th>
							<td><asp:Label ID="eventDateLabel" runat="server" /></td>
						</tr>
					</tbody>
				</table>

				<div class="pinstripe-divider">&nbsp;</div>

                <h3 class="blue" style="font-size: 12px; margin-bottom: 12px">Who do you want to invite to <!--***PAUL*** need event name here--></h3>
                <p>Type in friends or family email addresses or Sedogo account names.</p>

                <img src="images/profile/miniProfile.jpg" />
                <asp:TextBox ID="inviteTextBox1" runat="server" Width="210px" /><br />
                <img src="images/profile/miniProfile.jpg" />
                <asp:TextBox ID="inviteTextBox2" runat="server" Width="210px" /><br />
                <img src="images/profile/miniProfile.jpg" />
                <asp:TextBox ID="inviteTextBox3" runat="server" Width="210px" /><br />
                <img src="images/profile/miniProfile.jpg" />
                <asp:TextBox ID="inviteTextBox4" runat="server" Width="210px" /><br />
                <img src="images/profile/miniProfile.jpg" />
                <asp:TextBox ID="inviteTextBox5" runat="server" Width="210px" />
				<div class="pinstripe-divider" style="margin: 20px 0 14px 0">&nbsp;</div>
                <p style="margin-bottom: 8px">Invite message <em>(optional)</em></p>

                <asp:TextBox ID="additionalInviteTextTextBox" runat="server" TextMode="MultiLine"
                    Width="233px" Rows="3" />

                <div class="buttons">
                <p>
					<asp:LinkButton ID="backToEventDetailsLink" runat="server" CssClass="button-lrg" Text="Back" ToolTip="Back" OnClick="click_backToEventDetailsLink" CausesValidation="false" />
	                <asp:LinkButton ID="sendInvitesLink" runat="server" Text="Send invites" OnClick="sendInvitesLink_click" CssClass="button-lrg" />
	            
                </p>
                </div>

                <p style="font-size: 11px"><br />Please note that anyone without a sedogo account <br />will be invited to join to connect with your goal</p>
		    </div>
		</div>
    
    </div>
    </form>
</body>
</html>
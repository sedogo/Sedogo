<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addEventInvites.aspx.cs" Inherits="addEventInvites" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache"/>
	<meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>

	<title>Create goal : Sedogo : Create your future and connect with others to make it happen</title>

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
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
        <asp:ValidationSummary runat="server" ID="validationSummary" 
            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
            HeaderText="Please review the following errors:" />
    
	    <div id="modal">
            <h1>Who do you want to invite to <asp:Label ID="eventNameLabel" runat="server" /></h1>

            <p>Type in friends or family email addresses</p>

            <img src="images/profile/miniProfile.jpg" />
            <asp:TextBox ID="inviteTextBox1" runat="server" Width="300px" /><br />
            <img src="images/profile/miniProfile.jpg" />
            <asp:TextBox ID="inviteTextBox2" runat="server" Width="300px" /><br />
            <img src="images/profile/miniProfile.jpg" />
            <asp:TextBox ID="inviteTextBox3" runat="server" Width="300px" /><br />
            <img src="images/profile/miniProfile.jpg" />
            <asp:TextBox ID="inviteTextBox4" runat="server" Width="300px" /><br />
            <img src="images/profile/miniProfile.jpg" />
            <asp:TextBox ID="inviteTextBox5" runat="server" Width="300px" />

            <p>message (optional)</p>

            <asp:TextBox ID="additionalInviteTextTextBox" runat="server" TextMode="MultiLine"
                Width="300px" Rows="3" />

            <p>&nbsp;<br />(Please note anyone without a sedogo account will be invited to join before they can connect with your goal)</p>
		</div>
    
        <div class="buttons">
            <p><asp:LinkButton ID="sendInvitesLink" runat="server" Text="Send invites"
                OnClick="sendInvitesLink_click" CssClass="button-sml" />
            <asp:LinkButton ID="skipInvitesLink" runat="server" CssClass="button-sml" Text="Skip" 
                ToolTip="Skip" OnClick="click_skipInvitesLink" CausesValidation="false" />
            </p>
        </div>

    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewEvent.aspx.cs" Inherits="viewEvent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">
	<meta http-equiv="pragma" content="no-cache">

	<title>Edit event title & details : Sedogo : Create your future timeline.  Connect, track and interact with like minded people.</title>

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
	
<script language="JavaScript" type="text/javascript">
function setHiddenStartDateField()
{
	var form = document.forms[0];
	var d = form.startDateDay.options[form.startDateDay.selectedIndex].value;
	var m = form.startDateMonth.options[form.startDateMonth.selectedIndex].value;
	var y = form.startDateYear.options[form.startDateYear.selectedIndex].value;
	
	if( d == "" && m == "" && y == "" )
	{
        form.hiddenStartDate.value = "";
	}
	else
	{
        form.hiddenStartDate.value = d + "/" + m + "/" + y;
    }
}
</script>
	
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <div id="event-detail">
            <div class="right-col">
                <asp:Image ID="eventImage" runat="server" />
                <p><asp:LinkButton ID="uploadEventImage" runat="server" OnClick="click_uploadEventImage"
                    Text="Upload image" /></p>
                <h3 ID="messagesHeader" runat="server">Messages</h3>
                <p><asp:HyperLink ID="messagesLink" runat="server" /></p>
                <h3 ID="invitesHeader" runat="server">Invites</h3>
                <p><asp:HyperLink ID="invitesLink" runat="server" /></a></p>
                <h3 ID="alertsHeader" runat="server">Alerts</h3>
                <p><asp:HyperLink ID="alertsLink" runat="server" /><a href="" title=""></a></p>
                <h3 ID="trackingHeader" runat="server">Tracking</h3>
                <asp:PlaceHolder ID="trackingLinksPlaceholder" runat="server" />
            </div>
            <div class="left-col">
		        <h1 style="">Edit: <asp:Literal ID="eventTitleLabel" runat="server" /></h1>
		        <p>
		            <asp:HyperLink ID="editEventLink" runat="server" CssClass="button-sml" Text="edit" ToolTip="edit" />
		            <asp:LinkButton ID="achievedEventLink" runat="server" CssClass="button-sml" Text="achieved" 
		                ToolTip="achieved" OnClick="click_achievedEventLink" />
		        </p>
		        
		        <p><asp:LinkButton ID="sendMessageButton" runat="server" ToolTip="send a message" 
		            Text="send a message" OnClick="sendMessageButton_click" /> <asp:LinkButton 
		            ID="postCommentButton" runat="server" ToolTip="post a comment" Text="post a comment" 
		            OnClick="postCommentButton_click" /></p>
		        
		        <asp:PlaceHolder ID="commentsPlaceHolder" runat="server" />
		    </div>
		</div>
    
    </div>
    </form>
</body>
</html>

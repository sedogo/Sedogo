<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addEventSummary.aspx.cs" Inherits="addEventSummary" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="favicon.ico" >  
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
	<!--[if IE]>
		<link rel="stylesheet" href="css/main_ie.css" />
	<![endif]-->
	<!--[if lte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->

	<script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="js/jquery.livequery.js"></script>
	<script type="text/javascript" src="js/jquery.corner.js"></script>
	<script type="text/javascript" src="js/main.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
        <asp:ValidationSummary runat="server" ID="validationSummary" 
            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
            HeaderText="Please review the following errors:" />
    
	    <div id="modal">
	    <div id="modalContents">
    
            <div id="event-detail">
                <div class="right-col" style="padding: 50px 10px 0 0">
					<div style="width: 170px; overflow: hidden; float: right">
						<asp:Image ID="eventImage" runat="server" Width="170" />
					</div>
                </div>
                <div class="left-col">
    	    
	                <h1><asp:Label ID="eventNameLabel" runat="server" /> Summary</h1>
    	            
    	            <table class="summary">
    					<tbody>
    						<tr>
    							<th>Who:</th>
    							<td class="blue"><asp:Label ID="whoLabel" runat="server" /></td>
    						</tr>
    						<tr>
    							<th>Goal:</th>
    							<td><asp:Label ID="goalNameLabel" runat="server" /></td>
    						</tr>
    						<tr>
    							<th>Description:</th>
    							<td><asp:Label ID="descriptionLabel" runat="server" /></td>
    						</tr>
    						<tr>
    							<th>Where:</th>
    							<td><asp:Label ID="venueLabel" runat="server" /></td>
    						</tr>
    						<tr>
    							<th>Date:</th>
    							<td><asp:Label ID="dateLabel" runat="server" /></td>
    						</tr>
    						<tr>
    							<th>Reminder:</th>
    							<td><asp:Label ID="reminderLabel" runat="server" /></td>
    						</tr>
    						<tr>
    							<th>Invites:</th>
    							<td><asp:Label ID="invitesLabel" runat="server" /></td>
    						</tr>
    					</tbody>
    	            </table>
    	            
    	            <img src="images/popupLine.png" alt="" class="line-divider" style="margin-top: 30px" />

                </div>
            </div>
            
            <div class="buttons">
                <p>
					<asp:LinkButton ID="viewEventLink" runat="server" CssClass="button-lrg" Text="Save Goal" ToolTip="Save Goal" OnClick="click_viewEventLink" CausesValidation="false" />
                </p>
            </div>

		</div>
		</div>
    
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>
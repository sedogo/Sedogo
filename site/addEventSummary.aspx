<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addEventSummary.aspx.cs" Inherits="addEventSummary" %>

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
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
        <asp:ValidationSummary runat="server" ID="validationSummary" 
            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
            HeaderText="Please review the following errors:" />
    
	    <div id="modal">
	    <div id="modalContents">
    
            <div id="event-detail">
                <div class="right-col">
                    <asp:Image ID="eventImage" runat="server" />
                </div>
                <div class="left-col">
    	    
	                <h1><asp:Label ID="eventNameLabel" runat="server" /> Summary</h1>
    	            
                    <fieldset>
                        <ol>
                            <li>
                                <label for="whoLabel">Who:</label>
                                <h3 class="blue"><asp:Label ID="whoLabel" runat="server" /></h3>
                            </li>
                            <li>
                                <label for="goalNameLabel">Goal:</label>
                                <asp:Label ID="goalNameLabel" runat="server" />
                            </li>
                            <li>
                                <label for="descriptionLabel">Description:</label>
                                <asp:Label ID="descriptionLabel" runat="server" />
                            </li>
                            <li>
                                <label for="venueLabel">Where:</label>
                                <asp:Label ID="venueLabel" runat="server" />
                            </li>
                            <li>
                                <label for="dateLabel">Date:</label>
                                <asp:Label ID="dateLabel" runat="server" />
                            </li>
                            <li>
                                <label for="reminderLabel">Reminder:</label>
                                <asp:Label ID="reminderLabel" runat="server" />
                            </li>
                            <li>
                                <label for="invitesLabel">Invites:</label>
                                <asp:Label ID="invitesLabel" runat="server" />
                            </li>
                        </ol>
                    </fieldset>	            
                </div>
            </div>
            
            <div class="buttons">
                <p><asp:LinkButton ID="viewEventLink" runat="server" CssClass="button-sml" Text="View goal details" 
                    ToolTip="View event" OnClick="click_viewEventLink" CausesValidation="false" />
                </p>
            </div>

		</div>
		</div>
    
    </div>
    </form>
</body>
</html>

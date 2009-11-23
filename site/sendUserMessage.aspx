<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sendUserMessage.aspx.cs" Inherits="sendUserMessage" %>

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
            <h1>send message</h1>

            <p>Send message to <asp:Label ID="messageToLabel" runat="server" /><br />
            Regarding goal: <asp:Label ID="eventNameLabel" runat="server" /></p>
                        
            <fieldset>
                <ol>
                    <li>
                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="8"
                            ID="messageTextBox" Width="400px" />
                            <asp:RequiredFieldValidator ID="messageTextBoxValidator" runat="server"
                            ControlToValidate="messageTextBox" ErrorMessage="A message is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                    </li>
                </ol>
            </fieldset>
		</div>
    
        <div class="buttons">
            <asp:LinkButton 
                ID="saveChangesButton" runat="server" ToolTip="save" Text="Save" 
                OnClick="saveChangesButton_click" CssClass="button-sml" />
        </div>    

    </div>
    </form>
</body>
</html>

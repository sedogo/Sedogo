<%@ Page Language="C#" AutoEventWireup="true" CodeFile="postComment.aspx.cs" Inherits="postComment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="0" />
	<meta http-equiv="pragma" content="no-cache" />

	<title>Post comment : Sedogo : Create your future and connect with others to make it happen</title>

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
    
	    <div id="modal">
            <h1>Add comment</h1>
            <fieldset>
                <ol>
                    <li>
                        <label for="">Goal name</label>
                        <asp:Label ID="eventNameLabel" runat="server" />
                    </li>
                    <div class="pinstripe-divider">&nbsp;</div>
                    <li>
						<label for="">Comment</label>
                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="8"
                            ID="commentTextBox" Width="400px" />
                            <asp:RequiredFieldValidator ID="commentTextBoxValidator" runat="server"
                            ControlToValidate="commentTextBox" ErrorMessage="A comment is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                    </li>
                </ol>
            </fieldset>
		</div>

        <div class="buttons">
            <asp:LinkButton 
                ID="saveChangesButton" runat="server" ToolTip="save" Text="Save" 
                OnClick="saveChangesButton_click" CssClass="button-lrg" />
            <asp:LinkButton 
                ID="backButton" runat="server" ToolTip="save" Text="Back to goal details" 
                OnClick="backButton_click" CssClass="button-lrg" CausesValidation="false" />
        </div>    
    </div>
    </form>
</body>
</html>

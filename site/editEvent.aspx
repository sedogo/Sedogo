<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editEvent.aspx.cs" Inherits="editEvent" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div>

	    <div id="modal">
            <h1>edit event</h1>
            <p>Edit event</p>
            <fieldset>
                <ol>
                    <li>
                        <label for="">Event name</label>
                        <asp:TextBox runat="server"
                            ID="eventNameTextBox" Width="200px" MaxLength="200" />
                            <asp:RequiredFieldValidator ID="eventNameTextBoxValidator" runat="server"
                            ControlToValidate="eventNameTextBox" ErrorMessage="An event name is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="">Start date</label>
                        <asp:DropDownList ID="startDateDay" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="startDateMonth" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="startDateYear" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="hiddenStartDate" runat="server" />
                        <asp:CompareValidator ID="startDateValidator" runat="server"
                           ControlToValidate="hiddenStartDate" ErrorMessage="Select a start date"
                           Operator="DataTypeCheck" Type="Date" />
                    </li>
                    <li>
                        <label for="">Category</label>
                        <asp:DropDownList ID="categoryDropDownList" runat="server">
                            <asp:ListItem Text="Personal" Value="1" />
                            <asp:ListItem Text="Travel" Value="2" />
                            <asp:ListItem Text="Friends" Value="3" />
                            <asp:ListItem Text="Family" Value="4" />
                            <asp:ListItem Text="General" Value="5" />
                            <asp:ListItem Text="Health" Value="6" />
                            <asp:ListItem Text="Money" Value="7" />
                            <asp:ListItem Text="Education" Value="8" />
                            <asp:ListItem Text="Hobbies" Value="9" />
                            <asp:ListItem Text="Culture" Value="10" />
                            <asp:ListItem Text="Charity" Value="11" />
                            <asp:ListItem Text="Green" Value="12" />
                            <asp:ListItem Text="Misc" Value="13" />
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label for=""></label>
                        <asp:Button id="saveChangesButton" runat="server" OnClick="saveChangesButton_click" Text="Save" />
                    </li>
                </ol>
            </fieldset>
		</div>
    
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addEvent.aspx.cs" Inherits="addEvent" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache"/>
	<meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>

	<title>FAQ : Sedogo : Create your future timeline.  Connect, track and interact with like minded people.</title>

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
function setHiddenEndDateField()
{
	var form = document.forms[0];
	var d = form.endDateDay.options[form.endDateDay.selectedIndex].value;
	var m = form.endDateMonth.options[form.endDateMonth.selectedIndex].value;
	var y = form.endDateYear.options[form.endDateYear.selectedIndex].value;
	
	if( d == "" && m == "" && y == "" )
	{
        form.hiddenEndDate.value = "";
	}
	else
	{
        form.hiddenEndDate.value = d + "/" + m + "/" + y;
    }
}
</script>
	
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
	    <div id="modal">
            <h1>add event</h1>
            <p>Add event</p>
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
                        <label for="">End date</label>
                        <asp:DropDownList ID="endDateDay" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="endDateMonth" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="endDateYear" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="hiddenEndDate" runat="server" />
                        <asp:CompareValidator ID="endDateValidator" runat="server"
                           ControlToValidate="hiddenEndDate" ErrorMessage="Select an end date"
                           Operator="DataTypeCheck" Type="Date" />
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

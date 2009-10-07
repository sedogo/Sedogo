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
function setHiddenRangeStartDateField()
{
	var form = document.forms[0];
	var d = form.dateRangeStartDay.options[form.dateRangeStartDay.selectedIndex].value;
	var m = form.dateRangeStartMonth.options[form.dateRangeStartMonth.selectedIndex].value;
	var y = form.dateRangeStartYear.options[form.dateRangeStartYear.selectedIndex].value;
	
	if( d == "" && m == "" && y == "" )
	{
        form.hiddenDateRangeStartDate.value = "";
	}
	else
	{
        form.hiddenDateRangeStartDate.value = d + "/" + m + "/" + y;
    }
}
function setHiddenRangeEndDateField()
{
	var form = document.forms[0];
	var d = form.dateRangeEndDay.options[form.dateRangeEndDay.selectedIndex].value;
	var m = form.dateRangeEndMonth.options[form.dateRangeEndMonth.selectedIndex].value;
	var y = form.dateRangeEndYear.options[form.dateRangeEndYear.selectedIndex].value;
	
	if( d == "" && m == "" && y == "" )
	{
        form.hiddenDateRangeEndDate.value = "";
	}
	else
	{
        form.hiddenDateRangeEndDate.value = d + "/" + m + "/" + y;
    }
}
</script>
	
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
	    <div id="modal">
            <h1>add event</h1>
            <fieldset>
                <ol>
                    <li>
                        <label for="">What are you going to do?</label>
                    </li>
                    <li>
                        <asp:TextBox runat="server"
                            ID="eventNameTextBox" Width="200px" MaxLength="200" />
                            <asp:RequiredFieldValidator ID="eventNameTextBoxValidator" runat="server"
                            ControlToValidate="eventNameTextBox" ErrorMessage="An event name is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="">What type of thing?</label>
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
                        <label for="">When?</label>
                        <asp:DropDownList ID="dateTypeDropDownList" runat="server" 
                            OnSelectedIndexChanged="dateTypeDropDownList_changed" AutoPostBack="true">
                            <asp:ListItem Text="Specific date" Value="D" />
                            <asp:ListItem Text="Date range" Value="R" />
                            <asp:ListItem Text="Before age" Value="A" />
                        </asp:DropDownList>
                    </li>
                    <li id="startDateLI" runat="server">
                        <label for="">Start date</label>
                        <asp:DropDownList ID="startDateDay" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="startDateMonth" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="startDateYear" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="hiddenStartDate" runat="server" />
                        <asp:CompareValidator ID="startDateValidator" runat="server"
                           ControlToValidate="hiddenStartDate" ErrorMessage="Enter a valid start date"
                           Operator="DataTypeCheck" Type="Date" />
                    </li>
                    <li id="dateRangeLI1" runat="server">
                        <label for="">Start date</label>
                        <asp:DropDownList ID="dateRangeStartDay" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="dateRangeStartMonth" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="dateRangeStartYear" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="hiddenDateRangeStartDate" runat="server" />
                        <asp:CompareValidator ID="hiddenDateRangeStartDateValidator" runat="server"
                           ControlToValidate="hiddenDateRangeStartDate" ErrorMessage="Enter a valid start date"
                           Operator="DataTypeCheck" Type="Date" />
                    </li>
                    <li id="dateRangeLI2" runat="server">
                        <label for="">To date</label>
                        <asp:DropDownList ID="dateRangeEndDay" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="dateRangeEndMonth" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="dateRangeEndYear" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="hiddenDateRangeEndDate" runat="server" />
                        <asp:CompareValidator ID="hiddenDateRangeEndDateValidator" runat="server"
                           ControlToValidate="hiddenDateRangeEndDate" ErrorMessage="Enter a valid end date"
                           Operator="DataTypeCheck" Type="Date" />
                    </li>
                    <li id="birthdayLI" runat="server">
                        <label for="">Birthday</label>
                        <asp:DropDownList ID="birthdayDropDownList" runat="server">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label for="">Private event</label>
                        <asp:CheckBox ID="privateEventCheckbox" runat="server" />
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

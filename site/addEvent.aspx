<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addEvent.aspx.cs" Inherits="addEvent" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

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
	<link rel="stylesheet" href="css/calendarStyle.css" />
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
	<link href="css/cadialog.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
<script language="JavaScript" type="text/javascript">
function PickerStartDate_OnChange()
{
    <%= CalendarStartDate.ClientID.Replace("$","_").Replace(":","_") %>.SetSelectedDate(<%= PickerStartDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate());
}
function CalendarStartDate_OnChange()
{
    <%= PickerStartDate.ClientID.Replace("$","_").Replace(":","_") %>.SetSelectedDate(<%= CalendarStartDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate());
}
function popupCalendarStartDate(image)
{
    if( <%= CalendarStartDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate() == null )
    {
        <%= CalendarStartDate.ClientID.Replace("$","_").Replace(":","_") %>.ClearSelectedDate();
    }
    <%=CalendarStartDate.ClientObjectId%>.Show(image);    
}
function PickerRangeStartDate_OnChange()
{
    <%= CalendarRangeStartDate.ClientID.Replace("$","_").Replace(":","_") %>.SetSelectedDate(<%= PickerRangeStartDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate());
}
function CalendarRangeStartDate_OnChange()
{
    <%= PickerRangeStartDate.ClientID.Replace("$","_").Replace(":","_") %>.SetSelectedDate(<%= CalendarRangeStartDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate());
}
function popupCalendarRangeStartDate(image)
{
    if( <%= CalendarRangeStartDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate() == null )
    {
        <%= CalendarRangeStartDate.ClientID.Replace("$","_").Replace(":","_") %>.ClearSelectedDate();
    }
    <%=CalendarRangeStartDate.ClientObjectId%>.Show(image);    
}
function PickerRangeEndDate_OnChange()
{
    <%= CalendarRangeEndDate.ClientID.Replace("$","_").Replace(":","_") %>.SetSelectedDate(<%= PickerRangeEndDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate());
}
function CalendarRangeEndDate_OnChange()
{
    <%= PickerRangeEndDate.ClientID.Replace("$","_").Replace(":","_") %>.SetSelectedDate(<%= CalendarRangeEndDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate());
}
function popupCalendarRangeEndDate(image)
{
    if( <%= CalendarRangeEndDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate() == null )
    {
        <%= CalendarRangeEndDate.ClientID.Replace("$","_").Replace(":","_") %>.ClearSelectedDate();
    }
    <%=CalendarRangeEndDate.ClientObjectId%>.Show(image);    
}
function preSaveClick()
{
    document.forms[0].target = "_top";
}
</script>

        <asp:ValidationSummary runat="server" ID="validationSummary" 
            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
            HeaderText="Please review the following errors:" />
    
	    <div id="modal">
            <h1>Create goal</h1>
            
            <table border="0" cellspacing="0" cellpadding="2" width="400">
                <tr>
                    <td width="200">*Goal name</td>
                    <td width="200"><asp:TextBox runat="server"
                        ID="eventNameTextBox" Width="200px" MaxLength="200" />
                        <asp:RequiredFieldValidator ID="eventNameTextBoxValidator" runat="server"
                        ControlToValidate="eventNameTextBox" ErrorMessage="A goal name is required" Display="Dynamic">
                        </asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td colspan="2"><img src="images/popupLine.png" alt="" class="line-divider" /></td>
                </tr>
                <tr>
                    <td width="200" valign="top">Goal description</td>
                    <td width="200"><asp:TextBox runat="server" TextMode="MultiLine" Rows="4"
                        ID="eventDescriptionTextBox" Width="200px" /></td>
                </tr>
                <tr>
                    <td colspan="2"><img src="images/popupLine.png" alt="" class="line-divider" /></td>
                </tr>
                <tr>
                    <td width="200">Where</td>
                    <td width="200"><asp:TextBox runat="server" MaxLength="200"
                        ID="eventVenueTextBox" Width="200px" /></td>
                </tr>
                <tr>
                    <td colspan="2"><img src="images/popupLine.png" alt="" class="line-divider" /></td>
                </tr>
                <tr>
                    <td width="200">*When</td>
                    <td width="200"><asp:DropDownList ID="dateTypeDropDownList" runat="server" 
                        OnSelectedIndexChanged="dateTypeDropDownList_changed" AutoPostBack="true">
                        <asp:ListItem Text="Specific date" Value="D" />
                        <asp:ListItem Text="Date range" Value="R" />
                        <asp:ListItem Text="Before age" Value="A" />
                    </asp:DropDownList></td>
                </tr>
                <tr id="startDateLI" runat="server">
                    <td width="200">*Starts</td>
                    <td width="200">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td><ComponentArt:Calendar ID="PickerStartDate" 
                                    runat="server" 
                                    ClientSideOnSelectionChanged="PickerStartDate_OnChange"
                                    ControlType="Picker" 
                                    PickerCssClass="picker" 
                                    ImagesBaseUrl="./images/calendarImages/"
                                    PickerCustomFormat="dd/MM/yyyy" PickerFormat="Custom" 
                                    PopUpExpandControlId="calendarButton">
                                </ComponentArt:Calendar>
                                <ComponentArt:Calendar ID="CalendarStartDate" 
                                    runat="server" 
                                    CalendarCssClass="calendar"
                                    TitleCssClass="title" 
                                    ControlType="Calendar"
                                    DayCssClass="day" 
                                    DayHeaderCssClass="dayheader" 
                                    DayHoverCssClass="dayhover" 
                                    DayNameFormat="FirstTwoLetters"
                                    ImagesBaseUrl="./images/calendarImages/"
                                    MonthCssClass="month"
                                    NextImageUrl="cal_nextMonth.gif"
                                    NextPrevCssClass="nextprev" 
                                    OtherMonthDayCssClass="othermonthday" 
                                    PrevImageUrl="cal_prevMonth.gif" 
                                    SelectedDayCssClass="selectedday" 
                                    SelectMonthCssClass="selector"
                                    SelectMonthText="&curren;" 
                                    SelectWeekCssClass="selector"
                                    SelectWeekText="&raquo;" 
                                    SwapDuration="300"
                                    SwapSlide="Linear"
                                    ClientSideOnSelectionChanged="CalendarStartDate_OnChange" 
                                    PopUp="Custom" 
                                    PopUpExpandControlId="calendarButton">
                                </ComponentArt:Calendar></td>
                                <td><img alt="Click for pop-up calendar" 
                                    id="Img4" 
                                    onclick="popupCalendarStartDate(this)" 
                                    class="calendar_button" 
                                    src="./images/calendarImages/btn_calendar.gif" 
                                    width="25" height="22" /></td>
                                <td>&nbsp;&nbsp;</td>
                                <td><asp:DropDownList ID="datePickList" runat="server" 
                                    OnSelectedIndexChanged="datePickList_changed" AutoPostBack="true">
                                    <asp:ListItem Text="Select timeframe:" Value="" />
                                    <asp:ListItem Text="In 5 years" Value="5" />
                                    <asp:ListItem Text="In 10 years" Value="10" />
                                    <asp:ListItem Text="In 20 years" Value="20" />
                                </asp:DropDownList></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="dateRangeLI1" runat="server">
                    <td width="200">*Starts</td>
                    <td width="200">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td><ComponentArt:Calendar ID="PickerRangeStartDate" 
                                    runat="server" 
                                    ClientSideOnSelectionChanged="PickerRangeStartDate_OnChange"
                                    ControlType="Picker" 
                                    PickerCssClass="picker" 
                                    ImagesBaseUrl="./images/calendarImages/"
                                    PickerCustomFormat="dd/MM/yyyy" PickerFormat="Custom" 
                                    PopUpExpandControlId="calendarButton">
                                </ComponentArt:Calendar>
                                <ComponentArt:Calendar ID="CalendarRangeStartDate" 
                                    runat="server" 
                                    CalendarCssClass="calendar"
                                    TitleCssClass="title" 
                                    ControlType="Calendar"
                                    DayCssClass="day" 
                                    DayHeaderCssClass="dayheader" 
                                    DayHoverCssClass="dayhover" 
                                    DayNameFormat="FirstTwoLetters"
                                    ImagesBaseUrl="./images/calendarImages/"
                                    MonthCssClass="month"
                                    NextImageUrl="cal_nextMonth.gif"
                                    NextPrevCssClass="nextprev" 
                                    OtherMonthDayCssClass="othermonthday" 
                                    PrevImageUrl="cal_prevMonth.gif" 
                                    SelectedDayCssClass="selectedday" 
                                    SelectMonthCssClass="selector"
                                    SelectMonthText="&curren;" 
                                    SelectWeekCssClass="selector"
                                    SelectWeekText="&raquo;" 
                                    SwapDuration="300"
                                    SwapSlide="Linear"
                                    ClientSideOnSelectionChanged="CalendarRangeStartDate_OnChange" 
                                    PopUp="Custom" 
                                    PopUpExpandControlId="calendarButton">
                                </ComponentArt:Calendar></td>
                                <td><img alt="Click for pop-up calendar" 
                                    id="Img1" 
                                    onclick="popupCalendarRangeStartDate(this)" 
                                    class="calendar_button" 
                                    src="./images/calendarImages/btn_calendar.gif" 
                                    width="25" height="22" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="dateRangeLI2" runat="server">
                    <td width="200">*Ends</td>
                    <td width="200">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td><ComponentArt:Calendar ID="PickerRangeEndDate" 
                                    runat="server" 
                                    ClientSideOnSelectionChanged="PickerRangeEndDate_OnChange"
                                    ControlType="Picker" 
                                    PickerCssClass="picker" 
                                    ImagesBaseUrl="./images/calendarImages/"
                                    PickerCustomFormat="dd/MM/yyyy" PickerFormat="Custom" 
                                    PopUpExpandControlId="calendarButton">
                                </ComponentArt:Calendar>
                                <ComponentArt:Calendar ID="CalendarRangeEndDate" 
                                    runat="server" 
                                    CalendarCssClass="calendar"
                                    TitleCssClass="title" 
                                    ControlType="Calendar"
                                    DayCssClass="day" 
                                    DayHeaderCssClass="dayheader" 
                                    DayHoverCssClass="dayhover" 
                                    DayNameFormat="FirstTwoLetters"
                                    ImagesBaseUrl="./images/calendarImages/"
                                    MonthCssClass="month"
                                    NextImageUrl="cal_nextMonth.gif"
                                    NextPrevCssClass="nextprev" 
                                    OtherMonthDayCssClass="othermonthday" 
                                    PrevImageUrl="cal_prevMonth.gif" 
                                    SelectedDayCssClass="selectedday" 
                                    SelectMonthCssClass="selector"
                                    SelectMonthText="&curren;" 
                                    SelectWeekCssClass="selector"
                                    SelectWeekText="&raquo;" 
                                    SwapDuration="300"
                                    SwapSlide="Linear"
                                    ClientSideOnSelectionChanged="CalendarRangeEndDate_OnChange" 
                                    PopUp="Custom" 
                                    PopUpExpandControlId="calendarButton">
                                </ComponentArt:Calendar></td>
                                <td><img alt="Click for pop-up calendar" 
                                    id="Img2" 
                                    onclick="popupCalendarRangeEndDate(this)" 
                                    class="calendar_button" 
                                    src="./images/calendarImages/btn_calendar.gif" 
                                    width="25" height="22" /></td>
                                <td>&nbsp;&nbsp;</td>
                                <td><asp:DropDownList ID="daterangePickList" runat="server" 
                                    OnSelectedIndexChanged="daterangePickList_changed" AutoPostBack="true">
                                    <asp:ListItem Text="Select timeframe:" Value="" />
                                    <asp:ListItem Text="Within 1 year" Value="1" />
                                    <asp:ListItem Text="Within 5 years" Value="5" />
                                    <asp:ListItem Text="Within 10 years" Value="10" />
                                    <asp:ListItem Text="Within 20 years" Value="20" />
                                </asp:DropDownList></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"><img src="images/popupLine.png" alt="" class="line-divider" /></td>
                </tr>
                <tr id="birthdayLI" runat="server">
                    <td width="200">*Birthday</td>
                    <td width="200"><asp:DropDownList ID="birthdayDropDownList" runat="server">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="2"><img src="images/popupLine.png" alt="" class="line-divider" /></td>
                </tr>
                <tr>
                    <td width="200">*Timezone</td>
                    <td width="200"><asp:DropDownList ID="timezoneDropDownList" runat="server"
                        DataTextField="Description" DataValueField="TimezoneID">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="timezoneRequiredFieldValidator" runat="server"
                        ControlToValidate="timezoneDropDownList" ErrorMessage="A timezone is required" Display="Dynamic">
                        </asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td colspan="2"><img src="images/popupLine.png" alt="" class="line-divider" /></td>
                </tr>
                <tr>
                    <td width="200">*Category</td>
                    <td width="200"><asp:DropDownList ID="categoryDropDownList" runat="server">
                            <asp:ListItem Text="Personal" Value="1" />
                            <asp:ListItem Text="Travel" Value="2" />
                            <asp:ListItem Text="Friends" Value="3" />
                            <asp:ListItem Text="Family" Value="4" />
                            <asp:ListItem Text="General" Value="5" />
                            <asp:ListItem Text="Health" Value="6" />
                            <asp:ListItem Text="Money" Value="7" />
                            <asp:ListItem Text="Education" Value="8" />
                            <asp:ListItem Text="Hobbies" Value="9" />
                            <asp:ListItem Text="Work" Value="10" />
                            <asp:ListItem Text="Culture" Value="11" />
                            <asp:ListItem Text="Charity" Value="12" />
                            <asp:ListItem Text="Green" Value="13" />
                            <asp:ListItem Text="Misc" Value="14" />
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="2"><img src="images/popupLine.png" alt="" class="line-divider" /></td>
                </tr>
                <tr>
                    <td width="200">Nature of goal</td>
                    <td width="200">Private: <asp:CheckBox ID="privateEventCheckbox" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="2"><img src="images/popupLine.png" alt="" class="line-divider" /></td>
                </tr>
            </table>

		</div>
		<p class="required-field">&#42; Required field</p>
        <div class="buttons">
            <asp:LinkButton id="saveChangesButton" runat="server" OnClick="saveChangesButton_click" 
                Text="Save" CssClass="button-lrg" OnClientClick="javascript:preSaveClick()" />
        </div>

    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editEvent.aspx.cs" Inherits="editEvent" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">
	<meta http-equiv="pragma" content="no-cache">

	<title>Edit to do title & details : Sedogo : Create your future and connect with others to make it happen</title>

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
function CalendarAlertDate_OnChange()
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
</script>
	
</head>
<body>
    <form id="form1" runat="server">
    <div>

	    <div id="modal">
            <h1>edit to do</h1>
            <fieldset>
                <ol>
                    <li>
                        <label for="">To do name</label>
                        <asp:TextBox runat="server"
                            ID="eventNameTextBox" Width="200px" MaxLength="200" />
                            <asp:RequiredFieldValidator ID="eventNameTextBoxValidator" runat="server"
                            ControlToValidate="eventNameTextBox" ErrorMessage="A to do name is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="">To do description</label>
                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="4"
                            ID="eventDescriptionTextBox" Width="200px" />
                    </li>
                    <li>
                        <label for="">Venue</label>
                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="4"
                            ID="eventVenueTextBox" Width="200px" />
                    </li>
                    <li>
                        <label for="">When?</label>
                        <asp:DropDownList ID="dateTypeDropDownList" runat="server" 
                            OnSelectedIndexChanged="dateTypeDropDownList_changed" AutoPostBack="true">
                            <asp:ListItem Text="Specific date" Value="D" />
                            <asp:ListItem Text="Date range" Value="R" />
                            <asp:ListItem Text="Before age" Value="A" />
                            <asp:ListItem Text="In 5 years time" Value="5" />
                            <asp:ListItem Text="In 10 years time" Value="10" />
                            <asp:ListItem Text="In 20 years time" Value="20" />
                        </asp:DropDownList>
                    </li>
                    <li id="startDateLI" runat="server">
                        <label for="">Start date</label>
                        
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
                            </tr>
                        </table>
                    </li>
                    <li id="dateRangeLI1" runat="server">
                        <label for="">Start date</label>
                        
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
                    </li>
                    <li id="dateRangeLI2" runat="server">
                        <label for="">To date</label>

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
                            </tr>
                        </table>
                    </li>
                    <li id="birthdayLI" runat="server">
                        <label for="">Birthday</label>
                        <asp:DropDownList ID="birthdayDropDownList" runat="server">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label for="">Timezone</label>
                        <asp:DropDownList ID="timezoneDropDownList" runat="server"
                            DataTextField="Description" DataValueField="TimezoneID">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="timezoneRequiredFieldValidator" runat="server"
                        ControlToValidate="timezoneDropDownList" ErrorMessage="A timezone is required" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="">Private to do</label>
                        <asp:CheckBox ID="privateEventCheckbox" runat="server" />
                    </li>
                    <li>
                        <label for="">Must do</label>
                        <asp:CheckBox ID="mustDoCheckBox" runat="server" />
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
                </ol>                        
            </fieldset>
		</div>
    
        <div class="buttons">
            <asp:LinkButton 
                ID="saveChangesButton" runat="server" ToolTip="save" Text="Save" 
                OnClick="saveChangesButton_click" CssClass="button-sml" />
            <asp:LinkButton 
                ID="backButton" runat="server" ToolTip="save" Text="Back to to do details" 
                OnClick="backButton_click" CssClass="button-sml" CausesValidation="false" />
        </div>
    
    </div>
    </form>
</body>
</html>

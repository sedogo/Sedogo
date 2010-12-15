<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eventAlerts.aspx.cs" Inherits="eventAlerts" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="favicon.ico" >  
	<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
	<meta http-equiv="content-script-type" content="text/javascript" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="0" />
	<meta http-equiv="pragma" content="no-cache" />

	<title>Goal alerts : Sedogo : Create your future and connect with others to make it happen</title>

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
	
<script language="javascript" type="text/javascript">
function PickerAlertDate_OnChange()
{
    <%= CalendarAlertDate.ClientID.Replace("$","_").Replace(":","_") %>.SetSelectedDate(<%= PickerAlertDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate());
}
function CalendarAlertDate_OnChange()
{
    <%= PickerAlertDate.ClientID.Replace("$","_").Replace(":","_") %>.SetSelectedDate(<%= CalendarAlertDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate());
}
function popupCalendarAlertDate(image)
{
    if( <%= CalendarAlertDate.ClientID.Replace("$","_").Replace(":","_") %>.GetSelectedDate() == null )
    {
        <%= CalendarAlertDate.ClientID.Replace("$","_").Replace(":","_") %>.ClearSelectedDate();
    }
    <%=CalendarAlertDate.ClientObjectId%>.Show(image);    
}
</script>
	
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ValidationSummary runat="server" ID="validationSummary" 
            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
            HeaderText="Please review the following errors:" />
    
        <div id="event-detail">
            <div class="right-col">
                <h3 ID="currentAlertsHeader" runat="server">Reminders</h3>
                <asp:PlaceHolder ID="currentAlertsPlaceholder" runat="server" />
            </div>
            <div class="left-col">
		        <h1 style="">Reminders for <asp:Literal ID="eventTitleLabel" runat="server" /></h1>
		        <p><asp:Label ID="eventDescriptionLabel" runat="server" /><br />
		        <i><asp:Label ID="eventOwnersNameLabel" runat="server" /></i><br />
		        <asp:Label ID="eventDateLabel" runat="server" /></p>

                <p><b>Add new reminder:</b></p>

                <table>
                    <tr>
                        <td>
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td><ComponentArt:Calendar ID="PickerAlertDate" 
                                        runat="server" 
                                        ClientSideOnSelectionChanged="PickerAlertDate_OnChange"
                                        ControlType="Picker" 
                                        PickerCssClass="picker" 
                                        ImagesBaseUrl="./images/calendarImages/"
                                        PickerCustomFormat="dd/MM/yyyy" PickerFormat="Custom" 
                                        PopUpExpandControlId="calendarButton">
                                    </ComponentArt:Calendar>
                                    <ComponentArt:Calendar ID="CalendarAlertDate" 
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
                                        ClientSideOnSelectionChanged="CalendarAlertDate_OnChange" 
                                        PopUp="Custom" 
                                        PopUpExpandControlId="calendarButton">
                                    </ComponentArt:Calendar></td>
                                    <td><img alt="Click for pop-up calendar" 
                                        id="Img4" 
                                        onclick="popupCalendarAlertDate(this)" 
                                        class="calendar_button" 
                                        src="./images/calendarImages/btn_calendar.gif" 
                                        width="25" height="22" /></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:DropDownList ID="alertDatePickList" runat="server"
                                AutoPostBack="true" OnSelectedIndexChanged="alertDatePickList_changed">
                                <asp:ListItem Text="" Value="" />
                                <asp:ListItem Text="1 day" Value="1D" />
                                <asp:ListItem Text="1 week" Value="1W" />
                                <asp:ListItem Text="1 month" Value="1M" />
                                <asp:ListItem Text="3 months" Value="3M" />
                                <asp:ListItem Text="6 months" Value="6M" />
                                <asp:ListItem Text="1 year" Value="1Y" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                
                <asp:TextBox ID="newAlertTextBox" runat="server" TextMode="MultiLine"
                    Width="300px" Rows="6" /><asp:RequiredFieldValidator ID="firstNameTextBoxValidator" runat="server"
                    ControlToValidate="newAlertTextBox" ErrorMessage="Reminder description is required"
                    Display="None" SetFocusOnError="true">
                    </asp:RequiredFieldValidator>

                <p><asp:LinkButton ID="createAlertLink" runat="server" Text="Create reminder"
                    OnClick="createAlertLink_click" CssClass="button-sml" /></p>
		    </div>
		</div>

    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

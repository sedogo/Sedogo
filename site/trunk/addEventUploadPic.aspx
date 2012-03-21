<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addEventUploadPic.aspx.cs" Inherits="addEventUploadPic" %>
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
function setReminderDate()
{
	var form = document.forms[0];
	var d = form.alertDatePickList.options[form.alertDatePickList.selectedIndex].value;
	if( d == "1D" )
	{
	    PickerAlertDate.SetSelectedDate(new Date(<asp:Literal ID="Date1DValue1" runat="server" />));
	    CalendarAlertDate.SetSelectedDate(new Date(<asp:Literal ID="Date1DValue2" runat="server" />));
	}
	if( d == "1W" )
	{
	    PickerAlertDate.SetSelectedDate(new Date(<asp:Literal ID="Date1WValue1" runat="server" />));
	    CalendarAlertDate.SetSelectedDate(new Date(<asp:Literal ID="Date1WValue2" runat="server" />));
	}
	if( d == "1M" )
	{
	    PickerAlertDate.SetSelectedDate(new Date(<asp:Literal ID="Date1MValue1" runat="server" />));
	    CalendarAlertDate.SetSelectedDate(new Date(<asp:Literal ID="Date1MValue2" runat="server" />));
	}
	if( d == "3M" )
	{
	    PickerAlertDate.SetSelectedDate(new Date(<asp:Literal ID="Date3MValue1" runat="server" />));
	    CalendarAlertDate.SetSelectedDate(new Date(<asp:Literal ID="Date3MValue2" runat="server" />));
	}
	if( d == "6M" )
	{
	    PickerAlertDate.SetSelectedDate(new Date(<asp:Literal ID="Date6MValue1" runat="server" />));
	    CalendarAlertDate.SetSelectedDate(new Date(<asp:Literal ID="Date6MValue2" runat="server" />));
	}
	if( d == "1Y" )
	{
	    PickerAlertDate.SetSelectedDate(new Date(<asp:Literal ID="Date1YValue1" runat="server" />));
	    CalendarAlertDate.SetSelectedDate(new Date(<asp:Literal ID="Date1YValue2" runat="server" />));
	}
}
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
        <asp:ValidationSummary runat="server" ID="validationSummary" 
            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
            HeaderText="Please review the following errors:" />
    
	    <div id="modal">
	        <h1>Create a goal</h1>
	        
            <h3 class="blue">Want to add a picture with <asp:Label ID="eventNameLabel" runat="server" />?</h3>
            <p>It's easy. Choose your picture and upload it in seconds.</p>
            
            <fieldset>
                <ol>
                    <li>
                        <asp:FileUpload ID="eventPicFileUpload" runat="server" />
                    </li>
                </ol>
            </fieldset>

            <img src="images/popupLine.png" alt="" class="line-divider" />
    
            <h3 class="blue">Want to be reminded about <asp:Label ID="eventNameLabel2" runat="server" />?</h3>
            <p>Select a date to receive an alert about this goal</p>

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
                    </tr>
                    <tr>
                    <td>
                        <asp:DropDownList ID="alertDatePickList" runat="server">
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
            <p style="margin-top: 20px">Reminder message</p>
            <asp:TextBox ID="newAlertTextBox" runat="server" TextMode="MultiLine"
                Width="233px" Rows="4" />

            <img src="images/popupLine.png" alt="" class="line-divider" />

			<p class="required-field">&#42; Required field</p>
		</div>
    
        <div class="buttons">
            <asp:LinkButton id="saveChangesButton" runat="server" OnClick="saveChangesButton_click" 
                Text="Next" CssClass="button-lrg" />
            <asp:LinkButton
                ID="skipUploadButton" runat="server" ToolTip="Skip" Text="Skip" 
                OnClick="skipUploadButton_click" CssClass="button-lrg" CausesValidation="false" />
        </div>

    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

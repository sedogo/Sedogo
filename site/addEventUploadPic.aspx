<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addEventUploadPic.aspx.cs" Inherits="addEventUploadPic" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

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

            <img src="images/popupLine.png" alt="" />
    
            <h3 class="blue">Want to be reminded about <asp:Label ID="Label1" runat="server" />?</h3>
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
                Width="300px" Rows="6" />

            <img src="images/popupLine.png" alt="" />

            <p>*Required field</p>    
		</div>
    
        <div class="buttons">
            <asp:LinkButton id="saveChangesButton" runat="server" OnClick="saveChangesButton_click" 
                Text="Next" CssClass="button-sml" />
            <asp:LinkButton Visible="false"
                ID="skipUploadButton" runat="server" ToolTip="Skip" Text="Skip" 
                OnClick="skipUploadButton_click" CssClass="button-sml" CausesValidation="false" />
        </div>

    </div>
    </form>
</body>
</html>
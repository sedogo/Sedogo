<%@ Control Language="C#" AutoEventWireup="true" CodeFile="eventsListControl.ascx.cs" 
    Inherits="components_eventsListControl" %>

<SCRIPT TYPE="text/javascript">
function setColor(id, newColor)
{
    document.getElementById(id).style.background = newColor;
}
function showhideGoal(layer_ref)
{
    if (document.all)
    { //IS IE 4 or 5 (or 6 beta)
        eval("document.all." + layer_ref + ".style.display = 'block'");
    }
    if (document.layers)
    { //IS NETSCAPE 4 or below
        document.layers[layer_ref].display = 'block';
    }
    if (document.getElementById && !document.all)
    {
        hza = document.getElementById(layer_ref);
        hza.style.display = 'block';
    }
}
</script>     

<div class="one-col">
    <div class="events">
	    <h2 class="col-header">This month</h2>
	    <asp:Label ID="overdueTitleLabel" runat="server" Text="Overdue" />
	    <asp:PlaceHolder ID="overdueEventsPlaceHolder" runat="server" />
	    <asp:Label ID="todaysDateLabel" runat="server" />
	    <asp:PlaceHolder ID="todayEventsPlaceHolder" runat="server" />
	    <asp:Label ID="thisWeekTitleLabel" runat="server" Text="This week" />
	    <asp:PlaceHolder ID="thisWeekEventsPlaceHolder" runat="server" />
	    <asp:Label ID="thisMonthTitleLabel" runat="server" Text="This month" />
	    <asp:PlaceHolder ID="thisMonthEventsPlaceHolder" runat="server" />
    </div>
</div>
<div class="one-col">
    <div class="events">
	    <h2 class="col-header">Next 5 yrs</h2>
	    <asp:Label ID="thisYearTitleLabel" runat="server" Text="This year" />
	    <asp:PlaceHolder ID="nextYearEventsPlaceHolder" runat="server" />
	    <asp:Label ID="next2YearsTitleLabel" runat="server" Text="Next 2 years" />
	    <asp:PlaceHolder ID="next2YearsEventsPlaceHolder" runat="server" />
	    <asp:Label ID="next3YearsTitleLabel" runat="server" Text="Next 3 years" />
	    <asp:PlaceHolder ID="next3YearsEventsPlaceHolder" runat="server" />
	    <asp:Label ID="next4YearsTitleLabel" runat="server" Text="Next 4 years" />
	    <asp:PlaceHolder ID="next4YearsEventsPlaceHolder" runat="server" />
	    <asp:Label ID="next5YearsTitleLabel" runat="server" Text="Next 5 years" />
	    <asp:PlaceHolder ID="next5YearsEventsPlaceHolder" runat="server" />
    </div>
</div>
<div class="one-col-end">
    <div class="events">
	    <h2 class="col-header">5 yrs +</h2>
	    <asp:Label ID="fiveToTenYearsTitleLabel" runat="server" Text="5-10 years" />
	    <asp:PlaceHolder ID="next10YearsEventsPlaceHolder" runat="server" />
	    <asp:Label ID="tenToTwentyYearsTitleLabel" runat="server" Text="10-20 years" />
	    <asp:PlaceHolder ID="next20YearsEventsPlaceHolder" runat="server" />
	    <asp:Label ID="twentyPlusYearsTitleLabel" runat="server" Text="20+ years" />
	    <asp:PlaceHolder ID="next100YearsEventsPlaceHolder" runat="server" />
	    <asp:Label ID="unknownDateTitleLabel" runat="server" Text="Unknown" />
	    <asp:PlaceHolder ID="notScheduledEventsPlaceHolder" runat="server" />
    </div>
</div>

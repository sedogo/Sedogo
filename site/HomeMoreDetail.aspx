<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeMoreDetail.aspx.cs" Inherits="HomeMoreDetail"
    EnableEventValidation="false" ValidateRequest="false" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <meta name="google-site-verification" content="9MltlA8eLNS_PsMbFJZM0zWmAAIKbVkimMAhAsczoLg" />
    <link rel="shortcut icon" href="favicon.ico" />
    <meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
    <meta http-equiv="content-script-type" content="text/javascript" />
    <meta http-equiv="content-style-type" content="text/css" />
    <meta http-equiv="expires" content="never" />
    <title>Create your future and connect with others to make it happen</title>
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <meta name="copyright" content="" />
    <meta name="robots" content="" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <meta http-equiv="imagetoolbar" content="no" />
    <meta http-equiv="Cleartype" content="Cleartype" />
    <link rel="stylesheet" href="css/main.css" />
    <link rel="stylesheet" href="css/style.css" />
    <!--[if gte IE 6]>
		<link rel="stylesheet" href="css/main_lte-ie-6.css" />
	<![endif]-->
    <!--[if IE]>
		<link rel="stylesheet" href="css/main_ie.css" />
	<![endif]-->
    <link href="css/flexcrollstyles.css" rel="stylesheet" />
    <link href="css/tutorsty.css" rel="stylesheet" />

    <script type="text/javascript" src="js/DD_roundies_0.0.2a-min.js"></script>

    <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>

    <script type="text/javascript" src="js/jquery-ui-1.7.2.custom.min.js"></script>

    <script type="text/javascript" src="js/ui.dialog.js"></script>

    <script type="text/javascript" src="js/jquery.cookie.js"></script>

    <script type="text/javascript" src="js/jquery.livequery.js"></script>

    <script type="text/javascript" src="js/jquery.corner.js"></script>

    <script type="text/javascript" src="js/main.js"></script>

    <script type="text/javascript" src="utils/validationFunctions.js"></script>

    <script type="text/javascript">
		Timeline_ajax_url = "js/timeline/timeline_ajax/simile-ajax-api.js";
		Timeline_urlPrefix = 'js/timeline/timeline_js/';
		Timeline_parameters = 'bundle=true';
	</script>

    <script src="js/timeline/timeline_js/timeline-api.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/examples_modified.js"></script>

    <script type="text/javascript">
			var tl;
			var theme1;

			var today = new Date();
			var tomorrow = new Date();
			var sixMonthsAgo = new Date();
			tomorrow.setDate(today.getDate()+1);
			sixMonthsAgo.setDate(today.getDate()-182)

			function onLoad() {
				//Initiate timeline
				initiateTimeline();
			
				//Set up filter controls
				setupFilterHighlightControls(document.getElementById("controls"), tl, [1, 2], theme1);

				//Filter based on current cookie values after timelines load
				performFiltering(tl, [1, 2], document.getElementById("table-filter"));

				//Bug fix: scroll timeline programatically to trigger correct auto-height
				tl.getBand(0)._autoScroll(1);
				
				$('#imgMngT').click();
			}

			function initiateTimeline() {
				var eventSource = new Timeline.DefaultEventSource();

				theme1 = Timeline.ClassicTheme.create();
				theme1.autoWidth = true; // Set the Timeline's "width" automatically. Setting autoWidth on the timelines first bands theme, will affect all bands.

				var bandInfos = [
					Timeline.createBandInfo({
						width: "0",
						theme: theme1
					}),
					Timeline.createBandInfo({
						date: "<asp:Literal id="timelineStartDate1" runat="server" />",
						width: "250",
						intervalUnit: Timeline.DateTime.MONTH,
						intervalPixels: 50,
						theme: theme1,
						eventSource: eventSource,
						zoomIndex: 10,
						zoomSteps: new Array(
							{ pixelsPerInterval: 280, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 140, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 70, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 35, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 400, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 200, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 100, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 50, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 400, unit: Timeline.DateTime.MONTH },
							{ pixelsPerInterval: 200, unit: Timeline.DateTime.MONTH },
							{ pixelsPerInterval: 100, unit: Timeline.DateTime.MONTH} // DEFAULT zoomIndex
						)
					}),
					Timeline.createBandInfo({
						date: "<asp:Literal id="timelineStartDate2" runat="server" />",
						width: "70",
						intervalUnit: Timeline.DateTime.YEAR,
						intervalPixels: 100,
						showEventText: false,
						trackHeight: 0.5,
						trackGap: 0.2,
						theme: theme1,
						eventSource: eventSource,
						overview: true
					})
				];
				//Synchronise band scrolling
				bandInfos[2].syncWith = 1;
				bandInfos[2].highlight = true;

				//Generate marker for today
				for (var i = 0; i < bandInfos.length; i++) {
					bandInfos[i].decorators = [
						new Timeline.SpanHighlightDecorator({
							startDate: today,
							endDate: tomorrow,
                    		color: "#CCCCCC",
                    		opacity: 50,
                    		startLabel: "",
                    		// theme:      theme,
                    		cssClass: 't-highlight1'
						})
					];
				}

/*
				bandInfos[0].decorators = [
					new Timeline.SpanHighlightDecorator({
						startDate: sixMonthsAgo,
						endDate: sixMonthsAgo,
                   		color: "#FF0000",
                   		opacity: 50,
                   		endLabel: "My timeline",
                   		// theme:      theme,
                   		cssClass: 't-highlight2'
					})
				];
*/
				//Create timeline and load data
				tl = Timeline.create(document.getElementById("my-timeline"), bandInfos);
				var url = "<asp:Literal id="timelineURL" runat="server" />";
				Timeline.loadXML(url, function(xml, url) { eventSource.loadXML(xml, url); });
			}

			var resizeTimerID = null;
			function onResize() {
				if (resizeTimerID == null) {
					resizeTimerID = window.setTimeout(function() {
						resizeTimerID = null;
						tl.layout();
					}, 500);
				}
			}   		
    </script>

    <script type="text/javascript">
	$(document).ready(function(){
	});
    function breakout_of_frame()
    {
      if (top.location != location)
      {
        top.location.href = document.location.href ;
      }
    }
    function getElementID( name )
    {
	    var form = document.forms[0];
        var nID = -1;
        for( i=0 ; i < form.elements.length ; i++ )
        {
            if( form.elements[i].name == name )
            {
                nID = i;
            }
        }
        return nID;
    }
    function loginRedirect(eventID)
    {
        openModal("login.aspx?EID=" + eventID);
    }
    function openEvent(eventID) 
    {
        openModal("viewEvent.aspx?EID=" + eventID);
    }
    function doAddEvent()
    {
        openModal("login.aspx");
    }
    function checkAddButtonEnter(e)
    {
        var characterCode;
        if (e && e.which) // NN4 specific code
        {
            e = e;
            characterCode = e.which;
        }
        else
        {
            e = event;
            characterCode = e.keyCode; // IE specific code
        }
        if (characterCode == 13) //// Enter key is 13
        {
            e.returnValue = false;
            e.cancelBubble = true;
            doAddEvent();
        }
        else
        {
            return false;
        }
    }
    
    
    function ShowHideDiv(divId)
     {
        var shdiv = document.getElementsByTagName('div');
        for(i=0; i < shdiv.length; i++)
        {
            if(shdiv.item(i).id.indexOf("dmpop") > -1)
            {
                document.getElementById(shdiv.item(i).id).style.display='none';
            }
        }
		document.getElementById('dmpop' + divId).style.display='block';
	 }
	 
    function CloseDiv()
     {
        var shdiv = document.getElementsByTagName('div');
        for(i=0; i < shdiv.length; i++)
        {
            if(shdiv.item(i).id.indexOf("dmpop") > -1)
            {
                document.getElementById(shdiv.item(i).id).style.display='none';
            }
        }		
	 }
    
    </script>

    <script type="text/JavaScript">
        DD_roundies.addRule('.timeline-event-tape', '15px', true);
    </script>

</head>
<body onload="breakout_of_frame();onLoad();" onresize="onResize();">

    <script src="js/flexcroll-uncompressed.js" type="text/javascript"></script>

    <form id="defaultForm" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server">
    </asp:ScriptManager>
    <div>
        <div id="container">
            <ul id="account-options">
                <Sedogo:BannerLoginControl ID="bannerLogin" runat="server" />
            </ul>
            <div class="one-col">
                <a href="profile.aspx" title="sedogo : home">
                    <img src="images/sedogo.gif" title="sedogo" alt="sedogo logo" id="logo" /></a>
                <p class="strapline">
                    Create your future and connect<br />
                    with others to make it happen
                </p>
            </div>
            <div class="three-col">
                <table border="0" cellspacing="10" cellpadding="0" width="100%" class="add-find">
                    <tr>
                        <td>
                            <h3 class="blue">
                                <a href="login.aspx" class="modal">Add</a></h3>
                            <p class="blue">
                                to my goal list</p>
                            <asp:Panel ID="Panel1" runat="server">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <asp:TextBox ID="what" runat="server" Text="" MaxLength="1000" />
                                        </td>
                                        <td valign="top" style="padding-top: 4px">
                                            <a href="login.aspx" class="modal">
                                                <asp:Image ID="searchButton1" runat="server" ImageUrl="~/images/addButton.png" /></a>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>
                            <h3 class="blue">
                                <asp:LinkButton ID="findButton" runat="server" Text="Find" OnClick="searchButton_click" /></h3>
                            <p class="blue">
                                people with my goals</p>
                            <asp:Panel ID="Panel2" DefaultButton="searchButton2" runat="server">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <asp:TextBox ID="what2" runat="server" Text="" MaxLength="1000" ValidationGroup="what2Group" />
                                        </td>
                                        <td valign="top" style="padding-top: 4px">
                                            <asp:ImageButton ID="searchButton2" runat="server" OnClick="searchButton_click" ImageUrl="~/images/searchButton.png" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:RegularExpressionValidator ID="what2Validator" runat="server" ErrorMessage="Goal name must have at least 2 characters"
                                    ControlToValidate="what2" ValidationGroup="what2Group" ValidationExpression="[\S\s]{2,200}" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="timelines">
                <div id="tools">
                    <ul class="timeline-options">
                        <%--<li class="first"><a href="#" title="View all" id="view-all">View all</a></li>--%>
                        <%--<li class="last"><a href="#" title="Show goal categories" id="show-categories">Show
                            goal categories</a></li>--%>
                    </ul>
                    <div id="buttons">
                        <%--<a href="#" title="Scroll left" class="left" id="scroll-back">
                            <img src="images/left.gif" title="Scroll left" alt="Left arrow" /></a><a href="#"
                                title="Scroll right" class="right" id="scroll-forward"><img src="images/right.gif"
                                    title="Scroll right" alt="Left arrow" /></a>--%>
                    </div>
                </div>
                <div style="height: 2px; background: #fff url(js/timeline/timeline_js/images/bg_timeline-header.gif) repeat-x bottom;">
                </div>
                <div class="outer_div">
                    <div class="goal_cat_bg">
                        <div class="goal_cat_bg_col1">
                            <a href="#" title="Show goal categories" id="show-categories">
                                <img src="images/goalcategoryclosed.png" alt="" width="137" height="33" /></a></div>
                        <div class="goal_cat_bg_col2">
                            <div class="zoom_minus">
                                <img id="ZoomOut" src="images/minus.png" alt="" width="26" height="26" onmouseover="this.src='images/minus_rollover.png'; this.style.cursor='pointer';"
                                    onmouseout="this.src='images/minus.png'" /></div>
                            <div class="zoom_text">
                                <img src="images/zoom.jpg" alt="" /></div>
                            <div class="zoom_plus">
                                <img id="ZoomIn" src="images/plus.png" onmouseover="this.src='images/plus_rollover.png'; this.style.cursor='pointer';"
                                    onmouseout="this.src='images/plus.png'" alt="" width="26" height="26" /></div>
                        </div>
                        <div class="goal_cat_bg_col3">
                            <div class="arrow_strip">
                                <div class="arrow_previous">
                                    <a href="#" title="Scroll left" class="left" id="scroll-back">
                                        <img src="images/arrow_previous.jpg" alt="" /></a></div>
                                <div class="arrow_midbg">
                                    <div id="smallScroll" class="inner_arrow_midbg">
                                    </div>
                                </div>
                                <div class="arrow_next">
                                    <a href="#" title="Scroll right" class="right" id="scroll-forward">
                                        <img src="images/arrow_next.jpg" alt="" /></a></div>
                                <div id="dateRange" style="text-align: center;">
                                </div>
                            </div>
                        </div>
                        <div class="goal_cat_bg_co14">
                            <a href="help.aspx" title="help" class="modal">
                                <img src="images/help.png" onmouseover="this.src='images/help_rollover.png'; this.style.cursor='pointer';"
                                    onmouseout="this.src='images/help.png'" alt="" /></a></div>
                    </div>
                    <div class="timeline">
                        <div class="float_left">
                        </div>
                        <div class="float_right">
                            <img id="imgMngT" src="images/T_Close.jpg" title="Open/Close timeline" alt="Open/Close timeline"
                                style="cursor: pointer;" /></div>
                    </div>
                </div>
                <div class="tl-container" id="my-container" style="clear: both;">
                    <div id="my-timeline">
                    </div>
                    <noscript>
                        This page uses Javascript to show you a Timeline. Please enable Javascript in your
                        browser to see the full page. Thank you.
                    </noscript>
                </div>
            </div>
            <div class="controls" id="controls">
                <a href="#" class="close-controls">
                    <img src="images/close-controls.gif" title="Close controls" alt="Close controls" /></a>
            </div>
            <div id="other-content">
                <div class="one-col" style="background-color: #EFEFEF; height: 316px;">
                    <div style="float: left; padding-left: 10px; padding-top: 15px;">
                        <div style="color: #00BFFD; font-weight: bold; font-size: 14px; margin-bottom: 2px;">
                            Latest Achieved Goals
                        </div>
                        <div>
                            <% = BindLatestAchievedGoals() %>
                        </div>
                    </div>
                    <div style="float: left; padding-left: 10px; padding-top: 15px; margin-top: 15px;">
                        <div style="color: #00BFFD; font-weight: bold; font-size: 14px; margin-bottom: 2px;">
                            Goals Happening Today
                        </div>
                        <div>
                            <%= BindGoalsHappeningToday() %>
                        </div>
                    </div>
                </div>
                <div style="width: 720px; float: left;">
                    <div style="width: 100%; height: 15px; background-color: #00CDFF; padding: 5px; padding-top: 2px;">
                        <div style="color: White; font-size: 14px; font-weight: bold; width: 50%; float: left;">
                            Latest updated goals</div>
                        <div style="text-align: right; width: 50%; padding: 0px; float: left;">
                            <asp:Button runat="server" BackColor="white" ID="backButton" BorderStyle="None" OnClick="backButton_Click"
                                Text="Back" /></div>
                    </div>
                    <div style="width: 100%">
                    </div>
                    <div style="width: 100%">
                        <h3 style="color: #00CDFF; margin: 0px; margin-top: 10px; margin-bottom: 5px;">
                            Today</h3>
                        <asp:PlaceHolder ID="PlaceHolderToday" runat="server" />
                        <h3 style="color: #00CDFF; margin: 0px; margin-top: 10px; margin-bottom: 5px;">
                            This Week</h3>
                        <asp:PlaceHolder ID="PlaceHolderThisWeek" runat="server" />
                        <h3 style="color: #00CDFF; margin: 0px; margin-top: 10px; margin-bottom: 5px;">
                            Two Weeks Ago</h3>
                        <asp:PlaceHolder ID="PlaceHolderTwoWeekAgo" runat="server" />
                        <h3 style="color: #00CDFF; margin: 0px; margin-top: 10px; margin-bottom: 5px;">
                            Last Month</h3>
                        <asp:PlaceHolder ID="PlaceHolderLastMonth" runat="server" />
                        <h3 style="color: #00CDFF; margin: 0px; margin-top: 10px; margin-bottom: 5px;">
                            Last Six Month</h3>
                        <asp:PlaceHolder ID="PlaceHolderLastSixMonth" runat="server" />
                    </div>
                </div>
            </div>
            <div id="footer">
                <ul>
                    <li class="first">&copy; Sedogo Ltd 2008-2010</li>
                    <li><a href="about.aspx" title="About" class="modal">About</a></li>
                    <li><a href="faq.aspx" title="FAQ" class="modal">FAQ</a></li>
                    <li><a href="privacy.aspx" title="Privacy Policy" class="modal">Privacy Policy</a></li>
                    <li class="last"><a href="feedback.aspx" title="Feedback" class="modal">Feedback</a></li>
                </ul>
                <div style="text-align: right; margin-top: -25px">
                    <div style="color: #0cf">
                        Follow us <a target="_blank" style="padding-left: 7px" href="http://www.facebook.com/pages/Sedogo/261533591696">
                            <img src="images/facebook.gif" /></a> <a style="padding-left: 7px" target="_blank"
                                href="http://twitter.com/Sedogo">
                                <img src="images/twitter.gif" /></a></div>
                </div>
            </div>
        </div>
        <div id="modal-container">
            <a href="#" class="close-modal">
                <img src="images/close-modal.gif" title="Close window" alt="Close window" /></a>
            <iframe frameborder="0"></iframe>
        </div>
        <div id="modal-background">
        </div>
    </div>
    </form>

    <script type="text/javascript">
    var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
    document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>

    <script type="text/javascript">
    try
    {
        var pageTracker = _gat._getTracker("UA-12373356-1");
        pageTracker._trackPageview();
    } catch (err) { }
    </script>

</body>
</html>
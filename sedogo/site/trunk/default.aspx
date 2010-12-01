<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
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
    <link href="css/cadialog.css" rel="stylesheet" />

    <script type="text/javascript" src="js/DD_roundies_0.0.2a-min.js"></script>

    <script type="text/javascript" src="js/dom-drag.js"></script>

    <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>

    <script type="text/javascript" src="js/jquery-ui-1.7.2.custom.min.js"></script>

    <script type="text/javascript" src="js/ui.dialog.js"></script>

    <script type="text/javascript" src="js/jquery.cookie.js"></script>

    <script type="text/javascript" src="js/jquery.livequery.js"></script>

    <script type="text/javascript" src="js/jquery.corner.js"></script>

    <script type="text/javascript" src="js/main.js"></script>

    <script type="text/javascript" src="js/validationFunctions.js"></script>

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
						width: "235",
						intervalUnit: Timeline.DateTime.YEAR,
						intervalPixels: 100,
						theme: theme1,
						eventSource: eventSource,
						zoomIndex: 14,
						zoomSteps: new Array(
						    { pixelsPerInterval: 280, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 140, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 70, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 35, unit: Timeline.DateTime.HOUR },
							{ pixelsPerInterval: 400, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 200, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 100, unit: Timeline.DateTime.DAY },
							{ pixelsPerInterval: 50, unit: Timeline.DateTime.DAY },							
							{ pixelsPerInterval: 40, unit: Timeline.DateTime.DAY },//New	
							{ pixelsPerInterval: 400, unit: Timeline.DateTime.MONTH },
							{ pixelsPerInterval: 200, unit: Timeline.DateTime.MONTH },
							{ pixelsPerInterval: 100, unit: Timeline.DateTime.MONTH },// DEFAULT zoomIndex old						
							{ pixelsPerInterval: 50, unit: Timeline.DateTime.MONTH },//New
							{ pixelsPerInterval: 100, unit: Timeline.DateTime.YEAR }//New							
						)
					}),
					
					Timeline.createBandInfo({
						date: "<asp:Literal id="timelineStartDate2" runat="server" />",
						width: "70",
						intervalUnit: Timeline.DateTime.YEAR,
						intervalPixels: 35,
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
        $(document).ready(function() {
        });
        function breakout_of_frame() {
            if (top.location != location) {
                top.location.href = document.location.href;
            }
        }
        function getElementID(name) {
            var form = document.forms[0];
            var nID = -1;
            for (i = 0; i < form.elements.length; i++) {
                if (form.elements[i].name == name) {
                    nID = i;
                }
            }
            return nID;
        }
        function loginRedirect(eventID) {
            location.href = "login.aspx?EID=" + eventID;
        }
        function openEvent(eventID) {
            location.href = "viewEvent.aspx?EID=" + eventID;
        }
        function viewProfile(eventUserID) {
            //location.href = "userProfile.aspx?UID=" + eventUserID;
            location.href = "publicProfile.aspx?UID=" + eventUserID;
        }
        function viewUserTimeline(eventUserID) {
            //location.href = "userTimeline.aspx?UID=" + eventUserID;
            window.location = "login.aspx?UTID=" + eventUserID;
        }
        function doSendMessage(srhUserID) {
            //openModal("sendUserMessage.aspx?EID=-1&UID=" + srhUserID);
            window.location = "login.aspx";
        }
        function doAddEvent() {
            location.href = "login.aspx";
        }
        function checkAddButtonEnter(e) {
            var characterCode;
            if (e && e.which) // NN4 specific code
            {
                e = e;
                characterCode = e.which;
            }
            else {
                e = event;
                characterCode = e.keyCode; // IE specific code
            }
            if (characterCode == 13) //// Enter key is 13
            {
                e.returnValue = false;
                e.cancelBubble = true;
                doAddEvent();
            }
            else {
                return false;
            }
        }

        function ShowHideDiv(divId) {
            var shdiv = document.getElementsByTagName('div');
            for (i = 0; i < shdiv.length; i++) {
                if (shdiv.item(i).id.indexOf("dmpop") > -1) {
                    document.getElementById(shdiv.item(i).id).style.display = 'none';
                }
            }
            document.getElementById('dmpop' + divId).style.display = 'block';
        }

        function CloseDiv() {
            var shdiv = document.getElementsByTagName('div');
            for (i = 0; i < shdiv.length; i++) {
                if (shdiv.item(i).id.indexOf("dmpop") > -1) {
                    document.getElementById(shdiv.item(i).id).style.display = 'none';
                }
            }
        }
    function changeClass(id, newClass)
    {
        //document.getElementById(id).className = newClass; //.setAttribute("class", newClass);
    }
    function showTour1()
    {
        tourDialog2.Close();
        tourDialog3.Close();
        tourDialog4.Close();
        tourDialog5.Close();
        tourDialog1.Show();
    }
    function showTour2()
    {
        tourDialog1.Close();
        tourDialog3.Close();
        tourDialog4.Close();
        tourDialog5.Close();
        tourDialog2.Show();
    }
    function showTour3()
    {
        tourDialog1.Close();
        tourDialog2.Close();
        tourDialog4.Close();
        tourDialog5.Close();
        tourDialog3.Show();
    }
    function showTour4()
    {
        tourDialog1.Close();
        tourDialog2.Close();
        tourDialog3.Close();
        tourDialog5.Close();
        tourDialog4.Show();
    }
    function showTour5()
    {
        tourDialog1.Close();
        tourDialog2.Close();
        tourDialog3.Close();
        tourDialog4.Close();
        tourDialog5.Show();
    }
    if (document.images)
    {
        button1 = new Image;
        button2 = new Image;
        button1.src = 'images/takethetour_over.jpg'
        button2.src = 'signup_over.jpg'
    }
    function toggleTimeline()
    {
        $('#imgMngT').click();
    }
</script>

    <style type="text/css">
        .itemTemplate
        {
            border: 0px; /*solid 1px #dff3ff;*/
            height: 216px;
            width: 232px;
            margin: 0px; /*5px;*/
        }
        .rotatorBackground
        {
            float: left;
            margin-left: 0px; /*50px;*/
            margin-top: 0px; /*15px;*/
            width: 232px;
            height: 216px;
            border: 0; /*solid 2px #dedede;*/
            font-family: Arial;
            -moz-border-radius: 0px; /*15px;*/
            -webkit-border-radius: 0px; /*15px;*/
            _margin-left: 0px; /*25px;*/ /* IE6 hack*/
            _margin-top: 0px; /*7px;*/ /* IE6 hack*/
        }
        .horizontalRotator
        {
            margin-top: 0px; /*110px;*/
            margin-left: auto;
            margin-right: auto;
            width: 232px;
            height: 216px;
        }
    </style>

    <script type="text/JavaScript">
        DD_roundies.addRule('.timeline-event-tape', '15px', true);
    </script>

</head>
<body onload="breakout_of_frame();onLoad(); scrl();" onresize="onResize();">
    <form id="defaultForm" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server">
    </asp:ScriptManager>
    <div>
        <div id="container">
            <Sedogo:BannerLoginControl ID="BannerLoginControl1" runat="server" />
            
            <div class="three-col">
            <table border="0" cellspacing="0" cellpadding="0" align="right" width="300" style="margin-top:75px">
                <tr>
                    <td><a href="register.aspx" class="signuprollover" title="signup"><span 
                        class="displace">Signup</span></a></td> 
                    <td><p class="bannerLoginSignup">or</p></td>            
                    <td><a href="javascript:showTour1()" class="takethetourrollover" title="takethetour"><span 
                        class="displace">Tour</span></a></td>      
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
                                        <img src="images/arrow_previous1.jpg" onmouseover="this.src='images/arrow_previous.jpg'; this.style.cursor='pointer';"
                                    onmouseout="this.src='images/arrow_previous1.jpg'" alt="" /></a></div>
                                <div class="arrow_midbg">
                                    <div id="smallScroll" class="inner_arrow_midbg">
                                    </div>
                                </div>
                                <div class="arrow_next">
                                    <a href="#" title="Scroll right" class="right" id="scroll-forward">
                                        <img src="images/arrow_next1.jpg" onmouseover="this.src='images/arrow_next.jpg'; this.style.cursor='pointer';"
                                    onmouseout="this.src='images/arrow_next1.jpg'" alt="" /></a></div>
                                <div id="dateRange" style="text-align: center;">
                                </div>
                            </div>
                        </div>
                        <div class="goal_cat_bg_co14">
                            <a href="help.aspx" title="help">
                                <img src="images/help.png" onmouseover="this.src='images/help_rollover.png'; this.style.cursor='pointer';"
                                    onmouseout="this.src='images/help.png'" alt="" /></a></div>
                    </div>
                    <div class="timeline">
                        <div class="float_left">
                        <h2>Goal timeline</h2>
                        </div>
                        <div class="float_right">
                            <table>
                                <tr>
                                    <td style="padding-top:3px"><a href="javascript:toggleTimeline();" class="timelinelink">Close timeline</a></td>
                                    <td>&nbsp;</td>
                                    <td><img id="imgMngT" src="images/T_Close.jpg" title="Open/Close timeline" alt="Open/Close timeline"
                                        style="cursor: pointer;" /></td>
                                </tr>
                            </table>
                        </div>
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
                <div class="one-col">
                <div>
                    
                    <h2><asp:Label ID="Label1" runat="server" Text="Recent activity" /></h2>
                    <asp:PlaceHolder ID="goalsAddedPlaceHolder" runat="server" />
                    
                </div>
                </div>
            
                <div class="one-col">
                    <h2 class="col-header">
                        Fancy a challenge?</h2>
                    <table>
                        <tr>
                            <td><img src="images/iphoneLogo.jpg" /></td>
                            <td>&nbsp;</td>
                            <td valign="top"><p class="teaser">Get the Sedogo iPhone app. <a href="http://itunes.apple.com/gb/app/sedogo/id403226892?mt=8" target="_blank">Download here</a></p></td>
                        </tr>
                    </table>
                        
                    <div style="margin-top:20px">
                    <asp:Literal ID="homePageContent" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="one-col">
                    <h2 class="col-header">
                        Latest members</h2>
                    <div style="float: left; width: 100%; height: 153px;">
                        <div style="margin-left: 2px;">
                            <asp:DataList ID="dlMember" runat="server" RepeatColumns="6" RepeatDirection="Horizontal"
                                DataKeyField="UserId" OnItemDataBound="dlMember_ItemDataBound">
                                <ItemTemplate>
                                    <div>
                                        <div class="misc-pop-up" id="dmpop<%# DataBinder.Eval(Container.DataItem, "userId") %>"
                                            style="display: none; position: relative; z-index: 500;">
                                            <div class="simileAjax-bubble-container simileAjax-bubble-container-pngTranslucent"
                                                style="width: 160px; height: 70px; left: 0px; bottom: 35px;">
                                                <div class="simileAjax-bubble-innerContainer simileAjax-bubble-innerContainer-pngTranslucent">
                                                    <div class="simileAjax-bubble-border-top-left simileAjax-bubble-border-top-left-pngTranslucent">
                                                    </div>
                                                    <div class="simileAjax-bubble-border-top-right simileAjax-bubble-border-top-right-pngTranslucent">
                                                    </div>
                                                    <div class="simileAjax-bubble-border-bottom-left simileAjax-bubble-border-bottom-left-pngTranslucent">
                                                    </div>
                                                    <div class="simileAjax-bubble-border-bottom-right simileAjax-bubble-border-bottom-right-pngTranslucent">
                                                    </div>
                                                    <div class="simileAjax-bubble-border-left simileAjax-bubble-border-left-pngTranslucent">
                                                    </div>
                                                    <div class="simileAjax-bubble-border-right simileAjax-bubble-border-right-pngTranslucent">
                                                    </div>
                                                    <div class="simileAjax-bubble-border-top simileAjax-bubble-border-top-pngTranslucent">
                                                    </div>
                                                    <div class="simileAjax-bubble-border-bottom simileAjax-bubble-border-bottom-pngTranslucent">
                                                    </div>
                                                    <div class="simileAjax-bubble-contentContainer simileAjax-bubble-contentContainer-pngTranslucent">
                                                        <div style="position: static; width: 160px; font-size: 14px;">
                                                            <div style="float: left; width: 160px;">
                                                                <%--<img width="25" height="25" alt="" src="assets/profilePics/<%# DataBinder.Eval(Container.DataItem, "ProfilePicThumbnail") %>"
                                                                    onerror="this.src='images/profile/blankProfile.jpg'" /><br />--%>
                                                                <span class="blue" style="line-height: 22px;">
                                                                    <a href="publicProfile.aspx?UID=<%# DataBinder.Eval(Container.DataItem, "UserId") %>"><%# DataBinder.Eval(Container.DataItem, "FirstName")%>
                                                                    &nbsp;<%# DataBinder.Eval(Container.DataItem, "LastName")%></a>
                                                                </span>
                                                                <br />
                                                                <%# DataBinder.Eval(Container.DataItem, "GCount") + " Goals"%><br />
                                                            </div>
                                                            <div style="width: 160px; float: left;">
                                                                <span class="blue" style="line-height: 27px;"><a href='javascript:viewUserTimeline(<%# DataBinder.Eval(Container.DataItem, "UserId") %>)'
                                                                    style="text-decoration: underline;">View timeline</a></span></div>
                                                        </div>
                                                    </div>
                                                    <div class="simileAjax-bubble-close simileAjax-bubble-close-pngTranslucent misc-pop-up-link-close">
                                                    </div>
                                                    <div class="simileAjax-bubble-arrow-point-down simileAjax-bubble-arrow-point-down-pngTranslucent"
                                                        style="left: 1px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Image ID="profilePicImage" runat="server" /></div>
                                </ItemTemplate>
                            </asp:DataList></div>
                    </div>
                    <div style="float: left; width: 100%; height: 216px;">
                        <div style="margin-left: 2px;">
                        
<table cellspacing="0" cellpadding="0" width="230" style="border: 2px solid #CCCCCC;">
	<tbody><tr style="background-color:#CCCCCC;">
		<td style="padding: 5px;"><span style="font-size: 120%; color: white;"><b>Sedogo supports</b></span></td>
	</tr>
	<tr>
		<td>
			<table cellspacing="5" cellpadding="0" border="0">			
				<tbody><tr>
				    <td><a href="http://www.actionforchildren.org.uk/content/74/What-you-can-do"><img src="/images/AFC_Landscape_Generic_rgb20.jpg"></a></td>
				    <td style="padding-top:32px"><span style="font-size: 80%;"><a style="color: rgb(153, 153, 153)" href="http://www.sedogo.com/viewEvent.aspx?EID=1114">Find out more &gt;</a></span></td>
				</tr>
			</tbody></table>
		</td>
	</tr>
</tbody></table>
                        
                        </div>
                    </div>
                </div>
                <div class="one-col-end">
                    <h2 class="col-header">
                        Get inspired</h2>
                    <p class="teaser">
                        Need help getting started? <a href="getInspired.aspx">See some inspiring ideas here &gt;&gt;</a></p>
                    <div class="rotatorBackground">
                        <telerik:RadRotator ID="eventRotator" runat="server" Width="232px" Height="216px"
                            CssClass="horizontalRotator" RotatorType="FromCode" ItemHeight="216" ItemWidth="232">
                            <ItemTemplate>
                                <div class="itemTemplate">
                                    <a href="getInspired.aspx">
                                        <img src='<%# Page.ResolveUrl("~/images/") + Container.DataItem %>.png' alt="Customer Image" /></a>
                                </div>
                            </ItemTemplate>
                        </telerik:RadRotator>
                    </div>
                </div>
            </div>
            
            <Sedogo:FooterControl ID="footerControl" runat="server" />
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
    
    <ComponentArt:Dialog HeaderCssClass="headerCss" CssClass="dialog" AllowDrag="false" 
        ContentCssClass="contentCss" FooterCssClass="footerCss"
        Alignment="MiddleCentre" ID="tourDialog1" runat="server">
    <Header>
    
    </Header>
    <Content>
    
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-color:white;font-size:12px;font-family:Arial;padding:10px;">
                
                    <p><span class="tourSelected">1</span>&nbsp;&nbsp;<a class="tourNotSelected"
                        href="javascript:showTour2()">2</a>&nbsp;&nbsp;<a class="tourNotSelected"
                        href="javascript:showTour3()">3</a>&nbsp;&nbsp;<a class="tourNotSelected"
                        href="javascript:showTour4()">4</a>&nbsp;&nbsp;<a class="tourNotSelected"
                        href="javascript:showTour5()">5</a></p> 
                    <div style="padding:10px 50px">
                    <h2>Create goals, big or small...</h2>
                    <img src="images/Tour1image.jpg" />
                    </div>
                    
                    <div class="tourButtonClose">
                    <p><a href="javascript:tourDialog1.Close()">Close</a> <a 
                        href="javascript:tourDialog1.Close()"><img src="images/close-controls.gif" /></a></p>
                    </div>
                    <div class="tourButtonLeft">
                    <img src="images/buttonL.jpg" />
                    </div>
                    <div class="tourButtonRight">
                    <a href="javascript:showTour2()"><img src="images/buttonR.jpg" /></a>
                    </div>
                
                </td>
            </tr>
        </table>

    </Content>
    <Footer>
    
    </Footer>
    </ComponentArt:Dialog>
    
    <ComponentArt:Dialog HeaderCssClass="headerCss" CssClass="dialog" AllowDrag="false" 
        ContentCssClass="contentCss" FooterCssClass="footerCss"
        Alignment="MiddleCentre" ID="tourDialog2" runat="server">
    <Header>
    
    </Header>
    <Content>
    
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-color:white;font-size:12px;font-family:Arial;padding:10px;">
                
                    <p><a href="javascript:showTour1()" class="tourNotSelected">1</a>&nbsp;&nbsp;<span class="tourSelected">2</span>&nbsp;&nbsp;<a class="tourNotSelected"
                        href="javascript:showTour3()">3</a>&nbsp;&nbsp;<a class="tourNotSelected"
                        href="javascript:showTour4()">4</a>&nbsp;&nbsp;<a class="tourNotSelected"
                        href="javascript:showTour5()">5</a></p> 
                    <div style="padding:10px 50px">
                    <h2>...or look for people with the same goals.</h2>
                    <img src="images/Tour2image.jpg" />
                    </div>
                    
                    <div class="tourButtonClose">
                    <p><a href="javascript:tourDialog2.Close()">Close</a> <a 
                        href="javascript:tourDialog2.Close()"><img src="images/close-controls.gif" /></a></p>
                    </div>
                    <div class="tourButtonLeft">
                    <a href="javascript:showTour1()"><img src="images/buttonL.jpg" /></a>
                    </div>
                    <div class="tourButtonRight">
                    <a href="javascript:showTour3()"><img src="images/buttonR.jpg" /></a>
                    </div>
                
                </td>
            </tr>
        </table>

    </Content>
    <Footer>
    
    </Footer>
    </ComponentArt:Dialog>

    <ComponentArt:Dialog HeaderCssClass="headerCss" CssClass="dialog" AllowDrag="false" 
        ContentCssClass="contentCss" FooterCssClass="footerCss"
        Alignment="MiddleCentre" ID="tourDialog3" runat="server">
    <Header>
    
    </Header>
    <Content>
    
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-color:white;font-size:12px;font-family:Arial;padding:10px;">
                
                    <p><a href="javascript:showTour1()" class="tourNotSelected">1</a>&nbsp;&nbsp;<a 
                        href="javascript:showTour2()" class="tourNotSelected">2</a>&nbsp;&nbsp;<span class="tourSelected">3</span>&nbsp;&nbsp;<a 
                        href="javascript:showTour4()" class="tourNotSelected">4</a>&nbsp;&nbsp;<a 
                        href="javascript:showTour5()" class="tourNotSelected">5</a></p> 
                    <div style="padding:10px 50px">
                    <h2>Invite people to share your goal...</h2>
                    <img src="images/Tour3image.jpg" />
                    </div>
                    
                    <div class="tourButtonClose">
                    <p><a href="javascript:tourDialog3.Close()">Close</a> <a 
                        href="javascript:tourDialog3.Close()"><img src="images/close-controls.gif" /></a></p>
                    </div>
                    <div class="tourButtonLeft">
                    <a href="javascript:showTour2()"><img src="images/buttonL.jpg" /></a>
                    </div>
                    <div class="tourButtonRight">
                    <a href="javascript:showTour4()"><img src="images/buttonR.jpg" /></a>
                    </div>
                
                </td>
            </tr>
        </table>

    </Content>
    <Footer>
    
    </Footer>
    </ComponentArt:Dialog>

    <ComponentArt:Dialog HeaderCssClass="headerCss" CssClass="dialog" AllowDrag="false" 
        ContentCssClass="contentCss" FooterCssClass="footerCss"
        Alignment="MiddleCentre" ID="tourDialog4" runat="server">
    <Header>
    
    </Header>
    <Content>
    
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-color:white;font-size:12px;font-family:Arial;padding:10px;">
                
                    <p><a href="javascript:showTour1()" class="tourNotSelected">1</a>&nbsp;&nbsp;<a class="tourNotSelected" 
                        href="javascript:showTour2()">2</a>&nbsp;&nbsp;<a class="tourNotSelected" 
                        href="javascript:showTour3()">3</a>&nbsp;&nbsp;<span class="tourSelected">4</span>&nbsp;&nbsp;<a class="tourNotSelected" 
                        href="javascript:showTour5()">5</a></p> 
                    <div style="padding:10px 50px">
                    <h2>Keep track of your goals on your personal timeline.</h2>
                    <img src="images/Tour4image.jpg" />
                    </div>
                    
                    <div class="tourButtonClose">
                    <p><a href="javascript:tourDialog4.Close()">Close</a> <a 
                        href="javascript:tourDialog4.Close()"><img src="images/close-controls.gif" /></a></p>
                    </div>
                    <div class="tourButtonLeft">
                    <a href="javascript:showTour3()"><img src="images/buttonL.jpg" /></a>
                    </div>
                    <div class="tourButtonRight">
                    <a href="javascript:showTour5()"><img src="images/buttonR.jpg" /></a>
                    </div>
                
                </td>
            </tr>
        </table>

    </Content>
    <Footer>
    
    </Footer>
    </ComponentArt:Dialog>

    <ComponentArt:Dialog HeaderCssClass="headerCss" CssClass="dialog" AllowDrag="false" 
        ContentCssClass="contentCss" FooterCssClass="footerCss"
        Alignment="MiddleCentre" ID="tourDialog5" runat="server">
    <Header>
    
    </Header>
    <Content>
    
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-color:white;font-size:12px;font-family:Arial;padding:10px;">
                
                    <p><a href="javascript:showTour1()" class="tourNotSelected">1</a>&nbsp;&nbsp;<a class="tourNotSelected" 
                        href="javascript:showTour2()">2</a>&nbsp;&nbsp;<a class="tourNotSelected" 
                        href="javascript:showTour3()">3</a>&nbsp;&nbsp;<a class="tourNotSelected" 
                        href="javascript:showTour4()">4</a>&nbsp;&nbsp;<span class="tourSelected">5</span></p> 
                    <div style="padding:10px 50px">
                    <h2>Share pictures and videos of your goals<br />as you achieve them.</h2>
                    <img src="images/Tour5image.jpg" />
                    </div>
                    
                    <div class="tourButtonClose">
                    <p><a href="javascript:tourDialog5.Close()">Close</a> <a 
                        href="javascript:tourDialog5.Close()"><img src="images/close-controls.gif" /></a></p>
                    </div>
                    <div class="tourButtonLeft">
                    <a href="javascript:showTour4()"><img src="images/buttonL.jpg" /></a>
                    </div>
                    <div class="tourButtonRight">
                    <a target="_top" href="register.aspx"><img src="images/buttonR.jpg" /></a>
                    </div>
                
                </td>
            </tr>
        </table>

    </Content>
    <Footer>
    
    </Footer>
    </ComponentArt:Dialog>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default"
    MasterPageFile="~/Master.master" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
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
						date: '<asp:Literal id="timelineStartDate1" runat="server" />',
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
				//Create timeline and load data
				tl = Timeline.create(document.getElementById("my-timeline"), bandInfos);
				var url = "<asp:Literal id="timelineURL" runat="server" />";
				Timeline.loadXML(url, function(xml, url) { 
                    eventSource.loadXML(xml, url); 
                });
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
        $(document).ready(function () {
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
            openModal("login.aspx?UTID=" + eventUserID);
        }
        function doSendMessage(srhUserID) {
            //openModal("sendUserMessage.aspx?EID=-1&UID=" + srhUserID);
            openModal("login.aspx");
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

        function changeClass(id, newClass) {
            document.getElementById(id).className = newClass; //.setAttribute("class", newClass);
        }
        function showTour1() {
            tourDialog2.Close();
            tourDialog3.Close();
            tourDialog4.Close();
            tourDialog5.Close();
            tourDialog1.Show();
        }
        function showTour2() {
            tourDialog1.Close();
            tourDialog3.Close();
            tourDialog4.Close();
            tourDialog5.Close();
            tourDialog2.Show();
        }
        function showTour3() {
            tourDialog1.Close();
            tourDialog2.Close();
            tourDialog4.Close();
            tourDialog5.Close();
            tourDialog3.Show();
        }
        function showTour4() {
            tourDialog1.Close();
            tourDialog2.Close();
            tourDialog3.Close();
            tourDialog5.Close();
            tourDialog4.Show();
        }
        function showTour5() {
            tourDialog1.Close();
            tourDialog2.Close();
            tourDialog3.Close();
            tourDialog4.Close();
            tourDialog5.Show();
        }
        if (document.images) {
            button1 = new Image;
            button2 = new Image;
            button1.src = 'images/takethetour_over.jpg'
            button2.src = 'signup_over.jpg'
        }
        function toggleTimeline() {
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
        $(document).ready(function () {
            breakout_of_frame(); onLoad(); scrl();
        });
        $('body').resize(function () {
            onResize();
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="content">
    <div class="one-col">
        <div>
            <h2>
                <asp:Label ID="Label1" runat="server" Text="Recent activity" /></h2>
            <asp:PlaceHolder ID="goalsAddedPlaceHolder" runat="server" />
        </div>
    </div>
    <div class="one-col">
        <h2 class="col-header">
            how does it work?</h2>
        <p class="teaser">
            Sedogo lets you create your own timeline of future goals and connect with others
            to make them happen.</p>
        <p>
            <a href="register.aspx" title="get started" class="button">get started</a></p>
        <div style="margin-top: 20px">
            <asp:Literal ID="homePageContent" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="one-col">
        <h2 class="col-header">
            join in today</h2>
        <p class="teaser">
            Start creating personal goals right now. <a href="register.aspx" title="get started">
                Registering is fast, easy and free!</a></p>
        <div style="float: left; width: 100%; height: 216px; margin-top: 39px;">
            <div style="width: 100%; height: 2px; background-color: #00BFFD;">
            </div>
            <div style="width: 100%;">
                <div style="color: #00BFFD; font-size: 14px; line-height: 30px; text-align: left;
                    float: left;">
                    Latest Members</div>
                <div style="color: #A9ADA6; font-size: 14px; line-height: 30px; text-align: right;
                    visibility: hidden">
                    <%=TGoals%>&nbsp; members</div>
            </div>
            <div style="margin-left: 2px; margin-top: 28px;">
                <asp:DataList ID="dlMember" runat="server" RepeatColumns="6" RepeatDirection="Horizontal"
                    DataKeyField="UserId">
                    <ItemTemplate>
                        <div>
                            <div class="misc-pop-up" id="dmpop<%# DataBinder.Eval(Container.DataItem, "userId") %>"
                                style="display: none; position: relative; z-index: 10;">
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
                                                    <span class="blue" style="line-height: 22px;"><a href="publicProfile.aspx?UID=<%# DataBinder.Eval(Container.DataItem, "UserId") %>">
                                                        <%# DataBinder.Eval(Container.DataItem, "FirstName")%>
                                                        &nbsp;<%# DataBinder.Eval(Container.DataItem, "LastName")%></a> </span>
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
                            <img src="assets/profilePics/<%# DataBinder.Eval(Container.DataItem, "ProfilePicThumbnail") %>"
                                alt="" height="33" width="33" style="cursor: pointer; padding-bottom: 6px; padding-right: 6px;"
                                onerror="this.src='images/profile/blankProfile.jpg'" onmouseover="ShowHideDiv(<%# DataBinder.Eval(Container.DataItem, "userId") %>)" /></div>
                    </ItemTemplate>
                </asp:DataList></div>
        </div>
    </div>
    <div class="one-col-end">
        <h2 class="col-header">
            get inspired</h2>
        <p class="teaser">
            Need help getting started? <a href="getInspired.aspx">See popular goal searches and
                get ideas</a></p>
        <div class="rotatorBackground" style="padding-top: 58px;">
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
</asp:Content>
<asp:Content ContentPlaceHolderID="footer" runat="server">
    <ComponentArt:Dialog HeaderCssClass="headerCss" CssClass="dialog" AllowDrag="false"
        ContentCssClass="contentCss" FooterCssClass="footerCss" Alignment="MiddleCentre"
        ID="tourDialog1" runat="server">
        <Header>
        </Header>
        <Content>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="background-color: white; font-size: 12px; font-family: Arial; padding: 10px;">
                        <p>
                            <span class="tourSelected">1</span>&nbsp;&nbsp;<a class="tourNotSelected" href="javascript:showTour2()">2</a>&nbsp;&nbsp;<a
                                class="tourNotSelected" href="javascript:showTour3()">3</a>&nbsp;&nbsp;<a class="tourNotSelected"
                                    href="javascript:showTour4()">4</a>&nbsp;&nbsp;<a class="tourNotSelected" href="javascript:showTour5()">5</a></p>
                        <div style="padding: 10px 50px">
                            <h2>
                                Create goals, big or small...</h2>
                            <img alt=""  src="images/Tour1image.jpg" />
                        </div>
                        <div class="tourButtonClose">
                            <p>
                                <a href="javascript:tourDialog1.Close()">Close</a> <a href="javascript:tourDialog1.Close()">
                                    <img alt=""  src="images/close-controls.gif" /></a></p>
                        </div>
                        <div class="tourButtonLeft">
                            <img alt=""  src="images/buttonL.jpg" />
                        </div>
                        <div class="tourButtonRight">
                            <a href="javascript:showTour2()">
                                <img alt=""  src="images/buttonR.jpg" /></a>
                        </div>
                    </td>
                </tr>
            </table>
        </Content>
        <Footer>
        </Footer>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog HeaderCssClass="headerCss" CssClass="dialog" AllowDrag="false"
        ContentCssClass="contentCss" FooterCssClass="footerCss" Alignment="MiddleCentre"
        ID="tourDialog2" runat="server">
        <Header>
        </Header>
        <Content>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="background-color: white; font-size: 12px; font-family: Arial; padding: 10px;">
                        <p>
                            <a href="javascript:showTour1()" class="tourNotSelected">1</a>&nbsp;&nbsp;<span class="tourSelected">2</span>&nbsp;&nbsp;<a
                                class="tourNotSelected" href="javascript:showTour3()">3</a>&nbsp;&nbsp;<a class="tourNotSelected"
                                    href="javascript:showTour4()">4</a>&nbsp;&nbsp;<a class="tourNotSelected" href="javascript:showTour5()">5</a></p>
                        <div style="padding: 10px 50px">
                            <h2>
                                ...or look for people with the same goals.</h2>
                            <img alt=""  src="images/Tour2image.jpg" />
                        </div>
                        <div class="tourButtonClose">
                            <p>
                                <a href="javascript:tourDialog2.Close()">Close</a> <a href="javascript:tourDialog2.Close()">
                                    <img alt=""  src="images/close-controls.gif" /></a></p>
                        </div>
                        <div class="tourButtonLeft">
                            <a href="javascript:showTour1()">
                                <img alt=""  src="images/buttonL.jpg" /></a>
                        </div>
                        <div class="tourButtonRight">
                            <a href="javascript:showTour3()">
                                <img alt=""  src="images/buttonR.jpg" /></a>
                        </div>
                    </td>
                </tr>
            </table>
        </Content>
        <Footer>
        </Footer>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog HeaderCssClass="headerCss" CssClass="dialog" AllowDrag="false"
        ContentCssClass="contentCss" FooterCssClass="footerCss" Alignment="MiddleCentre"
        ID="tourDialog3" runat="server">
        <Header>
        </Header>
        <Content>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="background-color: white; font-size: 12px; font-family: Arial; padding: 10px;">
                        <p>
                            <a href="javascript:showTour1()" class="tourNotSelected">1</a>&nbsp;&nbsp;<a href="javascript:showTour2()"
                                class="tourNotSelected">2</a>&nbsp;&nbsp;<span class="tourSelected">3</span>&nbsp;&nbsp;<a
                                    href="javascript:showTour4()" class="tourNotSelected">4</a>&nbsp;&nbsp;<a href="javascript:showTour5()"
                                        class="tourNotSelected">5</a></p>
                        <div style="padding: 10px 50px">
                            <h2>
                                Invite people to share your goal...</h2>
                            <img alt=""  src="images/Tour3image.jpg" />
                        </div>
                        <div class="tourButtonClose">
                            <p>
                                <a href="javascript:tourDialog3.Close()">Close</a> <a href="javascript:tourDialog3.Close()">
                                    <img alt=""  src="images/close-controls.gif" /></a></p>
                        </div>
                        <div class="tourButtonLeft">
                            <a href="javascript:showTour2()">
                                <img alt=""  src="images/buttonL.jpg" /></a>
                        </div>
                        <div class="tourButtonRight">
                            <a href="javascript:showTour4()">
                                <img alt=""  src="images/buttonR.jpg" /></a>
                        </div>
                    </td>
                </tr>
            </table>
        </Content>
        <Footer>
        </Footer>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog HeaderCssClass="headerCss" CssClass="dialog" AllowDrag="false"
        ContentCssClass="contentCss" FooterCssClass="footerCss" Alignment="MiddleCentre"
        ID="tourDialog4" runat="server">
        <Header>
        </Header>
        <Content>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="background-color: white; font-size: 12px; font-family: Arial; padding: 10px;">
                        <p>
                            <a href="javascript:showTour1()" class="tourNotSelected">1</a>&nbsp;&nbsp;<a class="tourNotSelected"
                                href="javascript:showTour2()">2</a>&nbsp;&nbsp;<a class="tourNotSelected" href="javascript:showTour3()">3</a>&nbsp;&nbsp;<span
                                    class="tourSelected">4</span>&nbsp;&nbsp;<a class="tourNotSelected" href="javascript:showTour5()">5</a></p>
                        <div style="padding: 10px 50px">
                            <h2>
                                Keep track of your goals on your personal timeline.</h2>
                            <img alt=""  src="images/Tour4image.jpg" />
                        </div>
                        <div class="tourButtonClose">
                            <p>
                                <a href="javascript:tourDialog4.Close()">Close</a> <a href="javascript:tourDialog4.Close()">
                                    <img alt=""  src="images/close-controls.gif" /></a></p>
                        </div>
                        <div class="tourButtonLeft">
                            <a href="javascript:showTour3()">
                                <img alt=""  src="images/buttonL.jpg" /></a>
                        </div>
                        <div class="tourButtonRight">
                            <a href="javascript:showTour5()">
                                <img alt=""  src="images/buttonR.jpg" /></a>
                        </div>
                    </td>
                </tr>
            </table>
        </Content>
        <Footer>
        </Footer>
    </ComponentArt:Dialog>
    <ComponentArt:Dialog HeaderCssClass="headerCss" CssClass="dialog" AllowDrag="false"
        ContentCssClass="contentCss" FooterCssClass="footerCss" Alignment="MiddleCentre"
        ID="tourDialog5" runat="server">
        <Header>
        </Header>
        <Content>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="background-color: white; font-size: 12px; font-family: Arial; padding: 10px;">
                        <p>
                            <a href="javascript:showTour1()" class="tourNotSelected">1</a>&nbsp;&nbsp;<a class="tourNotSelected"
                                href="javascript:showTour2()">2</a>&nbsp;&nbsp;<a class="tourNotSelected" href="javascript:showTour3()">3</a>&nbsp;&nbsp;<a
                                    class="tourNotSelected" href="javascript:showTour4()">4</a>&nbsp;&nbsp;<span class="tourSelected">5</span></p>
                        <div style="padding: 10px 50px">
                            <h2>
                                Share pictures and videos of your goals<br />
                                as you achieve them.</h2>
                            <img alt=""  src="images/Tour5image.jpg" alt="" />
                        </div>
                        <div class="tourButtonClose">
                            <p>
                                <a href="javascript:tourDialog5.Close()">Close</a> <a href="javascript:tourDialog5.Close()">
                                    <img alt=""  src="images/close-controls.gif" /></a></p>
                        </div>
                        <div class="tourButtonLeft">
                            <a href="javascript:showTour4()">
                                <img alt=""  src="images/buttonL.jpg" /></a>
                        </div>
                        <div class="tourButtonRight">
                            <a target="_top" href="register.aspx">
                                <img alt=""  src="images/buttonR.jpg" /></a>
                        </div>
                    </td>
                </tr>
            </table>
        </Content>
        <Footer>
        </Footer>
    </ComponentArt:Dialog>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="signIn">
    <sedogo:bannerlogincontrol id="BannerLoginControl1" runat="server" />
    <div class="three-col">
        <table border="0" cellspacing="0" cellpadding="0" align="right" width="300" style="margin-top: 75px">
            <tr>
                <td>
                    <a href="register.aspx" class="signuprollover" title="signup"><span class="displace">
                        Signup</span></a>
                </td>
                <td>
                    <p class="bannerLoginSignup">
                        or</p>
                </td>
                <td>
                    <a href="javascript:showTour1()" class="takethetourrollover" title="takethetour"><span
                        class="displace">Tour</span></a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

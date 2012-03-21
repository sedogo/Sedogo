<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Timelines.ascx.cs" Inherits="components_Timelines" %>
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
                <h2>
                    Goal timeline</h2>
            </div>
            <div class="float_right">
                <table>
                    <tr>
                        <td style="padding-top: 3px">
                            <a href="javascript:toggleTimeline();" class="timelinelink">Close timeline</a>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <img id="imgMngT" src="images/T_Close.jpg" title="Open/Close timeline" alt="Open/Close timeline"
                                style="cursor: pointer;" />
                        </td>
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
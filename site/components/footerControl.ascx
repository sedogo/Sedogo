﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="footerControl.ascx.cs" Inherits="components_footerControl" %>

<div id="footer">
	<ul>
		<li class="first">&copy; Sedogo Ltd 2008-2010</li>
		<li><asp:HyperLink ID="aboutLink" runat="server" NavigateUrl="~/about.aspx" ToolTip="About" Text="About" /></li>
		<li><asp:HyperLink ID="faqLink" runat="server" NavigateUrl="~/faq.aspx" ToolTip="FAQ" Text="FAQ" /></li>
		<li><asp:HyperLink ID="privacyLink" runat="server" NavigateUrl="~/privacy.aspx" ToolTip="Privacy Policy" Text="Privacy Policy" /></li>
		<li class="last"><asp:HyperLink ID="feedbackLink" runat="server" NavigateUrl="~/feedback.aspx" ToolTip="Feedback" Text="Feedback" /></li>
	</ul>				
    <div style="text-align:right;margin-top:-25px">
    <div style="color:#0cf">Follow us <a target="_blank" style="padding-left:7px"
        href="http://www.facebook.com/pages/Sedogo/261533591696"><asp:Image ID="Image1" runat="server" 
            ImageUrl="~/images/facebook.gif" /> <a 
        style="padding-left:7px"
        target="_blank" href="http://twitter.com/Sedogo"><asp:Image runat="server" 
            ImageUrl="~/images/twitter.gif" /></div>
    </div>
</div>

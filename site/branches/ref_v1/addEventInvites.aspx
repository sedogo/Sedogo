<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addEventInvites.aspx.cs" Inherits="addEventInvites" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>

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

<script language="javascript">
function showDiv(divName)
{
    if (document.getElementById) { // DOM3 = IE5, NS6
        document.getElementById(divName).style.visibility = "visible";
        document.getElementById(divName).style.display = "block";
    } else {
        if (document.layers) { // Netscape 4
        document.divName.visibility = "visible";
        document.divName.display = "block";
        } else { // IE 4
            document.all.divName.style.visibility = "visible";
            document.all.divName.style.display = "block";
        }
    }
}
function hideDiv(divName) {
    document.getElementById(divName).style.visibility = "hidden";
    if (document.getElementById) { // DOM3 = IE5, NS6
        document.getElementById(divName).style.visibility = "hidden";
        document.getElementById(divName).style.display = "none";
    } else {
        if (document.layers) { // Netscape 4
            document.divName.visibility= "hidden";
            document.divName.display = "none";
        } else { // IE 4
            document.all.divName.style.visibility = "hidden";
            document.all.divName.style.display = "none";
        }
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
	    
    	    <div id="modalContents">
	    
	            <h1>Create a goal</h1>
    	        
                <h3 class="blue">Who do you want to invite to <asp:Label ID="eventNameLabel" runat="server" />?</h3>

                <p>Type in friends or family email addresses</p>

                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox1" runat="server" Width="210px" /></td>
                    </tr>
                </table>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox2" runat="server" Width="210px" /></td>
                    </tr>
                </table>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox3" runat="server" Width="210px" /></td>
                    </tr>
                </table>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox4" runat="server" Width="210px" /></td>
                    </tr>
                </table>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox5" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite6Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                <div style="visibility:hidden;display:none" id="invite6Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox6" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite7Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite7Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox7" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite8Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite8Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox8" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite9Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite9Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox9" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite10Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite10Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox10" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite11Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite11Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox11" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite12Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite12Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox12" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite13Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite13Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox13" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite14Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite14Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox14" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite15Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite15Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox15" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite16Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite16Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox16" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite17Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite17Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox17" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite18Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite18Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox18" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite19Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite19Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox19" runat="server" Width="210px" /></td>
                        <!--td><a href="javascript:showDiv('invite20Div')" class="invite-more">invite more</a></td-->
                    </tr>
                </table>
                </div>
                <div style="visibility:hidden;display:none" id="invite20Div">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><img src="images/profile/miniProfile.jpg" class="mini-profile" /></td>
                        <td><asp:TextBox ID="inviteTextBox20" runat="server" Width="210px" /></td>
                    </tr>
                </table>
                </div>
                
                <img src="images/popupLine.png" alt="" class="line-divider" />

                <p style="margin: 10px 0 4px 0">Invite message <em>(optional)</em></p>

                <asp:TextBox ID="additionalInviteTextTextBox" runat="server" TextMode="MultiLine"
                    Width="233px" Rows="3" MaxLength="1000" />
                
                <p style="font-size: 11px; padding-top: 20px">Please note anyone without a sedogo account will be <br />invited to join before they can connect with your goal</p>
                
                <img src="images/popupLine.png" alt="" class="line-divider" />
                
			<p class="required-field">&#42; Required field</p>

            </div>
            
            <div class="buttons">
                <p><asp:LinkButton ID="sendInvitesLink" runat="server" Text="Send emails"
                    OnClick="sendInvitesLink_click" CssClass="button-lrg" />
                <asp:LinkButton ID="skipInvitesLink" runat="server" CssClass="button-lrg" Text="Skip" 
                    ToolTip="Skip" OnClick="click_skipInvitesLink" CausesValidation="false" />
                </p>
            </div>

		</div>

    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>
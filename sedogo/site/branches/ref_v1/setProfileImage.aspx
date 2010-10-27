<%@ Page Language="C#" AutoEventWireup="true" CodeFile="setProfileImage.aspx.cs" Inherits="setProfileImage" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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

	<title>Set profile image : Sedogo : Create your future and connect with others to make it happen</title>

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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>    
    
        <asp:ValidationSummary runat="server" ID="validationSummary" 
            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
            HeaderText="Please review the following errors:" />
    
	    <div id="modal">
            <h1>Set your profile image or Avatar</h1>
            <p>Please update your profile by uploading an image to your profile, or by selecting one of the Avatar images below</p>
             <br />
             
             <table>
                <tr>
                    <td>Upload image</td>
                    <td><asp:FileUpload ID="profilePicFileUpload" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="2"><div class="pinstripe-divider">&nbsp;</div></td>
                </tr>
                <tr>
                    <td>Avatar image</td>
                    <td><telerik:RadComboBox ID="avatarComboBox" CssClass="ComboBox" runat="server" 
                        Height="200px" Width="200px">
                        </telerik:RadComboBox></td>
                </tr>
             </table>

		</div>
    
        <div class="buttons">
            <asp:LinkButton 
                ID="saveButton" runat="server" ToolTip="Save" Text="Save" 
                OnClick="saveButton_Click" CssClass="button-lrg" />
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

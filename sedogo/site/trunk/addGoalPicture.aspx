<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addGoalPicture.aspx.cs" Inherits="addGoalPicture" %>

<%@ Register TagPrefix="Sedogo" TagName="BannerLoginControl" Src="~/components/bannerLogin.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="SidebarControl" Src="~/components/sidebar.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="BannerAddFindControl" Src="~/components/bannerAddFindControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ Register TagPrefix="Sedogo" TagName="FooterControl" Src="~/components/footerControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="favicon.ico">
    <meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
    <meta http-equiv="content-script-type" content="text/javascript" />
    <meta http-equiv="content-style-type" content="text/css" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
	<title>Add goal picture : Sedogo : Create your future and connect with others to make it happen</title>
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
    <script type="text/javascript" src="js/DD_roundies_0.0.2a-min.js"></script>

    <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>

    <script type="text/javascript" src="js/jquery-ui-1.7.2.custom.min.js"></script>

    <script type="text/javascript" src="js/ui.dialog.js"></script>

    <script type="text/javascript" src="js/jquery.cookie.js"></script>

    <script type="text/javascript" src="js/jquery.livequery.js"></script>

    <script type="text/javascript" src="js/jquery.corner.js"></script>

    <script type="text/javascript" src="js/main.js"></script>

    <script type="text/javascript" src="js/validationFunctions.js"></script>

    <script type="text/javascript">
    function addPicture()
    {
	    <asp:Literal id="addPictureLiteral" runat="server" />
	    openModal(url);
    }
    </script>

    <script language="javascript" type="text/javascript">
        var state = 'none';
        function showhide(layer_ref) {
            if (state == 'block') {
                state = 'none';
            }
            else {
                state = 'block';
            }
            if (document.all) { //IS IE 4 or 5 (or 6 beta)
                eval("document.all." + layer_ref + ".style.display = state");
            }
            if (document.layers) { //IS NETSCAPE 4 or below
                document.layers[layer_ref].display = state;
            }
            if (document.getElementById && !document.all) {
                hza = document.getElementById(layer_ref);
                hza.style.display = state;
            }
        }
        
        function resetFlash() 
	    {
         var flash = document.getElementsByTagName('embed');        
         if(flash.length > 0)
          {
            for(var i=0;i<flash.length;i++)
            {
                flash[i].setAttribute("width", "300px"); 
                flash[i].setAttribute("height", "270px");       
            }
          }            
        
        }
        function setColor(id, newColor)
        {
            document.getElementById(id).style.background = newColor;
        }
    </script>

</head>
<body onload="resetFlash()">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="scriptManager" runat="server">
        </asp:ScriptManager>
        <div id="container">
            <Sedogo:BannerLoginControl ID="bannerLogin" runat="server" />
            <Sedogo:BannerAddFindControl ID="bannerAddFindControl" runat="server" />
            <div id="other-content">
                <Sedogo:SidebarControl ID="sidebarControl" runat="server" />
                <div class="three-col">
                    <div class="page-banner-content" id="pageBannerBarDiv" runat="server">
                        <div class="page-banner-header">
                            <asp:Literal ID="eventTitleLabel" runat="server" /></div>
                        <div class="page-banner-backbutton">
                            <asp:LinkButton ID="backButton" runat="server" Text="Back" CssClass="page-banner-linkstyle"
                                OnClick="backButton_click" CausesValidation="false" /></div>
                    </div>
                    <table width="100%" border="0" cellspacing="0" cellpadding="2">
                        <tr>
                            <td width="100%">

            <h1>Add a goal picture</h1>
            
            <table width="100%">
                <tr>
                    <td valign="top">
                    
            <fieldset style="padding-top:30px;">
                <ol>
                    <li>
                        <label for="">Select file</label>
                        <asp:FileUpload ID="goalPicFileUpload" runat="server" />
                        <asp:RequiredFieldValidator ID="goalPicFileUploadValidator" runat="server"
                            ControlToValidate="goalPicFileUpload" ErrorMessage="A file is required"
                            Display="None" SetFocusOnError="true">
                            </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="">Caption</label>
                        <asp:TextBox ID="captionTextBox" runat="server"></asp:TextBox>
                    </li>
                </ol>
            </fieldset>

                    </td>
                </tr>
                <tr>
                    <td><p>Valid file types are jpeg, gif and png images</p></td>
                </tr>
            </table>

            <p>&nbsp;</p>
            <div>
                <asp:LinkButton 
                    ID="uploadProfilePicButton" runat="server" ToolTip="Upload picture" Text="Upload picture" 
                    OnClick="uploadProfilePicButton_click" CssClass="button-sml" />
            </div>
            <p>&nbsp;</p>

                            </td>
                        </tr>
                    </table>
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
    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />
</body>
</html>

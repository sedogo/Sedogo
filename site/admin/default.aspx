<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="favicon.ico" >  
	<title>FAQ : Sedogo : Create your future and connect with others to make it happen</title>

<script language="JavaScript" type="text/javascript">
function breakout_of_frame()
{
  if (top.location != location)
  {
    top.location.href = document.location.href ;
  }
}
</script>

</head>
<body onload="breakout_of_frame()">
    <form id="form1" runat="server">
    <div>
    
    <asp:ValidationSummary runat="server" ID="validationSummary" 
        ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList"
        HeaderText="Please review the following errors:" />
    
    <img src="../images/sedogo.gif" alt="" />
        
    Welcome to Sedogo Administration<br />
    <br />
    <br />
    Log in:<br />
    Email address: <asp:TextBox runat="server" ID="emailAddress" Width="200px" />
        <asp:RequiredFieldValidator ID="emailAddressValidator" runat="server"
            ControlToValidate="emailAddress" ErrorMessage="An email address is required"
            Display="None" SetFocusOnError="true">
            </asp:RequiredFieldValidator><br />
    Password: <asp:TextBox runat="server" ID="userPassword" TextMode="password" Width="200px" />
		<asp:RequiredFieldValidator ID="userPasswordValidator" runat="server"
            ControlToValidate="userPassword" ErrorMessage="A password is required"
            Display="None" SetFocusOnError="true">
            </asp:RequiredFieldValidator><br />
    <asp:Button runat="server" ID="loginButton" text="Log in" OnClick="loginButton_Click" /><br />
    <br />
    
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

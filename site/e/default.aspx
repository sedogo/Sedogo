<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="e_default" %>
<%@ Register TagPrefix="Sedogo" TagName="GoogleAnalyticsControl" Src="~/components/googleAnalyticsControl.ascx" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="favicon.ico" >  
    <title>Verify email : Sedogo : Create your future and connect with others to make it happen</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Thankyou for verifying your email address, you can now log in using your email address
        and the password you entered when you signed up.
            
        <a href="../default.aspx">Click here to go to the log in page</a>
    
    </div>
    </form>

    <Sedogo:GoogleAnalyticsControl ID="googleAnalyticsControl" runat="server" />

</body>
</html>

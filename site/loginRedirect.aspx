<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loginRedirect.aspx.cs" Inherits="loginRedirect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Home : Sedogo : Create your future timeline.  Connect, track and interact with like minded people.</title>
    
    <script language="JavaScript" type="text/javascript">
    function breakout_of_frame()
    {
      if (top.location != location)
      {
        top.location.href = document.location.href ;
      }
      else
      {
        top.location.href = "profile.aspx";
      }
    }
    </script>
    
</head>
<body onload="breakout_of_frame()">
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>

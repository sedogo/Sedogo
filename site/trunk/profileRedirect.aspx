﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="profileRedirect.aspx.cs" Inherits="profileRedirect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Redirect : Sedogo : Create your future and connect with others to make it happen</title>
    
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
<%@ page language="VB" autoeventwireup="false" inherits="Login, App_Web_x5dfcoqc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WebEditor 1.0</title>
    <meta name="robots" content="NOINDEX, NOFOLLOW" />
    <link href="web.css" rel="Stylesheet"   type="text/css" />
</head>
<body> 
    <form id="form1" runat="server">
        <table style="width:100%;height:700px;">
	        <tr><td align="center" valing="middle">
		
		        <table class="formLogin">
			        <tr><td align="center" valign="middle">

						        <table>
						        <tr>
							        <td align="center" valign="middle">
                                        <asp:TextBox ID="txtLogin" Width="83px" TextMode="Password" runat="server"/><br/>
                                        <asp:TextBox ID="txtPass" Width="83px" TextMode="Password" runat="server"/><br/>						
                                        <asp:Button ID="btnValidar" runat="server" Text="OK" /><br/>
                                        <asp:Label ID="lblMsg" runat="server" Text=""/>
							        </td>
						        </tr>
						        </table>
						
			        </td></tr>
		        </table>

	        </td></tr>
        </table>
    </form>
</body>
</html>

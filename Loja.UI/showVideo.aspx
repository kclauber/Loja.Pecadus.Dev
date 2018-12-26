<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showVideo.aspx.cs" Inherits="showVideo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlImagem" runat="server">
        imagens aqui
    </asp:Panel>
    <asp:Panel ID="pnlVideo" runat="server" Visible="false">
        <video width="320" height="240" controls autoplay>
          <source src="videosProdutos/Highschool of the Dead - 01.mp4" type="video/mp4">
          <%--<source src="movie.ogg" type="video/ogg">--%>
            Your browser does not support the video tag.
        </video>
    </asp:Panel>
    <asp:Button ID="btnImagem" runat="server" Text="Button" 
        onclick="btnImagem_Click" />
    <asp:Button ID="btnVideo"  runat="server" Text="Button" 
        onclick="btnVideo_Click" />
    </form>
</body>
</html>

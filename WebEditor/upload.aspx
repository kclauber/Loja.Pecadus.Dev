<%@ page language="VB" autoeventwireup="false" validaterequest="false" inherits="upload, App_Web_x5dfcoqc" debug="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>WebEditor 1.0</title>
    <meta name="robots" content="NOINDEX, NOFOLLOW" />
    <link href="web.css" rel="Stylesheet"   type="text/css" />
</head>

<body>
<form id="formUpload" enctype="multipart/form-data" runat="server">

    <div id="divEscuro" style="margin:auto;">
	    <br/>
	    <div id="divWebEditor" style="margin:auto;">
		    <div id="barWebEditor">
				    WebEditor 1.0</div>
                       <br />
                       <br />
                       <br />
                       <br />
                       
                 <div style="border:silver solid 1px;width:400px;height:300px; margin:auto;
                                text-align:center;vertical-align:middle;padding:4px;">
                    <div style="text-align:left;">
                        <b>Upload de Arquivos</b>
                        <br />
                        <b>Dir:</b> <%=Request("d")%>
                    </div>
                        <br />
                        <br />
                        <br />
                    <div style="text-align:center;margin:auto;">
                        <asp:FileUpload ID="FileUpload1" cssclass="inputNmNovo" runat="server" />
                        <asp:FileUpload ID="FileUpload2" cssclass="inputNmNovo" runat="server" />
                        <asp:FileUpload ID="FileUpload3" cssclass="inputNmNovo" runat="server" />
                        <asp:FileUpload ID="FileUpload4" cssclass="inputNmNovo" runat="server" />
                        <asp:FileUpload ID="FileUpload5" cssclass="inputNmNovo" runat="server" />
                        <asp:FileUpload ID="FileUpload6" cssclass="inputNmNovo" runat="server" />
                        <asp:FileUpload ID="FileUpload7" cssclass="inputNmNovo" runat="server" />
                        <asp:FileUpload ID="FileUpload8" cssclass="inputNmNovo" runat="server" />
                        <asp:FileUpload ID="FileUpload9" cssclass="inputNmNovo" runat="server" />
                        <asp:FileUpload ID="FileUpload10" cssclass="inputNmNovo" runat="server" />
                        <br />
                        <br />
                        <asp:Label ID="msgUpload" runat="server" />
                    </div>                    
				 </div>
				 
		    <div class="divMenuEditorBaixo" style="width:480px;">
		        <asp:button CssClass="bt" id="btUpload" text="Upload" runat="server" />
			    <asp:button CssClass="bt" id="btVoltar" Text="Voltar" runat="server"/>
		    </div>
	    </div>
    </div>

</form>
</body>
</html>
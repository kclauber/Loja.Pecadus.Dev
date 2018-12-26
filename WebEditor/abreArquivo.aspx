<%@ page language="VB" autoeventwireup="false" validaterequest="false" inherits="abreArquivo, App_Web_x5dfcoqc" debug="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>WebEditor 1.0</title>
    <meta name="robots" content="NOINDEX, NOFOLLOW" />
    <link href="web.css" rel="Stylesheet"   type="text/css" />
</head>

<body>
<form id="form2" runat="server">

    <div id="divEscuro">
	    <br/>
	    <div id="divWebEditor">
		    <div id="barWebEditor">
				    WebEditor 1.0</div>
                       <br />
					    <asp:textBox id="webText" Height="500" Width="600"
					        Wrap="false" TextMode="MultiLine" runat="server"/>
    					
					    <input type="hidden" name="pastaRaiz"/>
					    <input type="hidden" name="nomeArq"/>
						    <div id="caminhoArq"></div>
    				
				    <div class="divMenuEditorBaixo">
					    <asp:button CssClass="bt" id="btSalvar" text="Salvar" runat="server" />
					    <asp:button CssClass="bt" id="btVoltar" text="Voltar" runat="server"/>
				    </div>
	    </div>
    </div>

</form>
</body>
</html>
<%@ page language="VB" autoeventwireup="false" validaterequest="false" inherits="_Default, App_Web_x5dfcoqc" debug="true" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>WebEditor 1.0</title>
    <meta name="robots" content="NOINDEX, NOFOLLOW" />
    <link href="web.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function rename(nome,display){
            if(display)
                document.getElementById('divRenomear').style.display = 'block';
            else
                document.getElementById('divRenomear').style.display = 'none';
                
            document.form1.NomeAntigo.value = nome;
            document.form1.NomeNovo.value = nome;
        }
        function lixeira(nome,display){
            if(display)
                document.getElementById('divLixeira').style.display = 'block';
            else
                document.getElementById('divLixeira').style.display = 'none';
                
            document.form1.arqLixeira.value = nome;
        }
        function apagar(nome,display){
            if(display)
                document.getElementById('divExcluir').style.display = 'block';
            else
                document.getElementById('divExcluir').style.display = 'none';
                
            document.form1.arqExcluir.value = nome;
        }
        function novaPasta(display){
            if(display)
                document.getElementById('divNovaPasta').style.display = 'block';
            else
                document.getElementById('divNovaPasta').style.display = 'none';
        }
        function renamePasta(nome,display){
            if(display)
                document.getElementById('divRnPasta').style.display = 'block';
            else
                document.getElementById('divRnPasta').style.display = 'none';
                
            document.form1.txtRnPastaAntigo.value = nome;
            document.form1.txtRnPastaNovo.value = nome;
        }
    </script>
</head>
<body>
<form id="form1" runat="server">

 <div id="divEscuro">
    <div id="divWebEditor">
		<div class="titulo">WebEditor 1.0</div>
			<div class="divCaixaConteudo">
				
					 <div id="divRaizConteudo">					   
                        <!-- Diretório atual -->
                        &nbsp;<asp:Label id="txtCurrentDir" runat="server"/>
                        <br />
                        &nbsp;&nbsp;
                        <asp:HyperLink id="imgLinkHome" ImageUrl="imagens/up.gif" runat="server" />
                        <asp:HyperLink id="LinkHome" Text="Voltar para a home" runat="server" />
                        <br />
                        &nbsp;&nbsp;
                        <asp:HyperLink id="upLinkI" ImageUrl="imagens/up.gif" runat="server"/>
                        <asp:HyperLink id="upLinkT" Text="Subir um diretório" runat="server"/>
                        <br />
                        &nbsp;&nbsp;
                        <asp:HyperLink id="imgAtu" ImageUrl="imagens/reload.gif" runat="server"/>
                        <asp:HyperLink id="LinkAtu" Text="Atualizar" runat="server"/>
                        <br />
                        <!-- Conteúdo -->
                        <asp:Label id="txtFileList" runat="server"/>                     
                     </div>
                     
                     <div style="display:none;" id="divRenomear">
					    <asp:Label ID="lblNomeNovo" Text="<br/>Renomear Arquivo:" runat="server" />
					    <asp:HiddenField ID="NomeAntigo" runat="server"/>
					    <asp:TextBox ID="NomeNovo" CssClass="inputNmNovo" runat="server"/><br />
					    <asp:Button ID="btNovoNome" CssClass="bt2"  Text="Renomear" runat="server" />
					    <input type="button" value="Cancelar" class="bt2" id="btRnCancelar" onclick="rename('',false)"/>
					 </div>
					 
                     <div style="display:none;" id="divLixeira">
					    <asp:Label ID="lblLixeira" Text="<br/>Enviar arquivo para lixeira?<br/>" runat="server" />
					    <asp:TextBox ID="arqLixeira" CssClass="inputNmNovo" runat="server"/><br />
					    <asp:Button ID="btLixeira" CssClass="bt2"  Text="Excluir" runat="server" />
					    <input type="button" value="Cancelar" class="bt2" id="btLxCancelar" onclick="lixeira('',false)"/>
					 </div>
					 
                     <div style="display:none;" id="divExcluir">
					    <asp:Label ID="lblExcluir" Text="<br/>Deseja excluir definitivamente este arquivo?:<br/>" runat="server" />
					    <asp:TextBox ID="arqExcluir" CssClass="inputNmNovo" runat="server"/><br />
					    <asp:Button ID="btExcluir" CssClass="bt2"  Text="Excluir" runat="server" />
					    <input type="button" value="Cancelar" class="bt2" id="btExCancelar" onclick="apagar('',false)"/>
					 </div>
					 
                     <div style="display:none;" id="divNovaPasta">
					    <asp:Label ID="lnlNvPasta" Text="<br/>Nova Pasta: " runat="server" />
					    <asp:TextBox ID="txtNovaPasta" CssClass="inputNmNovo" runat="server"/><br />
					    <asp:Button ID="btNovaPasta" CssClass="bt2"  Text="Criar Pasta" runat="server" />
					    <input type="button" value="Cancelar" class="bt2" id="btCrPstCancelar" onclick="novaPasta(false)"/>
					 </div>
					 
                     <div style="display:none;" id="divRnPasta">
					    <asp:Label ID="lblRnPasta" Text="<br/>Renomear Pasta: " runat="server" />
					    <asp:HiddenField ID="txtRnPastaAntigo" runat="server"/>
					    <asp:TextBox ID="txtRnPastaNovo" CssClass="inputNmNovo" runat="server"/><br />
					    <asp:Button ID="btRnPasta" CssClass="bt2"  Text="Renomear" runat="server" />
					    <input type="button" value="Cancelar" class="bt2" id="btRnPstCancelar" onclick="renamePasta('',false)"/>
					 </div>
				<br/>
				<br/>

				<div class="divMenuBaixo">
					
					<asp:ImageButton ID="novoDoc" ImageUrl="imagens/newdoc.gif" AlternateText="Novo arquivo" Width="16" Height="16" runat="server" />
					<img src="imagens/open_folder.gif" alt="Nova Pasta" onclick="novaPasta(true)" style="cursor:pointer;" width="16" height="16" runat="server" />
					<asp:ImageButton ID="upload"  ImageUrl="imagens/upload.gif" AlternateText="Upload de arquivos" Width="16" Height="16" runat="server" />
					<asp:ImageButton ID="lixeira" ImageUrl="imagens/lixo_v_16.gif" AlternateText="Lixeira" Width="16" Height="16" runat="server" />
					<asp:ImageButton ID="sair" ImageUrl="imagens/delete.gif" Width="16" Height="16" runat="server" />
				
				</div>
				
			</div>
	</div>
</div>	

</form>
</body>
</html>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="confirmacaoCompra.aspx.cs" Inherits="Loja.UI.Pecadus.processaRetornoPagSeg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="cadastro cont-int">
        <div class="container">
            <div class="row">
                <div class="col-md-12" style="text-align:center;">
                    <h2>SUA COMPRA FOI CONCLUÍDA COM SUCESSO!<br />
                        Este é o código da sua transação:
                        <asp:Label ID="lblTransacaoID" runat="server" /></h2>
                    <br />
                    <p style="color: #233241;">
                        Uma mensagem com os detalhes desta transação foi enviada para o seu e-mail cadastrado.<br>
                        Você também poderá acessar sua conta PagSeguro no endereço 
                    <a href="https://pagseguro.uol.com.br" target="_blank">https://pagseguro.uol.com.br</a><br />
                        para mais informações.
					<br>
                        <br>
                        Caso você receba uma notificação com status "cancelado" significa que o pagamento não foi aprovado.<br>
                        Neste caso, por favor entrar em contato com o PagSeguro no telefone (11) 5627-3440 e informe o número de sua compra.
					<br>
                        <br>
                        Se houver alguma dúvida, ou queira detalhes do envio do produto,<br />
                        por favor <a href="/Contato/" title="Entre em contato com a <%=ConfigurationManager.AppSettings["nomeSite"] %>">
                            <u>entre em contato conosco clicando aqui</u></a>.                    
                    </p>
                    <br />
                    <p><a class="btn btn-primary" href="/SexShop/" title="Adicionar mais produtos">Página inicial</a></p>
                </div>
            </div>
        </div>
    </section>

    <!-- Google Code for Convers&atilde;o 01 Conversion Page -->
    <!-- Conversão 01 (venda finalizada) -->
    <script type="text/javascript">
        /* <![CDATA[ */
        var google_conversion_id = 1011173106;
        var google_conversion_language = "en";
        var google_conversion_format = "3";
        var google_conversion_color = "ffffff";
        var google_conversion_label = "Eh8tCI7UpQUQ8o2V4gM";
        var google_conversion_value = 0;
	/* ]]> */
    </script>
    <script type="text/javascript" src="//www.googleadservices.com/pagead/conversion.js">
    </script>
    <noscript>
        <div style="display: inline;">
            <img height="1" width="1" style="border-style: none;" alt="" src="//www.googleadservices.com/pagead/conversion/1011173106/?value=0&amp;label=Eh8tCI7UpQUQ8o2V4gM&amp;guid=ON&amp;script=0" />
        </div>
    </noscript>
</asp:Content>

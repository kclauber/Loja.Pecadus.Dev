<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeFile="erro.aspx.cs" Inherits="erro" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <link rel="stylesheet" type="text/css" href="/css/custom-theme/jquery.ui.all.css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/scripts/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/scripts/jquery.ui.accordion.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="cadastro cont-int">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h2>Desculpe, não foi possivel acessar a página no momento.</h2>
                <p style="color: #233241;">
                    <b>Qual pode ser o problema?</b><br />
                    <ul style="color: #233241;">
                        <li>- Verifique se você digitou corretamente o endereço desejado.</li>
                        <li>- O conteúdo pode ter sido removido ou não estar mais disponível.</li>
                        <li>- O servidor pode estar fora do ar no momento.</li>
                    </ul>
                </p>                 
                <p style="color: #233241;">
                    Efetue uma nova pesquisa ou, se preferir, tente novamente em alguns instantes.<br />
                    Obrigado.
                </p>
            </div>
        </div>
    </div>
    </section>    
</asp:Content>

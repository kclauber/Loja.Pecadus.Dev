<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeFile="finalizarCompra.aspx.cs" Inherits="finalizarCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style>
        .gly-spin {
            -webkit-animation: spin 2s infinite linear;
            -moz-animation: spin 2s infinite linear;
            -o-animation: spin 2s infinite linear;
            animation: spin 2s infinite linear;
        }
        @-moz-keyframes spin {
          0% {
            -moz-transform: rotate(0deg);
          }
          100% {
            -moz-transform: rotate(359deg);
          }
        }
        @-webkit-keyframes spin {
          0% {
            -webkit-transform: rotate(0deg);
          }
          100% {
            -webkit-transform: rotate(359deg);
          }
        }
        @-o-keyframes spin {
          0% {
            -o-transform: rotate(0deg);
          }
          100% {
            -o-transform: rotate(359deg);
          }
        }
        @keyframes spin {
          0% {
            -webkit-transform: rotate(0deg);
            transform: rotate(0deg);
          }
          100% {
            -webkit-transform: rotate(359deg);
            transform: rotate(359deg);
          }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="cadastro cont-int">
    <div class="container">
        <div class="row">
            <div class="col-md-8 center-block" style="text-align:center;">
                <h2>OBRIGADO PELA SUA COMPRA!<br />
                    Já está quase tudo pronto.
                </h2>
                <p style="color: #233241;">
                    Por favor aguarde, para finalizar o seu processo de pagamento vamos encaminhá-lo(a) ao site do Pag Seguro.<br />
                    Lá você pode escolher a melhor opção para pagar pelo seu pedido.
                </p>
                <p style="color: #233241;">
                    Depois de tudo concluído você será redirecionado(a) para cá e verá o número do seu pedido.<br />
                </p>

                <p>
                    <i class="glyphicon glyphicon-refresh gly-spin" style="font-size:32px;"></i>
                </p>
                <p style="color: #233241; font-size:smaller;">
                    Obs.: Caso não seja redirecionado(a) em alguns segundos, por favor 
                    <asp:HyperLink ID="redirectPagSeguro" runat="server"><u>clique aqui</u></asp:HyperLink>.
                </p>
            </div>
        </div>
    </div>
    </section>
    <script>
        $(document).ready(function () {
            setTimeout(function () {
                window.location.href = '<%=pagSeguroURL%>'
            }, 5000);
        });
    </script>
</asp:Content>


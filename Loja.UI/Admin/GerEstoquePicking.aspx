<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="GerEstoquePicking.aspx.cs" Inherits="Loja.UI.Pecadus.Admin.GerEstoquePicking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        window.onload = function () { document.getElementById("<%=txtEan.ClientID %>").focus(); }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="migalha" style="text-align: left; padding-left: 10px; font">
        <a href="PainelControle.aspx"><u>Painel de controle</u></a> > <a href="GerEstoqueSaida.aspx">
            <u>Saída (Check-out)</u></a> > Picking</div>
    <hr />
    <div class="pv">
        <h2>
            Sistema de Picking (Coleta)</h2>
        <asp:Panel ID="pnlSeparacao" runat="server">
            <table align="center" style="width: 450px; border: silver solid 1px;">
                <tr>
                    <td align="right">
                        <b>Cód Barras:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEan" runat="server" Width="250px" MaxLength="13" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblEan" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Id Produto:</b>
                    </td>
                    <td width="300">
                        <asp:Label ID="lblIDProduto" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Titulo:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblTitulo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Qtd:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblQtdSeparada" runat="server" />
                        /
                        <asp:Label ID="lblQtd" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Estoque:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblEstoque" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnOk" runat="server" Text="OK" OnClick="btnOk_Click" />
                        <asp:Button ID="btnPular" runat="server" Text="Pular" OnClick="btnPular_Click" CausesValidation="false" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnVoltar_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlConfirmacao" Visible="false" runat="server">
            <table align="center" style="width: 400px; border: silver solid 1px;">
                <tr>
                    <td align="left">
                        <asp:CheckBox ID="chkEmbalagem" runat="server" />
                        Os itens foram colocados na embalagem de envio?
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:CheckBox ID="chkEnderecar" runat="server" />
                        A embalagem foi fechada e endereçada corretamente?
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        Ao clicar no botão continuar, o sistema atualizará o estoque deste(s) produto(s).<br />
                        <u>Um e-mail</u> será enviado ao cliente avisando-o que o produto será enviado.
                        <br />
                        <br />
                        Informe abaixo o código de envio se houver.<br />
                        <asp:TextBox ID="txtCodigoEnvio" runat="server" Width="250px" />
                        <br />
                        <br />
                        Certifique-se que tudo está correto antes de continuar.
                        <br />
                        <br />
                        Para encerrar o processo clique no botão "confirmar".
                    </td>
                </tr>
                <tr>
                    <td align="center" style="padding: 20px;">
                        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlRetorno" runat="server" Visible="false">
            <table align="center" style="width: 400px; border: silver solid 1px;">
                <tr>
                    <td align="center" style="padding: 50px;">
                        <asp:Label ID="lblMsgRetorno" runat="server" />
                        <br />
                        <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

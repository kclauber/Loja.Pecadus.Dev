<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="GerEstoqueEntrada.aspx.cs" Inherits="Loja.UI.Pecadus.Admin.GerEstoqueEntrada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        onload = function () {
            if ($("#<%=txtEstoque.ClientID %>").attr("disabled"))
                $("#<%=txtEan.ClientID %>").focus();
            else
                $("#<%=txtEstoque.ClientID %>").focus();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="migalha" style="text-align: left; padding-left: 10px; font">
        <a href="PainelControle.aspx"><u>Painel de controle</u></a> > Entrada (Check-in)</div>
    <hr />
    <div class="pv">
        <h2>
            Sistema de Entrada (Check-in)</h2>
        <table align="center" width="400">
            <tr>
                <td align="center" colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" nowrap>
                    Cód. Barras:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEan" runat="server" Width="250px" MaxLength="13"></asp:TextBox>
                    <asp:Button ID="btnFiltro" runat="server" Text="Filtrar" CausesValidation="false"
                        OnClick="btnFiltro_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    ID:
                </td>
                <td align="left">
                    <asp:Label ID="lblID" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    Título:
                </td>
                <td align="left">
                    <asp:Label ID="lblTitulo" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    Imagem:
                </td>
                <td style="text-align: left;">
                    <asp:HyperLink ID="lnkImg" Target="_blank" runat="server" ForeColor="Blue" Font-Underline="true" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    Descrição:
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="lblDesc" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    Observação:
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="lblObservao" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:RangeValidator ID="rngEstoque" runat="server" MinimumValue="1" MaximumValue="999"
                        ErrorMessage="Quantidade não pode ser negativa (max 999)" ControlToValidate="txtEstoque">*</asp:RangeValidator>
                    Qtd.:
                </td>
                <td>
                    <asp:TextBox ID="txtEstoque" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                    <asp:Button ID="btnCadastrar" runat="server" Text="OK" OnClick="btnCadastrar_Click"
                        Enabled="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

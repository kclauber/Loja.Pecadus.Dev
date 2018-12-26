<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="PainelControle.aspx.cs" Inherits="Loja.UI.Pecadus.Admin.PainelControle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 400px; display: table-cell; vertical-align: middle;">
        <table>
            <tr>
                <td>
                    Cadastro
                </td>
            </tr>
            <tr>
                <td align="center">
                    <ol>
                        <li><a href="CadDistribuidor.aspx">Distribuidor</a></li>
                        <li><a href="CadCategorias.aspx">Categorias</a></li>
                        <li><a href="CadProdutos.aspx">Produtos</a></li>
                    </ol>
                </td>
            </tr>
            <tr>
                <td>
                    Gerenciamento de EStoque
                </td>
            </tr>
            <tr>
                <td align="center">
                    <ol>
                        <li><a href="GerEstoqueEntrada.aspx">Entrada (Check-in)</a></li>
                        <li><a href="GerEstoqueSaida.aspx">Saída (Check-out)</a></li>
                    </ol>
                </td>
            </tr>
            <tr>
                <td>
                    Ferramentas Administrativas
                </td>
            </tr>
            <tr>
                <td align="center">
                    <ol>
                        <li><a href="PalavrasBuscadas.aspx">Palavras Buscadas</a></li>
                        <li><a href="Relatorios.aspx">Relatorios</a></li>
                    </ol>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

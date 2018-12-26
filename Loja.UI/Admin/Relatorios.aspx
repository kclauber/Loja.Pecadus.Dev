<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" 
CodeFile="Relatorios.aspx.cs" Inherits="Loja.UI.Pecadus.Admin.Relatorios" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table aling="center">
        <tr>
            <td align="right">
                Vlr Total Produtos:
            </td>
            <td>
                <asp:Label ID="lblVlrProdutos" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td align="right">
                Qtd Produtos:
            </td>
            <td>
                <asp:Label ID="lblQtdProdutos" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td align="right">
                Valor Médio:
            </td>
            <td>
                <asp:Label ID="lblTicketMedio" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td align="right">
                Produtos sem imagem:
            </td>
            <td>
                <asp:Label ID="lblSemimagem" runat="server" Text="" />
            </td>
        </tr>
    </table>
    <br />
    <hr />
    <div style="width: 550px; margin: auto; border: gray solid 2px;">
        <br />
        <b>Estoque de Produtos</b>
        <br />
        <asp:Chart ID="chartProdutos" Width="500" runat="server">
            <series>
                <asp:Series Name="Series1">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
        </asp:Chart>
    </div>
    <br />
    <hr />
    <br />
    <b>Produtos x Vendas</b> - 30 por Pg
    <asp:GridView ID="GridProdutosVendidos" Width="300px" runat="server" AutoGenerateColumns="False"
        AllowSorting="True" AllowPaging="True" DataSourceID="prodVendidosDS" CellPadding="4"
        ForeColor="#333333" GridLines="None" PageSize="30">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="id" SortExpression="id" HeaderText="ID" />
            <asp:BoundField DataField="titulo" SortExpression="titulo" HeaderText="Título" />
            <asp:BoundField DataField="numVendas" SortExpression="numVendas" HeaderText="Vendas" />
        </Columns>
        <EmptyDataTemplate>
            <table align="center">
                <tr>
                    <td>
                        Não há registros
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle HorizontalAlign="Center" BackColor="#507CD1" ForeColor="White" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="prodVendidosDS" runat="server" />
</asp:Content>

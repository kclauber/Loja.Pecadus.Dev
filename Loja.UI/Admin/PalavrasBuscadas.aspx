<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" 
CodeFile="PalavrasBuscadas.aspx.cs" Inherits="Loja.UI.Pecadus.Admin.PalavrasBuscadas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView ID="GridBusca" runat="server" PageSize="50" 
        DataSourceID="palavrasBuscadasDS" AllowPaging="True"
        AutoGenerateColumns="False" AllowSorting="True" CellPadding="4" 
        ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="true" SortExpression="id">
                <ItemStyle Width="40px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="palavra_chave" HeaderText="Palavra Chave" SortExpression="palavra_chave">
                <ItemStyle Width="300px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="hits" HeaderText="Hits" SortExpression="hits">
                <ItemStyle Width="40px"></ItemStyle>
            </asp:BoundField>
        </Columns>
        <EmptyDataTemplate>
            <table>
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
    <asp:SqlDataSource ID="palavrasBuscadasDS" runat="server">
    </asp:SqlDataSource>
</asp:Content>

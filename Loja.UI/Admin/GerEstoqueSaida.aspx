<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="GerEstoqueSaida.aspx.cs" Inherits="Loja.UI.Pecadus.Admin.GerEstoqueSaida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="migalha" style="text-align: left; padding-left: 10px; font">
        <a href="PainelControle.aspx"><u>Painel de controle</u></a> > Saída (Check-out)</div>
    <hr />
    <div class="pv">
        <div id="header" style="display: block;">
            <h2>
                Sistema de Saída (Check-out)</h2>
            Status do pedido:
            <asp:DropDownList ID="ddlStatus" runat="server">
                <asp:ListItem Text="Aprovado" Value="Aprovado" />
                <asp:ListItem Text="Aguardando Pagto" Value="Aguardando Pagto" />
                <asp:ListItem Text="Cancelado" Value="Cancelado" />
                <asp:ListItem Text="Completo" Value="Completo" />
                <asp:ListItem Text="Devolvido" Value="Devolvido" />
                <asp:ListItem Text="Enviado" Value="Enviado" />
            </asp:DropDownList>
            &nbsp; De:
            <asp:TextBox ID="txtDataDe" runat="server"></asp:TextBox>
            Até:
            <asp:TextBox ID="txtDataAte" runat="server"></asp:TextBox>
            <asp:Button ID="btnFiltro" runat="server" Text="Filtrar" OnClick="btnFiltro_Click" />
            <br />
            <br />
            <asp:GridView ID="GridPedidos" Width="600px" runat="server" AutoGenerateColumns="False"
                AllowSorting="True" DataSourceID="pedidoDS" AllowPaging="True" CellPadding="4"
                ForeColor="#333333" GridLines="None" PageSize="10" OnRowDataBound="GridPedidos_RowDataBound"
                OnRowCommand="GridPedidos_RowCommand">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkSelect" runat="server">Selecionar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="idCliente" SortExpression="idCliente" HeaderText="ID Cliente"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="cliente" SortExpression="cliente" HeaderText="Cliente" />
                    <asp:BoundField DataField="id" SortExpression="id" HeaderText="ID Pedido" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="TipoFrete" SortExpression="TipoFrete" HeaderText="Tipo Frete"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="status" SortExpression="status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField SortExpression="dtCadastro" HeaderText="Dt Cadastro" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblDtCadastro" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                        Não foi encontrado nenhum pedido
                </EmptyDataTemplate>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle HorizontalAlign="Center" BackColor="#507CD1" ForeColor="White" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:SqlDataSource ID="pedidoDS" runat="server" />
        </div>
        <hr style="border: 0; border-top: silver solid 1px; width: 500px;" />
        <table width="550" style="border: silver solid 1px">
            <tr>
                <td align="center" colspan="2">
                    <h3>
                        Dados Cliente</h3>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>Id Cliente:</b>
                </td>
                <td width="400">
                    <asp:Label ID="lblIDCliente" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>Cliente:</b>
                </td>
                <td>
                    <asp:Label ID="lblNome" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>Endereço:</b>
                </td>
                <td>
                    <asp:Label ID="lblEndereco" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>Cidade / Estado / CEP:</b>
                </td>
                <td>
                    <asp:Label ID="lblCidadeEstadoCep" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>E-mail:</b>
                </td>
                <td>
                    <asp:Label ID="lblEmail" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>Data Cadastro:</b>
                </td>
                <td>
                    <asp:Label ID="lblDtCadastroCliente" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <hr style="border: 0; border-top: silver solid 1px; width: 500px;" />
        <asp:Repeater ID="repPedidos" runat="server" OnItemDataBound="repPedidos_ItemDataBound">
            <HeaderTemplate>
                <table width="550" style="border: silver solid 1px">
                    <tr>
                        <td align="center" colspan="2">
                            <h3>
                                Dados dos Pedidos</h3>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="right">
                        <b>Id Pedido:</b>
                    </td>
                    <td width="400">
                        <asp:Label ID="lblIDPedido" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Id Transacao:</b>
                    </td>
                    <td width="400">
                        <asp:Label ID="lblTransacaoID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Frete:</b>
                    </td>
                    <td width="400">
                        <asp:Label ID="lblFrete" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Extras:</b>
                    </td>
                    <td width="400">
                        <asp:Label ID="lblExtras" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>TipoPagamento:</b>
                    </td>
                    <td width="400">
                        <asp:Label ID="lblPagamento" runat="server" />
                    </td>
                </tr>
                <tr style="color: red;">
                    <td align="right">
                        <b>Status:</b>
                    </td>
                    <td width="400">
                        <b>
                            <asp:Label ID="lblStatus" runat="server" /></b>
                    </td>
                </tr>
                <tr style="color: red;">
                    <td align="right">
                        <b>Anotação:</b>
                    </td>
                    <td width="400">
                        <asp:Label ID="lblAnotacao" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Data Cadastro:</b>
                    </td>
                    <td width="400">
                        <asp:Label ID="lblDtCadastroPedido" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnPicking" Text="Separar para envio" Visible="false" OnCommand="btnPicking_Click"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Panel ID="pnlSemRegistro" Visible="false" runat="server">
                            <table width="450" style="border: silver solid 2px">
                                <tr>
                                    <td align="center">
                                        <h3>
                                            Não há registros</h3>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Repeater ID="repItens" runat="server" OnItemDataBound="repItens_ItemDataBound">
                            <HeaderTemplate>
                                <table width="450" style="border: silver solid 2px">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <h3>
                                                Dados dos Itens</h3>
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
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
                                        <b>Cód Barras:</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEan" runat="server" />
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
                                        <asp:Label ID="lblQtdProduto" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <b>Valor:</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblValorProduto" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr style="border: 0; border-top: silver solid 1px; width: 400px;" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

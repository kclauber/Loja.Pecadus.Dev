<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true"
    CodeFile="CadCategorias.aspx.cs" Inherits="Loja.UI.Pecadus.Admin.CadCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Admin</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="migalha" style="text-align: left; padding-left: 10px; font">
        <a href="PainelControle.aspx"><u>Painel de controle</u></a> > Cadastro de categorias</div>
    <hr />
    <div class="pv">
        <asp:Panel ID="pnlGrid" runat="server">
            <h2>
                Cadastrado de Categorias</h2>
            <asp:GridView ID="GridCategorias" Width="600px" runat="server" AutoGenerateColumns="False"
                AllowSorting="True" OnRowCommand="GridCategorias_RowCommand" OnRowDataBound="GridCategorias_RowDataBound"
                DataSourceID="categoriasDS" AllowPaging="True" CellPadding="4" ForeColor="#333333"
                GridLines="None" PageSize="10">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server">Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="true" SortExpression="id" />
                    <asp:BoundField DataField="idCategoriaPai" HeaderText="ID. Pai" SortExpression="idCategoriaPai" />
                    <asp:BoundField DataField="titulo" HeaderText="Título" SortExpression="titulo">
                        <ItemStyle Width="230px"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText = "Ativo?">
                        <ItemTemplate>
                            <asp:Label ID="lblAtivo" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    Ainda não existem distribuidores categorias
                </EmptyDataTemplate>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle HorizontalAlign="Center" BackColor="#507CD1" ForeColor="White" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:SqlDataSource ID="categoriasDS" runat="server"></asp:SqlDataSource>
            <br />
            <br />
            <asp:Button ID="btnCadastroNovo" runat="server" Text=">> Cadastrar novo <<" CausesValidation="false"
                OnClick="btnCadastroNovo_Click" />
        </asp:Panel>
        <asp:Panel ID="pnlCadastro" Visible="false" runat="server">
            <br />
            <div style="margin: auto;">
                <asp:ValidationSummary ID="vSumm1" runat="server" DisplayMode="BulletList"
                    HeaderText="Por favor, preencha os valores destacados ou &lt;br&gt;corrija os seguintes problemas:" />
            </div>
            <table>
                <tr>
                    <td align="right">
                        ID:
                    </td>
                    <td align="left">
                        <asp:Label ID="lblID" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Categoria. Pai:
                    </td>
                    <td align="left" valign="middle">
                        <asp:DropDownList Width="257px" ID="ddlCategPai" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:RequiredFieldValidator ID="reqTitulo" runat="server" ErrorMessage="Título" ControlToValidate="txtTitulo">*</asp:RequiredFieldValidator>
                        Título:
                    </td>
                    <td align="left">
                        <asp:TextBox Width="250px" ID="txtTitulo" runat="server"/>
                    </td>
                </tr>
                <tr style="display:none">
                    <td align="right">
                        Palavras-Chave:
                    </td>
                    <td align="left" valign="middle">
                        <asp:TextBox ID="txtPalavrasChave" runat="server" Height="100px" MaxLength="200" TextMode="MultiLine" Width="250px"/>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        
                    </td>
                    <td align="left">
                        Ativo?: 
                        <asp:CheckBox ID="chkAtivo" Checked="true" runat="server" />
                    </td>
                </tr>
                <tr><td>&nbsp;</td></tr>
                <tr>
                    <td align="center" colspan="2">
                        <input type="button" value="<< Voltar" onclick="javascript:window.location='CadCategorias.aspx';" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnCadastrar" runat="server" Text="  Cadastrar  " OnClick="btnCadastrar_Click" />
                        <asp:Button ID="btnAtualizar" runat="server" Text="  Atualizar  " Onclick="btnAtualizar_Click" Visible="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

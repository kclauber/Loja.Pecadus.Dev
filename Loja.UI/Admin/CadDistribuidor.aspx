<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="CadDistribuidor.aspx.cs" Inherits="Loja.UI.Pecadus.Admin.CadDistribuidor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Admin</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="migalha" style="text-align: left; padding-left: 10px; font">
        <a href="PainelControle.aspx"><u>Painel de controle</u></a> > Cadastro de distribuidores</div>
    <hr />
    <div class="pv">
        <asp:Panel ID="pnlGrid" runat="server">
            <h2>
                Cadastrado de Distribuidores</h2>
            <asp:GridView ID="GridDistribuidor" Width="600px" runat="server" AutoGenerateColumns="False"
                AllowSorting="True" OnRowCommand="GridDistribuidor_RowCommand" OnRowDataBound="GridDistribuidor_RowDataBound"
                DataSourceID="distDS" AllowPaging="True" CellPadding="4" ForeColor="#333333"
                GridLines="None" PageSize="10">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server">Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="true" SortExpression="id" />
                    <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome" />
                    <asp:BoundField DataField="site" HeaderText="Site" SortExpression="site">
                        <ItemStyle Width="130px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="eMail" HeaderText="E-mail" SortExpression="eMail">
                        <ItemStyle Width="130px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="telefone" HeaderText="Telefone" SortExpression="telefone" />
                </Columns>
                <EmptyDataTemplate>
                    Ainda não existem distribuidores cadastrados
                </EmptyDataTemplate>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle HorizontalAlign="Center" BackColor="#507CD1" ForeColor="White" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:SqlDataSource ID="distDS" runat="server" />
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
                        <asp:RequiredFieldValidator ID="reqNome" runat="server" ErrorMessage="Nome" ControlToValidate="txtNome">*</asp:RequiredFieldValidator>
                        Nome:
                    </td>
                    <td align="left">
                        <asp:TextBox Width="250px" ID="txtNome" runat="server" MaxLength="30"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Site:
                    </td>
                    <td align="left">
                        <asp:TextBox Width="250px" ID="txtSite" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        E-mail:
                    </td>
                    <td align="left" valign="middle">
                        <asp:TextBox Width="250px" ID="txtEmail" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Telefone:
                    </td>
                    <td align="left" valign="middle">
                        <asp:TextBox Width="250px" ID="txtTelefone" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Observação:
                    </td>
                    <td align="left" valign="middle">
                        <asp:TextBox ID="txtObservacao" runat="server" Height="100px" MaxLength="200" TextMode="MultiLine" Width="250px"/>
                    </td>
                </tr>

                <tr><td>&nbsp;</td></tr>
                <tr>
                    <td align="center" colspan="2">
                        <input type="button" value="<< Voltar" onclick="javascript:window.location='CadDistribuidor.aspx';" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnCadastrar" runat="server" Text="  Cadastrar  " OnClick="btnCadastrar_Click" />
                        <asp:Button ID="btnAtualizar" runat="server" Text="  Atualizar  " Onclick="btnAtualizar_Click" Visible="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

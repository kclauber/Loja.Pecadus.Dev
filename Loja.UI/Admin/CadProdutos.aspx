<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="CadProdutos.aspx.cs" Inherits="Loja.UI.Pecadus.Admin.CadProdutos" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Admin</title>
    <script src="../scripts/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../scripts/CalculoPV.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            callCalcularPV();

            if ($('#<%=txtDescricaoCurta.ClientID %>').val() != undefined)
                $('#<%=txtDescricaoCurta.ClientID %>').limit('150', '#charsLeft');
        });

        function colapse(el) {
            el = $(el);
            el.toggle(700);
        }

        function callCalcularPV()
        { 
            CalcularPV('<%=ConfigurationManager.AppSettings["fretePCU"] %>', 
                       '<%=ConfigurationManager.AppSettings["imposto"] %>', 
                       '<%=ConfigurationManager.AppSettings["txOperadora"] %>', 
                       '<%=ConfigurationManager.AppSettings["custoFixPeriodo"] %>', 
                       '<%=ConfigurationManager.AppSettings["custoVarPeriodo"] %>', 
                       '<%=ConfigurationManager.AppSettings["perdaPeriodo"] %>', 
                       '<%=ConfigurationManager.AppSettings["MKP"] %>');
        }
    </script>
    <style type="text/css">
        .Text
        {
            width: 60px;
            display: block;
        }
        .border0
        {
            border: 0;
        }
        .tablePreco
        {
            width: 500px;
            border: black solid 1px;
        }
        .tablePreco td
        {
            border: 0;
            border-top: silver solid 1px;
            padding: 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="migalha" style="text-align: left; padding-left: 10px; font">
        <a href="PainelControle.aspx"><u>Painel de controle</u></a> > Cadastro de produtos</div>
    <hr />
    <div class="pv">
        <asp:Panel ID="pnlGrid" runat="server">
            <h2>
                Cadastrado de Produtos</h2>
            <div id="header">
                <asp:GridView ID="GridProdutos" Width="600px" runat="server" AutoGenerateColumns="False"
                    AllowSorting="True" OnRowCommand="GridProdutos_RowCommand" OnRowDataBound="GridProdutos_RowDataBound"
                    DataSourceID="prodDS" AllowPaging="True" CellPadding="4" ForeColor="#333333"
                    GridLines="None" PageSize="10">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server">Editar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="id" SortExpression="id" HeaderText="ID" InsertVisible="False"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="titulo" SortExpression="titulo" HeaderText="Título" />
                        <asp:TemplateField SortExpression="preco" HeaderText="Preço">
                            <ItemTemplate>
                                <asp:Label ID="lblPreco" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="estoque" SortExpression="estoque" HeaderText="Estoque"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField SortExpression="ativo" HeaderText="Ativo?" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblAtivo" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        Ainda não existem produtos cadastrados
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle HorizontalAlign="Center" BackColor="#507CD1" ForeColor="White" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:SqlDataSource ID="prodDS" runat="server" />
                <br />
                <br />
                Buscar:
                <asp:TextBox ID="txtBusca" Width="400px" runat="server" /><br />
                <asp:DropDownList ID="ddlEstoque" runat="server">
                    <asp:ListItem Text="Com e Sem Estoque" Value="0" />
                    <asp:ListItem Text="Com Estoque" Value="1" />
                    <asp:ListItem Text="Sem Estoque" Value="2" />
                </asp:DropDownList>
                <asp:DropDownList ID="ddlAtivo" runat="server">
                    <asp:ListItem Text="Ativo/Inativo" Value="0" />
                    <asp:ListItem Text="Ativo" Value="1" />
                    <asp:ListItem Text="Inativo" Value="2" />
                </asp:DropDownList>
                <asp:Button ID="btnFiltro" runat="server" Text="Filtrar" CausesValidation="false"
                    OnClick="btnFiltro_Click" />
                <br />
                <br />
                <asp:Button ID="btnCadastroNovo" runat="server" Text=">> Cadastrar novo <<" CausesValidation="false"
                    OnClick="btnCadastroNovo_Click" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCadastro" Visible="false" runat="server">
            <br />
            <div style="margin: auto;">
                <asp:ValidationSummary ID="vSumm1" runat="server" DisplayMode="List" 
                HeaderText="Por favor, preencha os valores destacados ou &lt;br&gt;corrija os seguintes problemas:" />
            </div>
            <!-- ID, Distribuidor, Categoria, Categoria Pai  -->
            <table>
                <tr>
                    <td>
                        ID:
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="reqDistribuidor" runat="server" ErrorMessage="Distribuidor"
                            ControlToValidate="ddlDistribuidor">*</asp:RequiredFieldValidator>
                        Distribuidor:
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="reqCategoriaPai" runat="server" ErrorMessage="Categoria Pai"
                            ControlToValidate="ddlCategoriaPai">*</asp:RequiredFieldValidator>
                        Categoria Pai:
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="reqCategoria" runat="server" ErrorMessage="Categoria"
                            ControlToValidate="ddlCategoria">*</asp:RequiredFieldValidator>
                        Categoria:
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblID" runat="server" Text="" />
                    </td>
                    <td>
                        <asp:DropDownList Width="130px" ID="ddlDistribuidor" runat="server" Height="16px" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategoriaPai" runat="server" AutoPostBack="True" Width="130px"
                            OnSelectedIndexChanged="ddlCategoriaPai_SelectedIndexChanged" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategoria" runat="server" Width="130px" />
                    </td>
                </tr>
            </table>
            <!-- Cód. Barras, Título, Imagem, Vídeo  -->
            <table align="center">
                <tr>
                    <td align="right">
                        Cód. Barras:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtEan" runat="server" Width="350px" MaxLength="13" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:RequiredFieldValidator ID="reqTitulo" runat="server" ErrorMessage="Título" ControlToValidate="txtTitulo">*</asp:RequiredFieldValidator>
                        Título:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtTitulo" runat="server" Width="350px" MaxLength="65" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Imagem:
                    </td>
                    <td style="text-align: left;">
                        <asp:FileUpload ID="uplImagem" Width="357px" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Vídeo:
                    </td>
                    <td style="text-align: left;">
                        <asp:FileUpload ID="uplVideo" Width="357px" runat="server" />
                        <asp:Button ID="btnImg" runat="server" Text="ok" OnClick="btnImg_Click" UseSubmitBehavior="False"
                            ValidationGroup="a" Enabled="False" />
                    </td>
                </tr>
            </table>
            <br />
            <!-- Grids de Imagens e Vídeos  -->
            <table>
                <tr>
                    <td align="center" valign="top">
                        <asp:GridView ID="grdImagensCadastradas" runat="server" AutoGenerateColumns="false"
                            CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grdImgCad_RowCommand"
                            OnRowDataBound="grdImgCad_RowDataBound">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="id" SortExpression="id" HeaderText="ID" InsertVisible="False"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50" />
                                <asp:BoundField DataField="titulo" SortExpression="titulo" HeaderText="Título" />
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataTemplate>
                                Nenhuma imagem cadastrada
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top">
                        <asp:Panel ID="pnlVideo" runat="server">
                        </asp:Panel>
                        <br />
                        <asp:GridView ID="grdVideosCadastrados" runat="server" AutoGenerateColumns="false"
                            CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grdVidCad_RowCommand"
                            OnRowDataBound="grdVidCad_RowDataBound">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="id" SortExpression="id" HeaderText="ID" InsertVisible="False"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50" />
                                <asp:BoundField DataField="titulo" SortExpression="titulo" HeaderText="Título" />
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataTemplate>
                                Nenhum vídeo cadastrado
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <!-- Resumo, Descrição, Palavras-chave -->
            <table align="center">
                <tr>
                    <td align="left">
                        <asp:RequiredFieldValidator ID="reqResumo" runat="server" ErrorMessage="Resumo" ControlToValidate="txtDescricaoCurta">*</asp:RequiredFieldValidator>
                        Resumo:<br />
                        <asp:TextBox ID="txtDescricaoCurta" runat="server" Height="90px" MaxLength="150"
                            TextMode="MultiLine" Width="500px" />
                        <br />
                        Limite 150chars. - Restam: <span id="charsLeft" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:RequiredFieldValidator ID="reqDescricao" runat="server" ErrorMessage="Descrição do produto"
                            ControlToValidate="txtDescricaoCompleta" Enabled="false">*</asp:RequiredFieldValidator>
                        Descrição:<br />
                        <CKEditor:CKEditorControl ID="txtDescricaoCompleta" Width="500" Height="350" BasePath="../ckeditor/"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Palavras-chave: (separar as palavras por virgula)<br />
                        <asp:TextBox ID="txtPalavrasChave" runat="server" Height="50px" MaxLength="200" TextMode="MultiLine"
                            Width="500px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Observação:<br />
                        <asp:TextBox ID="txtObservacao" runat="server" Height="50px" MaxLength="200" TextMode="MultiLine"
                            Width="500px" />
                    </td>
                </tr>
            </table>
            <br />
            <!--  Preco de Custo (PCU), Desconto %, Preço (PV), Valor Frete, Peso, Estoque, Dt. Cadastro -->
            <table width="500">
                <tr>
                    <td nowrap align="center">
                        <asp:RequiredFieldValidator ID="reqFrete" runat="server" ErrorMessage="Frete (se não houver digite ZERO)"
                            ControlToValidate="txtFrete">*</asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rangeFrete" runat="server" ControlToValidate="txtFrete" ErrorMessage="O valor do frete deve ser numérico"
                            MaximumValue="9999,99" MinimumValue="0" Type="Double">*</asp:RangeValidator>
                        Valor Frete:
                    </td>
                    <td nowrap align="center">
                        <asp:RequiredFieldValidator ID="reqPeso" runat="server" ErrorMessage="Peso (se não houver digite ZERO)"
                            ControlToValidate="txtPeso">*</asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rangePeso" runat="server" ControlToValidate="txtPeso" ErrorMessage="O peso deve ser numérico"
                            MaximumValue="9999" MinimumValue="0" Type="Double">*</asp:RangeValidator>
                        Peso (gramas):
                    </td>
                    <td nowrap align="center">
                        <asp:RequiredFieldValidator ID="reqDesconto" runat="server" ErrorMessage="Desconto (se não houver digite ZERO)"
                            ControlToValidate="txtDesconto">*</asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rangeDesconto" runat="server" ControlToValidate="txtDesconto"
                            ErrorMessage="O desconto deve ser entre 0% e 30%" MinimumValue="0" MaximumValue="30"
                            Type="Double">*</asp:RangeValidator>
                        Desconto (%):
                    </td>
                    <td nowrap align="center">
                        <asp:RequiredFieldValidator ID="reqEstoque" runat="server" ErrorMessage="Estoque"
                            ControlToValidate="txtEstoque">*</asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rangeEstoque" runat="server" ControlToValidate="txtEstoque"
                            ErrorMessage="O valor de estoque deve ser numérico" MaximumValue="9999" MinimumValue="0"
                            Type="Integer">*</asp:RangeValidator>
                        Estoque:
                    </td>
                    <td nowrap align="center">
                        Dt. Cadastro:
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:TextBox ID="txtFrete" runat="server" Width="80px">0,00</asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtPeso" runat="server" Width="80px">0</asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtDesconto" runat="server" Width="80px">0</asp:TextBox>
                    </td>                    
                    <td align="center">
                        <asp:TextBox ID="txtEstoque" runat="server" Width="80px">0</asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtDtCadastro" runat="server" Enabled="False" Width="80px" />
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" cellpadding="0" cellspacing="0" align="center" class="tablePreco">
                <tr>
                    <td class="border0">
                        <asp:RequiredFieldValidator ID="reqPrecoCusto" runat="server" ErrorMessage="Preço de Custo"
                            ControlToValidate="txtPrecoCusto">*</asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rangePrecoCusto" runat="server" ControlToValidate="txtPrecoCusto"
                            ErrorMessage="O preço de custo do produto deve ser númerico" MaximumValue="9999,99"
                            MinimumValue="0" Type="Double">*</asp:RangeValidator>
                        <b style="color: red;">PCU (R$)</b><br />
                        <input class="Text" id="txtPrecoCusto" value="0,00" type="text" onblur="callCalcularPV();" runat="server" />
                    </td>
                    <td class="border0">
                        Frete PCU (%)<br />
                        <input class="Text" id="txtFretePCU" type="text" disabled runat="server" />
                    </td>
                    <td class="border0">
                        <b style="color: red;">CAM (R$)<br />
                            <input class="Text" id="txtCAM" type="text" disabled runat="server" /></b>
                    </td>
                </tr>
                <tr>
                    <td>
                        Imposto (%)<br />
                        <input class="Text" id="txtImposto" type="text" disabled runat="server" />
                    </td>
                    <td>
                        Tx. Operadora (%)<br />
                        <input class="Text" id="txtTxOperadora" type="text" disabled runat="server" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        Custo fixo (%)<br />
                        <input class="Text" id="txtPCustFixo" type="text" disabled runat="server" />
                    </td>
                    <td>
                        Custo variável (%)<br />
                        <input class="Text" id="txtPCustoVariavel" type="text" disabled runat="server" />
                    </td>
                    <td>
                        Perdas (%)<br />
                        <input class="Text" id="txtPPerdas" type="text" disabled runat="server" />
                    </td>
                </tr>
                <tr>                    
                    <td>
                        Custo fixo período (R$)<br />
                        <input class="Text" id="txtCustoFixPeriodo" type="text" disabled runat="server" />
                    </td>
                    <td>
                        Custo var. período (R$)<br />
                        <input class="Text" id="txtCustoVarPeriodo" type="text" disabled runat="server" />
                    </td>
                    <td>
                        Perdas período (R$)<br />
                        <input class="Text" id="txtPerdaPeriodo" type="text" disabled runat="server" />
                    </td>                    
                </tr>
                <tr>
                    <td>
                        Vendas período (R$)<br />
                        <input class="Text" id="txtVendaPeriodo" type="text" disabled runat="server" />
                    </td>
                    <td></td>                    
                    <td>
                        <b style="color: red;">Total custos (%)</b><br />
                        <input class="Text" id="txtTotalCustos" type="text" disabled runat="server" />
                    </td>                    
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:RequiredFieldValidator ID="reqMkp" runat="server" ErrorMessage="Mark-Up" ControlToValidate="txtMKP">*</asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rangeMkp" runat="server" ControlToValidate="txtMKP" ErrorMessage="O Mark-Up do produto deve ser númerico"
                        MaximumValue="9999,99" MinimumValue="0" Type="Double">*</asp:RangeValidator>
                        <b style="color: red;">Mark-Up (%)</b><br />
                        <input type="text" ID="txtMKP" value="0" style="width: 80px" onblur="callCalcularPV();" runat="server" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="reqPreco" runat="server" ErrorMessage="Preço" ControlToValidate="txtPreco">*</asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rangePreco" runat="server" ControlToValidate="txtPreco" ErrorMessage="O preço do produto deve ser númerico"
                            MaximumValue="9999,99" MinimumValue="0" Type="Double">*</asp:RangeValidator>
                        <b style="color: red;">PV (R$)<br />
                            <input type="text" ID="txtPreco" value="0" style="width: 80px" onkeydown="return false;" runat="server" />
                        </b>
                    </td>
                </tr>
            </table>
            <!--  Ativo, Exibir em Destaque, Exibir na Home -->
            <table>
                <tr>
                    <td align="center">
                        <asp:CheckBox Checked="true" ID="chkAtivo" runat="server" Text="Ativo?" />
                        <asp:CheckBox ID="chkDestaque" runat="server" Text="Exibir em Destaque?" />
                        <asp:CheckBox ID="chkExibirHome" runat="server" Text="Exibir na Home?" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <input type="button" value="<< Voltar" onclick="javascript:window.location='CadProdutos.aspx';" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnCadastrar" runat="server" Text="  Cadastrar  " OnClick="btnCadastrar_Click" />
                        <asp:Button ID="btnAtualizar" runat="server" Text="  Atualizar  " OnClick="btnAtualizar_Click"
                            Visible="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

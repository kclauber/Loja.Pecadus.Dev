<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="carrinhoCompras.aspx.cs" Inherits="Loja.UI.Pecadus.carrinhoCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlCarrinho" runat="server" Visible="true">
      <!-- ====================== NAVEGAÇÃO - BREADCRUMB ====================== */ -->
      <nav class="pec-breadcrumb hidden-xs">
        <div class="container">
          <div class="row">
            <div class="col-md-12">
              <nav>
                <ol class="breadcrumb" itemscope itemtype="http://schema.org/BreadcrumbList">
                  <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem"><a href="/SexShop/">Home</a></li>
                  <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">Carrinho</li>
                </ol>
              </nav>
            </div>
          </div>
        </div>
      </nav>

        <!-- ====================== CONTEUDO PRODUTOS INTERNO ====================== */ -->
        <section class="carrinho cont-int">
            <div class="container">

                <div class="row">
                    <div class="col-md-12">
                        <h2><span class="glyphicon glyphicon glyphicon-stop" aria-hidden="true"></span>
                            Carrinho</h2>
                    </div>
                </div>
                <br />

                <div class="row">
                    <div class="col-md-12">
                        <div class="tabela-carrinho">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th colspan="2">Produto</th>
                                        <th class="hidden-xs">Código</th>
                                        <th class="hidden-xs">Preço Unitário</th>
                                        <th class="hidden-xs">Quantidade</th>
                                        <th class="hidden-xs">Total</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th><a href="#" class="btn btn-remove"><i class="glyphicon glyphicon-remove"></i></a></th>
                                        <td>
                                            <img class="img-responsive" src="/images/img-thumb-carrinho.jpg" alt="***Nome do Produto***" title="***Nome do Produto***"></td>
                                        <td>***Nome do Produto***</td>
                                        <td>0123456</td>
                                        <td>R$ 9.999,00</td>
                                        <td>
                                            <div class="form-group">
                                                <label class="sr-only" for="tipoGravacao">Quantidade</label>
                                                <select class="form-control">
                                                    <option>1</option>
                                                    <option>2</option>
                                                    <option>3</option>
                                                </select>
                                            </div>
                                        </td>
                                        <td>R$ 8.888,00</td>
                                    </tr>

                                </tbody>
                            </table>
                            <div class="col-md-12 botoes-carrinho">
                                <a class="btn btn-primary" href="/SexShop/" title="Adicionar mais produtos">Adicionar mais produtos</a>
                                <a class="btn btn-secundary" href="javascript:alert('Estamos redirecionando você para o sistema do PagSeguro.\nPor favor aguarde.');" title="Concluir Compra">Concluir Compra</a>
                            </div>
                            <div class="col-md-12 botoes-carrinho">
                                * Sua compra será processada pelo PagSeguro UOL em ambiente completamente seguro.
                            </div>
                        </div>
                        <!-- /.im-int -->
                    </div>
                </div>
            </div>
        </section>
    </asp:Panel>
    <asp:Panel ID="pnlCarrinhoOld" runat="server" Visible="false">
        <asp:Panel ID="pnlItens" runat="server">
            <asp:ValidationSummary ID="vSummQtd" HeaderText="Por favor, corrija os seguintes problemas:"
                ValidationGroup="quantidade" runat="server" ShowMessageBox="true" ShowSummary="False" />

            <asp:Repeater ID="repCarr" runat="server" OnItemDataBound="repCarr_ItemDataBound">
                <HeaderTemplate>
                    <table align="center" cellpadding="0" cellspacing="0" style="width: 900px; padding: 10px; background: white">
                        <tr>
                            <td width="1%"></td>
                            <td width="1%" align="center">Qtd.
                            </td>
                            <td width="1%"></td>
                            <td width="600" align="left">Produto
                            </td>
                            <td width="1%" align="center" nowrap="nowrap">Val. Unit.
                            </td>
                            <td width="1%" align="center" nowrap="nowrap">Val. Total &nbsp;
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td nowrap="nowrap">&nbsp;
                            <asp:LinkButton ID="btnExcluir" OnClick="RemoverItem" CssClass="EstTabPedidoNome"
                                runat="server">X</asp:LinkButton>
                            &nbsp;
                        </td>
                        <td nowrap="nowrap" align="center" valign="middle">&nbsp;
                            <asp:TextBox ID="txtQtd" MaxLength="3" Width="20" runat="server">1</asp:TextBox>
                            <asp:RangeValidator Type="Integer" ID="rngQtd" ControlToValidate="txtQtd" runat="server"
                                MinimumValue="1" MaximumValue="10" ValidationGroup="quantidade">*</asp:RangeValidator>
                        </td>
                        <td valign="middle" align="center">
                            <asp:HyperLink ID="lnkImgProd" runat="server" CssClass="imgProd">
                                <asp:Image ID="imgProd" ImageAlign="Left" Width="60" Height="60" BorderWidth="0"
                                    runat="server" ImageUrl="~/imagensProdutos/sem_imagem.gif" />
                            </asp:HyperLink>
                        </td>
                        <td valign="middle" align="left">&nbsp;
                            <asp:Label ID="lblRef" runat="server"></asp:Label>
                            <asp:HyperLink ID="lnkTitulo" CssClass="EstTabPedidoNome" runat="server">XXX</asp:HyperLink>
                        </td>
                        <td align="center" valign="middle" nowrap="nowrap">
                            <asp:Label ID="lblVlUnit" runat="server" />
                        </td>
                        <td align="center" valign="middle" nowrap="nowrap">
                            <asp:Label ID="lblVlTotProd" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <hr class="hr" style="width: 96%; color: #999" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="4" align="left">&nbsp;
                        </td>
                        <td align="right" colspan="2" style="padding-right: 5px;" nowrap="nowrap">Sub-total do pedido:&nbsp;
                            <asp:Label ID="lblSubTotalCompra" runat="server" />
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        <asp:Panel ID="pnlFrete" runat="server">
            <table align="center" cellpadding="0" cellspacing="0" style="width: 900px; background: white;">
                <tr>
                    <td align="right" style="vertical-align: top; padding-top: 2px;" nowrap="nowrap">&nbsp;Calcule meu frete:
                    </td>
                    <td style="padding-left: 20px;">
                        <asp:ValidationSummary ID="vsumFrete" HeaderText="Por favor, corrija os seguintes problemas:"
                            ValidationGroup="frete" runat="server" ShowMessageBox="true" ShowSummary="False" />
                        <asp:RequiredFieldValidator ID="reqCep1" runat="server" ErrorMessage="Informe os 5 primeiros digitos de seu CEP"
                            ControlToValidate="txtCep1" Display="None" ValidationGroup="frete" />
                        <asp:RequiredFieldValidator ID="reqCep2" runat="server" ErrorMessage="Informe os 3 últimos digitos de seu CEP"
                            ControlToValidate="txtCep2" Display="None" ValidationGroup="frete" />
                        CEP:
                        <asp:TextBox ID="txtCep1" MaxLength="5" runat="server" Width="80px"></asp:TextBox>
                        <asp:TextBox ID="txtCep2" MaxLength="3" runat="server" Width="50px"></asp:TextBox>
                        <asp:Button ID="btnCalcularFrete" runat="server" Text="Calcular" OnClick="btnCalcularFrete_Click"
                            ValidationGroup="frete" /><br />
                        &nbsp;&nbsp;&nbsp;&nbsp; ><a href="http://www.buscacep.correios.com.br/servicos/dnec/index.do"
                            target="_blank"> <u>Não sei meu CEP</u></a>
                    </td>
                    <td width="40%" valign="top">
                        <asp:RadioButtonList ID="rdlFrete" RepeatDirection="Horizontal" runat="server" Visible="false"
                            OnSelectedIndexChanged="rdlFrete_SelectedIndexChanged" AutoPostBack="true">
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr class="hr" style="width: 96%; color: #999" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlValorTotal" runat="server">
            <table align="center" cellpadding="0" cellspacing="0" style="width: 900px; padding: 10px; background: white">
                <tr class="rodape">
                    <td align="right" style="padding-right: 5px;" nowrap="nowrap">Valor total do pedido:&nbsp;
                        <asp:Label ID="lblTotalCompra" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlBotoes" runat="server" Visible="true">
            <table align="center" style="width: 900px; padding: 5px; background: white">
                <tr>
                    <td align="left">&nbsp;&nbsp; &nbsp;&nbsp;
                        <asp:ImageButton ID="imgContinuarCompra1" runat="server" ImageUrl="~/imagens/Voltar.gif"
                            OnClick="imgContinuarCompra_Click" />
                        <asp:ImageButton ID="imgAtualizaQuantidade1" runat="server" ImageUrl="~/imagens/Atualizar.gif"
                            OnClick="imgAtualizaQuantidade_Click" ValidationGroup="quantidade" />
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="imgFinalizar1" runat="server" ImageUrl="~/imagens/Finalizar.gif"
                            OnClick="imgFinalizar_Click" ValidationGroup="frete" />
                        &nbsp;&nbsp; &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnlVazio" Visible="false" runat="server">
        <table align="center" style="width: 700px; height: 250px; background: white;">
            <tr>
                <td align="center">
                    <h3 class="tituloDesc" style="font-size: 18px; text-align: center;">Seu <u>Carrinho de Compras</u> está vazio.</h3>

                    <p>
                        Para inserir algum produto no seu carrinho, navegue pelas categorias ou utilize a busca do site.<br />
                        Ao encontrar os itens desejados, clique no botão COMPRAR localizado na página do produto.
                    </p>

                    Está tendo problemas com o carrinho de compras? Entre em <a href="/Contato"><u>contato</u></a> conosco.
                    <br />
                    <br />
                    <a href="<%=ConfigurationManager.AppSettings["home"]%>">
                        <img id="imgContinuarComprando" src="~/imagens/Voltar.gif" border="0" alt="Continuar comprando"
                            runat="server" /></a>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

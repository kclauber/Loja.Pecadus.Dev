<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="carrinhoCompras.aspx.cs" Inherits="Loja.UI.Pecadus.carrinhoCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
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
                        <asp:Repeater ID="repCarrinho" runat="server" OnItemDataBound="repCarr_ItemDataBound">
                            <HeaderTemplate>
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
                            </HeaderTemplate>
                            <ItemTemplate>
                                    <tr>
                                        <th>
                                            <asp:LinkButton ID="lnkExcluir" OnClick="RemoverItem" CssClass="btn btn-remove" runat="server">
                                                <i class="glyphicon glyphicon-remove"></i>
                                            </asp:LinkButton>
                                        </th>
                                        <td>
                                            <asp:Image ID="imgProduto" ImageUrl="/imagensProdutos/sem-foto-2.jpg" CssClass="img-responsive" runat="server" />
                                        </td>
                                        <td><asp:Label ID="lblNomeProduto" runat="server"/></td>
                                        <td><asp:Label ID="lblCodProduto" runat="server"/></td>
                                        <td><asp:Label ID="lblPrecoProduto" runat="server"/></td>
                                        <td>
                                            <div class="form-group">
                                                <label class="sr-only" for="tipoGravacao">Quantidade</label>
                                                <asp:DropDownList ID="ddlQuantidade" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidade_SelectedIndexChanged" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td><asp:Label ID="lblPrecoTotalProduto" runat="server"/></td>
                                    </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                    <tr>
                                        <th></th>
                                        <td class="hidden-xs" colspan="6" align="right">
                                            Frete: 
                                            <asp:Label ID="lblPrecoFrete" runat="server"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th></th>
                                        <td class="hidden-xs" colspan="6" align="right">
                                            Valore total: 
                                            <asp:Label ID="lblPrecoTotalCompra" runat="server"/>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="col-md-12 botoes-carrinho">
                                <a class="btn btn-primary" href="/SexShop/" title="Adicionar mais produtos">Adicionar mais produtos</a>
                                <a class="btn btn-secundary" href="javascript:alert('Estamos redirecionando você para o sistema do PagSeguro.\nPor favor aguarde.');" title="Concluir Compra">Concluir Compra</a>
                            </div>
                            </FooterTemplate>
                        </asp:Repeater>
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
    </form>
</asp:Content>

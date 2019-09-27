<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="categorias.aspx.cs" Inherits="Loja.UI.Pecadus.categorias" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<form runat="server">

  <!-- ====================== NAVEGAÇÃO - BREADCRUMB ====================== */ -->
  <nav class="pec-breadcrumb">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <nav>
            <ol class="breadcrumb" itemscope itemtype="http://schema.org/BreadcrumbList">
              <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem"><a href="#">Home</a></li>
              <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">Nome da Categoria</li>
            </ol>
          </nav>
        </div>
      </div>
    </div>
  </nav>



  <!-- ====================== PRODUTOS ====================== */ -->
  <section class="produtos-destaque vitrine-protudos">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
            <img class="img-responsive banner-categoria hidden-xs" 
            src="/images/banners/img-banner-categoria.jpg" 
                alt="Frete Grátis">
        </div>
      </div>

      <div class="row">
            
            <asp:Repeater ID="repProdutoDestaque1" runat="server" OnItemDataBound="repProd1_ItemDataBound">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>

                        <div class="col-md-3 col-sm-4 col-xs-12">
                          <div class="box-produtos">
                            <asp:HyperLink ID="lnkImgProd" runat="server">
                                <asp:Image ID="imgProd" Width="270" Height="270" runat="server" 
                                    ImageUrl="/imagensProdutos/sem-foto-2.jpg" 
                                    CssClass="thumbnail img-responsive" />
                            </asp:HyperLink>
                            <h3>
                              <asp:HyperLink ID="lnkTitulo" runat="server" />
                            </h3>
                            <p>
                              <asp:Label ID="lblDescricao" runat="server" />
                            </p>
                            <!--
                            <p>
                                <span class="glyphicon glyphicon-star star-active" aria-hidden="true"></span>
                                <span class="glyphicon glyphicon-star star-active" aria-hidden="true"></span>
                                <span class="glyphicon glyphicon-star star-active" aria-hidden="true"></span>
                                <span class="glyphicon glyphicon-star" aria-hidden="true"></span>
                                <span class="glyphicon glyphicon-star" aria-hidden="true"></span>
                                <a href="" title="Avaliações">(01 Avaliação)</a>
                            </p>
                            -->
                            <asp:HyperLink ID="lnkPreco" runat="server" />
                            <asp:HyperLink ID="lnkDetalhes" runat="server" CssClass="btn btn-primary">
                              <span class="glyphicon glyphicon-search" aria-hidden="true"></span> Detalhes
                            </asp:HyperLink>
                            <div class="icon-posi" data-toggle="buttons">
                              <label class="btn btn-invisible">
                                <input type="checkbox" id="chkFavoritos" runat="server" />
                                <span class="glyphicon glyphicon-heart" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Adicionar aos Favoritos"></span>
                              </label>
                            </div>
                            <p>
                              <asp:LinkButton OnClick="AdicionarItem" ID="lnkComprar" CssClass="btn btn-secundary" runat="server">
                                <span class="glyphicon glyphicon-shopping-cart" aria-hidden="true"></span> Adicionar Carrinho
                              </asp:LinkButton>
                            </p>
                          </div>
                        </div>                        

                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
            
      </div><!-- Final Row-->
      <div class="row">
          <nav aria-label="Page navigation">
          <ul class="pagination">
              <li>
              <a href="#" aria-label="Previous">
                  <span aria-hidden="true">&laquo;</span>
              </a>
              </li>
              <li><a href="#">1</a></li>
              <li><a href="#">2</a></li>
              <li><a href="#">3</a></li>
              <li><a href="#">4</a></li>
              <li><a href="#">5</a></li>
              <li>
              <a href="#" aria-label="Next">
                  <span aria-hidden="true">&raquo;</span>
              </a>
              </li>
          </ul>
          </nav>
      </div>

    </div>
  </section>

</form>
</asp:Content>
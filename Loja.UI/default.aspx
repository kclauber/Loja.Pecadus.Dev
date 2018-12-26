<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="Loja.UI.Pecadus.Default" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">

  <!-- ====================== SLIDER CAROUSEL ====================== */ 
  <section class="slider">
    <div class="container">
      <div class="row">
        <div id="slider-pecadus" class="carousel slide" data-ride="carousel">
          <!-- Indicators -->
          <ol class="carousel-indicators">
            <li data-target="#slider-pecadus" data-slide-to="0" class="active"></li>
            <li data-target="#slider-pecadus" data-slide-to="1"></li>
            <li data-target="#slider-pecadus" data-slide-to="2"></li>
          </ol>

          <!-- Wrapper for slides -- >
          <div class="carousel-inner" role="listbox">

            <!--Repetir este elemento para adicionar mais imagens-- >
            <div class="item active">
              <img class="hidden-xs" src="images/slider/banner-01.jpg" alt="Banner 01">
              <img class="visible-xs" src="images/slider/banner-01-mob.jpg" alt="Banner 01">
              <div class="carousel-caption">
              </div>
            </div>
            <div class="item">
              <img class="hidden-xs" src="images/slider/banner-01.jpg" alt="Banner 02">
              <img class="visible-xs" src="images/slider/banner-01-mob.jpg" alt="Banner 02">
              <div class="carousel-caption">
              </div>
            </div>

          </div>

          <!-- Controls -- >
          <a class="left carousel-control" href="#slider-pecadus" role="button" data-slide="prev">
            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
          </a>
          <a class="right carousel-control" href="#slider-pecadus" role="button" data-slide="next">
            <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
          </a>
        </div>
      </div>
    </div>
  </section>
  -->


  <!-- ====================== CHAMADA BANNERS ====================== */ -->
  <!--
  <section class="banners hidden-xs">
    <div class="container">
      <div class="row">
        <div class="col-md-4 col-sm-4">
          <a href="#" title="Brinquedos e Acessórios para Elas">
            <img class="img-responsive" src="images/banners/para-elas.jpg" alt="Brinquedos e Acessórios para Elas">
          </a>
        </div>
        <div class="col-md-4 col-sm-4">
          <a href="#" title="Brinquedos e Acessórios para Eles">
            <img class="img-responsive" src="images/banners/para-eles.jpg" alt="Brinquedos e Acessórios para Eles">
          </a>
        </div>
        <div class="col-md-4 col-sm-4">
          <a href="#" title="Brinquedos e Acessórios para Nós">
            <img class="img-responsive" src="images/banners/para-nos.jpg" alt="Brinquedos e Acessórios para Nós">
          </a>
        </div>
      </div>
    </div>
  </section>
  -->



  <!-- ====================== SLIDER PRODUTOS ====================== */ -->
  <section class="slider-produtos vitrine-protudos">
    <div class="container">

      <div class="row">
        <div class="col-md-12">
            <h2><span class="glyphicon glyphicon glyphicon-stop" aria-hidden="true"></span>Os Mais Vendidos</h2>
        </div>
      </div>

      <div class="customNavigation visible-xs">
        <a class="btn prev"><span class="glyphicon glyphicon-menu-left" aria-hidden="true"></span></a>
        <a class="btn next"><span class="glyphicon glyphicon-menu-right" aria-hidden="true"></span></a>
      </div>

      <div id="owl-demo" class="owl-carousel owl-theme">


            <asp:Repeater ID="repProdutoDestaque1" runat="server" OnItemDataBound="repProd1_ItemDataBound">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>

                    <div class="item">
                      <div class="row">
                        <div class="col-md-12">
                            <asp:HyperLink ID="lnkImgProd" runat="server">
                                <asp:Image ID="imgProd" Width="270" Height="270" runat="server" 
                                    ImageUrl="~/imagensProdutos/sem-foto-2.jpg" 
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
                    </div>

                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>

        
      </div> <!-- FINAL OWL-DEMO -->
    </div>
  </section>

  <!-- ====================== BANNER CENTRO SITE ====================== */ -->
  <section class="banners-2">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <!--<a href="#" title="#">-->
            <img class="img-responsive hidden-xs" src="images/banners/frete-gratis.jpg" alt="Frete Grátis">
            <img class="img-responsive visible-xs" src="images/banners/frete-gratis-mob.jpg" alt="Frete Grátis">
          <!--</a>-->
        </div>
      </div>
    </div>
  </section>

  <!-- ====================== PRODUTOS ====================== */ -->
  <section class="produtos-destaque vitrine-protudos">
    <div class="container">

      <div class="row">
        <div class="col-md-12">
            <h2><span class="glyphicon glyphicon glyphicon-stop" aria-hidden="true"></span>Produtos Destaque</h2>
        </div>
      </div>

      <div class="row">
            
            <asp:Repeater ID="repProdutoDestaque2" runat="server" OnItemDataBound="repProd2_ItemDataBound">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>

                        <div class="col-md-3 col-sm-4 col-xs-12">
                          <div class="box-produtos">
                            <asp:HyperLink ID="lnkImgProd" runat="server">
                                <asp:Image ID="imgProd" Width="270" Height="270" runat="server" 
                                    ImageUrl="~/imagensProdutos/sem-foto-2.jpg" 
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
    </div>
  </section>


  <!-- ====================== CONTROLE SLIDER PRODUTOS ====================== */ -->
   <script type="text/javascript">
    $(document).ready(function() {
      var owl = $("#owl-demo");

      owl.owlCarousel({
          items : 4, //10 items above 1000px browser width
          itemsDesktop : [1000,4], //5 items between 1000px and 901px
          itemsDesktopSmall : [900,3], // betweem 900px and 601px
          itemsTablet: [599,2], //2 items between 600 and 0
          itemsMobile : [479,1], // itemsMobile disabled - inherit from itemsTablet option
      });

      // Custom Navigation Events
      $(".next").click(function(){
        owl.trigger('owl.next');
      })
      $(".prev").click(function(){
        owl.trigger('owl.prev');
      })
      $(".play").click(function(){
        owl.trigger('owl.play',1000);
      })
      $(".stop").click(function(){
        owl.trigger('owl.stop');
      })
    });
   </script>

    </form>
</asp:Content>
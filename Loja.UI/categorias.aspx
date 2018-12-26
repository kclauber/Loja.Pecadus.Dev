<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="categorias.aspx.cs" Inherits="Loja.UI.Pecadus.categorias" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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

<!-- ====================== CONTEUDO ====================== */ -->
<section class="categorias cont-int">
  <div class="container">
    <div class="row">

      <div class="col-md-12">
          <img class="img-responsive banner-categoria hidden-xs" src="images/banners/img-banner-categoria.jpg" alt="Frete Grátis">
      </div>

      <div class="col-sm-12">
        <section class="produtos-destaque vitrine-protudos">
            <div class="row">

              <!-- ITEM 01 -->
              <div class="col-md-3 col-sm-4 col-xs-12">
                <div class="box-produtos">
                  <img class="thumbnail img-responsive" src="images/produtos/sem-foto.jpg" alt="Kit Fantasia Presidiária Susan Sapeka">
                  <h3>
                    <a href="" title="">Lorem ipsum dolor sit amet, consectetur adipiscing elit</a>
                  </h3>
                  <p>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam metus massa, facilisis vel volutpat ut, tempor et nisi.
                  </p>
                  <p>
                      <span class="glyphicon glyphicon-star star-active" aria-hidden="true"></span>
                      <span class="glyphicon glyphicon-star star-active" aria-hidden="true"></span>
                      <span class="glyphicon glyphicon-star star-active" aria-hidden="true"></span>
                      <span class="glyphicon glyphicon-star" aria-hidden="true"></span>
                      <span class="glyphicon glyphicon-star" aria-hidden="true"></span>
                      <a href="" title="Avaliações">(01 Avaliação)</a>
                  </p>
                  <p>De: <span>199,00</span></p>
                  <p>Por:</p>
                  <ul>
                    <li>R$ 55,00</li>
                  </ul>
                    <a href="" title="Detalhes" class="btn btn-primary" aria-label="Left Align" role="button">
                      <span class="glyphicon glyphicon-search" aria-hidden="true"></span> Detalhes
                    </a>
                    <div class="icon-posi" data-toggle="buttons">
                      <label class="btn btn-invisible">
                        <input type="checkbox">
                        <span class="glyphicon glyphicon-heart" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Adicionar aos Favoritos"></span>
                      </label>
                    </div>
                  <p>
                    <a href="" title="Adicionar Carrinho" class="btn btn-secundary" aria-label="Left Align" role="button">
                      <span class="glyphicon glyphicon-shopping-cart" aria-hidden="true"></span> Adicionar Carrinho
                    </a>
                  </p>
                </div>
              </div>

              
            </div><!-- Final Row Produtos-->

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

        </section>


      </div>


    </div>
  </div>
</section>


</asp:Content>
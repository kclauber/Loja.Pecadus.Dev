<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="produto.aspx.cs" Inherits="Loja.UI.Pecadus.produtoDetalhe" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <link rel="stylesheet" type="text/css" href="/css/lightbox.css" />
    <script type="text/javascript" language="javascript" src="/scripts/magicZoom.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
  <!-- ====================== NAVEGAÇÃO - BREADCRUMB ====================== */ -->
  <nav class="pec-breadcrumb hidden-xs">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <nav>
            <ol class="breadcrumb" itemscope itemtype="http://schema.org/BreadcrumbList">
              <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem"><a href="/SexShop/">Home</a></li>
              <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem"><a href="#">Nome da Categoria</a></li>
              <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">Nome do produto</li>
            </ol>
          </nav>
        </div>
      </div>
    </div>
  </nav>

<!-- ====================== CONTEUDO PRODUTOS INTERNO ====================== */ -->
<section class="produtos-interno cont-int">
  <div class="container">
    <div class="row">

      <div class="col-md-12">
          <div class="produto-titul">
              <h2><span class="glyphicon glyphicon glyphicon-stop" aria-hidden="true"></span>
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit -sum dolor sit amet, consectetur adipiscing elit</h2>
              <ul>
                <li><span class="glyphicon glyphicon-star star-active" aria-hidden="true"></span>
                <span class="glyphicon glyphicon-star star-active" aria-hidden="true"></span>
                <span class="glyphicon glyphicon-star star-active" aria-hidden="true"></span>
                <span class="glyphicon glyphicon-star" aria-hidden="true"></span>
                <span class="glyphicon glyphicon-star" aria-hidden="true"></span></li>
                <li><a href="" title="Avaliações">(01 Avaliação)</a></li>
                <li> Cód. Ref.: 0123456</li>
                <li></li>
              </ul>
          </div>
      </div>

      <div class="col-md-6 col-sm-6">
        <div id="pec-carousel-produto-interno" class="carousel slide" data-ride="carousel">
          <div class="row">
          <!-- Wrapper for slides -->
            <div class="carousel-inner" role="listbox">
              <div class="item active thumbnail">
                <a href="/images/produtos/produto-01.jpg" title="***Nome do Produto***" data-lightbox="interna">
                <img class="img-responsive" src="/images/produtos/produto-01-thumb.jpg" alt="***Nome do Produto***" title="***Nome do Produto***"></a>
              </div>
              <div class="item thumbnail">
                <a href="/images/produtos/produto-02.jpg" title="***Nome do Produto***" data-lightbox="interna">
                <img class="img-responsive" src="/images/produtos/produto-02-thumb.jpg" alt="***Nome do Produto***" title="***Nome do Produto***"></a>
              </div>
              <div class="item thumbnail">
                <a href="/images/produtos/produto-03.jpg" title="***Nome do Produto***" data-lightbox="interna">
                <img class="img-responsive" src="/images/produtos/produto-03-thumb.jpg" alt="***Nome do Produto***" title="***Nome do Produto***"></a>
              </div>
            </div>

            <!-- Indicators -->
            <ol class="carousel-indicators">
              <li data-target="#pec-carousel-produto-interno" data-slide-to="0" class="active">
                <img class="img-responsive" src="/images/produtos/produto-01-thumb.jpg" title="***Nome do Produto***" alt="***Nome do Produto***">
              </li>
              <li data-target="#pec-carousel-produto-interno" data-slide-to="1">
                <img class="img-responsive" src="/images/produtos/produto-02-thumb.jpg" title="***Nome do Produto***" alt="***Nome do Produto***">
              </li>
              <li data-target="#pec-carousel-produto-interno" data-slide-to="2">
                <img class="img-responsive" src="/images/produtos/produto-03-thumb.jpg" title="***Nome do Produto***" alt="***Nome do Produto***">
              </li>
            </ol>
          </div>
        </div>
      </div>

      <div class="col-md-6 col-sm-6">
        <div class="produto-preco">
            <p>De: <span>199,00</span></p>
            <p>Por:</p>
            <ul>
              <li>R$ 55,00</li>
            </ul>
            <p><strong>Compre agora no cartão de crédito e parcele em até 18x*</strong></p>

            <a href="/Carrinho/id" title="Comprar" class="btn btn-primary" aria-label="Left Align" role="button"><span class="glyphicon glyphicon-shopping-cart"></span> Comprar</a>

            <img src="/images/img-compra-protegida2.png" alt="Compra Protegida">

            <div class="icon-posi" data-toggle="buttons">
              <label class="btn btn-invisible">
                <input type="checkbox">
                <span class="glyphicon glyphicon-heart" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Adicionar aos Favoritos"></span>
              </label>
            </div>

            
            <p class="txt-produto">
              * Sujeito a aprovação. Parcela mínima R$ 5,00.<br>
              Ao clicar em comprar declaro ter lido e aceito os <a href="#" title="termos de compra"><strong>termos de compra</strong></a> deste site.
            </p>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam metus massa, facilisis vel volutpat ut, tempor et nisi.
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam metus massa, facilisis vel volutpat ut, tempor et nisi.
            </p>
            <!-- 16:9 aspect ratio -->
            <div class="row">
              <div class="col-md-8">
                <div class="embed-responsive embed-responsive-16by9">
                  <iframe class="embed-responsive-item" src="//www.youtube.com/embed/zpOULjyy-n8?rel=0" allowfullscreen=""></iframe>
                </div>
              </div>
            </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-md-12 descricao-produto">
        <h3>Detalhes do Produto</h3>
        <p>Coelho de pernas para o AR, com compartimento secreto, acompanha um vibrador personal 13 cm na cor rosa e cadeado de segurança.</p>
        <p>Este COELHO em pelúcia antialérgica, traz dentro do seu compartimento secreto uma outra surpresa: um vibrador personal rosa de 13cm. Tudo fechado com cadeado de segurança.</p>
        <p>Os brinquedos em pelúcia são ideais para presentes e decoração sensual . Possui um compartimento interno para guardar os acessórios eróticos de forma discreta e segura.</p>
        <ul>
          <li><strong>Tamanho:</strong> Aproximadamente 45cm</li>
          <li><strong>Cor:</strong> Branco e Rosa</li>
          <li><strong>Material:</strong> Pelúcia anti-alérgica </li>
          <li><strong>Embalagem:</strong> Saco plástico com solapa</li>
          <li><strong>Acompanha:</strong> 1 Cadeado com chave e 1 vibrador personal</li>
        </ul>
      </div>
    </div>
    <div class="row">
      <div class="col-md-12">
        <h3>Palavras Relacionadas</h3>
        <a href="#">Coelho de Pelúcia</a>, <a href="#">Coelho de Pelúcia</a>, <a href="#">Coelho de Pelúcia</a>, <a href="#">Coelho de Pelúcia</a>, <a href="#">Coelho de Pelúcia</a>, <a href="#">Coelho de Pelúcia</a>
      </div>
    </div>
  </div>
</section>


</asp:Content>

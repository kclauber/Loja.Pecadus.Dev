﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
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
              <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">
                  <asp:HyperLink ID="lnkMigalhaHome" runat="server" Font-Bold="true" />
              </li>
              <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">
                  <asp:HyperLink ID="lnkMigalhaCateg" runat="server" Font-Bold="true" />
              </li>
              <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">
                  <asp:HyperLink ID="lnkMigalhaSubCateg" runat="server" Font-Bold="true" Visible="true" />
              </li>
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
                  <asp:Label ID="lblTitulo" runat="server" />
              </h2>
              <ul>
                <!--
                    <li>
                        <span class="glyphicon glyphicon-star star-active" aria-hidden="true"></span>
                        <span class="glyphicon glyphicon-star star-active" aria-hidden="true"></span>
                        <span class="glyphicon glyphicon-star star-active" aria-hidden="true"></span>
                        <span class="glyphicon glyphicon-star" aria-hidden="true"></span>
                        <span class="glyphicon glyphicon-star" aria-hidden="true"></span>
                    </li>
                    <li><a href="#" title="Avaliações">(01 Avaliação)</a></li>
                -->
                <li><asp:Label ID="lblCodigo" runat="server" /></li>
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
              <asp:Label ID="lblDescCurta" runat="server" />
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
        <asp:Label ID="lblDescCompleta" runat="server" />
      </div>
    </div>
    <div class="row">
      <div class="col-md-12">
        <h3>Palavras Relacionadas</h3>
        <asp:Label ID="lblPalavrasChave" runat="server" />
      </div>
    </div>
  </div>
</section>


</asp:Content>

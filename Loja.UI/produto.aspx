<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="produto.aspx.cs" Inherits="Loja.UI.Pecadus.produtoDetalhe" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <link rel="stylesheet" type="text/css" href="/css/lightbox.css" />
    <script type="text/javascript" language="javascript" src="/scripts/magicZoom.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">

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
                    TODO: criar sistema para exibir as estrelas de opinião
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
            <asp:Repeater ID="repImages" OnItemDataBound="repImages_ItemDataBound" runat="server">
                <HeaderTemplate>
                    <div class="carousel-inner" role="listbox">
                </HeaderTemplate>
                <ItemTemplate>
                    <div id="divFotoProduto" class="item thumbnail" runat="server">
                        <asp:Image ID="imgFotoProduto" CssClass="img-responsive" runat="server" />
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>

            <!-- Indicators -->
            <asp:Repeater ID="repThumbs" OnItemDataBound="repThumbs_ItemDataBound" runat="server">
                <HeaderTemplate>
                    <ol id="olThumb" class="carousel-indicators">
                </HeaderTemplate>
                <ItemTemplate>
                    <li id="liThumb" data-target="#pec-carousel-produto-interno" runat="server">
                        <asp:ImageButton ID="imgThumb" CssClass="img-responsive" runat="server" />
                    </li>                    
                </ItemTemplate>
                <FooterTemplate>
                    </ol>
                </FooterTemplate>
            </asp:Repeater>

          </div>
        </div>
      </div>

      <div class="col-md-6 col-sm-6">
        <div class="produto-preco">
            <asp:Label ID="lblPreco" runat="server" />
            <asp:Label ID="lblEstoque" runat="server" />            

            <asp:LinkButton OnClick="AdicionarItem" ID="lnkComprar" CssClass="btn btn-primary" runat="server">
                <span class="glyphicon glyphicon-shopping-cart"></span> Comprar
            </asp:LinkButton>
            
            <span id="lblDetalhes" class="btn btn-primary" style="background-color: #869791; cursor:default;" visible="false" runat="server">
                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span> Produto indisponível</span>

            <img src="/images/img-compra-protegida2.png" alt="Compra Protegida">

            <div class="icon-posi" data-toggle="buttons">
              <label class="btn btn-invisible">
                <input type="checkbox" id="chkFavoritos" runat="server" />
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
            <asp:panel ID="pnlVideoProduto" Visible="false" runat="server">
                <!-- 16:9 aspect ratio -->
                <div class="row">
                  <div class="col-md-8">
                    <div class="embed-responsive embed-responsive-16by9">
                      <iframe class="embed-responsive-item" src="<%=Session["video"] %>" allowfullscreen=""></iframe>
                    </div>
                  </div>
                </div>
            </asp:panel>
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


    <script type="text/javascript">
        $(document).ready(function () {
            //Adiciona a classe 'active' aos primeiros elementos de exibir as imagens do produto
            $("#ContentPlaceHolder1_repImages_divFotoProduto_0, #ContentPlaceHolder1_repThumbs_liThumb_0").addClass("active");

            //Adiciona o atributo para colocar a troca de imagens
            var index = 0;
            $("#olThumb").find('li').each(function () {
                var item = $(this);
                item.attr("data-slide-to", index);
                index++;
            });
        });
    </script>

    </form>
</asp:Content>

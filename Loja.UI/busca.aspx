<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="busca.aspx.cs" Inherits="Loja.UI.Pecadus.busca" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlBusca" runat="server">
      <!-- ====================== NAVEGAÇÃO - BREADCRUMB ====================== */ -->
      <nav class="pec-breadcrumb hidden-xs">
        <div class="container">
          <div class="row">
            <div class="col-md-12">
              <nav>
                <ol class="breadcrumb" itemscope itemtype="http://schema.org/BreadcrumbList">
                  <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem"><a href="#">Home</a></li>
                  <li itemprop="itemListElement" itemscope itemtype="http://schema.org/ListItem">Busca</li>
                </ol>
              </nav>
            </div>
          </div>
        </div>
      </nav>

        <section class="cadastro cont-int">
          <div class="container">

            <div class="row">
                <div class="col-md-12">
                    <h2><span class="glyphicon glyphicon glyphicon-stop" aria-hidden="true"></span>
                        Busca</h2>
                    <p style="color: #233241;">Caso ainda não tenha encontrado o produto certo, tente novamente com palavras similares ou termos genéricos.</p>                 
                    <br />
                </div>
                
              <div class="col-sm-12">
                <section class="produtos-destaque vitrine-protudos">
                    <div class="row">

                        <div class="col-md-3 col-sm-4 col-xs-12">
                          <div class="box-produtos">
                            <a href="/SexShop/categoriaPaiTitulo/categoriaTitulo/titulo/categoriaPaiID/categoriaID/ID/" title="">
                                    <img class="thumbnail img-responsive" src="/images/produtos/sem-foto-2.jpg" alt="Kit Fantasia Presidiária Susan Sapeka"></a>
                            <h3>
                              <a href="/SexShop/categoriaPaiTitulo/categoriaTitulo/titulo/categoriaPaiID/categoriaID/ID/" title="">Lorem ipsum dolor sit amet, consectetur adipiscing elit</a>
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
                              <a href="/SexShop/categoriaPaiTitulo/categoriaTitulo/titulo/categoriaPaiID/categoriaID/ID/" title="Detalhes" class="btn btn-primary" aria-label="Left Align" role="button">
                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span> Detalhes
                              </a>
                              <div class="icon-posi" data-toggle="buttons">
                                <label class="btn btn-invisible">
                                  <input type="checkbox">
                                  <span class="glyphicon glyphicon-heart" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Adicionar aos Favoritos"></span>
                                </label>
                              </div>
                            <p>
                              <a href="/Carrinho/id" title="Adicionar Carrinho" class="btn btn-secundary" aria-label="Left Align" role="button">
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

    </asp:Panel>
    <asp:Panel ID="pnlBuscaOld" Visible="false" runat="server">
        <asp:Panel ID="pnlItens" runat="server">
            <table align="center" style="width: 940px; height: 50px; background: white;">
                <tr>
                    <td align="center">
                        <h3 class="tituloDesc" style="font-size: 18px; text-align: center;">
                            <asp:Label ID="lblResultado" runat="server" /></h3>
                        <p>Caso ainda não tenha encontrado o produto certo, tente novamente com palavras similares ou termos genéricos.</p>
                    </td>
                </tr>
            </table>
            <br />
            <asp:DataList ID="dtlProdutosBusca" RepeatDirection="Horizontal" HorizontalAlign="Center"
                runat="server" RepeatColumns="4" ShowFooter="False" ShowHeader="False" OnItemDataBound="dtlProd_ItemDataBound">
                <ItemTemplate>
                    <table class="prodItem">
                        <tr>
                            <td align="center">
                                <asp:HyperLink ID="lnkImgProd" runat="server" CssClass="imgProd">
                                    <asp:Image ID="imgProd" Width="180" Height="180" runat="server" ImageUrl="~/imagensProdutos/sem_imagem.gif" />
                                </asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="tdDesc">
                                <asp:HyperLink ID="lnkDescricao" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="tdPreco">
                                <asp:HyperLink ID="lnkPreco" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <ItemStyle VerticalAlign="Top" />
            </asp:DataList>            
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnlVazio" Visible="false" runat="server">
        <table align="center" style="width: 700px; height: 250px; background: white;">
            <tr>
                <td align="center">
                    <h3 class="tituloDesc" style="font-size: 18px; text-align: center;">
                        Sua busca com a(s) palavra(s) <u>
                            <asp:Label ID="lblBusca" runat="server" /></u>, não retornou dados.</h3>
                    <p>
                        Por favor, verifique se as palavras estão escritas corretamente.<br />
                        Caso ainda não encontre nada, tente novamente com palavras similares ou termos genéricos.<br />
                        Por exemplo: "prótese peniana", "bolinhas de sexo", "gel de massagem".</p>
                    Está tendo problemas com a busca? Entre em <a href="/Contato"><u>contato</u></a>
                    conosco.
                    <br />
                    <br />
                    <a href="<%=ConfigurationManager.AppSettings["home"]%>">
                        <img id="imgContinuarComprando" src="~/imagens/Voltar.gif" border="0" alt="Voltar para a página inicial"
                            runat="server" /></a>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

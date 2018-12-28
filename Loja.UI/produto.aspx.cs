using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;
using System;
using System.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja.UI.Pecadus
{
    public partial class produtoDetalhe : System.Web.UI.Page
    {
        private int id = -1;
        private int idCategoria = -1;
        ProdutoOT produto = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Código de sessão do usuário
            if (Session["idUser"] == null)
                Session["idUser"] = DateTime.Now.ToString("yyMMddhhmmss");
            /////////////////////////////

            if (!Page.IsPostBack)
            {
                //Session["video"] = null;

                if (Request.QueryString.Count > 0)
                {
                    id = (Request["ID"] != null ? Convert.ToInt32(Request["ID"]) : -1);
                    idCategoria = (Request["categID"] != null ? Convert.ToInt32(Request["categID"]) : -1);
                }
                else if (RouteData.Values.Count > 0)
                {
                    id = (RouteData.Values["ID"] != null ? Convert.ToInt32(RouteData.Values["ID"]) : -1);
                    idCategoria = (RouteData.Values["categID"] != null ? Convert.ToInt32(RouteData.Values["categID"]) : -1);
                }

                if (id > -1)
                {
                    carregaDetalhesProduto(id);
                    carregaMetaTags();
                    //carregaMigalha();
                    carregarPalavrasChave();
                }
            }
        }

        protected void carregaDetalhesProduto(int id)
        {
            //produto = new ProdutosOP().SelectProduto(id, 0, 1);
            produto = Utilitarios.CarregaProdutoFake();

            if (produto == null)
                throw new Exception(String.Format("Produto não encontrado! ID: {0}", id));

            //Detalhes do produto
            string link = Utilitarios.CriaStringLinkProduto(produto.Categoria.TituloCategoriaPai,
                                                            produto.Categoria.Titulo,
                                                            produto.Titulo,
                                                            produto.Categoria.IDCategoriaPai,
                                                            produto.Categoria.ID,
                                                            produto.ID);

            lblTitulo.Text = produto.Titulo;
            lblCodigo.Text = String.Format("Cód. Ref.: {0:00000}", produto.ID);
            lblDescCurta.Text = produto.DescricaoCurta;
            lblDescCompleta.Text = produto.DescricaoCompleta;

            //if (produto.Estoque > 0)
            //{
            //    lblEstoque.Text = String.Format(@"<br />Apenas <span class='EstTabPedidoNome' 
            //                                            style='text-decoration:underline'>{0}</span> no estoque.",
            //                                    produto.Estoque);
            //}

            //if (produto.Desconto <= 0)
            //{
            //    lblPreco.Text = String.Format(@"<b>Por:</b> <span class='preco' style='font-size:25px;'>{0:R$ #,##0.00}</span><br/>", produto.Preco);
            //}
            //else
            //{
            //    lblPreco.Text = String.Format(@"<span class='precoDe'>De: {0:R$ #,##0.00}</span><br/>
            //                                    <b>Por:</b> <span class='preco' style='font-size:25px;'>{1:R$ #,##0.00}</span><br/>
            //                                    <span class='precoEcon'>Economize: {2:R$ #,##0.00}</span>",
            //                                    produto.Preco,
            //                                    produto.Preco - ((produto.Preco / 100) * produto.Desconto),
            //                                    produto.Preco - (produto.Preco - ((produto.Preco / 100) * produto.Desconto)));
            //}


            //if (produto.Videos.Count > 0)
            //{
            //    Session["video"] = produto.Videos[0].Titulo;
            //    imgVideo.Visible = true;
            //    mostraObjetoVideo(null, null);

            //    //Mostrando/Escondendo controles
            //    pnlImagem.Visible = false;
            //    pnlVideo.Visible = true;
            //}
            //else if (produto.Imagens.Count > 0)
            //{
            //    mostraObjetoImagem(produto.Imagens[0].ID, produto.Imagens[0].Titulo);
            //}
            //else
            //{
            //    mostraObjetoImagem(0, "");
            //}

            //if (produto.Estoque > 0)
            //    imgComprar.CommandArgument = id.ToString();
            //else
            //{
            //    imgComprar.Visible = false;
            //    imgProdIndisponivel.Visible = true;
            //}

            //carregaListaImagens(produto.Imagens);

            ////Produtos relacionados
            //if (idCategoria > -1)
            //{
            //    ProdutosOT _produtosRelacionados = new ProdutosOP().SelectProdutosRelacionados(id, idCategoria, 0, 4);
            //    if (_produtosRelacionados.Count > 0)
            //    {
            //        pnlProdRelacionados.Visible = true;
            //        dtlProd.DataSource = _produtosRelacionados;
            //        dtlProd.DataBind();
            //    }
            //    else
            //    {
            //        pnlProdRelacionados.Visible = false;
            //    }
            //}
        }

        private void carregaMetaTags()
        {
            //Preparando as META TAGS
            string description = produto.DescricaoCurta;
            string keywords = produto.PalavrasChave;
            string titulo = String.Concat(produto.Titulo, " - ",
                                          ConfigurationManager.AppSettings["nomeSiteCompleto"]);
            Page.Title = titulo;
            Utilitarios.CarregaMetaTags(this.Page, description, keywords, titulo);
        }
        protected void carregaMigalha()
        {
            if (produto != null)
            {
                Utilitarios.CriaLinksMigalhas(ref lnkMigalhaHome, ref lnkMigalhaCateg, ref lnkMigalhaSubCateg,
                                              produto.Categoria.IDCategoriaPai, produto.Categoria.ID);
            }
        }
        private void carregarPalavrasChave()
        {
            if (produto != null)
            {
                string[] arrPalavrasChave = produto.PalavrasChave.Split(',');

                lblPalavrasChave.Text = "";
                foreach (string palavra in arrPalavrasChave)
                {
                    if (!String.IsNullOrEmpty(lblPalavrasChave.Text))
                        lblPalavrasChave.Text += ", ";

                    lblPalavrasChave.Text += String.Format("<a href='/busca/{0}'>{0}</a>", Utilitarios.TiraAcentos(palavra.Trim()).Replace("-", " "));
                }
            }
        }
        
        protected void dtlProd_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            //{
            //    ProdutoOT produto = (ProdutoOT)e.Item.DataItem;
            //    HyperLink lnkImgProd = (HyperLink)e.Item.FindControl("lnkImgProd");
            //    HyperLink lnkDescricao = (HyperLink)e.Item.FindControl("lnkDescricao");
            //    HyperLink lnkPreco = (HyperLink)e.Item.FindControl("lnkPreco");
            //    Image imgProd = (Image)e.Item.FindControl("imgProd");

            //    Utilitarios.CarregaDescricaoProduto(produto, lnkImgProd, lnkDescricao, lnkPreco, imgProd, null);
            //}
        }

        protected void AdicionarItem(object sender, EventArgs e)
        {
            new Utilitarios().AdicionarItem(this.Page, Convert.ToInt32(((ImageButton)sender).CommandArgument), 1);
        }

        [WebMethod]
        public static void AdicionarFavorito(int idProduto, bool adicionar)
        {
            new Utilitarios().AdicionarFavorito(idProduto, adicionar);
        }
        #region -- Imagens e Vídeo --
        protected void carregaListaImagens(ProdutoImagensOT imagens)
        {
            //if (imagens.Count > 0 || Session["video"] != null)
            //{
            //    repListaImagens.DataSource = imagens;
            //    repListaImagens.DataBind();
            //}
        }
        public void listaImagensClick(Object src, CommandEventArgs e)
        {
            mostraObjetoImagem(Convert.ToInt32(e.CommandArgument), e.CommandName);
        }
        protected void repListaImagens_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                ProdutoImagemOT imagem = (ProdutoImagemOT)e.Item.DataItem;
                ImageButton img = (ImageButton)e.Item.FindControl("imgListaImagens");
                if (produto.Imagens.Count > 1)
                {
                    img.CommandArgument = imagem.ID.ToString();
                    img.CommandName = imagem.Titulo;
                    img.ImageUrl = "imagensProdutos/" + imagem.Titulo;
                    img.Width = 50;
                    img.Height = 50;
                }
                else
                    img.Visible = false;
            }
        }
        protected void mostraObjetoVideo(object sender, EventArgs e)
        {
            ////Mostrando/Escondendo controles
            //pnlImagem.Visible = false;
            //pnlVideo.Visible = true;
        }
        protected void mostraObjetoImagem(int idImg, string nameImg)
        {
            //Image imgAux = new Image();
            //imgAux.Width = 300;
            //imgAux.Height = 300;
            //imgAux.ToolTip = "Clique aqui para aumentar!";

            //if (idImg > 0 && nameImg != "")
            //{
            //    imgAux.ImageUrl = "~/imagensProdutos/" + nameImg;
            //    lnkImgProd.NavigateUrl = "~/imagensProdutos/" + nameImg;
            //    lnkImgProd.Attributes.Add("id", idImg.ToString());
            //    lnkImgProd.Attributes.Add("style", "cursor: crosshair;");
            //    lnkImgProd.Attributes.Add("rel", @"zoom-width:400px; 
            //                                       zoom-height:400px;
            //                                       zoom-position: right;");
            //}
            //else
            //{
            //    imgAux.ImageUrl = "~/imagensProdutos/sem_imagem.gif";
            //    lnkImgProd.Attributes.Add("style", "cursor: default;");
            //}

            //lnkImgProd.Controls.Add(imgAux);

            ////Mostrando/Escondendo controles
            //pnlImagem.Visible = true;
            //pnlVideo.Visible = false;
        }
        #endregion
    }
}
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
                Session["video"] = null;

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
#if DEBUG
            produto = ProdutosOP.CarregaProdutoFalso();
#else
            produto = new ProdutosOP().SelectProduto(id, 0, 1);
#endif

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
            chkFavoritos.Attributes.Add("prod", produto.ID.ToString());

            if (produto.Imagens.Count > 0)
            {
                repImages.DataSource = produto.Imagens;
                repImages.DataBind();

                repThumbs.DataSource = produto.Imagens;
                repThumbs.DataBind();
            }

            if (produto.Estoque > 0)
            {
                lnkComprar.CommandArgument = produto.ID.ToString();
                lblEstoque.Text = String.Format(@"<p><strong>Apenas <u>{0}</u> em estoque.</br>
                                                     Compre agora no cartão de crédito e parcele em até 18x*</strong></p>",
                                                produto.Estoque);
            }
            else
            {
                lnkComprar.Visible = false;
                lblDetalhes.Visible = true;
            }

            if (lblPreco != null)
            {
                //Tratamento dos preco
                string preco = "";
                if (produto.Desconto <= 0)
                {
                    preco = String.Format(@"<p>Por:</p><ul><li>&nbsp;{0:R$ #,##0.00}</li></ul>", produto.Preco);
                }
                else
                {
                    //Exibindo desconto            
                    preco = String.Format(@"<p>De: <span>{0:R$ #,##0.00}</span></p>
                                                <p>Por: </p><ul><li>{1:R$ #,##0.00}</li></ul>
                                                <p class='precoEcon'>Economize: {2:R$ #,##0.00}</p>",
                                            produto.Preco,
                                            produto.Preco - ((produto.Preco / 100) * produto.Desconto),
                                            produto.Preco - (produto.Preco - ((produto.Preco / 100) * produto.Desconto)));
                }
                lblPreco.Text = preco;
            }

            if (produto.Videos.Count > 0)
            {
                pnlVideoProduto.Visible = true;
                Session["video"] = produto.Videos[0].Titulo;
            }

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
            string titulo = String.Format("{0} - {1}", produto.Titulo,
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
#region -- Adiocionar ao carrinho e favoritos --
        protected void AdicionarItem(object sender, EventArgs e)
        {
            new Utilitarios().AdicionarItem(this.Page, Convert.ToInt32(((LinkButton)sender).CommandArgument), 1);
        }
        [WebMethod]
        public static void AdicionarFavorito(int idProduto, bool adicionar)
        {
            new Utilitarios().AdicionarFavorito(idProduto, adicionar);
        }
#endregion
#region -- Imagens e Vídeo --
        protected void repImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                ProdutoImagemOT imagem = (ProdutoImagemOT)e.Item.DataItem;
                Image img = (Image)e.Item.FindControl("imgFotoProduto");

                img.ToolTip = imagem.Titulo;
                img.ImageUrl = "/imagensProdutos/" + imagem.Titulo;                
            }
        }
        protected void repThumbs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                ProdutoImagemOT imagem = (ProdutoImagemOT)e.Item.DataItem;
                ImageButton img = (ImageButton)e.Item.FindControl("imgThumb");

                img.CommandArgument = imagem.ID.ToString();
                img.CommandName = imagem.Titulo;
                img.ImageUrl = "/imagensProdutos/" + imagem.Titulo;
            }
        }
        //protected void mostraObjetoVideo(object sender, EventArgs e)
        //{
        //    //Mostrando/Escondendo controles
        //    pnlImagem.Visible = false;
        //    pnlVideo.Visible = true;
        //}
        //protected void mostraObjetoImagem(int idImg, string nameImg)
        //{
        //    Image imgAux = new Image();
        //    imgAux.Width = 300;
        //    imgAux.Height = 300;
        //    imgAux.ToolTip = "Clique aqui para aumentar!";

        //    if (idImg > 0 && nameImg != "")
        //    {
        //        imgAux.ImageUrl = "~/imagensProdutos/" + nameImg;
        //        lnkImgProd.NavigateUrl = "~/imagensProdutos/" + nameImg;
        //        lnkImgProd.Attributes.Add("id", idImg.ToString());
        //        lnkImgProd.Attributes.Add("style", "cursor: crosshair;");
        //        lnkImgProd.Attributes.Add("rel", @"zoom-width:400px; 
        //                                           zoom-height:400px;
        //                                           zoom-position: right;");
        //    }
        //    else
        //    {
        //        imgAux.ImageUrl = "~/imagensProdutos/sem_imagem.gif";
        //        lnkImgProd.Attributes.Add("style", "cursor: default;");
        //    }

        //    lnkImgProd.Controls.Add(imgAux);

        //    //Mostrando/Escondendo controles
        //    pnlImagem.Visible = true;
        //    pnlVideo.Visible = false;
        //}
#endregion
    }
}
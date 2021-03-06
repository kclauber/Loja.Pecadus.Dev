﻿using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;

namespace Loja.UI.Pecadus
{
    public partial class categorias : System.Web.UI.Page
    {
        private int inicio = 0;
        private int pagina = 0;

        private int categoriaID = -1;
        private int categoriaPaiID = -1;
        private string categoriaTitulo = "";
        private string categoriaPaiTitulo = "";

        private int idCategoriaPaiAux = -1;
        private string tituloCategoriaPaiAux = "";

        private HtmlGenericControl h3 = null;
        private HtmlGenericControl div = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (RouteData.Values.Count > 0)
                {
                    if (RouteData.Values["categoriaID"] != null && RouteData.Values["categoriaID"].ToString() != "")
                        categoriaID = Convert.ToInt32(RouteData.Values["categoriaID"]);

                    if (RouteData.Values["categoriaPaiID"] != null && RouteData.Values["categoriaPaiID"].ToString() != "")
                        categoriaPaiID = Convert.ToInt32(RouteData.Values["categoriaPaiID"]);

                    categoriaTitulo = Convert.ToString(RouteData.Values["categoriaTitulo"]);
                    categoriaPaiTitulo = Convert.ToString(RouteData.Values["categoriaPaiTitulo"]);
                }

                if (ViewState["contador"] == null)
                    ViewState["contador"] = 1;

                ViewState["inicio"] = 0;
                ViewState["pagina"] = int.Parse(ConfigurationManager.AppSettings["paginas"]);
                ViewState["numPagina"] = 0;
                ViewState["fimPaginacao"] = 4;

                ListarProdutos();
                carregarMigalha();
                carregarMenuLateral();
                carregaTagCloud();

                //Preparando as META TAGS
                string description = String.Concat("Sex Shop online com grandes promoções, descontos e frete promocional. ",
                                                   "Compre produtos de sex shop com segurança e conforto. ",
                                                   "Milhares de artigos e acessórios eróticos.");
                string keywords = "sex shop, sexshop, sex-shop, sexyshop, loja virtual, compra online, artigos eroticos, acessorios eroticos";

                Page.Title = (!String.IsNullOrEmpty(Page.Title) ?
                                    Page.Title : ConfigurationManager.AppSettings["tituloPadrao"]);
                Utilitarios.CarregaMetaTags(this.Page, description, keywords, Page.Title);
            }
        }
        public void ListarProdutos()
        {
            ProdutosOT produtos = new ProdutosOT();
#if DEBUG
            ProdutoOT _produto = ProdutosOP.CarregaProdutoFalso();
            produtos.Add(_produto);
#else
            produtos = new ProdutosOP().SelectProdutosDestaque();
#endif

            //Busca a primeira lista de produtos
            repProdutoDestaque1.DataSource = produtos;
            repProdutoDestaque1.DataBind();
        }
        public void carregarMigalha()
        {
            //if (categoriaID > -1 && categoriaPaiID > -1)
            //{
            //    Utilitarios.CriaLinksMigalhas(ref lnkMigalhaHome, ref lnkMigalhaCategPai, ref lnkMigalhaCateg, categoriaPaiID, categoriaID);
            //    lnkMigalhaCategPai.Visible = true;
            //    lnkMigalhaCateg.Visible = true;

            //}
            //else if (categoriaPaiID > -1)
            //{
            //    Utilitarios.CriaLinksMigalhas(ref lnkMigalhaHome, ref lnkMigalhaCategPai, categoriaPaiID);
            //    lnkMigalhaCategPai.Visible = true;
            //}
        }
        
        #region -- Menu lateral --
        // Caso seja alterado algum método aqui, alterar também na página de ERRO!!

        private void carregarMenuLateral()
        {
            //CategoriasOT _categorias = new CategoriasOT();
            //new CategoriasOP().SelectCategoriasMenu(categoriaPaiID, ref _categorias);
            //repMenuLateral.DataSource = _categorias;
            //repMenuLateral.DataBind();
        }
        private void carregaTagCloud()
        {
            //DataView[] dv = new DataView[2];
            //dv = new ProdutosOP().SelectTagCloud();
            //ViewState["TdTagCloudPar"] = dv[1].Table;
            //if (dv[0].Table.Rows.Count > 0)
            //{
            //    repTagCloud.DataSource = dv[0];
            //    repTagCloud.DataBind();
            //}
            //else
            //    pnlCloud.Visible = false;
        }

        protected void repTagCloud_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["TdTagCloudPar"];
            int midValueBusca = int.Parse(dt.Rows[0]["maxValueBusca"].ToString()) / 2;
            int size = 0;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink lnkAux = (HyperLink)e.Item.FindControl("lnkTag");

                if (DataBinder.Eval(e.Item.DataItem, "tituloPai").ToString() != "")
                    lnkAux.Text = DataBinder.Eval(e.Item.DataItem, "tituloPai").ToString() + " ";

                lnkAux.Text += DataBinder.Eval(e.Item.DataItem, "titulo").ToString();

                size = int.Parse(DataBinder.Eval(e.Item.DataItem, "hits").ToString());

                if (size < (midValueBusca / 2))
                {
                    lnkAux.Style.Add("font-size", "10px");
                    lnkAux.Style.Add("color", "#da8585");
                }
                else if (size < (midValueBusca + (midValueBusca / 2)) && size >= midValueBusca)
                {
                    lnkAux.Style.Add("font-size", "11px");
                    lnkAux.Style.Add("text-transform", "lowercase");
                    lnkAux.Style.Add("color", "#fbadd4");
                }
                else if (size >= (midValueBusca + (midValueBusca / 2)))
                {
                    lnkAux.Style.Add("text-transform", "capitalize");
                    lnkAux.Style.Add("font-size", "12px");
                    lnkAux.Style.Add("color", "#e865a7");
                }
                else if (size < midValueBusca && size >= (midValueBusca / 2))
                {
                    lnkAux.Style.Add("font-size", "14px");
                    lnkAux.Style.Add("text-transform", "uppercase");
                    lnkAux.Style.Add("color", "#f99");
                }

                lnkAux.NavigateUrl = "/Busca/" + DataBinder.Eval(e.Item.DataItem, "titulo").ToString();
            }
        }
        protected void repMenuLateral_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "idCategoriaPai"))== -1)
                {
                    h3 = new HtmlGenericControl("h3");
                    h3.InnerHtml = Utilitarios.CriaStringLinkCategoria(DataBinder.Eval(e.Item.DataItem, "titulo").ToString(),
                                                                       Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id")));
                    e.Item.Controls.Add(h3);

                    div = new HtmlGenericControl("div");
                    e.Item.Controls.Add(div);

                    idCategoriaPaiAux = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"));
                    tituloCategoriaPaiAux = DataBinder.Eval(e.Item.DataItem, "titulo").ToString();
                }
                else
                {
                    div.InnerHtml += Utilitarios.CriaStringLinkCategoria(tituloCategoriaPaiAux, DataBinder.Eval(e.Item.DataItem, "titulo").ToString(),
                                                                         idCategoriaPaiAux, Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id")));
                    div.InnerHtml += "</br>";
                }                
            }
        }
        // Caso seja alterado algum método aqui, alterar também na página de ERRO!!
        #endregion

        protected void repProd1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                Utilitarios.CarregaDescricaoProduto(e.Item);
            }
        }
        protected void AdicionarItem(object sender, EventArgs e)
        {
            new Utilitarios().AdicionarItem(this.Page, Convert.ToInt32(((LinkButton)sender).CommandArgument), 1);
        }
    }
}
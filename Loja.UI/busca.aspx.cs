using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;

namespace Loja.UI.Pecadus
{
    public partial class busca : System.Web.UI.Page
    {
        ProdutosOT produtos = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            int qtd = 0;

            if (!Page.IsPostBack)
            {
                //string palavrasBusca = Utilitarios.TrataBusca(Convert.ToString(RouteData.Values["busca"]));
                //qtd = CarregaBusca(palavrasBusca);

                //if (qtd > 0)
                //{
                //    string resultado = String.Format("Encontramos {0} produto(s) buscando por \"{1}\"", qtd, Convert.ToString(RouteData.Values["busca"]));
                //    Page.Title = String.Format("{0} - {1}", ConfigurationManager.AppSettings["nomeSiteCompleto"], resultado);
                //    lblResultado.Text = resultado;
                //}
                //else
                //{
                //    Page.Title = String.Format("{1} - Sua busca por \"{0}\" não retornou produtos. Tente com palavras similares", palavrasBusca,
                //        ConfigurationManager.AppSettings["nomeSiteCompleto"]);
                //    pnlBusca.Visible = false;
                //    pnlVazio.Visible = true;
                //    lblBusca.Text = palavrasBusca.ToString();
                //}
            }
        }
        public int CarregaBusca(string palavrasBusca)
        {
            produtos = new ProdutosOP().SelectProdutosBusca(palavrasBusca, -1, -1);
            if (produtos != null)
            {
                dtlProdutosBusca.DataSource = produtos;
                dtlProdutosBusca.DataBind();
                return produtos.Count;
            }
            return 0;
        }

        protected void repProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                ProdutoOT produto = (ProdutoOT)e.Item.DataItem;
                HyperLink lnkImgProd = (HyperLink)e.Item.FindControl("lnkImgProd");
                HyperLink lnkDescricao = (HyperLink)e.Item.FindControl("lnkDescricao");
                HyperLink lnkPreco = (HyperLink)e.Item.FindControl("lnkPreco");
                Image imgProd = (Image)e.Item.FindControl("imgProd");

                Utilitarios.CarregaDescricaoProduto(produto, lnkImgProd, lnkDescricao, lnkPreco, imgProd, null, null);
            }
        }
        protected void dtlProd_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                ProdutoOT produto = (ProdutoOT)e.Item.DataItem;
                HyperLink lnkImgProd = (HyperLink)e.Item.FindControl("lnkImgProd");
                HyperLink lnkDescricao = (HyperLink)e.Item.FindControl("lnkDescricao");
                HyperLink lnkPreco = (HyperLink)e.Item.FindControl("lnkPreco");
                Image imgProd = (Image)e.Item.FindControl("imgProd");

                Utilitarios.CarregaDescricaoProduto(produto, lnkImgProd, lnkDescricao, lnkPreco, imgProd, null, null);
            }
        }
    }
}
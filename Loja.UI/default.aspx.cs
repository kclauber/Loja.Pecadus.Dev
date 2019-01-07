using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;

namespace Loja.UI.Pecadus
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Preparando as META TAGS
            string description = String.Concat("Sex Shop online com grandes promoções, descontos e frete promocional. ",
                                                "Compre produtos de sex shop com segurança e conforto. ",
                                                "Milhares de artigos e acessórios eróticos.");
            string keywords = "sex shop, sexshop, sex-shop, sexyshop, loja virtual, compra online, artigos eroticos, acessorios eroticos";

            string titulo = String.Format("{0} - {1}", ConfigurationManager.AppSettings["nomeSiteCompleto"], 
                                                       ConfigurationManager.AppSettings["tituloPadrao"]);
            Page.Title = titulo;
            Utilitarios.CarregaMetaTags(this.Page, description, keywords, titulo);

            if (!Page.IsPostBack)
            {
                ListarProdutos();
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

            //Busca a segunda lista de produtos
            //produtos = new ProdutosOP().SelectProdutosHome();
            produtos[0].ID = 2;
            repProdutoDestaque2.DataSource = produtos;
            repProdutoDestaque2.DataBind();
        }
        protected void repProd1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                Utilitarios.CarregaDescricaoProduto(e.Item);
            }
        }
        protected void repProd2_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
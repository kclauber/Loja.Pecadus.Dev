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

            string titulo = ConfigurationManager.AppSettings["tituloPadrao"];
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
            ProdutoOT _produto = new ProdutoOT();
            _produto.ID = 1;
            //_produto.EAN = Convert.ToString(_row["ean"]);
            _produto.Titulo = "Titulo Titulo Titulo Titulo Titulo";
            _produto.DescricaoCurta = "Descricao Curta Descricao Curta Descricao Curta";
            _produto.DescricaoCompleta = "Descricao Completa Descricao Completa Descricao Completa Descricao Completa";
            //_produto.PalavrasChave = Convert.ToString(_row["palavrasChave"]);
            //_produto.Observacao = Convert.ToString(_row["observacao"]);

            _produto.Preco = Convert.ToDouble(200.89);
            //_produto.PrecoCusto = Convert.ToDouble(_row["precoCusto"]);
            //_produto.MarkUp = Convert.ToDouble(_row["markUp"]);
            _produto.Desconto = Convert.ToInt32(5.00);
            //_produto.Frete = Convert.ToDouble(_row["frete"]);
            //_produto.Peso = Convert.ToInt32(_row["peso"]);
            //_produto.Estoque = Convert.ToInt32(_row["estoque"]);

            //_produto.DtCadastro = Convert.ToDateTime(_row["dtCadastro"]);
            //_produto.ExibirHome = Convert.ToBoolean(_row["exibirHome"]);
            //_produto.Destaque = Convert.ToBoolean(_row["destaque"]);
            //_produto.Ativo = Convert.ToBoolean(_row["ativo"]);

            ////Dados do distribuidor
            //_produto.Distribuidor.ID = Convert.ToInt32(_row["idDistribuidor"]);
            //_produto.Distribuidor.Nome = Convert.ToString(_row["nomeDistribuidor"]);

            //Dados da categoria
            _produto.Categoria.ID = 5;
            _produto.Categoria.IDCategoriaPai = 10;
            _produto.Categoria.Titulo = "Titulo Categoria";
            _produto.Categoria.TituloCategoriaPai = "Titulo Categoria Pai";

            //_produto.Imagens = SelectImagensProduto(_produto.ID);
            //_produto.Videos = SelectVideoProduto(_produto.ID);

            produtos.Add(_produto);

            //Busca a primeira lista de produtos
            //produtos = new ProdutosOP().SelectProdutosDestaque();
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
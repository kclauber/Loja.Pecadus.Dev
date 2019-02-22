using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;
using System;
using System.Configuration;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Resources;

public partial class finalizarCompra : System.Web.UI.Page
{
    bool isSandbox = bool.Parse(ConfigurationManager.AppSettings["isSandbox"]);
    ProdutosOT produtosCarrinho = null;
    public String pagSeguroURL = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = String.Format("{0} - {1}", ConfigurationManager.AppSettings["nomeSiteCompleto"],
                                                "Obrigado pela sua compra!");
        FinalizarCompra();
    }
    private void FinalizarCompra()
    {
        if (Carrinho.Instancia.TemItens)
        {
            //Buscando novamente os detalhes dos itens
            CarregaObjetoCarrinho();

            PaymentRequest payment = new PaymentRequest();
            //payment.Reference = "";

            foreach (ProdutoOT prod in produtosCarrinho)
            {
                payment.Items.Add(new Item(prod.ID.ToString(),
                                           Utilitarios.TiraAcentos(prod.Titulo).Replace("-", " "),
                                           prod.QuantidadeCarrinho,
                                           Convert.ToDecimal(prod.Preco)));
            }

            #region ## Elementos opicionais ##
            //Opcional
            payment.Sender = new Sender(Cliente.Instancia.Nome,
                                        Cliente.Instancia.Email,
                                        new Phone(
                                            Cliente.Instancia.Celular.Split(' ')[0],
                                            Cliente.Instancia.Celular.Split(' ')[1]
                                        )
                                    );

            //Opcional
            payment.Shipping = new Shipping
            {
                ShippingType = ShippingType.Sedex,
                Address = new Address {
                    Country = "BRA",
                    State = Cliente.Instancia.Estado,
                    City = Cliente.Instancia.Cidade,
                    District = Cliente.Instancia.Bairro,
                    PostalCode = Cliente.Instancia.CEP.Replace("-", ""),
                    Street = Cliente.Instancia.Endereco,
                    Number = Cliente.Instancia.Numero,
                    Complement = Cliente.Instancia.Complemento
                }
            };

            //Opcional
            SenderDocument senderCPF = new SenderDocument(Documents.GetDocumentByType("CPF"), Cliente.Instancia.CPF);
            payment.Sender.Documents.Add(senderCPF);
            #endregion

            AccountCredentials credentials = new AccountCredentials(
                PagSeguroConfiguration.Credentials(isSandbox).Email,
                PagSeguroConfiguration.Credentials(isSandbox).Token
            );

            //Carrinho.Instancia.Limpar();

            //Enviando ao Pag Seguro
            Uri paymentRedirectUri = payment.Register(credentials);
            redirectPagSeguro.NavigateUrl = paymentRedirectUri.ToString();
            pagSeguroURL = paymentRedirectUri.ToString();

            //Response.Redirect(paymentRedirectUri.ToString());
        }
    }
    public void CarregaObjetoCarrinho()
    {
        produtosCarrinho = new ProdutosOT();
        for (int a = 0; a < Carrinho.Instancia.CodigosItens.Length; a++)
        {
            ProdutoOT produto = new ProdutoOT
            {
                ID = Carrinho.Instancia.CodigosItens[a]
            };
#if DEBUG
                produto = ProdutosOP.CarregaProdutoFalso(produto.ID);
#else
            produto = new ProdutosOP().SelectProduto(produto.ID, -1, -1);
#endif

            produto.QuantidadeCarrinho = Carrinho.Instancia.ObterQuantidadeItem(Carrinho.Instancia.CodigosItens[a]);
            produtosCarrinho.Add(produto);
        }
    }
}
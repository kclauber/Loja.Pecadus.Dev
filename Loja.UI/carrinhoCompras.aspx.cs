using System;
using System.Configuration;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Resources;

namespace Loja.UI.Pecadus
{
    public partial class carrinhoCompras : System.Web.UI.Page
    {
        bool isSandbox = bool.Parse(ConfigurationManager.AppSettings["isSandbox"]);
        private bool freteGratis = false;
        public string sStatus, sToken = "";
        protected double vlTotalProdutos = 0;
        //private string TOKEN, KEY, URI, sURLRedirect;
        ProdutosOT produtosCarrinho = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.Title = "Veja os produtos que estão no seu carrinho de compras";

            //if (!Page.IsPostBack)
            //{
            //    Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            //    Session["PesoCarrinho"] = 0;

            //    if (Carrinho.Instancia.TemItens)
            //    {
            //        AtualizaCarrinho();
            //    }
            //    else
            //    {
            //        pnlCarrinho.Visible = false;
            //        pnlVazio.Visible = true;
            //    }

            //    txtCep1.Attributes.Add("onkeyup", "if(ctl00$ContentPlaceHolder1$txtCep1.value.length==5){ctl00$ContentPlaceHolder1$txtCep2.focus()};");
            //}
        }
        public void CarregaObjetoCarrinho()
        {
            produtosCarrinho = new ProdutosOT();
            for (int a = 0; a < Carrinho.Instancia.CodigosDosItens.Length; a++)
            {
                ProdutoOT _produto = new ProdutoOT();
                _produto.ID = Carrinho.Instancia.CodigosDosItens[a];
                _produto = new ProdutosOP().SelectProduto(_produto.ID, -1, -1);
                _produto.QuantidadeCarrinho = Carrinho.Instancia.ObterQuantidadeItem(Carrinho.Instancia.CodigosDosItens[a]);
                produtosCarrinho.Add(_produto);
            }
        }        
        protected void repCarr_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                ProdutoOT produto = (ProdutoOT)e.Item.DataItem;

                string link = Utilitarios.CriaStringLinkProduto(produto.Categoria.TituloCategoriaPai,
                                                                produto.Categoria.Titulo,
                                                                produto.Titulo,
                                                                produto.Categoria.IDCategoriaPai,
                                                                produto.Categoria.ID,
                                                                produto.ID);

                ((LinkButton)e.Item.FindControl("btnExcluir")).CommandArgument = produto.ID.ToString();

                HyperLink lnk;
                lnk = (HyperLink)e.Item.FindControl("lnkImgProd");
                lnk.ToolTip = produto.Titulo;
                lnk.NavigateUrl = link;

                lnk = (HyperLink)e.Item.FindControl("lnkTitulo");
                lnk.NavigateUrl = link;
                lnk.ToolTip = produto.Titulo;
                lnk.Text = produto.Titulo;

                ((TextBox)e.Item.FindControl("txtQtd")).Text = produto.QuantidadeCarrinho.ToString();
                ((TextBox)e.Item.FindControl("txtQtd")).Attributes.Add("style", "text-align:center");
                ((Label)e.Item.FindControl("lblRef")).Text = String.Format("Ref.: {0:00000} - ", produto.ID);

                if (produto.Desconto > 0)
                    produto.Preco = produto.Preco - ((produto.Preco / 100) * produto.Desconto);

                ((Label)e.Item.FindControl("lblVlUnit")).Text = String.Format("{0:R$ #,##0.00}", produto.Preco);
                ((Label)e.Item.FindControl("lblVlTotProd")).Text = String.Format("{0:R$ #,##0.00}", produto.Preco * produto.QuantidadeCarrinho);

                ((RangeValidator)e.Item.FindControl("rngQtd")).MaximumValue = produto.Estoque.ToString();
                ((RangeValidator)e.Item.FindControl("rngQtd")).ErrorMessage =
                "Máximo de XXX unidades deste item por pedido.".Replace("XXX", produto.Estoque.ToString());

                //Tratamento das imagens
                Image imgProd = (Image)e.Item.FindControl("imgProd");
                imgProd.AlternateText = produto.Titulo;
                if (produto.Imagens.Count > 0)
                    imgProd.ImageUrl = @"ShowImage.aspx?w=90&img=" + produto.Imagens[0].Titulo;

                vlTotalProdutos += (produto.Preco * produto.QuantidadeCarrinho);
                Session["PesoCarrinho"] = Convert.ToInt32(Session["PesoCarrinho"]) + (produto.Peso * produto.QuantidadeCarrinho);
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("lblSubTotalCompra")).Text = String.Format("{0:R$ #,##0.00}", vlTotalProdutos);                
            }

            lblTotalCompra.Text = String.Format("{0:R$ #,##0.00}", vlTotalProdutos);
            Session["VlrTotalSemFrete"] = vlTotalProdutos;
        }

        #region Botões carrinho
        public void RemoverItem(object sender, EventArgs e)
        {
            new Utilitarios().RemoverItem(Convert.ToInt32(((LinkButton)sender).CommandArgument));
            AtualizaCarrinho();
        }
        protected void imgContinuarCompra_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings["home"]);
        }
        protected void imgAtualizaQuantidade_Click(object sender, ImageClickEventArgs e)
        {
            AtualizaCarrinho();
        }
        private void AtualizaCarrinho()
        {
            atualizarItens();

            if (Carrinho.Instancia.TemItens)
            {
                CarregaObjetoCarrinho();
                repCarr.DataSource = produtosCarrinho;
                repCarr.DataBind();
            }
            else
            {
                Session["PesoCarrinho"] = 0;
                Session["CepDestino"] = "";
                Session["TipoFrete"] = "";
                Session["VlrFrete"] = 0;
                Session["VlrTotalSemFrete"] = 0;
                Session["VlrTotalComFrete"] = 0;

                pnlCarrinho.Visible = false;
                pnlVazio.Visible = true;
            }
        }
        protected void atualizarItens()
        {
            foreach (RepeaterItem item in repCarr.Items)
            {
                int id = Convert.ToInt32(((LinkButton)item.FindControl("btnExcluir")).CommandArgument);
                int quantidade = 0;
                if (String.IsNullOrEmpty(((TextBox)item.FindControl("txtQtd")).Text))
                    quantidade = 1;
                else
                    quantidade = Convert.ToInt32(((TextBox)item.FindControl("txtQtd")).Text);

                Carrinho.Instancia.AtualizarQuantidadeItem(id, quantidade);
            }
        }
        #endregion
        #region Calculo de Frete
        protected void btnCalcularFrete_Click(object sender, EventArgs e)
        {
            try
            {
                CalcularFrete();
                AdicionarValorFrete();
            }
            catch (Exception ex)
            {
                new Utilitarios().TratarExcessao(ex, Request.Url.ToString(), "carrinho.btnCalcularFrete_Click", this.Page);
            }
        }
        protected void CalcularFrete()
        {
            string status = "";
            string cepDestino = txtCep1.Text + txtCep2.Text;
            string peso = Session["PesoCarrinho"].ToString();
            Session["CepDestino"] = cepDestino;

            try
            {
                //Formatando o peso
                if (peso.Length >= 4)
                    peso = peso.Substring(0, 1) + "." + peso.Substring(1, peso.Length - 1);
                else
                    peso = "0." + peso;

                //Cria uma requisição ao webService com os dados informados
				//Serviço saiu do ar - trocar por outra alternativa URGENTE
				rdlFrete.Items.Add(new ListItem(String.Format("PAC - R$ {0:#,##0.00} (promoção frete fixo p/ todo Brasil)", 10.00d),
                                                String.Format("EN{0:#,##0.00}", 10.00d)));
                rdlFrete.Items[0].Selected = true;
				rdlFrete.Visible = true;

                //WebRequest request = WebRequest.Create(
                //        "http://frete.w21studio.com/calFrete.xml?cep=" + cepDestino +
                //        "&cod=4225&peso=" + peso +
                //        "&comprimento=" + ConfigurationManager.AppSettings["caixaComprimento"].ToString() +
                //        "&largura=" + ConfigurationManager.AppSettings["caixaLargura"].ToString() +
                //        "&altura=" + ConfigurationManager.AppSettings["caixaAltura"].ToString() +
                //        "&servico=3");
                //WebResponse response = request.GetResponse();
                //StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF7);
                ////Coloca os dados recebidos em um DataSet
                //DataSet ds = new DataSet();
                //ds.ReadXml(sr);
                //sr.Close();
                //response.Close();

                //rdlFrete.Items.Clear();
                //if (ds.Tables["frete"].Rows[0]["status"].ToString() == "OK")
                //{
                //    double vlrFrete = 0;
                //    //SEDEX ***********************************************//
                //    //Custo do frete + a embalagem
                //    vlrFrete = Convert.ToDouble(ds.Tables["frete"].Rows[0]["valor_sedex"].ToString().Replace(".", ","));
                //    vlrFrete += Convert.ToDouble(ConfigurationManager.AppSettings["caixaFrete"].ToString());
                //    rdlFrete.Items.Add(new ListItem(String.Format("Sedex - R$ {0:#,##0.00}", vlrFrete),
                //                                    String.Format("SD{0:#,##0.00}", vlrFrete)));
                //    //*****************************************************//

                //    //PAC *************************************************//
                //    //Promoção frete grátis
                //    if (freteGratis)
                //        vlrFrete = 0;
                //    else
                //    {
                //        vlrFrete = Convert.ToDouble(ds.Tables["frete"].Rows[0]["valor_pac"].ToString().Replace(".", ","));
                //        vlrFrete += Convert.ToDouble(ConfigurationManager.AppSettings["caixaFrete"].ToString());
                //    }
                //    rdlFrete.Items.Add(new ListItem(String.Format("PAC - R$ {0:#,##0.00}", vlrFrete),
                //                                    String.Format("EN{0:#,##0.00}", vlrFrete)));
                    
                //    //*****************************************************//
                //    //Promoção frete grátis
                //    if (freteGratis)
                //        rdlFrete.Items[1].Selected = true; //Envio por PAC
                //    else
                //        rdlFrete.Items[0].Selected = true; //Envio por Sedex
                //    //****************************************************//

                //    rdlFrete.Visible = true;
                //}				

                //status = ds.Tables["frete"].Rows[0]["status"].ToString();
            }
            catch (Exception ex)
            {
                new Utilitarios().TratarExcessao(ex, Request.Url.ToString(), "carrinho.CalculaFrete", this.Page);
            }
        }
        protected void rdlFrete_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdicionarValorFrete();
        }
        private void AdicionarValorFrete()
        {
            if (rdlFrete.Visible == true)
            {
                if (rdlFrete.SelectedIndex > -1)
                {
                    Session["VlrFrete"] = Convert.ToDouble(rdlFrete.SelectedValue.Replace("SD", "").Replace("EN", ""));
                    Session["TipoFrete"] = (rdlFrete.SelectedValue.Substring(0,2).ToString().Equals("SD") ? "2" : "1");
                    //Tipos de frete aceitos pelo PagSeguro
                    //1 	Encomenda normal (PAC)
                    //2 	SEDEX
                    //3 	Tipo de frete não especificado

                    //Adicionando o valor do frete ao valor total
                    Session["VlrTotalComFrete"] = Convert.ToDouble(Session["VlrTotalSemFrete"]) + Convert.ToDouble(Session["VlrFrete"]);
                    lblTotalCompra.Text = String.Format("{0:R$ #,##0.00}", Convert.ToDouble(Session["VlrTotalComFrete"]));
                }
            }
        }
        #endregion        
        #region Finalizar Compra
        #region ## Métodos com o framework PagSeguro ##
        protected void imgFinalizar_Click(object sender, ImageClickEventArgs e)
        {
            FinalizarCompra();
        }
        private void FinalizarCompra()
        {
            Page.Validate();

            if (Page.IsValid)
            {
                atualizarItens();

                if (rdlFrete.Visible == false || rdlFrete.SelectedIndex == -1)
                {
                    CalcularFrete();
                    AdicionarValorFrete();
                }

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
                    ////Opcional
                    //payment.Sender = new Sender(
                    //    "José Comprador",
                    //    "c43738373132648712943@sandbox.pagseguro.com.br",
                    //    new Phone(
                    //        "11",
                    //        "56273440"
                    //    )
                    //);

                    ////Opcional
                    //payment.Shipping = new Shipping();
                    //payment.Shipping.ShippingType = ShippingType.Sedex;
                    //payment.Shipping.Address = new Address(
                    //    "BRA",
                    //    "SP",
                    //    "Sao Paulo",
                    //    "Jardim Paulistano",
                    //    "01452002",
                    //    "Av. Brig. Faria Lima",
                    //    "1384",
                    //    "5o andar"
                    //);

                    ////Opcional
                    //SenderDocument senderCPF = new SenderDocument(
                    //    Documents.GetDocumentByType("CPF"), "12345678909");
                    //payment.Sender.Documents.Add(senderCPF);
                    #endregion

                    AccountCredentials credentials = new AccountCredentials(
                        PagSeguroConfiguration.Credentials(isSandbox).Email,
                        PagSeguroConfiguration.Credentials(isSandbox).Token
                    );

                    Carrinho.Instancia.Limpar();

                    //Enviando ao Pag Seguro
                    Uri paymentRedirectUri = payment.Register(credentials);
                    Response.Redirect(paymentRedirectUri.ToString());
                }
            }
        }
        #endregion
        #region ## Métodos Antigos ##
        /// <summary>
        /// Gera uma Url com os itens do carrinho e inclui o frete como um item extra
        /// PagSeguro => Utilizar opção "frete fixo"
        /// Ex. de chamada: string urlPagSeguro = geraHtmlItensComFrete();
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private string geraHtmlItensComFrete()
        {
            //Criando o post que será enviado ao pagseguro
            StringBuilder sb = new StringBuilder();
            sb.Append(@"https://pagseguro.uol.com.br/v2/checkout/payment.html");
            sb.Append("?receiverEmail=" + ConfigurationManager.AppSettings["mailCobrancaPagSeguro"]);
            sb.Append("&currency=BRL");

            //adicionando os itens
            int x = 0;
            foreach (ProdutoOT _produto in produtosCarrinho)
            {
                x++;
                sb.AppendFormat("&itemId{0}={1}", x, _produto.ID);
                sb.AppendFormat("&itemDescription{0}={1}", x, _produto.Titulo);
                sb.AppendFormat("&itemQuantity{0}={1}", x, _produto.QuantidadeCarrinho);

                if (_produto.Desconto > 0)
                    _produto.Preco = _produto.Preco - ((_produto.Preco / 100) * _produto.Desconto);
                sb.AppendFormat("&itemAmount{0}={1}", x, String.Format("{0:#,##0.00}", _produto.Preco).Replace(".", "").Replace(",", "."));
                
                sb.AppendFormat("&itemWeight{0}={1}", x, _produto.Peso);
            }

            //Informações de frete (opcionais)////////////////
            sb.AppendFormat("&shippingType={0}", Session["TipoFrete"]);
            sb.AppendFormat("&itemShippingCost1={0}", String.Format("{0:#,##0.00}", Convert.ToDouble(Session["VlrFrete"])).Replace(".", "").Replace(",", "."));
            sb.AppendFormat("&shippingAddressPostalCode={0}", Session["CepDestino"]);
            //shippingAddressStreet=Av. Brig. Faria Lima
            //shippingAddressNumber=1384
            //shippingAddressComplement=5o andar
            //shippingAddressDistrict=Jardim Paulistano
            //shippingAddressCity=Sao Paulo
            //shippingAddressState=SP
            //shippingAddressCountry=BRA		

            //Dados do comprador (opcionais)/////////////////
            //senderName=José Comprador
            //senderAreaCode=11
            //senderPhone=56273440
            //senderEmail=comprador@uol.com.br          

            return sb.ToString();
        }
        /// <summary>
        /// Gera uma Url com os itens do carrinho e inclui o peso para o frete ser calculado pelo PagSeguro
        /// PagSeguro => Utilizar opção "frete por peso"
        /// Ex. de chamada: string urlPagSeguro = geraHtmlItensSemFrete();
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private string geraHtmlItensSemFrete()
        {
            //Criando o post que será enviado ao pagseguro
            StringBuilder sb = new StringBuilder();
            sb.Append(@"https://pagseguro.uol.com.br/v2/checkout/payment.html");
            sb.Append("?receiverEmail=" + ConfigurationManager.AppSettings["mailCobrancaPagSeguro"]);
            sb.Append("&currency=BRL");
            sb.Append("&shippingType=");

            //adicionando os itens
            int x = 0;
            foreach (ProdutoOT _produto in produtosCarrinho)
            {
                x++;
                sb.AppendFormat("&itemId{0}={1}", x, _produto.ID);
                sb.AppendFormat("&itemDescription{0}={1}", x, _produto.Titulo);
                sb.AppendFormat("&itemQuantity{0}={1}", x, _produto.QuantidadeCarrinho);
                sb.AppendFormat("&itemAmount{0}={1}", x, String.Format("{0:#,##0.00}", _produto.Preco).Replace(".", "").Replace(",", "."));
                sb.AppendFormat("&itemWeight{0}={1}", x, _produto.Peso);
            }

            return sb.ToString();
        }
        #endregion
        #endregion
    }
}
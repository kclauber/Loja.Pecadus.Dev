using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Resources;

namespace Loja.UI.Pecadus
{
    public partial class carrinhoCompras : System.Web.UI.Page
    {
        bool isSandbox = bool.Parse(ConfigurationManager.AppSettings["isSandbox"]);
        private bool freteGratis = false;
        public string sStatus, sToken = "";
        protected double valorTotalProdutos = 0;
        //private string TOKEN, KEY, URI, sURLRedirect;
        ProdutosOT produtosCarrinho = null;
        public string pesoCarrinho;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = String.Format("{0} - Carrinho de compras", ConfigurationManager.AppSettings["nomeSiteCompleto"]);

            if (!Page.IsPostBack)
            {
                Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
                AtualizaCarrinho();
            }            
        }
        private void AtualizaCarrinho()
        {
            //Atualiza a quantidade de itens no header da página
            ((Label)this.Page.Master.FindControl("lblQtdCarrinho")).Text = Carrinho.Instancia.ObterQuantidadeItens().ToString();

            if (Carrinho.Instancia.TemItens)
            {
                CarregaObjetoCarrinho();
                repCarrinho.DataSource = produtosCarrinho;
                repCarrinho.DataBind();
            }
            else
            {
                Carrinho.Instancia.Limpar();

                pnlCarrinho.Visible = false;
                pnlVazio.Visible = true;
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
        protected void repCarr_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item))
            {
                ProdutoOT produto = (ProdutoOT)e.Item.DataItem;

                string link = Utilitarios.CriaStringLinkProduto(produto.Categoria.TituloCategoriaPai,
                                                                produto.Categoria.Titulo,
                                                                produto.Titulo,
                                                                produto.Categoria.IDCategoriaPai,
                                                                produto.Categoria.ID,
                                                                produto.ID);

                ((LinkButton)e.Item.FindControl("lnkExcluir")).CommandArgument = produto.ID.ToString();

                //Tratamento das imagens
                Image imgProd = (Image)e.Item.FindControl("imgProduto");
                imgProd.AlternateText = produto.Titulo;
                imgProd.ToolTip = produto.Titulo;
                if (produto.Imagens.Count > 0)
                    imgProd.ImageUrl = @"ShowImage.aspx?img=" + produto.Imagens[0].Titulo;

                ((Label)e.Item.FindControl("lblNomeProduto")).Text = produto.Titulo;
                ((Label)e.Item.FindControl("lblCodProduto")).Text = String.Format("Cód. Ref.: {0:00000}", produto.ID);

                //Verifica se tem percentual de desconto e faz o calculo
                if (produto.Desconto > 0)
                    produto.Preco = produto.Preco - ((produto.Preco / 100) * produto.Desconto);
                ((Label)e.Item.FindControl("lblPrecoProduto")).Text = String.Format("{0:R$ #,##0.00}", produto.Preco);

                ((Label)e.Item.FindControl("lblPrecoTotalProduto")).Text = String.Format("{0:R$ #,##0.00}", produto.Preco * produto.QuantidadeCarrinho);

                DropDownList ddlQuantidade = (DropDownList)e.Item.FindControl("ddlQuantidade");
                for (int i = 1; i <= 5; i++)
                {
                    ddlQuantidade.Items.Add(i.ToString());
                    if (produto.QuantidadeCarrinho.Equals(i))
                        ddlQuantidade.Items[i-1].Selected = true;
                }

                //Somando o preco de todos os produtos para exibir no rodapé
                valorTotalProdutos += produto.Preco * produto.QuantidadeCarrinho;

                Carrinho.Instancia.PesoProdutos = Carrinho.Instancia.PesoProdutos + (produto.Peso * produto.QuantidadeCarrinho);
            }
            else if (e.Item.ItemType.Equals(ListItemType.Footer))
            {
                TextBox txtCepDestino = ((TextBox)e.Item.FindControl("txtCepDestino"));
                txtCepDestino.Attributes.Add("placeholder", "Informe seu CEP");

                if (!String.IsNullOrEmpty(Carrinho.Instancia.CepDestino))
                {
                    ((Panel)e.Item.FindControl("pnlFrete")).Visible = true;
                    txtCepDestino.Text = Utilitarios.FormatarCep(Carrinho.Instancia.CepDestino);
                    CalcularFrete(e);
                }   

                Carrinho.Instancia.ValorTotalProdutos = valorTotalProdutos;
                ((Label)e.Item.FindControl("lblPrecoTotalCompra")).Text = String.Format("{0:R$ #,##0.00}", Carrinho.Instancia.ValorTotalProdutos + Carrinho.Instancia.Frete.Valor);
            }
        }

        #region Botões carrinho
        public void RemoverItem(object sender, EventArgs e)
        {
            new Utilitarios().RemoverItem(Convert.ToInt32(((LinkButton)sender).CommandArgument));
            AtualizaCarrinho();
        }
        protected void ddlQuantidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            atualizarQuantidadeItens();
            AtualizaCarrinho();
        }
        
        /// <summary>
        /// Atualiza a quantidade de todos os itens do carrinho
        /// </summary>
        protected void atualizarQuantidadeItens()
        {
            foreach (RepeaterItem item in repCarrinho.Items)
            {
                //Pega o ID do link de excluir por que já esta configurado lá
                int id = Convert.ToInt32(((LinkButton)item.FindControl("lnkExcluir")).CommandArgument);
                int quantidade = Convert.ToInt32(((DropDownList)item.FindControl("ddlQuantidade")).SelectedValue);
                
                Carrinho.Instancia.AtualizarQuantidadeItem(id, quantidade);
            }
        }
        #endregion
        #region Calculo de Frete        
        public void btnCalcularFrete_OnClick(object sender, EventArgs e)
        {
            //Encontra o footer template do repeater para depois acessar seus controle
            Control footerTemplate = repCarrinho.Controls[repCarrinho.Controls.Count - 1].Controls[0];
            TextBox txtCepDestino = ((TextBox)footerTemplate.FindControl("txtCepDestino"));

            if (!String.IsNullOrEmpty(txtCepDestino.Text))
                Carrinho.Instancia.CepDestino = txtCepDestino.Text;

            AtualizaCarrinho();
        }
        public void CalcularFrete(RepeaterItemEventArgs e)
        {
#if DEBUG
            List<FreteOT> list = new List<FreteOT>
            {
                new FreteOT() { Tipo = "SEDEX", Valor = 35.00D, Prazo = 2 },
                new FreteOT() { Tipo = "PAC", Valor = 19.75D, Prazo = 7 }
            };
#else
            DataSet ds = ConsultarWSCorreios();
#endif


            ((RadioButton)e.Item.FindControl("rdFreteSedex")).Text = String.Format("{0} - {1:R$ #,##0.00} ({2} dias)",
                                                                                   list[0].Tipo,
                                                                                   list[0].Valor,
                                                                                   list[0].Prazo);
            ((RadioButton)e.Item.FindControl("rdFretePac")).Text = String.Format("{0} - {1:R$ #,##0.00} ({2} dias)",
                                                                                   list[1].Tipo,
                                                                                   list[1].Valor,
                                                                                   list[1].Prazo);
        }

        protected DataSet ConsultarWSCorreios()
        {
            DataSet ds = new DataSet();
            try
            {
                string urlRequest = String.Format(@"http://ws.correios.com.br/calculador/CalcPrecoPrazo.aspx?
                                                    nCdEmpresa=&sDsSenha=
                                                    &nCdServico={0},{1}
                                                    &sCepOrigem={2}&sCepDestino={3}&nVlPeso={4}&nCdFormato={5}
                                                    &nVlComprimento={6}&nVlAltura={7}&nVlLargura={8}&nVlDiametro=0
                                                    &sCdMaoPropria=n&nVlValorDeclarado=0&sCdAvisoRecebimento=n
                                                    &StrRetorno=xml&nIndicaCalculo={9}",
                                                    Carrinho.FormasEnvio["Sedex"],
                                                    Carrinho.FormasEnvio["PAC"],
                                                    Carrinho.CepOrigem,
                                                    Carrinho.Instancia.CepDestino,
                                                    Carrinho.Instancia.PesoProdutos,
                                                    Carrinho.PacotesEnvio.Caixa,
                                                    Carrinho.MedidasCaixa["Comprimento"],
                                                    Carrinho.MedidasCaixa["Altura"],
                                                    Carrinho.MedidasCaixa["Largura"],
                                                    Carrinho.TiposRetornoWSCorreios.PrecoPrazo);

                WebRequest request = WebRequest.Create(urlRequest);
                
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF7))
                    {
                        //Coloca os dados recebidos em um DataSet
                        ds.ReadXml(sr);
                    }
                }
            }
            catch (Exception ex)
            {
                new Utilitarios().TratarExcessao(ex, Request.Url.ToString(), "carrinhoCompras.CalcularFreteCorreios", this.Page);
            }

            return ds;
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
                atualizarQuantidadeItens();

                //if (rdlFrete.Visible == false || rdlFrete.SelectedIndex == -1)
                //{
                //    CalcularFrete();
                //    AdicionarValorFrete();
                //}

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

        [Obsolete("CalcularFrete() não funciona mais, usar CalcularFreteCorreios().")]
        protected void CalcularFreteOld()
        {
            //string cepDestino = txtCep1.Text + txtCep2.Text;
            //string peso = Session["PesoCarrinho"].ToString();
            //Session["CepDestino"] = cepDestino;

            try
            {
                //Formatando o peso
                //            if (peso.Length >= 4)
                //                peso = peso.Substring(0, 1) + "." + peso.Substring(1, peso.Length - 1);
                //            else
                //                peso = "0." + peso;

                //            //Cria uma requisição ao webService com os dados informados
                ////Serviço saiu do ar - trocar por outra alternativa URGENTE
                //rdlFrete.Items.Add(new ListItem(String.Format("PAC - R$ {0:#,##0.00} (promoção frete fixo p/ todo Brasil)", 10.00d),
                //                                            String.Format("EN{0:#,##0.00}", 10.00d)));
                //            rdlFrete.Items[0].Selected = true;
                //rdlFrete.Visible = true;

                //WebRequest request = WebRequest.Create(
                //        "http://frete.w21studio.com/calFrete.xml?cep=" + 02289010 +
                //        "&cod=4225&peso=" + 1 +
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
                //string status = ds.Tables["frete"].Rows[0]["status"].ToString();
                //if (!String.IsNullOrEmpty(status) && status.ToUpper().Equals("OK"))
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
            }
            catch (Exception ex)
            {
                new Utilitarios().TratarExcessao(ex, Request.Url.ToString(), "carrinho.CalculaFrete", this.Page);
            }
        }
#endregion
#endregion
    }
}
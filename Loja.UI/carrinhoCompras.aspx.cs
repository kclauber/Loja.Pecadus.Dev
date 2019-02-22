using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
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
        //private string TOKEN, KEY, URI, sURLRedirect;
        ProdutosOT produtosCarrinho = null;
        private double valorTotalProdutos = 0;
        private double ValorPesoProdutos = 0.01D;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = String.Format("{0} - Carrinho de compras", ConfigurationManager.AppSettings["nomeSiteCompleto"]);

            if (!Page.IsPostBack)
            {
                Response.Cache.SetExpires(DateTime.Now.AddDays(-1));

                //Se o cliente não for nulo, não tiver informado um CEP no carrinho antes e tiver um CEP cadastrado
                if (Cliente.Instancia != null && String.IsNullOrEmpty(Carrinho.Instancia.CepDestino) && !String.IsNullOrEmpty(Cliente.Instancia.CEP))
                    Carrinho.Instancia.CepDestino = Cliente.Instancia.CEP;
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

                //Somando o preco e peso de todos os produtos para exibir no rodapé e fazer o calculo de frete
                valorTotalProdutos += produto.Preco * produto.QuantidadeCarrinho;
                ValorPesoProdutos += produto.Peso * produto.QuantidadeCarrinho;

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
                    //try
                    //{
                    RadioButton rdSedex = (RadioButton)e.Item.FindControl("rdFreteSedex");
                    RadioButton rdPAC = (RadioButton)e.Item.FindControl("rdFretePac");

                    new Utilitarios().CalcularFrete(ref rdSedex, ref rdPAC);
                    //}
                    //catch (Exception ex)
                    //{
                    //    new Utilitarios().TratarExcessao(ex, Request.Url.ToString(), "carrinhoCompras.CalcularFrete", this.Page);
                    //}
                }   

                Carrinho.Instancia.ValorTotalProdutos = valorTotalProdutos;
                Carrinho.Instancia.PesoProdutos = ValorPesoProdutos;

                ((Label)e.Item.FindControl("lblPrecoTotalCompra")).Text = String.Format("{0:R$ #,##0.00}", Carrinho.Instancia.ValorTotalProdutos + Carrinho.Instancia.Frete.Valor);
            }
        }

        #region Botões carrinho
        public void RemoverItem(object sender, EventArgs e)
        {
            Carrinho.Instancia.RemoverItem(Convert.ToInt32(((LinkButton)sender).CommandArgument));
            AtualizaCarrinho();
        }
        protected void ddlQuantidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            atualizarQuantidadeItens();
            AtualizaCarrinho();
        }
        
        /// <summary>
        /// Atualiza a quantidade de todos os itens do carrinho
        /// (Criado desta maneira por causa do modelo antigo de carrinho de compras)
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
        
        protected void rdFrete_CheckedChanged(Object sender, EventArgs e)
        {
            //Recupera o valor do RadioButton e limpa para usar no objeto
            string frete = ((RadioButton)sender).Text;
            frete = frete.Replace(" - ", "")
                         .Replace("R$", "")
                         .Replace("(", "")
                         .Replace(")", "")
                         .Replace(" dias", "");
            string[] arrFrete = frete.Split(' ');

            Carrinho.Instancia.Frete = new FreteOT()
            {
                Tipo = arrFrete[0],
                Valor = Convert.ToDouble(arrFrete[1]),
                Prazo = Convert.ToInt32(arrFrete[2])
            };

            AtualizaCarrinho();
        }        
        #endregion
        #region Finalizar Compra
        #region ## Métodos com o framework PagSeguro ##
        protected void btnConcluirCompra_Click(object sender, EventArgs e)
        {
            if (Cliente.Instancia == null)
                Response.Redirect("/LoginCliente/?finalizarCompra=true");
            else
                Response.Redirect("/Cadastro/?finalizarCompra=true");
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
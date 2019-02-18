using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;

namespace Loja.UI.Pecadus.Admin
{
    public partial class GerEstoquePicking : System.Web.UI.Page
    {
        private int idPedido = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["idPedido"]))
                idPedido = Convert.ToInt32(Request.QueryString["idPedido"]);

            if (!Page.IsPostBack)
            {                
                if (criarDataSet(idPedido))
                    proximoItem(0);
            }
        }
        /// <summary>
        /// Passa os dados do objeto PedidoOT para um DataTable para facilitar a manipulação
        /// </summary>
        /// <param name="idPedido"></param>
        /// <returns></returns>
        private bool criarDataSet(int idPedido)
        {
            Cliente cliente = (Cliente)Session["cliente"];
            PedidoOT pedido = null;

            if (cliente == null)
                return false;

            //Seleciona o item correto dentro do objeto
            foreach (PedidoOT _ped in cliente.Pedidos)
            {
                if (_ped.ID == idPedido)
                {
                    pedido = _ped;
                    break;
                }
            }

            if (pedido == null)
                return false;

            DataTable dtItens = new DataTable();
            dtItens.Columns.Add("numItem");
            dtItens.Columns.Add("id");
            dtItens.Columns.Add("Titulo");
            dtItens.Columns.Add("EAN");
            dtItens.Columns.Add("Quantidade");
            dtItens.Columns.Add("QuantidadeSeparada");
            dtItens.Columns.Add("estoque");

            int a = -1;
            foreach (ProdutoOT prod in pedido.Produtos)
            {
                a++;
                DataRow dr = dtItens.NewRow();
                dr["numItem"] = a;
                dr["id"] = prod.ID.ToString();
                dr["Titulo"] = prod.Titulo;
                dr["EAN"] = (!String.IsNullOrEmpty(prod.EAN) ? prod.EAN : " -- ");
                dr["Quantidade"] = prod.QuantidadeCarrinho.ToString();
                dr["QuantidadeSeparada"] = 0;
                dr["estoque"] = prod.Estoque.ToString();
                dtItens.Rows.Add(dr);
            }

            if (dtItens.Rows.Count > 0)
                ViewState["dtItens"] = dtItens;

            return true;
        }
        private void proximoItem(int itemAtual)
        {
            if (ViewState["dtItens"] != null)
            {
                DataTable dtItens = (DataTable)ViewState["dtItens"];
                DataRow dr = null;

                if (dtItens != null && dtItens.Rows.Count > 0)
                {
                    //Ainda existem itens não separados
                    if (continuarSeparando(dtItens))
                    {
                        if (itemAtual >= dtItens.Rows.Count)
                        {
                            itemAtual = 0;
                            ViewState["itemAtual"] = 0;
                        }

                        foreach (DataRow _dr in dtItens.Rows)
                        {
                            if (Convert.ToInt32(_dr["numItem"]) == itemAtual)
                            {
                                //Verificando a quandidade de itens separados para este ID                            
                                if (Convert.ToInt32(_dr["QuantidadeSeparada"]) < Convert.ToInt32(_dr["Quantidade"]))
                                {
                                    dr = _dr;
                                    break;
                                }
                                else
                                {
                                    //Se já foram separados todos, segue para o próximo item
                                    itemAtual++;
                                    ViewState["itemAtual"] = itemAtual;
                                    proximoItem(itemAtual);
                                }
                            }
                        }
                    }
                    //Acabaram os itens a separar
                    else
                    {
                        pnlSeparacao.Visible = false;
                        pnlConfirmacao.Visible = true;
                    }
                }

                exibeInformacoes(dr);
            }
        }
        private void exibeInformacoes(DataRow dr)
        {
            if (dr != null)
            {
                lblIDProduto.Text = dr["ID"].ToString();
                lblTitulo.Text = dr["Titulo"].ToString();
                lblQtd.Text = dr["Quantidade"].ToString();
                lblQtdSeparada.Text = dr["QuantidadeSeparada"].ToString();
                lblEan.Text = dr["EAN"].ToString();
                lblEstoque.Text = dr["estoque"].ToString();
            }
        }
        /// <summary>
        /// Verifica se existe algum item com quantidade em aberto para separação
        /// </summary>
        /// <returns></returns>
        private bool continuarSeparando(DataTable dtItens)
        {
            foreach (DataRow _dr in dtItens.Rows)
            {
                if (Convert.ToInt32(_dr["QuantidadeSeparada"]) < Convert.ToInt32(_dr["Quantidade"]))
                {
                    return true;
                }
            }
            return false;
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (txtEan.Text.Equals(lblEan.Text) || (lblEan.Text.Equals(" -- ") && txtEan.Text.Equals(lblIDProduto.Text)))
            {
                DataTable dtItens = (DataTable)ViewState["dtItens"];
                int itemAtual = Convert.ToInt32(ViewState["itemAtual"]);

                if (dtItens != null && dtItens.Rows.Count > 0)
                {
                    foreach (DataRow _dr in dtItens.Rows)
                    {
                        if (Convert.ToInt32(_dr["numItem"]) == itemAtual)
                        {
                            _dr["QuantidadeSeparada"] = Convert.ToInt32(_dr["QuantidadeSeparada"]) + 1;
                            if (Convert.ToInt32(_dr["QuantidadeSeparada"]) == Convert.ToInt32(_dr["Quantidade"]))
                            {
                                itemAtual++;
                                ViewState["itemAtual"] = itemAtual;
                            }
                            break;
                        }
                    }
                }
                ViewState["dtItens"] = dtItens;
                txtEan.Text = "";
                proximoItem(itemAtual);
            }
            else
            {
                Utilitarios.ShowMessageBox(this.Page, "Cód. de barras diferente do esperado!\\nSe não existir ou for ilegível utilize o ID.");
                return;
            }

        }
        protected void btnPular_Click(object sender, EventArgs e)
        {
            int itemAtual = Convert.ToInt32(ViewState["itemAtual"]);
            itemAtual++;

            ViewState["itemAtual"] = itemAtual;
            txtEan.Text = "";

            proximoItem(itemAtual);
        }
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!chkEmbalagem.Checked)
            {
                Utilitarios.ShowMessageBox(this.Page, "Confirme que os produtos foram embalados.");
                return;
            }

            if (!chkEnderecar.Checked)
            {
                Utilitarios.ShowMessageBox(this.Page, "Confirme que a embalagem foi endereçada.");
                return;
            }

            try
            {
                PedidosOP pedidosOP = new PedidosOP();
                PedidoOT pedido = new PedidoOT() { ID = idPedido };
                pedidosOP.SelectPedidoByID(ref pedido);
                if (pedido != null)
                {
                    pedido.Status = "Enviado";
                    pedido.Anotacao = String.Format("Enviado dia {0:dd/MM/yyyy} {1}",
                                                     DateTime.Today,
                                                     (!txtCodigoEnvio.Text.Equals("") ? " - Código de Envio: " + txtCodigoEnvio.Text : ""));
                    pedidosOP.UpdatePedido(pedido);

                    Cliente cliente = new Cliente(){ID = pedido.IdCliente};
                    new ClientesOP().SelectCliente(ref cliente);

                    string detalhesProdutos = "";
                    double valorTotal = 0;
                    foreach(ProdutoOT prod in pedido.Produtos)
                    {
                        //Atualizar o status do pedido
                        new ProdutosOP().AtualizaEstoque(prod.ID, (prod.QuantidadeCarrinho * -1));

                        detalhesProdutos += 
                            String.Format(@"Ref.: {0:00000} - {1}<br/>
                                            Quantidade: {2}<br/><br/>",
                                            prod.ID,
                                            prod.Titulo,
                                            prod.QuantidadeCarrinho);

                        valorTotal += (prod.Preco * prod.QuantidadeCarrinho);
                    }
                    valorTotal += pedido.ValorFrete;
                    pedido.ValorTotal = valorTotal;

                    string mensagem = String.Format(@"<p>Olá {0},
                                                        <br/><br/>
                                                        Obrigado por comprar na {1}, tenha certeza que você comprou produtos de qualidade.
                                                        <br/><br/>
                                                        Informamos que seu pedido já foi separado para envio e o mesmo será 
                                                        despachado no prazo máximo de 48 horas.<br/>
                                                        Após este período começará a correr o prazo de entrega dos correios 
                                                        (2 a 3 dias úteis para Sedex e 3 a 5 dias úteis para PAC).<br/>
                                                        {2}
                                                        <br/>
                                                        Mais uma vez agradecemos sua compra e esperamos realizar bons negócios juntos no futuro.
                                                        <br/><br/>
                                                        <b>Seguem abaixo detalhes do seu pedido:</b><br/>
                                                        Quantidade de itens: {3}<br/>
                                                        Valor total do pedido: {4}<br/>
                                                        Forma de envio: {5}
                                                        <br/><br/>
                                                        <b>Detalhes dos produtos:</b><br/>
                                                        {6}
                                                        <br/>
                                                        <hr/>
                                                        Este é um e-mail automático, por favor não o responda.<br/>
                                                        Em caso de dúvida por favor entre em contato com nossa central pelo e-mail {7}.
                                                        </p>",
                                                        cliente.Nome,
                                                        ConfigurationManager.AppSettings["nomeSiteCompleto"],
                                                        (!txtCodigoEnvio.Text.Equals("") ? String.Format("Este é seu código de rastreio: {0}.<br/>", txtCodigoEnvio.Text) : ""),
                                                        pedido.Produtos.Count,
                                                        String.Format("{0:R$ #,##0.00}", pedido.ValorTotal),
                                                        (pedido.TipoFrete.Equals("SD") ? "Sedex" : "PAC"),
                                                        detalhesProdutos,
                                                        ConfigurationManager.AppSettings["mailPrincipal"]);

                    //new Utilitarios().EnviarEmail()
                }

                lblMsgRetorno.Text = "Pedido atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                string msgerr = "Houve um erro de processamento!\\nUm e-mail com o erro foi enviado ao admin.";
                lblMsgRetorno.Text = msgerr.Replace("\\n", "<br>");

                Utilitarios.ShowMessageBox(this.Page, msgerr);
                new Utilitarios().TratarExcessao(ex, Request.Url.ToString(), "btnConfirmar_Click", this.Page);
            }

            pnlConfirmacao.Visible = false;
            pnlRetorno.Visible = true;
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GerEstoqueSaida.aspx");
        }
    }
}
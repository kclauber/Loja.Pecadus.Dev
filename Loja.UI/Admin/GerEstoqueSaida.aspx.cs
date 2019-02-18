using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Objeto;
using Loja.Persistencia;

namespace Loja.UI.Pecadus.Admin
{
    public partial class GerEstoqueSaida : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtDataDe.Text = DateTime.Now.AddDays(-14).ToString("dd/MM/yyyy");
                txtDataAte.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

            pedidoDS.ConnectionString = ConfigurationManager.ConnectionStrings["conPadrao"].ConnectionString;
            pedidoDS.ProviderName = "System.Data.Odbc";
            pedidoDS.SelectCommand = new PedidosOP().getStringSqlPedidos(ddlStatus.SelectedValue, Convert.ToDateTime(txtDataDe.Text), Convert.ToDateTime(txtDataAte.Text));
            GridPedidos.DataBind();
        }
        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            pedidoDS.SelectCommand = new PedidosOP().getStringSqlPedidos(ddlStatus.SelectedValue, Convert.ToDateTime(txtDataDe.Text), Convert.ToDateTime(txtDataAte.Text));
            GridPedidos.DataBind();

            limparDados();
        }

        protected void GridPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer && e.Row.RowType != DataControlRowType.Pager)
                {
                    LinkButton sel = (LinkButton)e.Row.FindControl("lnkSelect");
                    sel.CommandName = "Selecionar";
                    sel.CommandArgument = DataBinder.Eval(e.Row.DataItem, "idCliente").ToString();
                    sel.CausesValidation = false;

                    ((Label)e.Row.FindControl("lblDtCadastro")).Text = String.Format("{0:dd/MM/yy}", DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "dtCadastro").ToString()));
                }
            }
        }
        protected void GridPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Selecionar")
            {
                Cliente cliente = new Cliente() { ID = Convert.ToInt32(e.CommandArgument.ToString()) };
                new PedidosOP().SelectPedidoCliente(ref cliente);
                Session["cliente"] = cliente;

                if (cliente != null)
                {
                    lblIDCliente.Text = cliente.ID.ToString();
                    lblNome.Text = cliente.Nome.ToString().ToUpper();
                    lblEndereco.Text = cliente.Endereco.ToString().ToUpper();
                    lblCidadeEstadoCep.Text = cliente.Cidade.ToString().ToUpper() + " / " +
                                              cliente.Estado.ToString().ToUpper() + " / " +
                                              cliente.CEP.ToString();
                    lblEmail.Text = cliente.Email.ToString().ToUpper();
                    lblDtCadastroCliente.Text = cliente.DtCadastro.ToString("dd/MM/yyyy");

                    repPedidos.DataSource = cliente.Pedidos;
                    repPedidos.DataBind();
                }
            }
        }

        protected void repPedidos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                Label lblAux;
                PedidoOT pedido = (PedidoOT)e.Item.DataItem;

                lblAux = (Label)e.Item.FindControl("lblIDPedido");
                lblAux.Text = pedido.ID.ToString();

                lblAux = (Label)e.Item.FindControl("lblTransacaoID");
                lblAux.Text = pedido.TransacaoID.ToString();

                lblAux = (Label)e.Item.FindControl("lblFrete");
                lblAux.Text = String.Format("{0} - {1:R$ #,##0.00}",
                              (pedido.TipoFrete.Equals("SD") ? "Sedex" : "PAC"),
                              pedido.ValorFrete);

                lblAux = (Label)e.Item.FindControl("lblAnotacao");
                lblAux.Text = pedido.Anotacao.ToString();

                lblAux = (Label)e.Item.FindControl("lblPagamento");
                lblAux.Text = pedido.TipoPagamento.ToString() + " " +
                              String.Format("({0}x)", pedido.Parcelas.ToString());

                lblAux = (Label)e.Item.FindControl("lblStatus");
                lblAux.Text = pedido.Status.ToString();

                lblAux = (Label)e.Item.FindControl("lblExtras");
                lblAux.Text = String.Format("{0:R$ #,##0.00}", pedido.Extras);

                lblAux = (Label)e.Item.FindControl("lblDtCadastroPedido");
                lblAux.Text = pedido.DtCadastro.ToString("dd/MM/yyyy H:mm:ss");

                if (pedido.Status.ToUpper().Equals("APROVADO"))
                {
                    Button btnAux = (Button)e.Item.FindControl("btnPicking");
                    btnAux.Visible = true;
                    btnAux.CommandArgument = pedido.ID.ToString();
                }

                if (pedido.Produtos.Count > 0)
                {
                    Repeater repAux = (Repeater)e.Item.FindControl("repItens");
                    repAux.DataSource = pedido.Produtos;
                    repAux.DataBind();
                }
                else
                {
                    Panel pnlAux = (Panel)e.Item.FindControl("pnlSemRegistro");
                    pnlAux.Visible = true;
                }
            }
        }
        protected void repItens_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                Label lblAux;
                ProdutoOT item = (ProdutoOT)e.Item.DataItem;

                lblAux = (Label)e.Item.FindControl("lblIDProduto");
                lblAux.Text = item.ID.ToString();

                lblAux = (Label)e.Item.FindControl("lblEan");
                lblAux.Text = item.EAN.ToString();

                lblAux = (Label)e.Item.FindControl("lblTitulo");
                lblAux.Text = item.Titulo.ToString();

                lblAux = (Label)e.Item.FindControl("lblQtdProduto");
                lblAux.Text = item.QuantidadeCarrinho.ToString();

                lblAux = (Label)e.Item.FindControl("lblValorProduto");
                lblAux.Text = String.Format("{0:R$ #,##0.00}", item.Preco);
            }
        }

        protected void btnPicking_Click(object sender, CommandEventArgs e)
        {
            Response.Redirect("GerEstoquePicking.aspx?idPedido=" + e.CommandArgument);
        }

        private void limparDados()
        {
            lblIDCliente.Text = "";
            lblNome.Text = "";
            lblEndereco.Text = "";
            lblCidadeEstadoCep.Text = "";
            lblEmail.Text = "";
            lblDtCadastroCliente.Text = "";

            repPedidos.DataSource = null;
            repPedidos.DataBind();
        }
    }
}
using Loja.Objeto;
using System;
using System.Web.UI;

public partial class loginCliente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Esconde alguns elementos da página para manter o foco do cliente
        //((Panel)this.Page.Master.FindControl("pnlBusca")).Visible = false;
        //((Panel)this.Page.Master.FindControl("pnlMenu")).Visible = false;
        if (!Page.IsPostBack)
        {
            if (Cliente.Instancia != null)
            {
                Response.Redirect("/Cadastro/?finalizarCompra=true");
            }
        }
    }
}
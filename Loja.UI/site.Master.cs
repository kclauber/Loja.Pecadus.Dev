using Loja.Objeto;
using Loja.Util;
using System;
using System.Web.UI;

namespace Loja.UI.Pecadus
{
    public partial class site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Código de sessão do usuário
            if (Session["idUser"] == null)
                Session["idUser"] = DateTime.Now.ToString("yyMMddHmmss");
            /////////////////////////////            

            if (!Page.IsPostBack)
            {
                //Tentativa de mostrar a página para os Crawlers sem o Viewstate gigante
                try
                {
                    if (Request.UserAgent.ToLower().Contains("googlebot") || Request.Browser.Crawler)
                        Page.EnableViewState = false;
                }
                catch { } //Se der erro não faz nada

                //Atualiza a quantidade de itens no header da página
                lblQtdCarrinho.Text = Carrinho.Instancia.ObterQuantidadeItens().ToString();

                //Exibe ou esconde os links para login e logoff
                if (Cliente.Instancia == null)
                {
                    pnlLogin.Visible = true;
                    pnlLogoff.Visible = false;
                }
                else
                {
                    lblNomeCliente.Text = Cliente.Instancia.Nome;

                    pnlLogin.Visible = false;
                    pnlLogoff.Visible = true;
                }
            }
        }
    }
}
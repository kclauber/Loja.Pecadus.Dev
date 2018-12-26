using System;
using Loja.Persistencia;
using Loja.Util;

namespace Loja.UI.Pecadus.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["loginAdmin"] != null)
                {
                    Response.Redirect("PainelControle.aspx");
                }
            }
        }
        protected void btnLogar_Click(object sender, EventArgs e)
        {
            if (new UsuariosOP().Login(Utilitarios.TiraAcentos(txtUsuario.Text),
                                       Utilitarios.TiraAcentos(txtSenha.Text)))
            {
                Session["loginAdmin"] = txtUsuario.Text;
                Response.Redirect("PainelControle.aspx");
            }
            else
            {
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                lblMensagem.Text = "Erro: usuário e senha inválidos!";
            }
        }
    }
}
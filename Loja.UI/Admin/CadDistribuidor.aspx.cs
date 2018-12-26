using System;
using System.Configuration;
using System.Web.UI.WebControls;
using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;
using System.Web.UI;

namespace Loja.UI.Pecadus.Admin
{
    public partial class CadDistribuidor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            distDS.ConnectionString = ConfigurationManager.ConnectionStrings["conPadrao"].ConnectionString;
            distDS.ProviderName = "System.Data.Odbc";
            distDS.SelectCommand = new DistribuidoresOP().getSqlAdmin();
        }
        public void limparCampos()
        {
            txtNome.Text = "";
            txtSite.Text = "";
            txtEmail.Text = "";
            txtTelefone.Text = "";
            txtObservacao.Text = "";

            pnlGrid.Visible = true;
            pnlCadastro.Visible = false;

            btnCadastrar.Visible = true;
            btnAtualizar.Visible = false;

            GridDistribuidor.DataBind();
        }

        #region -- Grid --
        protected void GridDistribuidor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                DistribuidorOT _distribuidor = new DistribuidorOT() { ID = Convert.ToInt32(e.CommandArgument.ToString()) };
                new DistribuidoresOP().SelectDistribuidor(ref _distribuidor);
                if (_distribuidor != null)
                {
                    lblID.Text = _distribuidor.ID.ToString();
                    txtNome.Text = _distribuidor.Nome;
                    txtSite.Text = _distribuidor.Site;
                    txtEmail.Text = _distribuidor.EMail;
                    txtTelefone.Text = _distribuidor.Telefone;
                    txtObservacao.Text = _distribuidor.Observacao;

                    pnlGrid.Visible = false;
                    pnlCadastro.Visible = true;

                    btnCadastrar.Visible = false;
                    btnAtualizar.Visible = true;
                }
            }
        }
        protected void GridDistribuidor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer && e.Row.RowType != DataControlRowType.Pager)
                {
                    LinkButton edit = (LinkButton)e.Row.FindControl("lnkEdit");
                    edit.CommandName = "Editar";
                    edit.CommandArgument = DataBinder.Eval(e.Row.DataItem, "id").ToString();
                    edit.CausesValidation = false;
                }
            }
        }
        #endregion
        protected void btnCadastroNovo_Click(object sender, EventArgs e)
        {
            pnlGrid.Visible = false;
            pnlCadastro.Visible = true;

            btnCadastrar.Visible = true;
            btnAtualizar.Visible = false;
        }
        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            DistribuidorOT distribuidor = new DistribuidorOT();

            distribuidor.Nome = txtNome.Text;
            distribuidor.Site = txtSite.Text;
            distribuidor.EMail = txtEmail.Text;
            distribuidor.Telefone = txtTelefone.Text;
            distribuidor.Observacao = txtObservacao.Text;

            new DistribuidoresOP().InsertDistribuidor(distribuidor);
            limparCampos();
        }
        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            DistribuidorOT distribuidor = new DistribuidorOT();

            distribuidor.ID = Convert.ToInt32(lblID.Text);
            distribuidor.Nome = txtNome.Text;
            distribuidor.Site = txtSite.Text;
            distribuidor.EMail = txtEmail.Text;
            distribuidor.Telefone = txtTelefone.Text;
            distribuidor.Observacao = txtObservacao.Text;

            new DistribuidoresOP().UpdateDistribuidor(distribuidor);
            limparCampos();
        }
}
}
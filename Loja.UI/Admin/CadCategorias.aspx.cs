using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Persistencia;
using Loja.Objeto;
using Loja.Util;

namespace Loja.UI.Pecadus.Admin
{
    public partial class CadCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            categoriasDS.ConnectionString = ConfigurationManager.ConnectionStrings["conPadrao"].ConnectionString;
            categoriasDS.ProviderName = "System.Data.Odbc";
            categoriasDS.SelectCommand = new CategoriasOP().getSqlAdmin();
        }
        public void carregaCategorias(int idCategoriaPaiSelecionada)
        {
            ddlCategPai.Items.Clear();
            if (idCategoriaPaiSelecionada > -1)
                ddlCategPai.DataSource = new CategoriasOP().SelectCategoriasPai(idCategoriaPaiSelecionada);
            else
                ddlCategPai.DataSource = new CategoriasOP().SelectCategoriasPai();
            ddlCategPai.DataTextField = "titulo";
            ddlCategPai.DataValueField = "id";
            ddlCategPai.DataBind();
            ddlCategPai.Items.Insert(0, new ListItem("-- Selecione --", ""));
        }
        public void limparCampos()
        {
            txtTitulo.Text = "";
            txtPalavrasChave.Text = "";

            pnlGrid.Visible = true;
            pnlCadastro.Visible = false;

            GridCategorias.DataBind();
        }

        #region -- Grid --
        protected void GridCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                CategoriaOT _categoria = new CategoriaOT() { ID = Convert.ToInt32(e.CommandArgument.ToString()) };
                new CategoriasOP().SelectCategoria(ref _categoria);
                if (_categoria != null)
                {
                    lblID.Text = _categoria.ID.ToString();
                    txtTitulo.Text = _categoria.Titulo;
                    txtPalavrasChave.Text = _categoria.PalavrasChave;
                    chkAtivo.Checked = _categoria.Ativo;

                    carregaCategorias(_categoria.ID);
                    
                    if (_categoria.IDCategoriaPai > -1)
                        ddlCategPai.SelectedValue = _categoria.IDCategoriaPai.ToString();
                    
                    pnlGrid.Visible = false;
                    pnlCadastro.Visible = true;

                    btnCadastrar.Visible = false;
                    btnAtualizar.Visible = true;                    
                }
            }
        }
        protected void GridCategorias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer && e.Row.RowType != DataControlRowType.Pager)
                {
                    LinkButton edit = (LinkButton)e.Row.FindControl("lnkEdit");
                    edit.CommandName = "Editar";
                    edit.CommandArgument = DataBinder.Eval(e.Row.DataItem, "id").ToString();
                    edit.CausesValidation = false;

                    Label lblAux = (Label)e.Row.FindControl("lblAtivo");
                    lblAux.Text = (DataBinder.Eval(e.Row.DataItem, "ativo").ToString().Equals("1") ? "Sim" : "Não");
                }
            }
        }
        #endregion
        protected void btnCadastroNovo_Click(object sender, EventArgs e)
        {
            pnlGrid.Visible = false;
            pnlCadastro.Visible = true;

            carregaCategorias(-1);
        }
        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            CategoriaOT categoria = new CategoriaOT();

            if (ddlCategPai.SelectedValue != "")
                categoria.IDCategoriaPai = Convert.ToInt32(ddlCategPai.SelectedValue);
            categoria.Titulo = txtTitulo.Text;
            categoria.PalavrasChave = txtPalavrasChave.Text;
            categoria.Ativo = chkAtivo.Checked;

            new CategoriasOP().InsertCategoria(categoria);
            limparCampos();
        }
        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            CategoriaOT categoria = new CategoriaOT();

            categoria.ID = Convert.ToInt32(lblID.Text);
            if (ddlCategPai.SelectedValue != "")
                categoria.IDCategoriaPai = Convert.ToInt32(ddlCategPai.SelectedValue);
            categoria.Titulo = txtTitulo.Text;
            categoria.PalavrasChave = txtPalavrasChave.Text;
            categoria.Ativo = chkAtivo.Checked;

            new CategoriasOP().UpdateCategoria(categoria);
            limparCampos();
        }
}
}
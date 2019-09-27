using System;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
//using CKEditor.NET;
using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;

namespace Loja.UI.Pecadus.Admin
{
    public partial class CadProdutos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            prodDS.ConnectionString = ConfigurationManager.ConnectionStrings["conPadrao"].ConnectionString;
            prodDS.ProviderName = "System.Data.Odbc";
            prodDS.SelectCommand = new ProdutosOP().getSqlAdmin();

            //txtDescricaoCompleta.ToolbarStartupExpanded = false;
            //txtDescricaoCompleta.config.keystrokes = new object[] 
            //{ 
            //    new object[] { CKEditorConfig.CKEDITOR_ALT + 121 /*F10*/, "toolbarFocus" },
            //    new object[] { CKEditorConfig.CKEDITOR_ALT + 122 /*F11*/, "elementsPathFocus" },
            //    new object[] { CKEditorConfig.CKEDITOR_SHIFT + 121 /*F10*/, "contextMenu" },
            //    new object[] { CKEditorConfig.CKEDITOR_CTRL + 90 /*Z*/, "undo" },
            //    new object[] { CKEditorConfig.CKEDITOR_CTRL + 89 /*Y*/, "redo" },
            //    new object[] { CKEditorConfig.CKEDITOR_CTRL + CKEditorConfig.CKEDITOR_SHIFT + 90 /*Z*/, "redo" },
            //    new object[] { CKEditorConfig.CKEDITOR_CTRL + 76 /*L*/, "link" },
            //    new object[] { CKEditorConfig.CKEDITOR_CTRL + 66 /*B*/, "bold" },
            //    new object[] { CKEditorConfig.CKEDITOR_CTRL + 73 /*I*/, "italic" },
            //    new object[] { CKEditorConfig.CKEDITOR_CTRL + 85 /*U*/, "underline" },
            //    new object[] { CKEditorConfig.CKEDITOR_ALT + 109 /*-*/, "toolbarCollapse" }
            //};

            if (!Page.IsPostBack)
            {
                txtFretePCU.Value = ConfigurationManager.AppSettings["fretePCU"].ToString();
                txtImposto.Value = ConfigurationManager.AppSettings["imposto"].ToString();
                txtTxOperadora.Value = ConfigurationManager.AppSettings["txOperadora"].ToString();
                txtCustoFixPeriodo.Value = ConfigurationManager.AppSettings["custoFixPeriodo"].ToString();
                txtCustoVarPeriodo.Value = ConfigurationManager.AppSettings["custoVarPeriodo"].ToString();
                txtPerdaPeriodo.Value = ConfigurationManager.AppSettings["perdaPeriodo"].ToString();
                txtVendaPeriodo.Value = CalcularVendaPeriodo();
            }
        }

        private string CalcularVendaPeriodo()
        {
            //TODO: Puxar o valor da base de dados
            return "6000.00";
        }
        #region -- Combos --
        public void carregaDistribuidor()
        {
            ddlDistribuidor.Items.Clear();
            ddlDistribuidor.DataSource = new DistribuidoresOP().SelectDistribuidores();
            ddlDistribuidor.DataTextField = "nome";
            ddlDistribuidor.DataValueField = "id";
            ddlDistribuidor.DataBind();
            ddlDistribuidor.Items.Insert(0, new ListItem("--Selecione--", ""));
        }
        public void carregaCategoriasPai()
        {
            ddlCategoriaPai.Items.Clear();
            ddlCategoriaPai.DataSource = new CategoriasOP().SelectCategoriasPai(-1);
            ddlCategoriaPai.DataTextField = "titulo";
            ddlCategoriaPai.DataValueField = "id";
            ddlCategoriaPai.DataBind();
            ddlCategoriaPai.Items.Insert(0, new ListItem("--Selecione--", ""));
        }
        public void carregaCategorias(int IDCategoriaPai)
        {
            ddlCategoria.Items.Clear();
            ddlCategoria.DataSource = new CategoriasOP().SelectCategorias(IDCategoriaPai); ;
            ddlCategoria.DataTextField = "titulo";
            ddlCategoria.DataValueField = "id";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("--Selecione--", ""));
        }
        protected void ddlCategoriaPai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategoria.Items.Clear();
            if (ddlCategoriaPai.SelectedIndex > 0)
                carregaCategorias(Convert.ToInt32(ddlCategoriaPai.SelectedValue));
        }
        #endregion
        #region -- Grids --
        protected void GridProdutos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                ProdutoOT _produto = new ProdutosOP().SelectProduto(Convert.ToInt32(e.CommandArgument.ToString()), 0, 10);
                if (_produto != null)
                {
                    lblID.Text = _produto.ID.ToString();
                    txtEan.Text = _produto.EAN;
                    txtTitulo.Text = _produto.Titulo;
                    txtDescricaoCurta.Text = _produto.DescricaoCurta;
                    //txtDescricaoCompleta.Text = _produto.DescricaoCompleta;
                    txtPalavrasChave.Text = _produto.PalavrasChave;
                    txtObservacao.Text = _produto.Observacao;
                    
                    txtPrecoCusto.Value = String.Format("{0:#,##0.00}", _produto.PrecoCusto);
                    txtDesconto.Text = _produto.Desconto.ToString();
                    txtPreco.Value = String.Format("{0:#,##0.00}", _produto.Preco);
                    txtMKP.Value = String.Format("{0:#,##0.00}", _produto.MarkUp); ;
                    txtFrete.Text = String.Format("{0:#,##0.00}", _produto.Frete);
                    txtPeso.Text = _produto.Peso.ToString();
                    txtEstoque.Text = _produto.Estoque.ToString();

                    txtDtCadastro.Text = _produto.DtCadastro.ToString("dd/MM/yyyy");
                    chkAtivo.Checked = _produto.Ativo;
                    chkDestaque.Checked = _produto.Destaque;
                    chkExibirHome.Checked = _produto.ExibirHome;
                    btnImg.Enabled = true;

                    //Carregando imagens e video cadastrados
                    carregaImagens();
                    carregaVideo(_produto.Videos);

                    carregaDistribuidor();
                    for (int a = 0; a < ddlDistribuidor.Items.Count; a++)
                    {
                        if (ddlDistribuidor.Items[a].Value.Equals(_produto.Distribuidor.ID.ToString()))
                            ddlDistribuidor.SelectedIndex = a;
                    }

                    //Carregando categorias
                    carregaCategoriasPai();
                    for (int a = 0; a < ddlCategoriaPai.Items.Count; a++)
                    {
                        if (ddlCategoriaPai.Items[a].Value.Equals(_produto.Categoria.IDCategoriaPai.ToString()))
                            ddlCategoriaPai.SelectedIndex = a;
                    }
                    carregaCategorias(Convert.ToInt32(ddlCategoriaPai.SelectedValue));
                    for (int a = 0; a < ddlCategoria.Items.Count; a++)
                    {
                        if (ddlCategoria.Items[a].Value.Equals(_produto.Categoria.ID.ToString()))
                            ddlCategoria.SelectedIndex = a;
                    }                    

                    pnlGrid.Visible = false;
                    pnlCadastro.Visible = true;

                    btnCadastrar.Visible = false;
                    btnAtualizar.Visible = true;
                }
            }
        }
        protected void GridProdutos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer && e.Row.RowType != DataControlRowType.Pager)
                {
                    LinkButton edit = (LinkButton)e.Row.FindControl("lnkEdit");
                    edit.CommandName = "Editar";
                    edit.CommandArgument = DataBinder.Eval(e.Row.DataItem, "id").ToString();
                    edit.CausesValidation = false;
                    ((Label)e.Row.FindControl("lblPreco")).Text = String.Format("{0:R$ #,##0.00}", double.Parse(DataBinder.Eval(e.Row.DataItem, "preco").ToString()));
                    ((Label)e.Row.FindControl("lblAtivo")).Text = (DataBinder.Eval(e.Row.DataItem, "ativo").ToString().Equals("1") ? "Sim" : "Não");
                }
            }
        }
        protected void grdImgCad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.IndexOf("Delete") > -1)
            {
                new ProdutosOP().DeleteImagem(Convert.ToInt32(e.CommandArgument));
                string fileName = e.CommandName.Replace("Delete", "");
                string imagensProdutos = Server.MapPath("imagensProdutos").Replace("\\admin", "");
                if (File.Exists(imagensProdutos + "\\" + fileName))
                    File.Delete(imagensProdutos + "\\" + fileName);

                carregaImagens();
            }
        }
        protected void grdImgCad_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer && e.Row.RowType != DataControlRowType.Pager)
                {
                    LinkButton del = (LinkButton)e.Row.FindControl("lnkDelete");
                    del.CommandArgument = DataBinder.Eval(e.Row.DataItem, "id").ToString();
                    del.CommandName = "Delete" + DataBinder.Eval(e.Row.DataItem, "titulo").ToString();
                }
            }
        }
        protected void grdVidCad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.IndexOf("Delete") > -1)
            {
                new ProdutosOP().DeleteVideo(Convert.ToInt32(e.CommandArgument));
                string fileName = e.CommandName.Replace("Delete", "");
                string videosProdutos = Server.MapPath("videosProdutos").Replace("\\admin", "");
                if (File.Exists(videosProdutos + "\\" + fileName))
                    File.Delete(videosProdutos + "\\" + fileName);

                //carregaVideo(0, "");
            }
        }
        protected void grdVidCad_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer && e.Row.RowType != DataControlRowType.Pager)
                {
                    LinkButton del = (LinkButton)e.Row.FindControl("lnkDelete");
                    del.CommandArgument = DataBinder.Eval(e.Row.DataItem, "id").ToString();
                    del.CommandName = "Delete" + DataBinder.Eval(e.Row.DataItem, "titulo").ToString();
                }
            }
        }
        #endregion
        #region -- Botoes --
        protected void btnImg_Click(object sender, EventArgs e)
        {
            if (lblID.Text != "")
            {
                salvaImagem(Convert.ToInt32(lblID.Text));
                salvaVideo(Convert.ToInt32(lblID.Text));
            }
        }
        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            bool? temEstoque = null;
            if (Convert.ToInt32(ddlEstoque.SelectedValue) > 0)
                temEstoque = (ddlEstoque.SelectedValue.Equals("1") ? true : false);
            bool? ativo = null;
            if (Convert.ToInt32(ddlAtivo.SelectedValue) > 0)
                ativo = (ddlAtivo.SelectedValue.Equals("1") ? true : false);

            prodDS.SelectCommand = new ProdutosOP().getSqlAdmin(txtBusca.Text, temEstoque, ativo);
            GridProdutos.DataBind();
        }
        protected void btnCadastroNovo_Click(object sender, EventArgs e)
        {
            pnlGrid.Visible = false;
            pnlCadastro.Visible = true;

            carregaDistribuidor();
            carregaCategoriasPai();
            if (ddlCategoriaPai.SelectedIndex > 0)
                carregaCategorias(Convert.ToInt32(ddlCategoriaPai.SelectedValue));

        }
        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            ProdutoOT produto = new ProdutoOT();

            produto.EAN = txtEan.Text.Trim();
            produto.Distribuidor.ID = Convert.ToInt32(ddlDistribuidor.SelectedValue);
            produto.Categoria.ID = Convert.ToInt32(ddlCategoria.SelectedValue);
            produto.Titulo = txtTitulo.Text.Replace("'", "''").Trim();
            produto.DescricaoCurta = txtDescricaoCurta.Text.Replace("'", "''").Trim();
            //produto.DescricaoCompleta = txtDescricaoCompleta.Text.Replace("'", "''").Trim();
            produto.PalavrasChave = txtPalavrasChave.Text.Replace("'", "''").Trim();
            produto.Observacao = txtObservacao.Text.Replace("'", "''").Trim();

            produto.PrecoCusto = Convert.ToDouble(txtPrecoCusto.Value.Trim());
            produto.Preco = Convert.ToDouble(txtPreco.Value.Trim());
            produto.MarkUp = Convert.ToDouble(txtMKP.Value.Trim());
            produto.Desconto = Convert.ToInt32(txtDesconto.Text.Trim());
            produto.Frete = Convert.ToDouble(txtFrete.Text.Trim());
            produto.Peso = Convert.ToInt32(txtPeso.Text.Trim());
            produto.Estoque = Convert.ToInt32(txtEstoque.Text.Trim());

            produto.Ativo = chkAtivo.Checked;
            produto.Destaque = chkDestaque.Checked;
            produto.ExibirHome = chkExibirHome.Checked;

            produto.ID = new ProdutosOP().InsertProdutos(produto);

            if (uplImagem.PostedFile.ContentLength > 0)
            {
                try
                {
                    salvaImagem(produto.ID);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            limpaCampos();
        }
        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            ProdutoOT produto = new ProdutoOT();

            produto.ID = Convert.ToInt32(lblID.Text);

            produto.EAN = txtEan.Text.Trim();
            produto.Distribuidor.ID = Convert.ToInt32(ddlDistribuidor.SelectedValue);
            produto.Categoria.ID = Convert.ToInt32(ddlCategoria.SelectedValue);
            produto.Titulo = txtTitulo.Text.Replace("'", "''").Trim();
            produto.DescricaoCurta = txtDescricaoCurta.Text.Replace("'", "''").Trim();
            //produto.DescricaoCompleta = txtDescricaoCompleta.Text.Replace("'", "''").Trim();
            produto.PalavrasChave = txtPalavrasChave.Text.Replace("'", "''").Trim();
            produto.Observacao = txtObservacao.Text.Replace("'", "''").Trim();

            produto.PrecoCusto = Convert.ToDouble(txtPrecoCusto.Value.Trim());
            produto.Preco = Convert.ToDouble(txtPreco.Value.Trim());
            produto.MarkUp = Convert.ToDouble(txtMKP.Value.Trim());
            produto.Desconto = Convert.ToInt32(txtDesconto.Text.Trim());
            produto.Frete = Convert.ToDouble(txtFrete.Text.Trim());
            produto.Peso = Convert.ToInt32(txtPeso.Text.Trim());
            produto.Estoque = Convert.ToInt32(txtEstoque.Text.Trim());

            produto.Ativo = chkAtivo.Checked;
            produto.Destaque = chkDestaque.Checked;
            produto.ExibirHome = chkExibirHome.Checked;

            new ProdutosOP().UpdateProdutos(produto);

            if (uplImagem.PostedFile.ContentLength > 0)
            {
                try
                {
                    salvaImagem(produto.ID);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            limpaCampos();
        }
        #endregion
        #region -- Outros --
        public void carregaImagens()
        {
            if (lblID.Text != "")
            {
                grdImagensCadastradas.DataSource = new ProdutosOP().SelectImagensProduto(Convert.ToInt32(lblID.Text));
                grdImagensCadastradas.DataBind();
            }
        }
        public void carregaVideo(ProdutoVideosOT videos)
        {
            pnlVideo.Controls.Clear();
            if (videos.Count > 0)
            {
                //Instanciando o form
                HtmlGenericControl obj = new HtmlGenericControl("object");

                //definindo os atributos
                obj.ID = "Object";
                obj.Attributes.Add("classid", "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000");
                obj.Attributes.Add("codebase", "http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0");
                obj.Attributes.Add("width", "300");
                obj.Attributes.Add("height", "300");

                //adicionando os campos obrigatórios
                obj.InnerHtml += "\n<param name='movie' value='FLVPlayer.swf' />";
                obj.InnerHtml += "\n<param name='salign' value='lt' />";
                obj.InnerHtml += "\n<param name='quality' value='high' />";
                obj.InnerHtml += "\n<param name='scale' value='noscale' />";
                obj.InnerHtml += "\n<param name='FlashVars' value='&MM_ComponentVersion=1&skinName=Skin&streamName=../videosProdutos/" + videos[0].Titulo.Replace(".flv", "") + "&autoPlay=false&autoRewind=false' />";
                obj.InnerHtml += "\n<embed src='FLVPlayer.swf' flashvars='&MM_ComponentVersion=1&skinName=Skin&streamName=../videosProdutos/" + videos[0].Titulo.Replace(".flv", "") + "&autoPlay=false&autoRewind=false'" +
                " quality='high' scale='noscale' width='300' height='300' name='FLVPlayer' salign='LT'" +
                " type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' />";

                //adicionar o controle diretamente na página
                pnlVideo.Controls.Add(obj);

                //Carregando o grid de vídeos
                grdVideosCadastrados.DataSource = videos[0];
                grdVideosCadastrados.DataBind();
            }
        }
        protected void salvaImagem(int id)
        {
            string path = Server.MapPath("imagensProdutos").Replace("\\admin", "") + "\\";

            if (uplImagem.PostedFile.ContentLength > 0)
            {
                try
                {
                    if (uplImagem.PostedFile.FileName.ToLower().IndexOf(".jpg") > 0)
                    {
                        //uplImagem.PostedFile.FileName
                        string fileName = Utilitarios.TiraAcentos(txtTitulo.Text.Trim());

                        //Verifica se já existe arquivo com este nome
                        int cont = 0;
                        do
                            cont++;
                        while (File.Exists(path + fileName + "-" + cont + ".jpg"));

                        fileName = fileName + "-" + cont + ".jpg";

                        //Salva o arquivo
                        uplImagem.PostedFile.SaveAs(path + fileName);
                        //Guarda na base
                        new ProdutosOP().InsertImagens(id, fileName);
                        //Mostra na tela
                        carregaImagens();
                    }
                    else
                    {
                        Utilitarios.ShowMessageBox(this.Page, "Utilize apenas vídeos JPG");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        protected void salvaVideo(int id)
        {
            string path = Server.MapPath("videosProdutos").Replace("\\admin", "") + "\\";

            if (uplVideo.PostedFile.ContentLength > 0)
            {
                try
                {
                    if (uplVideo.PostedFile.FileName.ToLower().IndexOf(".flv") > 0)
                    {
                        string fileName = Utilitarios.TiraAcentos(txtTitulo.Text.Trim());

                        //Verifica se já existe arquivo com este nome
                        int cont = 0;
                        do
                            cont++;
                        while (File.Exists(path + fileName + "-" + cont + ".flv"));

                        fileName = fileName + "-" + cont + ".flv";

                        //Salva o arquivo
                        uplVideo.PostedFile.SaveAs(path + fileName);
                        //Guarda na base
                        new ProdutosOP().InsertVideos(id, fileName);
                        //Mostra na tela
                        //carregaVideo(id, fileName);
                    }
                    else
                    {
                        Utilitarios.ShowMessageBox(this.Page, "Utilize apenas vídeos FLV");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public void limpaCampos()
        {
            //Esvaziando os controles
            lblID.Text = "";
            txtEan.Text = "";
            txtTitulo.Text = "";
            txtDescricaoCurta.Text = "";
            //txtDescricaoCompleta.Text = "";
            txtPalavrasChave.Text = "";

            txtPrecoCusto.Value = "";
            txtPreco.Value = "0,00";
            txtDesconto.Text = "0";
            txtFrete.Text = "0,00";
            txtPeso.Text = "0";
            txtEstoque.Text = "0";

            txtDtCadastro.Text = "";
            chkAtivo.Checked = true;
            chkDestaque.Checked = false;
            chkExibirHome.Checked = false;
            grdImagensCadastradas.DataSource = null;
            grdImagensCadastradas.DataBind();

            ddlDistribuidor.Items.Clear();
            ddlCategoriaPai.Items.Clear();
            ddlCategoria.Items.Clear();

            GridProdutos.DataSource = GridProdutos.DataSource;
            GridProdutos.DataBind();

            pnlGrid.Visible = true;
            pnlCadastro.Visible = false;
        }
        #endregion
    }
}
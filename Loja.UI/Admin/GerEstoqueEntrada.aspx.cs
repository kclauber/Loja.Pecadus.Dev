using System;
using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;

namespace Loja.UI.Pecadus.Admin
{
    public partial class GerEstoqueEntrada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }
        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            if (txtEan.Text != "")
            {
                limpaCampos();

                ProdutoOT produto = new ProdutosOP().SelectProduto(txtEan.Text, 0, 0);
                if (produto != null)
                {
                    lblID.Text = produto.ID.ToString();
                    txtEan.Text = produto.EAN;
                    lblTitulo.Text = produto.Titulo;
                    lblDesc.Text = produto.DescricaoCompleta.Substring(0, 200) + "...";
                    lblObservao.Text = produto.Observacao;

                    if (produto.Imagens.Count > 0)
                    {
                        lnkImg.NavigateUrl = @"/imagensProdutos/" + produto.Imagens[0].Titulo;
                        lnkImg.Text = "Clique para ver a imagem";
                    }
                    else
                        lnkImg.Enabled = false;

                    txtEstoque.Enabled = true;
                    btnCadastrar.Enabled = true;
                }
                else
                {
                    lblTitulo.Text = "PRODUTO NÃO ENCONTRADO";
                }
            }
        }
        public void limpaCampos()
        {
            lblID.Text = "";
            lblTitulo.Text = "";
            lblDesc.Text = "";
            lnkImg.Text = "";

            txtEstoque.Text = "";
            txtEstoque.Enabled = false;
            btnCadastrar.Enabled = false;
        }
        /// <summary>
        /// Incrementa o estoque do item na base de dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            new ProdutosOP().AtualizaEstoque(Convert.ToInt32(lblID.Text), Convert.ToInt32(txtEstoque.Text));

            Utilitarios.ShowMessageBox(this.Page, "Estoque atualizado com sucesso!");
            txtEan.Text = "";
            limpaCampos();
        }
    }
}
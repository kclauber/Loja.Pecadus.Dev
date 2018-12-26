using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using Loja.Persistencia;

namespace Loja.UI.Pecadus.Admin
{
    public partial class Relatorios : System.Web.UI.Page
    {
        private string sqlProdVendidos = String.Format(@"SELECT count(b.idProduto) numVendas, a.id, a.titulo 
                                                              FROM produtos a left join pedidosItens b on a.id = b.idProduto
                                                         group by a.id, a.titulo 
                                                         order by numVendas desc");
        protected void Page_Load(object sender, EventArgs e)
        {
            //Produtos Vendidos
            prodVendidosDS.ConnectionString = ConfigurationManager.ConnectionStrings["conPadrao"].ConnectionString;
            prodVendidosDS.ProviderName = "System.Data.Odbc";
            prodVendidosDS.SelectCommand = sqlProdVendidos;

            //Grafico Estoque
            DataTable dt = new ProdutosOP().RelatorioProdutos();
            if (dt.Rows.Count > 0)
            {
                lblVlrProdutos.Text = String.Format("R$ {0:0.00}", Convert.ToDouble(dt.Rows[0]["VlrTotal"]));
                lblQtdProdutos.Text = dt.Rows[0]["QtdProdutos"].ToString();
                lblTicketMedio.Text = String.Format("R$ {0:0.00}", (Convert.ToDouble(dt.Rows[0]["VlrTotal"]) / Convert.ToDouble(dt.Rows[0]["QtdProdutos"])));
                lblSemimagem.Text = dt.Rows[0]["SemImagem"].ToString();
            }

            GeraGraficoProdutos(dt.Rows[0]);
        }

        private void GeraGraficoProdutos(DataRow dr)
        {
            if (dr != null)
            {
                chartProdutos.Series.Add("Default");
                chartProdutos.Series[0].Font = new Font("Verdana", 8.25F, FontStyle.Regular);

                chartProdutos.ChartAreas[0].Area3DStyle.Enable3D = true;
                chartProdutos.ChartAreas[0].Area3DStyle.Rotation = 25;
                chartProdutos.ChartAreas[0].Area3DStyle.Inclination = 10;
                chartProdutos.ChartAreas[0].Area3DStyle.IsRightAngleAxes = false;

                chartProdutos.Series[0].Points.Add(Convert.ToDouble(dr["QtdProdutos"]));
                chartProdutos.Series[0].Points.Add(Convert.ToDouble(dr["ComEstoque"]));
                chartProdutos.Series[0].Points.Add(Convert.ToDouble(dr["SemEstoque"]));
                chartProdutos.Series[0].Points.Add(Convert.ToDouble(dr["Ativos"]));
                chartProdutos.Series[0].Points.Add(Convert.ToDouble(dr["AtivosComEstoque"]));
                chartProdutos.Series[0].Points.Add(Convert.ToDouble(dr["AtivosSemEstoque"]));
                chartProdutos.Series[0].Points.Add(Convert.ToDouble(dr["Inativos"]));
                chartProdutos.Series[0].Points.Add(Convert.ToDouble(dr["InativosComEstoque"]));
                chartProdutos.Series[0].Points.Add(Convert.ToDouble(dr["InativosSemEstoque"]));

                chartProdutos.Series[0].Points[0].AxisLabel = "Qtd de Produtos";
                chartProdutos.Series[0].Points[1].AxisLabel = "Com Estoque";
                chartProdutos.Series[0].Points[2].AxisLabel = "Sem Estoque";
                chartProdutos.Series[0].Points[3].AxisLabel = "Ativos";
                chartProdutos.Series[0].Points[4].AxisLabel = "Ativos Com Estoque";
                chartProdutos.Series[0].Points[5].AxisLabel = "Ativos Sem Estoque";
                chartProdutos.Series[0].Points[6].AxisLabel = "Inativos";
                chartProdutos.Series[0].Points[7].AxisLabel = "Inativos Com Estoque";
                chartProdutos.Series[0].Points[8].AxisLabel = "Inativos Sem Estoque";

                chartProdutos.Series[0].Points[0].Color = ColorTranslator.FromHtml("#006");
                chartProdutos.Series[0].Points[1].Color = ColorTranslator.FromHtml("#009");
                chartProdutos.Series[0].Points[2].Color = ColorTranslator.FromHtml("#00C");
                chartProdutos.Series[0].Points[3].Color = ColorTranslator.FromHtml("#060");
                chartProdutos.Series[0].Points[4].Color = ColorTranslator.FromHtml("#090");
                chartProdutos.Series[0].Points[5].Color = ColorTranslator.FromHtml("#0C0");
                chartProdutos.Series[0].Points[6].Color = ColorTranslator.FromHtml("#F00");
                chartProdutos.Series[0].Points[7].Color = ColorTranslator.FromHtml("#F03");
                chartProdutos.Series[0].Points[8].Color = ColorTranslator.FromHtml("#F06");

                for (int a = 0; a <= 8; a++)
                {
                    chartProdutos.Series[0].Points[a].IsValueShownAsLabel = true;
                    chartProdutos.Series[0].Points[a].LabelBackColor = Color.White;
                    chartProdutos.Series[0].Points[a].LabelForeColor = Color.Black;
                }
            }
        }
    }
}
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using Loja.Util;

namespace Loja.UI.Pecadus
{
    public partial class calculoFrete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            string retorno = CalculaSedex(ConfigurationManager.AppSettings["cepOrigem"].ToString(),
                                          txtCepDestino.Text.Replace("-", ""),
                                          30.000,
                                          false,
                                          false,
                                          0);
            lblMensagem.Text = "Retornou -> " + retorno;
        }
        protected void btnEncomenda_Click(object sender, EventArgs e)
        {
            try
            {
                string retorno = CalculaPAC(txtCepDestino.Text, 30.000);
                lblMensagem.Text = "Retornou -> " + retorno;
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        protected string CalculaSedex(string cepOrigem, string cepDestino, double peso, bool avisoRecebimento, bool MaoPropria, double valorDeclarado)
        {
            //Cria uma requisição ao service dos correios, com os dados informados
            WebRequest request = WebRequest.Create("http://www.correios.com.br/encomendas/precos/calculo.cfm?" +
                                                   "servico=40010" +
                                                   "&cepOrigem=" + cepOrigem +
                                                   "&cepDestino=" + cepDestino +
                                                   "&peso=" + peso.ToString() +
                                                   "&resposta=xml");
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF7);

            //Coloca os dados recebidos em um DataSet
            DataSet ds = new DataSet();
            ds.ReadXml(sr);
            sr.Close();
            response.Close();

            if (ds.Tables["erro"].Rows[0]["codigo"].ToString() != "0")
                throw new Exception(ds.Tables["erro"].Rows[0]["descricao"].ToString());
            else
                return ds.Tables["Dados_Postais"].Rows[0]["preco_postal"].ToString().Replace(".", ",");
        }
        protected string CalculaPAC(string cepDestino, double peso)
        {

            //Cria uma requisição ao service dos correios, com os dados informados
            WebRequest request = WebRequest.Create("http://frete.w21studio.com/calFrete.xml?cep=" + cepDestino + "&cod=4225&peso=1&comprimento=60&largura=60&altura=5&servico=3");
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF7);
            //Coloca os dados recebidos em um DataSet
            DataSet ds = new DataSet();
            ds.ReadXml(sr);
            sr.Close();
            response.Close();

            if (ds.Tables["frete"].Rows[0]["status"].ToString() != "OK")
                throw new Exception(ds.Tables["frete"].Rows[0]["status"].ToString());
            else
                return ds.Tables["frete"].Rows[0]["valor_sedex"].ToString().Replace(".", ",") + " - " + ds.Tables["frete"].Rows[0]["valor_pac"].ToString().Replace(".", ",");
        }
    }
}
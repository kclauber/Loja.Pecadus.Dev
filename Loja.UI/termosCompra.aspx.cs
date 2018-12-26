using System;
using System.Configuration;
using System.Web.UI;
using Loja.Util;

namespace Loja.UI.Pecadus
{
    public partial class termos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Preparando as META TAGS
            string description = String.Concat("Visando sempre sua satisfação e clareza de informações, ",
                                               "nós publicamos os termos a seguir que regem nossa política de atendimento ao público.");
            string keywords = "atendimento ao publico, sex-shop virtual publico, produtos de sexshop, termos de venda";

            string titulo = String.Concat("Termos e política de atendimento - ",
                                          ConfigurationManager.AppSettings["nomeSiteCompleto"]); ;
            Page.Title = titulo;
            Utilitarios.CarregaMetaTags(this.Page, description, keywords, titulo);
        }
    }
}
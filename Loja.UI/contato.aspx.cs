using System;
using System.Configuration;
using System.Web.UI;
using Loja.Util;

namespace Loja.UI.Pecadus
{
    public partial class contato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Preparando as META TAGS
            string description = String.Concat("Temos uma equipe pronta para atende-lo e tirar suas dúvidas. ",
                                               "Faça suas compras com segurança e conforto. ",
                                               "Sua privacidade estará segura em nosso site");
            string keywords = "contato, telefone de contato, e-mail de contato, formulário de contato, atendimento";

            Page.Title = "Entre em contato conosco e tire suas dúvidas agora mesmo!";
            Utilitarios.CarregaMetaTags(this.Page, description, keywords, Page.Title);
        }

        private void LimpaCampos()
        {
            //txtNome.Text = "";
            //txtEmail.Text = "";
            //txtMensagem.Text = "";
            //txtMensagem.Text = "";
            //txtTel.Text = "";
        }
        protected void btnEnviar_Click(object sender, ImageClickEventArgs e)
        {
            //if (Page.IsValid)
            //{
            //    try
            //    {
            //        string message = String.Format(@"<table>
            //                                             <tr><td colspan='2'>Um novo contato foi feito no site!</td></tr>
            //                                             <tr><td>&nbsp;</td></tr>
            //                                             <tr><td>Nome:</td><td>{0}</td></tr>
            //                                             <tr><td>E-mail:</td><td>{1}</td></tr>
            //                                             <tr><td>Telefone:</td><td>{2}</td></tr>
            //                                             <tr><td>Mensagem:</td><td>{3}</td></tr>
            //                                         </table>",
            //                                       txtNome.Text,
            //                                       txtEmail.Text,
            //                                       txtTel.Text,
            //                                       txtMensagem.Text.Replace("\r\n", "<br/>"));

            //        new Utilitarios().EnviarEmail(String.Format("[{0}] Contato - {1}", ConfigurationManager.AppSettings["nomeSite"], txtNome.Text),
            //                                      message, txtEmail.Text, ConfigurationManager.AppSettings["mailPrincipal"]);

            //        Utilitarios.ShowMessageBox(this.Page, "A mensagem foi enviada com sucesso!\\nObrigado pelo contato.");
            //        LimpaCampos();
            //    }
            //    catch (Exception ex)
            //    {
            //        Utilitarios.ShowMessageBox(this.Page, "Houve um erro ao enviar sua mensagem.\\nPor favor tente novamente mais tarde.\\n\\nA equipe de suporte já foi avisada.");
            //        new Utilitarios().TratarExcessao(ex, Request.Url.ToString(), "contato.btnEnviar_Click", this.Page);
            //    }
            //}
        }
    }
}
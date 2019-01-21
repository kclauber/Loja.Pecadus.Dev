using Loja.Objeto;
using Loja.Persistencia;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Loja.Util
{
    public class Utilitarios
    {
        public static void ShowMessageBox(Page page, string msg)
        {
            string meuscript = "alert('" + msg + "');";
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "MessageBox", meuscript, true);
        }
        #region -- Envio de e-mail --
        public void EnviarEmail(string subject,
                                string message,
                                string mailFrom,
                                string mailTo)
        {
            MailMessage mail = new MailMessage(mailFrom, mailTo, subject, message);
            mail.IsBodyHtml = true;

            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            mail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            mail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["mailHost"]);

            try
            {
                smtp.Send(mail);
            }
            catch
            { }
            finally
            {
                mail.Dispose();
            }
        }
        public void EnviarEmail(string subject,
                                string message,
                                string mailFrom,
                                string mailTo,
                                string mailCC,
                                string mailBcc)
        {
            MailMessage mail = new MailMessage(mailFrom, mailTo, subject, message);
            //mail.ReplyToList.Add(new MailAddress(""));

            if (!String.IsNullOrEmpty(mailCC))
                mail.CC.Add(new MailAddress(mailCC));
            if (!String.IsNullOrEmpty(mailBcc))
                mail.Bcc.Add(new MailAddress(mailBcc));

            mail.IsBodyHtml = true;

            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            mail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            mail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["mailHost"]);

            try
            {
                smtp.Send(mail);
            }
            catch
            { }
            finally
            {
                mail.Dispose();
            }
        }
        #endregion
        #region -- Funções Carrinho --
        public void AdicionarItem(Page page, int idProduto, int quantidade)
        {
            ProdutoOT produto;
#if DEBUG
            produto = ProdutosOP.CarregaProdutoFalso();
#else
            produto = new ProdutosOP().SelectProduto(idProduto, 0, 1);
#endif
            if (produto != null)
            {
                int qtdCarrinho = Carrinho.Instancia.ObterQuantidadeItem(produto.ID);
                if (qtdCarrinho < produto.Estoque)
                    Carrinho.Instancia.AdicionarItem(idProduto, quantidade);                
            }
            page.Response.Redirect("/Carrinho/");
        }
        public void RemoverItem(int codigo)
        {
            Carrinho.Instancia.RemoverItem(codigo);
        }
#endregion
#region -- Tratamento de String --
        public static void CarregaMetaTags(Page page, string description, string keywords, string titulo)
        {
            //Aplicando a expressão regular para tirar HTML e replace para tirar quebras de linha
            if (!String.IsNullOrEmpty(description))
                description = Regex.Replace(description, "<[^>]*>", " ").Replace("\r\n", " ").Replace(",,", ",").Trim();
            if (!String.IsNullOrEmpty(keywords))
                keywords = Regex.Replace(keywords, "<[^>]*>", " ").Replace("\r\n", " ").Replace(",,", ",").Trim();

            HtmlMeta meta;

            //Description
            meta = new HtmlMeta();
            meta.Name = "description"; meta.Content = description;
            page.Header.Controls.AddAt(1, meta);

            //Keywords
            meta = new HtmlMeta();
            meta.Name = "keywords"; meta.Content = keywords;
            page.Header.Controls.AddAt(0, meta);

            //Google
            meta = new HtmlMeta();
            meta.Name = "google-site-verification"; meta.Content = "RyF5dUfcGo6ZcTsDCH95C6HUJ26mBiUeS03jM9BUQxw";
            page.Header.Controls.AddAt(0, meta);

            //Language
            meta = new HtmlMeta();
            meta.HttpEquiv = "Content-Language"; meta.Content = "pt-br";
            page.Header.Controls.AddAt(0, meta);

            //Content-Type
            meta = new HtmlMeta();
            meta.HttpEquiv = "Content-Type"; meta.Content = "text/html; charset=iso-8859-1";
            page.Header.Controls.AddAt(0, meta);

            //Revisit-After
            meta = new HtmlMeta();
            meta.HttpEquiv = "revisit-after"; meta.Content = "1 day";
            page.Header.Controls.AddAt(0, meta);

            //Title
            meta = new HtmlMeta();
            meta.HttpEquiv = "title"; meta.Content = titulo;
            page.Header.Controls.AddAt(0, meta);
        }
        public static string TrataBusca(string busca)
        {
            string[] arr;
            string[] del;
            string strAux = "";

            strAux = "para|com|que|a|e|i|o|u|os|as|em|bem|da|de|di|do|du|pra|pro|c/|p/|c|p|qu|  ";
            arr = strAux.Split('|');

            strAux = "!|@|#|$|%|&|-|+|=|?|~|^|´|`|[|]|{|}|(|)|:|;|,|.|*";
            del = strAux.Split('|');

            //Tratando as palavras
            busca = busca.ToLower();
            for (int a = 0; a < arr.Length; a++)
            {
                string aux = " " + arr[a] + " ";
                if (busca.IndexOf(aux) > -1)
                    busca = busca.Replace(aux, " ");
            }

            for (int b = 0; b < del.Length; b++)
                busca = busca.Replace(del[b], "");

            return busca.ToLower().Trim();
        }
        public static string TiraAcentos(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return "";
            else
            {
                texto = texto.Replace("(", "")
                             .Replace(")", "")
                             .Replace("[", "")
                             .Replace("]", "")
                             .Replace("{", "")
                             .Replace("}", "")
                             .Replace("/", "")
                             .Replace(@"\", "")
                             .Replace("&amp;", "")
                             .Replace("&", "")
                             .Replace("'", "")
                             .Replace(@"\", "")
                             .Replace("/", "")
                             .Replace(" ", "-")
                             .Replace("---", "-")
                             .Replace("--", "-");

                byte[] bytes = Encoding.GetEncoding("iso-8859-8").GetBytes(texto);
                return Encoding.UTF8.GetString(bytes);
            }
        }
        public static string CriaStringLinkProduto(string categoriaPaiTitulo, string categoriaTitulo, string produtoTitulo,
                                                   int idCategoriaPai, int idCategoria, int idProduto)
        {
            return String.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/#produto",
                                 ConfigurationManager.AppSettings["home"],
                                 TiraAcentos(categoriaPaiTitulo),
                                 TiraAcentos(categoriaTitulo),
                                 TiraAcentos(produtoTitulo),
                                 idCategoriaPai,
                                 idCategoria,
                                 idProduto);
        }
        public static string CriaStringLinkCategoria(string categoriaPaiTitulo, string categoriaTitulo,
                                                     int idCategoriaPai, int idCategoria)
        {
            return String.Format("<a href='{0}/{1}/{2}/{3}/{4}'>{5}</a>",
                                 ConfigurationManager.AppSettings["home"],
                                 TiraAcentos(categoriaPaiTitulo),
                                 TiraAcentos(categoriaTitulo),
                                 idCategoriaPai,
                                 idCategoria,
                                 categoriaTitulo);
        }
        public static string CriaStringLinkCategoria(string categoriaPaiTitulo, int idCategoriaPai)
        {
            return String.Format("<a href='{0}/{1}/{2}/'>{3}</a>",
                                 ConfigurationManager.AppSettings["home"],
                                 TiraAcentos(categoriaPaiTitulo),
                                 idCategoriaPai,
                                 categoriaPaiTitulo);
        }

        public static void CriaLinksMigalhas(ref HyperLink lnkMigalhaHome, ref HyperLink lnkCategoriaPai, ref HyperLink lnkCategoria,
                                             int idCategoriaPai, int idCategoria)
        {
            CategoriaOT categoriaPai = new CategoriaOT() { ID = idCategoriaPai };
            CategoriaOT categoria = new CategoriaOT() { ID = idCategoria };
            CategoriasOP categoriaOP = new CategoriasOP();
            categoriaOP.SelectCategoria(ref categoriaPai);
            categoriaOP.SelectCategoria(ref categoria);

            lnkMigalhaHome.Text = "HOME";
            lnkMigalhaHome.NavigateUrl = ConfigurationManager.AppSettings["home"];

            lnkCategoriaPai.Text = " >> " + categoriaPai.Titulo;
            lnkCategoriaPai.NavigateUrl = String.Format("{0}/{1}/{2}",
                                                        ConfigurationManager.AppSettings["home"],
                                                        TiraAcentos(categoriaPai.Titulo),
                                                        categoriaPai.ID);

            lnkCategoria.Text = " >> " + categoria.Titulo;
            lnkCategoria.NavigateUrl = String.Format("{0}/{1}/{2}/{3}/{4}",
                                                     ConfigurationManager.AppSettings["home"],
                                                     TiraAcentos(categoriaPai.Titulo),
                                                     TiraAcentos(categoria.Titulo),
                                                     categoriaPai.ID,
                                                     categoria.ID);
        }
        public static void CriaLinksMigalhas(ref HyperLink lnkMigalhaHome, ref HyperLink lnkCategoriaPai, int idCategoriaPai)
        {
            CategoriaOT categoria = new CategoriaOT() { ID = idCategoriaPai };
            new CategoriasOP().SelectCategoria(ref categoria);

            lnkMigalhaHome.Text = "HOME";
            lnkMigalhaHome.NavigateUrl = ConfigurationManager.AppSettings["home"];

            lnkCategoriaPai.Text = " >> " + categoria.Titulo;
            lnkCategoriaPai.NavigateUrl = String.Format("{0}/{1}/{2}",
                                                        ConfigurationManager.AppSettings["home"],
                                                        TiraAcentos(categoria.Titulo),
                                                        categoria.ID);
        }
        public static string FormatarCep(string cep)
        {
            try
            {
                cep = cep.Replace("-", "")
                         .Replace(".", "")
                         .Replace("/", "")
                         .Replace(@"\", "");
                return Convert.ToUInt64(cep).ToString(@"00000\-000");
            }
            catch
            {
                //Se der algum erro retorna sem formatar para não travar o processo
                return cep;
            }
        }
        #endregion
        public void AdicionarFavorito(int idProduto, bool adicionar)
        {
            if(adicionar)
                Carrinho.Instancia.AdicionarFavorito(idProduto);
            else
                Carrinho.Instancia.RemoverFavorito(idProduto);
        }
        public static void CarregaDescricaoProduto(RepeaterItem e)
        {
            ProdutoOT produto = (ProdutoOT)e.DataItem;
            if (produto != null)
            {
                string link = CriaStringLinkProduto(produto.Categoria.TituloCategoriaPai,
                                                    produto.Categoria.Titulo,
                                                    produto.Titulo,
                                                    produto.Categoria.IDCategoriaPai,
                                                    produto.Categoria.ID,
                                                    produto.ID);
#region Imagem
                Image imgProd = (Image)e.FindControl("imgProd");
                if (imgProd != null)
                {
                    //Tratamento das imagens
                    imgProd.AlternateText = produto.Titulo;
                    if (produto.Imagens.Count > 0)
                        imgProd.ImageUrl = @"ShowImage.aspx?w=270&img=" + produto.Imagens[0].Titulo;
                }
                HyperLink lnkImgProd = (HyperLink)e.FindControl("lnkImgProd");
                if (lnkImgProd != null)
                {
                    //Tratamento dos links
                    lnkImgProd.ToolTip = produto.Titulo;
                    lnkImgProd.NavigateUrl = link;
                }
#endregion
#region Descrição
#region Descrição com link
                //if (lnkDescricao != null)
                //{
                //    //Tratamento da descricao
                //    lnkDescricao.ToolTip = produto.Titulo;
                //    lnkDescricao.NavigateUrl = link;

                //    string desc = produto.DescricaoCurta;
                //    //Aplicando a expressão regular para tirar HTML e replace para tirar quebras de linha                
                //    desc = Regex.Replace(desc, "<[^>]*>", " ").Replace("\r\n", " ").Replace(",,", ",").Trim();
                //    desc = (desc.Length > 90 ? desc.Substring(0, 90) + "..." : desc);
                //    lnkDescricao.Text = desc;
                //}
#endregion
                Label lblDescricao = (Label)e.FindControl("lblDescricao");
                if (lblDescricao != null)
                {
                    string desc = produto.DescricaoCurta;
                    //Aplicando a expressão regular para tirar HTML e replace para tirar quebras de linha                
                    desc = Regex.Replace(desc, "<[^>]*>", " ").Replace("\r\n", " ").Replace(",,", ",").Trim();
                    desc = (desc.Length > 90 ? desc.Substring(0, 90) + "..." : desc);
                    lblDescricao.Text = desc;
                }
#endregion
#region Preço
                HyperLink lnkPreco = (HyperLink)e.FindControl("lnkPreco");
                if (lnkPreco != null)
                {
                    //Tratamento dos preco
                    string preco = "";
                    if (produto.Desconto <= 0)
                    {
                        preco = String.Format(@"<p>Por:</p><ul><li>{0:R$ #,##0.00}</li></ul>", produto.Preco);
                    }
                    else
                    {
                        //Exibindo desconto            
                        preco = String.Format(@"<p>De: <span><s>{0:R$ #,##0.00}</s></span></p>
                                                <p>Por:</p><ul><li>{1:R$ #,##0.00}</li></ul>
                                                <p class='precoEcon'>Economize: {2:R$ #,##0.00}</p>",
                                                produto.Preco,
                                                produto.Preco - ((produto.Preco / 100) * produto.Desconto),
                                                produto.Preco - (produto.Preco - ((produto.Preco / 100) * produto.Desconto)));
                    }
                    lnkPreco.Text = preco;
                }
#endregion
#region Titulo
                HyperLink lnkTitulo = (HyperLink)e.FindControl("lnkTitulo");
                if (lnkTitulo != null)
                {
                    //Tratamento do titulo
                    lnkTitulo.Text = produto.Titulo;
                    lnkTitulo.ToolTip = produto.Titulo;
                    lnkTitulo.NavigateUrl = link;
                }
#endregion
#region Comprar
                LinkButton lnkComprar = (LinkButton)e.FindControl("lnkComprar");
                if (lnkComprar != null)
                {
                    //Tratamento do botão de comprar
                    lnkComprar.CommandArgument = produto.ID.ToString();
                }
#endregion
#region Detalhes
                HyperLink lnkDetalhes = (HyperLink)e.FindControl("lnkDetalhes");
                if (lnkDetalhes != null)
                {
                    //Tratamento dos links
                    lnkDetalhes.ToolTip = produto.Titulo;
                    lnkDetalhes.NavigateUrl = link;
                }
#endregion
#region Adicionar aos favoritos
                HtmlInputCheckBox chkFavoritos = (HtmlInputCheckBox)e.FindControl("chkFavoritos");
                if (chkFavoritos != null)
                {
                    chkFavoritos.Attributes.Add("prod", produto.ID.ToString());
                }
#endregion
            }
        }
#region -- Tratamento de Erro --
        public void TratarExcessao(Exception e, String Url, string metodo, Page page)
        {
            //Erro causado por chamada javascript
            if (Url.ToLower().Contains("/undefined/"))
                return;

            //Erro causado por redirecionamento da página de categorias
            if (e.Message.ToLower().Contains("thread was being aborted."))
                return;

            //if (Url.ToLower().Contains(@"http://localhost"))
            //    throw e;

            string subject = "Exceção não tratada!";
            string msg = String.Format(@"<b>URL        => </b> {0}<br/>
                                         <b>Método     => </b> {1}<br/>
                                         <b>Erro       => </b> {2}<br/>
                                         <b>Source     => </b> {3}<br/>
                                         <b>Data       => </b> {4}<br/>
                                         <b>StackTrace => </b> {5}",
                                         Url, metodo, e.Message, e.Source, e.Data, e.StackTrace);
            string mailFrom = ConfigurationManager.AppSettings["mailServidor"];
            string mailTo = ConfigurationManager.AppSettings["mailPrincipal"];

            EnviarEmail(subject, msg, mailFrom, mailTo);

            if (!Url.ToLower().Contains("erro"))
                page.Response.Redirect("/Erro");
        }
#endregion
                
    }
}

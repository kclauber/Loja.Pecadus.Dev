<%@ Application Language="C#" %>

<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

        void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Contato",
                "Contato/",
                "~/contato.aspx");

            routes.MapPageRoute("Cadastro",
                "Cadastro/",
                "~/cadastro.aspx");

            routes.MapPageRoute("LoginCliente",
                "LoginCliente/",
                "~/loginCliente.aspx");

            routes.MapPageRoute("Pedidos",
                "Pedidos/",
                "~/pedidos.aspx");

            routes.MapPageRoute("Termos",
                "Termos/",
                "~/termosCompra.aspx");

            routes.MapPageRoute("Sobre",
                "Sobre/",
                "~/sobreEmpresa.aspx");

            routes.MapPageRoute("Trocas",
                "Trocas/",
                "~/politicaTrocas.aspx");

            routes.MapPageRoute("Erro",
                "Erro/",
                "~/erro.aspx");

            routes.MapPageRoute("RetornoMoip",
                "RetornoMoip/",
                "~/processaRetornoMoip.aspx");

            routes.MapPageRoute("RetornoPagSeguro",
                "RetornoPagSeguro/",
                "~/processaRetornoPagSeg.aspx");

            routes.MapPageRoute("Confirmacao",
                "Confirmacao/",
                "~/confirmacaoCompra.aspx");

            routes.MapPageRoute("Carrinho",
                "Carrinho/{id}",
                "~/carrinhoCompras.aspx", true,
                new RouteValueDictionary {
                { "id", "" }
                });

            routes.MapPageRoute("Busca",
                "Busca/",
                "~/Busca.aspx", true,
                new RouteValueDictionary {
                { "busca", "" }
                });

            routes.MapPageRoute("SexShop",
                "SexShop/",
                "~/default.aspx");

            routes.MapPageRoute("CategoriaPai",
                    "SexShop/{categoriaPaiTitulo}/{categoriaPaiID}/",
                    "~/categorias.aspx", true,
                new RouteValueDictionary {
                { "categoriaPaiTitulo", "" },
                { "categoriaPaiID", "" }
                });

            routes.MapPageRoute("Categoria",
                    "SexShop/{categoriaPaiTitulo}/{categoriaTitulo}/{categoriaPaiID}/{categoriaID}/",
                    "~/categorias.aspx", true,
                new RouteValueDictionary {
                { "categoriaPaiID", "" },
                { "categoriaID", "" }
            });

            routes.MapPageRoute("Produto",
                    "SexShop/{categoriaPaiTitulo}/{categoriaTitulo}/{titulo}/{categoriaPaiID}/{categoriaID}/{ID}/",
                    "~/produto.aspx", true,
                new RouteValueDictionary {
                { "titulo", "" } ,
                { "categoriaPaiID", "" } ,
                { "categoriaID", "" } ,
                { "ID", "" }
                });
        }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            string erro = "";
            string codErro = "999";

            HttpException httpEx = (HttpException)Server.GetLastError();
            if (httpEx != null)
            {
                erro += "<br/><b>GetHttpCode:</b> " + httpEx.GetHttpCode().ToString();
                codErro = httpEx.GetHttpCode().ToString();
            }

            HttpRequest req = System.Web.HttpContext.Current.Request;
            if (req != null)
            {
                erro += "<br/><b>AcceptTypes:</b> " + req.AcceptTypes;
                erro += "<br/><b>AnonymousID:</b> " + req.AnonymousID;
                erro += "<br/><b>ApplicationPath:</b> " + req.ApplicationPath;
                erro += "<br/><b>AppRelativeCurrentExecutionFilePath:</b> " + req.AppRelativeCurrentExecutionFilePath;
                erro += "<br/><b>Browser:</b> " + req.Browser;
                erro += "<br/><b>ClientCertificate:</b> " + req.ClientCertificate;
                erro += "<br/><b>ContentEncoding:</b> " + req.ContentEncoding;
                erro += "<br/><b>ContentLength:</b> " + req.ContentLength;
                erro += "<br/><b>ContentType:</b> " + req.ContentType;
                erro += "<br/><b>Cookies:</b> " + req.Cookies;
                erro += "<br/><b>CurrentExecutionFilePath:</b> " + req.CurrentExecutionFilePath;
                erro += "<br/><b>FilePath:</b> " + req.FilePath;
                erro += "<br/><b>Files:</b> " + req.Files;
                erro += "<br/><b>Filter:</b> " + req.Filter;
                erro += "<br/><b>Form:</b> " + req.Form;
                erro += "<br/><b>Headers:</b> " + req.Headers;
                erro += "<br/><b>HttpChannelBinding:</b> "; //+ req.HttpChannelBinding;
                erro += "<br/><b>HttpMethod:</b> " + req.HttpMethod;
                erro += "<br/><b>InputStream:</b> " + req.InputStream;
                erro += "<br/><b>IsAuthenticated:</b> " + req.IsAuthenticated;
                erro += "<br/><b>IsLocal:</b> " + req.IsLocal;
                erro += "<br/><b>IsSecureConnection:</b> " + req.IsSecureConnection;
                erro += "<br/><b>LogonUserIdentity:</b> " + req.LogonUserIdentity;
                erro += "<br/><b>Params:</b> " + req.Params;
                erro += "<br/><b>Path:</b> " + req.Path;
                erro += "<br/><b>PathInfo:</b> " + req.PathInfo;
                erro += "<br/><b>PhysicalApplicationPath:</b> " + req.PhysicalApplicationPath;
                erro += "<br/><b>PhysicalPath:</b> " + req.PhysicalPath;
                erro += "<br/><b>QueryString:</b> " + req.QueryString;
                erro += "<br/><b>RawUrl:</b> " + req.RawUrl;
                erro += "<br/><b>RequestType:</b> " + req.RequestType;
                erro += "<br/><b>ServerVariables:</b> " + req.ServerVariables;
                erro += "<br/><b>TotalBytes:</b> " + req.TotalBytes;
                erro += "<br/><b>Url:</b> " + req.Url;
                erro += "<br/><b>UrlReferrer:</b> " + req.UrlReferrer;
                erro += "<br/><b>UserAgent:</b> " + req.UserAgent;
                erro += "<br/><b>UserHostAddress:</b> " + req.UserHostAddress;
                erro += "<br/><b>UserHostName:</b> " + req.UserHostName;
                erro += "<br/><b>UserLanguages:</b> " + req.UserLanguages;

                EnviarEmailErro(erro);
            }

            if (codErro != "999" && req != null)
            {
                if (!req.Url.ToString().Contains(".jpg") &&
                    !req.Url.ToString().Contains(".gif") &&
                    !req.Url.ToString().Contains(".png") &&
                    !req.Url.ToString().Contains(".bmp"))
                {
#if !DEBUG
                Response.Redirect("/Erro/?codErro=" + codErro);
#endif
            }
        }
    }

    public void EnviarEmailErro(string msg)
    {
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.Subject = "Application_Error";
        mail.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["mailPrincipal"]);
        mail.To.Add(new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["mailPrincipal"]));

        mail.IsBodyHtml = true;
        mail.Body = msg;

        //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
        mail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
        mail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["mailHost"]);

        try
        {
            smtp.Send(mail);
        }
        catch
        {}
        finally
        {
            mail.Dispose();
        }
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }
</script>
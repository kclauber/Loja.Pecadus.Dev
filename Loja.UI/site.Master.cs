﻿using Loja.Util;
using System;
using System.Configuration;
using System.Web.UI;

namespace Loja.UI.Pecadus
{
    public partial class site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Código de sessão do usuário
            if (Session["idUser"] == null)
                Session["idUser"] = DateTime.Now.ToString("yyMMddHmmss");
            /////////////////////////////

            string titulo = ConfigurationManager.AppSettings["tituloPadrao"];
            Page.Title = titulo;

            if (!Page.IsPostBack)
            {
                //Tentativa de mostrar a página para os Crawlers sem o Viewstate gigante
                try
                {
                    if (Request.UserAgent.ToLower().Contains("googlebot") || Request.Browser.Crawler)
                        Page.EnableViewState = false;
                }
                catch { } //Se der erro não faz nada

                lblQtdCarrinho.Text = Carrinho.Instancia.ObterQuantidadeItens().ToString();
            }
        }
    }
}
﻿using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;

namespace Loja.UI.Pecadus
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Preparando as META TAGS
            string description = String.Concat("Sex Shop online com grandes promoções, descontos e frete promocional. ",
                                                "Compre produtos de sex shop com segurança e conforto. ",
                                                "Milhares de artigos e acessórios eróticos.");
            string keywords = "sex shop, sexshop, sex-shop, sexyshop, loja virtual, compra online, artigos eroticos, acessorios eroticos";

            string titulo = ConfigurationManager.AppSettings["tituloPadrao"];
            Page.Title = titulo;
            Utilitarios.CarregaMetaTags(this.Page, description, keywords, titulo);

            if (!Page.IsPostBack)
            {
                //ListarProdutos();
            }
        }
        public void ListarProdutos()
        {
            ProdutosOT produtosAux = null;
            produtosAux = new ProdutosOP().SelectProdutosDestaque();
            repProdutoDestaque1.DataSource = produtosAux;
            repProdutoDestaque1.DataBind();

            produtosAux = null;
            produtosAux = new ProdutosOP().SelectProdutosHome();
            repProdutoDestaque2.DataSource = produtosAux;
            repProdutoDestaque2.DataBind();
        }
        protected void repProd1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                ProdutoOT produto = (ProdutoOT)e.Item.DataItem;
                HyperLink lnkImgProd = (HyperLink)e.Item.FindControl("lnkImgProd");
                HyperLink lnkDescricao = (HyperLink)e.Item.FindControl("lnkDescricao");
                HyperLink lnkPreco = (HyperLink)e.Item.FindControl("lnkPreco");
                Image imgProd = (Image)e.Item.FindControl("imgProd");
                LinkButton lnkComprar = (LinkButton)e.Item.FindControl("lnkComprar");

                Utilitarios.CarregaDescricaoProduto(produto, lnkImgProd, lnkDescricao, lnkPreco, imgProd, null, lnkComprar);
            }
        }
        protected void repProd2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            //{
            //    ProdutoOT produto = (ProdutoOT)e.Item.DataItem;
            //    HyperLink lnkImgProd = (HyperLink)e.Item.FindControl("lnkImgProd");
            //    HyperLink lnkDescricao = (HyperLink)e.Item.FindControl("lnkDescricao");
            //    HyperLink lnkPreco = (HyperLink)e.Item.FindControl("lnkPreco");
            //    Image imgProd = (Image)e.Item.FindControl("imgProd");

            //    Utilitarios.CarregaDescricaoProduto(produto, lnkImgProd, lnkDescricao, lnkPreco, imgProd, null);
            //}
        }
        protected void AdicionarItem(object sender, EventArgs e)
        {
            new Utilitarios().AdicionarItem(this.Page, Convert.ToInt32(((LinkButton)sender).CommandArgument), 1);
        }
    }
}
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Loja.Persistencia;
using Loja.Util;

public partial class erro : System.Web.UI.Page
{
    private string tituloPagina = "";
    private string idAux = "";
    private string tituloAux = "";
    private HtmlGenericControl h3 = null;
    private HtmlGenericControl div = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //carregaTagCloud();

            //Preparando as META TAGS
            //string description = String.Concat("Sex Shop online com grandes promoções, descontos e frete promocional. ",
            //                                   "Compre produtos de sex shop com segurança e conforto. ",
            //                                   "Milhares de artigos e acessórios eróticos.");
            //string keywords = "sex shop, sexshop, sex-shop, sexyshop, loja virtual, compra online, artigos eroticos, acessorios eroticos";

            //tituloPagina = (!String.IsNullOrEmpty(tituloPagina) ? tituloPagina : ConfigurationManager.AppSettings["tituloPadrao"]);
            //Utilitarios.CarregaMetaTags(this.Page, description, keywords, tituloPagina);
            //this.Page.Title = tituloPagina;
        }
    }

    #region -- Menu lateral --
    // Caso seja alterado algum método aqui, alterar também na página de categorias!!
    private void carregaTagCloud()
    {
        //DataView[] dv = new DataView[2];
        //dv = new ProdutosOP().SelectTagCloud();
        //ViewState["TdTagCloudPar"] = dv[1].Table;
        //if (dv[0].Table.Rows.Count > 0)
        //{
        //    repTagCloud.DataSource = dv[0];
        //    repTagCloud.DataBind();
        //}
        //else
        //    pnlCloud.Visible = false;
    }

    protected void repTagCloud_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataTable dt = (DataTable)ViewState["TdTagCloudPar"];
        int midValueBusca = int.Parse(dt.Rows[0]["maxValueBusca"].ToString()) / 2;
        int size = 0;

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink lnkAux = (HyperLink)e.Item.FindControl("lnkTag");

            if (DataBinder.Eval(e.Item.DataItem, "tituloPai").ToString() != "")
                lnkAux.Text = DataBinder.Eval(e.Item.DataItem, "tituloPai").ToString() + " ";

            lnkAux.Text += DataBinder.Eval(e.Item.DataItem, "titulo").ToString();

            size = int.Parse(DataBinder.Eval(e.Item.DataItem, "hits").ToString());

            if (size < (midValueBusca / 2))
            {
                lnkAux.Style.Add("font-size", "10px");
                lnkAux.Style.Add("color", "#da8585");
            }
            else if (size < (midValueBusca + (midValueBusca / 2)) && size >= midValueBusca)
            {
                lnkAux.Style.Add("font-size", "11px");
                lnkAux.Style.Add("text-transform", "lowercase");
                lnkAux.Style.Add("color", "#fbadd4");
            }
            else if (size >= (midValueBusca + (midValueBusca / 2)))
            {
                lnkAux.Style.Add("text-transform", "capitalize");
                lnkAux.Style.Add("font-size", "12px");
                lnkAux.Style.Add("color", "#e865a7");
            }
            else if (size < midValueBusca && size >= (midValueBusca / 2))
            {
                lnkAux.Style.Add("font-size", "14px");
                lnkAux.Style.Add("text-transform", "uppercase");
                lnkAux.Style.Add("color", "#f99");
            }

            lnkAux.NavigateUrl = "/Busca/" + DataBinder.Eval(e.Item.DataItem, "titulo").ToString();
        }
    }
    protected void repMenuLateral_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (String.IsNullOrEmpty(DataBinder.Eval(e.Item.DataItem, "idCategoriaPai").ToString()))
            {
                h3 = new HtmlGenericControl("h3");
                h3.InnerHtml = String.Format("<a href='" + ConfigurationManager.AppSettings["home"] + "/{0}/{1}'>{2}</a>",
                                             Utilitarios.TiraAcentos(DataBinder.Eval(e.Item.DataItem, "titulo").ToString()),
                                             DataBinder.Eval(e.Item.DataItem, "id").ToString(),
                                             DataBinder.Eval(e.Item.DataItem, "titulo").ToString());
                e.Item.Controls.Add(h3);

                div = new HtmlGenericControl("div");
                e.Item.Controls.Add(div);

                idAux = DataBinder.Eval(e.Item.DataItem, "id").ToString();
                tituloAux = DataBinder.Eval(e.Item.DataItem, "titulo").ToString();
            }
            else
            {
                div.InnerHtml += String.Format("<a href='" + ConfigurationManager.AppSettings["home"] + "/{0}/{1}/{2}/{3}'>{4}</a><br/>",
                                               Utilitarios.TiraAcentos(tituloAux),
                                               Utilitarios.TiraAcentos(DataBinder.Eval(e.Item.DataItem, "titulo").ToString()),
                                               idAux,
                                               DataBinder.Eval(e.Item.DataItem, "id").ToString(),
                                               DataBinder.Eval(e.Item.DataItem, "titulo").ToString());

            }
        }
    }
    // Caso seja alterado algum método aqui, alterar também na página de categorias!!
    #endregion
}
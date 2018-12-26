using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using Loja.Objeto;
using Loja.Util;
using Loja.Persistencia;

namespace Loja.UI.Pecadus
{
    public partial class processaRetornoPagSeg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request["TransacaoID"]))
                lblTransacaoID.Text = Request["TransacaoID"].ToString();

            if (!Page.IsPostBack)
            {
            }
        }
    }
}
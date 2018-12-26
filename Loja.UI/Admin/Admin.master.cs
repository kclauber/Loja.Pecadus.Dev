using System;

namespace Loja.UI.Pecadus.Admin
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loginAdmin"] == null)
            {
                if (!Request.PhysicalPath.Contains("admin\\default.aspx"))
                    Response.Redirect("~/admin/default.aspx");
            }
        }
    }
}
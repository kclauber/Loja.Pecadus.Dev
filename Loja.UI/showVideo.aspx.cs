using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class showVideo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImagem_Click(object sender, EventArgs e)
    {
        pnlImagem.Visible = !pnlImagem.Visible;
        pnlVideo.Visible = !pnlVideo.Visible;
    }
    protected void btnVideo_Click(object sender, EventArgs e)
    {
        pnlImagem.Visible = !pnlImagem.Visible;
        pnlVideo.Visible = !pnlVideo.Visible;
    }
}
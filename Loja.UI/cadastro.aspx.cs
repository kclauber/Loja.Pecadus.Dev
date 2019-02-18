using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;
using System;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class cadastro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool finalizarCompra = false;
            if (!String.IsNullOrEmpty(Request.QueryString["finalizarCompra"]) && Request.QueryString["finalizarCompra"].Equals("true"))
                finalizarCompra = true;

            if (Cliente.Instancia != null)
            {                
                CarregarDadosCliente();

                btnCadastrar.Visible = false;
                btnAtualizar.Visible = true;
                btnFinalizarCompra.Visible = true;

                if (finalizarCompra && !String.IsNullOrEmpty(Cliente.Instancia.CEP))
                {
                    if (Carrinho.Instancia.CepDestino.Equals(Cliente.Instancia.CEP))
                    {
                        lblObservacao.Text = "Obs.: O CEP informado é diferente do CEP de cadastro. O valor foi recalculado.";
                    }

                    lblTitulo.Text = "Confira seus dados e endereço de entrega";
                    pnlFinalizaCompra.Visible = true;
                    CalcularFrete();
                }
            }
            
            btnFinalizarCompra.Attributes.Add("style", "margin-top: 0px;");
            txtCepDestino.Attributes.Add("placeholder", "CEP");
        }
    }

    protected void btnCadastroNovo_Click(object sender, EventArgs e)
    {
        Cliente cliente = new Cliente();
        RecuperarDadosForm(ref cliente);

        new ClientesOP().InsertCliente(ref cliente);
    }

    protected void btnAtualizarCadastro_Click(object sender, EventArgs e)
    {
        Cliente cliente = new Cliente();
        RecuperarDadosForm(ref cliente);

        new ClientesOP().UpdateCliente(ref cliente);
    }

    private void RecuperarDadosForm(ref Cliente cliente)
    {
        cliente.Nome = txtNome.Value;
        cliente.CEP = txtCepDestino.Text;
        cliente.Endereco = txtEndereco.Value;
        cliente.Numero = txtNumero.Value;
        cliente.Complemento = txtComplemento.Value;
        cliente.Bairro = txtBairro.Value;
        cliente.Cidade = txtCidade.Value;
        cliente.Estado = ddlEstado.Value.ToString();
        cliente.Email = txtEmail.Value;

        cliente.DataNascimento = txtDataNascimento.Value;
        cliente.Celular = txtCelular.Value;
        cliente.Senha = txtSenha1.Value;
        cliente.CPF = txtCPF.Value;
    }

    private void CarregarDadosCliente()
    {
        txtNome.Value = Cliente.Instancia.Nome;
        txtEndereco.Value = Cliente.Instancia.Endereco;
        txtNumero.Value = Cliente.Instancia.Numero;
        txtComplemento.Value = Cliente.Instancia.Complemento;
        txtBairro.Value = Cliente.Instancia.Bairro;
        txtCidade.Value = Cliente.Instancia.Cidade;

        for (int i = 0; i < ddlEstado.Items.Count; i++)
        {
            if (ddlEstado.Items[i].Value.Equals(Cliente.Instancia.Estado))
            {
                ddlEstado.SelectedIndex = i;
                break;
            }
        }

        txtDataNascimento.Value = Cliente.Instancia.DataNascimento;
        txtCelular.Value = Cliente.Instancia.Celular;
        txtCPF.Value = Cliente.Instancia.CPF;

        txtEmail.Value = Cliente.Instancia.Email;
        txtSenha1.Value = Cliente.Instancia.Senha;
        txtSenha2.Value = Cliente.Instancia.Senha;

        txtCepDestino.Text = Cliente.Instancia.CEP;
    }
    protected void rdFrete_CheckedChanged(Object sender, EventArgs e)
    {
        //Recupera o valor do RadioButton e limpa para usar no objeto
        string frete = ((RadioButton)sender).Text;
        frete = frete.Replace(" - ", "")
                     .Replace("R$", "")
                     .Replace("(", "")
                     .Replace(")", "")
                     .Replace(" dias", "");
        string[] arrFrete = frete.Split(' ');

        Carrinho.Instancia.Frete = new FreteOT()
        {
            Tipo = arrFrete[0],
            Valor = Convert.ToDouble(arrFrete[1]),
            Prazo = Convert.ToInt32(arrFrete[2])
        };

        //AtualizaCarrinho();
    }
    protected void txtCepDestino_TextChanged(object sender, EventArgs e)
    {
        CalcularFrete();
    }
    public void CalcularFrete()
    {
        if (!String.IsNullOrEmpty(txtCepDestino.Text))
            Carrinho.Instancia.CepDestino = txtCepDestino.Text;

        try
        {
            new Utilitarios().CalcularFrete(ref rdFreteSedex, ref rdFretePac);
        }
        catch (Exception ex)
        {
            new Utilitarios().TratarExcessao(ex, Request.Url.ToString(), "cadastro.CalcularFrete", this.Page);
        }
    }

    [WebMethod]
    public static string Login(string eMail, string senha)
    {
        Cliente cliente = new Cliente
        {
            Email = eMail,
            Senha = senha
        };
#if DEBUG
        new ClientesOP().SelectClienteFalso(ref cliente);
#else
        new ClientesOP().SelectCliente(ref cliente);        
#endif
        if (!cliente.ID.Equals(-1))
        {
            Cliente.Instancia = cliente;
            return Cliente.Instancia.ID.ToString();
        }
        else
        {
            Cliente.Instancia = null;
            return "erro";
        }
    }

    [WebMethod]
    public static void Logoff()
    {
        Cliente.Instancia = null;
    }

    [WebMethod]
    public static string AdicionarNews(string nome, string eMail)
    {
        Cliente cliente = new Cliente
        {
            Nome = nome,
            Email = eMail
        };

        //TODO: Criar função para inserir na base de dados
        return "ok";
    }
}
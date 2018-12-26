using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;
using System;
using System.Web.Services;

public partial class cadastro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnCadastroNovo_Click(object sender, EventArgs e)
    {
        ClienteOT cliente = new ClienteOT();
        cliente.Nome = txtNome.Value;
        cliente.CEP = txtCEP.Value;
        cliente.Endereco = txtEndereco.Value;
        cliente.Numero = txtNumero.Value;
        cliente.Complemento = txtComplemento.Value;
        cliente.Bairro = txtBairro.Value;
        cliente.Cidade = txtCidade.Value;
        cliente.Estado = ddlEstado.Value.ToString();
        cliente.Telefone = txtTelefone.Value;
        cliente.Email = txtEmail.Value;

        cliente.DataNascimento = txtDataNascimento.Value;
        cliente.Celular = txtCelular.Value;
        cliente.Senha = txtSenha1.Value;
        cliente.CPF = txtCPF.Value;
    }

    [WebMethod]
    public static string Login(string eMail, string senha)
    {
        ClienteOT cliente = new ClienteOT
        {
            Email = eMail,
            Senha = senha
        };
        new ClientesOP().SelectCliente(ref cliente);
        if (cliente != null)
            return cliente.ID.ToString();
        else
            return "erro";
    }

    [WebMethod]
    public static string AdicionarNews(string nome, string eMail)
    {
        ClienteOT cliente = new ClienteOT
        {
            Nome = nome,
            Email = eMail
        };

        //TODO: Criar função para inserir na base de dados
        return "ok";
    }
}
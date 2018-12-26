using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loja.Objeto
{
    public class ClienteOT : Master
    {
        public ClienteOT()
        {
            this.ID = -1;
            this.Pedidos = new PedidosOT();
        }
        public PedidosOT Pedidos { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
    public class Clientes : List<ClienteOT> { }
}

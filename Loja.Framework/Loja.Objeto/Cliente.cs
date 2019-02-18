using Loja.Util;
using System.Collections.Generic;
using System.Web;

namespace Loja.Objeto
{
    public class Cliente : Master
    {
        public Cliente()
        {
            ID = -1;
            Pedido = new PedidoOT();
            Pedidos = new PedidosOT();
        }
        public static Cliente Instancia
        {
            get
            {
                return (Cliente)HttpContext.Current.Session["cliente"];
            }
            set
            {
                HttpContext.Current.Session["cliente"] = value;
            }
        }
        public PedidoOT Pedido { get; set; }
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
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public Carrinho Carrinho { get; set; }
    }
    public class Clientes : List<Cliente> { }
}

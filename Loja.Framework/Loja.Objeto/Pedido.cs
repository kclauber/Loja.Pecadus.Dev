using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loja.Objeto
{
    public class PedidoOT
    {
        public PedidoOT()
        {
            this.ID = -1;
            this.Produtos = new ProdutosOT();
        }
        
        public int ID { get; set; }
        public int IdCliente { get; set; }
        public string TransacaoID { get; set; }
        public string TipoFrete { get; set; }
        public double ValorFrete { get; set; }
        public double ValorDesconto { get; set; }
        public string Anotacao { get; set; }
        public string TipoPagamento { get; set; }
        public int Parcelas { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// Valores de extras se houver
        /// </summary>
        public double Extras { get; set; }
        public DateTime DtCadastro { get; set; }
        /// <summary>
        /// ValorTotal inclui valor dos itens e despesas de frete e extras se houver
        /// </summary>
        public double ValorTotal { get; set; }

        public ProdutosOT Produtos { get; set; }
    }

    public class PedidosOT : List<PedidoOT> { }
}
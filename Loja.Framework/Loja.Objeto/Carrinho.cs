using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Loja.Util
{
    [Serializable()]
    public class Carrinho
    {
        private Carrinho()
        {
            FormasEnvio = new Dictionary<string, string>
            {
                { "Sedex", "04014" },
                { "PAC", "04510" }
            };
            MedidasCaixa = new Dictionary<string, int>
            {
                { "Comprimento", Convert.ToInt32(ConfigurationManager.AppSettings["caixaComprimento"]) },
                { "Altura", Convert.ToInt32(ConfigurationManager.AppSettings["caixaAltura"]) },
                { "Largura", Convert.ToInt32(ConfigurationManager.AppSettings["caixaLargura"]) }
            };

            Limpar();
        }
        public static Carrinho Instancia
        {
            get
            {
                if (HttpContext.Current.Session["carrinho"] == null)
                {
                    HttpContext.Current.Session["carrinho"] = new Carrinho();
                }
                return (Carrinho)HttpContext.Current.Session["carrinho"];
            }
        }

        private Dictionary<int, int> ListaDeItens { get; set; }
        private List<int> ListaFavoritos { get; set; }        


        /// <summary>
        /// Se o item não existir na colection, adiciona
        /// Se o item existir na colection, soma a quantidade
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantidade"></param>
        public void AdicionarItem(int id, int quantidade)
        {
            if (this.ListaDeItens.ContainsKey(id))
            {
                this.ListaDeItens[id] += quantidade;
            }
            else
            {
                this.ListaDeItens.Add(id, quantidade);
            }
        }
        public void AdicionarFavorito(int id)
        {
            this.ListaFavoritos.Add(id);
        }
        /// <summary>
        /// Se o item existir na colection, atualiza a quantidade do item para a quantidade informada
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantidade"></param>
        public void AtualizarQuantidadeItem(int id, int quantidade)
        {
            if (this.ListaDeItens.ContainsKey(id))
            {
                this.ListaDeItens[id] = quantidade;
            }
        }
        /// <summary>
        /// Remove o item da colection
        /// </summary>
        /// <param name="id"></param>
        public void RemoverItem(int id)
        {
            this.ListaDeItens.Remove(id);
        }
        public void RemoverFavorito(int id)
        {
            this.ListaFavoritos.Remove(id);
        }
        public int ObterQuantidadeItem(int id)
        {
            if (this.ListaDeItens.ContainsKey(id))
                return this.ListaDeItens[id];
            else
                return -1;
        }
        public int ObterQuantidadeItens()
        {
            return this.ListaDeItens.Count;
        }
        public void Limpar()
        {
            this.ListaDeItens = new Dictionary<int, int>();
            this.ListaFavoritos = new List<int>();
            this.PesoProdutos = 0;
            this.ValorTotalProdutos = 0D;
            this.Frete = new FreteOT();
            this.CepDestino = String.Empty;
        }
        public bool TemItens
        {
            get
            {
                return this.ListaDeItens.Count > 0;
            }
        }
        public bool TemFavoritos
        {
            get
            {
                return this.ListaFavoritos.Count > 0;
            }
        }
        public int[] CodigosItens
        {
            get
            {
                return this.ListaDeItens.Keys.ToArray();
            }
        }
        public int[] CodigosFavoritos
        {
            get
            {
                return this.ListaFavoritos.ToArray();
            }
        }

        #region -- Propriedades para calcular o envio dos produtos --
        public static string CepOrigem
        {
            get
            {
                return ConfigurationManager.AppSettings["cepOrigem"];
            }
        }
        public string CepDestino { get; set; }
        public int PesoProdutos { get; set; }
        public static Dictionary<string, string> FormasEnvio { get; set; }
        public static Dictionary<string, int> MedidasCaixa { get; set; }

        public enum PacotesEnvio
        {
            Caixa = 1,    //Formato caixa/ pacote
            Rolo = 2,     //Formato rolo/ prisma
            Envelope = 3  //Envelope
        }
        public enum TiposRetornoWSCorreios
        {
            Preco = 1,
            Prazo = 2,
            PrecoPrazo = 3
        }

        #endregion
        
        public double ValorTotalProdutos { get; set; }
        public FreteOT Frete { get; set; }
    }

    public class FreteOT
    {
        public FreteOT()
        {
            this.Tipo = "";
            this.Valor = 0D;
            this.Prazo = 0;
        }
        public string Tipo { get; set; }
        public double Valor { get; set; }
        public int Prazo { get; set; }
    }
}
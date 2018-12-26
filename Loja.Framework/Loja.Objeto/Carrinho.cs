using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Util
{
    [Serializable()]
    public class Carrinho
    {
        private Carrinho()
        {
            this.ListaDeItens = new Dictionary<int, int>();
            this.ListaDeFavoritos = new List<int>();
        }
        private Dictionary<int, int> ListaDeItens { get; set; }
        private List<int> ListaDeFavoritos { get; set; }
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
            this.ListaDeFavoritos.Add(id);
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
            this.ListaDeFavoritos.Remove(id);
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
            this.ListaDeFavoritos = new List<int>();
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
                return this.ListaDeFavoritos.Count > 0;
            }
        }
        public int[] CodigosDosItens
        {
            get
            {
                return this.ListaDeItens.Keys.ToArray();
            }
        }
        public int[] CodigosDosFavoritos
        {
            get
            {
                return this.ListaDeFavoritos.ToArray();
            }
        }
    }
}
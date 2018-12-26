using System;
using System.Collections.Generic;

namespace Loja.Objeto
{
    public class ProdutoOT : Master
    {
        public ProdutoOT()
        {
            this.Imagens = new ProdutoImagensOT();
            this.Videos = new ProdutoVideosOT();
            this.Distribuidor = new DistribuidorOT();
            this.Categoria = new CategoriaOT();
        }

        public ProdutoImagensOT Imagens { get; set; }
        public ProdutoVideosOT Videos { get; set; }
        public DistribuidorOT Distribuidor { get; set; }
        public CategoriaOT Categoria { get; set; }

        public string EAN { get; set; }
        public string Titulo { get; set; }
        public string DescricaoCurta { get; set; }
        public string DescricaoCompleta { get; set; }
        public string PalavrasChave { get; set; }
        public string Observacao { get; set; }
        public double Preco { get; set; }
        public double PrecoCusto { get; set; }
        public double MarkUp { get; set; }
        public int Desconto { get; set; }
        public double Frete { get; set; }
        public int Peso { get; set; }
        public int Estoque { get; set; }
        public int QuantidadeCarrinho { get; set; }
        public bool ExibirHome { get; set; }
        public bool Destaque { get; set; }
    }
    public class ProdutosOT : List<ProdutoOT> { }

    public class ProdutoImagemOT
    {
        public ProdutoImagemOT()
        {
            this.ID = -1;
        }

        public int ID { get; set; }
        public bool Destaque { get; set; }
        public string Titulo { get; set; }
        public DateTime DtCadastro { get; set; }
        public bool Ativo { get; set; }
    }
    public class ProdutoImagensOT : List<ProdutoImagemOT> { }

    public class ProdutoVideoOT
    {
        public ProdutoVideoOT()
        {
            this.ID = -1;
        }

        public int ID { get; set; }
        public bool Destaque { get; set; }
        public string Titulo { get; set; }
        public DateTime DtCadastro { get; set; }
        public bool Ativo { get; set; }
    }
    public class ProdutoVideosOT : List<ProdutoVideoOT> { }
}

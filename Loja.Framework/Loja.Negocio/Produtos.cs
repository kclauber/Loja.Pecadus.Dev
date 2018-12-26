using System;
using Loja.Objeto;
using Loja.Persistencia;
using System.IO;

namespace Loja.Negocio
{
    public class ProdutosON
    {
        public ProdutosOT SelectProdutosHome()
        {
            return new ProdutosOP().SelectProdutosHome();
        }
        public ProdutosOT SelectProdutosDestaque()
        {
            return new ProdutosOP().SelectProdutosDestaque();
        }

        public ProdutoOT SelectProduto(int idProduto, int p, int p_2)
        {
            throw new NotImplementedException();
        }

        public void DeleteImagem(int p)
        {
            throw new NotImplementedException();
        }

        public void DeleteImagem(int p, string fileName)
        {
            string imagensProdutos = HttpServerUtility.Server.MapPath("imagensProdutos").Replace("\\admin", "");
            if (File.Exists(imagensProdutos + "\\" + fileName))
                File.Delete(imagensProdutos + "\\" + fileName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loja.Objeto
{
    public class CategoriaOT : Master
    {
        public CategoriaOT()
        {
            this.IDCategoriaPai = -1;
        }
        public string Titulo { get; set; }
        public int IDCategoriaPai { get; set; }
        public string TituloCategoriaPai { get; set; }
        public string PalavrasChave { get; set; }
    }
    public class CategoriasOT : List<CategoriaOT> { }
}

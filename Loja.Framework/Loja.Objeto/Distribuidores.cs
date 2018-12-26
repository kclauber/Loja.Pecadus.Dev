using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loja.Objeto
{
    public class DistribuidorOT : Master
    {
        public string Nome { get; set; }
        public string Site { get; set; }
        public string EMail { get; set; }
        public string Telefone { get; set; }
        public string Observacao { get; set; }
    }
    public class DistribuidoresOT : List<DistribuidorOT> { }
}

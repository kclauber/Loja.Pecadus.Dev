using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loja.Objeto
{
    public abstract class Master
    {
        public Master()
        {
            this.ID = -1;
        }
        public int ID { get; set; }
        public DateTime DtCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}

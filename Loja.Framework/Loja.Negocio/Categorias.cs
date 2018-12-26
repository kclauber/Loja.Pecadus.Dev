using Loja.Persistencia;

namespace Loja.Negocio
{
    public class CategoriasON
    {
        public void SelectCategoria(ref Objeto.CategoriaOT categoriaPai)
        {
            new CategoriasOP().SelectCategoria(ref categoriaPai);
        }
    }
}

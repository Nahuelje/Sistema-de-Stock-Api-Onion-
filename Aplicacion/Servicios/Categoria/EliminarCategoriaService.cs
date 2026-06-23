using SistemaApiRest.Dominio.Excepciones;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Categoria
{

    /// Caso de uso: Eliminar una categoría.
    /// Regla de negocio: no se puede eliminar una categoría que tenga productos asociados.

    public class EliminarCategoriaService
    {
        private readonly IRepositorioCategoria _repositorioCategoria;
        private readonly IRepositorioProducto _repositorioProducto;

        public EliminarCategoriaService(
            IRepositorioCategoria repositorioCategoria,
            IRepositorioProducto repositorioProducto)
        {
            _repositorioCategoria = repositorioCategoria;
            _repositorioProducto = repositorioProducto;
        }


        /// Retorna false si la categoría no existe, true si fue eliminada exitosamente.
        /// Lanza DominioException si la categoría tiene productos asociados.

        public bool Ejecutar(int id)
        {
            var categoria = _repositorioCategoria.ObtenerPorId(id);

            if (categoria == null)
                return false;

            // Regla de negocio: no eliminar categoría con productos
            if (_repositorioProducto.ExistenProductosEnCategoria(id))
                throw new DominioException(
                    $"No se puede eliminar la categoría '{categoria.Nombre}' porque tiene productos asociados.");

            _repositorioCategoria.Eliminar(id);
            return true;
        }
    }
}

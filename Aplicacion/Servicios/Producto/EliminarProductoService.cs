using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Producto
{

    /// Caso de uso: Eliminar un producto.

    public class EliminarProductoService
    {
        private readonly IRepositorioProducto _repositorioProducto;

        public EliminarProductoService(IRepositorioProducto repositorioProducto)
        {
            _repositorioProducto = repositorioProducto;
        }


        /// Retorna false si el producto no existe, true si fue eliminado exitosamente.

        public bool Ejecutar(int id)
        {
            var producto = _repositorioProducto.ObtenerPorId(id);

            if (producto == null)
                return false;

            _repositorioProducto.Eliminar(id);
            return true;
        }
    }
}

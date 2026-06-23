using SistemaApiRest.Aplicacion.Dto.Producto;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Producto
{

    /// Caso de uso: Obtener un producto por Id.
    /// Enriquece el DTO con el nombre de la categoría.

    public class ObtenerProductoService
    {
        private readonly IRepositorioProducto _repositorioProducto;
        private readonly IRepositorioCategoria _repositorioCategoria;

        public ObtenerProductoService(
            IRepositorioProducto repositorioProducto,
            IRepositorioCategoria repositorioCategoria)
        {
            _repositorioProducto = repositorioProducto;
            _repositorioCategoria = repositorioCategoria;
        }


        /// Retorna null si el producto no existe.

        public ProductoDto? Ejecutar(int id)
        {
            var producto = _repositorioProducto.ObtenerPorId(id);

            if (producto == null)
                return null;

            // Enriquecemos el DTO con el nombre de la categoría
            var categoria = _repositorioCategoria.ObtenerPorId(producto.CategoriaId);

            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = categoria?.Nombre ?? "Sin categoría"
            };
        }
    }
}

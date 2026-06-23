using SistemaApiRest.Aplicacion.Dto.Producto;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Producto
{

    /// Caso de uso: Obtener todos los productos.
    /// Acepta un filtro opcional por categoría.

    public class ObtenerTodosProductosService
    {
        private readonly IRepositorioProducto _repositorioProducto;
        private readonly IRepositorioCategoria _repositorioCategoria;

        public ObtenerTodosProductosService(
            IRepositorioProducto repositorioProducto,
            IRepositorioCategoria repositorioCategoria)
        {
            _repositorioProducto = repositorioProducto;
            _repositorioCategoria = repositorioCategoria;
        }


        /// Si categoriaId es null retorna todos los productos.
        /// Si categoriaId tiene valor, filtra por esa categoría.

        public IEnumerable<ProductoDto> Ejecutar(int? categoriaId = null)
        {
            // Obtenemos el diccionario de categorías para enriquecer los DTOs
            var categorias = _repositorioCategoria.ObtenerTodas()
                .ToDictionary(c => c.Id, c => c.Nombre);

            var productos = categoriaId.HasValue
                ? _repositorioProducto.ObtenerPorCategoria(categoriaId.Value)
                : _repositorioProducto.ObtenerTodos();

            return productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Stock = p.Stock,
                CategoriaId = p.CategoriaId,
                CategoriaNombre = categorias.TryGetValue(p.CategoriaId, out var nombre)
                    ? nombre
                    : "Sin categoría"
            });
        }
    }
}

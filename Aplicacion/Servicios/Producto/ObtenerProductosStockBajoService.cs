using SistemaApiRest.Aplicacion.Dto.Producto;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Producto
{
    /// Caso de uso: Obtener productos cuyo stock está por debajo de un umbral.
    /// Si no se especifica umbral, usa 10 como valor por defecto.
    public class ObtenerProductosStockBajoService
    {
        private readonly IRepositorioProducto _repositorioProducto;
        private readonly IRepositorioCategoria _repositorioCategoria;

        public ObtenerProductosStockBajoService(
            IRepositorioProducto repositorioProducto,
            IRepositorioCategoria repositorioCategoria)
        {
            _repositorioProducto = repositorioProducto;
            _repositorioCategoria = repositorioCategoria;
        }

        public IEnumerable<ProductoDto> Ejecutar(int umbral = 10)
        {
            var categorias = _repositorioCategoria.ObtenerTodas()
                .ToDictionary(c => c.Id, c => c.Nombre);

            return _repositorioProducto.ObtenerTodos()
                .Where(p => p.Stock <= umbral)
                .OrderBy(p => p.Stock) // Los de menor stock primero
                .Select(p => new ProductoDto
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

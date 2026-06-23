using SistemaApiRest.Aplicacion.Dto.Producto;
using SistemaApiRest.Dominio.Excepciones;
using SistemaApiRest.Dominio.Interfaces;
using EntidadProducto = SistemaApiRest.Dominio.Entidades.Producto;

namespace SistemaApiRest.Aplicacion.Servicios.Producto
{

    /// Caso de uso: Crear un nuevo producto.
    /// Valida que la categoría exista antes de crearlo.
    public class CrearProductoService
    {
        private readonly IRepositorioProducto _repositorioProducto;
        private readonly IRepositorioCategoria _repositorioCategoria;

        public CrearProductoService(
            IRepositorioProducto repositorioProducto,
            IRepositorioCategoria repositorioCategoria)
        {
            _repositorioProducto = repositorioProducto;
            _repositorioCategoria = repositorioCategoria;
        }


        /// Retorna el Id del producto creado.

        public int Ejecutar(CrearProductoInput input)
        {
            // Validación de integridad referencial a nivel de aplicación
            var categoria = _repositorioCategoria.ObtenerPorId(input.CategoriaId);
            if (categoria == null)
                throw new DominioException($"No existe una categoría con Id {input.CategoriaId}.");

            // Bug fix: evitar productos con nombre duplicado
            if (_repositorioProducto.ExisteConNombre(input.Nombre))
                throw new DominioException($"Ya existe un producto con el nombre '{input.Nombre}'.");

            var producto = new EntidadProducto(
                input.Nombre,
                input.Descripcion,
                input.Precio,
                input.Stock,
                input.CategoriaId);

            return _repositorioProducto.Agregar(producto);
        }
    }
}

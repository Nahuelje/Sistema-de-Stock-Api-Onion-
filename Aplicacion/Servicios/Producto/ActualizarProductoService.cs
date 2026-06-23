using SistemaApiRest.Aplicacion.Dto.Producto;
using SistemaApiRest.Dominio.Excepciones;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Producto
{

    /// Caso de uso: Actualizar un producto existente (actualización parcial).
    /// Solo actualiza los campos que vienen con valor en el input (no null).

    public class ActualizarProductoService
    {
        private readonly IRepositorioProducto _repositorioProducto;
        private readonly IRepositorioCategoria _repositorioCategoria;

        public ActualizarProductoService(
            IRepositorioProducto repositorioProducto,
            IRepositorioCategoria repositorioCategoria)
        {
            _repositorioProducto = repositorioProducto;
            _repositorioCategoria = repositorioCategoria;
        }


        /// Retorna false si el producto no existe, true si fue actualizado.

        public bool Ejecutar(int id, ActualizarProductoInput input)
        {
            var producto = _repositorioProducto.ObtenerPorId(id);

            if (producto == null)
                return false;

            // Actualización selectiva: solo los campos enviados
            if (input.Nombre != null)
                producto.EstablecerNombre(input.Nombre);

            if (input.Descripcion != null)
                producto.EstablecerDescripcion(input.Descripcion);

            if (input.Precio.HasValue)
                producto.ActualizarPrecio(input.Precio.Value);

            if (input.CategoriaId.HasValue)
            {
                // Validamos que la nueva categoría exista
                var categoria = _repositorioCategoria.ObtenerPorId(input.CategoriaId.Value);
                if (categoria == null)
                    throw new DominioException($"No existe una categoría con Id {input.CategoriaId.Value}.");

                producto.EstablecerCategoria(input.CategoriaId.Value);
            }

            _repositorioProducto.Actualizar(producto);
            return true;
        }
    }
}

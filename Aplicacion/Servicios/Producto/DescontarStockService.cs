using SistemaApiRest.Aplicacion.Dto.Producto;
using SistemaApiRest.Dominio.Excepciones;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Producto
{
    /// Caso de uso: Descontar unidades del stock de un producto.
    /// Delega las reglas de negocio (cantidad > 0, stock suficiente) a la entidad del dominio.

    public class DescontarStockService
    {
        private readonly IRepositorioProducto _repositorioProducto;

        public DescontarStockService(IRepositorioProducto repositorioProducto)
        {
            _repositorioProducto = repositorioProducto;
        }


        /// Descuenta unidades del stock del producto indicado.
        /// Retorna false si el producto no existe.
        /// Lanza DominioException si la cantidad es inválida o si no hay stock suficiente.

        public bool Ejecutar(int productoId, ModificarStockInput input)
        {
            var producto = _repositorioProducto.ObtenerPorId(productoId);

            if (producto == null)
                return false;

            // La entidad valida: cantidad > 0 y stock >= cantidad
            producto.DescontarStock(input.Cantidad);

            _repositorioProducto.Actualizar(producto);
            return true;
        }
    }
}

using SistemaApiRest.Aplicacion.Dto.Producto;
using SistemaApiRest.Dominio.Excepciones;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Producto
{
    /// Caso de uso: Depositar (agregar) unidades al stock de un producto.
    /// Delega la regla de negocio (cantidad > 0) a la entidad del dominio.

    public class DepositarStockService
    {
        private readonly IRepositorioProducto _repositorioProducto;

        public DepositarStockService(IRepositorioProducto repositorioProducto)
        {
            _repositorioProducto = repositorioProducto;
        }


        /// Deposita unidades al stock del producto indicado.
        /// Retorna false si el producto no existe.
        /// Lanza DominioException si la cantidad es inválida (≤ 0).

        public bool Ejecutar(int productoId, ModificarStockInput input)
        {
            var producto = _repositorioProducto.ObtenerPorId(productoId);

            if (producto == null)
                return false;

            // La regla "cantidad > 0" la valida la entidad: no la repetimos aquí
            producto.AgregarStock(input.Cantidad);

            _repositorioProducto.Actualizar(producto);
            return true;
        }
    }
}

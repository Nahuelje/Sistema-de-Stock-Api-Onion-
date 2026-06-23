using Microsoft.AspNetCore.Mvc;
using SistemaApiRest.Aplicacion.Dto.Producto;
using SistemaApiRest.Aplicacion.Servicios.Producto;
using SistemaApiRest.Dominio.Excepciones;

namespace SistemaApiRest.Presentacion.Controllers
{
    /// <summary>
    /// Controller de Productos. Expone el CRUD completo via REST.
    /// Sigue convenciones RESTful: verbos correctos, códigos de respuesta apropiados.
    /// </summary>
    [ApiController]
    [Route("api/productos")]
    public class ProductoController : ControllerBase
    {
        /// <summary>
        /// GET /api/productos/stock-bajo?umbral=10 — Productos con stock en o por debajo del umbral.
        /// Ordenados de menor a mayor stock. Umbral por defecto: 10.
        /// </summary>
        [HttpGet("stock-bajo")]
        public IActionResult StockBajo(
            ObtenerProductosStockBajoService service,
            [FromQuery] int umbral = 10)
        {
            var productos = service.Ejecutar(umbral);
            return Ok(productos);
        }

        /// <summary>
        /// GET /api/productos — Lista todos los productos.
        /// GET /api/productos?categoriaId=1 — Filtra por categoría.
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerTodos(
            ObtenerTodosProductosService service,
            [FromQuery] int? categoriaId = null)
        {
            var productos = service.Ejecutar(categoriaId);
            return Ok(productos);
        }

        /// <summary>
        /// GET /api/productos/{id} — Obtiene un producto específico por Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public IActionResult ObtenerPorId(int id, ObtenerProductoService service)
        {
            var producto = service.Ejecutar(id);

            if (producto == null)
                return NotFound(new { mensaje = $"No existe un producto con Id {id}." });

            return Ok(producto);
        }

        /// <summary>
        /// POST /api/productos — Crea un nuevo producto.
        /// </summary>
        [HttpPost]
        public IActionResult Crear(
            [FromBody] CrearProductoInput input,
            CrearProductoService service)
        {
            try
            {
                int id = service.Ejecutar(input);
                return CreatedAtAction(nameof(ObtenerPorId), new { id }, new { id, mensaje = "Producto creado exitosamente." });
            }
            catch (DominioException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        /// <summary>
        /// PATCH /api/productos/{id} — Actualiza parcialmente un producto.
        /// Solo se actualizan los campos enviados (los null se ignoran).
        /// </summary>
        [HttpPatch("{id:int}")]
        public IActionResult Actualizar(
            int id,
            [FromBody] ActualizarProductoInput input,
            ActualizarProductoService service)
        {
            try
            {
                bool actualizado = service.Ejecutar(id, input);

                if (!actualizado)
                    return NotFound(new { mensaje = $"No existe un producto con Id {id}." });

                return NoContent();
            }
            catch (DominioException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        /// <summary>
        /// DELETE /api/productos/{id} — Elimina un producto.
        /// </summary>
        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id, EliminarProductoService service)
        {
            bool eliminado = service.Ejecutar(id);

            if (!eliminado)
                return NotFound(new { mensaje = $"No existe un producto con Id {id}." });

            return NoContent();
        }

        /// <summary>
        /// PATCH /api/productos/{id}/stock/depositar — Agrega unidades al stock de un producto.
        /// Body: { "cantidad": N }
        /// </summary>
        [HttpPatch("{id:int}/stock/depositar")]
        public IActionResult DepositarStock(
            int id,
            [FromBody] ModificarStockInput input,
            DepositarStockService service)
        {
            try
            {
                bool ejecutado = service.Ejecutar(id, input);

                if (!ejecutado)
                    return NotFound(new { mensaje = $"No existe un producto con Id {id}." });

                return NoContent();
            }
            catch (DominioException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        /// <summary>
        /// PATCH /api/productos/{id}/stock/descontar — Descuenta unidades del stock de un producto.
        /// Body: { "cantidad": N }
        /// Retorna 409 Conflict si no hay stock suficiente.
        /// </summary>
        [HttpPatch("{id:int}/stock/descontar")]
        public IActionResult DescontarStock(
            int id,
            [FromBody] ModificarStockInput input,
            DescontarStockService service)
        {
            try
            {
                bool ejecutado = service.Ejecutar(id, input);

                if (!ejecutado)
                    return NotFound(new { mensaje = $"No existe un producto con Id {id}." });

                return NoContent();
            }
            catch (DominioException ex)
            {
                // Stock insuficiente o cantidad inválida → 409 Conflict
                return Conflict(new { mensaje = ex.Message });
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SistemaApiRest.Aplicacion.Dto.Categoria;
using SistemaApiRest.Aplicacion.Servicios.Categoria;
using SistemaApiRest.Dominio.Excepciones;

namespace SistemaApiRest.Presentacion.Controllers
{
    /// Controller de Categorías. Capa más externa: traduce HTTP ↔ casos de uso.
    /// No contiene lógica de negocio: solo orquesta servicios y maneja respuestas HTTP.
    [ApiController]
    [Route("api/categorias")]
    public class CategoriaController : ControllerBase
    {
        /// GET /api/categorias — Lista todas las categorías.
        [HttpGet]
        public IActionResult ObtenerTodas(ObtenerCategoriasService service)
        {
            var categorias = service.Ejecutar();
            return Ok(categorias);
        }

        /// GET /api/categorias/{id} — Obtiene una categoría específica por Id.
        [HttpGet("{id:int}")]
        public IActionResult ObtenerPorId(int id, ObtenerCategoriaService service)
        {
            var categoria = service.Ejecutar(id);

            if (categoria == null)
                return NotFound(new { mensaje = $"No existe una categoría con Id {id}." });

            return Ok(categoria);
        }

        /// POST /api/categorias — Crea una nueva categoría.
        [HttpPost]
        public IActionResult Crear(
            [FromBody] CrearCategoriaInput input,
            CrearCategoriaService service)
        {
            try
            {
                int id = service.Ejecutar(input);
                return CreatedAtAction(nameof(ObtenerPorId), new { id }, new { id, mensaje = "Categoría creada exitosamente." });
            }
            catch (DominioException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        /// PATCH /api/categorias/{id} — Actualiza parcialmente una categoría.
        /// Solo se actualizan los campos enviados (los null se ignoran).
        [HttpPatch("{id:int}")]
        public IActionResult Actualizar(
            int id,
            [FromBody] ActualizarCategoriaInput input,
            ActualizarCategoriaService service)
        {
            try
            {
                bool actualizado = service.Ejecutar(id, input);

                if (!actualizado)
                    return NotFound(new { mensaje = $"No existe una categoría con Id {id}." });

                return NoContent();
            }
            catch (DominioException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        /// DELETE /api/categorias/{id} — Elimina una categoría si no tiene productos.
        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id, EliminarCategoriaService service)
        {
            try
            {
                bool eliminada = service.Ejecutar(id);

                if (!eliminada)
                    return NotFound(new { mensaje = $"No existe una categoría con Id {id}." });

                return NoContent();
            }
            catch (DominioException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }
    }
}


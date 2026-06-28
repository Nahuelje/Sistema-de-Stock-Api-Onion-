using Microsoft.AspNetCore.Mvc;
using SistemaApiRest.Aplicacion.Dto;
using SistemaApiRest.Aplicacion.Servicios.Categoria;
using SistemaApiRest.Dominio.Excepciones;

namespace SistemaApiRest.Presentacion.Controllers;

[ApiController]
[Route("api/categorias")]
public class CategoriaController : ControllerBase
{
    private readonly CrearCategoriaService _crear;
    private readonly ObtenerCategoriasService _obtenerTodas;
    private readonly ObtenerCategoriaService _obtenerUna;
    private readonly EliminarCategoriaService _eliminar;

    public CategoriaController(
        CrearCategoriaService crear,
        ObtenerCategoriasService obtenerTodas,
        ObtenerCategoriaService obtenerUna,
        EliminarCategoriaService eliminar)
    {
        _crear = crear;
        _obtenerTodas = obtenerTodas;
        _obtenerUna = obtenerUna;
        _eliminar = eliminar;
    }

    [HttpGet]
    public IActionResult ObtenerTodas()
    {
        var categorias = _obtenerTodas.Ejecutar();
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public IActionResult ObtenerPorId(int id)
    {
        try
        {
            var categoria = _obtenerUna.Ejecutar(id);
            return Ok(categoria);
        }
        catch (DominioException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult Crear([FromBody] CrearCategoriaInput input)
    {
        try
        {
            var id = _crear.Ejecutar(input);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, new { id });
        }
        catch (DominioException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        try
        {
            _eliminar.Ejecutar(id);
            return NoContent();
        }
        catch (DominioException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using SistemaApiRest.Aplicacion.Dto;
using SistemaApiRest.Aplicacion.Servicios.Producto;
using SistemaApiRest.Dominio.Excepciones;

namespace SistemaApiRest.Presentacion.Controllers;

[ApiController]
[Route("api/productos")]
public class ProductoController : ControllerBase
{
    private readonly CrearProductoService _crear;
    private readonly ObtenerTodosProductosService _obtenerTodos;
    private readonly ObtenerProductoService _obtenerUno;
    private readonly EliminarProductoService _eliminar;
    private readonly ModificarStockService _stock;

    public ProductoController(
        CrearProductoService crear,
        ObtenerTodosProductosService obtenerTodos,
        ObtenerProductoService obtenerUno,
        EliminarProductoService eliminar,
        ModificarStockService stock)
    {
        _crear = crear;
        _obtenerTodos = obtenerTodos;
        _obtenerUno = obtenerUno;
        _eliminar = eliminar;
        _stock = stock;
    }

    [HttpGet]
    public IActionResult ObtenerTodos()
    {
        var productos = _obtenerTodos.Ejecutar();
        return Ok(productos);
    }

    [HttpGet("{id}")]
    public IActionResult ObtenerPorId(int id)
    {
        try
        {
            var producto = _obtenerUno.Ejecutar(id);
            return Ok(producto);
        }
        catch (DominioException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult Crear([FromBody] CrearProductoInput input)
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

    [HttpPost("{id}/depositar")]
    public IActionResult Depositar(int id, [FromBody] ModificarStockInput input)
    {
        try
        {
            _stock.Depositar(id, input.Cantidad);
            return Ok(new { mensaje = "Stock actualizado." });
        }
        catch (DominioException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost("{id}/descontar")]
    public IActionResult Descontar(int id, [FromBody] ModificarStockInput input)
    {
        try
        {
            _stock.Descontar(id, input.Cantidad);
            return Ok(new { mensaje = "Stock actualizado." });
        }
        catch (DominioException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}
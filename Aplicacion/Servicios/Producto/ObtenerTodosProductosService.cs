using SistemaApiRest.Aplicacion.Dto;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Producto;

public class ObtenerTodosProductosService
{
    private readonly IRepositorioProducto _repositorio;

    public ObtenerTodosProductosService(IRepositorioProducto repositorio)
    {
        _repositorio = repositorio;
    }

    public IEnumerable<ProductoDto> Ejecutar()
    {
        return _repositorio.ObtenerTodos().Select(p => new ProductoDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Precio = p.Precio,
            Stock = p.Stock,
            CategoriaId = p.CategoriaId
        });
    }
}
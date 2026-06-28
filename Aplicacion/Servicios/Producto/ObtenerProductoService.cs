using SistemaApiRest.Aplicacion.Dto;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Producto;

public class ObtenerProductoService
{
    private readonly IRepositorioProducto _repositorio;

    public ObtenerProductoService(IRepositorioProducto repositorio)
    {
        _repositorio = repositorio;
    }

    public ProductoDto Ejecutar(int id)
    {
        var producto = _repositorio.ObtenerPorId(id)
            ?? throw new Dominio.Excepciones.DominioException($"No existe un producto con Id {id}.");

        return new ProductoDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Precio = producto.Precio,
            Stock = producto.Stock,
            CategoriaId = producto.CategoriaId
        };
    }
}
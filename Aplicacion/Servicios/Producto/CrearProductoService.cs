using SistemaApiRest.Aplicacion.Dto;
using SistemaApiRest.Dominio.Excepciones;
using SistemaApiRest.Dominio.Interfaces;
using ProductoEntidad = SistemaApiRest.Dominio.Entidades.Producto;

namespace SistemaApiRest.Aplicacion.Servicios.Producto;

public class CrearProductoService
{
    private readonly IRepositorioProducto _repositorio;

    public CrearProductoService(IRepositorioProducto repositorio)
    {
        _repositorio = repositorio;
    }

    public int Ejecutar(CrearProductoInput input)
    {
        var producto = new ProductoEntidad(input.Nombre, input.Precio, input.Stock, input.CategoriaId);
        return _repositorio.Agregar(producto);
    }
}

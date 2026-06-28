using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Producto;

public class EliminarProductoService
{
    private readonly IRepositorioProducto _repositorio;

    public EliminarProductoService(IRepositorioProducto repositorio)
    {
        _repositorio = repositorio;
    }

    public void Ejecutar(int id)
    {
        var producto = _repositorio.ObtenerPorId(id)
            ?? throw new Dominio.Excepciones.DominioException($"No existe un producto con Id {id}.");

        _repositorio.Eliminar(producto.Id);
    }
}
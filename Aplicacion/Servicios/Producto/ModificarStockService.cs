using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Producto;

public class ModificarStockService
{
    private readonly IRepositorioProducto _repositorio;

    public ModificarStockService(IRepositorioProducto repositorio)
    {
        _repositorio = repositorio;
    }

    public void Depositar(int id, int cantidad)
    {
        var producto = _repositorio.ObtenerPorId(id)
            ?? throw new Dominio.Excepciones.DominioException($"No existe un producto con Id {id}.");

        producto.AgregarStock(cantidad);
        _repositorio.Actualizar(producto);
    }

    public void Descontar(int id, int cantidad)
    {
        var producto = _repositorio.ObtenerPorId(id)
            ?? throw new Dominio.Excepciones.DominioException($"No existe un producto con Id {id}.");

        producto.DescontarStock(cantidad);
        _repositorio.Actualizar(producto);
    }
}
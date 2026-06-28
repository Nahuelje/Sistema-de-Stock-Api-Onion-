using SistemaApiRest.Dominio.Entidades;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Persistencia.Repositorios;

public class RepositorioProductoEnMemoria : IRepositorioProducto
{
    private readonly List<Producto> _productos = new();
    private int _proximoId = 1;

    public int Agregar(Producto producto)
    {
        producto.EstablecerId(_proximoId++);
        _productos.Add(producto);
        return producto.Id;
    }

    public Producto? ObtenerPorId(int id)
    {
        return _productos.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Producto> ObtenerTodos()
    {
        return _productos.ToList();
    }

    public void Actualizar(Producto producto)
    {
        var indice = _productos.FindIndex(p => p.Id == producto.Id);
        if (indice >= 0)
            _productos[indice] = producto;
    }

    public void Eliminar(int id)
    {
        var producto = _productos.FirstOrDefault(p => p.Id == id);
        if (producto != null)
            _productos.Remove(producto);
    }

    public bool ExistenProductosEnCategoria(int categoriaId)
    {
        return _productos.Any(p => p.CategoriaId == categoriaId);
    }
}

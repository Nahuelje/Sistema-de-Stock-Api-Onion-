using SistemaApiRest.Dominio.Entidades;
using SistemaApiRest.Dominio.Interfaces;
using SistemaApiRest.Persistencia.Contexto;

namespace SistemaApiRest.Persistencia.Repositorios;

public class RepositorioProductoEf : IRepositorioProducto
{
    private readonly AppDbContext _contexto;

    public RepositorioProductoEf(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public int Agregar(Producto producto)
    {
        _contexto.Productos.Add(producto);
        _contexto.SaveChanges();
        return producto.Id;
    }

    public Producto? ObtenerPorId(int id)
    {
        return _contexto.Productos.Find(id);
    }

    public IEnumerable<Producto> ObtenerTodos()
    {
        return _contexto.Productos.ToList();
    }

    public void Actualizar(Producto producto)
    {
        _contexto.Productos.Update(producto);
        _contexto.SaveChanges();
    }

    public void Eliminar(int id)
    {
        var producto = _contexto.Productos.Find(id);
        if (producto != null)
        {
            _contexto.Productos.Remove(producto);
            _contexto.SaveChanges();
        }
    }

    public bool ExistenProductosEnCategoria(int categoriaId)
    {
        return _contexto.Productos.Any(p => p.CategoriaId == categoriaId);
    }
}
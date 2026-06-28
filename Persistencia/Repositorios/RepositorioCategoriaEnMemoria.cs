using SistemaApiRest.Dominio.Entidades;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Persistencia.Repositorios;

public class RepositorioCategoriaEnMemoria : IRepositorioCategoria
{
    private readonly List<Categoria> _categorias = new();
    private int _proximoId = 1;

    public int Agregar(Categoria categoria)
    {
        categoria.EstablecerId(_proximoId++);
        _categorias.Add(categoria);
        return categoria.Id;
    }

    public Categoria? ObtenerPorId(int id)
    {
        return _categorias.FirstOrDefault(c => c.Id == id);
    }

    public IEnumerable<Categoria> ObtenerTodas()
    {
        return _categorias.ToList();
    }

    public void Actualizar(Categoria categoria)
    {
        var indice = _categorias.FindIndex(c => c.Id == categoria.Id);
        if (indice >= 0)
            _categorias[indice] = categoria;
    }

    public void Eliminar(int id)
    {
        var categoria = _categorias.FirstOrDefault(c => c.Id == id);
        if (categoria != null)
            _categorias.Remove(categoria);
    }

    public bool ExisteConNombre(string nombre)
    {
        return _categorias.Any(c => c.Nombre == nombre);
    }
}

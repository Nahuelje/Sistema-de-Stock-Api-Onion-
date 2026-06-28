using Microsoft.EntityFrameworkCore;
using SistemaApiRest.Dominio.Entidades;
using SistemaApiRest.Dominio.Interfaces;
using SistemaApiRest.Persistencia.Contexto;

namespace SistemaApiRest.Persistencia.Repositorios;

public class RepositorioCategoriaEf : IRepositorioCategoria
{
    private readonly AppDbContext _contexto;

    public RepositorioCategoriaEf(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public int Agregar(Categoria categoria)
    {
        _contexto.Categorias.Add(categoria);
        _contexto.SaveChanges();
        return categoria.Id;
    }

    public Categoria? ObtenerPorId(int id)
    {
        return _contexto.Categorias.Find(id);
    }

    public IEnumerable<Categoria> ObtenerTodas()
    {
        return _contexto.Categorias.ToList();
    }

    public void Actualizar(Categoria categoria)
    {
        _contexto.Categorias.Update(categoria);
        _contexto.SaveChanges();
    }

    public void Eliminar(int id)
    {
        var categoria = _contexto.Categorias.Find(id);
        if (categoria != null)
        {
            _contexto.Categorias.Remove(categoria);
            _contexto.SaveChanges();
        }
    }

    public bool ExisteConNombre(string nombre)
    {
        return _contexto.Categorias.Any(c => c.Nombre == nombre);
    }
}
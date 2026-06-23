using Microsoft.EntityFrameworkCore;
using SistemaApiRest.Dominio.Entidades;
using SistemaApiRest.Dominio.Interfaces;
using SistemaApiRest.Persistencia.Contexto;

namespace SistemaApiRest.Persistencia.Repositorios
{
    /// <summary>
    /// Implementación del repositorio de Categorías usando EF Core.
    /// Registrado como Scoped: un contexto por request HTTP.
    /// Reemplaza a RepositorioCategoriaEnMemoria en producción.
    /// </summary>
    public class RepositorioCategoriaEf : IRepositorioCategoria
    {
        private readonly AppDbContext _context;

        public RepositorioCategoriaEf(AppDbContext context)
        {
            _context = context;
        }

        public int Agregar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            // EF Core asigna el Id generado por la BD directamente en la entidad
            return categoria.Id;
        }

        public Categoria? ObtenerPorId(int id)
        {
            return _context.Categorias.Find(id);
        }

        public IEnumerable<Categoria> ObtenerTodas()
        {
            return _context.Categorias.ToList();
        }

        public bool ExisteConNombre(string nombre)
        {
            // La comparación case-insensitive la maneja la collation de SQL Server
            string nombreNormalizado = nombre.Trim();
            return _context.Categorias
                .Any(c => c.Nombre == nombreNormalizado);
        }

        public void Actualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
            }
        }
    }
}

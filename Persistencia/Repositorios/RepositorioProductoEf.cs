using SistemaApiRest.Dominio.Entidades;
using SistemaApiRest.Dominio.Interfaces;
using SistemaApiRest.Persistencia.Contexto;

namespace SistemaApiRest.Persistencia.Repositorios
{
    /// <summary>
    /// Implementación del repositorio de Productos usando EF Core.
    /// Registrado como Scoped: un contexto por request HTTP.
    /// Reemplaza a RepositorioProductoEnMemoria en producción.
    /// </summary>
    public class RepositorioProductoEf : IRepositorioProducto
    {
        private readonly AppDbContext _context;

        public RepositorioProductoEf(AppDbContext context)
        {
            _context = context;
        }

        public int Agregar(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
            // EF Core asigna el Id generado por la BD directamente en la entidad
            return producto.Id;
        }

        public Producto? ObtenerPorId(int id)
        {
            return _context.Productos.Find(id);
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            return _context.Productos.ToList();
        }

        public IEnumerable<Producto> ObtenerPorCategoria(int categoriaId)
        {
            return _context.Productos
                .Where(p => p.CategoriaId == categoriaId)
                .ToList();
        }

        public void Actualizar(Producto producto)
        {
            // EF Core ya está trackeando la entidad (fue obtenida del contexto)
            // Update asegura que los cambios se marquen como modificados
            _context.Productos.Update(producto);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
            }
        }

        public bool ExistenProductosEnCategoria(int categoriaId)
        {
            return _context.Productos.Any(p => p.CategoriaId == categoriaId);
        }

        public bool ExisteConNombre(string nombre)
        {
            string nombreNormalizado = nombre.Trim();
            return _context.Productos.Any(p => p.Nombre == nombreNormalizado);
        }
    }
}

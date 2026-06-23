using Microsoft.EntityFrameworkCore;
using SistemaApiRest.Dominio.Entidades;
using SistemaApiRest.Persistencia.Configuraciones;

namespace SistemaApiRest.Persistencia.Contexto
{
    /// <summary>
    /// DbContext principal de la aplicación.
    /// Punto de entrada de EF Core hacia la base de datos.
    /// Registrado en el contenedor de DI con AddDbContext (ciclo Scoped por request).
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Producto> Productos => Set<Producto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica las configuraciones Fluent API por entidad (IEntityTypeConfiguration<T>)
            modelBuilder.ApplyConfiguration(new CategoriaConfiguracion());
            modelBuilder.ApplyConfiguration(new ProductoConfiguracion());
        }
    }
}

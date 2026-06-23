using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaApiRest.Dominio.Entidades;

namespace SistemaApiRest.Persistencia.Configuraciones
{
    /// <summary>
    /// Configuración Fluent API para la entidad Categoria.
    /// Define tabla, columnas, restricciones y el índice único en Nombre.
    /// </summary>
    public class CategoriaConfiguracion : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            // Clave primaria — EF Core autogenera el Id en la BD
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Descripcion)
                   .HasMaxLength(500);

            // Índice único: refuerza la regla de negocio "nombre único" a nivel de BD
            builder.HasIndex(c => c.Nombre)
                   .IsUnique()
                   .HasDatabaseName("IX_Categorias_Nombre");
        }
    }
}

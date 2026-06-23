using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaApiRest.Dominio.Entidades;

namespace SistemaApiRest.Persistencia.Configuraciones
{
    /// <summary>
    /// Configuración Fluent API para la entidad Producto.
    /// Define tabla, columnas, precisión decimal, FK a Categoria y restricciones.
    /// </summary>
    public class ProductoConfiguracion : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");

            // Clave primaria
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Nombre)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.Descripcion)
                   .HasMaxLength(1000);

            // decimal(18,2): compatible con MySQL
            builder.Property(p => p.Precio)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Stock)
                   .IsRequired();

            builder.Property(p => p.CategoriaId)
                   .IsRequired();

            // Relación Producto → Categoria
            // Restrict: no permite eliminar una Categoria si tiene Productos (lo valida la app también)
            builder.HasOne<Categoria>()
                   .WithMany()
                   .HasForeignKey(p => p.CategoriaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaApiRest.Dominio.Entidades;

namespace SistemaApiRest.Persistencia.Configuraciones;

public class ProductoConfiguracion : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.ToTable("productos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Precio).HasPrecision(10, 2);
        builder.Property(p => p.Stock).IsRequired();
        builder.Property(p => p.CategoriaId).IsRequired();
    }
}
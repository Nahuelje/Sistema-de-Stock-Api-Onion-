using SistemaApiRest.Dominio.Excepciones;

namespace SistemaApiRest.Dominio.Entidades
{
    public class Producto
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public decimal Precio { get; private set; }
        public int Stock { get; private set; }
        public int CategoriaId { get; private set; }

        public Producto() { Nombre = ""; }

        public void EstablecerId(int id)
        {
            if (id <= 0) throw new DominioException("El Id del producto debe ser positivo.");
            Id = id;
        }

        public Producto(string nombre, decimal precio, int stock, int categoriaId)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DominioException("El nombre no puede estar vacío.");
            if (nombre.Length > 100)
                throw new DominioException("Nombre muy largo (max 100).");
            if (precio < 0)
                throw new DominioException("El precio no puede ser negativo.");
            if (stock < 0)
                throw new DominioException("El stock no puede ser negativo.");
            if (categoriaId <= 0)
                throw new DominioException("CategoriaId inválido.");

            Nombre = nombre.Trim();
            Precio = precio;
            Stock = stock;
            CategoriaId = categoriaId;
        }

        public void AgregarStock(int cantidad)
        {
            if (cantidad <= 0) throw new DominioException("La cantidad debe ser > 0.");
            Stock += cantidad;
        }

        public void DescontarStock(int cantidad)
        {
            if (cantidad <= 0) throw new DominioException("La cantidad debe ser > 0.");
            if (Stock < cantidad) throw new DominioException($"Stock insuficiente. Tenés: {Stock}");
            Stock -= cantidad;
        }
    }
}
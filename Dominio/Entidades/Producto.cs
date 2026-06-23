using SistemaApiRest.Dominio.Excepciones;

namespace SistemaApiRest.Dominio.Entidades
{

    /// Entidad Producto. Núcleo del dominio.
    /// Encapsula toda la lógica de negocio relacionada con un producto:
    /// validaciones de precio, stock, y operaciones de negocio.

    public class Producto
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public decimal Precio { get; private set; }
        public int Stock { get; private set; }
        public int CategoriaId { get; private set; }

        // Constructor vacío requerido por algunos ORMs y deserializadores
        public Producto()
        {
            Nombre = string.Empty;
            Descripcion = string.Empty;
        }

        public Producto(string nombre, string descripcion, decimal precio, int stock, int categoriaId)
        {
            EstablecerNombre(nombre);
            EstablecerDescripcion(descripcion);
            EstablecerPrecio(precio);
            EstablecerStock(stock);
            EstablecerCategoria(categoriaId);
        }

        // Setter interno para que la persistencia pueda asignar el Id generado
        public void EstablecerId(int id)
        {
            if (id <= 0)
                throw new DominioException("El Id del producto debe ser positivo.");
            Id = id;
        }

        public void EstablecerNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DominioException("El nombre del producto no puede estar vacío.");

            if (nombre.Length > 200)
                throw new DominioException("El nombre no puede superar los 200 caracteres.");

            Nombre = nombre.Trim();
        }

        public void EstablecerDescripcion(string descripcion)
        {
            if (descripcion != null && descripcion.Length > 1000)
                throw new DominioException("La descripción no puede superar los 1000 caracteres.");

            Descripcion = descripcion?.Trim() ?? string.Empty;
        }

        public void EstablecerPrecio(decimal precio)
        {
            if (precio <= 0)
                throw new DominioException("El precio debe ser mayor que cero.");

            Precio = precio;
        }

        public void EstablecerStock(int stock)
        {
            if (stock < 0)
                throw new DominioException("El stock no puede ser negativo.");

            Stock = stock;
        }

        public void EstablecerCategoria(int categoriaId)
        {
            if (categoriaId <= 0)
                throw new DominioException("El Id de categoría debe ser válido.");

            CategoriaId = categoriaId;
        }

        // --- Métodos de negocio del dominio ---
        /// Agrega unidades al stock del producto.

        public void AgregarStock(int cantidad)
        {
            if (cantidad <= 0)
                throw new DominioException("La cantidad a agregar debe ser mayor que cero.");

            Stock += cantidad;
        }


        /// Descuenta unidades del stock. Lanza excepción si no hay suficiente stock.

        public void DescontarStock(int cantidad)
        {
            if (cantidad <= 0)
                throw new DominioException("La cantidad a descontar debe ser mayor que cero.");

            if (Stock < cantidad)
                throw new DominioException($"Stock insuficiente. Stock actual: {Stock}, solicitado: {cantidad}.");

            Stock -= cantidad;
        }


        /// Actualiza el precio del producto. No puede ser negativo ni cero.

        public void ActualizarPrecio(decimal nuevoPrecio)
        {
            if (nuevoPrecio <= 0)
                throw new DominioException("El nuevo precio debe ser mayor que cero.");

            Precio = nuevoPrecio;
        }
    }
}

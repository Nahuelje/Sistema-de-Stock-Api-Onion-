using SistemaApiRest.Dominio.Excepciones;

namespace SistemaApiRest.Dominio.Entidades
{
    /// <summary>
    /// Entidad Categoria. Representa una clasificación de productos.
    /// La lógica de validación vive aquí, en el dominio, no en los servicios.
    /// </summary>
    public class Categoria
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }

        // Constructor vacío requerido por algunos ORMs y deserializadores
        public Categoria()
        {
            Nombre = string.Empty;
            Descripcion = string.Empty;
        }

        public Categoria(string nombre, string descripcion)
        {
            EstablecerNombre(nombre);
            EstablecerDescripcion(descripcion);
        }

        // Setter interno para que la persistencia pueda asignar el Id generado
        public void EstablecerId(int id)
        {
            if (id <= 0)
                throw new DominioException("El Id de la categoría debe ser positivo.");
            Id = id;
        }

        public void EstablecerNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DominioException("El nombre de la categoría no puede estar vacío.");

            if (nombre.Length > 100)
                throw new DominioException("El nombre de la categoría no puede superar los 100 caracteres.");

            Nombre = nombre.Trim();
        }

        public void EstablecerDescripcion(string descripcion)
        {
            if (descripcion != null && descripcion.Length > 500)
                throw new DominioException("La descripción no puede superar los 500 caracteres.");

            Descripcion = descripcion?.Trim() ?? string.Empty;
        }
    }
}

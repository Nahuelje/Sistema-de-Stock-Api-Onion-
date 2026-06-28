namespace SistemaApiRest.Dominio.Entidades
{
    public class Categoria
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }

        public Categoria() { Nombre = ""; Descripcion = ""; }

        public Categoria(string nombre, string descripcion)
        {
            EstablecerNombre(nombre);
            EstablecerDescripcion(descripcion);
        }

        public void EstablecerNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Excepciones.DominioException("El nombre no puede estar vacío.");
            if (nombre.Length > 100)
                throw new Excepciones.DominioException("Nombre muy largo (max 100).");
            Nombre = nombre.Trim();
        }

        public void EstablecerDescripcion(string descripcion)
        {
            Descripcion = descripcion?.Trim() ?? "";
        }

        public void EstablecerId(int id)
        {
            if (id <= 0) throw new Excepciones.DominioException("Id inválido.");
            Id = id;
        }
    }
}
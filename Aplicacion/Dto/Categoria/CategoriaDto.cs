namespace SistemaApiRest.Aplicacion.Dto.Categoria
{
    /// DTO de salida para Categoria.
    /// Expone solo los datos que la API devuelve al cliente.

    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}

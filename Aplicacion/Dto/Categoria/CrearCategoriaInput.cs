namespace SistemaApiRest.Aplicacion.Dto.Categoria
{

    /// DTO de entrada para crear una Categoria.
    /// Recibe los datos del cliente via HTTP POST.

    public class CrearCategoriaInput
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}

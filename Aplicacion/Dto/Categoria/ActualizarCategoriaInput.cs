namespace SistemaApiRest.Aplicacion.Dto.Categoria
{
    /// DTO de entrada para actualizar una Categoria (PATCH parcial).
    /// Los campos null se ignoran — solo se actualizan los enviados.
    public class ActualizarCategoriaInput
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}

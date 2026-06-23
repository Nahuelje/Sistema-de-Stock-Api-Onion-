namespace SistemaApiRest.Aplicacion.Dto.Producto
{

    /// DTO de entrada para actualizar los datos descriptivos de un Producto.
    /// Todos los campos son opcionales (null = no actualizar ese campo).
    /// NOTA: el stock NO se modifica desde aquí. Usar los endpoints
    ///   PATCH /api/productos/{id}/stock/depositar
    ///   PATCH /api/productos/{id}/stock/descontar

    public class ActualizarProductoInput
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? CategoriaId { get; set; }
    }
}

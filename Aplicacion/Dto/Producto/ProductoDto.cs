namespace SistemaApiRest.Aplicacion.Dto.Producto
{

    /// DTO de salida para Producto.
    /// Incluye el nombre de la categoría (datos desnormalizados para comodidad del cliente).

    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;
    }
}

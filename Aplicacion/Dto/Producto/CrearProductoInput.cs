namespace SistemaApiRest.Aplicacion.Dto.Producto
{

    /// DTO de entrada para crear un Producto.

    public class CrearProductoInput
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
    }
}

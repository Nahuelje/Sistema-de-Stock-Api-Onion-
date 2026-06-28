namespace SistemaApiRest.Aplicacion.Dto;

public class CrearProductoInput
{
    public string Nombre { get; set; } = "";
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public int CategoriaId { get; set; }
}
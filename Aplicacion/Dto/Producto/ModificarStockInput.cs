namespace SistemaApiRest.Aplicacion.Dto.Producto
{
    /// DTO de entrada para las operaciones de stock (depositar o descontar).
    /// Se comparte entre ambos servicios ya que los dos reciben solo una cantidad.

    public class ModificarStockInput
    {
        /// <summary>Cantidad de unidades a depositar o descontar. Debe ser mayor que cero.</summary>
        public int Cantidad { get; set; }
    }
}

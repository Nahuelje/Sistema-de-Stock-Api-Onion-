using SistemaApiRest.Dominio.Entidades;

namespace SistemaApiRest.Dominio.Interfaces;

public interface IRepositorioProducto
{
    int Agregar(Producto producto);
    Producto? ObtenerPorId(int id);
    IEnumerable<Producto> ObtenerTodos();
    void Actualizar(Producto producto);
    void Eliminar(int id);
    bool ExistenProductosEnCategoria(int categoriaId);
}






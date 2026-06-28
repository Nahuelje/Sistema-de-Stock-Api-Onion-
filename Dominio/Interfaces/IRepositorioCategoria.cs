using SistemaApiRest.Dominio.Entidades;

public interface IRepositorioCategoria
{
    int Agregar(Categoria categoria);
    Categoria? ObtenerPorId(int id);
    IEnumerable<Categoria> ObtenerTodas();
    void Actualizar(Categoria categoria);
    void Eliminar(int id);
    bool ExisteConNombre(string nombre);
}
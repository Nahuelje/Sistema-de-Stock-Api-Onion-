using SistemaApiRest.Dominio.Entidades;

namespace SistemaApiRest.Dominio.Interfaces
{

    /// Contrato del repositorio de Categorías.
    /// Definido en el Dominio: el dominio no sabe cómo se implementa,
    /// solo sabe qué operaciones necesita. La implementación vive en Persistencia.
    /// </summary>
    public interface IRepositorioCategoria
    {
        /// <summary>Agrega una nueva categoría y retorna el Id generado.</summary>
        int Agregar(Categoria categoria);

        /// <summary>Obtiene una categoría por su Id. Retorna null si no existe.</summary>
        Categoria? ObtenerPorId(int id);

        /// <summary>Retorna todas las categorías disponibles.</summary>
        IEnumerable<Categoria> ObtenerTodas();

        /// <summary>Verifica si ya existe una categoría con el nombre dado (para unicidad).</summary>
        bool ExisteConNombre(string nombre);

        /// <summary>Actualiza los datos de una categoría existente.</summary>
        void Actualizar(Categoria categoria);

        /// <summary>Elimina una categoría por Id.</summary>
        void Eliminar(int id);
    }
}

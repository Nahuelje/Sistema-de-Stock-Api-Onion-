using SistemaApiRest.Dominio.Entidades;

namespace SistemaApiRest.Dominio.Interfaces
{

    /// Contrato del repositorio de Productos.
    /// Definido en el Dominio: abstraído de cualquier implementación concreta.

    public interface IRepositorioProducto
    {
        /// <summary>Agrega un nuevo producto y retorna el Id generado.</summary>
        int Agregar(Producto producto);

        /// <summary>Obtiene un producto por su Id. Retorna null si no existe.</summary>
        Producto? ObtenerPorId(int id);

        /// <summary>Retorna todos los productos.</summary>
        IEnumerable<Producto> ObtenerTodos();

        /// <summary>Retorna productos filtrando por categoría.</summary>
        IEnumerable<Producto> ObtenerPorCategoria(int categoriaId);

        /// <summary>Actualiza los datos de un producto existente.</summary>
        void Actualizar(Producto producto);

        /// <summary>Elimina un producto por Id.</summary>
        void Eliminar(int id);

        /// <summary>Verifica si existen productos asociados a una categoría (para validar eliminación).</summary>
        bool ExistenProductosEnCategoria(int categoriaId);

        /// <summary>Verifica si ya existe un producto con el nombre dado (para unicidad).</summary>
        bool ExisteConNombre(string nombre);
    }
}

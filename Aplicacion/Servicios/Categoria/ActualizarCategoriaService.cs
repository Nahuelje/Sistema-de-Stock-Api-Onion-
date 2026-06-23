using SistemaApiRest.Aplicacion.Dto.Categoria;
using SistemaApiRest.Dominio.Excepciones;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Categoria
{
    /// Caso de uso: Actualizar parcialmente una categoría (PATCH).
    /// Solo modifica los campos enviados (los null se ignoran).
    /// Valida unicidad de nombre si se quiere cambiar.
    public class ActualizarCategoriaService
    {
        private readonly IRepositorioCategoria _repositorioCategoria;

        public ActualizarCategoriaService(IRepositorioCategoria repositorioCategoria)
        {
            _repositorioCategoria = repositorioCategoria;
        }

        /// Retorna false si la categoría no existe.
        /// Lanza DominioException si el nuevo nombre ya está en uso.
        public bool Ejecutar(int id, ActualizarCategoriaInput input)
        {
            var categoria = _repositorioCategoria.ObtenerPorId(id);

            if (categoria == null)
                return false;

            // Solo actualiza el nombre si fue enviado
            if (input.Nombre != null)
            {
                // Valida unicidad: no puede usar el nombre de otra categoría
                bool nombreYaUsado = _repositorioCategoria.ExisteConNombre(input.Nombre)
                    && !categoria.Nombre.Equals(input.Nombre.Trim(), StringComparison.OrdinalIgnoreCase);

                if (nombreYaUsado)
                    throw new DominioException($"Ya existe una categoría con el nombre '{input.Nombre}'.");

                categoria.EstablecerNombre(input.Nombre);
            }

            // Solo actualiza la descripción si fue enviada
            if (input.Descripcion != null)
                categoria.EstablecerDescripcion(input.Descripcion);

            _repositorioCategoria.Actualizar(categoria);
            return true;
        }
    }
}

using SistemaApiRest.Aplicacion.Dto.Categoria;
using SistemaApiRest.Dominio.Excepciones;
using SistemaApiRest.Dominio.Interfaces;
using EntidadCategoria = SistemaApiRest.Dominio.Entidades.Categoria;

namespace SistemaApiRest.Aplicacion.Servicios.Categoria
{

    /// Caso de uso: Crear una nueva categoría.
    /// Valida que el nombre sea único antes de persistirla.

    public class CrearCategoriaService
    {
        private readonly IRepositorioCategoria _repositorioCategoria;

        public CrearCategoriaService(IRepositorioCategoria repositorioCategoria)
        {
            _repositorioCategoria = repositorioCategoria;
        }


        /// Ejecuta el caso de uso. Retorna el Id de la categoría creada.

        public int Ejecutar(CrearCategoriaInput input)
        {
            // Regla de negocio: no puede haber dos categorías con el mismo nombre
            if (_repositorioCategoria.ExisteConNombre(input.Nombre))
                throw new DominioException($"Ya existe una categoría con el nombre '{input.Nombre}'.");

            var categoria = new EntidadCategoria(input.Nombre, input.Descripcion);

            return _repositorioCategoria.Agregar(categoria);
        }
    }
}

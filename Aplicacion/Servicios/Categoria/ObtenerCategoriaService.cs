using SistemaApiRest.Aplicacion.Dto.Categoria;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Categoria
{
    /// Caso de uso: Obtener una categoría por su Id.
    /// Retorna null si no existe (el controller decide el 404).
    public class ObtenerCategoriaService
    {
        private readonly IRepositorioCategoria _repositorioCategoria;

        public ObtenerCategoriaService(IRepositorioCategoria repositorioCategoria)
        {
            _repositorioCategoria = repositorioCategoria;
        }

        public CategoriaDto? Ejecutar(int id)
        {
            var categoria = _repositorioCategoria.ObtenerPorId(id);

            if (categoria == null)
                return null;

            return new CategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };
        }
    }
}

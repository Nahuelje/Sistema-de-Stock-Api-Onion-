using SistemaApiRest.Aplicacion.Dto.Categoria;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Categoria
{

    /// Caso de uso: Obtener todas las categorías.
    /// Mapea entidades de dominio a DTOs de salida.

    public class ObtenerCategoriasService
    {
        private readonly IRepositorioCategoria _repositorioCategoria;

        public ObtenerCategoriasService(IRepositorioCategoria repositorioCategoria)
        {
            _repositorioCategoria = repositorioCategoria;
        }


        /// Retorna todas las categorías mapeadas a DTOs.

        public IEnumerable<CategoriaDto> Ejecutar()
        {
            var categorias = _repositorioCategoria.ObtenerTodas();

            // Mapeo manual: Entidad → DTO
            // En proyectos más grandes se usa AutoMapper o Mapster
            return categorias.Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            });
        }
    }
}

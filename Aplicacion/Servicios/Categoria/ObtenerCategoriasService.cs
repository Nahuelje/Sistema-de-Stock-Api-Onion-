using SistemaApiRest.Aplicacion.Dto;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Categoria;

public class ObtenerCategoriasService
{
    private readonly IRepositorioCategoria _repositorio;

    public ObtenerCategoriasService(IRepositorioCategoria repositorio)
    {
        _repositorio = repositorio;
    }

    public IEnumerable<CategoriaDto> Ejecutar()
    {
        return _repositorio.ObtenerTodas().Select(c => new CategoriaDto
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Descripcion = c.Descripcion
        });
    }
}
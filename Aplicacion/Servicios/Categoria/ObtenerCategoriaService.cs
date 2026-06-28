using SistemaApiRest.Aplicacion.Dto;
using SistemaApiRest.Dominio.Excepciones;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Categoria;

public class ObtenerCategoriaService
{
    private readonly IRepositorioCategoria _repositorio;

    public ObtenerCategoriaService(IRepositorioCategoria repositorio)
    {
        _repositorio = repositorio;
    }

    public CategoriaDto Ejecutar(int id)
    {
        var categoria = _repositorio.ObtenerPorId(id)
            ?? throw new DominioException($"No existe una categoría con Id {id}.");

        return new CategoriaDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion
        };
    }
}
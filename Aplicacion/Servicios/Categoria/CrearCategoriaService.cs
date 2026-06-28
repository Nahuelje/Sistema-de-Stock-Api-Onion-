using SistemaApiRest.Aplicacion.Dto;
using SistemaApiRest.Dominio.Excepciones;
using SistemaApiRest.Dominio.Interfaces;
using CategoriaEntidad = SistemaApiRest.Dominio.Entidades.Categoria;

namespace SistemaApiRest.Aplicacion.Servicios.Categoria;

public class CrearCategoriaService
{
    private readonly IRepositorioCategoria _repositorio;

    public CrearCategoriaService(IRepositorioCategoria repositorio)
    {
        _repositorio = repositorio;
    }

    public int Ejecutar(CrearCategoriaInput input)
    {
        if (_repositorio.ExisteConNombre(input.Nombre))
            throw new DominioException("Ya existe una categoría con ese nombre.");

        var categoria = new CategoriaEntidad(input.Nombre, input.Descripcion);
        return _repositorio.Agregar(categoria);
    }
}
using SistemaApiRest.Dominio.Excepciones;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Aplicacion.Servicios.Categoria;

public class EliminarCategoriaService
{
    private readonly IRepositorioCategoria _repositorio;

    public EliminarCategoriaService(IRepositorioCategoria repositorio)
    {
        _repositorio = repositorio;
    }

    public void Ejecutar(int id)
    {
        var categoria = _repositorio.ObtenerPorId(id)
            ?? throw new DominioException($"No existe una categoría con Id {id}.");

        _repositorio.Eliminar(categoria.Id);
    }
}
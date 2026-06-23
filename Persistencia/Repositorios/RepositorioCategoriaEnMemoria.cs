using SistemaApiRest.Dominio.Entidades;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Persistencia.Repositorios
{

    /// Implementación del repositorio de Categorías usando almacenamiento en memoria.
    /// Registrada como Singleton para que el estado persista durante la vida de la app.
    /// En un proyecto real, aquí iría la implementación con EF Core o Dapper.

    public class RepositorioCategoriaEnMemoria : IRepositorioCategoria
    {
        // Diccionario como almacén: clave = Id, valor = Categoria
        private readonly Dictionary<int, Categoria> _categorias = new();
        private int _proximoId = 1;

        public RepositorioCategoriaEnMemoria()
        {
            // Datos de prueba 
            SeedDatosDePrueba();
        }

        private void SeedDatosDePrueba()
        {
            var electronica = new Categoria("Electrónica", "Dispositivos electrónicos y accesorios tecnológicos");
            electronica.EstablecerId(_proximoId++);
            _categorias[electronica.Id] = electronica;

            var ropa = new Categoria("Ropa", "Indumentaria para todas las edades y estilos");
            ropa.EstablecerId(_proximoId++);
            _categorias[ropa.Id] = ropa;

            var alimentos = new Categoria("Alimentos", "Productos alimenticios y bebidas");
            alimentos.EstablecerId(_proximoId++);
            _categorias[alimentos.Id] = alimentos;
        }

        public int Agregar(Categoria categoria)
        {
            categoria.EstablecerId(_proximoId++);
            _categorias[categoria.Id] = categoria;
            return categoria.Id;
        }

        public Categoria? ObtenerPorId(int id)
        {
            _categorias.TryGetValue(id, out var categoria);
            return categoria;
        }

        public IEnumerable<Categoria> ObtenerTodas()
        {
            return _categorias.Values.ToList();
        }

        public bool ExisteConNombre(string nombre)
        {
            return _categorias.Values.Any(c =>
                c.Nombre.Equals(nombre.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public void Actualizar(Categoria categoria)
        {
            _categorias[categoria.Id] = categoria;
        }

        public void Eliminar(int id)
        {
            _categorias.Remove(id);
        }
    }
}

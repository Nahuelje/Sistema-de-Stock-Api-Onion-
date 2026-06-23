using SistemaApiRest.Dominio.Entidades;
using SistemaApiRest.Dominio.Interfaces;

namespace SistemaApiRest.Persistencia.Repositorios
{
    /// <summary>
    /// Implementación del repositorio de Productos usando almacenamiento en memoria.
    /// Registrado como Singleton: el estado persiste mientras la app esté corriendo.
    /// </summary>
    public class RepositorioProductoEnMemoria : IRepositorioProducto
    {
        private readonly Dictionary<int, Producto> _productos = new();
        private int _proximoId = 1;

        public RepositorioProductoEnMemoria()
        {
            // Datos de prueba pre-cargados (usan los Ids de las categorías del seed de Categoria)
            SeedDatosDePrueba();
        }

        private void SeedDatosDePrueba()
        {
            // Categoría 1 = Electrónica, Categoría 2 = Ropa, Categoría 3 = Alimentos
            var productos = new List<Producto>
            {
                new Producto("Laptop Ultrabook Pro", "Laptop de alto rendimiento con procesador i7, 16GB RAM", 1299.99m, 15, 1),
                new Producto("Auriculares Bluetooth", "Auriculares inalámbricos con cancelación de ruido", 89.99m, 50, 1),
                new Producto("Smartphone X12", "Teléfono inteligente con pantalla AMOLED 6.5\"", 699.00m, 30, 1),
                new Producto("Remera de Algodón", "Remera 100% algodón, disponible en varios colores", 19.99m, 200, 2),
                new Producto("Zapatillas Running", "Calzado deportivo con suela amortiguadora", 79.99m, 75, 2),
                new Producto("Arroz Integral 1kg", "Arroz integral de primera calidad", 2.49m, 500, 3),
                new Producto("Café Molido Premium", "Blend de café arábica molido, tueste medio", 12.99m, 150, 3),
            };

            foreach (var producto in productos)
            {
                producto.EstablecerId(_proximoId++);
                _productos[producto.Id] = producto;
            }
        }

        public int Agregar(Producto producto)
        {
            producto.EstablecerId(_proximoId++);
            _productos[producto.Id] = producto;
            return producto.Id;
        }

        public Producto? ObtenerPorId(int id)
        {
            _productos.TryGetValue(id, out var producto);
            return producto;
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            return _productos.Values.ToList();
        }

        public IEnumerable<Producto> ObtenerPorCategoria(int categoriaId)
        {
            return _productos.Values
                .Where(p => p.CategoriaId == categoriaId)
                .ToList();
        }

        public void Actualizar(Producto producto)
        {
            // La entidad ya fue modificada por referencia en el servicio
            // Solo aseguramos que esté en el diccionario
            _productos[producto.Id] = producto;
        }

        public void Eliminar(int id)
        {
            _productos.Remove(id);
        }

        public bool ExistenProductosEnCategoria(int categoriaId)
        {
            return _productos.Values.Any(p => p.CategoriaId == categoriaId);
        }

        public bool ExisteConNombre(string nombre)
        {
            return _productos.Values.Any(p =>
                p.Nombre.Equals(nombre.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }
}

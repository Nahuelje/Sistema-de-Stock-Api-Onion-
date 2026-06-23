# Sistema API REST — Arquitectura Onion

API REST educativa construida con **ASP.NET Core (.NET 9)** siguiendo la **Arquitectura Onion**.

## Estructura del Proyecto

```
SistemaApiRest/
├── Dominio/                    ← Núcleo: sin dependencias externas
│   ├── Entidades/              ← Producto.cs, Categoria.cs
│   ├── Interfaces/             ← IRepositorioProducto, IRepositorioCategoria
│   └── Excepciones/            ← DominioException.cs
│
├── Aplicacion/                 ← Casos de uso (depende solo de Dominio)
│   ├── Dto/                    ← Inputs y outputs de la API
│   └── Servicios/              ← Un servicio por caso de uso
│
├── Persistencia/               ← Implementaciones de repositorios
│   └── Repositorios/           ← En memoria (fácil migrar a EF Core)
│
├── Presentacion/               ← Capa más externa
│   └── Controllers/            ← CategoriaController, ProductoController
│
└── Program.cs                  ← Composición root (IoC)
```

## Endpoints

### Categorías (`/api/categorias`)
| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/categorias` | Lista todas las categorías |
| POST | `/api/categorias` | Crea una nueva categoría |
| DELETE | `/api/categorias/{id}` | Elimina categoría (sin productos) |

### Productos (`/api/productos`)
| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/productos` | Lista todos los productos |
| GET | `/api/productos?categoriaId=1` | Filtra por categoría |
| GET | `/api/productos/{id}` | Obtiene producto por Id |
| POST | `/api/productos` | Crea nuevo producto |
| PATCH | `/api/productos/{id}` | Actualización parcial de datos |
| PATCH | `/api/productos/{id}/stock/depositar` | Agrega unidades al stock |
| PATCH | `/api/productos/{id}/stock/descontar` | Descuenta unidades del stock |
| DELETE | `/api/productos/{id}` | Elimina producto |

## Cómo ejecutar

```bash
dotnet run
# Abrir: https://localhost:{puerto}/swagger
```

## Reglas de negocio implementadas

- No se puede crear una categoría con nombre duplicado
- No se puede eliminar una categoría que tenga productos asociados
- El precio de un producto debe ser > 0
- El stock no puede ser negativo
- Al crear un producto, la categoría debe existir
- La actualización es parcial: solo se modifican los campos enviados

## Próximos pasos para extender

1. ~~Agregar `DepositarStockService` y `DescontarStockService`~~ ✅ Completado
2. Reemplazar repositorios en memoria por EF Core + SQL Server
3. Agregar validaciones con FluentValidation
4. Agregar autenticación con JWT
5. Separar en proyectos `.csproj` independientes por capa

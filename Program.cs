using Microsoft.EntityFrameworkCore;
using SistemaApiRest.Aplicacion.Servicios.Categoria;
using SistemaApiRest.Aplicacion.Servicios.Producto;
using SistemaApiRest.Dominio.Excepciones;
using SistemaApiRest.Dominio.Interfaces;
using SistemaApiRest.Persistencia.Contexto;
using SistemaApiRest.Persistencia.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// ──────────────────────────────────────────────
// REGISTRO DE DEPENDENCIAS (IoC Container)
// ──────────────────────────────────────────────

// ════════════════════════════════════════════════════════════
// OPCIÓN A — En Memoria (desactivada, para desarrollo rápido)
//   Ventaja: corre sin BD, tiene seed data de prueba.
// ════════════════════════════════════════════════════════════
// builder.Services.AddSingleton<IRepositorioCategoria, RepositorioCategoriaEnMemoria>();
// builder.Services.AddSingleton<IRepositorioProducto, RepositorioProductoEnMemoria>();

// ════════════════════════════════════════════════════════════
// OPCIÓN B — EF Core + MySQL (ACTIVA)
//   Requiere MySQL corriendo y la base de datos creada.
//   Correr las migraciones:
//        dotnet ef migrations add InitialCreate
//        dotnet ef database update
// ════════════════════════════════════════════════════════════
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));
builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEf>();
builder.Services.AddScoped<IRepositorioProducto, RepositorioProductoEf>();

// Servicios de Categoria (casos de uso)
builder.Services.AddScoped<CrearCategoriaService>();
builder.Services.AddScoped<ObtenerCategoriasService>();
builder.Services.AddScoped<EliminarCategoriaService>();

// Servicios de Producto (casos de uso)
builder.Services.AddScoped<CrearProductoService>();
builder.Services.AddScoped<ObtenerProductoService>();
builder.Services.AddScoped<ObtenerTodosProductosService>();
builder.Services.AddScoped<ActualizarProductoService>();
builder.Services.AddScoped<EliminarProductoService>();
builder.Services.AddScoped<DepositarStockService>();
builder.Services.AddScoped<DescontarStockService>();
builder.Services.AddScoped<ObtenerProductosStockBajoService>();

// Servicios de Categoria nuevos
builder.Services.AddScoped<ObtenerCategoriaService>();
builder.Services.AddScoped<ActualizarCategoriaService>();

// Infraestructura de la API
builder.Services.AddControllers();
builder.Services.AddOpenApi();


// ──────────────────────────────────────────────
// PIPELINE HTTP
// ──────────────────────────────────────────────

var app = builder.Build();

// ──────────────────────────────────────────────
// MANEJO GLOBAL DE EXCEPCIONES
// Captura cualquier excepción no controlada y devuelve JSON limpio.
// ──────────────────────────────────────────────
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        var mensaje = error?.Error is DominioException
            ? error.Error.Message
            : "Ocurrió un error interno en el servidor.";
        await context.Response.WriteAsJsonAsync(new { mensaje, codigo = 500 });
    });
});

if (app.Environment.IsDevelopment())
{
    // .NET 10 genera el spec OpenAPI en: /openapi/v1.json
    app.MapOpenApi();

    // Swagger UI apunta a ese spec nativo (sin necesitar SwaggerGen)
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Sistema API REST v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

// Redirigir raíz → Swagger para comodidad en desarrollo
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapControllers();

app.Run();

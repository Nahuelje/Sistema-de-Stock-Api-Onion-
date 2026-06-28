using SistemaApiRest.Aplicacion.Servicios.Categoria;
using SistemaApiRest.Aplicacion.Servicios.Producto;
using SistemaApiRest.Dominio.Interfaces;
using SistemaApiRest.Persistencia.Repositorios;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IRepositorioCategoria, RepositorioCategoriaEnMemoria>();
builder.Services.AddSingleton<IRepositorioProducto, RepositorioProductoEnMemoria>();


builder.Services.AddScoped<CrearCategoriaService>();
builder.Services.AddScoped<ObtenerCategoriasService>();
builder.Services.AddScoped<ObtenerCategoriaService>();
builder.Services.AddScoped<EliminarCategoriaService>();


builder.Services.AddScoped<CrearProductoService>();
builder.Services.AddScoped<ObtenerTodosProductosService>();
builder.Services.AddScoped<ObtenerProductoService>();
builder.Services.AddScoped<EliminarProductoService>();
builder.Services.AddScoped<ModificarStockService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Sistema Stock API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapControllers();
app.Run();

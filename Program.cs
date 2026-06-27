using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ────────────────────────────────────────────────────────────
// REGISTRO DE DEPENDENCIAS
// Acá vas a agregar tus repositorios y servicios a medida que
// los crees en las capas Persistencia y Aplicacion.
// ────────────────────────────────────────────────────────────

// TODO: registrar DbContext cuando crees AppDbContext
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseMySql(
//         builder.Configuration.GetConnectionString("DefaultConnection"),
//         ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
//     ));

// TODO: registrar repositorios
// builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEf>();
// builder.Services.AddScoped<IRepositorioProducto, RepositorioProductoEf>();

// TODO: registrar servicios de aplicación
// builder.Services.AddScoped<CrearCategoriaService>();
// ...

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// ────────────────────────────────────────────────────────────
// PIPELINE HTTP
// ────────────────────────────────────────────────────────────

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

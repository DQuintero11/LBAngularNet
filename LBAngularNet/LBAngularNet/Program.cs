using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Configurar URLs explícitamente
//builder.WebHost.UseUrls("http://localhost:4000", "https://localhost:4001");


// Add Swagger Config
AddSwaggerConfig();

// Add services to the container.
builder.Services.AddControllersWithViews();


// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:7102") // Puerto de Angular
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

//HABILITA CORS
app.UseCors("AllowAngular");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Habilita swagger en dllo
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
        c.RoutePrefix = string.Empty; // Para acceder directamente en "/"
    });
    app.UseHsts();
}

app.UseHttpsRedirection();

//app.MapGet("/hello", () => Results.Ok("Hola Mundo"))
//   .WithName("GetHello")
//   .WithOpenApi(); // Habilita documentación para este endpoint

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();




///
void AddSwaggerConfig()
{
    // Agregar servicios de Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Mi API con Controllers",
            Version = "v1",
            Description = "Ejemplo de API con Swagger en .NET 6+",
            Contact = new OpenApiContact
            {
                Name = "Tu Nombre",
                Email = "tu@email.com",
                Url = new Uri("https://tusitio.com")
            }
        });
    });
}


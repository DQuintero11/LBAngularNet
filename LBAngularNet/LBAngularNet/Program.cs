using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

AddSwaggerConfig();
AddControllersViews();
AddHttpDirections();

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
    app.UseSwaggerUI();
}


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
    builder.Services.AddSwaggerGen();
}

//
void AddHttpDirections()
{
    builder.Services.AddHttpsRedirection(options =>
    {
        options.HttpsPort = 7145;
    });
}


void AddControllersViews()
{
    // Add services to the container.
    builder.Services.AddControllersWithViews();
}
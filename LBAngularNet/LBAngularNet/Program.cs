using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nest;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


AddJWTConfig();
AddElasticSearch();
AddSwaggerConfig();
AddControllersViews();
AddDbContext();
AddDependencyInjectionServices();
AddDependencyInjectionRepositorys();
AddHttpDirections();
AddCors();

var app = builder.Build();

HabilitaCORS();
isDevelopment();

app.UseStaticFiles();
app.UseRouting();

AddMaps();

app.Run();






///
void AddSwaggerConfig()
{
    // Agregar servicios de Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

///
void AddHttpDirections()
{
    builder.Services.AddHttpsRedirection(options =>
    {
        options.HttpsPort = 7145;
    });
}

///
void AddControllersViews()
{
    // Add services to the container.
    builder.Services.AddControllersWithViews();
}

///
void AddDependencyInjectionServices()
{
    //builder.Services.AddScoped<EmployeeServices>();
    //builder.Services.AddScoped<ShipperServices>();
    //builder.Services.AddScoped<ProductsServices>();
    //builder.Services.AddScoped<OrdersServices>();
}

///
void AddDependencyInjectionRepositorys()
{

    //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    //builder.Services.AddScoped<IShipperRepository, ShipperRepository>();
    //builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
    //builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
}

///
void AddDbContext()
{
    //  builder.Services.AddDbContext<AppDbContext>(options =>
    //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

///
void AddCors()
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngular", policy =>
        {
            policy.WithOrigins("http://localhost:7102") // Puerto de Angular
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });
}

/// 
void HabilitaCORS()
{
    //HABILITA CORS
    app.UseCors("AllowAngular");
}

///
void isDevelopment()
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        // Habilita swagger en dllo
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}


///
void AddMaps()
{
    app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

    app.MapFallbackToFile("index.html");
}


///
void AddElasticSearch()
{
    var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("Demo");

    var elasticClient = new ElasticClient(settings);

    builder.Services.AddSingleton(elasticClient);   
}

void AddJWTConfig()
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

}
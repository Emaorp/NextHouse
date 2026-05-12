using NextHouse.Application;
using NextHouse.Persistence;
// Requiere instalar el paquete NuGet "Swashbuckle.AspNetCore"
// dotnet add package Swashbuckle.AspNetCore
using Swashbuckle.AspNetCore.SwaggerGen;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// --- 1. AGREGADO: Configuración de Swagger ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// ---------------------------------------------

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// --- 2. AGREGADO: Habilitar Swagger en la tubería ---
// Lo ponemos fuera del if para que siempre lo puedas ver mientras estudias
app.UseSwagger();
app.UseSwaggerUI();
// ---------------------------------------------------

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
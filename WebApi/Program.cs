using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. Registrar Capas (Clean Architecture)
// Esto mantiene el Program.cs limpio y delega la responsabilidad a cada capa
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// 2. Registrar servicios de la API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3. Configurar el Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
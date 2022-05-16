using Microsoft.EntityFrameworkCore;
using Unach.Inventario.API.Helpers;

var builder = WebApplication.CreateBuilder(args);
/*
    * Asignar la cadena de conexión de la base de datos obtenida 
    * desde AppSettings
*/
ContextDB.CadenaConexion = builder.Configuration.GetConnectionString( "BDConexion" );
builder.Services.AddDbContext<InventarioAPIContext>( opt => opt.UseSqlServer( ContextDB.CadenaConexion ));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

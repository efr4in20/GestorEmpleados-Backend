using GestorEmpleados.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<DbContext>(options =>
//{
//    options.UseSqlServer((builder.Configuration.GetConnectionString("LCPVconnection")),
//    sqlServerOptionsAction: sqlOptions =>
//    {
//        sqlOptions.EnableRetryOnFailure(
//        maxRetryCount: 10,
//        maxRetryDelay: TimeSpan.FromSeconds(3600),
//        errorNumbersToAdd: null);
//        sqlOptions.CommandTimeout(3600);
//    });
//});
builder.Services.AddSingleton<EmpleadoData>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");
app.UseAuthorization();

app.MapControllers();

app.Run();

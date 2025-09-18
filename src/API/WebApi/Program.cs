//using WebApi.Application.Configuration;
using API_CMB.src.API.WebApi.Middleware;
using Dapper;
using Domain.DomainInterfaces;
using Domain.RepositoriesInterfaces;
using Infraestructure.Persistence;
using Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Linq;
using System.Reflection;
using WebApi.Application.Mapping;
using WebApi.Application.Services;
using WebApi.Application.Services.Centro;
using WebApi.Application.Services.Estudios;
using WebApi.Application.Services.Examenes;
using WebApi.Application.Services.Personal;
using WebApi.Application.Services.Recursos;
using WebApi.Application.Services.Salas;
using WebApi.Application.Services.SystemUsersService;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Microsoft.AspNetCore.Mvc.ApiExplorer.EnableEnhancedModelMetadata", true);

// Configurar Npgsql para manejar DateTimeOffset con timezone UTC
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
NpgsqlConnection.GlobalTypeMapper.EnableDynamicJson();

// Configurar mapeo de columnas para Dapper
SqlMapper.Settings.CommandTimeout = 30;


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
//builder.Services.AddAutoMapper(typeof(Program).mapp);

builder.Services.AddAutoMapper(typeof(MappingProfile));



//Registro de Interfaces
builder.Services.AddScoped<IDapperWrapper, DapperWrapper>();
builder.Services.AddScoped<IArchivoDigitalRepository, ArchivoDigitalRepository>();
builder.Services.AddScoped<ICurrentArchivoDigital, CurrentArchivoDigitalService>();
builder.Services.AddScoped<IArchivoDigitalService, ArchivoDigitalService>();
builder.Services.AddScoped<ISystemUsersRepository, SystemUsersRepository>();
builder.Services.AddScoped<ISystemUsersService, SystemUsersService>();
builder.Services.AddScoped<ICentroRepository, CentroRepository>();
builder.Services.AddScoped<ICentroService, Centroservice>();
builder.Services.AddScoped<IPersonalRepository, PersonalRepository>();
builder.Services.AddScoped<IPersonalService, PersonalService>();
builder.Services.AddScoped<IEstudiosRepository, EstudiosRepository>();
builder.Services.AddScoped<IEstudiosService, EstudiosService>();
builder.Services.AddScoped<ISalasRepository, SalasRepository>();
builder.Services.AddScoped<ISalasService, SalasService>();
builder.Services.AddScoped<IRecursosRepository, RecursosRepository>();
builder.Services.AddScoped<IRecursosService, RecursosService>();
builder.Services.AddScoped<IExamenesRepository, ExamenesRepository>();
builder.Services.AddScoped<IExamenesService, ExamenesService>();

var app = builder.Build();

app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API INTELGASTRO CMB");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "Web INTELGASTRO CMB";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Agregar middleware de manejo de errores globales
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

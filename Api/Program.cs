using Application.Common.Interfaces;
using Application.Services;
using Application.Validators;
using Infraestructure.Persistence;
using Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// 1. Inyección del DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("db")));

// 2. Registro de Repositorios (Adaptadores Salientes)
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<CustomerSerivce>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

// 5. Configuración de Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Management API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseCors("Angular");
app.UseAuthorization();

try
{
    app.MapControllers();
}

catch(System.Reflection.ReflectionTypeLoadException ex)
{
    Console.WriteLine("CARGA ERRONEAS DE TIPOS");
    foreach(var dev in ex.LoaderExceptions)
    {
        Console.WriteLine($"->{dev?.Message}");
    }

    throw;
}

app.Run();
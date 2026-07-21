using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NaturaHub.Application.Interfaces;
using NaturaHub.Application.Services;
using NaturaHub.Domain.Interfaces;
using NaturaHub.Infrastructure.Data;
using NaturaHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ==========================================
// 1. Configuração do Banco de Dados
// ==========================================
builder.Services.AddDbContext<NaturaHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ==========================================
// 2. Injeção de Dependência (DI) - Repositórios
// ==========================================
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();

// ==========================================
// 3. Injeção de Dependência (DI) - Serviços
// ==========================================
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStockMovementService, StockMovementService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();


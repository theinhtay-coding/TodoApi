using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Register ProductRepository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Register Todo endpoints
app.MapTodoEndpoints();

// Register Product endpoints
app.MapProductEndpoints();

app.Run();

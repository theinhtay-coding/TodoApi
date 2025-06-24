using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Repositories;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    //.WriteTo.File($"Logs/log-{DateTime.Now:yyyyMMdd}.txt")
    .Enrich.FromLogContext()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (error != null)
        {
            Log.Error(error.Error, "Unhandled exception occurred");
            var result = System.Text.Json.JsonSerializer.Serialize(new
            {
                error = "An unexpected error occurred.",
                detail = error.Error.Message
            });
            await context.Response.WriteAsync(result);
        }
    });
});

app.MapGet("/", (ILogger<Program> logger) =>
{
    logger.LogInformation("Hello World endpoint was called.");
    return "Hello World!";
});

app.MapTodoEndpoints();
app.MapProductEndpoints();

app.Run();

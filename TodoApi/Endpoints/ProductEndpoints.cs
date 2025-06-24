using Microsoft.AspNetCore.Builder;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Results;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IProductRepository repo, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("ProductEndpoints");
            logger.LogInformation("GET /products endpoint called.");

            var result = await repo.GetAllAsync();
            return result.Success ? Results.Ok(result.Data) : Results.Problem(result.Error);
        });

        group.MapGet("/{id:int}", async (int id, IProductRepository repo) =>
        {
            var result = await repo.GetByIdAsync(id);
            return result.Success ? Results.Ok(result.Data) : Results.NotFound(result.Error);
        });

        group.MapPost("/", async (Product product, IProductRepository repo) =>
        {
            if (product is null)
                return Results.BadRequest("Product cannot be null.");

            var result = await repo.AddAsync(product);
            return result.Success
                ? Results.Created($"/products/{result.Data!.ProductId}", result.Data)
                : Results.Problem(result.Error);
        });

        group.MapPut("/{id:int}", async (int id, Product product, IProductRepository repo) =>
        {
            var result = await repo.UpdateAsync(id, product);
            return result.Success ? Results.NoContent() : Results.NotFound(result.Error);
        });

        group.MapDelete("/{id:int}", async (int id, IProductRepository repo) =>
        {
            var result = await repo.DeleteAsync(id);
            return result.Success ? Results.NoContent() : Results.NotFound(result.Error);
        });

        return group;
    }
}
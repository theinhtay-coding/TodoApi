using Microsoft.AspNetCore.Builder;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Results;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (IProductRepository repo) =>
        {
            var result = await repo.GetAllAsync();
            return result.Success ? Results.Ok(result.Data) : Results.Problem(result.Error);
        });

        app.MapGet("/products/{id}", async (int id, IProductRepository repo) =>
        {
            var result = await repo.GetByIdAsync(id);
            return result.Success ? Results.Ok(result.Data) : Results.NotFound(result.Error);
        });

        app.MapPost("/products", async (Product product, IProductRepository repo) =>
        {
            if (product is null)
                return Results.BadRequest("Product cannot be null.");

            var result = await repo.AddAsync(product);
            return result.Success
                ? Results.Created($"/products/{result.Data!.ProductId}", result.Data)
                : Results.Problem(result.Error);
        });

        app.MapPut("/products/{id}", async (int id, Product product, IProductRepository repo) =>
        {
            var result = await repo.UpdateAsync(id, product);
            return result.Success ? Results.NoContent() : Results.NotFound(result.Error);
        });

        app.MapDelete("/products/{id}", async (int id, IProductRepository repo) =>
        {
            var result = await repo.DeleteAsync(id);
            return result.Success ? Results.NoContent() : Results.NotFound(result.Error);
        });
    }
}
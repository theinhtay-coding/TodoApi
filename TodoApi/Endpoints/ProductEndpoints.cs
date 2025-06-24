using Microsoft.AspNetCore.Builder;
using TodoApi.Models;
using TodoApi.Repositories;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (IProductRepository repo) =>
            Results.Ok(await repo.GetAllAsync()));

        app.MapGet("/products/{id}", async (int id, IProductRepository repo) =>
        {
            var product = await repo.GetByIdAsync(id);
            return product is not null ? Results.Ok(product) : Results.NotFound();
        });

        app.MapPost("/products", async (Product product, IProductRepository repo) =>
        {
            if (product is null)
            {
                return Results.BadRequest("Product cannot be null.");
            }
            var created = await repo.AddAsync(product);
            return Results.Created($"/products/{created.ProductId}", created);
        });

        app.MapPut("/products/{id}", async (int id, Product product, IProductRepository repo) =>
        {
            var updated = await repo.UpdateAsync(id, product);
            return updated ? Results.NoContent() : Results.NotFound();
        });

        app.MapDelete("/products/{id}", async (int id, IProductRepository repo) =>
        {
            var deleted = await repo.DeleteAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}
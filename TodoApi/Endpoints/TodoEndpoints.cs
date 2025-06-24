using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

public static class TodoEndpoints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/todoitems", async (AppDbContext db) =>
            await db.Todos.ToListAsync());

        app.MapGet("/todoitems/complete", async (AppDbContext db) =>
            await db.Todos.Where(t => t.IsComplete).ToListAsync());

        app.MapGet("/todoitems/{id}", async (int id, AppDbContext db) =>
            await db.Todos.FindAsync(id) is Todo todo
                ? Results.Ok(todo)
                : Results.NotFound());

        app.MapPost("/todoitems", async (Todo todo, AppDbContext db) =>
        {
            db.Todos.Add(todo);
            await db.SaveChangesAsync();
            return Results.Created($"/todoitems/{todo.Id}", todo);
        });

        app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, AppDbContext db) =>
        {
            var todo = await db.Todos.FindAsync(id);

            if (todo is null) return Results.NotFound();

            todo.Name = inputTodo.Name;
            todo.IsComplete = inputTodo.IsComplete;

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        app.MapDelete("/todoitems/{id}", async (int id, AppDbContext db) =>
        {
            if (await db.Todos.FindAsync(id) is Todo todo)
            {
                db.Todos.Remove(todo);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }

            return Results.NotFound();
        });
    }
}
# TodoApi

A minimal API project using .NET 9, Entity Framework Core InMemory, and the repository pattern for managing Todo and Product entities.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022 or later

### Running the Project

1. Open the solution in Visual Studio.
2. Set `TodoApi` as the startup project.
3. Press `F5` or use `dotnet run` in the `TodoApi` project directory.

The API will be available at `https://localhost:7239` (or the port shown in your console).

## API Endpoints

### Todo Endpoints

- `GET /todoitems` - List all todos
- `GET /todoitems/complete` - List completed todos
- `GET /todoitems/{id}` - Get a todo by ID
- `POST /todoitems` - Create a new todo
- `PUT /todoitems/{id}` - Update a todo
- `DELETE /todoitems/{id}` - Delete a todo

### Product Endpoints

- `GET /products` - List all products
- `GET /products/{id}` - Get a product by ID
- `POST /products` - Create a new product
- `PUT /products/{id}` - Update a product
- `DELETE /products/{id}` - Delete a product

## Example HTTP Requests

You can use the `TodoApi.http` file for sample requests, or tools like Postman/curl.

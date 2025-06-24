# TodoApi

A minimal API project using .NET 9, Entity Framework Core InMemory, the repository pattern, Serilog logging, and the Result pattern for managing Todo and Product entities.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022 or later

### Running the Project

1. Open the solution in Visual Studio.
2. Set `TodoApi` as the startup project.
3. Press `F5` or use `dotnet run` in the `TodoApi` project directory.

The API will be available at `https://localhost:7239` (or the port shown in your console).

## Features

### Serilog Logging

- The project uses [Serilog](https://serilog.net/) for structured logging.
- Logs are written to both the console and rolling log files in the `Logs` directory.
- Logging is used in repositories and endpoints for important operations and error tracking.
- Example log statements:
  - When a product is added, updated, deleted, or not found.
  - When endpoints are called.

### Result Pattern

- The project uses a generic `Result<T>` class to encapsulate operation outcomes.
- Each repository method returns a `Result<T>` indicating success, data, and error messages.
- Endpoints use the result to return consistent HTTP responses (e.g., 200 OK, 404 Not Found, 500 Problem).
- This pattern improves error handling and response consistency.

## API Endpoints

### Todo Endpoints

- `GET /todoitems` - List all todos
- `GET /todoitems/complete` - List completed todos
- `GET /todoitems/{id}` - Get a todo by ID
- `POST /todoitems` - Create a new todo
- `PUT /todoitems/{id}` - Update a todo
- `DELETE /todoitems/{id}` - Delete a todo

### Product Endpoints

- `GET /api/products` - List all products
- `GET /api/products/{id}` - Get a product by ID
- `POST /api/products` - Create a new product
- `PUT /api/products/{id}` - Update a product
- `DELETE /api/products/{id}` - Delete a product

## Example HTTP Requests

You can use the `TodoApi.http` file for sample requests, or tools like Postman/curl.

### Example: Create a Product

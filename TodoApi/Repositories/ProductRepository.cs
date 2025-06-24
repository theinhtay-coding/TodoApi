using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Results;

namespace TodoApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(AppDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result<List<Product>>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all products from the database.");
            var products = await _context.Products.ToListAsync();
            return Result<List<Product>>.Ok(products);
        }

        public async Task<Result<Product>> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching product with ID {ProductId}", id);
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _logger.LogInformation("Product found: {@Product}", product);
                return Result<Product>.Ok(product);
            }
            _logger.LogWarning("Product with ID {ProductId} not found.", id);
            return Result<Product>.Fail("Product not found.");
        }

        public async Task<Result<Product>> AddAsync(Product product)
        {
            _logger.LogInformation("Adding a new product: {@Product}", product);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Product added successfully: {@Product}", product);
            return Result<Product>.Ok(product);
        }

        public async Task<Result<bool>> UpdateAsync(int id, Product product)
        {
            _logger.LogInformation("Updating product with ID {ProductId}", id);
            var existing = await _context.Products.FindAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found.", id);
                return Result<bool>.Fail("Product not found.");
            }

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Product with ID {ProductId} updated successfully.", id);
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting product with ID {ProductId}", id);
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found.", id);
                return Result<bool>.Fail("Product not found.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Product with ID {ProductId} deleted successfully.", id);
            return Result<bool>.Ok(true);
        }
    }
}
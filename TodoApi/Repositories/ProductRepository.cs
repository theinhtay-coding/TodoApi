using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Results;

namespace TodoApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Product>>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            return Result<List<Product>>.Ok(products);
        }

        public async Task<Result<Product>> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product != null
                ? Result<Product>.Ok(product)
                : Result<Product>.Fail("Product not found.");
        }

        public async Task<Result<Product>> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Result<Product>.Ok(product);
        }

        public async Task<Result<bool>> UpdateAsync(int id, Product product)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null)
                return Result<bool>.Fail("Product not found.");

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;
            await _context.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return Result<bool>.Fail("Product not found.");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }
    }
}
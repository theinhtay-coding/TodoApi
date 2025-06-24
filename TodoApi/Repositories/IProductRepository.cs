using TodoApi.Models;
using TodoApi.Results;

namespace TodoApi.Repositories
{
    public interface IProductRepository
    {
        Task<Result<List<Product>>> GetAllAsync();
        Task<Result<Product>> GetByIdAsync(int id);
        Task<Result<Product>> AddAsync(Product product);
        Task<Result<bool>> UpdateAsync(int id, Product product);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
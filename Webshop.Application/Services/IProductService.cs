using Webshop.Domain.Entities;

namespace Webshop.Application.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> GetByNameAsync(string name);
    Task<bool> CreateAsync(Product product);
    Task<bool> UpdateAsync(Product product);
}
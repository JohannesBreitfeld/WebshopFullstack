using Webshop.Domain.Entities;

namespace Webshop.Application.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<bool> CreateAsync(Product product);
}
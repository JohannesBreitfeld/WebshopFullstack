using Webshop.Domain.Entities;

namespace Webshop.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> GetByNameAsync(string name);
    Task AddAsync(Product product);
    void Update(Product product);
    void Delete(Product product);
    Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids);
}

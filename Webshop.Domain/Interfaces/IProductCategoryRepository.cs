using Webshop.Domain.Entities;

namespace Webshop.Domain.Interfaces;

public interface IProductCategoryRepository
{
    Task<IEnumerable<ProductCategory>> GetAllAsync();
    Task<ProductCategory?> GetByIdAsync(int id);
    Task AddAsync(ProductCategory category);
    void Update(ProductCategory category);
    void Delete(ProductCategory category);
}

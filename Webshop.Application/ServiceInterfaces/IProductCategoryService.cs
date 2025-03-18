using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;
using Webshop.Domain.Entities;

namespace Webshop.Application.ServiceInterfaces;

public interface IProductCategoryService
{
    Task<IEnumerable<ProductCategory>> GetAllAsync();
    Task<ProductCategory?> GetByIdAsync(int id);
    Task<bool> CreateAsync(ProductCategory productCategory);
    Task<bool> UpdateAsync(ProductCategory productCategory);
    Task<bool> DeleteAsync(int id);
}

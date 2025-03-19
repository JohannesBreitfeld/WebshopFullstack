using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;
using Webshop.Domain.Entities;

namespace Webshop.Application.ServiceInterfaces;

public interface IProductCategoryService
{
    Task<ProductCategoriesResponse?> GetAllAsync();
    Task<ProductCategoryResponse?> GetByIdAsync(int id);
    Task<ProductCategoryResponse?> CreateAsync(CreateProductCategoryRequest request);
    Task<ProductCategoryResponse?> UpdateAsync(int id, UpdateProductCategoryRequest request);
    Task<bool> DeleteAsync(int id);
}

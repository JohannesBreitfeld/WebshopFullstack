using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;

namespace Webshop.Application.ServiceInterfaces;

public interface IProductService
{
    Task<ProductsResponse?> GetAllAsync();
    Task<ProductResponse?> GetByIdAsync(int id);
    Task<ProductResponse?> GetByNameAsync(string name);
    Task<ProductResponse?> CreateAsync(CreateProductRequest request);
    Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request);
    Task<bool> DeleteAsync(int id);
}
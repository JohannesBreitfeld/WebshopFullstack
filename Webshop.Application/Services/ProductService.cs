using Microsoft.Extensions.Logging;
using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;
using Webshop.Application.EntityMapping;
using Webshop.Application.ServiceInterfaces;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IUnitOfWork unitOfWork, ILogger<ProductService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ProductsResponse?> GetAllAsync()
    {
        try
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var response = products.MapToResponse();

            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching products");
            return null;
        }
    }

    public async Task<ProductResponse?> GetByIdAsync(int id)
    {
        try
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            var response = product?.MapToResponse();

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching product with ID {id}");
            return null;
        }
    }

    public async Task<ProductResponse?> GetByNameAsync(string name)
    {
        try
        {
            var product = await _unitOfWork.Products.GetByNameAsync(name);
            var response = product?.MapToResponse();
           
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching product with name {name}");
            return null;
        }
    }

    public async Task<ProductResponse?> CreateAsync(CreateProductRequest request)
    {
        try
        {
            var product = request.MapToProduct();

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();

            var response = product.MapToResponse();

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product");
            return null;
        }
    }

    public async Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request)
    {
        try
        {
            var product = request.MapToProduct(id);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveAsync();
            var response = product.MapToResponse();

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product");
            return null;
        }
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        try
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if(product is null)
            {
                return false;
            }

            product.SoftDeleted = true;
            await _unitOfWork.SaveAsync();
        
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product");
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)

    {
        try
        {    
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product is null)
            {
                return false;
            }

            _unitOfWork.Products.Delete(product);

            await _unitOfWork.SaveAsync();

            return true;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error deleting product");
            return false;
        }
    }
}

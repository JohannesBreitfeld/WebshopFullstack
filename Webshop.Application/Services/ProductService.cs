using Microsoft.Extensions.Logging;
using Webshop.Application.ServiceInterfaces;
using Webshop.Domain.Entities;
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

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            return await _unitOfWork.Products.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching products");
            return Enumerable.Empty<Product>();
        }
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        try
        {
            return await _unitOfWork.Products.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching product with ID {id}");
            return null;
        }
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        try
        {
            return await _unitOfWork.Products.GetByNameAsync(name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching product with name {name}");
            return null;
        }
    }

    public async Task<bool> CreateAsync(Product product)
    {
        try
        {
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        try
        {
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveAsync(); 
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product");
            return false;
        }
    }
}

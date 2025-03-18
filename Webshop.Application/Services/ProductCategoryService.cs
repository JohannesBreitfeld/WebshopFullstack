using Microsoft.Extensions.Logging;
using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;
using Webshop.Application.ServiceInterfaces;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductCategoryService> _logger;

    public ProductCategoryService(IUnitOfWork unitOfWork, ILogger<ProductCategoryService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductCategory>> GetAllAsync()
    {
        try
        {
            return await _unitOfWork.ProductCategories.GetAllAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error fetching products");
            return Enumerable.Empty<ProductCategory>();
        }
        
    }

    public async Task<ProductCategory?> GetByIdAsync(int id)
    {
        try
        { 
            return await _unitOfWork.ProductCategories.GetByIdAsync(id); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching ProductCategory with ID {id}");
            return null;
        }

    }

    public async Task<bool> CreateAsync(ProductCategory productCategory)
    {
        try
        {
            await _unitOfWork.ProductCategories.AddAsync(productCategory);
            await _unitOfWork.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating productcategory");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(ProductCategory productCategory)
    {
        try
        {
            _unitOfWork.ProductCategories.Update(productCategory);
            await _unitOfWork.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating ProductCategory");
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var productCategory = await _unitOfWork.Products.GetByIdAsync(id);
            if (productCategory is null)
            {
                return false;
            }

            await _unitOfWork.Products.DeleteAsync(productCategory);
            await _unitOfWork.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting ProductCategory with id {id}");
            return false;
        }
    }
}

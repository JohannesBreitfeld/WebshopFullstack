using Microsoft.Extensions.Logging;
using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;
using Webshop.Application.EntityMapping;
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

    public async Task<ProductCategoriesResponse?> GetAllAsync()
    {
        try
        {
            var products = await _unitOfWork.ProductCategories.GetAllAsync();
            var response = products.MapToResponse();
            return response;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error fetching products");
            return null;
        }
        
    }

    public async Task<ProductCategoryResponse?> GetByIdAsync(int id)
    {
        try
        { 
            var product = await _unitOfWork.ProductCategories.GetByIdAsync(id);
            var response = product?.MapToResponse();
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching ProductCategory with ID {id}");
            return null;
        }
    }

    public async Task<ProductCategoryResponse?> CreateAsync(CreateProductCategoryRequest request)
    {
        try
        {
            var productCategory = request.MapToProductCategory();

            await _unitOfWork.ProductCategories.AddAsync(productCategory);
            await _unitOfWork.SaveAsync();

            var response = productCategory.MapToResponse();
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating productcategory");
            return null;
        }
    }

    public async Task<ProductCategoryResponse?> UpdateAsync(int id, UpdateProductCategoryRequest request)
    {
        try
        {
            var productCategory = request.MapToProductCategory(id);
            _unitOfWork.ProductCategories.Update(productCategory);
            await _unitOfWork.SaveAsync();
            var response = productCategory.MapToResponse();
            
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating ProductCategory");
            return null;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var productCategory = await _unitOfWork.ProductCategories.GetByIdAsync(id);
            if (productCategory is null)
            {
                return false;
            }

            _unitOfWork.ProductCategories.Delete(productCategory);
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

using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;
using Webshop.Domain.Entities;

namespace Webshop.Application.EntityMapping;

public static class ProductCategoryMapping
{
    public static ProductCategory MapToProductCategory(this CreateProductCategoryRequest request)
    {
        return new ProductCategory
        {
            Name = request.Name,
        };
    }

    public static ProductCategoryResponse MapToResponse(this ProductCategory productCategory)
    {
        return new ProductCategoryResponse()
        {
            Id = productCategory.Id,
            Name = productCategory.Name
        };
    }

    public static ProductCategoriesResponse MapToResponse(this IEnumerable<ProductCategory> productCategories)
    {
        return new ProductCategoriesResponse
        {
            Items = productCategories.Select(p => p.MapToResponse())
        };
    }

    public static ProductCategory MapToProductCategory(this UpdateProductCategoryRequest request, int id)
    {
        return new ProductCategory()
        {
            Id = id,
            Name = request.Name
        };
    }
}

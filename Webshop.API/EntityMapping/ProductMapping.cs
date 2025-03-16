using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;
using Webshop.Domain.Entities;

namespace Webshop.API.EntityMapping;

public static class ProductMapping
{
    public static Product MapToProduct(this CreateProductRequest request)
    {
        return new Product
        {
            Name = request.Name,
            Price = request.Price,
            Description = request.Description,
            StockBalance = request.StockBalance,
            Status = request.Status
        };
    }

    public static ProductResponse MapToResponse(this Product product)
    {
        return new ProductResponse()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            StockBalance = product.StockBalance,
            Status = product.Status
        };
    }

    public static ProductsResponse MapToResponse(this IEnumerable<Product> products)
    {
        return new ProductsResponse
        {
            Items = products.Select(p => p.MapToResponse())
        };
    }

    public static Product MapToProduct(this UpdateProductRequest request, int id)
    {
        return new Product()
        {
            Id = id,
            Name = request.Name,
            Price = request.Price,
            Description = request.Description,
            StockBalance = request.StockBalance,
            Status = request.Status
        };
    }

}

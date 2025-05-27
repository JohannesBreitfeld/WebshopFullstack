using Webshop.Domain.Entities;
using Webshop.Domain.Enums;
using Webshop.Infrastructure.MongoDb.Models;

namespace Webshop.Infrastructure.MongoDb.Mapping;

public static class MongoProductMapper
{
    public static Product MapToDomain(this MongoProduct mongo)
    {
        return new Product
        {
            Id = mongo.Id,
            Name = mongo.Name,
            Price = mongo.Price,
            Description = mongo.Description,
            StockBalance = mongo.StockBalance,
            Status = (ProductStatus)mongo.Status,
            ImageUrl = mongo.ImageUrl,
            ProductCategoryId = mongo.ProductCategoryId,
            SoftDeleted = mongo.SoftDeleted
        };
    }

    public static MongoProduct MapToMongo(this Product product)
    {
        return new MongoProduct
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            StockBalance = product.StockBalance,
            Status = (int)product.Status,
            ImageUrl = product.ImageUrl,
            ProductCategoryId = product.ProductCategoryId,
            SoftDeleted = product.SoftDeleted
        };
    }
}


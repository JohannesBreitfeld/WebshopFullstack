using Webshop.Domain.Entities;
using Webshop.Infrastructure.MongoDb.Models;

namespace Webshop.Infrastructure.MongoDb.Mapping;

public static class MongoProductCategoryMapper
{
    public static ProductCategory MapToDomain(this MongoProductCategory mongo)
    {
        return new ProductCategory
        {
            Id = 0,
            Name = mongo.Name,
            Products = new List<Product>() // Hämtas separat
        };
    }

    public static MongoProductCategory MapToMongo(this ProductCategory category)
    {
        return new MongoProductCategory
        {
            Name = category.Name,
            ProductIds = category.Products.Select(p => p.Id.ToString()).ToList()
        };
    }
}


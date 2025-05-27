using Webshop.Domain.Entities;
using Webshop.Infrastructure.MongoDb.Models;

namespace Webshop.Infrastructure.MongoDb.Mapping;

public static class MongoProductCategoryMapper
{
    public static ProductCategory MapToDomain(this MongoProductCategory mongo)
    {
        return new ProductCategory
        {
            Id = mongo.Id,
            Name = mongo.Name,
            Products = new List<Product>()
        };
    }

    public static MongoProductCategory MapToMongo(this ProductCategory category)
    {
        return new MongoProductCategory
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}


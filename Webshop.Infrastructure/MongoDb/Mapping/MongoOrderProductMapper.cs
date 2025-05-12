using Webshop.Domain.Entities;
using Webshop.Infrastructure.MongoDb.Models;

namespace Webshop.Infrastructure.MongoDb.Mapping;

public static class MongoOrderProductMapper
{
    public static OrderProduct MapToDomain(this MongoOrderProduct mongo)
    {
        return new OrderProduct
        {
            ProductId = 0, // Sätt korrekt via Product-mappning
            Quantity = mongo.Quantity
        };
    }

    public static MongoOrderProduct MapToMongo(this OrderProduct op)
    {
        return new MongoOrderProduct
        {
            ProductId = op.ProductId.ToString(),
            Quantity = op.Quantity
        };
    }
}


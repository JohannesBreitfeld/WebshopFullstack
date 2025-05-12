using Webshop.Domain.Entities;
using Webshop.Infrastructure.MongoDb.Models;

namespace Webshop.Infrastructure.MongoDb.Mapping;

public static class MongoOrderProductMapper
{
    public static OrderProduct MapToDomain(this MongoOrderProduct mongo,int orderId)
    {
        return new OrderProduct
        {
            OrderId = orderId,
            ProductId = mongo.ProductId, 
            Quantity = mongo.Quantity
        };
    }

    public static MongoOrderProduct MapToMongo(this OrderProduct op)
    {
        return new MongoOrderProduct
        {
            ProductId = op.ProductId,
            Quantity = op.Quantity
        };
    }
}


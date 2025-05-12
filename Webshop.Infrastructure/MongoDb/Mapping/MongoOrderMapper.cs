using Webshop.Domain.Entities;
using Webshop.Infrastructure.MongoDb.Models;

namespace Webshop.Infrastructure.MongoDb.Mapping;

public static class MongoOrderMapper
{
    public static Order MapToDomain(this MongoOrder mongo)
    {
        return new Order
        {
            Id = mongo.Id, 
            CustomerId = mongo.Id,
            DateTime = mongo.DateTime,
            OrderProducts = mongo.OrderProducts.Select(op => op.MapToDomain(mongo.Id)).ToList()
        };
    }

    public static MongoOrder MapToMongo(this Order order)
    {
        return new MongoOrder
        {
            Id = order.Id,
            CustomerId = order.CustomerId.ToString(),
            DateTime = order.DateTime,
            OrderProducts = order.OrderProducts.Select(op => op.MapToMongo()).ToList()
        };
    }
}

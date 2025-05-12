using Webshop.Domain.Entities;
using Webshop.Infrastructure.MongoDb.Models;

namespace Webshop.Infrastructure.MongoDb.Mapping;

public static class MongoOrderMapper
{
    public static Order MapToDomain(this MongoOrder mongo)
    {
        return new Order
        {
            Id = 0, // Du kan hantera MongoDB-ID som string separat
            CustomerId = 0, // Sätt korrekt via Customer
            DateTime = mongo.DateTime,
            OrderProducts = mongo.OrderProducts.Select(MongoOrderProductMapper.ToDomain).ToList()
        };
    }

    public static MongoOrder MapToMongo(this Order order)
    {
        return new MongoOrder
        {
            CustomerId = order.CustomerId.ToString(),
            DateTime = order.DateTime,
            OrderProducts = order.OrderProducts.Select(MongoOrderProductMapper.ToMongo).ToList()
        };
    }
}

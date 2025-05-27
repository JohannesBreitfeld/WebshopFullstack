using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Webshop.Infrastructure.MongoDb.Models;

public class MongoOrderProduct
{
    [BsonId]
    public int ProductId { get; set; }

    public int Quantity { get; set; }
}

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Webshop.Infrastructure.MongoDb.Models;

public class MongoOrder
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.ObjectId)]
    public string CustomerId { get; set; } = string.Empty;

    public DateTime DateTime { get; set; }

    public List<MongoOrderProduct> OrderProducts { get; set; } = [];
}

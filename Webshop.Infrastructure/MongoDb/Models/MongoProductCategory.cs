using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Webshop.Infrastructure.MongoDb.Models;

public class MongoProductCategory
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public List<string> ProductIds { get; set; } = [];
}

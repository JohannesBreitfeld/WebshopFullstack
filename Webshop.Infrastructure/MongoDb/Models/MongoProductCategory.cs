using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Webshop.Infrastructure.MongoDb.Models;

public class MongoProductCategory
{
    [BsonId]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}

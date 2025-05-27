using MongoDB.Bson.Serialization.Attributes;

namespace Webshop.Infrastructure.MongoDb.SequenceServices;

public class Counter
{
    [BsonId]
    public string Id { get; set; } = null!;

    public int SequenceValue { get; set; }
}
